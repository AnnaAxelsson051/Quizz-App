// Fixad


using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Blazored.LocalStorage;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorQuiz.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
           
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext!);

            builder.Services.AddHttpClient("BlazorQuiz.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorQuiz.ServerAPI"));
            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}