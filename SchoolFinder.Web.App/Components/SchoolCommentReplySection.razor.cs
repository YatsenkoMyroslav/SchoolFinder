using Microsoft.AspNetCore.Components;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Components
{
    public partial class SchoolCommentReplySection : FinderComponent
    {
        [Parameter]
        public Guid CommentId { get; set; }

        [Inject]
        public SchoolCommentReplyService ReplyService { get; set; } = null!;

        public bool IsUserCanReply => State.IsModerator()
                                        || State.IsStudent()
                                        || State.IsSchoolAdmin();
        public bool IsReplySectionVisible { get; set; } = false;
        public ReplyDto Reply { get; set; } = new ReplyDto();
        public List<ReplyDto> Replies { get; set; } = new List<ReplyDto>();
        public ReplyFilter Filter { get; set; }  = new ReplyFilter() { PageSize = int.MaxValue };

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Filter.CommentId = CommentId;
            await ReadReplies();
        }

        public async Task AddReply()
        {
            await ReplyService.Add(Reply);
            IsReplySectionVisible = false;
            await ReadReplies();
        }

        public void CancelReply()
        {
            IsReplySectionVisible = false;
        }

        public void EnableReply()
        {
            Reply = new ReplyDto { CreatedBy = State.User!, CommentId = CommentId };
            IsReplySectionVisible = true;
            StateHasChanged();
        }

        public async Task ReadReplies()
        {
            PagedList<ReplyDto> response = await ReplyService.Find(Filter);
            Replies = response.Values.ToList();
            StateHasChanged();
        }
    }
}
