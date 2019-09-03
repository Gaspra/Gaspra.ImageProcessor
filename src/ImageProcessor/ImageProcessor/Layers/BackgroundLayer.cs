using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace ImageProcessor.Layers
{
    public class BackgroundLayer : IImageLayer
    {
        public string HexColourCode { get; }

        public BackgroundLayer(string hexColourCode)
        {
            HexColourCode = hexColourCode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            BackgroundLayer backgroundLayer = (BackgroundLayer)obj;

            return
                backgroundLayer.HexColourCode.Equals(HexColourCode);
        }

        public override int GetHashCode()
        {
            return HashCode
                .Combine(HexColourCode);
        }

        public Image<Rgba32> GenerateLayer(Resolution resolution)
        {
            var imageLayer = new Image<Rgba32>(
                resolution.Width,
                resolution.Height);

            imageLayer.Mutate(i => i
                .Fill(Rgba32.FromHex(HexColourCode)));

            return imageLayer;
        }
    }
}
