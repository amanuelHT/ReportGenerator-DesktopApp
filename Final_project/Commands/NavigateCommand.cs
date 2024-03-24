using Final_project.Service;

namespace Final_project.Commands
{

    public class NavigateCommand : CommandBase

    {
        private readonly INavigationService _navigationService;


        NavigationStore _navigationStore;

        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            // call back that give us the model we want , when we call this function createViewModel()
            // it will return the <TviewModel> and will set that as the currnet viwemodel for the applicatoin

            //navigationStore.CurrentViewModel = _createViewModel();before we have thi
            //but after we make the navigationservice we can simply use for all of them 

            _navigationService.Navigate();
        }
    }
}
