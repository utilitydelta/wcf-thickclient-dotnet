using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityDelta.Client.ServiceLocator;
using UtilityDelta.Shared.Interface;

namespace UtilityDelta.Client.Test
{
    [TestClass]
    public class TestDynamicProxyBuilder
    {
        [TestMethod]
        public void TestAllServiceInterfacesCanCompileProxy()
        {
            var service = new DynamicProxyBuilder();
            var assembly = Assembly.GetAssembly(typeof(ITestService));
            foreach (var type in assembly.GetTypes())
            {
                var proxyType = service.BuildProxy(type, "testProxy");
                Assert.AreEqual(proxyType.GetInterfaces().Length, 1);
                Assert.AreEqual(proxyType.GetInterfaces()[0].FullName, type.FullName);
            }
        }
    }
}