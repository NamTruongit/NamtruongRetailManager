using NRMDesktopUI.library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NRMDesktopUI.library.API
{
    public interface IUserEndPoint
    {
        Task<List<UserModel>> GetAll();
    }
}