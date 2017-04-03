﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UtilityDelta.Client.ServiceLocator.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IService")]
    internal interface IService {
        
        // CODEGEN: Generating message contract since the wrapper name (ServiceRequest) of message ServiceRequest does not match the default value (ProcessService)
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ProcessService", ReplyAction="http://tempuri.org/IService/ProcessServiceResponse")]
        UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceResponse ProcessService(UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ProcessService", ReplyAction="http://tempuri.org/IService/ProcessServiceResponse")]
        System.Threading.Tasks.Task<UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceResponse> ProcessServiceAsync(UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ServiceRequest", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    internal partial class ServiceRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string InterfaceAssemblyName;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string InterfaceClassName;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string MethodName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream RequestParameter;
        
        public ServiceRequest() {
        }
        
        public ServiceRequest(string InterfaceAssemblyName, string InterfaceClassName, string MethodName, System.IO.Stream RequestParameter) {
            this.InterfaceAssemblyName = InterfaceAssemblyName;
            this.InterfaceClassName = InterfaceClassName;
            this.MethodName = MethodName;
            this.RequestParameter = RequestParameter;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ServiceResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    internal partial class ServiceResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream Response;
        
        public ServiceResponse() {
        }
        
        public ServiceResponse(System.IO.Stream Response) {
            this.Response = Response;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    internal interface IServiceChannel : UtilityDelta.Client.ServiceLocator.ServiceReference.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    internal partial class ServiceClient : System.ServiceModel.ClientBase<UtilityDelta.Client.ServiceLocator.ServiceReference.IService>, UtilityDelta.Client.ServiceLocator.ServiceReference.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceResponse UtilityDelta.Client.ServiceLocator.ServiceReference.IService.ProcessService(UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceRequest request) {
            return base.Channel.ProcessService(request);
        }
        
        public System.IO.Stream ProcessService(string InterfaceAssemblyName, string InterfaceClassName, string MethodName, System.IO.Stream RequestParameter) {
            UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceRequest inValue = new UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceRequest();
            inValue.InterfaceAssemblyName = InterfaceAssemblyName;
            inValue.InterfaceClassName = InterfaceClassName;
            inValue.MethodName = MethodName;
            inValue.RequestParameter = RequestParameter;
            UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceResponse retVal = ((UtilityDelta.Client.ServiceLocator.ServiceReference.IService)(this)).ProcessService(inValue);
            return retVal.Response;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceResponse> UtilityDelta.Client.ServiceLocator.ServiceReference.IService.ProcessServiceAsync(UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceRequest request) {
            return base.Channel.ProcessServiceAsync(request);
        }
        
        public System.Threading.Tasks.Task<UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceResponse> ProcessServiceAsync(string InterfaceAssemblyName, string InterfaceClassName, string MethodName, System.IO.Stream RequestParameter) {
            UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceRequest inValue = new UtilityDelta.Client.ServiceLocator.ServiceReference.ServiceRequest();
            inValue.InterfaceAssemblyName = InterfaceAssemblyName;
            inValue.InterfaceClassName = InterfaceClassName;
            inValue.MethodName = MethodName;
            inValue.RequestParameter = RequestParameter;
            return ((UtilityDelta.Client.ServiceLocator.ServiceReference.IService)(this)).ProcessServiceAsync(inValue);
        }
    }
}