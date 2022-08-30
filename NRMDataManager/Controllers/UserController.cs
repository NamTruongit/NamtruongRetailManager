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
    public class UserController : ApiController
    {

        [HttpGet]
        public UserModel GetById()
        {
            string userID = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(userID).First();

        }

    }
}
