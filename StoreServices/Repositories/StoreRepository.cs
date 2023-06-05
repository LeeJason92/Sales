using StoreServices.Context;
using StoreServices.Interfaces;
using StoreServices.Models;
using System.Transactions;

namespace StoreServices.Repositories
{
    public class StoreRepository : IStore
    {
        private StoreContext _StoreContext;

        public StoreRepository(StoreContext StoreContext)
        {
            _StoreContext = StoreContext;
        }

        public async Task<ServiceResponse<string>> AddStore(MStore IStore)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _StoreContext.AddAsync(IStore);
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

        public async Task<ServiceResponse<string>> DeleteStore(int StoreId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(StoreId);
                _StoreContext.MStores.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditStore(int StoreId, MStore mStoreNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(StoreId);
                MStore mStoreOld = responseData.Data;
                ModelHelper.CopyProperty(mStoreNew, ref mStoreOld);
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

        public async Task<IQueryable<MStore>> GetAllStore()
        {
            await Task.Yield();
            return _StoreContext.MStores;
        }

        public async Task<ServiceResponse<MStore>> GetById(int StoreId)
        {
            ServiceResponse<MStore> response = new ServiceResponse<MStore>();
            try
            {
                MStore StoreData = _StoreContext.MStores.Where(w => w.StoreNo == StoreId).FirstOrDefault();
                response.Data = StoreData;

                if (StoreData != null)
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
