using Final_project.ViewModels; // Import your view model namespace here
using Firebase.Storage;
using System.IO;
using System.Net;
using System.Windows.Controls;

namespace Final_project.Views
{
    public partial class GeneratedReportsList : UserControl
    {

        public GeneratedReportsList()
        {
            InitializeComponent();

        }

        public void LoadPDF(string url)
        {
            WebClient client = new WebClient();
            byte[] myDataBuffer = client.DownloadData((new Uri(url)));

            MemoryStream storeStream = new MemoryStream();
            storeStream.SetLength(myDataBuffer.Length);
            storeStream.Write(myDataBuffer, 0, (int)storeStream.Length);
            pdfViewer.Load(storeStream);
            storeStream.Flush();
        }

        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem != null)
            {
                var _viewModel = DataContext as GeneratedReportListVM;


                string selectedItem = listBox.SelectedItem.ToString();
                string pdfFileName = selectedItem + ".pdf";

                _viewModel.PdfFilePath = "reports/" + pdfFileName;

                var task = new FirebaseStorage("hprd-24-040.appspot.com")
                    .Child(_viewModel.PdfFilePath)
                    .GetDownloadUrlAsync();

                string downloadUrl = await task;
                LoadPDF(downloadUrl);
            }
        }

    }
}
