using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ImageVM : ViewModelBase
    {
        private Uri _imageUri;

        public Uri ImageUri
        {
            get => _imageUri;
            set
            {
                _imageUri = value;
                OnPropertyChanged(nameof(ImageUri));
            }
        }

        public ICommand RemoveImageCommand { get; }

        public ImageVM(string imagePath)
        {
            ImageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            RemoveImageCommand = new RelayCommand(RemoveImage);
        }

        private void RemoveImage()
        {
            // Invoke the RequestRemoval action if it's not null
            RequestRemoval?.Invoke(this);
        }

        // Action for requesting removal
        public Action<ImageVM> RequestRemoval;
    }
}
