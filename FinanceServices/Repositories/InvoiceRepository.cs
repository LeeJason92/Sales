using FinanceServices.Context;
using FinanceServices.Interfaces;
using FinanceServices.Models;
using Microsoft.Extensions.Primitives;
using System.Transactions;

namespace FinanceServices.Repositories
{
    public class InvoiceRepository : IInvoice
    {
        private FinanceContext _FinanceContext;
        
        public InvoiceRepository(FinanceContext FinanceContext)
        {
            _FinanceContext = FinanceContext;
        }

        public async Task<ServiceResponse<string>> AddInvoice(TInvoice IInvoice)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var checkDoResponse = await GetByDeliveryOrderId(IInvoice.DeliveryOrderNo);
                if(checkDoResponse.isSuccess)
                {
                    if(checkDoResponse.Data != null)
                    {
                        throw new Exception("Invoice Data Already Exists");
                    }
                    else
                    {
                        await _FinanceContext.AddAsync(IInvoice);
                        Commit();
                        response.Data = "Save Success";
                        response.Success();
                    }
                }
                else
                {
                    throw new Exception(checkDoResponse.Message);
                }
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;

        }

        public void Commit()
        {
            _FinanceContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeleteInvoice(int InvoiceId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(InvoiceId);
                _FinanceContext.TInvoices.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditInvoice(int InvoiceId, TInvoice TInvoiceNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(InvoiceId);
                TInvoice TInvoiceOld = responseData.Data;
                ModelHelper.CopyProperty(TInvoiceNew, ref TInvoiceOld);
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

        public async Task<IQueryable<TInvoice>> GetAllInvoice()
        {
            await Task.Yield();
            return _FinanceContext.TInvoices;
        }

        public async Task<ServiceResponse<TInvoice>> GetById(int InvoiceId)
        {
            ServiceResponse<TInvoice> response = new ServiceResponse<TInvoice>();
            try
            {
                TInvoice InvoiceData = _FinanceContext.TInvoices.Where(w => w.InvoiceNo == InvoiceId).FirstOrDefault();
                response.Data = InvoiceData;

                if(InvoiceData != null) 
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

        public async Task<ServiceResponse<TInvoice>> GetByDeliveryOrderId(int deliveryOrderId)
        {
            ServiceResponse<TInvoice> response = new ServiceResponse<TInvoice>();
            try
            {
                TInvoice InvoiceData = _FinanceContext.TInvoices.Where(w => w.DeliveryOrderNo == deliveryOrderId).FirstOrDefault();
                response.Data = InvoiceData;

                if (InvoiceData != null)
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
