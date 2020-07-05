using Blog.MVP.Blazor.SSR.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    public class AccessState
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly AuthStateCache _cache;
        private readonly NavigationManager _navigationManager;

        public AccessState(IHttpContextAccessor accessor, AuthStateCache cache, NavigationManager navigationManager)
        {
            _accessor = accessor;
            _cache = cache;
            _navigationManager = navigationManager;
        }

        public async Task<string> GetAccessToken()
        {
            // 获取当前用户的sid唯一标志
            var sid = _accessor.HttpContext.User.Claims
                   .Where(c => c.Type.Equals("sid"))
                   .Select(c => c.Value)
                   .FirstOrDefault();

            // 正常，则返回结果
            if (sid != null && _cache.HasSubjectId(sid))
            {
                return _cache.Get(sid).AccessToken;
            }

            // 否则，跳转登录页，去认证中心拉取
            _navigationManager.NavigateTo("/Login", true);

            return await Task.FromResult(string.Empty);

        }


    }
}
