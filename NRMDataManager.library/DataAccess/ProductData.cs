﻿using NRMDataManager.library.Internal.DataAccess;
using NRMDataManager.library.Internal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDataManager.library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProduct()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "NRMData");
            return output;

        }
    }
}