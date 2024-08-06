using Services.Models.OtherModels;

namespace Services.Models.Response;

public class ReportModel
{
    public Guid Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public ICollection<RevenueModel> Revenues { get; set; }
    
    public ICollection<CostModel> Costs { get; set; }
    
    public double TotalRevenue { get; set; }
    
    public double TotalCost { get; set; }
}