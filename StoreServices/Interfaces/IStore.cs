using StoreServices.Models;

namespace StoreServices.Interfaces
{
    public interface IStore
    {
        Task<IQueryable<MStore>> GetAllStore();
        Task<ServiceResponse<MStore>> GetById(int StoreId);
        Task<ServiceResponse<string>> AddStore(MStore IStore);
        Task<ServiceResponse<string>> EditStore(int StoreId, MStore mStoreNew);
        Task<ServiceResponse<string>> DeleteStore(int StoreId);
        void Commit();
    }
}
