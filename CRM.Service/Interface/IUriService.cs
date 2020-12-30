using CRM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Interface
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}
