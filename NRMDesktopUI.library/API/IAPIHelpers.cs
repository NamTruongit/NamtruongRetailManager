using NRMDesktopUI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace NRMDesktopUI.Helpers
{
    public interface IAPIHelpers
    {
        HttpClient ApiClient { get; }
        Task<Authenticateduser> Authenticate(string username, string password);

        void LogOffUser();
        Task GetLoggedInUserInfo(string token);
    }
}