using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ImageCollectionVM : ViewModelBase
    {
        public ObservableCollection<ImageVM> Images { get; private set; }
        public ICommand UploadImageCommand { get; }

        public ImageCollectionVM()
        {
            Images = new ObservableCollection<ImageVM>();
            UploadImageCommand = new RelayCommand(UploadImage);
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
                    var imageVM = new ImageVM(filePath);
                    // Subscribe to the RequestRemoval action
                    imageVM.RequestRemoval = RemoveImage;
                    Images.Add(imageVM);
                }
            }
        }




        private void RemoveImage(ImageVM imageVM)
        {
            if (Images.Contains(imageVM))
            {
                Images.Remove(imageVM);
            }
        }
    }
}
