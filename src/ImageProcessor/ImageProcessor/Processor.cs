using ImageProcessor.Caches;
using ImageProcessor.Extensions;
using ImageProcessor.Layers;
using ImageProcessor.Models;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;

namespace ImageProcessor
{
    public class Processor : IProcessor
    {
        private readonly ILogger logger;
        private readonly IEnumerable<IImageLayer> imageLayers;
        private readonly IImageCache imageCache;

        public Processor(
            ILogger<Processor> logger,
            IEnumerable<IImageLayer> imageLayers,
            IImageCache imageCache)
        {
            this.logger = logger;
            this.imageLayers = imageLayers;
            this.imageCache = imageCache;
        }

        public Image<Rgba32> Execute(ImageRequest imageRequest)
        {
            logger.BeginProcessingImageRequest(imageRequest);

            var renderLayers = new List<IImageLayer>();

            foreach(var layer in imageLayers)
            {
                if(layer.BuildFrom(imageRequest))
                {
                    renderLayers.Add(layer);
                }
            }

            var maltImage = new MaltImage(imageRequest.Resolution, renderLayers);

            var image = imageCache.TryGetImage(maltImage);

            return image;
        }
    }
}
