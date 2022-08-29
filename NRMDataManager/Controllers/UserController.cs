using Microsoft.AspNet.Identity;
using NRMDataManager.library.DataAccess;
using NRMDataManager.library.Internal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NRMDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
   
        public List<UserModel> GetById()
        {
            string userID = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(userID);

        }

    }
}
