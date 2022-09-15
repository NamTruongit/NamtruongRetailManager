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

        public async Task<Dictionary<string,string>> GetAllRolls()
        {
            using (HttpResponseMessage reponse = await _apiHelper.ApiClient.GetAsync("/api/User/Admin/GetAllRoles"))
            {
                if (reponse.IsSuccessStatusCode)
                {
                    var result = await reponse.Content.ReadAsAsync<Dictionary<string, string>>();
                    return result;
                }
                else
                {
                    throw new Exception(reponse.ReasonPhrase);
                }

            }
        }

        public async Task AddUserToRole(string userId,string roleName)
        {
            var data = new { userId, roleName };
            using (HttpResponseMessage reponse = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Admin/AddRoles",data))
            {
                if (reponse.IsSuccessStatusCode == false)
                {
                    throw new Exception(reponse.ReasonPhrase);
                }

            }
        }

        public async Task RemoveUserFromRole(string userId, string roleName)
        {
            var data = new { userId, roleName };
            using (HttpResponseMessage reponse = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Admin/RemoveRoles", data))
            {
                if (reponse.IsSuccessStatusCode == false)
                {
                    throw new Exception(reponse.ReasonPhrase);
                }

            }
        }

    }
}

