using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace CRM.DAO.Implements
{
    public class SQLDBAdapter : IDBAdapter
    {
        private readonly IConfiguration _config;
        public SQLDBAdapter(IConfiguration config) {
            this._config = config;
        }
        /// <summary>
        /// Returns connection with open status
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection() {
            try
            {
                String connectionString = CreateConnectionString();
                DbConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Returns string connection configured in appsettings
        /// </summary>
        /// <returns></returns>
        private string CreateConnectionString() {
            return this._config.GetConnectionString("SQLServer");
        }
    }
}
