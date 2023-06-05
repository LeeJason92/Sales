using PricingServices.Models;

namespace PricingServices.Interfaces
{
    public interface IPricing
    {
        Task<IQueryable<MPricing>> GetAllPricing();
        Task<ServiceResponse<List<MPricing>>> GetPricing(int productId, int storeId, int customerTypeId, DateTime validFrom, DateTime validTo);
        Task<ServiceResponse<MPricing>> GetByPricingId(int pricingId);
        Task<ServiceResponse<List<MPricing>>> GetByProductId(int ProductId);
        Task<ServiceResponse<List<MPricing>>> GetByStoreId(int StoreId);
        Task<ServiceResponse<List<MPricing>>> GetByCustomerType(int CustomerTypeId);
        Task<ServiceResponse<string>> AddPricing(MPricing IPricing);
        Task<ServiceResponse<string>> EditByPricingId(int pricingId, MPricing mPricingNew);
        Task<ServiceResponse<string>> EditPricing(int productId, int storeId, int customerTypeId, DateTime validFrom, DateTime validTo, MPricing mPricingNew);
        Task<ServiceResponse<string>> DeleteByPricingId(int pricingId);
        Task<ServiceResponse<string>> DeletePricing(int productId, int storeId, int customerTypeId, DateTime validFrom, DateTime validTo);
        void Commit();
    }
}
