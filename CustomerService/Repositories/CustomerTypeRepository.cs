using CustomerService.Context;
using CustomerService.Interfaces;
using CustomerService.Models;
using System.Transactions;

namespace CustomerService.Repositories
{
    public class CustomerTypeRepository : ICustomerType
    {
        private CustomerContext _CustomerContext;

        public CustomerTypeRepository(CustomerContext CustomerContext)
        {
            _CustomerContext = CustomerContext;
        }

        public async Task<ServiceResponse<string>> AddCustomerType(MCustomerType ICustomerType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _CustomerContext.AddAsync(ICustomerType);
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
            _CustomerContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeleteCustomerType(int CustomerTypeId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(CustomerTypeId);
                _CustomerContext.MCustomerTypes.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditCustomerType(int CustomerTypeId, MCustomerType mCustomerTypeNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(CustomerTypeId);
                MCustomerType mCustomerTypeOld = responseData.Data;
                ModelHelper.CopyProperty(mCustomerTypeNew, ref mCustomerTypeOld);
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

        public async Task<IQueryable<MCustomerType>> GetAllCustomerType()
        {
            await Task.Yield();
            return _CustomerContext.MCustomerTypes;
        }

        public async Task<ServiceResponse<MCustomerType>> GetById(int CustomerTypeId)
        {
            ServiceResponse<MCustomerType> response = new ServiceResponse<MCustomerType>();
            try
            {
                MCustomerType CustomerTypeData = _CustomerContext.MCustomerTypes.Where(w => w.CustomerTypeNo == CustomerTypeId).FirstOrDefault();
                response.Data = CustomerTypeData;

                if (CustomerTypeData != null)
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
