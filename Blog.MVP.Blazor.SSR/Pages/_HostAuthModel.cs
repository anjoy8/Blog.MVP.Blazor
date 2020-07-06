using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.MVP.Blazor.SSR.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Blog.MVP.Blazor.SSR.Pages
{
    // 第二部分: 配置razor page,定义登录,登出等逻辑
    public class _HostAuthModel : PageModel
    {
        public readonly AuthStateCache Cache;

        public _HostAuthModel(AuthStateCache cache)
        {
            Cache = cache;
        }

        // 每次刷新页面异步加载
        public async Task<IActionResult> OnGet()
        {
            System.Diagnostics.Debug.WriteLine($"\n_Host OnGet IsAuth? {User.Identity.IsAuthenticated}");

            // 判断Httpcontext是否登录状态
            if (User.Identity.IsAuthenticated)
            {
                var sid = User.Claims
                    .Where(c => c.Type.Equals("sid"))
                    .Select(c => c.Value)
                    .FirstOrDefault();

                Console.WriteLine($"sid: {sid}");

                // 如果缓存中不存在
                if (sid != null && !Cache.HasSubjectId(sid))
                {
                    var authResult = await HttpContext.AuthenticateAsync("oidc");
                    DateTimeOffset expiration = authResult.Properties.ExpiresUtc.Value;
                    string accessToken = await HttpContext.GetTokenAsync("access_token");
                    string refreshToken = await HttpContext.GetTokenAsync("refresh_token");

                    Console.WriteLine($"accessToken: {accessToken}");
                    Cache.Add(sid, expiration, accessToken, refreshToken);
                }
            }

            return Page();
        }

        // 登录
        public IActionResult OnGetLogin()
        {
            System.Diagnostics.Debug.WriteLine("\n_Host OnGetLogin");
            var authProps = new AuthenticationProperties
            {
                IsPersistent = true,
                // 设置token的过期时间，相当于前端的localstorage
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                RedirectUri = Url.Content("~/")
            };

            // 认证中心登录
            return Challenge(authProps, "oidc");
        }

        // 登出
        public async Task OnGetLogout()
        {
            System.Diagnostics.Debug.WriteLine("\n_Host OnGetLogout");
            var authProps = new AuthenticationProperties
            {
                RedirectUri = Url.Content("~/")
            };
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc", authProps);
        }
    }

}
