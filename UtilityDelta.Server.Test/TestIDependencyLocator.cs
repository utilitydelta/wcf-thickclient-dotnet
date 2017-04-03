using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityDelta.Server.Dependencies;
using UtilityDelta.Server.Domain;
using UtilityDelta.Shared.Common;
using UtilityDelta.Shared.Interface;

namespace UtilityDelta.Server.Test
{
    [TestClass]
    public class TestIDependencyLocator
    {
        [TestMethod]
        public void TestDomainConcreteClass()
        {
            var service = new DependencyLocator();
            var concreteClass = service.GetService<ITestService>();
            Assert.IsInstanceOfType(concreteClass, typeof(TestService));
        }

        [TestMethod]
        public void TestSerializerConcreteClass()
        {
            var service = new DependencyLocator();
            var concreteClass = service.GetService<ISerializer>();
            Assert.IsInstanceOfType(concreteClass, typeof(Serializer));
        }
    }
}