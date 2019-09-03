using ImageProcessor;
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
