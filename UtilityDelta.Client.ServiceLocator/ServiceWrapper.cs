using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using UtilityDelta.Client.ServiceLocator.ServiceReference;
using UtilityDelta.Shared.Common;

namespace UtilityDelta.Client.ServiceLocator
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly ISerializer m_serializer;
        private readonly IServer m_server;
        private ServiceClient m_client;

        public ServiceWrapper(ISerializer serializer, IServer server)
        {
            m_serializer = serializer;
            m_server = server;
        }

        /// <summary>
        ///     Programmatically setup the binding and authentication details to connect
        ///     to the remote WCF endpoint.
        /// </summary>
        private void InitialiseService()
        {
            m_client?.Abort();

            var tcpBinding = new NetTcpBinding(SecurityMode.TransportWithMessageCredential)
            {
                TransferMode = TransferMode.Streamed,
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = long.MaxValue,
                MaxBufferPoolSize = long.MaxValue,
                ReaderQuotas =
                {
                    MaxArrayLength = int.MaxValue,
                    MaxDepth = int.MaxValue,
                    MaxStringContentLength = int.MaxValue
                },
                CloseTimeout = new TimeSpan(0, 0, 30, 0),
                OpenTimeout = new TimeSpan(0, 0, 30, 0),
                ReceiveTimeout = new TimeSpan(0, 0, 30, 0),
                SendTimeout = new TimeSpan(0, 0, 30, 0)
            };

            tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;
            tcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

            var remoteAddress =
                new EndpointAddress($"{ServiceConstants.NettcpProtocol}{m_server.Server}{ServiceConstants.UsernameEndpoint}");
            m_client = new ServiceClient(tcpBinding, remoteAddress);

            Debug.Assert(m_client.ClientCredentials != null, "_client.ClientCredentials != null");

            m_client.ClientCredentials.UserName.UserName = m_server.Username;
            m_client.ClientCredentials.UserName.Password = m_server.Password;
        }

        public async Task<TReturn> ProcessService<TReturn>(
            string interfaceAssemblyName, string interfaceClassName, string methodName, object requestParameter)
        {
            var requestParameterStream = await m_serializer.Serialize(requestParameter);

            ServiceResponse response;
            try
            {
                if (m_client == null)
                {
                    InitialiseService();
                }

                response = await m_client.ProcessServiceAsync(
                    interfaceAssemblyName, interfaceClassName, methodName, requestParameterStream);
            }
            catch (MessageSecurityException)
            {
                try
                {
                    InitialiseService();
                    response = await m_client.ProcessServiceAsync(
                        interfaceAssemblyName, interfaceClassName, methodName, requestParameterStream);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }

            return (TReturn) await m_serializer.DeSerialize(response.Response, typeof(TReturn));
        }
    }
}