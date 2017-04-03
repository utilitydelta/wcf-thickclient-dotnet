using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UtilityDelta.Server.Dependencies;
using UtilityDelta.Server.Domain;

namespace UtilityDelta.Server.Endpoint
{
    public class CustomAuthentication : UserNamePasswordValidator
    {
        private static readonly log4net.ILog m_log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void Validate(string userName, string password)
        {
            using (var dependencyLocator = new DependencyLocator())
            {
                var userService = dependencyLocator.GetService<IUserService>();
                if (!userService.CheckCredentials(userName, password).Result)
                {
                    m_log.Warn(new { userName, result = "Failed" });
                    throw new SecurityTokenException("Invalid credentials");
                }
                m_log.Info(new { userName, result = "Success" });
            }
        }
    }
}