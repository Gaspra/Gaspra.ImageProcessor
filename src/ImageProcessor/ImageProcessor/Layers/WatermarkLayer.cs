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
        public Resolution Resolution { get; set; }
        public int LayerOrder => 2;
        public float Opacity => 0.6f;
        public string WatermarkText { get; set; }

        private Image<Rgba32> layerRender = null;
        public Image<Rgba32> LayerRender
        {
            get
            {
                if (layerRender == null)
                {
                    layerRender = GenerateLayer();
                }

                return layerRender;
            }
            set
            {
                layerRender = value;
            }
        }

        public override int GetHashCode()
        {
            return HashCode
                .Combine(typeof(WatermarkLayer), WatermarkText, Resolution.GetHashCode());
        }

        private Image<Rgba32> GenerateLayer()
        {
            var imageLayer = new Image<Rgba32>(
                Resolution.Width,
                Resolution.Height);

            var font = SystemFonts.CreateFont("Arial", 39, FontStyle.Regular);

            float targetWidth = Resolution.Width - (8);
            float targetHeight = Resolution.Height - (8);

            SizeF size = TextMeasurer.Measure(WatermarkText, new RendererOptions(font));

            float scalingFactor = Math.Min(Resolution.Width / size.Width, Resolution.Height / size.Height);

            Font scaledFont = new Font(font, scalingFactor * font.Size);

            var center = new PointF(Resolution.Width / 2, Resolution.Height / 2);
            var textGraphicOptions = new TextGraphicsOptions(true)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            imageLayer.Mutate(i => i.DrawText(textGraphicOptions, WatermarkText, scaledFont, Rgba32.RebeccaPurple, center));

            return imageLayer;
        }

        public bool BuildFrom(ImageRequest imageRequest)
        {
            if(string.IsNullOrWhiteSpace(imageRequest.WatermarkText))
            {
                return false;
            }

            Resolution = imageRequest.Resolution;
            WatermarkText = imageRequest.WatermarkText;

            return true;
        }
    }
}
