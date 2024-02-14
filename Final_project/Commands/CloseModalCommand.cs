﻿using Final_project.Stores;

namespace Final_project.Commands
{
    public class CloseModalCommand : CommandBase
    {

        private readonly NavigationStore _navigationStore;
        public CloseModalCommand(NavigationStore navigationStore)
        {

            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.Close();
        }
    }
}
