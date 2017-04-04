using System;

namespace UtilityDelta.Client.ServiceLocator
{
    public interface IDynamicProxyBuilder
    {
        /// <summary>
        ///     Returns a dynamically compiled proxy type that inherites from the provided classToProxy interface
        /// </summary>
        /// <param name="classToProxy"></param>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        Type BuildProxy(Type classToProxy, string proxyName);
    }
}