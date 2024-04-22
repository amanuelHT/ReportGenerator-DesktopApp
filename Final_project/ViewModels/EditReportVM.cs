using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using Final_project.ViewModels.TablesVM;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class EditReportVM : ObservableObject
    {
        private readonly ReportStore _reportStore;

        public Guid ReportId { get; private set; }
        public ReportFormVM ReportFormVM { get; private set; }

        public EditReportVM(ReportModel reportModel, ReportStore reportStore, ModalNavigation modalNavigation, NavigationStore navigationStore)
        {
            _reportStore = reportStore;
            if (reportModel == null)
            {
                throw new ArgumentNullException(nameof(reportModel));
            }

            ReportId = reportModel.Id;

            ICommand submitCommand = new EditReportCommand(this, reportStore, navigationStore);
            ICommand cancelCommand = new CloseModalCommand(navigationStore);

            // Pass ReportId to ReportFormVM
            ReportFormVM = new ReportFormVM(submitCommand, cancelCommand, reportStore, modalNavigation, ReportId)
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



            // Assuming reportModel.DataFraOppdragsgiverPrøver is a collection of DataFraOppdragsgiverPrøverModel
            foreach (var prøve in reportModel.DataFraOppdragsgiverPrøver)
            {
                var prøveVM = new DataFraOppdragsgiverPrøverVM(prøve);
                ReportFormVM.DataFraOppdragsgiverTableVM.Prøver.Add(prøveVM);
            }


            foreach (var prøve in reportModel.DataEtterKuttingOgSlipingModel)
            {
                var prøveVM = new DataEtterKuttingOgSlipingPrøveVM(prøve);
                ReportFormVM.DataEtterKuttingOgSlipingTableVM.Prøver.Add(prøveVM);
            }
        }
    }
}
