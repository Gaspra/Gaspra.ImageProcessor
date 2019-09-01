using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using ImageProcessor.Process;
using ImageProcessor.Website.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageProcessor.Website.Image
{
    public class ImageController : Controller
    {
        private IEnumerable<IImageProcessor> imageProcessors;

        public ImageController()
        {
            imageProcessors = new List<IImageProcessor>
            {
                new ResolutionProcessor()//,
                //new BackgroundProcessor(),
                //new WatermarkProcessor()
            };
        }

        public void Index()
        {
            var maltImage = ImageRequest
                .From(Request.Form)
                .TransformToMaltImage();

            maltImage.LoadImage(Directory.GetCurrentDirectory()+"/example.png");

            foreach(var processor in imageProcessors)
            {
                if(processor.Should(maltImage))
                {
                    processor.Execute(maltImage);
                }
            }

            maltImage.RawImage.Save("D:/Temp/test.png");

            //return File(
            //
            //    imageByteArray,
            //    System.Net.Mime.MediaTypeNames.Application.Octet,
            //    "D:/Temp/test.png");
        }
    }
}