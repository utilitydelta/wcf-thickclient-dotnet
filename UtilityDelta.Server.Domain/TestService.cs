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
            var result = new DtoPerformOperationResult
            {
                ExecutedBy = await m_userService.GetCurrentUserName()
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
        }
    }
}