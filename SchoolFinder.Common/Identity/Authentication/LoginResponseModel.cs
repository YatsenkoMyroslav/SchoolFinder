namespace SchoolFinder.Common.Identity.Authentication
{
    public class LoginResponseModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireOn { get; set; }
        public List<string>? UserRoles { get; set; } = new List<string>();
        public string UserId { get; set; } = string.Empty;

        public bool IsEmpty => string.IsNullOrEmpty(UserId) 
            || string.IsNullOrEmpty(Token)
            || UserRoles == null;
    }
}
