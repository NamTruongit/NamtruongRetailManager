using Microsoft.Extensions.Configuration;
using NRMDataManager.library.Internal.DataAccess;
using NRMDataManager.library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDataManager.library.DataAccess
{
    public class UserData
    {
        private readonly IConfiguration _config;

        public UserData(IConfiguration config)
        {
            _config = config;
        }
        public List<UserModel> GetUserById(string id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { Id = id };
            var output = sql.LoadData<UserModel, dynamic>("dbo.SPUserLookUp", p, "NRMData");
            return output;
        }
    } 
}
