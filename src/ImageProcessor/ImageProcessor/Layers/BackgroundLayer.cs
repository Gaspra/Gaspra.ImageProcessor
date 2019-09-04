using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace ImageProcessor.Layers
{
    public class BackgroundLayer : IImageLayer
    {
        public Resolution Resolution { get; set;  }
        public int LayerOrder => 0;
        public float Opacity => 1;
        public string HexColourCode { get; set; }

        private Image<Rgba32> layerRender = null;
        public Image<Rgba32> LayerRender
        {
            get
            {
                if (layerRender == null)
                {
                    layerRender = new Image<Rgba32>(
                        Resolution.Width,
                        Resolution.Height);

                    layerRender.Mutate(i => i
                        .Fill(Rgba32.FromHex(HexColourCode)));
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
                .Combine(typeof(BackgroundLayer), HexColourCode, Resolution.GetHashCode());
        }

        public bool BuildFrom(ImageRequest imageRequest)
        {
            if(string.IsNullOrWhiteSpace(imageRequest.BackgroundHexColourCode))
            {
                return false;
            }

            Resolution = imageRequest.Resolution;
            HexColourCode = imageRequest.BackgroundHexColourCode;

            return true;
        }
    }
}
