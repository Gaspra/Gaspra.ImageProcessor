using ImageProcessor.Models;
using Microsoft.AspNetCore.Http;

namespace ImageProcessor.Website.Models
{
    public class ImageRequest
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public static ImageRequest From(IFormCollection form)
        {
            return new ImageRequest
            {
                Height = int.Parse(form[nameof(Height)]),
                Width = int.Parse(form[nameof(Width)])
            };
        }

        public MaltImage TransformToMaltImage()
        {
            /*

                Create new malt image

            */

            var resolution = new Resolution()
            {
                Height = Height,
                Width = Width
            };

            return new MaltImage(resolution, null, null);
        }
    }
}
