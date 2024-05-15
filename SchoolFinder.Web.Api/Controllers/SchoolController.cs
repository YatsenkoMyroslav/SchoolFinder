using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Core.Services;

namespace SchoolFinder.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolService _schoolService;
        
        public SchoolController(SchoolService schoolService) {
            _schoolService = schoolService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] SchoolDto school)
        {
            await _schoolService.Create(school);
            return Ok(true);
        }

        [HttpDelete]
        [Route("delete/{schoolId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid schoolId)
        {
            await _schoolService.Delete(schoolId);
            return Ok(true);
        }

        [HttpPost]
        [Route("find")]
        public async Task<IActionResult> Find([FromBody] SchoolFilter filter)
        {
            PagedList<SchoolDto> schools = await _schoolService.Find(filter);
            return Ok(schools);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] SchoolDto school)
        {
            await _schoolService.Update(school);
            return Ok(true);
        }
    }
}
