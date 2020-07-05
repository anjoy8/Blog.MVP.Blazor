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

namespace Blog.MVP.Blazor.SSR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // services and state
            services.AddScoped<BlogService>();
            services.AddScoped<AccessState>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddTransient<HttpClient>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // 第一部分:认证方案的配置
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://ids.neters.club/";
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
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
