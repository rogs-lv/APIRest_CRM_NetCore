using CRM.DAO.DAO;
using CRM.Entity;
using CRM.Models;
using CRM.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service
{
    public class MasterDataService : IMasterDataService
    {
        private readonly MasterDataDAO  masterDAO;
        private readonly IConfiguration _config;
        public MasterDataService(IConfiguration config, MasterDataDAO masterDataDAO) {
            masterDAO   = masterDataDAO;
            _config     = config;
        }
        public Item GetItem(string idItem, string whsCode, string priceList) {
            Item item = masterDAO.GetItem(idItem, whsCode, priceList);
            if (item != null)
                return item;
            else
                return null;
        }
        public List<Item> GetAllItems(ref int countRows, PaginationFilter filter) {
            List<Item> listItem = masterDAO.GetAllItems(ref countRows, filter);
            return listItem;
        }
    }
}
