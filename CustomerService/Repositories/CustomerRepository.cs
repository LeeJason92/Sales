using CustomerService.Context;
using CustomerService.Interfaces;
using CustomerService.Models;
using Microsoft.Extensions.Primitives;
using System.Transactions;

namespace CustomerService.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private CustomerContext _CustomerContext;

        public CustomerRepository(CustomerContext CustomerContext)
        {
            _CustomerContext = CustomerContext;
        }

        public async Task<ServiceResponse<string>> AddCustomer(MCustomer ICustomer)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _CustomerContext.AddAsync(ICustomer);
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

        public async Task<ServiceResponse<string>> DeleteCustomer(int CustomerId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(CustomerId);
                _CustomerContext.MCustomers.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditCustomer(int CustomerId, MCustomer mCustomerNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(CustomerId);
                MCustomer mCustomerOld = responseData.Data;
                ModelHelper.CopyCustomerProperty(mCustomerNew, ref mCustomerOld);
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

        public async Task<IQueryable<MCustomer>> GetAllCustomer()
        {
            await Task.Yield();
            return _CustomerContext.MCustomers;
        }

        public async Task<ServiceResponse<MCustomer>> GetById(int CustomerId)
        {
            ServiceResponse<MCustomer> response = new ServiceResponse<MCustomer>();
            try
            {
                MCustomer CustomerData = _CustomerContext.MCustomers.Where(w => w.CustomerNo == CustomerId).FirstOrDefault();
                response.Data = CustomerData;

                if(CustomerData != null) 
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

        public async Task<ServiceResponse<List<MCustomer>>> GetByName(string keyword)
        {
            ServiceResponse<List<MCustomer>> response = new ServiceResponse<List<MCustomer>>();
            try
            {
                List<MCustomer> CustomerData = _CustomerContext.MCustomers.Where(w => w.CustomerName.Trim().ToLower().Contains(keyword.Trim().ToLower())).ToList();
                response.Data = CustomerData;
                response.TotalData = CustomerData.Count();

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
