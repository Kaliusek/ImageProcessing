using ImageProcessing.ViewModels.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageProcessing.ViewModels.ViewModels
{
    public sealed class ApplicationViewModel : INotifyPropertyChanged
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IApplicationController _controller;

        private ApplicationViewModel()
        {

        }

        static ApplicationViewModel()
        {

        }

        private static ApplicationViewModel _appViewModel;
        public static ApplicationViewModel AppViewModel
        {
            get
            {
                if (_appViewModel == null)
                {
                    _appViewModel = new ApplicationViewModel();
                }
                return _appViewModel;
            }
        }

        public void SetController(IApplicationController controller)
        {
            try
            {
                _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            }
            catch(ArgumentNullException ex)
            {
                log.Error("ApplicationController cannot be null");
                throw ex;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region properties
        private BitmapSource _imageSource;
        public BitmapSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ImageIsLoaded");
            }
        }

        public bool ImageIsLoaded
        {
            get
            {
                return ImageSource != null;
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        } 
        #endregion

        #region methods
        public void LoadImage(object obj)
        {
            try
            {
                _controller.LoadImage(this);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void ChangeColors(object obj)
        {
            try
            {
                _controller.ChangeColors(this);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void ChangeColorsAsync(object obj)
        {
            try
            {
                _controller.ChangeColorsAsync(this);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void SaveImage(object obj)
        {
            try
            {
                _controller.SaveImage(this);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        #endregion

        #region commands
        private RelayCommand<object> _loadImageCommand;
        public ICommand LoadImageCommand
        {
            get
            {
                if (_loadImageCommand == null)
                {
                    _loadImageCommand = new RelayCommand<object>(LoadImage);
                }
                return _loadImageCommand;
            }
        }

        private RelayCommand<object> _changeColorsCommand;
        public ICommand ChangeColorsCommand
        {
            get
            {
                if (_changeColorsCommand == null)
                {
                    _changeColorsCommand = new RelayCommand<object>(ChangeColors);
                }
                return _changeColorsCommand;
            }
        }

        private RelayCommand<object> _changeColorsAsyncCommand;
        public ICommand ChangeColorsAsyncCommand
        {
            get
            {
                if (_changeColorsAsyncCommand == null)
                {
                    _changeColorsAsyncCommand = new RelayCommand<object>(ChangeColorsAsync);
                }
                return _changeColorsAsyncCommand;
            }
        }

        private RelayCommand<object> _saveImageCommand;
        public ICommand SaveImageCommand
        {
            get
            {
                if (_saveImageCommand == null)
                {
                    _saveImageCommand = new RelayCommand<object>(SaveImage);
                }
                return _saveImageCommand;
            }
        }
        #endregion
    }
}
