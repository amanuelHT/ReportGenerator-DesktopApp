using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Service;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class SettingsVM : ObservableObject
    {
        public ICommand NavigateLogin { get; }

        public SettingsVM(INavigationService loginNavigationService)
        {

            NavigateLogin = new NavigateCommand(loginNavigationService);

        }
    }
}
