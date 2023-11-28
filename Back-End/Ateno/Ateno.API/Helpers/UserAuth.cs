using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Ateno.API.Helpers
{
    public static class UserAuth
    {
        //Gambiarra para solucionar problema de autenticação da requests pelo Identity
        public static string GetUserAuth(HttpRequest request)
        {
            try
            {
                string token = request.Headers
                                      .Where(x => x.Key == "Authorization")
                                      .Select(x => x.Value)
                                      .FirstOrDefault()
                                      .ToString()
                                      .Split(" ")[1];

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                ClaimsPrincipal userLogged = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = LifetimeValidator,
                    IssuerSigningKey =
                       new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ateno Secret Token JWT For Auth The User In Application, This Key Is Unique")),
                    ClockSkew = TimeSpan.FromMinutes(30),
                    ValidIssuer = "Emissor",
                    ValidAudience = "Publico"
                }, out SecurityToken validatedToken);

                if (!userLogged.Identity.IsAuthenticated)
                    return null;

                return userLogged.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) != null ?
                         userLogged.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value :
                         null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }

            return false;
        }
    }
}
