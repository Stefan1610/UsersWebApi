using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Authentication Controller
    public class Auth2Controller : ControllerBase
    {
        private AuthService authService;
        public Auth2Controller(AuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("register")]
        //Registration method
        public IActionResult Register([FromBody] RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserData authData = authService.Register(model);
            if (authData.Errors != null)
            {
                return BadRequest(authData.Errors);
            }
            return Ok(authData);
        }
        [HttpPost("login")]
        //Login method
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserData authData = authService.Login(model);
            if (authData.Errors != null)
            {
                return BadRequest(authData.Errors);
            }
            return Ok(authData);
        }
    }
}
