using System.Threading.Tasks;

namespace UtilityDelta.Server.Domain
{
    public interface IUserService
    {
        string UserName { get; set; }
        Task<bool> CheckCredentials(string userName, string password);
    }
}