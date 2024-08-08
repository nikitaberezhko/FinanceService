using Domain;

namespace Services.Repositories.Interfaces;

public interface IReportRepository
{
    Task<Guid> Add(Report report);

    Task<Guid> Delete(Report report);

    Task<List<Report>> GetShortenedList(int page, int pageSize);

    Task<Report> GetById(Report report);

}