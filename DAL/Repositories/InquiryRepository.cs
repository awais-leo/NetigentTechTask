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
    public class InquiryRepository : IInquiryRepository
    {
        private readonly NetigentContext _context;
        private readonly ILogger<InquiryRepository> _logger;
        public InquiryRepository(NetigentContext context, ILogger<InquiryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<Inqury>> GetAllInquiriesAsync()
        {
            _logger.LogInformation("Getting all inquiries");

            return await _context.Inquries.ToListAsync();
            
        }

        public async Task<Inqury?> GetInquiryByIdAsync(int id)
        {
            _logger.LogInformation("Getting inquiry by id");

            return await _context.Inquries.FindAsync(id) ?? new();
            
        }

        public async Task<int> AddInquiryAsync(Inqury inquiry)
        {
            _logger.LogInformation("Adding inquiry");

            await _context.Inquries.AddAsync(inquiry);
            return await _context.SaveChangesAsync();
            
        }

        public async Task<int> DeleteInquiryAsync(int id)
        {
            _logger.LogInformation("Deleting inquiry");

            return await _context.Inquries
                .Where(inquiryId => inquiryId.Id == id)
                .ExecuteDeleteAsync();
            
        }


        public async Task<int> UpdateInquiry(Inqury inquiry)
        {
            _logger.LogInformation("Updating inquiry");

            return await _context.Inquries
                           .Where(x => x.Id == inquiry.Id)
                           .ExecuteUpdateAsync(setter => setter
                            .SetProperty(x => x.ApplicationId, inquiry.ApplicationId)
                            .SetProperty(x => x.SendToPerson, inquiry.SendToPerson)
                            .SetProperty(x => x.SendToRole, inquiry.SendToRole)
                            .SetProperty(x => x.SendToPersonId, inquiry.SendToPersonId)
                            .SetProperty(x => x.Subject, inquiry.Subject)
                            .SetProperty(x => x.Inquiry, inquiry.Inquiry)
                            .SetProperty(x => x.Response, inquiry.Response)
                            .SetProperty(x => x.AskedDt, inquiry.AskedDt)
                            .SetProperty(x => x.CompletedDt, inquiry.CompletedDt)
                           );
           
        }
    }
}
