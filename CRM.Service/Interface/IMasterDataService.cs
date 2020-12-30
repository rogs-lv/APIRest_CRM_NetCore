using CRM.Entity;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Interface
{
    public interface IMasterDataService
    {
        Item GetItem(string idItem, string whsCode, string priceList);
        List<Item> GetAllItems(ref int countRows, PaginationFilter pagination);
    }
}
