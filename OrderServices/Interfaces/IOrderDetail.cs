using Microsoft.Data.SqlClient.DataClassification;
using OrderServices.Models;

namespace OrderServices.Interfaces
{
    public interface IOrderDetail
    {
        Task<ServiceResponse<List<VOrderDetail>>> GetAllOrderDetails(int personaNo);
        Task<ServiceResponse<List<VOrderDetail>>> GetByStatus(int personaNo, string status);
    }
}
