using CRM.Models;
using CRM.Service.Interface;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service
{
    public class UriService: IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri) {
            this._baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationFilter filter, string route) {
            Uri _endPointUri = new Uri(string.Concat(_baseUri, route));
            string modifiedUri = QueryHelpers.AddQueryString(_endPointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "whsCode", filter.WhsCode);
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "priceList", filter.PriceList);
            return new Uri(modifiedUri);
        }
    }
}
