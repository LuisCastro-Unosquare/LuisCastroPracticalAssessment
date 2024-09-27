using LC_Assessment_Todo.Models;
using LC_Assessment_Todo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LC_Assessment_Todo.Controllers
{
    [ApiController, Route("[controller]"), AllowAnonymous]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto loginDto, [FromServices] IAuthService authService)
        {
            if (loginDto == null)
                return BadRequest(new Result<LoginDto>("Neither username nor password was provided."));

            var token = authService.GenerateToken(loginDto.Username, loginDto.Password);
            return Ok(new Result<AuthTokenDto>(new AuthTokenDto() { Token = token}));
        }
    }
}
