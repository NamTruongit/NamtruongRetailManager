﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NRMDataManager.library.DataAccess;
using NRMDataManager.library.Models;
using System.Collections.Generic;

namespace NRMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData();
            return data.GetProduct();
        }
    }
}
