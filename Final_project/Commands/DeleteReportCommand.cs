using Final_project.Stores;
using System.Windows;

namespace Final_project.Commands
{
    public class DeleteReportCommand : AsyncCommandBase
    {
        private Guid _reportid;
        private readonly ReportStore _reportStore;

        public DeleteReportCommand(Guid reportid, ReportStore reportStore)
        {
            _reportid = reportid;
            _reportStore = reportStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {



            var confirmDelete = MessageBox.Show("Are you sure you want to delete this report?", 
                "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmDelete != MessageBoxResult.Yes)
            {
                return;
            }

            // Execute the deletion
            try
            {
                await _reportStore.Delete(_reportid);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to delete report: " + ex.Message);
            }

        }
    }

}
