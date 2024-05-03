namespace Report_Generator_Domain.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Filepath { get; set; }

        // Constructor with parameters
        public MessageModel(string content, string sender, string receiver, string filepath)
        {
            Content = content;
            Sender = sender;
            Receiver = receiver;
            Filepath = filepath;
        }

    }
}
