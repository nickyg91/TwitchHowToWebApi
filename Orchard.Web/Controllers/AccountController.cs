using Microsoft.AspNetCore.Mvc;
using Orchard.Core.Exceptions;
using Orchard.Models;
using Orchard.Models.Authentication;
using Orchard.Services.Domain;
using Orchard.Web.Authentication;

namespace Orchard.Web.Controllers
{
    [Route("api/account")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly IUserService _userService;

        public AccountController(TokenGenerator tokenGenerator, IUserService userService)
        {
            _tokenGenerator = tokenGenerator;
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<bool> CreateUser(UserModel userToCreate)
        {
            return await _userService.CreateAccount(userToCreate);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginInformation)
        {
            try
            {
                return Ok(await _tokenGenerator.GenerateToken(loginInformation));
            }
            catch (InvalidLoginException e)
            {
                return Unauthorized(e);
            }
        }

        [HttpGet("token/refresh/{token}")]
        public async Task<IActionResult> RefreshToken(string token)
        {
            var newToken = await _tokenGenerator.RefreshToken(token);
            if (newToken == null)
            {
                return Forbid();
            }

            return Ok(newToken);
        }
    }
}
