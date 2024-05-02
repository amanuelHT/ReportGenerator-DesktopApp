using Firebase.Storage;
using Google.Cloud.Firestore;
using System.IO;

public class FirebaseStore
{
    private readonly FirebaseStorage _storage;
    private readonly FirestoreDb _db;

    public FirebaseStore()
    {
        // Set the path to the JSON credentials file
        string path = @"..\..\hprd-24-040-firebase-adminsdk-l7jhz-64ddf61372.json";

        // Set the environment variable for Google Application Credentials
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

        // Create a FirestoreDb instance with the specified project ID
        _db = FirestoreDb.Create("hprd-24-040");

        // Create a FirebaseStorage instance with the specified bucket name
        _storage = new FirebaseStorage("hprd-24-040.appspot.com");
    }




    public async Task UploadReportAsync(Stream stream, string filename)
    {

        await _storage
            .Child("reports")
            .Child(filename)
            .PutAsync(stream);
    }

    public async Task<string> GetDownloadUrlAsync(string filePath)
    {
        // Returns the download URL for the specified file path in Firebase Storage
        return await _storage
            .Child(filePath)
            .GetDownloadUrlAsync();
    }
    public async Task DeleteFileAsync(string filePath)
    {
        // Deletes the file specified by the file path from Firebase Storage
        await _storage
            .Child(filePath)
            .DeleteAsync();
    }



    public async Task AddReportAsync(string title)
    {
        // Adds a new report document to Firestore collection
        CollectionReference collectionRef = _db.Collection("reports");
        await collectionRef.AddAsync(new
        {
            title
        });
    }
    public async Task DeleteReportAsync(string title)
    {
        // Deletes the report document with the specified title from Firestore collection
        CollectionReference collectionRef = _db.Collection("reports");
        QuerySnapshot querySnapshot = await collectionRef.WhereEqualTo("title", title).GetSnapshotAsync();

        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
        {
            string documentId = documentSnapshot.Id;
            DocumentReference docRef = collectionRef.Document(documentId); // Construct the document reference
            await docRef.DeleteAsync();
        }
    }


    public async Task<IEnumerable<string>> GetReportTitlesAsync()
    {
        // Retrieves the titles of all reports from Firestore collection
        List<string> titles = new List<string>();
        QuerySnapshot querySnapshot = await _db.Collection("reports").GetSnapshotAsync();

        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
        {
            if (documentSnapshot.TryGetValue("title", out object title))
            {
                titles.Add(title.ToString());
            }
        }

        return titles;
    }
}
