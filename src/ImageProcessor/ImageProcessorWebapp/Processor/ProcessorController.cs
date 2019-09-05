using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ImageProcessor;
using ImageProcessor.Models;
using ImageProcessorWebapp.Extensions;
using ImageProcessorWebapp.Models;
using ImageProcessorWebapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageProcessorWebapp.Processor
{
    public class ProcessorController : Controller
    {
        private readonly ILogger logger;
        private readonly IProductImages productImages;
        private readonly IProcessor processor;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProcessorController(
            ILogger<ProcessorController> logger,
            IProductImages productImages,
            IProcessor processor,
            IHostingEnvironment hostingEnvironment)
        {
            this.logger = logger;

            this.productImages = productImages ??
                throw new ArgumentNullException(nameof(productImages));

            this.processor = processor ??
                throw new ArgumentNullException(nameof(processor));

            this.hostingEnvironment = hostingEnvironment ??
                throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        public IActionResult Index()
        {
            var model = new ProductImagesModel
            {
                Filenames = productImages.ImageList()
            };

            return View(model);
        }

        public IActionResult Image()
        {
            try
            {
                logger.ImageRequest(Request);

                var requestDictionary = QueryStringToDictionary(Request.QueryString.ToString());

                var imageRequest = BuildImageRequest(requestDictionary);

                var renderedImage = processor.Execute(imageRequest);

                /*
                    save image to disk if download requested
                */
                if (requestDictionary.ContainsKey("download") &&
                    bool.Parse(requestDictionary["download"]))
                {
                    SaveImage(renderedImage, $"{productImages.ImageDirectory()}/output", Guid.NewGuid().ToString(), requestDictionary["imageformat"]);
                }

                /*
                    return image
                */
                using (MemoryStream m = new MemoryStream())
                {
                    renderedImage.Save(m, ImageEncoderForFormat(requestDictionary["imageformat"]));

                    byte[] imageBytes = m.ToArray();

                    string base64String = Convert.ToBase64String(imageBytes);

                    return base.File(imageBytes, $"image/{requestDictionary["imageformat"]}");
                }
            }
            catch (Exception ex)
            {
                logger.BadImageRequest(Request, ex);

                if (hostingEnvironment.IsDevelopment())
                {
                    return BadRequest(ex);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        private ImageRequest BuildImageRequest(IDictionary<string,string> requestDictionary)
        {
            var imagePath = $"{productImages.ImageDirectory()}{requestDictionary["imagename"]}";

            requestDictionary.TryGetValue("height", out var heightValue);
            int.TryParse(heightValue, out var height);

            requestDictionary.TryGetValue("width", out var widthValue);
            int.TryParse(widthValue, out var width);

            requestDictionary.TryGetValue("watermark", out var watermark);

            requestDictionary.TryGetValue("backgroundcolour", out var backgroundColour);

            var imageRequest = new ImageRequest(
                imagePath,
                watermark,
                backgroundColour,
                width,
                height
                );

            return imageRequest;
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

                if (values.Length.Equals(2))
                {
                    var key = values[0].ToLower();

                    var value = values[1];

                    requestDictionary.Add(key, value);
                }
            }

            return requestDictionary;
        }

        private void SaveImage(Image<Rgba32> image, string outputDirectory, string fileName, string format)
        {
            Directory.CreateDirectory(outputDirectory);

            var sanitizedFileName = Path.GetFileNameWithoutExtension(fileName);

            logger.LogDebug($"Saving image to: {outputDirectory}/{sanitizedFileName}.{format}");

            image.Save($"{outputDirectory}/{sanitizedFileName}.{format}");
        }

        private IImageEncoder ImageEncoderForFormat(string format)
        {
            switch(format.ToLower())
            {
                case "png":
                    return new PngEncoder();

                case "jpeg":
                    return new JpegEncoder();

                case "bmp":
                    return new BmpEncoder();

                case "gif":
                    return new GifEncoder();

                default:
                    throw new ArgumentException(nameof(format));
            }
        }
    }
}