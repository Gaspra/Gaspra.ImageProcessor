using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ImageProcessor.Extensions;
using ImageProcessor.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageProcessorWebapp.Processor
{
    public class ProcessorController : Controller
    {
        private readonly IProductImages ProductImages;

        public ProcessorController(
            IProductImages productImages)
        {
            ProductImages = productImages ??
                throw new ArgumentNullException(nameof(productImages));
        }

        public IActionResult Index()
        {
            return View();
        }

        /*
            todo;
                tidy up!
        */
        public IActionResult Image()
        {
            /*
                query string / build image
            */
            var requestDictionary = QueryStringToDictionary(Request.QueryString.ToString());

            var imagePath = ProductImages.GetImagePathFromName(requestDictionary["imagename"]);

            if(!string.IsNullOrWhiteSpace(imagePath))
            {
                requestDictionary.TryGetValue("height", out var heightValue);
                int.TryParse(heightValue, out var height);

                requestDictionary.TryGetValue("width", out var widthValue);
                int.TryParse(widthValue, out var width);

                requestDictionary.TryGetValue("watermark", out var watermark);

                requestDictionary.TryGetValue("backgroundcolour", out var backgroundColour);

                var maltImage = new MaltImage(
                    imagePath,
                    watermark,
                    backgroundColour,
                    width,
                    height
                    );

                var renderedImage = maltImage.ProcessImage();

                /*
                    save image to disk
                */
                if (requestDictionary.ContainsKey("download") &&
                    bool.Parse(requestDictionary["download"]))
                {
                    SaveImage(renderedImage, $"{ProductImages.GetImageDirectory()}/output", Guid.NewGuid().ToString(), requestDictionary["imageformat"]);
                }

                /*
                    return image
                */
                using (MemoryStream m = new MemoryStream())
                {
                    renderedImage.Save(m, new PngEncoder());

                    byte[] imageBytes = m.ToArray();

                    string base64String = Convert.ToBase64String(imageBytes);

                    return base.File(imageBytes, "image/png");
                }
            }

            return BadRequest();
        }

        private IDictionary<string, string> QueryStringToDictionary(string queryString)
        {
            var requestDictionary = new Dictionary<string, string>();

            var decoded = HttpUtility.UrlDecode(queryString);

            var sanitized = decoded.Replace("?", "");

            var splitQuery = sanitized.Split('&');

            foreach(var splitPart in splitQuery)
            {
                var values = splitPart.Split('=');

                var key = values[0].ToLower();

                var value = values[1];

                requestDictionary.Add(key, value);
            }

            return requestDictionary;
        }

        private void SaveImage(Image<Rgba32> image, string outputDirectory, string fileName, string format)
        {
            Directory.CreateDirectory(outputDirectory);

            var sanitizedFileName = Path.GetFileNameWithoutExtension(fileName);

            image.Save($"{outputDirectory}/{sanitizedFileName}.{format}");
        }
    }
}