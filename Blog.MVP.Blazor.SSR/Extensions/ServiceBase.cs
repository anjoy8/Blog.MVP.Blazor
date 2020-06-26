using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    public abstract class ServiceBase
    {
        protected ServiceBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        protected IServiceProvider ServiceProvider { get; }
        protected HttpClient HttpClient => ServiceProvider.GetService<HttpClient>();
        protected IJSRuntime JS => ServiceProvider.GetService<IJSRuntime>();
        protected AppState AppState => ServiceProvider.GetService<AppState>();

        protected async Task<HttpClient> SecureHttpClientAsync()
        {
            var httpClient = ServiceProvider.GetService<HttpClient>();
            //AppState.SetUserInfo(await JS.GetUserInfoAsync());
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            var token = await AppState.GetAccessToken();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return httpClient;
        }
    }
}
