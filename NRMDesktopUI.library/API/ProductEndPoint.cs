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
    public class ProductEndPoint : IProductEndPoint
    {
        private IAPIHelpers _apiHelpers;
        public ProductEndPoint(IAPIHelpers apiHelper)
        {
            _apiHelpers = apiHelper;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            using (HttpResponseMessage reponse = await _apiHelpers.ApiClient.GetAsync("/api/Product"))
            {
                if (reponse.IsSuccessStatusCode)
                {
                    var result = await reponse.Content.ReadAsAsync<List<ProductModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(reponse.ReasonPhrase);
                }
            }
        }
    }
}
