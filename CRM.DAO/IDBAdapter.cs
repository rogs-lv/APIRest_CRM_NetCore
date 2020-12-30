using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CRM.DAO
{
    public interface IDBAdapter
    {
        IDbConnection GetConnection();
    }
}
