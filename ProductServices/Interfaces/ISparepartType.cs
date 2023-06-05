using ProductServices.Models;

namespace ProductServices.Interfaces
{
    public interface ISparepartType
    {
        Task<IQueryable<MSparepartType>> GetAllSparepartType();
        Task<ServiceResponse<MSparepartType>> GetById(int SparepartTypeId);
        Task<ServiceResponse<string>> AddSparepartType(MSparepartType ISparepartType);
        Task<ServiceResponse<string>> EditSparepartType(int SparepartTypeId, MSparepartType mSparepartTypeNew);
        Task<ServiceResponse<string>> DeleteSparepartType(int SparepartTypeId);
        void Commit();
    }
}
