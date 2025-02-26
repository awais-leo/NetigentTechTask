using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class StatusLevelRepository : IStatusLevelRepository
    {
        private readonly NetigentContext _context;
        private readonly ILogger<StatusLevelRepository> _logger;
        public StatusLevelRepository(NetigentContext context, ILogger<StatusLevelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<StatusLevel>> GetAllStatusLevelsAsync()
        {
            _logger.LogInformation("Getting all status levels");

            return await _context.StatusLevels.ToListAsync();
        }

        public async Task<StatusLevel?> GetStatusLevelByIdAsync(int id)
        {
            _logger.LogInformation("Getting status level by id");

            return await _context.StatusLevels.FindAsync(id);
        }

        public async Task<int> AddStatusLevelAsync(StatusLevel statusLevel)
        {
            _logger.LogInformation("Adding status level");

            await _context.StatusLevels.AddAsync(statusLevel);
            return await _context.SaveChangesAsync();
            
        }

        public async Task<int> DeleteStatusLevelAsync(int id)
        {
            _logger.LogInformation("Deleting status level");

           return await _context.StatusLevels
                .Where(statusId => statusId.Id == id)
                .ExecuteDeleteAsync();
            
        }

        public async Task<int> UpdateStatusLevel(StatusLevel statusLevel)
        {
            _logger.LogInformation("Updating status level");

            return await _context.StatusLevels
                           .Where(x=>x.Id == statusLevel.Id)
                           .ExecuteUpdateAsync(setter => setter
                            .SetProperty(x => x.StatusName, statusLevel.StatusName)
                           );

        }
    }
}
