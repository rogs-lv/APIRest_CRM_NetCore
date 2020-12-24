using CRM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings) {
            this._next = next;
            this._appSettings = appSettings.Value;
        }
        public async Task Invoke(HttpContext context) {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            
            if (!String.IsNullOrEmpty(token))
                validTokenHeader(token);
            
            await _next(context);
        }

        private void validTokenHeader(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = "", // Validacion de emisor
                    ValidAudience = "", // Validacion de receptero
                    ClockSkew = TimeSpan.Zero // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                }, out SecurityToken validateToken);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
