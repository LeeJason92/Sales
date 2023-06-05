using WarehouseServices.Models;

namespace WarehouseServices.Interfaces
{
    public interface IWarehouseOutbound
    {
        Task<IQueryable<TWarehouseOutbound>> GetAllWarehouse();
        Task<ServiceResponse<TWarehouseOutbound>> GetById(int WarehouseId);
        Task<ServiceResponse<List<TWarehouseOutbound>>> GetBySalesOrderNo(int salesOrderNo);
        Task<ServiceResponse<SalesOrderModel>> GetSalesOrderData(int salesOrderNo);
        Task<ServiceResponse<string>> AddWarehouse(TWarehouseOutbound IWarehouse);
        Task<ServiceResponse<string>> EditWarehouse(int WarehouseId, TWarehouseOutbound TWarehouseOutboundNew);
        Task<ServiceResponse<string>> DeleteWarehouse(int WarehouseId);
        void Commit();
    }
}
