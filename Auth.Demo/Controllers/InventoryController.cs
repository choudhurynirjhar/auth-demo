using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        // GET: api/Inventory
        [Authorize(Roles = "Administrator, User")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Inventory
        [Authorize(Policy = "EmployeeMoreThan20Years")]
        [HttpPost]
        public void Post([FromBody] Inventory value)
        {
        }
    }
}