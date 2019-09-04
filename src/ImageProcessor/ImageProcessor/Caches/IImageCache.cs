using ImageProcessor.Layers;
using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageProcessor.Caches
{
    public interface IImageCache
    {
        Image<Rgba32> TryGetImage(MaltImage maltImage);

        Image<Rgba32> TryGetLayer(IImageLayer imageLayer);
    }
}
