using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Web.App.Services
{
    public class AppState
    {
        public string Token { get; set; } = string.Empty;
        public UserDto? User { get; set; }
        public bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrEmpty(Token);
            }
        }

        public AppState(string token, UserDto user)
        {
            Token = token;
            User = user;
        }
        public AppState() {
        }
    }
}
