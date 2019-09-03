using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace ImageProcessor.Layers
{
    public class MaltImageLayer : IImageLayer
    {
        public string ImagePath { get; }

        public MaltImageLayer(string imagePath)
        {
            ImagePath = imagePath;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            MaltImageLayer maltImageLayer = (MaltImageLayer)obj;

            return
                maltImageLayer.ImagePath.Equals(ImagePath);
        }

        public override int GetHashCode()
        {
            return HashCode
                .Combine(ImagePath);
        }

        public Image<Rgba32> GenerateLayer(Resolution resolution)
        {
            var imageLayer = Image.Load(ImagePath);

            if(!imageLayer.Width.Equals(resolution.Width) &&
                !imageLayer.Height.Equals(resolution.Height))
            {
                imageLayer.Mutate(i =>
                    i.Resize(resolution.Width, resolution.Height));
            }

            return imageLayer;
        }
    }
}
