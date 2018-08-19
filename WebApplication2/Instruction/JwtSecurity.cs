using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication2.Models;

namespace WebApplication2.Instruction
{
    public class JwtSecurity: IJwtSecurity
    {
        private readonly AppSettings appSettings;

        public JwtSecurity(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public JwtSecurity(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public SymmetricSecurityKey Create()
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Key);
            return new SymmetricSecurityKey(key);
        }
    }

    public interface IJwtSecurity
    {
        SymmetricSecurityKey Create();
    }
}
