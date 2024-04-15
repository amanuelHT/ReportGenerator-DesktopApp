using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class EditReportVM : ObservableObject
    {
        private readonly ReportStore _reportStore;

        public Guid ReportId { get; private set; }
        public ReportFormVM ReportFormVM { get; private set; }

        public EditReportVM(HomeVM homeVM, ReportModel reportModel, ReportStore reportStore, NavigationStore navigationStore)
        {
            _reportStore = reportStore ?? throw new ArgumentNullException(nameof(reportStore));
            if (reportModel == null)
            {
                throw new ArgumentNullException(nameof(reportModel));
            }

            ReportId = reportModel.Id;

            ICommand submitCommand = new EditReportCommand(this, reportStore, navigationStore);
            ICommand cancelCommand = new CloseModalCommand(homeVM, navigationStore);

            // Pass ReportId to ReportFormVM
            ReportFormVM = new ReportFormVM(submitCommand, cancelCommand, reportStore, ReportId)
            {
                Tittle = reportModel.Tittle,
                Status = reportModel.Status,
                Kunde = reportModel.Kunde,
                // Initialize ImageCollectionViewModel with existing images
                ImageCollectionViewModel = new ImageCollectionVM(reportStore, ReportId)
            };

            // Convert ReportImageModel to ImageVM and add to ImageCollectionViewModel
            foreach (var img in reportModel.Images)
            {
                var imageVM = new ImageVM(img.Id, img.Name, img.ImageUrl, ReportFormVM.ImageCollectionViewModel, reportStore);
                ReportFormVM.ImageCollectionViewModel.Images.Add(imageVM);
            }
        }
    }
}
