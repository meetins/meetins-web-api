using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Meetins.Core.Options
{
    public class RefreshTokenOptions
    {
        public const string ISSUER = "MeetinsAuthServer"; // издатель токена
        public const string AUDIENCE = "MeetinsAuthClient"; // потребитель токена
        const string KEY = "Assupion#cret_6Jk8lLkey!321";   // ключ для шифрации
        public const int LIFETIME = 131400; // время жизни токена в минутах
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
