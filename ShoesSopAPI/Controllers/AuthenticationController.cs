using Microsoft.AspNetCore.Mvc;
using ShoesSopAPI.Data;
using ShoesSopAPI.Services.Interfaces;

namespace ShoesSopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<Account>> Login(string username, string password)
        {
            var account = await _authenticationService.Login(username, password);
            IActionResult response = Unauthorized();

            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
        
    }
}
