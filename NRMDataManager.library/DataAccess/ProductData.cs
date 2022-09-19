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
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sql;

        public ProductData(ISqlDataAccess sql)
        {
            _sql = sql;
        }
        public List<ProductModel> GetProduct()
        {
            var output = _sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "NRMData");
            return output;
        }

        public ProductModel GetProductByID(int productId)
        {
            var output = _sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "NRMData").FirstOrDefault();
            return output;
        }
    }
}
