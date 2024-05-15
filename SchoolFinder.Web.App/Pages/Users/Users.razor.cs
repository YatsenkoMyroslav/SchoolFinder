using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Web.App.Components;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Pages.Users
{
    public partial class Users : FinderComponent
    {
        [Inject]
        public UserService UserService { get; set; } = null!;

        public RadzenDataList<UserDto> DataList { get; set; } = new RadzenDataList<UserDto>();
        public List<UserDto> DataUsers { get; set; } = new List<UserDto>();
        public UserFilter Filter { get; set; } = new UserFilter();
        public bool IsLoading { get; set; } = false;
        public int TotalCount { get; set; }
        public readonly Dictionary<string, string> Roles = new Dictionary<string, string>()
        {
            { "Абітурієнт", UserRoles.Applicant },
            { "Учень", UserRoles.Student },
            { "Шкільна адміністрація" , UserRoles.SchoolAdmin },
            { "Модератор" , UserRoles.Moderator },
            { "Адміністратор" , UserRoles.Admin },
            { "Супер адміністратор" , UserRoles.SuperAdmin }
        };

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await ReadUsers();
        }

        public async Task OnFilterChange()
        {
            await ReadUsers();
        }

        public async Task OnRead(LoadDataArgs args)
        {
            IsLoading = true;
            Filter.PageIndex = (args.Skip ?? 0) / Filter.PageSize;
            await ReadUsers();

            IsLoading = false;
        }

        public async Task OnRoleChanged(string role, UserDto user)
        {
            user.Roles = new List<string> { Roles[role] };
            await UserService.Update(user);
        }

        public async Task OnSearchBoxChange(string newValue)
        {
            Filter.SearchText = newValue;
            await SetToPageOne();
            await ReadUsers();
        }

        private async Task ReadUsers()
        {
            PagedList<UserDto> users = await UserService.Find(Filter);
            TotalCount = users.TotalCount;
            DataUsers = users.Values.ToList();
        }

        private async Task SetToPageOne()
        {
            Filter.PageIndex = 0;
            await DataList.GoToPage(0);
        }
    }
}
