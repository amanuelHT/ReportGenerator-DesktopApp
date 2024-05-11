using Firebase.Storage;
using Google.Cloud.Firestore;
using System.IO;


namespace Final_project.Stores
{
    public static class FirestoreHelper
    {
        public static FirestoreDb Database { get; private set; }
        public static FirebaseStorage Storage { get; private set; }

        public static void InitializeFirestoreAndStorage()
        {
            var serviceAccountPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/serviceAccountKey.json");

            if (!File.Exists(serviceAccountPath))
            {
                throw new FileNotFoundException("The service account key file was not found.", serviceAccountPath);
            }

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", serviceAccountPath);
 
            Database = FirestoreDb.Create("hprd-24-040");

            Storage = new FirebaseStorage("hprd-24-040.appspot.com");
        }
    }
}


