namespace Services.Models.OtherModels;

public class CostModel
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }
    
    public double Amount { get; set; }
}