using WebApi.Models.ApiModels;

namespace WebApi.Models.Response;

public class GetAllReportsResponse
{
    public List<ShortenedReportApiModel> Reports { get; set; }
}