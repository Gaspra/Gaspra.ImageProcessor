using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace ImageProcessor.Models
{
    public class Resolution
    {
        public int Width { get; set; }

        public int Height { get; set; }

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
            return HashCode.Combine(Width, Height);
        }

        public static Resolution From(Image<Rgba32> image)
        {
            return new Resolution()
            {
                Width = image.Width,
                Height = image.Height
            };
        }
    }
}
