using BLL.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;


namespace BLL.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILogger<ApplicationService> _logger;

        public ApplicationService(IApplicationRepository applicationRepository, ILogger<ApplicationService> logger)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
        {
            _logger.LogInformation("Fetching all applications");

            return await _applicationRepository.GetAllApplicationsAsync();
        }

        public async Task<Application?> GetApplicationByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching application by ID: {id}");

            return await _applicationRepository.GetApplicationByIdAsync(id);
        }

        public async Task<int> AddApplicationAsync(Application application)
        {
            _logger.LogInformation("Adding new application");

            return await _applicationRepository.AddApplicationAsync(application);
        }

        public async Task<int> UpdateApplicationAsync(Application application)
        {
            _logger.LogInformation($"Updating application ID: {application.Id}");

            return await _applicationRepository.UpdateApplication(application);
        }

        public async Task<int> DeleteApplicationAsync(int id)
        {
            _logger.LogInformation($"Deleting application ID: {id}");

            return await _applicationRepository.DeleteApplicationAsync(id);
        }
    }
}
