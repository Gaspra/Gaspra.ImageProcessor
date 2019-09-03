using ImageProcessor.Layers;
using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;

namespace ImageProcessor.Extensions
{
    public static class ImageProcessorExtensions
    {
        public static Image<Rgba32> ProcessImage(this MaltImage maltImage)
        {
            var backgroundLayer = new BackgroundLayer(maltImage.BackgroundHexColourCode);

            var maltImageLayer = new MaltImageLayer(maltImage.ImageLocation);

            var watermarkLayer = new WatermarkLayer(maltImage.WatermarkText);

            var imageLayers = new List<Image<Rgba32>>
            {
                backgroundLayer.GenerateLayer(maltImage.Resolution),
                maltImageLayer.GenerateLayer(maltImage.Resolution),
                watermarkLayer.GenerateLayer(maltImage.Resolution)
            };

            return CombineLayers(imageLayers, maltImage.Resolution);
        }

        public static Image<Rgba32> CombineLayers(IEnumerable<Image<Rgba32>> imageLayers, Resolution resolution)
        {
            var layeredImage = new Image<Rgba32>(resolution.Width, resolution.Height);

            foreach (var layer in imageLayers)
            {
                layeredImage
                    .Mutate(i => i.DrawImage(layer, 1));
            }

            return layeredImage;
        }
    }
}
