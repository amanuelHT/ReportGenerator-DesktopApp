using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ImageVM : ViewModelBase
    {
        private readonly ImageCollectionVM _imageCollectionVM;
        private readonly ReportStore _reportStore;
        public ReportImageModel ReportImageModel { get; private set; }
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

            // Use the new RemoveImageCommand
            RemoveImageCommand = new DeleteImageCommand(reportStore, this, _imageCollectionVM);

        }

        private void ReportStore_ImageDeleted(Guid id)
        {
            ImageVM imageToRemove = _imageCollectionVM.Images.FirstOrDefault(image => image.ImageId == id);

            if (imageToRemove != null)
            {
                _imageCollectionVM.RemoveImage(imageToRemove);
            }
        }

        public override void Dispose()
        {
            _reportStore.ImageDeleted -= ReportStore_ImageDeleted;
            base.Dispose();
        }
    }
}
