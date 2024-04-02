using Final_project.Commands;
using Final_project.Stores;
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

        public Guid ImageId { get; private set; }
        public string ImageName { get; private set; }
        public ICommand RemoveImageCommand { get; private set; }

        public ImageVM(Guid imageId, string name, string imageUri, ReportStore reportStore)
        {
            ImageId = imageId;
            ImageName = name;
            ImageUri = new Uri(imageUri, UriKind.RelativeOrAbsolute);
            RemoveImageCommand = new DeleteImageCommand(reportStore, imageId);
        }
    }
}
