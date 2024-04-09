using Final_project.Commands;
using Final_project.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ImageCollectionVM : ViewModelBase
    {
        public ObservableCollection<ImageVM> Images { get; private set; }
        public ImageVM _imageVM { get; private set; }
        public ICommand UploadImageCommand { get; private set; }

        public event Action<ImageVM> ImageAdded;
        public event Action<ImageVM> ImageDeleted;
        public ImageCollectionVM(ReportStore reportStore, Guid reportId)
        {
            Images = new ObservableCollection<ImageVM>();


            // Initialize the UploadImageCommand with the AddImageCommand
            UploadImageCommand = new AddImageCommand(this);
        }

        public void RemoveImage(ImageVM image)
        {
            if (Images.Contains(image))
            {
                Images.Remove(image);
                ImageDeleted?.Invoke(image); // Raise the ImageDeleted event
            }
        }

        public void AddImage(ImageVM image)
        {
            Images.Add(image);
            ImageAdded?.Invoke(image);
        }
    }
}
