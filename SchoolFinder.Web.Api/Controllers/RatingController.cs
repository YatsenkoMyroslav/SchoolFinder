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
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;

        public RatingController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] List<RatingDto> ratings)
        {
            await _ratingService.Create(ratings);
            return Ok(true);
        }

        [HttpDelete]
        [Route("delete/{ratingId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid ratingId)
        {
            await _ratingService.Delete(ratingId);
            return Ok(true);
        }

        [HttpPost]
        [Route("find")]
        public async Task<IActionResult> Find([FromBody] RatingFilter filter)
        {
            PagedList<RatingDto> ratings = await _ratingService.Find(filter);
            return Ok(ratings);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] RatingDto rating)
        {
            await _ratingService.Update(rating);
            return Ok(true);
        }
    }
}
