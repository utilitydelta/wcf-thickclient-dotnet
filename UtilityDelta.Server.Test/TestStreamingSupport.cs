using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityDelta.Shared.Interface;

namespace UtilityDelta.Server.Test
{
    [TestClass]
    public class TestStreamingSupport
    {
        [TestMethod]
        public void AllServicesSupportStreaming()
        {
            var assembly = Assembly.GetAssembly(typeof(ITestService));
            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    Assert.IsTrue(method.ReturnParameter != null
                                  && method.ReturnParameter.ParameterType != typeof(Task)
                                  && method.ReturnParameter.ParameterType != typeof(void));
                    Assert.IsTrue(method.GetParameters().Length == 1);
                }
            }
        }
    }
}