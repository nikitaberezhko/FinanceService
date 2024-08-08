namespace WebApi.Models.ApiModels;

public class RevenueApiModel
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }
    
    public double Amount { get; set; }
}