using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageProcessor.Layers
{
    public interface IImageLayer
    {
        Image<Rgba32> GenerateLayer(Resolution resolution);
    }
}
