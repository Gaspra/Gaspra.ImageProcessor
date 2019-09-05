using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace ImageProcessorWebapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseUrls("http://*:16533")
                .ConfigureLogging((logging) =>
                {
                    logging
                        .AddSeq("http://localhost:5341")
                        .AddConsole();
                })
                .UseStartup<Startup>();
    }
}
