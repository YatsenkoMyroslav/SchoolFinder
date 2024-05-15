using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Web.App.Services;
using Microsoft.JSInterop;
using SchoolFinder.Common.Abstraction;
using Radzen.Blazor.Rendering;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Common.Abstraction.Extensions;

namespace SchoolFinder.Web.App.Components
{
    public partial class SchoolDataList : FinderComponent
    {
        [Parameter]
        public Guid? OwnerId { get; set; }
        [Parameter]
        public EventCallback<SchoolDto> OnSchoolClick { get; set; }

        [Inject]
        public ContextMenuService ContextMenuService { get; set; } = null!;
        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;
        [Inject]
        public SchoolService SchoolService { get; set; } = null!;

        public RadzenDataList<SchoolDto> DataList { get; set; } = new RadzenDataList<SchoolDto>();
        public List<SchoolDto> Schools { get; set; } = new List<SchoolDto>();
        public SchoolFilter Filter { get; set; } = new SchoolFilter();
        public List<RatingCategory>? FilterSelectedCategories { get; set; }
        public bool IsLoading { get; set; } = false;
        public int TotalCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Filter.OwnerId = OwnerId;
            await ReadSchools();
            await GetLocation();
        }

        public void ClearFilter()
        {
            Filter = new SchoolFilter() { OwnerId = OwnerId };
            FilterSelectedCategories = null;
            StateHasChanged();
        }

        private async Task GetLocation()
        {
            await JSRuntime.InvokeVoidAsync("getGeolocation", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public void ReceiveCoordinates(double latitude, double longitude)
        {
            Console.WriteLine($"Latitude: {latitude}, Longitude: {longitude}");
            Filter.UserLocation = new Geo()
            {
                Latitude = latitude,
                Longitude = longitude
            };
        }

        private async Task ReadSchools()
        {
            PagedList<SchoolDto> requests = await SchoolService.Find(Filter);
            TotalCount = requests.TotalCount;
            Schools = requests.Values.ToList();
        }

        public async Task OnFilterChange()
        {
            await SetToPageOne();
            await ReadSchools();
        }

        public async Task OnRead(LoadDataArgs args)
        {
            IsLoading = true;
            Filter.PageIndex = (args.Skip ?? 0) / Filter.PageSize;
            await ReadSchools();

            IsLoading = false;
        }

        public async Task OnItemClick(SchoolDto school)
        {
            if (OnSchoolClick.HasDelegate)
            {
                await OnSchoolClick.InvokeAsync(school);
            }
        }

        public async Task OnSchoolsUpdate()
        {
            await ReadSchools();
            StateHasChanged();
        }

        public void SelectedCategoriesChanged()
        {
            if (FilterSelectedCategories == null)
            {
                Filter.RatingCategoryFilters = new List<RatingCategoryFilter>();
            }
            else
            {
                IEnumerable<RatingCategory> toAdd = 
                    FilterSelectedCategories.ExceptByProperty(Filter.RatingCategoryFilters.Select(c => c.Category), c => c);
                IEnumerable<RatingCategory> toRemove =
                    Filter.RatingCategoryFilters.Select(c => c.Category).ExceptByProperty(FilterSelectedCategories, c => c);

                foreach (RatingCategory category in toAdd)
                {
                    Filter.RatingCategoryFilters.Add(new RatingCategoryFilter { Category = category });
                }

                foreach (RatingCategory category in toRemove)
                {
                    RatingCategoryFilter filter = Filter.RatingCategoryFilters.First(c => c.Category == category);
                    Filter.RatingCategoryFilters.Remove(filter);
                }
            }
            StateHasChanged();
        }

        private async Task SetToPageOne()
        {
            Filter.PageIndex = 0;
            await DataList.GoToPage(0);
        }
    }
}
