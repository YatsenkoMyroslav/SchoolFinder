using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Common.Identity.Authentication
{
    public class LoginResponseModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireOn { get; set; }
        public UserDto User { get; set; } = new UserDto();

        public bool IsEmpty => string.IsNullOrEmpty(User.Id) 
            || string.IsNullOrEmpty(Token)
            || User.Roles == null;
    }
}
