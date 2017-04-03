using System.Threading.Tasks;

namespace UtilityDelta.Server.Domain
{
    public class UserService : IUserService
    {
        public string UserName { get; set; }

        public async Task<bool> CheckCredentials(string userName, string password)
        {
            //TODO: Replace this with your authentication routine
            return await Task.Run(() => userName == "admin" && password == "password");
        }
    }
}