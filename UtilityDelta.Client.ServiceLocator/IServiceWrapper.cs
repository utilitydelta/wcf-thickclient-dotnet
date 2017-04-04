using System.Threading.Tasks;

namespace UtilityDelta.Client.ServiceLocator
{
    public interface IServiceWrapper
    {
        Task<TReturn> ProcessService<TReturn>(string interfaceAssemblyName, string interfaceClassName, string methodName, object requestParameter);
    }
}