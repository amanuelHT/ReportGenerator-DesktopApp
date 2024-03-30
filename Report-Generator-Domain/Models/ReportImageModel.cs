namespace Domain.Models
{
    public class ReportImageModel
    {
        public Guid Id { get; }
        public string Name { get; }
        public string ImageUrl { get; }

        public ReportImageModel(Guid id, string name, string imageUrl)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}