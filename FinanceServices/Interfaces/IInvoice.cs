using FinanceServices.Models;

namespace FinanceServices.Interfaces
{
    public interface IInvoice
    {
        Task<IQueryable<TInvoice>> GetAllInvoice();
        Task<ServiceResponse<TInvoice>> GetById(int InvoiceId);
        Task<ServiceResponse<TInvoice>> GetByDeliveryOrderId(int deliveryOrderId);
        Task<ServiceResponse<string>> AddInvoice(TInvoice IInvoice);
        Task<ServiceResponse<string>> EditInvoice(int InvoiceId, TInvoice TInvoiceNew);
        Task<ServiceResponse<string>> DeleteInvoice(int InvoiceId);
        void Commit();
    }
}
