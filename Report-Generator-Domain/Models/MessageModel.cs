using Google.Cloud.Firestore;

namespace Report_Generator_Domain.Models
{
    [FirestoreData]
    public class MessageModel
    {

        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Content { get; set; }

        [FirestoreProperty]
        public string Sender { get; set; }


        [FirestoreProperty]
        public string Receiver { get; set; }


        [FirestoreProperty]
        public string Filepath { get; set; }

        [FirestoreProperty]
        public Google.Cloud.Firestore.Timestamp Timestamp { get; set; }

        // Computed property to return the date in a readable format
        public DateTime Date => Timestamp.ToDateTime();

    }
}
