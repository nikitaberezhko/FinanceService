namespace Services.Models.Request;

public class GenerateReportModel
{
    public DateOnly EndDate { get; set; }
    
    public int Period { get; set; }
}