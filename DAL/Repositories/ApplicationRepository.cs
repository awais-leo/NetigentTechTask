using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly NetigentContext _context;
        private readonly ILogger<ApplicationRepository> _logger;
        public ApplicationRepository(NetigentContext context, ILogger<ApplicationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
        {
            _logger.LogInformation("Getting all applications");

            return await  _context.Applications
                        .AsNoTracking()
                        .Include(a => a.Status).ToListAsync();
            
        }

        public async Task<Application?> GetApplicationByIdAsync(int id)
        {
            _logger.LogInformation("Getting application by id");

             return await _context.Applications
                               .Include(a => a.Inquries)
                               .Include(a => a.Status)
                               .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> AddApplicationAsync(Application application)
        {
            _logger.LogInformation("Adding application");

            await _context.Applications.AddAsync(application);
            return await _context.SaveChangesAsync() ;
            
        }

        public async Task<int> DeleteApplicationAsync(int id)
        {
            _logger.LogInformation("Deleting application");

            return await _context.Applications
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();
            
        }

        public async Task<int> UpdateApplication(Application application)
        {
            _logger.LogInformation("Updating application");

            return await _context.Applications
                .Where(a => a.Id == application.Id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(a => a.AppStatus, application.AppStatus)
                    .SetProperty(a => a.ProjectRef, application.ProjectRef)
                    .SetProperty(a => a.ProjectName, application.ProjectName)
                    .SetProperty(a => a.ProjectLocation, application.ProjectLocation)
                    .SetProperty(a => a.OpenDt, application.OpenDt)
                    .SetProperty(a => a.StartDt, application.StartDt)
                    .SetProperty(a => a.CompletedDt, application.CompletedDt)
                    .SetProperty(a => a.ProjectValue, application.ProjectValue)
                    .SetProperty(a => a.StatusId, application.StatusId)
                    .SetProperty(a => a.Notes, application.Notes)
                    .SetProperty(a => a.Modified, application.Modified)
                    .SetProperty(a => a.IsDeleted, application.IsDeleted)
                    .SetProperty(a => a.Inquries, application.Inquries)
                    .SetProperty(a => a.Status, application.Status)
                );
            
        }
    }
}
