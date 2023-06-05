using OrderServices.Context;
using OrderServices.Interfaces;
using OrderServices.Models;
using System.Transactions;

namespace OrderServices.Repositories
{
    public class DeliveryOrderRepository : IDeliveryOrder
    {
        private OrderContext _OrderContext;

        public DeliveryOrderRepository(OrderContext OrderContext)
        {
            _OrderContext = OrderContext;
        }

        public async Task<ServiceResponse<string>> AddDeliveryOrder(TDeliveryOrder IDeliveryOrder)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _OrderContext.AddAsync(IDeliveryOrder);
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
            _OrderContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeleteDeliveryOrder(int DeliveryOrderId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(DeliveryOrderId);
                _OrderContext.TDeliveryOrders.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditDeliveryOrder(int DeliveryOrderId, TDeliveryOrder TDeliveryOrderNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(DeliveryOrderId);
                TDeliveryOrder TDeliveryOrderOld = responseData.Data;
                ModelHelper.CopyDeliveryOrderProperty(TDeliveryOrderNew, ref TDeliveryOrderOld);
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

        public async Task<IQueryable<TDeliveryOrder>> GetAllDeliveryOrder()
        {
            await Task.Yield();
            return _OrderContext.TDeliveryOrders;
        }

        public async Task<ServiceResponse<TDeliveryOrder>> GetById(int DeliveryOrderId)
        {
            ServiceResponse<TDeliveryOrder> response = new ServiceResponse<TDeliveryOrder>();
            try
            {
                TDeliveryOrder DeliveryOrderData = _OrderContext.TDeliveryOrders.Where(w => w.DeliveryOrderNo == DeliveryOrderId).FirstOrDefault();
                response.Data = DeliveryOrderData;

                if (DeliveryOrderData != null)
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

        public async Task<ServiceResponse<List<TDeliveryOrder>>> GetBySalesOrderNo(int salesOrderNo)
        {
            ServiceResponse<List<TDeliveryOrder>> response = new ServiceResponse<List<TDeliveryOrder>>();
            try
            {
                List<TDeliveryOrder> DeliveryOrderData = _OrderContext.TDeliveryOrders.Where(w => w.SalesOrderNo == salesOrderNo).ToList();
                response.Data = DeliveryOrderData;
                response.TotalData = DeliveryOrderData.Count;

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
