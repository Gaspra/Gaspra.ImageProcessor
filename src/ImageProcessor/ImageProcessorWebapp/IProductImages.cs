namespace ImageProcessorWebapp
{
    public interface IProductImages
    {
        string GetImageDirectory();
        string GetImagePathFromName(string name);
    }
}
