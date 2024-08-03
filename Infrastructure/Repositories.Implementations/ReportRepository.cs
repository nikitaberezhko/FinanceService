using Domain;
using Exceptions.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations;

public class ReportRepository(DbContext context) : IReportRepository
{
    public async Task Add(Report report)
    {
        try
        {
            report.Id = Guid.NewGuid();
            await context.Set<Report>().AddAsync(report);
            await context.SaveChangesAsync();
        }
        catch
        {
            throw new InfrastructureException
            {
                Title = "Report already exists",
                Message = $"Report with this date period: {report.Date} already exists",
                StatusCode = StatusCodes.Status409Conflict
            };
        }
        
    }
    

    public async Task Delete(Report report)
    {
        try
        {
            await context.Set<Report>().Where(x => x.Id == report.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, true));
            await context.Set<Revenue>().Where(x => x.ReportId == report.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, true));
            await context.Set<Cost>().Where(x => x.ReportId == report.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, true));
            
            await context.SaveChangesAsync();
        }
        catch
        {
            throw new InfrastructureException
            {
                Title = "Report not found",
                Message = $"Report with this id: {report.Id} not found",
                StatusCode = StatusCodes.Status404NotFound
            };
        }
    }

    public async Task<List<Report>> GetShortenedList()
    {
        var result = await context.Set<Report>()
            .IgnoreAutoIncludes()
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
        
        return result;
    }
    
    public async Task<Report> GetById(Report report)
    {
        var result = await context.Set<Report>().FirstOrDefaultAsync(x => x.Id == report.Id);
        if (result != null)
            return result;

        throw new InfrastructureException
        {
            Title = "Report not found",
            Message = $"Report with this id: {report.Id} not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
}