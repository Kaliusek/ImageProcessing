using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessingTests
{
    [TestClass]
    public class ImageProcessingLibraryTests
    {
        [TestMethod]
        public void PixelsShouldBeRedOrGreenOrBlue()
        {
            BitmapSource bitmapImage;

            PixelFormat pf = PixelFormats.Bgra32;
            int imageWidth = 200;
            int imageHeight = 200;
            int rawStride = (imageWidth * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * imageHeight];

            Random rnd = new Random();
            rnd.NextBytes(rawImage);

            bitmapImage = BitmapSource.Create(imageWidth, imageHeight, 96, 96, pf, null, rawImage, rawStride);

            ImageProcessingLibrary.ImageProcessing proc = new ImageProcessingLibrary.ImageProcessing();
            bitmapImage = proc.ToMainColors(bitmapImage);

            int width = bitmapImage.PixelWidth;
            int height = bitmapImage.PixelHeight;
            WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapImage);
            int stride = width * ((writeableBitmap.Format.BitsPerPixel + 7) / 8);
            int arraySize = height * stride;
            byte[] pixels = new byte[arraySize];
            writeableBitmap.CopyPixels(pixels, stride, 0);

            int j = 0;
            for (int i = 0; i < pixels.Length / 4; ++i)
            {
                int red = pixels[j];
                int green = pixels[j + 1];
                int blue = pixels[j + 2];

                Assert.IsTrue((red == 0 || red == 255));
                Assert.IsTrue((green == 0 || green == 255));
                Assert.IsTrue((blue == 0 || blue == 255));

                j += 4;
            }
        }
    }
}
