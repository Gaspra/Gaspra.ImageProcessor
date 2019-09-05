using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace ImageProcessor.Models
{
    public class Resolution
    {
        public int Width { get; }
        public int Height { get; }

        public Resolution(int width, int height)
        {
            if (width > 3000) width = 3000;
            if (height > 3000) height = 3000;

            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            Resolution resolution = (Resolution)obj;

            return
                resolution.Width.Equals(Width)
                && resolution.Height.Equals(Height);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                typeof(Resolution), Width, Height);
        }

        public static Resolution From(Image<Rgba32> image)
        {
            return new Resolution(image.Width, image.Height);
        }
    }
}
