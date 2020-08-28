using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
                        options.Listen(IPAddress.Loopback, 5050, listenOptions =>
                        {
                            listenOptions.UseHttps("server.pfx", "123456");
                        });
                    })
                    //.UseUrls("https://*:5050")
                    ;
                });
    }
}
