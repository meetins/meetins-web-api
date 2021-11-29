using Meetins.BLL.Interfaces;
using Meetins.BLL.Options;
using Meetins.DAL.Entities;
using Meetins.DAL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _db;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        public async Task<string> GenerateTokenAsync(string email, string password)
        {
            var identity = await GetIdentityAsync(email, password);

            if (identity != null)
            {
                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return encodedJwt;
            }

            return null;
        }
        private async Task<ClaimsIdentity> GetIdentityAsync(string email, string password)
        {
            UserEntity user = await _db.Users.IdentityUserAsync(email, password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.FirstName+user.LastName)
                };

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            // если пользователя не найдено
            return null;
        }
    }
}
