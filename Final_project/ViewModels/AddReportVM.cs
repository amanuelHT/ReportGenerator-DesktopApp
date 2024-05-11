using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Stores;
using Final_project.Views;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class AddReportVM : ObservableObject

    {
        public ReportFormVM ReportFormVM { get; }

        public AddReportVM(
            ModalWindow modalWindow,
            ModalNavigation modalNavigation,
            ReportStore reportStore,
            NavigationStore navigationStore)
        {

            ICommand submitCommand = new AddReportCommand(
                modalNavigation,
                this,
                reportStore,
                navigationStore);

            ICommand cancelCommand = new CloseModalCommand(navigationStore);

            ReportFormVM = new ReportFormVM(
                submitCommand,
                cancelCommand,
                reportStore,
                modalNavigation,
                Guid.Empty);

        }
    }
}
