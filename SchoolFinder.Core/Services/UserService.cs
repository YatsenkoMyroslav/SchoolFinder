using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Core.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PagedList<UserDto>> Find(UserFilter filter)
        {
            List<User> users = await _userManager.Users
                .Filter(filter)
                .Skip(filter.PageSize * filter.PageIndex)
                .Take(filter.PageSize)
                .ToListAsync();

            List<UserDto> result = new List<UserDto>();

            foreach (User user in users) {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                result.Add(user.ToDto(roles.ToList()));
            }

            int totalCount = await _userManager.Users
                .Filter(filter)
                .CountAsync();

            return new PagedList<UserDto>(result, totalCount);
        }

        public async Task<bool> Update(UserDto userDto)
        {
            User entity = await _userManager.FindByIdAsync(userDto.Id);
            IList<string> roles = await _userManager.GetRolesAsync(entity);

            if(entity.FirstName != userDto.FirstName || entity.LastName != userDto.LastName)
            {
                entity.FirstName = userDto.FirstName;
                entity.LastName = userDto.LastName;

                await _userManager.UpdateAsync(entity);
            }

            if (!(userDto.Roles?.SequenceEqual(roles.ToList())) ?? false)
            {
                IEnumerable<string> rolesToRemove = roles.ToList().Except(userDto.Roles ?? Enumerable.Empty<string>());
                IEnumerable<string> rolesToAdd = userDto.Roles?.Except(roles.ToList()) ?? roles.ToList();

                await _userManager.RemoveFromRolesAsync(entity, rolesToRemove);
                await _userManager.AddToRolesAsync(entity, rolesToAdd);
            }
            
            return true;
        }
    }
}
