using ProductServices.Models;

namespace ProductServices.Interfaces
{
    public interface ISparepart
    {
        Task<IQueryable<MSparepart>> GetAllSparepart();
        Task<ServiceResponse<MSparepart>> GetById(int SparepartId);
        Task<ServiceResponse<List<MSparepart>>> GetByDesc(string keyword);
        Task<ServiceResponse<string>> AddSparepart(MSparepart ISparepart);
        Task<ServiceResponse<string>> EditSparepart(int SparepartId, MSparepart mSparepartNew);
        Task<ServiceResponse<string>> EditSparepartStock(int ProductId, int usedStock);
        Task<ServiceResponse<string>> DeleteSparepart(int SparepartId);
        void Commit();
    }
}
