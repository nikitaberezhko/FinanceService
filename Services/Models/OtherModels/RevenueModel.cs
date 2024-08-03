namespace Services.Models.OtherModels;

public class RevenueModel
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }
    
    public double Amount { get; set; }
}