using NRMDesktopUI.library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NRMDesktopUI.library.API
{
    public interface IUserEndPoint
    {
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRolls();
        Task AddUserToRole(string userId, string roleName);
        Task RemoveUserFromRole(string userId, string roleName);
    }
}