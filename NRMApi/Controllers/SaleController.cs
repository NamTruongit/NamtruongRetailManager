using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //Old Way with asp.net basic: RequestContext.Principal.Identity.GetUserId();
            data.SaveSale(sale, userId);
            Console.WriteLine();
        }
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSaleReport")]
        public List<SaleReportModel> GetSaleReport()
        {
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }
    }
}
