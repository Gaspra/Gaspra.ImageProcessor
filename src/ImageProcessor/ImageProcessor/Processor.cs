using ImageProcessor.Caches;
using ImageProcessor.Layers;
using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessor
{
    public class Processor : IProcessor
    {
        private readonly IEnumerable<IImageLayer> ImageLayers;
        private readonly IImageCache ImageCache;

        public Processor(IEnumerable<IImageLayer> imageLayers, IImageCache imageCache)
        {
            ImageLayers = imageLayers;
            ImageCache = imageCache;
        }

        public Image<Rgba32> Execute(ImageRequest imageRequest)
        {
            var renderLayers = new List<IImageLayer>();

            foreach(var layer in ImageLayers)
            {
                if(layer.BuildFrom(imageRequest))
                {
                    renderLayers.Add(layer);
                }
            }

            var maltImage = new MaltImage(imageRequest.Resolution, renderLayers);

            var image = ImageCache.TryGetImage(maltImage);

            return image;
        }
    }
}
