﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NRMApi.Data;
using NRMApi.Models;
using NRMDataManager.library.DataAccess;
using NRMDataManager.library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NRMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public UserModel GetById()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserData data = new UserData();

            return data.GetUserById(userID).First();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/User/Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();

            //var userStore = new UserStore<ApplicationUser>(_context);
            //var userManager = new UserManager<ApplicationUser>(userStore);

            var users = _context.Users.ToList();
            var userRoles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };
            //var roles = _context.Roles.ToList();

            foreach (var user in users)
            { 
                ApplicationUserModel u = new ApplicationUserModel()
                {
                    Id = user.Id,
                    Email = user.Email
                };

                u.Roles = userRoles.Where(x => x.UserId == u.Id).ToDictionary(key => key.RoleId, val => val.Name);
                //foreach (var r in user.Roles)
                //{
                //    u.Roles.Add(r.RoleId, roles.Where(x => x.Id == r.RoleId).First().Name);
                //}

                output.Add(u);
            }
            
            return output;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/User/Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
                var roles = _context.Roles.ToDictionary(x => x.Id, x => x.Name);
                return roles;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User/Admin/AddRoles")]
        public async Task AddRole(UserRolePairModel pairing)
        {
            var user = await _userManager.FindByIdAsync(pairing.UserId);
            await _userManager.AddToRoleAsync(user, pairing.RoleName);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User/Admin/RemoveRoles")]
        public async Task RemoveARole(UserRolePairModel pairing)
        {
            var user = await _userManager.FindByIdAsync(pairing.UserId);
            await _userManager.RemoveFromRoleAsync(user, pairing.RoleName);

        }
    }
}
