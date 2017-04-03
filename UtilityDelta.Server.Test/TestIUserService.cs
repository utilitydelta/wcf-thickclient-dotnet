using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityDelta.Server.Domain;

namespace UtilityDelta.Server.Test
{
    [TestClass]
    public class TestIUserService
    {
        [TestMethod]
        public async Task TestAuthenticationFail()
        {
            var service = new UserService("admin");
            var authResult = await service.CheckCredentials("admin", "password123");
            Assert.IsFalse(authResult);
        }

        [TestMethod]
        public async Task TestAuthenticationPass()
        {
            var service = new UserService("admin");
            var authResult = await service.CheckCredentials("admin", "password");
            Assert.IsTrue(authResult);
        }

        [TestMethod]
        public async Task TestCurrentUser()
        {
            var service = new UserService("blah");
            Assert.AreEqual(await service.GetCurrentUserName(), "blah");
        }
    }
}