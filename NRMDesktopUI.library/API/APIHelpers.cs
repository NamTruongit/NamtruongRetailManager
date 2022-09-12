using NRMDesktopUI.library;
using NRMDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.Helpers
{
    public class APIHelpers : IAPIHelpers
    {
        private HttpClient _apiClient;

        public HttpClient ApiClient
        {
            get { return _apiClient; }
        }

        private ILoggedInUserModel _loggedInUser;
        public APIHelpers(ILoggedInUserModel loggedInUser)
        {
            InitiallizeClient();
            _loggedInUser = loggedInUser;
        }
        private void InitiallizeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            _apiClient = new HttpClient(); 
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Authenticateduser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("username",username),
                new KeyValuePair<string,string>("password",password),
            });

            using (HttpResponseMessage reponse = await _apiClient.PostAsync("/Token", data))
            {
                if (reponse.IsSuccessStatusCode)
                {
                    var result = await reponse.Content.ReadAsAsync<Authenticateduser>();
                    return result;
                }
                else
                {
                    throw new Exception(reponse.ReasonPhrase);
                }
            }
        }

        public async Task GetLoggedInUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using (HttpResponseMessage reponse = await _apiClient.GetAsync("/api/User"))
            {
                if (reponse.IsSuccessStatusCode)
                {
                    var result = await reponse.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedInUser.CreateDate = result.CreateDate;
                    _loggedInUser.EmailAddress = result.EmailAddress;
                    _loggedInUser.FirstsName = result.FirstsName;
                    _loggedInUser.Id = result.Id;
                    _loggedInUser.LastName = result.LastName;
                    _loggedInUser.Token = token;

                }
                else
                {
                    throw new Exception(reponse.ReasonPhrase);
                }
            }
        }

    }
}
