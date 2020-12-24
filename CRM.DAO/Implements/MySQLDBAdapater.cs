using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CRM.DAO.Implements
{
    public class MySQLDBAdapater : IDBAdapter
    {
        public IDbConnection GetConnection(string strConnection) {
            return null;
        }
    }
}
