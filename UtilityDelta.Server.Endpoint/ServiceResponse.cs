using System.IO;
using System.ServiceModel;

namespace UtilityDelta.Server.Endpoint
{
    /// <summary>
    ///     Used in the SOA Service implementation by the client
    ///     to request a service from the server.
    /// </summary>
    [MessageContract]
    public class ServiceResponse
    {
        /// <summary>
        ///     Parameters to be passed into the method's implementation, in serialized form.
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public Stream Response;
    }
}