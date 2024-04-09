using Final_project.ViewModels;
using Microsoft.Win32;

namespace Final_project.Commands
{
    public class AddImageCommand : CommandBase
    {
        private readonly ImageCollectionVM _imageCollectionVM;

        public AddImageCommand(ImageCollectionVM imageCollectionVM)
        {
            _imageCollectionVM = imageCollectionVM;
        }

        public override void Execute(object parameter)
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

                    var imageVM = new ImageVM(imageId, name, filePath, _imageCollectionVM, null); // Assuming null is okay for reportStore

                    // Use the AddImage method in ImageCollectionVM
                    _imageCollectionVM.AddImage(imageVM);
                }
            }
        }
    }
}
