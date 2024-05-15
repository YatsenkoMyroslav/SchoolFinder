using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Core.Services;

namespace SchoolFinder.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly RegistrationService _registrationService;
        private readonly RegistrationFormService _registrationFormService;

        public RegistrationController(RegistrationFormService registrationFormService, RegistrationService registrationService)
        {
            _registrationFormService = registrationFormService;
            _registrationService = registrationService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRequest([FromBody] RegistrationForm registrationForm)
        {
            int result = await _registrationFormService.Create(registrationForm);

            return Ok(result != 0);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("find")]
        public async Task<IActionResult> FindRequests([FromBody] RegistrationFormFilter filter)
        {
            PagedList<RegistrationForm> registrationForms = await _registrationFormService.Find(filter);
            return Ok(registrationForms);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            bool result = await _registrationService.Registrate(model);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateRequest([FromBody] RegistrationForm form)
        {
            int result = await _registrationFormService.Update(form);
            return Ok(result != 0);
        }
    }
}
