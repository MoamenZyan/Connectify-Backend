using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Connectify.API.Controllers
{
    
    [Route("/api/v1/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserApplicationService _userApplicationService;
        public UserController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllUsers()
        {
            Guid currentUserId = new Guid(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            Console.WriteLine(currentUserId);
            var users = await _userApplicationService.GetAllUsers(currentUserId);
            return Ok(users);
        }

        [HttpPost]
        [Route("login")]
        [Authorize(AuthenticationSchemes = "Basic")]
        public async Task<IActionResult> LoginUser()
        {
            Guid currentUserId = new Guid(HttpContext.User.Claims.First().Value);
            var token = await _userApplicationService.LoginUser(currentUserId);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterNewUser()
        {
            var form = await Request.ReadFormAsync();
            var result = await _userApplicationService.RegisterUser(form);
            if (!result.Item1)
                return BadRequest(new { status = false, message = result.Item2 });

            return Ok(new {status = true, message = "user created!"});
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteUser()
        {
            Guid currentUserId = new Guid(HttpContext.User.Claims.First().Value);
            var result = await _userApplicationService.DeleteUser(currentUserId);
            if (!result)
                return BadRequest(new { status = false, message = "error in deleting user" });

            return Ok(new { status = true, message = "user deleted!" });
        }
    }
}
