using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Core.Services;

namespace SchoolFinder.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("find")]
        public async Task<IActionResult> Find([FromBody] UserFilter filter)
        {
            PagedList<UserDto> users = await _userService.Find(filter);
            return Ok(users);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserDto user)
        {
            await _userService.Update(user);
            return Ok(true);
        }
    }
}
