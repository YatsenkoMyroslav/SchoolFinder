using Microsoft.AspNetCore.Components;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Common.School.Request.Feedback;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Components
{
    public partial class SchoolCommentsSection : FinderComponent
    {
        [Parameter]
        public SchoolDto School { get; set; } = null!;

        [Inject]
        public SchoolCommentService CommentService { get; set; } = null!;
        [Inject]
        public SchoolCommentCreationService CommentCreationService { get; set; } = null!;

        public bool AddCommentSectionVisible { get; set; }
        public CommentCreationRequestDto Comment { get; set; } = new CommentCreationRequestDto();
        public ReplyDto Reply { get; set; } = new ReplyDto();
        public PagedList<CommentDto> Comments { get; set; } = new PagedList<CommentDto>();
        public CommentFilter Filter { get; set; } = new CommentFilter();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Filter.SchoolId = School.Id;
            Filter.PageSize = int.MaxValue;
            await ReadComments();
        }

        public async Task AddComment()
        {
            AddCommentSectionVisible = false;
            if (Comment.Ratings?.Any() ?? false)
            {
                Comment.Ratings = Comment.Ratings.Where(r => r.Value > 0 && r.Value < 6).ToList();
            }
            bool result = false;
            if (State.IsModerator())
            {
                result = await CommentService.Add(Comment.ToCommentDtoModel());
            }
            else
            {
                result = await CommentCreationService.Add(Comment);
            }
            
            await ReadComments();
            StateHasChanged();
        }

        public async Task ReadComments()
        {
            Comments = await CommentService.Find(Filter);
            StateHasChanged();
        }

        public void CloseAddCommentSection()
        {
            AddCommentSectionVisible = false;
            StateHasChanged();
        }

        public void ShowAddCommentSection()
        {
            Comment = new CommentCreationRequestDto();
            Comment.SchoolId = School.Id;
            Comment.CreatedById = State.User!.Id;
            foreach (RatingCategory category in Enum.GetValues(typeof(RatingCategory)))
            {
                if (Comment.Ratings != null)
                {
                    Comment.Ratings.Add(new RatingCreationRequestDto() { Category = category });
                }
                else
                {
                    Comment.Ratings = new List<RatingCreationRequestDto>()
                    {
                        new RatingCreationRequestDto()
                        {
                            Category = category
                        }
                    };
                }
            }
            AddCommentSectionVisible = true;
            StateHasChanged();
        }
    }
}
