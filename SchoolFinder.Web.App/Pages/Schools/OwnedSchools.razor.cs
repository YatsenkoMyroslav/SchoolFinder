using Microsoft.AspNetCore.Components;
using SchoolFinder.Web.App.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolFinder.Web.App.Pages.Schools
{
    public partial class OwnedSchools : FinderComponent
    {
        [Parameter]
        public Guid OwnerId { get; set; }
    }
}
