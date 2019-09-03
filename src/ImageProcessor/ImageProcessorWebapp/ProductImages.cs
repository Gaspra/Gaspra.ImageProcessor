using Microsoft.Extensions.Configuration;
using System.IO;

namespace ImageProcessorWebapp
{
    public class ProductImages : IProductImages
    {
        public IConfiguration configuration;

        public ProductImages(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetImageDirectory()
        {
            var productImagesPath = "";

            var productImageSection = configuration.GetSection("ProductImages");

            if (productImageSection.Exists())
            {
                productImagesPath = productImageSection.Value;
            }
            else
            {
                productImagesPath = $"{Directory.GetCurrentDirectory()}/product_images/";
            }

            if (!productImagesPath.EndsWith("/") && !productImagesPath.EndsWith("\\"))
            {
                productImagesPath += "/";
            }

            return productImagesPath;
        }

        public string GetImagePathFromName(string name)
        {
            var imagePath = GetImageDirectory() + name;
            if (File.Exists(imagePath))
            {
                return imagePath;
            }
            return null;
        }
    }
}
