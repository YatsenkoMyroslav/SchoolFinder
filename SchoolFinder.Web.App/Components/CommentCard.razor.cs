using Microsoft.AspNetCore.Components;
using Radzen;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Web.App.Services;
using Microsoft.AspNetCore.Components.Web;
using SchoolFinder.Common.School.Model;

namespace SchoolFinder.Web.App.Components
{
    public partial class CommentCard : FinderComponent
    {
        [Parameter]
        public CommentDto Comment { get; set; } = new CommentDto();
        [Parameter]
        public EventCallback OnChange { get; set; } = EventCallback.Empty;
        [Parameter]
        public SchoolDto School { get; set; } = null!;

        [Inject]
        public ContextMenuService ContextMenuService { get; set; } = null!;
        [Inject]
        public SchoolCommentService CommentService { get; set; } = null!;

        private CommentDto _commentCopy { get; set; } = new CommentDto();
        public bool IsCommentInEditMode { get; set; } = false;
        public bool IsOwnerLoggedIn => (Comment.CreatedBy?.Id ?? "") == (State.User?.Id ?? "" );
        
        public bool IsUserCanEdit => State.IsModerator() || IsOwnerLoggedIn;
        public ReplyDto Reply { get; set; } = null!;

        public void CancelEdit()
        {
            Comment = _commentCopy;
            IsCommentInEditMode = false;
        }

        public async Task Delete()
        {
            await CommentService.Delete(Comment.Id);
            await OnChange.InvokeAsync();
        }

        public void EnableEdit()
        {
            _commentCopy = (CommentDto)Comment.Clone();
            if(Comment.Ratings == null)
            {
                Comment.Ratings = new List<RatingDto>();
            }
            foreach(RatingCategory category in Enum.GetValues(typeof(RatingCategory)))
            {
                if(Comment.Ratings!.FirstOrDefault(r => r.Category == category) == null)
                {
                    Comment.Ratings.Add(new RatingDto() { Category = category });
                }
            }
            IsCommentInEditMode = true;
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
            IsCommentInEditMode = false;
            Comment.Ratings = Comment.Ratings?.Where(r => r.Value > 0 && r.Value < 6).ToList();
            await CommentService.Update(Comment);
            await OnChange.InvokeAsync();
        }
    }
}
