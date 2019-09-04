using ImageProcessor.Layers;
using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessor.Extensions
{
    public static class ImageLayerExtensions
    {
        public static Image<Rgba32> CombineLayers(this IEnumerable<IImageLayer> imageLayers)
        {
            var orderedLayers = imageLayers.OrderBy(i => i.LayerOrder);

            var bottomLayer = orderedLayers.First().LayerRender;

            foreach(var layer in orderedLayers.Skip(1))
            {
                bottomLayer.Mutate(i => i
                    .DrawImage(layer.LayerRender, layer.Opacity));
            }

            return bottomLayer;
        }
    }
}
