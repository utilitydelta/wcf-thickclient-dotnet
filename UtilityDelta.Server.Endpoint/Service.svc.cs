using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using System.Web.Hosting;
using log4net.Config;
using UtilityDelta.Server.Dependencies;
using UtilityDelta.Server.Domain;
using UtilityDelta.Shared.Common;

namespace UtilityDelta.Server.Endpoint
{
    /// <summary>
    /// Support multiple concurrent requests - handled via an IIS application pool
    /// </summary>
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.PerSession,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service : IService
    {
        private static readonly ISerializer m_serializer;
        private static readonly log4net.ILog m_log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string UserNameCustomBinding = "UserNameCustomBinding";
        private const string Log4NetConfigSetting = "log4net_config";

        static Service()
        {
            //Avoid paying the cost of precompiling the serializer every time a call is made
            //The serializer doesn't store any state anyway :)
            using (var dependencyLocator = new DependencyLocator())
            {
                m_serializer = dependencyLocator.GetService<ISerializer>();
            }
        }

        /// <summary>
        /// This is the setup of the WCF service - it is only called once by IIS
        /// </summary>
        /// <param name="config"></param>
        public static void Configure(ServiceConfiguration config)
        {
            Log4NetSetup();

            var fullQualifiedMachineName = Dns.GetHostEntry(Environment.MachineName).HostName;
            var contractDesc = ContractDescription.GetContract(typeof(IService));

            var usernameEndpoint = new ServiceEndpoint(
                            contractDesc,
                            new CustomBinding(UserNameCustomBinding),
                            new EndpointAddress($"{ServiceConstants.NettcpProtocol}{fullQualifiedMachineName}{ServiceConstants.UsernameEndpoint}"));

            foreach (var op in usernameEndpoint.Contract.Operations)
            {
                var dataContractBehavior =
                    op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }

            usernameEndpoint.Behaviors.Add(new DispatcherSynchronizationBehavior { AsynchronousSendEnabled = true });
            config.AddServiceEndpoint(usernameEndpoint);

            config.Description.Behaviors.Add(new ServiceCredentials
            {
                UserNameAuthentication =
                {
                    UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom,
                    CustomUserNamePasswordValidator = new CustomAuthentication()
                }
            });

            //Designed to use with a self-signed certificate issued by the machine the endpoint is hosted on
            //Could change this to 'find by thumbprint' to find a specific certificate if required
            config.Credentials.ServiceCertificate.SetCertificate(
                StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByIssuerName, fullQualifiedMachineName);

            config.Description.Behaviors.Add(new ServiceSecurityAuditBehavior
            {
                AuditLogLocation = AuditLogLocation.Application,
                MessageAuthenticationAuditLevel = AuditLevel.Failure,
                ServiceAuthorizationAuditLevel = AuditLevel.Failure,
                SuppressAuditFailure = true
            });
            config.Description.Behaviors.Add(new ServiceMetadataBehavior
            {
                HttpsGetEnabled = true,
                HttpGetEnabled = true
            });
            config.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
        }

        private static void Log4NetSetup()
        {
            var path = ConfigurationManager.AppSettings[Log4NetConfigSetting];
            var fullPath = HostingEnvironment.MapPath("~/" + path);
            Debug.Assert(fullPath != null, "fullPath != null");
            var fileInfo = new FileInfo(fullPath);

            XmlConfigurator.ConfigureAndWatch(fileInfo);
        }

        public async Task<ServiceResponse> ProcessService(ServiceRequest request)
        {
            ServiceResponse response = null;

            try
            {
                using (var dependencyLocator = new DependencyLocator())
                {
                    //Set current username for services
                    var userService = dependencyLocator.GetService<IUserService>();
                    userService.UserName = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;

                    var resultFromImplementation = await ExecuteServiceCall(request, dependencyLocator);
                    if (resultFromImplementation != null)
                    {
                        response = new ServiceResponse
                        {
                            Response = await m_serializer.Serialize(resultFromImplementation)
                        };
                    }
                }
            }
            catch (Exception e)
            {
                m_log.Fatal("Failed calling service.", e.InnerMostException());
                throw;
            }

            return response;
        }

        private static async Task<object> ExecuteServiceCall(ServiceRequest request, IDependencyLocator dependencyLocator)
        {
            var newImplementationClass = CreateDomainClass(request, dependencyLocator);

            m_log.Info(
                new
                {
                    request.InterfaceAssemblyName,
                    request.InterfaceClassName,
                    request.MethodName,
                    ConcreteClassName = newImplementationClass.GetType().FullName
                });

            //Get the method we need to call from the implementation class
            //and then invoke it, passing our parameters.
            var methodToExecute = newImplementationClass.GetType()
                .GetMethod(request.MethodName);

            //De-serialize all the parameters that were given to 
            var parameterTypes = methodToExecute.GetParameters();
            if (parameterTypes.Length != 1)
            {
                m_log.Fatal(new { nbrParams = parameterTypes.Length });
                throw new Exception("Requests through service must specify exactly one parameter. This is allow TCP streaming for large datasets.");
            }
            var parameters = new List<object>
            {
                await m_serializer.DeSerialize(request.RequestParameter, parameterTypes[0].ParameterType)
            };

            //Run the method to get results from domain
            //If you get a crash here that means that the service you called raised an exception somewhere.
            var task = (Task)methodToExecute.Invoke(newImplementationClass, parameters.ToArray());

            await task;

            return ((dynamic)task).Result;
        }

        private static object CreateDomainClass(ServiceRequest request, IDependencyLocator dependencyLocator)
        {
            var businessAssembly = Assembly.Load(request.InterfaceAssemblyName);

            var interfaceClassName = request.InterfaceAssemblyName + "." + request.InterfaceClassName;
            var classType = businessAssembly.GetType(interfaceClassName, true, true);

            return dependencyLocator.GetService(classType);
        }
    }
}