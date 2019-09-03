using ImageProcessor.Models;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;

namespace ImageProcessor.Layers
{
    public class WatermarkLayer : IImageLayer
    {
        public string WatermarkText { get; }

        public WatermarkLayer(string watermarkText)
        {
            WatermarkText = watermarkText;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            WatermarkLayer watermarkLayer = (WatermarkLayer)obj;

            return
                watermarkLayer.WatermarkText.Equals(WatermarkText);
        }

        public override int GetHashCode()
        {
            return HashCode
                .Combine(WatermarkText);
        }

        public Image<Rgba32> GenerateLayer(Resolution resolution)
        {
            var imageLayer = new Image<Rgba32>(
                resolution.Width,
                resolution.Height);

            var font = SystemFonts.CreateFont("Arial", 39, FontStyle.Regular);

            float targetWidth = resolution.Width - (8);
            float targetHeight = resolution.Height - (8);

            SizeF size = TextMeasurer.Measure(WatermarkText, new RendererOptions(font));

            float scalingFactor = Math.Min(resolution.Width / size.Width, resolution.Height / size.Height);

            Font scaledFont = new Font(font, scalingFactor * font.Size);

            var center = new PointF(resolution.Width / 2, resolution.Height / 2);
            var textGraphicOptions = new TextGraphicsOptions(true)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            imageLayer.Mutate(i => i.DrawText(textGraphicOptions, WatermarkText, scaledFont, Rgba32.RebeccaPurple, center));

            return imageLayer;
        }
    }
}
