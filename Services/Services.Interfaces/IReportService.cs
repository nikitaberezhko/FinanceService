using OrderApiModels.ApiModels;
using Services.Models.Request;
using Services.Models.Response;

namespace Services.Services.Interfaces;

public interface IReportService
{
    Task<ReportModel> Generate(GenerateReportModel model);

    Task<Guid> Delete(DeleteReportModel model);

    Task<List<ShortenedReportModel>> GetShortenedList(GetAllReportsModel model);

    Task<ReportModel> GetById(GetReportByIdModel model);
}