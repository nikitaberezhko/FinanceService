using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Report> Reports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>().Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Report>().Property(x => x.Date).HasColumnName("date");
        modelBuilder.Entity<Report>().Property(x => x.TotalRevenue).HasColumnName("total_revenue");
        modelBuilder.Entity<Report>().Property(x => x.TotalCost).HasColumnName("total_cost");
        modelBuilder.Entity<Report>().Property(x => x.IsDeleted).HasColumnName("is_deleted");
        
        
        modelBuilder.Entity<Revenue>().Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Revenue>().Property(x => x.ReportId).HasColumnName("report_id");
        modelBuilder.Entity<Revenue>().Property(x => x.OrderId).HasColumnName("order_id");
        modelBuilder.Entity<Revenue>().Property(x => x.Amount).HasColumnName("amount");
        modelBuilder.Entity<Revenue>().Property(x => x.IsDeleted).HasColumnName("is_deleted");
        
        
        modelBuilder.Entity<Cost>().Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Cost>().Property(x => x.ReportId).HasColumnName("report_id");
        modelBuilder.Entity<Cost>().Property(x => x.OrderId).HasColumnName("order_id");
        modelBuilder.Entity<Cost>().Property(x => x.Amount).HasColumnName("amount");
        modelBuilder.Entity<Cost>().Property(x => x.IsDeleted).HasColumnName("is_deleted");
    }
}