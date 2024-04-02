using Domain.Models;

namespace Report_Generator_Domain.Commands
{
    public interface ICreateImageCommand
    {
        Task Execute(Guid id, List<ReportImageModel> images);
    }
}
