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
    public class UserEndPoint : IUserEndPoint
    {
        private readonly IAPIHelpers _apiHelper;
        public UserEndPoint(IAPIHelpers apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<UserModel>> GetAll()
        {
            using (HttpResponseMessage reponse = await _apiHelper.ApiClient.GetAsync("/api/User/Admin/GetAllUsers"))
            {
                if (reponse.IsSuccessStatusCode)
                {
                    var result = await reponse.Content.ReadAsAsync<List<UserModel>>();
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

