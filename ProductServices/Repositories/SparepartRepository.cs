using ProductServices.Context;
using ProductServices.Interfaces;
using ProductServices.Models;
using System.Transactions;

namespace ProductServices.Repositories
{
    public class SparepartRepository : ISparepart
    {
        private ProductContext _productContext;

        public SparepartRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<ServiceResponse<string>> AddSparepart(MSparepart ISparepart)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _productContext.AddAsync(ISparepart);
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

        public async Task<ServiceResponse<string>> DeleteSparepart(int SparepartId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(SparepartId);
                _productContext.MSpareparts.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditSparepart(int SparepartId, MSparepart mSparepartNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(SparepartId);
                MSparepart mSparepartOld = responseData.Data;
                ModelHelper.CopySparepartProperty(mSparepartNew, ref mSparepartOld);
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

        public async Task<ServiceResponse<string>> EditSparepartStock(int ProductId, int countUsed)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(ProductId);
                MSparepart mProduct = responseData.Data;
                mProduct.Stock -= Math.Abs(countUsed);

                Commit();
                response.Data = "Edit Stock Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }
            await Task.Yield();

            return response;
        }

        public async Task<IQueryable<MSparepart>> GetAllSparepart()
        {
            await Task.Yield();
            return _productContext.MSpareparts;
        }

        public async Task<ServiceResponse<MSparepart>> GetById(int SparepartId)
        {
            ServiceResponse<MSparepart> response = new ServiceResponse<MSparepart>();
            try
            {
                MSparepart SparepartData = _productContext.MSpareparts.Where(w => w.SparepartNo == SparepartId).FirstOrDefault();
                response.Data = SparepartData;

                if (SparepartData != null)
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

        public async Task<ServiceResponse<List<MSparepart>>> GetByDesc(string keyword)
        {
            ServiceResponse<List<MSparepart>> response = new ServiceResponse<List<MSparepart>>();
            try
            {
                List<MSparepart> SparepartData = _productContext.MSpareparts.Where(w => w.SparepartDesc.Trim().ToLower().Contains(keyword.Trim().ToLower())).ToList();
                response.Data = SparepartData;
                response.TotalData = SparepartData.Count;

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
