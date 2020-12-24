using CRM.DAO.DAO;
using CRM.Entity;
using CRM.Helpers;
using CRM.Models;
using CRM.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRM.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private LoginDAO loginDAO;
        private readonly IConfiguration config;
        private readonly AppSettings _appSettings;
        public AuthenticateService(IConfiguration _config, IOptions<AppSettings> appSettings) {
            this.config = _config;
            this.loginDAO = new LoginDAO(_config);
            this._appSettings = appSettings.Value;
        }
        /// <summary>
        /// Returns token for user authenticate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AuthenticateResponse Authenticate(AuthenticateRequest request) {
            var user = loginDAO.Login(request);
            
            if (user == null) return null;

            var token = generateJwtToken(user);
            return new AuthenticateResponse(token);
        }
        /// <summary>
        /// Returns token valid
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string generateJwtToken(User user)
        {
            // generate token that is valid for hours indicate in appsettings
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new  Claim("userName", user.UserName), new Claim("firstName", user.FirstName), new Claim("lastName", user.LastName)}),
                Expires = DateTime.UtcNow.AddHours(double.Parse(config.GetSection("AppSettings:ExpireHour").Value)),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
