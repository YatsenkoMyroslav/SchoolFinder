using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Core.Services;

namespace SchoolFinder.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CommentDto comment)
        {
            await _commentService.Create(comment);
            return Ok(true);
        }

        [HttpDelete]
        [Route("delete/{commentId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid commentId)
        {
            await _commentService.Delete(commentId);
            return Ok(true);
        }

        [HttpPost]
        [Route("find")]
        public async Task<IActionResult> Find([FromBody] CommentFilter filter)
        {
            PagedList<CommentDto> comments = await _commentService.Find(filter);
            return Ok(comments);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] CommentDto comment)
        {
            await _commentService.Update(comment);
            return Ok(true);
        }
    }
}
