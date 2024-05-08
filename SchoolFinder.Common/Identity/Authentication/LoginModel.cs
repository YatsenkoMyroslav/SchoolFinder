using System.ComponentModel.DataAnnotations;

namespace SchoolFinder.Common.Identity.Authentication
{
    public class LoginModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
