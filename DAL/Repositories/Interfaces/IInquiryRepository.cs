using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IInquiryRepository
    {
        Task<IEnumerable<Inqury>> GetAllInquiriesAsync();
        Task<Inqury?> GetInquiryByIdAsync(int id);
        Task<int> AddInquiryAsync(Inqury inquiry);
        Task<int> UpdateInquiry(Inqury inquiry);
        Task<int> DeleteInquiryAsync(int id);
    }
}
