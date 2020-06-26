using Blog.MVP.Blazor.SSR.Extensions;
using Blog.MVP.Blazor.SSR.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blog.MVP.Blazor.SSR.Services
{
    public class BlogService : ServiceBase
    {
        public BlogService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        public async Task<MessageModel<List<BlogArticle>>> GetBlogs(int page = 1)
        {
            var httpClient = await SecureHttpClientAsync();
            return await httpClient.GetFromJsonAsync<MessageModel<List<BlogArticle>>>($"http://apk.neters.club/api/Blog/GetBlogsByTypesForMVP?types=MVP_azure_2020|MVP_aspnetcore-abp-blazor_2020|MVP_ids4_2020&page={page}");
        }
        public async Task<HttpResponseMessage> UpdateBlog(BlogArticle blogArticle)
        {
            var httpClient = await SecureHttpClientAsync();
            return await httpClient.PutAsJsonAsync("http://apk.neters.club/api/Blog/Update", blogArticle);
        }

    }
}
