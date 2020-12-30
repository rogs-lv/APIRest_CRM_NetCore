using CRM.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Data;
using CRM.Entity;
using Microsoft.Extensions.Logging;

namespace CRM.DAO.DAO
{
    public class LoginDAO
    {
        IDBAdapter dBAdapter;
        private readonly ILogger<LoginDAO> _logger;
        /// <summary>
        /// Constructor class LoginDAO
        /// </summary>
        /// <param name="_config">Instance of configuration</param>
        /// <param name="logger">Instance Ilogger specifying the type class</param>
        public LoginDAO(IConfiguration _config, ILogger<LoginDAO> logger) {
            _logger         = logger;
            dBAdapter       = DBFactory.GetDefaultDBAdapter(_config);
        }
        /// <summary>
        /// Returns data user if exist in database
        /// </summary>
        /// <param name="user">Data of the user who made the request</param>
        /// <returns>Class type User</returns>
        public User Login(AuthenticateRequest user) {
            IDbConnection connection = dBAdapter.GetConnection();
            try
            {
                string storeProcedure = "IDS_LoginPrueba";
                User userLogin = connection.Query<User>(
                        storeProcedure, 
                        new { Usuario = $"{user.UserName}", Password = $"{user.Password}" },
                        commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
                return userLogin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
