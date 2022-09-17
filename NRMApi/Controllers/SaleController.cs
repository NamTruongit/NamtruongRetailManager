using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NRMDataManager.library.DataAccess;
using NRMDataManager.library.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NRMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SaleController(IConfiguration config)
        {
            _config = config;
        }

        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData(_config);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //Old Way with asp.net basic: RequestContext.Principal.Identity.GetUserId();
            data.SaveSale(sale, userId);
            Console.WriteLine();
        }
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSaleReport")]
        [HttpGet]
        public List<SaleReportModel> GetSaleReport()
        {
            SaleData data = new SaleData(_config);
            return data.GetSaleReport();
        }
    }
}
