using ProductServices.Models;

namespace ProductServices.Interfaces
{
    public interface IProduct
    {
        Task<IQueryable<MProduct>> GetAllProduct();
        Task<ServiceResponse<MProduct>> GetById(int ProductId);
        Task<ServiceResponse<List<MProduct>>> GetByDesc(string keyword);
        Task<ServiceResponse<string>> AddProduct(MProduct IProduct);
        Task<ServiceResponse<string>> EditProduct(int ProductId, MProduct mProductNew);
        Task<ServiceResponse<string>> EditProductStock(int ProductId, int usedStock);
        Task<ServiceResponse<string>> DeleteProduct(int ProductId);
        void Commit();
    }
}
