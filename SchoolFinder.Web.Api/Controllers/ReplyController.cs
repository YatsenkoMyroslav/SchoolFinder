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
    public class ReplyController : ControllerBase
    {
        private readonly ReplyService _replyService;

        public ReplyController(ReplyService replyService)
        {
            _replyService = replyService;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] ReplyDto reply)
        {
            await _replyService.Add(reply);
            return Ok(true);
        }

        [HttpDelete]
        [Route("delete/{replyId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid replyId)
        {
            await _replyService.Delete(replyId);
            return Ok(true);
        }

        [HttpPost]
        [Route("find")]
        public async Task<IActionResult> Find([FromBody] ReplyFilter filter)
        {
            PagedList<ReplyDto> replies = await _replyService.Find(filter);
            return Ok(replies);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ReplyDto reply)
        {
            await _replyService.Update(reply);
            return Ok(true);
        }
    }
}
