using System.Threading.Tasks;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UtilityDelta.Server.Domain;
using UtilityDelta.Shared.Common;
using UtilityDelta.Shared.Dto;

namespace UtilityDelta.Server.Test
{
    [TestClass]
    public class TestITestService
    {
        [TestMethod]
        public async Task TestAdd()
        {
            var log = new Mock<ILog>();

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.UserName).Returns(() => "testUser");

            var service = new TestService(userService.Object, log.Object);
            var result = await service.PerformOperation(new DtoPerformOperation
            {
                NumberOne = 22,
                NumberTwo = 5,
                OperationType = EnumOperationType.Add
            });

            Assert.AreEqual(result.ExecutedBy, "testUser");
            Assert.AreEqual(result.Result, 27);
        }

        [TestMethod]
        public async Task TestSubtract()
        {
            var log = new Mock<ILog>();

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.UserName).Returns(() => "testUser");

            var service = new TestService(userService.Object, log.Object);
            var result = await service.PerformOperation(new DtoPerformOperation
            {
                NumberOne = 22,
                NumberTwo = 5,
                OperationType = EnumOperationType.Subtract
            });

            Assert.AreEqual(result.ExecutedBy, "testUser");
            Assert.AreEqual(result.Result, 17);
        }
    }
}