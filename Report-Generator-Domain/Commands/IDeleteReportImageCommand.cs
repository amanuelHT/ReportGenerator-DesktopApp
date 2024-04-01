namespace Report_Generator_Domain.Commands
{
    public interface IDeleteReportImageCommand
    {
        Task Execute(Guid id);
    }
}
