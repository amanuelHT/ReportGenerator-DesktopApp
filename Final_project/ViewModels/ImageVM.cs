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

        // Constructor used when creating an instance with an image path
        public ImageVM(string imagePath)
        {
            ImageUri = new Uri(imagePath, UriKind.Absolute);
        }
    }
}
