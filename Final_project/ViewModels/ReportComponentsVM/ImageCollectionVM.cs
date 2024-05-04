using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using Final_project.ViewModels.ReportComponentsVM;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels
{
    public partial class ImageCollectionVM : ObservableObject
    {
        private readonly ReportStore _reportStore;
        private readonly Guid _reportId;

        public ObservableCollection<ImageVM> Images { get; private set; }

        public event Action<ImageVM> ImageAdded;
        public event Action<ImageVM> ImageDeleted;

        public ImageCollectionVM(ReportStore reportStore, Guid reportId)
        {
            _reportStore = reportStore;
            _reportId = reportId;
            Images = new ObservableCollection<ImageVM>();
        }

        [RelayCommand]
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
                    var imageVM = new ImageVM(_reportId, this, _reportStore)
                    {
                        ImageId = imageId,
                        ImageName = name,
                        ImageUri = new Uri(filePath, UriKind.Absolute)
                    };
                    Images.Add(imageVM);
                    ImageAdded?.Invoke(imageVM);
                }
            }
        }

        [RelayCommand]
        private void RemoveImage(ImageVM imageVM)
        {
            if (imageVM != null && Images.Contains(imageVM))
            {
                Images.Remove(imageVM);
                ImageDeleted?.Invoke(imageVM);
            }
        }
    }
}
