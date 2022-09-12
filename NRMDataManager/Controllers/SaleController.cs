using Microsoft.AspNet.Identity;
using NRMDataManager.library.DataAccess;
using NRMDataManager.library.Models;
using NRMDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NRMDataManager.Controllers
{
   [Authorize]
    public class SaleController : ApiController
    {
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();
            data.SaveSale(sale, userId);
            Console.WriteLine();
        }
        [Route("GetSaleReport")]
        public List<SaleReportModel> GetSaleReport()
        {
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }
    }

}
