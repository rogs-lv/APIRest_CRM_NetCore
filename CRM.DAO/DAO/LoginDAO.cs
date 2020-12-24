using CRM.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Data;
using CRM.Entity;

namespace CRM.DAO.DAO
{
    public class LoginDAO
    {
        //private readonly IConfiguration config;
        IDBAdapter dBAdapter;
        private readonly string strConnection;
        public LoginDAO(IConfiguration _config) {
            //config      = _config;
            dBAdapter       = DBFactory.GetDefaultDBAdapter(_config);
            strConnection   = _config.GetConnectionString("SQLServer");
        }
        /// <summary>
        /// Returns data user if exist in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User Login(AuthenticateRequest user) {
            IDbConnection connection = dBAdapter.GetConnection(strConnection);
            try
            {
                User userLogin = connection.Query<User>($"EXEC \"IDS_LoginPrueba\" '{user.UserName}','{user.Password}'").FirstOrDefault();
                return userLogin;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
