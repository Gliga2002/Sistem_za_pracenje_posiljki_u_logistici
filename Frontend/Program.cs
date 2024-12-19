using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthService>();



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5226/api/Posiljke/") });

await builder.Build().RunAsync();
