using BLL.DTOs.Users;
using BLL.Exceptions;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {

            return Ok(_userServices.Authenticate(loginRequest));
        }

        [AllowAnonymous]
        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {

            Guid registeredId = _userServices.Register(registerRequest);
            return Ok(registeredId);
        }


    }
}