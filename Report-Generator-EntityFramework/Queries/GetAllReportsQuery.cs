using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Queries;
using System.Diagnostics;

namespace Report_Generator_EntityFramework.Queries
{
    public class GetAllReportsQuery : IGetAllReportsQuery
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public GetAllReportsQuery(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task<IEnumerable<ReportModel>> Execute()
        {
            try
            {
                using (var context = _contextFactory.Create())
                {
                    var reportModelDtos = await context.ReportModels
                        .Include(r => r.Images)
                        .ToListAsync();

                    return reportModelDtos.Select(dto => new ReportModel(
                        dto.Id,
                        dto.Tittle,
                        dto.Status,
                        dto.Kunde,
                        dto.Images.Select(imageDto => new ReportImageModel(
                            imageDto.Id,
                            imageDto.Name ?? "DefaultName", // Handle NULL Name
                            imageDto.ImageUrl ?? "DefaultImageUrl" // Handle NULL ImageUrl
                            )).ToList()
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); // Log the exception
                return new List<ReportModel>(); // Return an empty collection or handle as needed
            }
        }

    }
}
