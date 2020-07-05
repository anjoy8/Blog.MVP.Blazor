using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    /// <summary>
    /// 配置状态服务处理器,定时校验授权状态
    /// RevalidationInterval为刷新时间,类似于滑动时间
    /// </summary>
    public class AuthStateHandler 
        : RevalidatingServerAuthenticationStateProvider
    {
        private readonly AuthStateCache Cache;

        public AuthStateHandler(
            ILoggerFactory loggerFactory,
            AuthStateCache cache)
            : base(loggerFactory)
        {
            Cache = cache;
        }

        protected override TimeSpan RevalidationInterval
            => TimeSpan.FromSeconds(10); // TODO read from config

        protected override Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            var sid =
                authenticationState.User.Claims
                .Where(c => c.Type.Equals("sid"))
                .Select(c => c.Value)
                .FirstOrDefault();

            if (sid != null && Cache.HasSubjectId(sid))
            {
                var data = Cache.Get(sid);

                System.Diagnostics.Debug.WriteLine($"NowUtc: {DateTimeOffset.UtcNow.ToString("o")}");
                System.Diagnostics.Debug.WriteLine($"ExpUtc: {data.Expiration.ToString("o")}");

                if(DateTimeOffset.UtcNow >= data.Expiration)
                {
                    System.Diagnostics.Debug.WriteLine($"*** EXPIRED ***");
                    Cache.Remove(sid);
                    return Task.FromResult(false);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"(not in cache)");
            }

            return Task.FromResult(true);
        }
    }
}
