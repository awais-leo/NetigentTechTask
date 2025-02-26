using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IStatusLevelRepository
    {
        Task<IEnumerable<StatusLevel>> GetAllStatusLevelsAsync();
        Task<StatusLevel?> GetStatusLevelByIdAsync(int id);
        Task<int> AddStatusLevelAsync(StatusLevel statusLevel);
        Task<int> UpdateStatusLevel(StatusLevel statusLevel);
        Task<int> DeleteStatusLevelAsync(int id);
    }
}
