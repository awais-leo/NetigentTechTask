using DAL.Models;

namespace BLL.Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetAllApplicationsAsync();
        Task<Application?> GetApplicationByIdAsync(int id);
        Task<int> AddApplicationAsync(Application application);
        Task<int> UpdateApplicationAsync(Application application);
        Task<int> DeleteApplicationAsync(int id);
    }
}
