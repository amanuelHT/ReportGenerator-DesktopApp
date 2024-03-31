namespace Report_Generator_EntityFramework.DTOs
{
    // The DTO class for ReportImageModel
    public class ReportImageModelDto
    {
        // Properties you want to expose to the client
        // Foreign key property
        public Guid ReportModelId { get; set; }

        // Navigation property for the related report
        public virtual ReportModelDto ReportModel { get; set; }


        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        // Additional properties or methods can be added here
    }


}

