namespace Domain;

public class Report
{
    public Guid Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public virtual ICollection<Revenue> Revenues { get; set; }
    
    public virtual ICollection<Cost> Costs { get; set; }
    
    public double TotalRevenue { get; set; }
    
    public double TotalCost { get; set; }
    
    public bool IsDeleted { get; set; }
}