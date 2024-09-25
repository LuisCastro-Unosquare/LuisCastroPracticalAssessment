using LC_Assessment_Todo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LC_Assessment_Todo.Controllers
{
    [ApiController, Route("[controller]"), AllowAnonymous]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult Login([FromServices] IAuthService authService)
        {
            var token = authService.GenerateToken("1");
            return Ok(new { token });
        }
    }
}
