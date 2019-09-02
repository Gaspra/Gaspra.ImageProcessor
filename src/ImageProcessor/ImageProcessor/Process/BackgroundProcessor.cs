using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ImageProcessor.Process
{
    public class BackgroundProcessor : IImageProcessor
    {
        public void Execute(MaltImage maltImage)
        {

        }

        public bool Should(MaltImage maltImage)
        {
            return true;
        }
    }
}
