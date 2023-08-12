using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityServer.Api.Enitities;
using AutoMapper;
using IdentityServer.Api.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IdentityServer.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDetails>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUsers() {
             var users = await _userManager.Users.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserDetails>>(users));

        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetails>> GetUser(string username)
        {
            var users = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == username);
            return Ok(_mapper.Map<UserDetails>(users));

        }
    }
}
