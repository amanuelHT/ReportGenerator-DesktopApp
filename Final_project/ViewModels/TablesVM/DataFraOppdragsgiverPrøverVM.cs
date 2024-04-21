using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels.TablesVM
{
    public partial class DataFraOppdragsgiverPrøverVM : ObservableObject
    {


        private readonly ReportStore _reportStore;
        private readonly ModalNavigation _modalNavigation;
        private Guid _reportModelId;

        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string description;

        public DataFraOppdragsgiverPrøverVM(ReportStore reportStore, ModalNavigation modalNavigation, Guid reportid, DataFraOppdragsgiverTableVM dataFraOppdragsgiverTableVM)
        {
            _reportStore = reportStore;
            _modalNavigation = modalNavigation;
            _reportModelId = reportid;
            SubmitCommand = new SubmitDataFraOppgavegiverCommand(this, reportStore, _reportModelId, dataFraOppdragsgiverTableVM);

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(Description))
                {
                    ((SubmitDataFraOppgavegiverCommand)SubmitCommand).RaiseCanExecuteChanged();
                }
            };
        }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }


    }
}
