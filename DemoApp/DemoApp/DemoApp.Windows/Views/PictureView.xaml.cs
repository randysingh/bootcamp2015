using System;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using DemoApp.Common.ViewModels;

namespace DemoApp.Windows.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PictureView : Page
    {
        public PictureView()
        {
            this.InitializeComponent();
            if (ViewModel != null)
            {
                ViewModel.TakeSelfieEvent += TakePicture;
            }
            else
            {
                DataContextChanged += OnDataContextChanged;
            }
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (ViewModel != null && ViewModel.TakeSelfieEvent == null)
            {
                ViewModel.TakeSelfieEvent += TakePicture;
            }
        }

        public PictureViewModel ViewModel
        {
            get { return DataContext as PictureViewModel; }
        }

        private async void TakePicture(object sender, EventArgs eventArgs)
        {
            var camera = new CameraCaptureUI();
            camera.PhotoSettings.AllowCropping = true;
            camera.PhotoSettings.CroppedAspectRatio = new Size(1,1);
            var file = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (file != null)
            {
                (ViewModel.SavePhoto).Execute(file);
                
            }
        }
    }
}
