using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Request.Feedback;
using SchoolFinder.Core.Services;
using System.Data;

namespace SchoolFinder.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CommentCreationController : Controller
    {
        private readonly CommentCreationService _commentCreationService;

        public CommentCreationController(CommentCreationService commentCreationService)
        {
            _commentCreationService = commentCreationService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CommentCreationRequestDto request)
        {
            await _commentCreationService.Create(request);
            return Ok(true);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Moderator)]
        [Route("find")]
        public async Task<IActionResult> Find([FromBody] CommentCreationRequestFilter filter)
        {
            PagedList<CommentCreationRequestDto> schools = await _commentCreationService.Find(filter);
            return Ok(schools);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Moderator)]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] CommentCreationRequestDto request)
        {
            await _commentCreationService.Update(request);
            return Ok(true);
        }
    }
}
