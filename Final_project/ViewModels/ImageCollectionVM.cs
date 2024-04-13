using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels
{
    public partial class ImageCollectionVM : ObservableObject
    {
        public ObservableCollection<ImageVM> Images { get; private set; }
        public ImageVM _imageVM { get; private set; }


        public event Action<ImageVM> ImageAdded;
        public event Action<ImageVM> ImageDeleted;
        public ImageCollectionVM(ReportStore reportStore, Guid reportId)
        {
            Images = new ObservableCollection<ImageVM>();

        }

        public void RemoveImage(ImageVM image)
        {
            if (Images.Contains(image))
            {
                Images.Remove(image);
                ImageDeleted?.Invoke(image); // Raise the ImageDeleted event
            }
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
                    var imageVM = new ImageVM(imageId, name, filePath, this, null); // Assuming null is okay for reportStore
                    Images.Add(imageVM);
                    ImageAdded?.Invoke(imageVM);
                }
            }
        }
    }
}
