using System.Windows; // Necessary for using MessageBox

namespace Final_project.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        public override async void Execute(object parameter)
        {
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception ex) // Catch the exception to handle it
            {
                // Log the exception or handle it as needed
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
