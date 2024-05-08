using Microsoft.AspNetCore.Mvc;
using SchoolFinder.Common.Identity.Authentication;
using SchoolFinder.Core.Services;

namespace SchoolFinder.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            LoginResponseModel response = await _authService.Login(model);

            return Ok(response);
        }
    }
}
