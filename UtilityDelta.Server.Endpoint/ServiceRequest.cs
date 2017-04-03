using System.IO;
using System.ServiceModel;

namespace UtilityDelta.Server.Endpoint
{
    /// <summary>
    ///     Used in the SOA Service implementation by the client
    ///     to request a service from the server.
    /// </summary>
    [MessageContract]
    public class ServiceRequest
    {
        /// <summary>
        ///     What assembly contains the interface for the service
        /// </summary>
        [MessageHeader]
        public string InterfaceAssemblyName;

        /// <summary>
        ///     The name of the interface in an assembly. This interface must be
        ///     hooked up in Autofac to resolve into a concrete implemenation
        /// </summary>
        [MessageHeader]
        public string InterfaceClassName;

        /// <summary>
        ///     The service to call on the implementation of the provided interface.
        /// </summary>
        [MessageHeader]
        public string MethodName;

        /// <summary>
        ///     Parameters to be passed into the method's implementation, in serialized form.
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public Stream RequestParameter;
    }
}