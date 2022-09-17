using Microsoft.Extensions.Configuration;
using NRMDataManager.library.Internal.DataAccess;
using NRMDataManager.library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDataManager.library.DataAccess
{
    public class InventoryData
    {
        private readonly IConfiguration _config;

        public InventoryData(IConfiguration config)
        {
            _config = config;
        }
        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var output = sql.LoadData<InventoryModel, dynamic>("spInventory_GetAll", new { }, "NRMData");
            return output; 
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            sql.SaveData("spInventory_Insert", item, "NRMData");

        }
    }
}
