using NRMDataManager.library.Internal.DataAccess;
using NRMDataManager.library.Models;
using NRMDesktopUI.library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDataManager.library.DataAccess
{
    public class SaleData : ISaleData
    {
        private readonly IProductData _productData;
        private readonly ISqlDataAccess _sql;

        public SaleData(IProductData productData,ISqlDataAccess sql)
        {
            _productData = productData;
            _sql = sql;
        }

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            //Make this solid/dry/better
            //Start filling in the models we will save to the database
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            var taxRate = ConfigHelper.GetTaxRate() / 100;
            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProducID = item.ProducID,
                    Quantity = item.Quantity
                };

                var productInfo = _productData.GetProductByID(detail.ProducID);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProducID} could not be found in the database.");
                }
                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);
                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                // get the information about this product
                details.Add(detail);
            }
            //fill in the availabe information
            //Create the sale model

            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierID = cashierId
            };

            sale.Total = sale.SubTotal * sale.Tax;

            //save the sale model

            try
            {
                _sql.StartTransaction("NRMData");
                _sql.SaveDataInTransaction<SaleDBModel>("dbo.spSale_Insert", sale);
                sale.Id = _sql.LoadDataInTransaction<int, dynamic>("spSaleLookUp", new { sale.CashierID, sale.SaleDate }).FirstOrDefault();

                // get the id from the sale table
                //finish filling in the sale deteil model

                foreach (var item in details)
                {
                    item.SaleID = sale.Id;
                    //save the sale detail model 
                    _sql.SaveDataInTransaction("dbo.SaleDetail_Insert", item);
                }
                _sql.CommitTransaction();
            }
            catch
            {
                _sql.RollbackTransaction();
                throw;
            }
        }

        public List<SaleReportModel> GetSaleReport()
        {
            var output = _sql.LoadData<SaleReportModel, dynamic>("spSale_SaleReport", new { }, "NRMData");
            return output;
        }
    }
}
