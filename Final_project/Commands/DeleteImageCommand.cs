using Final_project.Stores;
using Final_project.ViewModels;

namespace Final_project.Commands
{
    public class DeleteImageCommand : AsyncCommandBase
    {
        private readonly ReportStore _reportStore;
        private readonly ImageVM _imageVM;
        private readonly ImageCollectionVM _imageCollectionVM;

        public DeleteImageCommand(ReportStore reportStore, ImageVM imageVM, ImageCollectionVM imageCollectionVM)
        {
            _reportStore = reportStore;
            _imageVM = imageVM;
            _imageCollectionVM = imageCollectionVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                // Delete the image from the report store
                await _reportStore.DeleteImage(_imageVM.ImageId);

                // Remove the image from the UI collection
                _imageCollectionVM.RemoveImage(_imageVM);
            }
            catch (Exception)
            {
                // Handle exception or set error message if needed
            }
        }
    }
}
