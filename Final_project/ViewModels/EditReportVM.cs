using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class EditReportVM : ViewModelBase
    {
        public Guid ReportId { get; set; }
        public ReportFormVM ReportFormVM { get; }

        public EditReportVM(ReportModel reportModel, ReportStore reportStore, ModalNavigation navigationStore)
        {
            if (reportModel == null)
            {
                throw new ArgumentNullException(nameof(reportModel));
            }

            ReportId = reportModel.Id;

            ICommand submitCommand = new EditReportCommand(this, reportStore, navigationStore);
            ICommand cancelCommand = new CloseModalCommand(navigationStore);

            ReportFormVM = new ReportFormVM(submitCommand, cancelCommand)
            {
                Tittle = reportModel.Tittle,
                Status = reportModel.Status,
                Kunde = reportModel.Kunde
            };

            LoadImages(reportModel.Images);
        }

        private void LoadImages(IEnumerable<ReportImageModel> images)
        {
            if (images == null) return;

            foreach (var image in images)
            {
                var imageVM = new ImageVM(image.ImageUrl);

                imageVM.RequestRemoval = ReportFormVM.RemoveImage;
                ReportFormVM.ImageCollectionViewModel.Images.Add(imageVM);
            }
        }


    }
}
