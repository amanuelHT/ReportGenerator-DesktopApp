using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class AccountVM : ViewModelBase
    {
        private readonly AccountStore _accountStore;

        public string Email => _accountStore.CurrentAccount?.Email;
        public string Username => _accountStore.CurrentAccount?.Username;



        public ICommand NavigateSettingsCommand { get; }

        //~AccountVM()
        //{
        //    // we use dispoe to clean so that we don't have memory leack

        //}


        public AccountVM(AccountStore accountStore, INavigationService SsettingsNavigationService)
        {
            _accountStore = accountStore;
            NavigateSettingsCommand = new NavigateCommand(SsettingsNavigationService);

            _accountStore.CurrentAccountChanged += _accountStore_CurrentAccountChanged;

        }

        private void _accountStore_CurrentAccountChanged()
        {
            OnPropertyChanged(nameof(Username));

            OnPropertyChanged(nameof(Email));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= _accountStore_CurrentAccountChanged;
            base.Dispose();
        }
    }
}
