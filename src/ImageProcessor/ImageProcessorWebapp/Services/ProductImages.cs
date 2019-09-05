using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace ImageProcessorWebapp.Services
{
    public class ProductImages : IProductImages
    {
        public IConfiguration configuration;

        public ProductImages(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ImageDirectory()
        {
            return $"{Directory.GetCurrentDirectory()}/product_images/";
        }

        public IEnumerable<string> ImageList()
        {
            var directoryImages = Directory.GetFiles(ImageDirectory());

            var productImages = new List<string>();

            foreach(var image in directoryImages)
            {
                productImages.Add(Path.GetFileName(image));
            }

            return productImages;
        }
    }
}
