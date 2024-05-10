using Final_project.Stores;
using Firebase.Storage;
using Google.Cloud.Firestore;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.IO;

public class FirebaseStore
{
    private readonly FirestoreDb _db;
    private readonly FirebaseStorage _storage;

    public FirebaseStore()
    {
        _db = FirestoreHelper.Database;
        _storage = FirestoreHelper.Storage;
    }

    public async Task UploadReportAsync(Stream stream, string filename)
    {
        await _storage
            .Child("reports")
            .Child(filename)
            .PutAsync(stream);
    }

    public async Task UploadReportMessageAsync(Stream stream, string filename, string id)
    {
        await _storage
            .Child("Messages")
            .Child(id)
            .Child(filename)
            .PutAsync(stream);
    }

    public async Task<string> GetDownloadUrlAsync(string filePath)
    {
        return await _storage
            .Child(filePath)
            .GetDownloadUrlAsync();
    }

    public async Task DeleteFileAsync(string filePath)
    {
        await _storage
            .Child(filePath)
            .DeleteAsync();
    }

    public async Task AddReportAsync(string title)
    {
        CollectionReference collectionRef = _db.Collection("reports");
        await collectionRef.AddAsync(new { title });
    }

    public async Task SendMessageAsync(MessageModel messageModel)
    {
        try
        {
            CollectionReference collectionRef = _db.Collection("Messages");
            DocumentReference docRef = await collectionRef.AddAsync(new
            {
                Content = messageModel.Content,
                Sender = messageModel.Sender,
                Receiver = messageModel.Receiver,
                Filepath = messageModel.Filepath,
                Timestamp = DateTime.UtcNow
            });
            string messageId = docRef.Id;
            Console.WriteLine("Successfully sent message with ID: " + messageId);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending message: " + ex.Message);
        }
    }

    public async Task DeleteReportAsync(string title)
    {
        CollectionReference collectionRef = _db.Collection("reports");
        QuerySnapshot querySnapshot = await collectionRef.WhereEqualTo("title", title).GetSnapshotAsync();

        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
        {
            string documentId = documentSnapshot.Id;
            DocumentReference docRef = collectionRef.Document(documentId);
            await docRef.DeleteAsync();
        }
    }

    public async Task DeleteUserAsync(string userId)
    {
        CollectionReference collectionRef = _db.Collection("users");
        DocumentReference docRef = collectionRef.Document(userId);
        await docRef.DeleteAsync();
    }

    public async Task<ObservableCollection<UserInfo>> LoadUsersAsync()
    {
        ObservableCollection<UserInfo> usersCollection = new ObservableCollection<UserInfo>();
        CollectionReference usersRef = FirestoreHelper.Database.Collection("users");
        QuerySnapshot usersSnapshot = await usersRef.GetSnapshotAsync();

        foreach (var doc in usersSnapshot.Documents)
        {
            UserInfo userInfo = doc.ConvertTo<UserInfo>();
            userInfo.UserId = doc.Id;
            usersCollection.Add(userInfo);
        }

        return usersCollection;
    }

    public async Task<ObservableCollection<MessageModel>> LoadMessages()
    {
        ObservableCollection<MessageModel> messages = new ObservableCollection<MessageModel>();

        try
        {
            CollectionReference messagesRef = FirestoreHelper.Database.Collection("Messages");
            QuerySnapshot snapshot = await messagesRef.GetSnapshotAsync();

            foreach (var doc in snapshot.Documents)
            {
                MessageModel message = doc.ConvertTo<MessageModel>();
                message.Id = doc.Id;
                messages.Add(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading messages: {ex.Message}");
        }

        return messages;
    }

    public async Task<IEnumerable<string>> GetReportTitlesAsync()
    {
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
