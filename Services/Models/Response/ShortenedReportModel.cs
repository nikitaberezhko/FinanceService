namespace Services.Models.Response;

public class ShortenedReportModel
{
    public Guid Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public double Profit { get; set; }
    
    public double TotalRevenue { get; set; }
    
    public double TotalCost { get; set; }
}