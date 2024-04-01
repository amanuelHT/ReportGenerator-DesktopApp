using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ImageVM : ViewModelBase
    {
        private Uri _imageUri;
        private Guid _imageId; // Add image ID property

        public Uri ImageUri
        {
            get => _imageUri;
            set
            {
                _imageUri = value;
                OnPropertyChanged(nameof(ImageUri));
            }
        }

        public Guid ImageId // Image ID property
        {
            get => _imageId;
            set
            {
                _imageId = value;
                OnPropertyChanged(nameof(ImageId));
            }
        }

        public ICommand RemoveImageCommand { get; }

        public ImageVM(string imagePath, Guid imageId) // Modify constructor to accept image ID
        {
            ImageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            ImageId = imageId; // Assign image ID
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
