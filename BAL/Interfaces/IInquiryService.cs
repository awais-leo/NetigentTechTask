using DAL.Models;

namespace BLL.Interfaces
{
    public interface IInquiryService
    {
        Task<IEnumerable<Inqury>> GetAllInquiriesAsync();
        Task<Inqury?> GetInquiryByIdAsync(int id);
        Task<int> AddInquiryAsync(Inqury inquiry);
        Task<int> UpdateInquiryAsync(Inqury inquiry);
        Task<int> DeleteInquiryAsync(int id);
    }
}
