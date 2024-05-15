namespace SchoolFinder.Common.Identity.User
{
    public static class UserExtensions
    {
        public static IQueryable<User> Filter(this IQueryable<User> users, UserFilter filter)
        {
            return users.Where(u =>
                string.IsNullOrEmpty(filter.SearchText) || u.FirstName.Contains(filter.SearchText)
                    || u.FirstName.Contains(filter.SearchText)
                    || u.LastName.Contains(filter.SearchText)
                    || u.Email.Contains(filter.SearchText));
        }

        public static UserDto ToDto(this User user, List<string>? roles = null)
        {
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            };
        }

        public static User ToModel(this UserDto dto)
        {
            return new User()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
        }
    }
}
