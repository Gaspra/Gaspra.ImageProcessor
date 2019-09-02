using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace ImageProcessor.Models
{
    public class MaltImage : IDisposable
    {
        public Image<Rgba32> RawImage { get; set; }
        public Resolution Resolution { get; }
        public Watermark Watermark { get; }
        public Background Background { get; }

        public MaltImage(
            Resolution resolution,
            Watermark watermark,
            Background background)
        {
            Resolution = resolution;
            Watermark = watermark;
            Background = background;
        }

        public void LoadImage(string filePath)
        {
            RawImage = Image.Load(filePath);
        }

        public void Dispose()
        {
            RawImage.Dispose();
        }

        public override string ToString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
