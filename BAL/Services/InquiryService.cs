using BLL.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly IInquiryRepository _inquiryRepository;
        private readonly ILogger<InquiryService> _logger;

        public InquiryService(IInquiryRepository inquiryRepository, ILogger<InquiryService> logger)
        {
            _inquiryRepository = inquiryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Inqury>> GetAllInquiriesAsync()
        {
            _logger.LogInformation("Fetching all inquiries");
            return await _inquiryRepository.GetAllInquiriesAsync();
        }

        public async Task<Inqury?> GetInquiryByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching inquiry by ID: {id}");
            return await _inquiryRepository.GetInquiryByIdAsync(id);
        }

        public async Task<int> AddInquiryAsync(Inqury inquiry)
        {
            _logger.LogInformation("Adding new inquiry");
            return await _inquiryRepository.AddInquiryAsync(inquiry);
        }

        public async Task<int> UpdateInquiryAsync(Inqury inquiry)
        {
            _logger.LogInformation($"Updating inquiry ID: {inquiry.Id}");
            return await _inquiryRepository.UpdateInquiry(inquiry);
        }

        public async Task<int> DeleteInquiryAsync(int id)
        {
            _logger.LogInformation($"Deleting inquiry ID: {id}");
            return await _inquiryRepository.DeleteInquiryAsync(id);
        }
    }
}
