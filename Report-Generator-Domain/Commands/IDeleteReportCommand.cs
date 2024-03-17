namespace Report_Generator_Domain.Commands
{
    public interface IDeleteReportCommand
    {
        Task Execute(Guid id);
    }
}
