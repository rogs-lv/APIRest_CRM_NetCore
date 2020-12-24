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
        /// <summary>
        /// Returns connection with open status
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection(string strConnection) {
            try
            {
                // String connectionString = CreateConnectionString();
                DbConnection connection = new SqlConnection(strConnection);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Returns string connection configured in appsettings
        /// </summary>
        /// <returns></returns>
        private string CreateConnectionString() {
            return ""; //this.conf.GetConnectionString("SQLServer");
        }
    }
}
