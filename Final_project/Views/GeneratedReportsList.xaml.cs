using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Firebase.Storage;
using Google.Cloud.Firestore;
using Microsoft.Win32;
//using static Syncfusion.XlsIO.Implementation.Collections.CollectionBaseEx<T>;


namespace Final_project.Views
{
    /// <summary>
    /// Interaction logic for GeneratedReportsList.xaml
    /// </summary>
    public partial class GeneratedReportsList : UserControl
    {
        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();
        public string PdfFilePath { get; set; }
        public CollectionReference collectionRef { get; set; }
        public QuerySnapshot snapshot;

        public GeneratedReportsList()
        {
            InitializeComponent();
            ReportsList();
            DataContext = this;
        }
        private async void ReportsList()
        {
            string path = @"..\..\hprd-24-040-firebase-adminsdk-l7jhz-64ddf61372.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            FirestoreDb db = FirestoreDb.Create("hprd-24-040");

            collectionRef = db.Collection("reports");
            snapshot = await collectionRef.GetSnapshotAsync();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.TryGetValue("title", out object title))
                {
                    Items.Add(title.ToString());
                }
            }

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

                string selectedItem = listBox.SelectedItem.ToString();
                string pdfFileName = selectedItem + ".pdf";

                PdfFilePath = "reports/" + pdfFileName;
                MessageBox.Show(PdfFilePath);
                var task = new FirebaseStorage("hprd-24-040.appspot.com")
                    .Child(PdfFilePath)
                    .GetDownloadUrlAsync();

                string downloadUrl = await task;
                LoadPDF(downloadUrl);
            }
        }

        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                string filepath = openFileDialog.FileName;
                string filename = System.IO.Path.GetFileName(filepath);
                string filenameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(filepath);
                MessageBox.Show(filepath);

                var stream = File.Open(filepath, FileMode.Open);

                var task = new FirebaseStorage("hprd-24-040.appspot.com")
                    .Child("reports")
                    .Child(filename)
                    .PutAsync(stream);


                string path = @"..\..\hprd-24-040-firebase-adminsdk-l7jhz-64ddf61372.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
                FirestoreDb db = FirestoreDb.Create("hprd-24-040");
                collectionRef = db.Collection("reports");
                Google.Cloud.Firestore.DocumentReference document = await collectionRef.AddAsync(new
                {
                    title = filenameWithoutExtension,
                });

                await System.Threading.Tasks.Task.Delay(1000);

                Items.Clear();
                ReportsList();

            }
        }

        private async void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this report?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var storage = new FirebaseStorage("hprd-24-040.appspot.com");

                await storage.Child(PdfFilePath).DeleteAsync();

                FirestoreDb db = FirestoreDb.Create("hprd-24-040");

                CollectionReference collectionRef = db.Collection("reports");

                string ReportTitle = System.IO.Path.GetFileNameWithoutExtension(PdfFilePath);
                MessageBox.Show(ReportTitle);
                QuerySnapshot querySnapshot = await collectionRef.WhereEqualTo("title", ReportTitle).GetSnapshotAsync();

                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    string documentId = documentSnapshot.Id;

                    await collectionRef.Document(documentId).DeleteAsync();
                }

                Items.Clear();
                ReportsList();
            }
        }

        private async void DownloadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var storage = new FirebaseStorage("hprd-24-040.appspot.com");

            var fileReference = storage.Child(PdfFilePath);

            string downloadUrl = await fileReference.GetDownloadUrlAsync();

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.FileName = System.IO.Path.GetFileName(PdfFilePath);
            if (saveFileDialog.ShowDialog() == true)
            {
                string localFilePath = saveFileDialog.FileName;

                // Download the file content from the download URL
                using (var webClient = new WebClient())
                {
                    await webClient.DownloadFileTaskAsync(new System.Uri(downloadUrl), localFilePath);
                }

                Console.WriteLine("File downloaded and saved successfully");
            }
        }
    }
}