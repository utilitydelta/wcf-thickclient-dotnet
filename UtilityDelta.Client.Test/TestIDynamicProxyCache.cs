using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UtilityDelta.Client.ServiceLocator;
using UtilityDelta.Shared.Dto;
using UtilityDelta.Shared.Interface;

namespace UtilityDelta.Client.Test
{
    [TestClass]
    public class TestDynamicProxyCache
    {
        [TestMethod]
        public void TestCacheLogic()
        {
            var dynamicProxyBuilder = new Mock<IDynamicProxyBuilder>();
            var processService = new Mock<IServiceWrapper>();
            var mockTestService = new Mock<ITestService>();

            var service = new DynamicProxyCache(dynamicProxyBuilder.Object, processService.Object);

            dynamicProxyBuilder.Setup(x => x.BuildProxy(typeof(ITestService), It.IsAny<string>()))
                .Returns(() => typeof(TestClass));
            var proxyInstance = service.GetProxyInstance<ITestService>();

            var dtoIn = new DtoPerformOperation();
            var dtoOut = new DtoPerformOperationResult
            {
                ExecutedBy = "kldslk",
                Result = 999
            };

            processService.Setup(
                    x => x.ProcessService<DtoPerformOperationResult>(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(), dtoIn))
                .ReturnsAsync(() => dtoOut);

            var result = proxyInstance.PerformOperation(dtoIn).Result;

            Assert.AreEqual(result.ExecutedBy, dtoOut.ExecutedBy);
            Assert.AreEqual(result.Result, dtoOut.Result);

            dynamicProxyBuilder.Verify(x => x.BuildProxy(typeof(ITestService), It.IsAny<string>()), Times.Once);

            //Get from cache this time
            var proxyInstance2 = service.GetProxyInstance<ITestService>();
            Assert.AreSame(proxyInstance2, proxyInstance);

            dynamicProxyBuilder.Verify(x => x.BuildProxy(typeof(ITestService), It.IsAny<string>()), Times.Once);
        }

        private class TestClass : ITestService
        {
            private readonly IServiceWrapper _processService;

            public TestClass(IServiceWrapper service)
            {
                _processService = service;
            }

            public async Task<DtoPerformOperationResult> PerformOperation(DtoPerformOperation dtoIn)
            {
                return await _processService.ProcessService<DtoPerformOperationResult>("", "", "", dtoIn);
            }
        }
    }
}