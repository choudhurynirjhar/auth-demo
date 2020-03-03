using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;
        private readonly ITokenRefresher tokenRefresher;

        public NameController(IJWTAuthenticationManager jWTAuthenticationManager, ITokenRefresher tokenRefresher)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
            this.tokenRefresher = tokenRefresher;
        }

        // GET: api/Name
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "New York", "New Jersey" };
        }

        // GET: api/Name/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "New Jersey";
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = jWTAuthenticationManager.Authenticate(userCred.Username, userCred.Password);
            
            if (token == null) 
                return Unauthorized();
            
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshCred refreshCred)
        {
            var token = tokenRefresher.Refresh(refreshCred);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
