using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SchoolFinder.Web.App.Services
{
    public class FinderNavigationManager
    {
        private readonly NavigationManager _navigator;

        public string Uri => _navigator.Uri;

        public FinderNavigationManager(NavigationManager navigator)
        {
            _navigator = navigator;
        }

        public void NavigateTo(string uri, bool forceLoad = false)
        {
            _navigator.NavigateTo(uri, forceLoad);
        }

        public void NavigateToLogin()
        {
            _navigator.NavigateTo("/login");
        }

        public void NavigateToLogout()
        {
            _navigator.NavigateTo("/logout");
        }

        public void NavigateToRegistration()
        {
            _navigator.NavigateTo("/registration");
        }

        public Uri ToAbsoluteUri(string relativeUri)
        {
            return _navigator.ToAbsoluteUri(relativeUri);
        }
    }
}
