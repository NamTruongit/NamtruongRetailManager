using NRMDataManager.library.Models;
using System.Collections.Generic;

namespace NRMDataManager.library.DataAccess
{
    public interface ISaleData
    {
        List<SaleReportModel> GetSaleReport();
        void SaveSale(SaleModel saleInfo, string cashierId);
    }
}