using ImageProcessor;
using ImageProcessor.Caches;
using ImageProcessor.Layers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageProcessorWebapp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddMemoryCache()
                .AddTransient<IImageLayer, BackgroundLayer>()
                .AddTransient<IImageLayer, MaltImageLayer>()
                .AddTransient<IImageLayer, WatermarkLayer>()
                .AddTransient<IProcessor, ImageProcessor.Processor>()
                .AddSingleton<IImageCache, ImageCache>()
                .AddSingleton<IProductImages, ProductImages>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseDeveloperExceptionPage()
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseMvc(builder =>
                {
                    builder.MapRoute(
                        name: "default",
                        template: "{controller}/{action}/{id?}",
                        defaults: new { controller="Processor", action="Index" });
                });
        }
    }
}
