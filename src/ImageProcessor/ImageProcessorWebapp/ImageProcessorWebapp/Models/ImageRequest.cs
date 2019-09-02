using System;
using System.Collections.Generic;

namespace ImageProcessorWebapp.Models
{
    public class ImageRequest
    {
        public string ImageName { get; }
        public int Height { get; }
        public int Width { get; }
        public string BackgroundColour { get; }

        public ImageRequest(
            string imageName,
            int height,
            int width,
            string backgroundColour)
        {
            ImageName = imageName ?? throw new ArgumentNullException(nameof(imageName));
            Height = height;
            Width = width;
            BackgroundColour = backgroundColour ?? throw new ArgumentNullException(nameof(backgroundColour));
        }

        public static ImageRequest From(IDictionary<string,string> query)
        {
            return new ImageRequest(
                query[nameof(ImageName).ToLower()],
                int.Parse(query[nameof(Height).ToLower()]),
                int.Parse(query[nameof(Width).ToLower()]),
                query[nameof(BackgroundColour).ToLower()]
                );
        }
    }
}
