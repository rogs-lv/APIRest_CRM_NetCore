using CRM.DAO.Implements;
using CRM.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DAO
{
    public class DBFactory
    {
        /// <summary>
        /// Returns instance of the indicated database type
        /// </summary>
        /// <param name="dbType">type database</param>
        /// <param name="config">intance of configuration</param>
        /// <returns></returns>
        public static IDBAdapter GetDBAdapter(DBType dbType, IConfiguration config) {
            switch (dbType)
            {
                case DBType.Hana:
                    return new HanaDBAdapter();
                case DBType.MySQL:
                    return new MySQLDBAdapater();
                case DBType.SQLServer:
                    return new SQLDBAdapter();
                default:
                    throw new SystemException("Database type not supported");
            }
        }
        /// <summary>
        /// Returns previously configured IDBAdapter instance
        /// </summary>
        /// <param name="config">intance of configuration</param>
        /// <returns></returns>
        public static IDBAdapter GetDefaultDBAdapter(IConfiguration config) {
            try
            {
                string defaultDBClass = config.GetSection("AppSettings:dbClass").Value;
                Type type = Type.GetType(defaultDBClass);
                return (IDBAdapter)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
