using Blog.MVP.Blazor.SSR.Extensions;
using Blog.MVP.Blazor.SSR.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blog.MVP.Blazor.SSR.Services
{
    /// <summary>
    /// 服务基类
    /// 主要用来对Http请求的基础封装
    /// </summary>
    public class BlogService : BaseService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public BlogService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 获取全部博文
        /// </summary>
        /// <param name="types"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<MessageModel<List<BlogArticleVo>>> GetBlogs(string types, int page = 1)
        {
            var httpClient = await SecurityHttpClientAsync();
            return await httpClient.GetFromJsonAsync<MessageModel<List<BlogArticleVo>>>($"/api/Blog/GetBlogsByTypesForMVP?types={types}&page={page}");
        }

        /// <summary>
        /// 获取博文详情
        /// </summary>
        /// <param name="types"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<MessageModel<BlogArticle>> GetBlogByIdForMVP(int id = 0)
        {
            var httpClient = await SecurityHttpClientAsync();
            return await httpClient.GetFromJsonAsync<MessageModel<BlogArticle>>($"/api/Blog/GetBlogByIdForMVP?id={id}");
        }

        /// <summary>
        /// 更新博客
        /// </summary>
        /// <param name="blogArticle"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> UpdateBlog(BlogArticle blogArticle)
        {
            var httpClient = await SecurityHttpClientAsync();
            return await httpClient.PutAsJsonAsync("/api/Blog/Update", blogArticle);
        }

        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="blogArticle"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> AddForMVP(BlogArticle blogArticle)
        {
            var httpClient = await SecurityHttpClientAsync();
            return await httpClient.PostAsJsonAsync("/api/Blog/AddForMVP", blogArticle);
        }

    }
}