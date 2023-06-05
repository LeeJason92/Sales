using ProductServices.Context;
using ProductServices.Interfaces;
using ProductServices.Models;
using System.Transactions;

namespace ProductServices.Repositories
{
    public class SparepartTypeRepository : ISparepartType
    {
        private ProductContext _productContext;

        public SparepartTypeRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<ServiceResponse<string>> AddSparepartType(MSparepartType ISparepartType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _productContext.AddAsync(ISparepartType);
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
            _productContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeleteSparepartType(int SparepartTypeId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(SparepartTypeId);
                _productContext.MSparepartTypes.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditSparepartType(int SparepartTypeId, MSparepartType mSparepartTypeNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(SparepartTypeId);
                MSparepartType mSparepartTypeOld = responseData.Data;
                ModelHelper.CopySparepartTypeProperty(mSparepartTypeNew, ref mSparepartTypeOld);
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

        public async Task<IQueryable<MSparepartType>> GetAllSparepartType()
        {
            await Task.Yield();
            return _productContext.MSparepartTypes;
        }

        public async Task<ServiceResponse<MSparepartType>> GetById(int SparepartTypeId)
        {
            ServiceResponse<MSparepartType> response = new ServiceResponse<MSparepartType>();
            try
            {
                MSparepartType SparepartTypeData = _productContext.MSparepartTypes.Where(w => w.SparpartTypeNo == SparepartTypeId).FirstOrDefault();
                response.Data = SparepartTypeData;

                if (SparepartTypeData != null)
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
