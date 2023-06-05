using ProductServices.Models;

namespace ProductServices.Interfaces
{
    public interface IProductType
    {
        Task<IQueryable<MProductType>> GetAllProductType();
        Task<ServiceResponse<MProductType>> GetById(int ProductTypeId);
        Task<ServiceResponse<string>> AddProductType(MProductType IProductType);
        Task<ServiceResponse<string>> EditProductType(int ProductTypeId, MProductType mProductTypeNew);
        Task<ServiceResponse<string>> DeleteProductType(int ProductTypeId);
        void Commit();
    }
}
