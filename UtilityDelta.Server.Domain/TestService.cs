using System;
using System.Threading.Tasks;
using log4net;
using UtilityDelta.Shared.Common;
using UtilityDelta.Shared.Dto;
using UtilityDelta.Shared.Interface;

namespace UtilityDelta.Server.Domain
{
    public class TestService : ITestService
    {
        private readonly ILog m_log;
        private readonly IUserService m_userService;

        public TestService(IUserService userService, ILog log)
        {
            m_userService = userService;
            m_log = log;
        }

        public async Task<DtoPerformOperationResult> PerformOperation(DtoPerformOperation dtoIn)
        {
            return await Task.Run(() =>
            {
                m_log.Info(new {dtoIn.OperationType, dtoIn.NumberOne, dtoIn.NumberTwo});

                var result = new DtoPerformOperationResult
                {
                    ExecutedBy = m_userService.UserName
                };

                switch (dtoIn.OperationType)
                {
                    case EnumOperationType.Add:
                        result.Result = dtoIn.NumberOne + dtoIn.NumberTwo;
                        break;
                    case EnumOperationType.Subtract:
                        result.Result = dtoIn.NumberOne - dtoIn.NumberTwo;
                        break;
                    default:
                        m_log.Fatal(new {Error = "Unknown operation type", dtoIn.OperationType});
                        throw new ArgumentOutOfRangeException();
                }

                return result;
            });
        }
    }
}