using SixLabors.ImageSharp;

namespace ImageProcessor.Models
{
    public class ImageRequest
    {
        public string ImageLocation { get; }
        public string WatermarkText { get; }
        public string BackgroundHexColourCode { get; }
        public Resolution Resolution { get; }

        public ImageRequest(
            string imageLocation,
            string watermarkText,
            string backgroundHexColourCode,
            int width,
            int height)
        {
            ImageLocation = imageLocation;
            WatermarkText = watermarkText;
            BackgroundHexColourCode = backgroundHexColourCode;

            Resolution resolution = null;
            if(width.Equals(0) ||
                height.Equals(0))
            {
                using (var image = Image.Load(ImageLocation))
                {
                    var imageWidth = image.Width;
                    var imageHeight = image.Height;

                    if(width.Equals(0) && height.Equals(0))
                    {
                        resolution = new Resolution(
                            image.Width,
                            image.Height);
                    }
                    else if (width.Equals(0) && !height.Equals(0))
                    {
                        resolution = new Resolution(
                            (int)(image.Width * ((float)height / (float)image.Height)),
                            height);
                    }
                    else if (!width.Equals(0) && height.Equals(0))
                    {
                        resolution = new Resolution(
                            width,
                            (int)(image.Height * ((float)width / (float)image.Width)));
                    }
                }
            }
            else
            {
                resolution = new Resolution(width, height);
            }

            Resolution = resolution;
        }
    }
}
