using StoreServices.Models;

namespace StoreServices.Interfaces
{
    public interface IMarketingArea
    {
        Task<IQueryable<MMarketingArea>> GetAllMarketingArea();
        Task<ServiceResponse<MMarketingArea>> GetById(int MarketingAreaId);
        Task<ServiceResponse<string>> AddMarketingArea(MMarketingArea IMarketingArea);
        Task<ServiceResponse<string>> EditMarketingArea(int MarketingAreaId, MMarketingArea mMarketingAreaNew);
        Task<ServiceResponse<string>> DeleteMarketingArea(int MarketingAreaId);
        void Commit();
    }
}
