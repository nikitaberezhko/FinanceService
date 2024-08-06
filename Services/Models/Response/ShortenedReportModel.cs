namespace Services.Models.Response;

public class ShortenedReportModel
{
    public Guid Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public double TotalRevenue { get; set; }
    
    public double TotalCost { get; set; }
}