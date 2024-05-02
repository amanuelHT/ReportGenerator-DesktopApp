namespace Report_Generator_Domain.Models
{
    public class MessageModel
    {
        public string Content { get; set; }
        public UserInfo Sender { get; set; }
        public UserInfo Receiver { get; set; }

        // Constructor with parameters
        public MessageModel(string content, UserInfo sender, UserInfo receiver)
        {
            Content = content;
            Sender = sender;
            Receiver = receiver;
        }

    }
}
