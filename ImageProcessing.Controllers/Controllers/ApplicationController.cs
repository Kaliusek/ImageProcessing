using ImageProcessing.ViewModels.Interfaces;
using ImageProcessing.ViewModels.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows.Media.Imaging;

namespace ImageProcessing.Controllers.Controllers
{
    public class ApplicationController : IApplicationController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void ChangeColors(ApplicationViewModel vm)
        {
            try
            {
                if (vm.ImageIsLoaded)
                {
                    ImageProcessingLibrary.ImageProcessing processing = new ImageProcessingLibrary.ImageProcessing();
                    BitmapSource image = processing.ToMainColors(vm.ImageSource);
                    vm.ImageSource = image;

                    vm.Message = $"Success! It took {processing.SecondsElapsedForChangeColors} seconds to change to the main colors";
                }
            }
            catch (Exception ex)
            {
                vm.Message = "An unexpected error has occurred";
                log.Error(ex);
                throw;
            }
        }

        public async void ChangeColorsAsync(ApplicationViewModel vm)
        {
            try
            {
                if (vm.ImageIsLoaded)
                {
                    vm.ImageSource.Freeze();
                    ImageProcessingLibrary.ImageProcessing processing = new ImageProcessingLibrary.ImageProcessing();
                    BitmapSource image = await processing.ToMainColorsAsync(vm.ImageSource);
                    vm.ImageSource = image;

                    vm.Message = $"Success! It took {processing.SecondsElapsedForChangeColors} seconds to change to the main colors asynchronously";
                }
            }
            catch (Exception ex)
            {
                vm.Message = "An unexpected error has occurred";
                log.Error(ex);
                throw;
            }
        }

        public void LoadImage(ApplicationViewModel vm)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG|*.jpg|PNG|*.png|BMP|*.bmp";
                bool? result = openFileDialog.ShowDialog();

                if (result.GetValueOrDefault())
                {
                    ImageProcessingLibrary.ImageProcessing processing = new ImageProcessingLibrary.ImageProcessing();
                    vm.ImageSource = processing.LoadImage(openFileDialog.FileName);
                    vm.Message = "Loaded";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                vm.Message = "An unexpected error has occurred";
                throw;
            }
        }

        public void SaveImage(ApplicationViewModel vm)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPG|*.jpg|PNG|*.png|BMP|*.bmp";
                bool? result = saveFileDialog.ShowDialog();

                if (result.GetValueOrDefault())
                {
                    ImageProcessingLibrary.ImageProcessing processing = new ImageProcessingLibrary.ImageProcessing();
                    processing.SaveImage(vm.ImageSource, saveFileDialog.FileName, saveFileDialog.FilterIndex);
                    vm.Message = "Saved";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                vm.Message = "An unexpected error has occurred";
                throw;
            }
        }
    }
}
