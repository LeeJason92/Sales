using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrderServices.Models;

namespace OrderServices.Interfaces
{
    public interface ISalesOrder
    {
        Task<IQueryable<TSalesOrder>> GetAllOrder();
        Task<ServiceResponse<TSalesOrder>> GetById(int OrderId);
        Task<ServiceResponse<List<TSalesOrder>>> GetSalesByBusinessUnit(int businessUnitType);
        Task<ServiceResponse<List<TSalesOrder>>> GetMonthlySalesByBusinessUnit(int businessUnitType, int monthNum, int year);
        Task<ServiceResponse<List<TSalesOrder>>> GetAnnualSalesByBusinessUnit(int businessUnitType, int year);
        Task<ServiceResponse<TSalesOrderRevenueReport>> GetMonthlyRevenueSales(int personaNo, int monthNum, int year);
        Task<ServiceResponse<TSalesOrderRevenueReport>> GetMonthlyRevenueSalesByStore(int personaNo, int storeNo, int monthNum, int year);
        Task<ServiceResponse<TSalesOrderRevenueReport>> GetAnnualRevenueSales(int personaNo, int year);
        Task<ServiceResponse<TSalesOrderRevenueReport>> GetAnnualRevenueSalesByStore(int personaNo, int storeNo, int year);
        Task<ServiceResponse<string>> AddOrder(TSalesOrder tSalesOrder);
        Task<ServiceResponse<string>> EditOrder(int OrderId, TSalesOrder TSalesOrderNew);
        Task<ServiceResponse<string>> DeleteOrder(int OrderId);
        void Commit();
    }
}
