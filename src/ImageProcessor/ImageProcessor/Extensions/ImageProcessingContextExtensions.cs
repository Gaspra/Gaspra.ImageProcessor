using SixLabors.Fonts;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;

namespace ImageProcessor.Extensions
{
    public static class ImageProcessingContextExtensions
    {
        public static IImageProcessingContext<TPixel> ApplyScalingWaterMarkSimple<TPixel>(this IImageProcessingContext<TPixel> processingContext, Font font, string text, TPixel color, float padding)
            where TPixel : struct, IPixel<TPixel>
        {
            return processingContext.Apply(img =>
            {
                float targetWidth = img.Width - (padding * 2);

                float targetHeight = img.Height - (padding * 2);

                SizeF size = TextMeasurer.Measure(text, new RendererOptions(font));

                float scalingFactor = Math.Min(img.Width / size.Width, img.Height / size.Height);

                Font scaledFont = new Font(font, scalingFactor * font.Size);

                var center = new PointF(img.Width / 2, img.Height / 2);

                var textGraphicOptions = new TextGraphicsOptions(true)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                img.Mutate(i => i.DrawText(textGraphicOptions, text, scaledFont, color, center));
            });
        }
    }
}
