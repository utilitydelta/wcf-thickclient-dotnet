using System.Threading.Tasks;

namespace UtilityDelta.Server.Domain
{
    public interface IUserService
    {
        Task<bool> CheckCredentials(string userName, string password);
        string UserName { get; set; }
    }
}