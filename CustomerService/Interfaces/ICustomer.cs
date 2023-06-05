using CustomerService.Models;

namespace CustomerService.Interfaces
{
    public interface ICustomer
    {
        Task<IQueryable<MCustomer>> GetAllCustomer();
        Task<ServiceResponse<MCustomer>> GetById(int CustomerId);
        Task<ServiceResponse<List<MCustomer>>> GetByName(string keyword);
        Task<ServiceResponse<string>> AddCustomer(MCustomer ICustomer);
        Task<ServiceResponse<string>> EditCustomer(int CustomerId, MCustomer mCustomerNew);
        Task<ServiceResponse<string>> DeleteCustomer(int CustomerId);
        void Commit();
    }
}
