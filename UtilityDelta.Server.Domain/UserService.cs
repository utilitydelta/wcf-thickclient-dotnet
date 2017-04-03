using System.Threading.Tasks;

namespace UtilityDelta.Server.Domain
{
    public class UserService : IUserService
    {
        private readonly string m_userName;

        public UserService(string userName)
        {
            m_userName = userName;
        }

        public async Task<bool> CheckCredentials(string userName, string password)
        {
            //TODO: Replace this with your authentication routine
            return await Task.Run(() => userName == "admin" && password == "password");
        }

        public async Task<string> GetCurrentUserName()
        {
            return await Task.Run(() => m_userName);
        }
    }
}