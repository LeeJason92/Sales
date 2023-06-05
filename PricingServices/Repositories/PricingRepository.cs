using PricingServices.Context;
using PricingServices.Interfaces;
using PricingServices.Models;
using System.Transactions;

namespace PricingServices.Repositories
{
    public class PricingRepository : IPricing
    {
        private PricingContext _PricingContext;

        public PricingRepository(PricingContext PricingContext)
        {
            _PricingContext = PricingContext;
        }

        public async Task<ServiceResponse<string>> AddPricing(MPricing IPricing)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _PricingContext.AddAsync(IPricing);
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
            _PricingContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeletePricing(int productId, int storeId, int customerTypeId, DateTime validFrom, DateTime validTo)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetPricing(productId, storeId, customerTypeId, validFrom, validTo);
                _PricingContext.MPricings.Remove(responseData.Data[0]);
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

        public async Task<ServiceResponse<string>> DeleteByPricingId(int pricingId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetByPricingId(pricingId);
                _PricingContext.MPricings.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditPricing(int productId, int storeId, int customerTypeId, DateTime validFrom, DateTime validTo, MPricing mPricingNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetPricing(productId, storeId, customerTypeId, validFrom, validTo);
                MPricing mPricingOld = responseData.Data[0];
                ModelHelper.CopyPricingProperty(mPricingNew, ref mPricingOld);
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

        public async Task<ServiceResponse<string>> EditByPricingId(int pricingId, MPricing mPricingNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetByPricingId(pricingId);
                MPricing mPricingOld = responseData.Data;
                ModelHelper.CopyPricingProperty(mPricingNew, ref mPricingOld);
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

        public async Task<IQueryable<MPricing>> GetAllPricing()
        {
            await Task.Yield();
            return _PricingContext.MPricings;
        }

        public async Task<ServiceResponse<List<MPricing>>> GetPricing(int productId, int storeId, int customerTypeId, DateTime validFrom, DateTime validTo)
        {
            ServiceResponse<List<MPricing>> response = new ServiceResponse<List<MPricing>>();
            try
            {
                List<MPricing> PricingData = _PricingContext.MPricings.Where(w => w.ProductNo == productId && w.StoreNo == storeId
                                                                        && w.CustomerTypeNo == customerTypeId
                                                                        && w.ValidFrom.Date == Convert.ToDateTime(validFrom).Date
                                                                        && w.ValidTo.Date == Convert.ToDateTime(validTo).Date).ToList();
                response.Data = PricingData;
                response.TotalData = PricingData.Count;

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<MPricing>> GetByPricingId(int pricingId)
        {
            ServiceResponse<MPricing> response = new ServiceResponse<MPricing>();
            try
            {
                MPricing PricingData = _PricingContext.MPricings.Where(w => w.Id == pricingId).FirstOrDefault();
                response.Data = PricingData;

                if (PricingData != null)
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

        public async Task<ServiceResponse<List<MPricing>>> GetByProductId(int ProductId)
        {
            ServiceResponse<List<MPricing>> response = new ServiceResponse<List<MPricing>>();
            try
            {
                List<MPricing> PricingData = _PricingContext.MPricings.Where(w => w.ProductNo == ProductId).ToList();
                response.Data = PricingData;
                response.TotalData = PricingData.Count;

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<List<MPricing>>> GetByStoreId(int StoreId)
        {
            ServiceResponse<List<MPricing>> response = new ServiceResponse<List<MPricing>>();
            try
            {
                List<MPricing> PricingData = _PricingContext.MPricings.Where(w => w.StoreNo == StoreId).ToList();
                response.Data = PricingData;
                response.TotalData = PricingData.Count;

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<List<MPricing>>> GetByCustomerType(int CustomerTypeId)
        {
            ServiceResponse<List<MPricing>> response = new ServiceResponse<List<MPricing>>();
            try
            {
                List<MPricing> PricingData = _PricingContext.MPricings.Where(w => w.CustomerTypeNo == CustomerTypeId).ToList();
                response.Data = PricingData;
                response.TotalData = PricingData.Count;

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
