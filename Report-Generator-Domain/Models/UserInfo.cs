using Google.Cloud.Firestore;

namespace Report_Generator_Domain.Models
{
    [FirestoreData]
    public class UserInfo
    {
        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }

        [FirestoreProperty]
        public string BirthDate { get; set; }

        [FirestoreProperty]
        public string Role { get; set; }
    }
}
