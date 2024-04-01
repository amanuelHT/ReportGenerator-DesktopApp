using Report_Generator_Domain.Models;

namespace Final_project.Stores
{
    public class AccountStore
    {
        private Account _currentAccount;
        public Account CurrentAccount
        {
            get => _currentAccount;
            set
            {
                if (_currentAccount != value)
                {
                    _currentAccount = value;
                    CurrentAccountChanged?.Invoke();
                }
            }
        }


    public bool IsLoggedIn => CurrentAccount != null;

        public event Action CurrentAccountChanged;

        public void Logout()
        {
            CurrentAccount = null;
        }
       
    }
}