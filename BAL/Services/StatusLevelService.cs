using BLL.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;


namespace BLL.Services
{
    public class StatusLevelService : IStatusLevelService
    {
        private readonly IStatusLevelRepository _statusLevelRepository;
        private readonly ILogger<StatusLevelService> _logger;

        public StatusLevelService(IStatusLevelRepository statusLevelRepository, ILogger<StatusLevelService> logger)
        {
            _statusLevelRepository = statusLevelRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<StatusLevel>> GetAllStatusLevelsAsync()
        {
            _logger.LogInformation("Fetching all status levels");
            return await _statusLevelRepository.GetAllStatusLevelsAsync();
        }

        public async Task<StatusLevel?> GetStatusLevelByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching status level by ID: {id}");
            return await _statusLevelRepository.GetStatusLevelByIdAsync(id);
        }

        public async Task<int> AddStatusLevelAsync(StatusLevel statusLevel)
        {
            _logger.LogInformation("Adding new status level");
            return await _statusLevelRepository.AddStatusLevelAsync(statusLevel);
        }

        public async Task<int> UpdateStatusLevelAsync(StatusLevel statusLevel)
        {
            _logger.LogInformation($"Updating status level ID: {statusLevel.Id}");
            return await _statusLevelRepository.UpdateStatusLevel(statusLevel);
        }

        public async Task<int> DeleteStatusLevelAsync(int id)
        {
            _logger.LogInformation($"Deleting status level ID: {id}");
            return await _statusLevelRepository.DeleteStatusLevelAsync(id);
        }
    }
}
