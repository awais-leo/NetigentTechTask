using DAL.Models;

namespace BLL.Interfaces
{
    public interface IStatusLevelService
    {
        Task<IEnumerable<StatusLevel>> GetAllStatusLevelsAsync();
        Task<StatusLevel?> GetStatusLevelByIdAsync(int id);
        Task<int> AddStatusLevelAsync(StatusLevel statusLevel);
        Task<int> UpdateStatusLevelAsync(StatusLevel statusLevel);
        Task<int> DeleteStatusLevelAsync(int id);
    }
}
