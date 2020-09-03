using Blog.MVP.Blazor.SSR.Services;
using Blog.MVP.Blazor.SSR.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.HttpOverrides;

namespace Blog.MVP.Blazor.SSR
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //nginx 
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            // 配置Hsts
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("mvp.neters.club");
            });

            // 非开发环境
            if (!_env.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                  {
                      options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                      options.HttpsPort = 443;
                  }); 
            }

            services.AddSameSiteCookiePolicy();

            // services and state
            services.AddScoped<BlogService>();
            services.AddScoped<AccessState>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddTransient<HttpClient>();

            // 第一部分:认证方案的配置
            // add cookie-based session management with OpenID Connect authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies", options =>
            {
                //options.Cookie.Name = "blazorclient";

                //options.ExpireTimeSpan = TimeSpan.FromHours(1);
                //options.SlidingExpiration = false;
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://ids.neters.club/";
                options.RequireHttpsMetadata = false;//必须https协议
                options.ClientId = "blazorserver"; // 75 seconds
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.SaveTokens = true;

                // 为api在使用refresh_token的时候,配置offline_access作用域
                options.GetClaimsFromUserInfoEndpoint = true;
                // 作用域获取
                options.Scope.Clear();
                options.Scope.Add("roles");//"roles"
                options.Scope.Add("rolename");//"rolename"
                options.Scope.Add("blog.core.api");
                options.Scope.Add("profile");
                options.Scope.Add("openid");

                options.Events = new OpenIdConnectEvents
                {
                    // called if user clicks Cancel during login
                    OnAccessDenied = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/");
                        return Task.CompletedTask;
                    }
                };
            });

            // 第三部分：授权状态的保护与管理
            services.AddSingleton<AuthStateCache>();
            // 开启AuthenticationStateProvider 服务
            services.AddScoped<AuthenticationStateProvider, AuthStateHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();

            // ******
            // BLAZOR COOKIE Auth Code (begin)
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseAuthentication();
            // BLAZOR COOKIE Auth Code (end)
            // ******

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}