using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Stores;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels
{
    public class ReportDetailsVM : ObservableObject, IDisposable
    {
        private readonly SelectedReportStore _selectedReportStore;
        private ReportModel SelectedReport => _selectedReportStore.SelectedReport;

        public bool HasReportSelected => SelectedReport != null;
        public string Tittle => SelectedReport?.Tittle ?? "Unknown";
        public string Status => SelectedReport?.Status == true ? "Godkjent" : "Ikke Godkjent";
        public string Kunde => SelectedReport?.Kunde ?? "No name";

        // Collection of image URLs
        public ObservableCollection<string> ImageUrls { get; private set; }

        public ReportDetailsVM(SelectedReportStore selectedReportStore)
        {
            _selectedReportStore = selectedReportStore ?? throw new ArgumentNullException(nameof(selectedReportStore));
            _selectedReportStore.SelectedReportChanged += _selectedReportStore_SelectedReportChanged;

            // Check if SelectedReport is not null before loading images
            if (_selectedReportStore.SelectedReport != null)
            {
                LoadImages(_selectedReportStore.SelectedReport.Images);
            }
        }

        private void LoadImages(IEnumerable<ReportImageModel> images)
        {
            // Initialize the ImageUrls collection if it's null
            if (ImageUrls == null)
            {
                ImageUrls = new ObservableCollection<string>();
            }
            else
            {
                // Clear the existing ImageUrls before loading new images
                ImageUrls.Clear();
            }

            // Check if the images collection is null
            if (images == null) return;

            // Add image URLs to the ImageUrls collection
            foreach (var image in images)
            {
                ImageUrls.Add(image.ImageUrl);
            }
        }

        public void Dispose()
        {
            _selectedReportStore.SelectedReportChanged -= _selectedReportStore_SelectedReportChanged;

        }
        private void _selectedReportStore_SelectedReportChanged()
        {
            OnPropertyChanged(nameof(HasReportSelected));
            OnPropertyChanged(nameof(Tittle));
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(Kunde));


        }
    }
}