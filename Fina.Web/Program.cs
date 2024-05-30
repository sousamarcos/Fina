using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fina.Web;
using MudBlazor.Services;
using Fina.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services
    .AddHttpClient(
        WebConfiguration.HttpClientName,
        opt =>
        {
            opt.BaseAddress = new Uri(Configuration.BackEndURL);
        });

await builder.Build().RunAsync();
