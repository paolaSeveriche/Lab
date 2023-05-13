using CurrieTechnologies.Razor.SweetAlert2;
using Lab.WEB.Repositories;
using Lab.WEB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Lab.WEB.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IRepository, Repository>();
// Se debe inyectar el servicio de Alert Para que funcione luego ir a INDEX HTML en www.root
builder.Services.AddSweetAlert2();
builder.Services.AddAuthorizationCore();

//LOGIN


builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderTest>();



//.AddSingleton-> inyectando el api para que consuma el web
//AddSingleton-> Deja loguear
builder.Services.AddSingleton(sp => new HttpClient{BaseAddress = new Uri("https://localhost:7083/") });

await builder.Build().RunAsync();
