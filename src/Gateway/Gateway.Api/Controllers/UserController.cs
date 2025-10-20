using Gateway.Api.Models.User;
using Gateway.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserGrpcClient _userClient;

        public UserController(IUserGrpcClient userClient)
        {
            _userClient = userClient;
        }

        // GET api/user/{userId}
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userClient.GetUserByIdAsync(userId, HttpContext.Request.Headers["Authorization"].FirstOrDefault()!);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // GET api/user/by-login/{login}
        [Authorize]
        [HttpGet("by-login/{login}")]
        public async Task<IActionResult> GetUserByLogin(string login)
        {
            var user = await _userClient.GetUserIdByLoginAsync(login, HttpContext.Request.Headers["Authorization"].FirstOrDefault()!);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST api/user/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
        {
            var result = await _userClient.RegisterAsync(model.Username, model.Password);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // POST api/user/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var result = await _userClient.LoginAsync(model.Username, model.Password);
            if (result == null) return Unauthorized();
            return Ok(result);
        }

        // POST api/user/logout
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestModel model)
        {
            var result = await _userClient.LogoutAsync(model.Token);
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
