using Domain;

namespace Services.Repositories.Interfaces;

public interface IReportRepository
{
    Task Add(Report report);
    
    Task Delete(Report report);

    Task<List<Report>> GetShortenedList();

    Task<Report> GetById(Report report);

}