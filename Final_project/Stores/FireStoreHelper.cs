using Firebase.Storage;
using Google.Cloud.Firestore;
using System.IO;


namespace Final_project.Stores
{
    public static class FirestoreHelper
    {
        public static FirestoreDb Database { get; private set; }
        public static FirebaseStorage Storage { get; private set; }

        // Call this method early in your application startup process.
        public static void InitializeFirestoreAndStorage()
        {
            // Path to the JSON credentials file
            var serviceAccountPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/serviceAccountKey.json");

            if (!File.Exists(serviceAccountPath))
            {
                throw new FileNotFoundException("The service account key file was not found.", serviceAccountPath);
            }

            // Set the environment variable for Google Application Credentials
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", serviceAccountPath);

            // Create FirestoreDb instance
            Database = FirestoreDb.Create("hprd-24-040");

            // Create FirebaseStorage instance
            Storage = new FirebaseStorage("hprd-24-040.appspot.com");
        }
    }
}


//namespace Final_project.Stores
//{
//   internal static  class FireStoreHelper
//    {

//        public static FirestoreDb Databasec { get; private set; }


//        static string fireconfig = @"
//        {
//          ""type"": ""service_account"",
//          ""project_id"": ""hprd-24-040"",
//          ""private_key_id"": ""4999d0beb1460b671e996663b991b814f048b499"",
//          ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCYWXBwEv6SrHzx\nS6LLQmuK7/vMvspYssHo2fkuqoRb3YWLRXjjBiWAkTV8SrBRDdzlz3oJ0EemNWtE\nIv+C41A3b8qXyz3hs0qqskBfiHBf9q13w0cSiKI6B6/h67wmJTbGrS8gP0MBZnCK\n/1dE1FR/TtAR8FCuLpNyI8qabq//aDW8TmfTaPCcIj0ehVZZ0scY+gzUuuaZM3sd\nldJNxjIlgw9vpGIuv96IR3fRJNuvVE8Zg6LfmMmXrKwp2Qf8fGblvFnL88hf8WW0\nXczjESpyBpoaUG54mi53QdgZ/mo9eHTzSFwB6R8FnnNcTmq3IHPvJALOxQ7iec3x\nCkQ6S6UTAgMBAAECggEALmmU+Pe4aXu15pbJxg16kM2ykroPD/2JWIRLRv24q/nt\nDLRB8zu0ohnaxv5D/7Vu9e4CukmjAk4k2xGkcL4KepEKkbrfo8pItX7vYItC3OFc\nEG/Dz7AjZ/Vejj+QRriwms8426SaJ6uLHrVSY0wiX9RlTAm/sojK74ta1jhhUrf2\nmtASdDcjCoE0ytVn48JxohUloB+7dA8DwBHQgjIdksR5XL1nxZrYG6ZqeC1rJnmP\nx4cViYeG0atdTsRE4gKgQpE+Gyz0FQCndW9WDzHa7Rj950kJKVtUrfB+JcmN9PpV\nr7gGS2NaKHzl09Wq5WRzKew47fvIyybexS2nyKyqOQKBgQDRR/7p2rRTdhyDeyWn\nn+e4hZDFEHG8PIVOh52qPqF/dWaY7HJFP7huiZ8cA+xPUhQOK33eDEIO6Z+uPH7H\nBG7EdN9CCOjMM12BmfTcS9qhyaBMKQgig/fXW6r+CAzHrEopzQwYuA6iM3lymLID\nT5kQPbc7KjcyYd5/QyHVbutYSQKBgQC6W+a+qWzaAcPAZRnBFdx/sTTNm2DvyxRH\ndbvVdXzngPUNcFtMKfH4dEfyk2NetmTgcteaG3FyMAHJIWI7z5YnkgjQY3w9UUWF\n0iw4gnE5u1uxs5IBXUNB495Yyz15h43ZKPhgP5TA/4V0DRt+abXp7EyUOTRikj3L\n8B9P21pqewKBgGub22UfgY1QtASfM5NnU1y7wN6zP+gMLndcoCNDpQLGuQR0v6T/\nyLN9rARZuA5pI8rNbiDYqLbGRcbvcDig/NujRJDNx/YHi1LyeMc1cauy4uuGRZqJ\nxMxFZDzOotOgsVmhB1FGgq/AS/gyr4WoTgnd0fNoF42eaSuCt0jpibWRAoGBAIZe\nuVOEKf5PA2v3+cflEWXoyd/uRsjfrrGPzG2vFu59ZzPXbZzPKa8xeKcJar7h2H6V\nj6uIWhYxUzhIn+HsXaIOg9htwykbLnu8/TGJrEYqN4U7quzc6B/cQ3fWo267NKX+\nCoirj4BRPVJeGRLe+dG/FcBSNtlUBMbFTm5wBRN9AoGBAIj1ogIDO/D7rDDCfEsC\nYpJb6suMsAm4i2/ATdglzJ20pgkirIqcWgOYwq93HNGEKc9YQOijF9Er7KsgkbRo\nkR+Vn3cpakKo4KcYEXD4UijL2JY943RFd9ZZ0CamkuZ8CKXE8HkCGp7nP+B6eO8r\neQKp+79TiOSv0SwKK9CJHate\n-----END PRIVATE KEY-----\n"",
//          ""client_email"": ""firebase-adminsdk-l7jhz@hprd-24-040.iam.gserviceaccount.com"",
//          ""client_id"": ""104361092473159268842"",
//          ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
//          ""token_uri"": ""https://oauth2.googleapis.com/token"",
//          ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
//          ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-l7jhz%40hprd-24-040.iam.gserviceaccount.com"",
//          ""universe_domain"": ""googleapis.com""
//        }
//        ";
//                static string filepath = " ";

//        public static void SetEnviromentVariable(FirestoreDb Database) 
//        {
//            filepath = Path.Combine(Path.GetTempPath(),
//                Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".json";
//            File.WriteAllText(filepath, fireconfig);
//            File.SetAttributes(filepath, FileAttributes.Hidden);
//            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
//            Database = FirestoreDb.Create("hprd-24-040");
//            File.Delete(filepath);
//        }

//    }
//}
