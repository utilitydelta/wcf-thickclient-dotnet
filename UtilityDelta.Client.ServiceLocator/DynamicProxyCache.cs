using System;
using System.Collections.Generic;

namespace UtilityDelta.Client.ServiceLocator
{
    public class DynamicProxyCache : IDynamicProxyCache
    {
        private const string ProxyClassPostfix = "Proxy";

        private readonly Dictionary<string, object> m_cache = new Dictionary<string, object>();
        private readonly IDynamicProxyBuilder m_dynamicProxyBuilder;
        private readonly IProcessService m_processService;

        public DynamicProxyCache(IDynamicProxyBuilder dynamicProxyBuilder, IProcessService processService)
        {
            m_processService = processService;
            m_dynamicProxyBuilder = dynamicProxyBuilder;
        }

        public T GetProxyInstance<T>()
        {
            var classType = typeof(T);
            if (!classType.IsInterface)
            {
                throw new Exception("Only interfaces can be used with the ServiceLocator.");
            }

            var serviceClassProxyName = classType.Name + ProxyClassPostfix;
            if (m_cache.TryGetValue(serviceClassProxyName, out object newProxyClass))
            {
                return (T) newProxyClass;
            }

            //Create a new assembly (dll) in memory which contains the interface implementation
            //and cache this for next time
            var proxyType = m_dynamicProxyBuilder.BuildProxy(classType, serviceClassProxyName);
            var proxyInstance = Activator.CreateInstance(proxyType, m_processService);
            m_cache.Add(serviceClassProxyName, proxyInstance);

            return (T) proxyInstance;
        }
    }
}