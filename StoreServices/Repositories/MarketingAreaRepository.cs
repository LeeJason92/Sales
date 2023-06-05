using StoreServices.Context;
using StoreServices.Interfaces;
using StoreServices.Models;
using System.Transactions;

namespace StoreServices.Repositories
{
    public class MarketingAreaRepository : IMarketingArea
    {
        private StoreContext _StoreContext;

        public MarketingAreaRepository(StoreContext StoreContext)
        {
            _StoreContext = StoreContext;
        }

        public async Task<ServiceResponse<string>> AddMarketingArea(MMarketingArea IMarketingArea)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _StoreContext.AddAsync(IMarketingArea);
                Commit();
                response.Data = "Save Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;

        }

        public void Commit()
        {
            _StoreContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeleteMarketingArea(int MarketingAreaId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(MarketingAreaId);
                _StoreContext.MMarketingAreas.Remove(responseData.Data);
                Commit();
                response.Data = "Delete Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<string>> EditMarketingArea(int MarketingAreaId, MMarketingArea mMarketingAreaNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(MarketingAreaId);
                MMarketingArea mMarketingAreaOld = responseData.Data;
                ModelHelper.CopyMarketingAreaProperty(mMarketingAreaNew, ref mMarketingAreaOld);
                Commit();
                response.Data = "Edit Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }
            await Task.Yield();

            return response;
        }


        public async Task<IQueryable<MMarketingArea>> GetAllMarketingArea()
        {
            await Task.Yield();
            return _StoreContext.MMarketingAreas;
        }

        public async Task<ServiceResponse<MMarketingArea>> GetById(int MarketingAreaId)
        {
            ServiceResponse<MMarketingArea> response = new ServiceResponse<MMarketingArea>();
            try
            {
                MMarketingArea MarketingAreaData = _StoreContext.MMarketingAreas.Where(w => w.AreaNo == MarketingAreaId).FirstOrDefault();
                response.Data = MarketingAreaData;

                if (MarketingAreaData != null)
                {
                    response.TotalData = 1;
                }

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

    }
}
