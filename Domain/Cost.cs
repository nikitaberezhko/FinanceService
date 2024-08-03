namespace Domain;

public class Cost
{
    public Guid Id { get; set; }
    
    public Guid ReportId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public double Amount { get; set; }
    
    public bool IsDeleted { get; set; }
}