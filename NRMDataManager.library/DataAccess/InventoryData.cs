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
    public class InventoryData : IInventoryData
    {
        private readonly ISqlDataAccess _dataAccess;

        public InventoryData(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public List<InventoryModel> GetInventory()
        {
            var output = _dataAccess.LoadData<InventoryModel, dynamic>("spInventory_GetAll", new { }, "NRMData");
            return output;
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            _dataAccess.SaveData("spInventory_Insert", item, "NRMData");

        }
    }
}
