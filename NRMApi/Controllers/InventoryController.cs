using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NRMDataManager.library.DataAccess;
using NRMDataManager.library.Models;
using System.Collections.Generic;

namespace NRMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IInventoryData _inventory;

        public InventoryController(IConfiguration config, IInventoryData inventory)
        {
            _config = config;
            _inventory = inventory;
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet]
        public List<InventoryModel> Get()
        {
           
            return _inventory.GetInventory();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost] 
        public void Post(InventoryModel item)
        {

            _inventory.SaveInventoryRecord(item);

        }
    }
}
