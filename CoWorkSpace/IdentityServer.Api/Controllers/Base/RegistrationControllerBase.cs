using AutoMapper;
using IdentityServer.Api.DTOs;
using IdentityServer.Api.Enitities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Api.Controllers.Base
{
    public class RegistrationControllerBase : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationControllerBase(ILogger<AuthenticationController> logger, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        protected async Task<IActionResult> RegisterNewUserWithRoles(NewUserDto newUser, IEnumerable<string> roles) {
            var user = _mapper.Map<User>(newUser);
            var result = await _userManager.CreateAsync(user, newUser.Password);

            if (!result.Succeeded) {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            _logger.LogInformation($"Uspesno registrovan korisnik: {user.UserName}");

            foreach (var role in roles)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (roleExists)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    _logger.LogInformation($"Dodano pravo {role} korisniku: {user.UserName}");
                }
                else {
                    _logger.LogInformation($"Pravo {role} me postoji");
                }
            }

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
