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


        private readonly Guid _reportId;

        public ObservableCollection<ImageVM> Images { get; private set; }
        public ICommand UploadImageCommand { get; private set; }

        public event Action<ImageVM> ImageAdded;

        public ImageCollectionVM(ReportStore reportStore, Guid reportId)
        {
            _reportStore = reportStore ?? throw new ArgumentNullException(nameof(reportStore));
            _reportId = reportId;
            UploadImageCommand = new RelayCommand(UploadImage);
            Images = new ObservableCollection<ImageVM>();

            LoadImagesFromReports();
        }

        private void LoadImagesFromReports()
        {
            var reportImages = _reportStore.ReportModels
                .FirstOrDefault(rm => rm.Id == _reportId)?.Images;

            if (reportImages != null)
            {
                foreach (var reportImageModel in reportImages)
                {
                    var imageVM = new ImageVM(
                        reportImageModel.Id,
                        reportImageModel.Name,
                        reportImageModel.ImageUrl,
                        this, _reportStore);

                    Images.Add(imageVM);
                }
            }
        }

        public void RemoveImage(Guid imageId)
        {
            var imageToRemove = Images.FirstOrDefault(i => i.ImageId == imageId);
            if (imageToRemove != null)
            {
                Images.Remove(imageToRemove);
            }
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

                    // Create a new ImageVM instance
                    var imageVM = new ImageVM(imageId, name, filePath, this, _reportStore);

                    Images.Add(imageVM);
                    ImageAdded?.Invoke(imageVM);



                }
            }
        }



    }
}
