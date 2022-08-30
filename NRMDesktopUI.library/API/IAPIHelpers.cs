using NRMDesktopUI.Models;
using System.Threading.Tasks;

namespace NRMDesktopUI.Helpers
{
    public interface IAPIHelpers
    {
        Task<Authenticateduser> Authenticate(string username, string password);

        Task GetLoggedInUserInfo(string token);
    }
}