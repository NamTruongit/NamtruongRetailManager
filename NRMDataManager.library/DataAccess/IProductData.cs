using NRMDataManager.library.Models;
using System.Collections.Generic;

namespace NRMDataManager.library.DataAccess
{
    public interface IProductData
    {
        List<ProductModel> GetProduct();
        ProductModel GetProductByID(int productId);
    }
}