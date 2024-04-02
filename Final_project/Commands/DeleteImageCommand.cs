using Final_project.Stores;

namespace Final_project.Commands
{
    public class DeleteImageCommand : AsyncCommandBase
    {
        private readonly ReportStore _reportStore;
        private readonly Guid _imageId;

        public DeleteImageCommand(ReportStore reportStore, Guid id)
        {
            _reportStore = reportStore;
            _imageId = id;
        }

        public override async Task ExecuteAsync(object parameter)
        {


            try
            {

                await _reportStore.DeleteImage(_imageId);
            }
            catch (Exception)
            {
                // Handle exception or set error message if needed
            }
            finally
            {
                // Cleanup or update UI if needed
            }
        }
    }
}
