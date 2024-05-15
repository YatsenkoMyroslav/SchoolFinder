using Microsoft.AspNetCore.Components;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Common.School.Request;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Components
{
    public partial class SchoolCommentsSection : FinderComponent
    {
        [Parameter]
        public Guid SchoolId { get; set; } = Guid.Empty;

        [Inject]
        public SchoolCommentService CommentService { get; set; } = null!;

        public bool AddCommentSectionVisible { get; set; }
        public CommentDto Comment { get; set; } = new CommentDto();
        public PagedList<CommentDto> Comments { get; set; } = new PagedList<CommentDto>();
        public CommentFilter Filter { get; set; } = new CommentFilter();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Filter.SchoolId = SchoolId;
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
            bool result = await CommentService.Add(Comment);
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
            Comment = new CommentDto();
            Comment.School.Id = SchoolId;
            Comment.CreatedBy.Id = State.User!.Id;
            foreach (RatingCategory category in Enum.GetValues(typeof(RatingCategory)))
            {
                if (Comment.Ratings != null)
                {
                    Comment.Ratings.Add(new RatingDto() { Category = category });
                }
                else
                {
                    Comment.Ratings = new List<RatingDto>()
                    {
                        new RatingDto()
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
