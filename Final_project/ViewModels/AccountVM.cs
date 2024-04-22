using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class AccountVM : ObservableObject, IDisposable
    {
        private readonly AccountStore _accountStore;

        public string Email => _accountStore.CurrentAccount?.Email;
        public string Username => _accountStore.CurrentAccount?.Username;

        public ICommand NavigateSettingsCommand { get; }

        public AccountVM(AccountStore accountStore, INavigationService settingsNavigationService)
        {
            _accountStore = accountStore;
            NavigateSettingsCommand = new NavigateCommand(settingsNavigationService);

            _accountStore.CurrentAccountChanged += _accountStore_CurrentAccountChanged;
        }

        private void _accountStore_CurrentAccountChanged()
        {
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Email));
        }

        public void Dispose()
        {
            _accountStore.CurrentAccountChanged -= _accountStore_CurrentAccountChanged;
        }
    }
}
