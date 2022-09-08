using NRMDesktopUI.Helpers;
using NRMDesktopUI.library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.library.API
{
    public class SaleEndPoint : ISaleEndPoint
    {
        private IAPIHelpers _apiHelpers;
        public SaleEndPoint(IAPIHelpers apiHelper)
        {
            _apiHelpers = apiHelper;
        }

        public async Task PostSale(SaleModel sale)
        {
            using (HttpResponseMessage reponse = await _apiHelpers.ApiClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if (reponse.IsSuccessStatusCode)
                {
                    // log successfull call?
                }
                else
                {
                    throw new Exception(reponse.ReasonPhrase);
                }
            }
        }
    }
}
