using Final_project.Stores;

namespace Final_project.Commands
{
    public class LoadReportCommand : AsyncCommandBase
    {

        private readonly ReportStore _store;



        public LoadReportCommand(ReportStore reportStore)
        {
            _store = reportStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.Load();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
