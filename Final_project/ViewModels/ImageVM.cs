using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ImageVM : ViewModelBase
    {


        private readonly ImageCollectionVM _imageCollectionVM;
        private readonly ReportStore _reportStore;

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

        public ImageVM(Guid imageId, string name, string imageUri, ImageCollectionVM imageCollectionVM, ReportStore reportStore)
        {
            ImageId = imageId;
            ImageName = name;
            ImageUri = new Uri(imageUri, UriKind.RelativeOrAbsolute);

            _imageCollectionVM = imageCollectionVM;
            _reportStore = reportStore;

            RemoveImageCommand = new DeleteImageCommand(_reportStore, ImageId);

            reportStore.ImageDeleted += ReportStore_ImageDeleted;
        }

        private void ReportStore_ImageDeleted(Guid imageId)
        {
            if (ImageId == imageId)
            {
                _imageCollectionVM.RemoveImage(ImageId);
            }



        }

        public override void Dispose()
        {
            _reportStore.ImageDeleted -= ReportStore_ImageDeleted;
            base.Dispose();
        }

    }
}
