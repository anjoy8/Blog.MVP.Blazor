using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    public abstract class BaseService
    {
        protected BaseService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        protected IServiceProvider ServiceProvider { get; }
        protected HttpClient HttpClient => ServiceProvider.GetService<HttpClient>();
        protected IJSRuntime JS => ServiceProvider.GetService<IJSRuntime>();
        protected AccessState AccessState => ServiceProvider.GetService<AccessState>();

        protected async Task<HttpClient> SecurityHttpClientAsync()
        {
            var httpClient = ServiceProvider.GetService<HttpClient>();
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            var token = await AccessState.GetAccessToken();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            httpClient.BaseAddress = new Uri("http://apk.neters.club");
            return httpClient;
        }
    }
}
