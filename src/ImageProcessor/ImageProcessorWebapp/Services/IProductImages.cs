using System.Collections.Generic;

namespace ImageProcessorWebapp.Services
{
    public interface IProductImages
    {
        string ImageDirectory();
        IEnumerable<string> ImageList();
    }
}
