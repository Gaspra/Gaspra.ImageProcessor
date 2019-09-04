using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageProcessor
{
    public interface IProcessor
    {
        Image<Rgba32> Execute(ImageRequest imageRequest);
    }
}
