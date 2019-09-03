using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ImageProcessor.Models;
using ImageProcessor.Process;
using ImageProcessorWebapp.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace ImageProcessorWebapp.Processor
{
    public class ProcessorController : Controller
    {
        private readonly IProductImages ProductImages;
        private readonly IEnumerable<IImageProcessor> ImageProcessors;

        public ProcessorController(
            IProductImages productImages,
            IEnumerable<IImageProcessor> imageProcessors)
        {
            ProductImages = productImages ?? throw new ArgumentNullException(nameof(productImages));
            ImageProcessors = imageProcessors ?? throw new ArgumentNullException(nameof(productImages));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Image()
        {
            /*
                query string / build image
            */
            var requestDictionary = QueryStringToDictionary(Request.QueryString.ToString());

            var imageRequest = ImageRequest.From(requestDictionary);

            var maltImage = new MaltImage(new Resolution() { Height = imageRequest.Height, Width = imageRequest.Width }, null, null);

            // check cache for the image before loading it
            maltImage.LoadImage(ProductImages.GetImageDirectory() + imageRequest.ImageName);

            /*
                process image
            */
            foreach (var imageProcessor in ImageProcessors)
            {
                if (imageProcessor.Should(maltImage))
                {
                    imageProcessor.Execute(maltImage);
                }
            }

            /*
                save image to disk
            */
            if (requestDictionary.ContainsKey("download") &&
                bool.Parse(requestDictionary["download"]))
            {
                SaveImage(maltImage, imageRequest.ImageName, requestDictionary["imageformat"]);
            }

            /*
                return image
            */
            using (MemoryStream m = new MemoryStream())
            {
                maltImage.RawImage.Save(m, new PngEncoder());

                byte[] imageBytes = m.ToArray();

                string base64String = Convert.ToBase64String(imageBytes);

                //return Content(base64String, "image/png");

                return base.File(imageBytes, "image/png");
            }
        }

        private void SaveImage(MaltImage maltImage, string fileName, string format)
        {
            var outputPath = $"{ProductImages.GetImageDirectory()}/output/";

            Directory.CreateDirectory(outputPath);

            var sanitizedFileName = Path.GetFileNameWithoutExtension(fileName);

            maltImage.RawImage.Save($"{outputPath}{sanitizedFileName}_{maltImage.ToString()}.{format}");
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
    }
}