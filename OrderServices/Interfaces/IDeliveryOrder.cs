using OrderServices.Models;

namespace OrderServices.Interfaces
{
    public interface IDeliveryOrder
    {
        Task<IQueryable<TDeliveryOrder>> GetAllDeliveryOrder();
        Task<ServiceResponse<TDeliveryOrder>> GetById(int DeliveryOrderId);
        Task<ServiceResponse<List<TDeliveryOrder>>> GetBySalesOrderNo(int salesOrderNo);
        Task<ServiceResponse<string>> AddDeliveryOrder(TDeliveryOrder IDeliveryOrder);
        Task<ServiceResponse<string>> EditDeliveryOrder(int DeliveryOrderId, TDeliveryOrder TDeliveryOrderNew);
        Task<ServiceResponse<string>> DeleteDeliveryOrder(int DeliveryOrderId);
        void Commit();
    }
}
