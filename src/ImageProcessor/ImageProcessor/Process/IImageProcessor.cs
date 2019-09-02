using ImageProcessor.Models;

namespace ImageProcessor.Process
{
    public interface IImageProcessor
    {
        bool Should(MaltImage maltImage);
        void Execute(MaltImage maltImage);
    }


}
