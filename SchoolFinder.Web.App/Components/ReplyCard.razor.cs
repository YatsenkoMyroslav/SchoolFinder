using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Components
{
    public partial class ReplyCard : FinderComponent
    {
        [Parameter]
        public ReplyDto Reply { get; set; } = new ReplyDto();
        [Parameter]
        public EventCallback OnChange { get; set; } = EventCallback.Empty;

        [Inject]
        public ContextMenuService ContextMenuService { get; set; } = null!;
        [Inject]
        public SchoolCommentReplyService ReplyService { get; set; } = null!;

        public bool IsInEditMode { get; set; } = false;
        public bool IsNew { get; set; } = false;
        public bool IsUserCanEdit => State.IsModerator() || State.IsAdmin() || State.User?.Id == Reply.CreatedBy.Id;

        public void CancelEdit()
        {
            IsInEditMode = true;
            StateHasChanged();
        }

        public void EnableEdit()
        {
            IsInEditMode = true;
            StateHasChanged();
        }

        public async Task Delete()
        {
            await ReplyService.Delete(Reply.Id);
            await OnChange.InvokeAsync();
        }

        public void OnContextMenuButtonClick(MouseEventArgs args)
        {
            ContextMenuService.Open(args, new List<ContextMenuItem> {
                new ContextMenuItem(){ Text = "Редагувати", Value = "Edit", Icon = "edit" },
                new ContextMenuItem(){ Text = "Видалити", Value = "Delete", Icon = "delete" },
            }, OnMenuItemClick);
        }

        public async void OnMenuItemClick(MenuItemEventArgs args)
        {
            switch (args.Value)
            {
                case "Edit":
                    EnableEdit();
                    break;
                case "Delete":
                    await Delete();
                    break;
            }
            ContextMenuService.Close();
            StateHasChanged();
        }

        public async Task Save()
        {
            IsInEditMode = false;
            await ReplyService.Update(Reply);
            await OnChange.InvokeAsync();
        }
    }
}
