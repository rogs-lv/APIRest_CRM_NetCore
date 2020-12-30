using CRM.Entity;
using CRM.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRM.DAO.DAO
{
    public class MasterDataDAO
    {
        IDBAdapter dBAdapter;
        private readonly ILogger<MasterDataDAO> _logger;
        public MasterDataDAO(IConfiguration config, ILogger<MasterDataDAO> logger) {
            _logger = logger;
            dBAdapter = DBFactory.GetDefaultDBAdapter(config);
        }
        public Item GetItem(string idItem, string whsCode, string priceList) {
            IDbConnection connection = dBAdapter.GetConnection();
            try
            {
                string storeProcedure = "IDS_GetDatosMaestros";
                Item item = connection.Query<Item>(
                        storeProcedure,
                        new { Type = $"5", Param1 = $"{whsCode}", Param2 = $"{priceList}", Param3 = $"{idItem}" },
                        commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        public List<Item> GetAllItems(ref int countRows, PaginationFilter validFilter) {
            IDbConnection connection = dBAdapter.GetConnection();
            try
            {
                string storeProcedure = "IDS_GetDatosMaestros";
                var item = connection.Query<Item>(
                        storeProcedure,
                        new { Type = $"6", Param1 = $"{validFilter.WhsCode}", Param2 = $"{validFilter.PriceList}", Skip = ((validFilter.PageNumber - 1) * validFilter.PageSize), Take = validFilter.PageSize },
                        commandType: CommandType.StoredProcedure)
                    .ToList();
                countRows = connection.Query<int>(
                        storeProcedure,
                        new { Type = $"7", Param1 = $"{validFilter.WhsCode}", Param2 = $"{validFilter.PriceList}" },
                        commandType: CommandType.StoredProcedure)
                    .First();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
