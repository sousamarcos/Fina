using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fina.Web;
using MudBlazor.Services;
using Fina.Core;
using Fina.Web.Services;
using Fina.Core.Services;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddTransient<MudLocalizer, CustomMudLocalizer>();


builder.Services
    .AddHttpClient(
        WebConfiguration.HttpClientName,
        opt =>
        {
            opt.BaseAddress = new Uri(Configuration.BackEndURL);
        });

builder.Services.AddTransient<ICategoryService, CategoryService>();

await builder.Build().RunAsync();
