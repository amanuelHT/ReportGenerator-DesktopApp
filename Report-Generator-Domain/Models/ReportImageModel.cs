namespace Domain.Models
{
    public class ReportImageModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public Guid ReportModelId { get; set; }
        public ReportModel ReportModel { get; set; }

        public ReportImageModel(Guid id, string name, string imageUrl, Guid reportModelId)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            ReportModelId = reportModelId; 
        }
    }
}
