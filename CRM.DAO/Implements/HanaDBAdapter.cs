using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CRM.DAO.Implements
{
    public class HanaDBAdapter : IDBAdapter
    {
        public IDbConnection GetConnection() {
            return null;
        }
    }
}
