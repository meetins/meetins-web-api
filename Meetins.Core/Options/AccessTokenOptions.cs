using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Meetins.Core.Options
{
    public class AccessTokenOptions
    {
        public const string ISSUER = "MeetinsAuthServer"; // издатель токена
        public const string AUDIENCE = "MeetinsAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const double LIFETIME = 30; // время жизни токена в минутах
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
