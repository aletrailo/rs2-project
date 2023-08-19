using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IdentityServer.Api.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using IdentityServer.Api.Enitities;
using IdentityServer.Api.DTOs;
using IdentityServer.Api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

namespace IdentityServer.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : RegistrationControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthenticationService authenticationService) : base(logger, mapper, userManager, roleManager)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] NewUserDto newUser) {
            return await RegisterNewUserWithRoles(newUser, new string[] { "User" });
        }


        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAdministrator([FromBody] NewUserDto newUser)
        {
            return await RegisterNewUserWithRoles(newUser, new string[] { "Admin" });
        }


        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticationModel),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LogIn([FromBody] UserCredentialsDto userCredentials)
        {
            var user = await _authenticationService.ValidateUser(userCredentials);
            if(user == null)
            {
                return Unauthorized();
            }

            return Ok(await _authenticationService.CreateAuthenticationModel(user));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticationModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AuthenticationModel>> Refresh([FromBody] RefreshTokenModel refreshTokenCredentials)
        {
            var user = await _userManager.FindByNameAsync(refreshTokenCredentials.UserName);
            if (user is null)
            {
                _logger.LogWarning("{Refresh}: Refreshing token failed. Unknown username {UserName}.", nameof(Refresh), refreshTokenCredentials.UserName);
                return Forbid();
            }

            var refreshToken = user.RefreshTokens.FirstOrDefault(r => r.Token == refreshTokenCredentials.RefreshToken);
            if (refreshToken is null)
            {
                _logger.LogWarning("{Refresh}: Refreshing token failed. The refresh token is not found.", nameof(Refresh));
                return Unauthorized();
            }

            if (refreshToken.ExpiryTime < DateTime.Now)
            {
                _logger.LogWarning("{Refresh}: Refreshing token failed. The refresh token is not valid.", nameof(Refresh));
                return Unauthorized();
            }

            return Ok(await _authenticationService.CreateAuthenticationModel(user));
        }

        [Authorize]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenModel refreshTokenCredentials)
        {
            var user = await _userManager.FindByNameAsync(refreshTokenCredentials.UserName);
            if (user is null)
            {
                _logger.LogWarning("{Logout}: Logout failed. Unknown username {UserName}.", nameof(Logout), refreshTokenCredentials.UserName);
                return Forbid();
            }

            await _authenticationService.RemoveRefreshToken(user, refreshTokenCredentials.RefreshToken);

            return Accepted();
        }



    }
}
