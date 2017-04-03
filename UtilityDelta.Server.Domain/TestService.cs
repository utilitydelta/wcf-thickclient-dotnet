using System;
using System.Threading.Tasks;
using UtilityDelta.Shared.Common;
using UtilityDelta.Shared.Dto;

namespace UtilityDelta.Server.Domain
{
    public class TestService
    {
        private readonly IUserService m_userService;

        public TestService(IUserService userService)
        {
            m_userService = userService;
        }

        public async Task<DtoPerformOperationResult> PerformOperation(DtoPerformOperation dtoIn)
        {
            return await Task.Run(() =>
            {
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
                        throw new ArgumentOutOfRangeException();
                }

                return result;
            });
        }
    }
}