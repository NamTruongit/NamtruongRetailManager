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
        private readonly ISaleData _saleData;

        public SaleController(IConfiguration config,ISaleData saleData)
        {
            _config = config;
            _saleData = saleData;
        }

        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //Old Way with asp.net basic: RequestContext.Principal.Identity.GetUserId();
            _saleData.SaveSale(sale, userId);
            Console.WriteLine();
        }
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSaleReport")]
        [HttpGet]
        public List<SaleReportModel> GetSaleReport()
        {
            return _saleData.GetSaleReport();
        }
    }
}
