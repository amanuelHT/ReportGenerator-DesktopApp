using CommunityToolkit.Mvvm.Input;
using Final_project.Stores;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ImageCollectionVM : ViewModelBase
    {
        private readonly ReportStore _reportStore;

        public ObservableCollection<ImageVM> Images { get; private set; }
        public ICommand UploadImageCommand { get; }

        public ImageCollectionVM(ReportStore reportStore)
        {
            Images = new ObservableCollection<ImageVM>();
            UploadImageCommand = new RelayCommand(UploadImage);
            _reportStore = reportStore;


            _reportStore.ImageDeleted += ReportStore_ImageDeleted;
        }

        // In ImageCollectionVM
        private void ReportStore_ImageDeleted(Guid id)
        {
            var imageToRemove = Images.FirstOrDefault(image => image.ImageId == id);
            if (imageToRemove != null)
            {
                Application.Current.Dispatcher.Invoke(() => Images.Remove(imageToRemove));
            }
        }

        public override void Dispose()
        {
            _reportStore.ImageDeleted -= ReportStore_ImageDeleted;
            base.Dispose();
        }


        private void UploadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    // Generate a unique image ID
                    Guid imageId = Guid.NewGuid();

                    var imageVM = new ImageVM(filePath, imageId, _reportStore);

                    Images.Add(imageVM);
                }
            }
        }
    }
}
