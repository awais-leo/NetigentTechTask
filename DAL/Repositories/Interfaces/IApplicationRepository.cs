using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<Application>> GetAllApplicationsAsync();
        Task<Application?> GetApplicationByIdAsync(int id);
        Task<int> AddApplicationAsync(Application application);
        Task<int> UpdateApplication(Application application);
        Task<int> DeleteApplicationAsync(int id);

    }
}
