using Google.Cloud.Firestore;

namespace Report_Generator_Domain.Models
{
    [FirestoreData]
    public class UserInfo
    {

        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }

        [FirestoreProperty]
        public string BirthDate { get; set; }

        [FirestoreProperty]
        public string Role { get; set; }

        public string RoleColor
        {
            get
            {
                return Role switch
                {
                    "Kunde" => "#ffc0cb",
                    "User" => "#ffd700",
                    _ => "#23c3eb"
                };
            }
        }
    }
}
