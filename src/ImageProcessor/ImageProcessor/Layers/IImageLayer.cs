using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace ImageProcessor.Layers
{
    public interface IImageLayer
    {
        Resolution Resolution { get; }
        int LayerOrder { get; }
        float Opacity { get; }
        Image<Rgba32> LayerRender { get; set; }
        bool BuildFrom(ImageRequest imageRequest);
    }
}
