using OrderServices.Context;
using OrderServices.Interfaces;
using OrderServices.Models;
using System.Transactions;

namespace OrderServices.Repositories
{
    public class OrderDetailRepository : IOrderDetail
    {
        private OrderContext _OrderContext;

        public OrderDetailRepository(OrderContext OrderContext)
        {
            _OrderContext = OrderContext;
        }

        public async Task<ServiceResponse<List<VOrderDetail>>> GetAllOrderDetails(int personaNo)
        {
            ServiceResponse<List<VOrderDetail>> response = new ServiceResponse<List<VOrderDetail>>();

            try
            {
                if(personaNo == 4 || personaNo == 5) 
                {
                    await Task.Yield();
                    response.Data = _OrderContext.VOrderDetails.ToList();
                    response.TotalData = response.Data.Count;
                    response.Success();
                }
                else
                {
                    throw new Exception("Unauthorized Persona");
                }
            }
            catch (Exception ex) 
            {
                response.Fail(ex);
            }

            return response;
        }

        public async Task<ServiceResponse<List<VOrderDetail>>> GetByStatus(int personaNo, string status)
        {
            ServiceResponse<List<VOrderDetail>> response = new ServiceResponse<List<VOrderDetail>>();
            try
            {
                if(personaNo == 4 || personaNo == 5)
                {
                    List<VOrderDetail> OrderData = _OrderContext.VOrderDetails.Where(w => w.StatusOrder.Trim().ToLower().Contains(status.Trim().ToLower())).ToList();
                    response.Data = OrderData;
                    response.TotalData = OrderData.Count;

                    await Task.Yield();
                    response.Success();
                }
                else
                {
                    throw new Exception("Unauthorized Persona");
                }
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }
    }
}
