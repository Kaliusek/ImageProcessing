using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessingLibrary
{
    public class ImageProcessing
    {
        public ImageProcessing()
        {
            _watch = new Stopwatch();
        }

        private double _secondsElapsedForChangeColors;
        public double SecondsElapsedForChangeColors => _secondsElapsedForChangeColors;

        private Stopwatch _watch;

        public BitmapSource LoadImage(string fileName)
        {
            try
            {
                Uri uri = new Uri(fileName);
                BitmapSource bitmapImage = new BitmapImage(uri);
                return bitmapImage;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public BitmapSource ToMainColors(BitmapSource image)
        {
            try
            {
                _watch.Restart();

                int width = image.PixelWidth;
                int height = image.PixelHeight;
                WriteableBitmap writeableBitmap = new WriteableBitmap(image);
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

                    Color color = new Color();

                    if (red >= green && red >= blue)
                    {
                        color = ColorTranslator.FromHtml("#FF0000");
                    }
                    else if (green >= red && green >= blue)
                    {
                        color = ColorTranslator.FromHtml("#00FF00");
                    }
                    else
                    {
                        color = ColorTranslator.FromHtml("#0000FF");
                    }

                    pixels[j] = color.R;
                    pixels[j + 1] = color.G;
                    pixels[j + 2] = color.B;
                    j += 4;
                }

                Int32Rect rect = new Int32Rect(0, 0, width, height);

                writeableBitmap.WritePixels(rect, pixels, stride, 0);

                _watch.Stop();
                _secondsElapsedForChangeColors = _watch.Elapsed.TotalSeconds;

                writeableBitmap.Freeze();

                return writeableBitmap;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<BitmapSource> ToMainColorsAsync(BitmapSource image)
        {
            try
            {
                return await Task.Run(() => { return ToMainColors(image); });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SaveImage(BitmapSource image, string fileName, int filterIndex)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    BitmapEncoder encoder;

                    if (filterIndex == 0)
                    {
                        encoder = new JpegBitmapEncoder();
                    }
                    else if (filterIndex == 1)
                    {
                        encoder = new PngBitmapEncoder();
                    }
                    else
                    {
                        encoder = new BmpBitmapEncoder();
                    }

                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(fs);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
