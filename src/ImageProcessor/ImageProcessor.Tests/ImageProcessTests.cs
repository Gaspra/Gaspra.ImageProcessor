using ImageProcessor.Models;
using ImageProcessor.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using System.IO;

namespace ImageProcessor.Tests
{
    [TestClass]
    public class ImageProcessTests
    {
        //[TestMethod]
        //public void TestProcess()
        //{
        //    var resolution = new Resolution
        //    {
        //        Width = 567,
        //        Height = 1024
        //    };

        //    var assetPath = $"{Directory.GetCurrentDirectory()}/Asset/example.png";

        //    var outputPath = $"{Directory.GetCurrentDirectory()}/Asset/output.png";

        //    Begin(assetPath, outputPath, resolution);
        //}

        //public void Begin(
        //        string file,
        //        string output,
        //        Resolution resolution)
        //{
        //    var maltImage = new MaltImage(resolution, null, null);

        //    maltImage.LoadImage(file);

        //    var resProcessor = new ResolutionProcessor();

        //    if (resProcessor.Should(maltImage))
        //    {
        //        resProcessor.Execute(maltImage);
        //    }

        //    maltImage.RawImage.Save(output);
        //}

    }
}
