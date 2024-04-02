using CommunityToolkit.Mvvm.Input;
using Final_project.Stores;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ImageCollectionVM : ViewModelBase
    {
        private readonly ReportStore _reportStore;

        public ObservableCollection<ImageVM> Images { get; private set; }
        public ICommand UploadImageCommand { get; private set; }

        public event Action<ImageVM> ImageAdded;

        public ImageCollectionVM(ReportStore reportStore)
        {
            Images = new ObservableCollection<ImageVM>();
            UploadImageCommand = new RelayCommand(UploadImage);
            reportStore = reportStore;
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
                    Guid imageId = Guid.NewGuid();
                    string name = System.IO.Path.GetFileNameWithoutExtension(filePath);

                    var imageVM = new ImageVM(imageId, name, filePath, _reportStore);

                    Images.Add(imageVM);
                    ImageAdded?.Invoke(imageVM);
                }
            }
        }


    }
}
