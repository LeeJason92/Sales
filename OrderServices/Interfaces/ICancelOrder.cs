using OrderServices.Models;

namespace OrderServices.Interfaces
{
    public interface ICancelOrder
    {
        Task<IQueryable<TCancelOrder>> GetAllCancelOrder();
        Task<ServiceResponse<List<TCancelOrder>>> GetBySalesOrderNo(int salesOrderNo);
        Task<ServiceResponse<TCancelOrder>> GetBySalesOrderNoId(int salesOrderNo, int id);
        Task<ServiceResponse<string>> AddCancelOrder(TCancelOrder ICancelOrder);
        Task<ServiceResponse<string>> EditCancelOrder(int salesOrderNo, int id, TCancelOrder TCancelOrderNew);
        Task<ServiceResponse<string>> DeleteCancelOrder(int salesOrderNo, int id);
        void Commit();
    }
}
