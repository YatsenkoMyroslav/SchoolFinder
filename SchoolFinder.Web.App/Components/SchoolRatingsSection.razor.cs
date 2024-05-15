using Microsoft.AspNetCore.Components;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Components
{
    public partial class SchoolRatingsSection : ComponentBase
    {
        [Inject]
        public SchoolRatingService RatingService { get; set; } = null!;

        [Parameter]
        public Guid SchoolId { get; set; } = Guid.Empty;

        PagedList<RatingDto> Ratings { get; set; } = new PagedList<RatingDto>();

        public RatingFilter Filter { get; set; } = new RatingFilter()
        {
            PageSize = int.MaxValue
        };

        protected override async Task OnInitializedAsync()
        {
            Filter.SchoolId = SchoolId;
            Ratings = await RatingService.Find(Filter);
        }
    }
}
