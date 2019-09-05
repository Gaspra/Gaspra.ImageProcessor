using ImageProcessor.Extensions;
using ImageProcessorWebapp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ImageProcessorWebapp
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache(options => {
                    options.SizeLimit = configuration
                        .GetSection("CacheOptions")
                        .GetValue<long>("CacheSizeLimit");
                    })
                .RegisterImageProcessorServices()
                .AddSingleton<IProductImages, ProductImages>()
                .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app)
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
