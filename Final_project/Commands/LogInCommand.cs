using Final_project.Service;
using Final_project.Stores;
using Final_project.ViewModels;
using Report_Generator_Domain.Models;

namespace Final_project.Commands
{

    public class LogInCommand : CommandBase
    {



        private readonly LoginVM _login;
        private readonly AccountStore _accountStore;

        public INavigationService _navigationService { get; }



        public LogInCommand(LoginVM login, AccountStore accountStore, INavigationService navigationService)
        {
            _login = login;
            _accountStore = accountStore;
            _navigationService = navigationService;
        }


        public override void Execute(object parameter)
        {

            Account account = new Account()
            {
                Email = $"{_login.Username}@gmail.com",
                Username = _login.Username
            };


            _accountStore.CurrentAccount = account;
            _navigationService.Navigate();

        }
    }
}
