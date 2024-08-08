namespace WebApi.Models.Request;

public class GenerateReportRequest
{
    public DateOnly EndDate { get; set; }
    
    public int Period { get; set; }
}