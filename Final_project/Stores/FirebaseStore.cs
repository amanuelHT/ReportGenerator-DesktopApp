using Final_project.Stores;
using Firebase.Storage;
using Google.Cloud.Firestore;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
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


    public async Task SendMessageAsync(MessageModel messageModel)
    {
        try
        {
            // Create a reference to the messages collection in Firestore
            CollectionReference collectionRef = _db.Collection("Messages");

            // Create a new document with the message details
            DocumentReference docRef = await collectionRef.AddAsync(new
            {
                Content = messageModel.Content,
                Sender = messageModel.Sender,
                Receiver = messageModel.Receiver,
                Filepath = messageModel.Filepath,
                Timestamp = DateTime.UtcNow // Optionally, include a timestamp
            });

            // Optionally, you can retrieve the ID of the newly created document
            string messageId = docRef.Id;

            // Handle the response if needed
            Console.WriteLine("Successfully sent message with ID: " + messageId);
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            Console.WriteLine("Error sending message: " + ex.Message);
        }
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

    public async Task<ObservableCollection<UserInfo>> LoadUsersAsync()
    {
        // Create an empty ObservableCollection to store user information
        ObservableCollection<UserInfo> usersCollection = new ObservableCollection<UserInfo>();

        // Reference the "users" collection in Firestore
        CollectionReference usersRef = FirestoreHelper.Database.Collection("users");

        // Asynchronously retrieve a snapshot of the collection
        QuerySnapshot usersSnapshot = await usersRef.GetSnapshotAsync();

        // Loop through each document in the snapshot
        foreach (var doc in usersSnapshot.Documents)
        {
            // Convert the document data to a UserInfo object
            UserInfo userInfo = doc.ConvertTo<UserInfo>();

            // Set the UserId property of the UserInfo object to the document's ID
            userInfo.UserId = doc.Id;

            // Add the UserInfo object to the ObservableCollection
            usersCollection.Add(userInfo);
        }

        // Return the populated ObservableCollection containing user information
        return usersCollection;
    }
    public async Task<ObservableCollection<MessageModel>> LoadMessages()
    {
        // Create an empty ObservableCollection to store user information
        ObservableCollection<MessageModel> usersCollection = new ObservableCollection<MessageModel>();

        // Reference the "users" collection in Firestore
        CollectionReference usersRef = FirestoreHelper.Database.Collection("Messages");

        // Asynchronously retrieve a snapshot of the collection
        QuerySnapshot usersSnapshot = await usersRef.GetSnapshotAsync();

        // Loop through each document in the snapshot
        foreach (var doc in usersSnapshot.Documents)
        {
            // Convert the document data to a UserInfo object
            MessageModel message = doc.ConvertTo<MessageModel>();

            // Set the UserId property of the UserInfo object to the document's ID
            message.Id = doc.Id;

            // Add the UserInfo object to the ObservableCollection
            usersCollection.Add(message);
        }

        // Return the populated ObservableCollection containing user information
        return usersCollection;
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
