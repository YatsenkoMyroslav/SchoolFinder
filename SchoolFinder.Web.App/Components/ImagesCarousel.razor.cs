using Microsoft.AspNetCore.Components;
using SchoolFinder.Common;

namespace SchoolFinder.Web.App.Components
{
    public partial class ImagesCarousel : FinderComponent
    {
        [Parameter]
        public List<FileBytes> Photos { get; set; } = new List<FileBytes>();

        private const string _noContentImagePath = "img/NoContentImage.jpg";
    }
}
