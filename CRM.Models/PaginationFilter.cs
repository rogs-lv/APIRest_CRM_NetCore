using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Models
{
    public class PaginationFilter
    {
        public int      PageNumber  { get; set; }
        public int      PageSize    { get; set; }
        public string   WhsCode     { get; set; }
        public string   PriceList   { get; set; }
        public PaginationFilter() {
            this.PageNumber = 1;
            this.PageSize   = 10;
            this.WhsCode    = "";
            this.PriceList  = "";
        }
        public PaginationFilter(int pageNumber, int pageSize, string whsCode, string priceList) {
            // filter such that the maximum page size a user can request for is 10
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            // If he/she requests a page size of 1000, it would default back to 10.
            this.PageSize   = pageSize > 10 ? 10 : pageSize;
            this.WhsCode    = String.IsNullOrEmpty(whsCode) ? "" : whsCode;
            this.PriceList  = String.IsNullOrEmpty(priceList) ? "" : priceList;
        }
    }
}
