using NRMDataManager.library.Models;
using System.Collections.Generic;

namespace NRMDataManager.library.DataAccess
{
    public interface IInventoryData
    {
        List<InventoryModel> GetInventory();
        void SaveInventoryRecord(InventoryModel item);
    }
}