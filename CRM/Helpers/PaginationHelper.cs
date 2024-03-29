﻿using CRM.Models;
using CRM.Service.Interface;
using CRM.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public class PaginationHelper
    {
        /// <summary>
        /// Create response of type PagedResponse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagedData">rows or data returned</param>
        /// <param name="validFilter">filters for include in uri</param>
        /// <param name="totalRecords">total rows returned</param>
        /// <param name="uriService">interface to create uri</param>
        /// <param name="route">route of controller</param>
        /// <returns>return response of type PagedResponse</returns>
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.WhsCode, validFilter.PriceList), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.WhsCode, validFilter.PriceList), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize, validFilter.WhsCode, validFilter.PriceList), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize, validFilter.WhsCode, validFilter.PriceList), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
