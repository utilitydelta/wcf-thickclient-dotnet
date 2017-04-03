using System.Threading.Tasks;
using UtilityDelta.Shared.Dto;

namespace UtilityDelta.Shared.Interface
{
    public interface ITestService
    {
        Task<DtoPerformOperationResult> PerformOperation(DtoPerformOperation dtoIn);
    }
}