using ImageProcessing.ViewModels.ViewModels;

namespace ImageProcessing.ViewModels.Interfaces
{
    public interface IApplicationController
    {
        void LoadImage(ApplicationViewModel vm);
        void ChangeColors(ApplicationViewModel vm);
        void ChangeColorsAsync(ApplicationViewModel vm);
        void SaveImage(ApplicationViewModel vm);
    }
}
