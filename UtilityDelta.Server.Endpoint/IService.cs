using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace UtilityDelta.Server.Endpoint
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Task<ServiceResponse> ProcessService(ServiceRequest vServiceRequest);
    }
}