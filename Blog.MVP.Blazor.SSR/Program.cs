using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;

namespace Blog.MVP.Blazor.SSR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.IPv6Any, 443, listenOptions =>
                        {
                            listenOptions.UseHttps(Path.Combine(AppContext.BaseDirectory, "socialnetwork.pfx"), "123456");
                        });
                    })
                    //.UseUrls("https://*:5050")
                    ;
                });
    }
}
