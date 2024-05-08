using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Request;
using SchoolFinder.Core.Services;

namespace SchoolFinder.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SchoolRegistrationController : ControllerBase
    {
        private readonly SchoolRegistrationService _schoolRegistrationService;

        public SchoolRegistrationController(SchoolRegistrationService schoolRegistrationService)
        {
            _schoolRegistrationService = schoolRegistrationService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] SchoolCreationRequest request)
        {
            await _schoolRegistrationService.Create(request);
            return Ok(true);
        }

        [HttpPost]
        [Route("find")]
        public async Task<IActionResult> Find([FromBody] SchoolCreationRequestFilter filter)
        {
            PagedList<SchoolCreationRequest> schools = await _schoolRegistrationService.Find(filter);
            return Ok(schools);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] SchoolCreationRequest request)
        {
            await _schoolRegistrationService.Update(request);
            return Ok(true);
        }
    }
}
