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
        private HttpClient ApiClient { get; set; }

        public APIHelpers()
        {
            InitiallizeClient();
        }
        private void InitiallizeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            ApiClient = new HttpClient(); 
            ApiClient.BaseAddress = new Uri(api);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Authenticateduser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("username",username),
                new KeyValuePair<string,string>("password",password),
            });

            using (HttpResponseMessage reponse = await ApiClient.PostAsync("/Token", data))
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

    }
}
