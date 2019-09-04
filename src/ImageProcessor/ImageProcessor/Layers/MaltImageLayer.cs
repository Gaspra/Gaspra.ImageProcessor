using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace ImageProcessor.Layers
{
    public class MaltImageLayer : IImageLayer
    {
        public Resolution Resolution { get; set; }
        public int LayerOrder => 1;
        public float Opacity => 1;
        public string ImagePath { get; set; }

        private Image<Rgba32> layerRender = null;
        public Image<Rgba32> LayerRender
        {
            get
            {
                if (layerRender == null)
                {
                    layerRender = Image.Load(ImagePath);

                    if (!Resolution.From(layerRender).Equals(Resolution))
                    {
                        layerRender.Mutate(i =>
                            i.Resize(Resolution.Width, Resolution.Height));
                    }
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
                .Combine(typeof(MaltImageLayer), ImagePath, Resolution.GetHashCode());
        }

        public bool BuildFrom(ImageRequest imageRequest)
        {
            if (string.IsNullOrWhiteSpace(imageRequest.ImageLocation))
            {
                return false;
            }

            Resolution = imageRequest.Resolution;
            ImagePath = imageRequest.ImageLocation;

            return true;
        }
    }
}
