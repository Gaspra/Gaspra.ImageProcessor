using ImageProcessor.Process;
using Microsoft.Extensions.DependencyInjection;

namespace ImageProcessor
{
    public static class RegisterImageProcessorsExtension
    {
        public static IServiceCollection RegisterImageProcessors(this IServiceCollection collection)
        {
            collection
                .AddTransient<IImageProcessor, ResolutionProcessor>()
                .AddTransient<IImageProcessor, BackgroundProcessor>()
                //.AddTransient<IImageProcessor, WatermarkProcessor>()
                ;

            return collection;
        }
    }
}
