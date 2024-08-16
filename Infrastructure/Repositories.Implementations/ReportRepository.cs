using Domain;
using Exceptions.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations;

public class ReportRepository(DbContext context) : IReportRepository
{
    public async Task<Guid> Add(Report report)
    {
        if (await context.Set<Report>()
                .FirstOrDefaultAsync(x => x.Date == report.Date && !x.IsDeleted) == null)
        {
            report.Id = Guid.NewGuid();
            await context.Set<Report>().AddAsync(report);
            await context.SaveChangesAsync();
            
            return report.Id;
        }
           
        throw new InfrastructureException
        {
            Title = "Report already exists",
            Message = $"Report with this date generated: {report.Date} already exists",
            StatusCode = StatusCodes.Status409Conflict
        };
    }
    

    public async Task<Guid> Delete(Report report)
    {
        if (await context.Set<Report>()
                .FirstOrDefaultAsync(x => x.Id == report.Id && !x.IsDeleted) != null)
        {
            await context.Set<Report>().Where(x => x.Id == report.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, true));
            await context.Set<Revenue>().Where(x => x.ReportId == report.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, true));
            await context.Set<Cost>().Where(x => x.ReportId == report.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, true));
        
            await context.SaveChangesAsync();
            return report.Id;
        }
        
        throw new InfrastructureException
        {
            Title = "Report not found",
            Message = $"Report with this id: {report.Id} not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task<List<Report>> GetShortenedList(int page, int pageSize)
    {
        var result = await context.Set<Report>()
            .IgnoreAutoIncludes()
            .Where(x => x.IsDeleted == false)
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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