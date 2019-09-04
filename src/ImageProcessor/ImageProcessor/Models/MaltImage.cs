using ImageProcessor.Layers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;

namespace ImageProcessor.Models
{
    public class MaltImage
    {
        public Resolution Resolution { get; }
        public IEnumerable<IImageLayer> ImageLayers { get; }

        public MaltImage(
            Resolution resolution,
            IEnumerable<IImageLayer> imageLayers)
        {
            Resolution = resolution;
            ImageLayers = imageLayers;
        }

        public override int GetHashCode()
        {
            var newHash = new HashCode();

            foreach(var layer in ImageLayers)
            {
                newHash.Add(layer.GetHashCode());
            }

            return newHash.ToHashCode();
        }
    }
}