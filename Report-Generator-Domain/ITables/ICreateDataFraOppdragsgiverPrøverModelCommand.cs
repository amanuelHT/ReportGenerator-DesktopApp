using Report_Generator_Domain.Models;

namespace Report_Generator_Domain.ITables
{
    public interface ICreateDataFraOppdragsgiverPrøverModelCommand
    {
        Task Execute(Guid id, List<DataFraOppdragsgiverPrøverModel> tables);
    }
}
