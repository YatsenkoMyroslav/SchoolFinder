using Microsoft.AspNetCore.Components;
using SchoolFinder.Web.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolFinder.Web.App.Components
{
    public partial class AuthenticatedComponent : FinderComponent
    {
        [Parameter]
        public RenderFragment? Authenticated { get; set; }
        [Parameter]
        public RenderFragment? NotAuthenticated { get; set;}

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}
