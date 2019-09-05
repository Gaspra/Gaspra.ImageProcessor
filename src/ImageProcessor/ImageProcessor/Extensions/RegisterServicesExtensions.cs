using ImageProcessor.Caches;
using ImageProcessor.Layers;
using Microsoft.Extensions.DependencyInjection;

namespace ImageProcessor.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection RegisterImageProcessorServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IImageLayer, BackgroundLayer>()
                .AddTransient<IImageLayer, MaltImageLayer>()
                .AddTransient<IImageLayer, WatermarkLayer>()
                .AddTransient<IProcessor, Processor>()
                .AddSingleton<IImageCache, ImageCache>();

            return serviceCollection;
        }
    }
}
