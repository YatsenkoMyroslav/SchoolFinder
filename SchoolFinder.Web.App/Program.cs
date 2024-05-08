using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using SchoolFinder.Web.App;
using SchoolFinder.Web.App.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddFinder( 
    apiUrl: builder.Configuration.GetValue<string>("ApiHost")
);

await builder.Build().RunAsync();
