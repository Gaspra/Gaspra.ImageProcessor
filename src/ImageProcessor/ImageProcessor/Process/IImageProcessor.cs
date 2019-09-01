using ImageProcessor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Process
{
    public interface IImageProcessor
    {
        bool Should(MaltImage maltImage);
        void Execute(MaltImage maltImage);
    }

    
}
