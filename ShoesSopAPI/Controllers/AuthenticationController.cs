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
            if(username == null || password == null)
            {
                return BadRequest("The username or password field is required.");
            }
            var account = await _authenticationService.Login(username, password);

            if (account == null)
            {
                return BadRequest("Isvalid username or password");
            }
            return Ok(account);
        }
        
    }
}
