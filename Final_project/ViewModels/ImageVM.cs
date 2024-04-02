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

        public Guid ImageId { get; set; }


        public ICommand RemoveImageCommand { get; }

        public ImageVM(string imagePath, Guid imageId, ReportStore reportStore) // Modify constructor to accept image ID
        {
            ImageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            ImageId = imageId;
            RemoveImageCommand = new DeleteImageCommand(reportStore, imageId);


        }




    }
}
