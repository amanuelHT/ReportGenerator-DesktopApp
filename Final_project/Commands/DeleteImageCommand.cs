using Final_project.Stores;

namespace Final_project.Commands
{
    public class DeleteImageCommand : AsyncCommandBase
    {
        private readonly ReportStore _reportStore;

        public DeleteImageCommand(ReportStore reportStore)
        {
            _reportStore = reportStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {


            try
            {
                Guid imageId = (Guid)parameter;
                await _reportStore.DeleteImage(imageId);
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
