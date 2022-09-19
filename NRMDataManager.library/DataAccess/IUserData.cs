using NRMDataManager.library.Models;
using System.Collections.Generic;

namespace NRMDataManager.library.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string id);
    }
}