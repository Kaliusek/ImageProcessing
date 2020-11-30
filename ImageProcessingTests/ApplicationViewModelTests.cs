using ImageProcessing.ViewModels.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Media.Imaging;

namespace ImageProcessingTests
{
    [TestClass]
    public class ApplicationViewModelTests
    {
        [TestMethod]
        public void ApplicationViewModelShouldNotBeNull()
        {
            Assert.IsNotNull(ApplicationViewModel.AppViewModel);
        }

        [TestMethod]
        public void ApplicationShouldHaveOneInstanceOfApplicationViewModel()
        {
            var viewModelA = ApplicationViewModel.AppViewModel;
            var viewModelB = ApplicationViewModel.AppViewModel;

            Assert.AreSame(viewModelA, viewModelB);
        }

        [TestMethod]
        public void SetControllerShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ApplicationViewModel.AppViewModel.SetController(null));
        }

        [TestMethod]
        public void ImageShouldNotBeLoaded()
        {
            Assert.IsFalse(ApplicationViewModel.AppViewModel.ImageIsLoaded);
        }

        [TestMethod]
        public void ImageShouldBeLoaded()
        {
            ApplicationViewModel.AppViewModel.ImageSource = new BitmapImage();
            Assert.IsTrue(ApplicationViewModel.AppViewModel.ImageIsLoaded);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ApplicationViewModel.AppViewModel.ImageSource = null;
        }
    }
}
