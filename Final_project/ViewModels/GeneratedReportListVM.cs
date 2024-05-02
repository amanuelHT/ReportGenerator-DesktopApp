using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Service;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;

namespace Final_project.ViewModels
{
    public partial class GeneratedReportListVM : ObservableObject
    {
        private readonly FirebaseStore _firebaseStore;

        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();
        public string SelectedItem { get; set; }
        public string PdfFilePath { get; set; }


        public GeneratedReportListVM(FirebaseStore storageService, INavigationService navigationService)
        {
            _firebaseStore = storageService;

            Initialize();
        }

        private async void Initialize()
        {
            var reportTitles = await _firebaseStore.GetReportTitlesAsync();
            foreach (var title in reportTitles)
            {
                Items.Add(title);
            }
        }

        [RelayCommand]
        private async void Upload()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                string filepath = openFileDialog.FileName;
                string filename = Path.GetFileName(filepath);
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
                var stream = File.Open(filepath, FileMode.Open);

                await _firebaseStore.UploadReportAsync(stream, filename);
                await _firebaseStore.AddReportAsync(filenameWithoutExtension);

                Items.Clear();
                Initialize();
            }
        }

        [RelayCommand]
        private async void Delete()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this report?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                await _firebaseStore.DeleteReportAsync(PdfFilePath);
                await _firebaseStore.DeleteReportAsync(Path.GetFileNameWithoutExtension(PdfFilePath));

                Items.Clear();
                Initialize();


            }
        }


        [RelayCommand]
        private async void Download()
        {
            var downloadUrl = await _firebaseStore.GetDownloadUrlAsync(PdfFilePath);

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.FileName = Path.GetFileName(PdfFilePath);
            if (saveFileDialog.ShowDialog() == true)
            {
                string localFilePath = saveFileDialog.FileName;
                using (var webClient = new WebClient())
                {
                    await webClient.DownloadFileTaskAsync(new System.Uri(downloadUrl), localFilePath);
                }
                MessageBox.Show("File downloaded and saved successfully");
            }
        }



    }
}
