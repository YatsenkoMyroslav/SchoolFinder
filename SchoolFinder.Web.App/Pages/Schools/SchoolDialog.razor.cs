using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using Radzen.Blazor;
using SchoolFinder.Common;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Common.School.Request;
using SchoolFinder.Web.App.Components;
using SchoolFinder.Web.App.Services;
namespace SchoolFinder.Web.App.Pages.Schools
{
    public partial class SchoolDialog : FinderComponent
    {
        [Inject]
        public DialogService DialogService { get; set; } = null!;
        [Inject]
        public NotificationService NotificationService { get; set; } = null!;
        [Inject]
        public SchoolService SchoolService { get; set; } = null!;
        [Inject]
        public SchoolCreationRequestService SchoolRequestService { get; set; } = null!;
        [Inject]
        public UserService UserService { get; set; } = null!;

        [Parameter]
        public SchoolDto? School { get; set; }

        public List<UserDto> Users { get; set; } = new List<UserDto>();
        protected bool IsNew { get; set; } = false;
        protected bool IsFormEditable => IsNew
                                            || State.IsAdmin()
                                            || State.IsModerator()
                                            || (State?.User?.Id == School?.Owner.Id);
        protected RadzenUpload Upload { get; set; } = new RadzenUpload();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (School == null)
            {
                IsNew = true;
                School = new SchoolDto();
                School.Owner = State.User!;
            }

            if (IsNew && State.IsAdmin())
            {
                UserFilter filter = new UserFilter()
                {
                    PageSize = int.MaxValue
                };
                PagedList<UserDto> response = await UserService.Find(filter);
                Users = response.Values.ToList();
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        public void CloseDialog()
        {
            DialogService.Close();
        }

        public async Task Delete()
        {
            await SchoolService.Delete(School!.Id);
            CloseDialog();
        }

        public async Task OnFileChange(UploadChangeEventArgs args)
        {
            IEnumerable<IBrowserFile> files = args.Files.Select(f => f.Source);
            School!.Photos = new List<FileBytes>();

            foreach (var file in files)
            {
                Stream stream = file.OpenReadStream(10 * 1024 * 1024);
                MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                stream.Close();

                FileBytes photo = new FileBytes()
                {
                    Name = file.Name,
                    Data = ms.ToArray(),
                    Extension = file.Name.Substring(file.Name.LastIndexOf('.') + 1)
                };
                ms.Close();
                School!.Photos.Add(photo);
            }
        }

        public void OnMapClick(GoogleMapClickEventArgs args)
        {
            School!.Location.Latitude = args.Position.Lat;
            School.Location.Longitude = args.Position.Lng;
        }

        public async Task Save()
        {
            if (IsNew)
            {
                string notificationMessage = "";
                if (State.User?.Roles?.Contains(UserRoles.Admin) ?? false)
                {
                    await SchoolService.Add(School!);
                    notificationMessage = "Школа додана успішно";
                }
                else
                {
                    SchoolCreationRequestDto request = School!.ToRequest();
                    await SchoolRequestService.Add(request);
                    notificationMessage = "Запит на створення школи успішно додано";
                }

                NotificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = notificationMessage,
                    Duration = 4000
                });
            }
            else
            {
                await SchoolService.UpdateSchool(School!);
            }
            DialogService.Close();
        }
    }
}
