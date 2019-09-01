using ImageProcessor.Models;
using SixLabors.ImageSharp.Processing;

namespace ImageProcessor.Process
{
    public class ResolutionProcessor : IImageProcessor
    {
        public void Execute(MaltImage maltImage)
        {
            maltImage.RawImage.Mutate(i =>
                i.Resize(
                    maltImage.Resolution.Width,
                    maltImage.Resolution.Height));
        }

        public bool Should(MaltImage maltImage)
        {
            var imageResolution = Resolution
                .From(maltImage.RawImage);

            return !imageResolution
                .Equals(maltImage.Resolution);
        }

    }
}
