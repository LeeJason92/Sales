using CustomerService.Models;

namespace CustomerService.Interfaces
{
    public interface ICustomerType
    {
        Task<IQueryable<MCustomerType>> GetAllCustomerType();
        Task<ServiceResponse<MCustomerType>> GetById(int CustomerTypeId);
        Task<ServiceResponse<string>> AddCustomerType(MCustomerType ICustomerType);
        Task<ServiceResponse<string>> EditCustomerType(int CustomerTypeId, MCustomerType mCustomerTypeNew);
        Task<ServiceResponse<string>> DeleteCustomerType(int CustomerTypeId);
        void Commit();
    }
}
