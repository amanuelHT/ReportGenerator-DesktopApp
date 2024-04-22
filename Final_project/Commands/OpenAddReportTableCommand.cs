


using Final_project.Stores;
using Final_project.ViewModels.TablesVM;

namespace Final_project.Commands
{
    public class OpenAddReportTableCommand : CommandBase

    {

        private readonly ModalNavigation _modalNavigation;
        private readonly ReportStore _reportStore;
        private readonly DataFraOppdragsgiverTableVM _dataFraOppdragsgiverTableVM;
        private readonly Guid _reportid;

        public OpenAddReportTableCommand(ModalNavigation modalNavigation, ReportStore reportStore, DataFraOppdragsgiverTableVM dataFraOppdragsgiverTableVM, Guid reportid)
        {
            _modalNavigation = modalNavigation;
            _reportStore = reportStore;
            _dataFraOppdragsgiverTableVM = dataFraOppdragsgiverTableVM;
            _reportid = reportid;
        }


        public override void Execute(object parameter)
        {
            DataFraOppdragsgiverPrøverVM dataFraOppdragsgiverPrøverVM = new DataFraOppdragsgiverPrøverVM(_reportStore, _modalNavigation, _reportid, _dataFraOppdragsgiverTableVM);
            _modalNavigation.CurrentView = dataFraOppdragsgiverPrøverVM;



        }

    }
}
