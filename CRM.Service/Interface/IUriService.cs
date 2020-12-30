using CRM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Interface
{
    public interface IUriService
    {
        /// <summary>
        /// Create new Uri
        /// </summary>
        /// <param name="filter">filters to build new uri</param>
        /// <param name="route">route to build new uri</param>
        /// <returns>new object type uri</returns>
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}
