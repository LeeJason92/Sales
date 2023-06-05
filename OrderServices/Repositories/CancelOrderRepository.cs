using OrderServices.Context;
using OrderServices.Interfaces;
using OrderServices.Models;
using System.Transactions;

namespace OrderServices.Repositories
{
    public class CancelOrderRepository : ICancelOrder
    {
        private OrderContext _OrderContext;
        private IDeliveryOrder _deliveryOrderRepository;

        public CancelOrderRepository(OrderContext OrderContext, IDeliveryOrder deliveryOrderRepository)
        {
            _OrderContext = OrderContext;
            _deliveryOrderRepository = deliveryOrderRepository;
        }

        public async Task<ServiceResponse<string>> AddCancelOrder(TCancelOrder ICancelOrder)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var checkDo = await _deliveryOrderRepository.GetBySalesOrderNo(ICancelOrder.SalesOrderNo);

                if (checkDo.isSuccess)
                {
                    if (checkDo.Data.Count > 0)
                    {
                        throw new Exception("Can't Cancel Order that Have Been Delivered");
                    }
                    else
                    {
                        await _OrderContext.AddAsync(ICancelOrder);
                        Commit();
                        response.Data = "Save Success";
                        response.Success();
                    }
                }
                else
                {
                    throw new Exception(checkDo.Message);
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
            _OrderContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeleteCancelOrder(int salesOrderNo, int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetBySalesOrderNoId(salesOrderNo, id);
                _OrderContext.TCancelOrders.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditCancelOrder(int salesOrderNo, int id, TCancelOrder TCancelOrderNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetBySalesOrderNoId(salesOrderNo, id);
                TCancelOrder TCancelOrderOld = responseData.Data;
                ModelHelper.CopyCancelOrderProperty(TCancelOrderNew, ref TCancelOrderOld);
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

        public async Task<IQueryable<TCancelOrder>> GetAllCancelOrder()
        {
            await Task.Yield();
            return _OrderContext.TCancelOrders;
        }

        public async Task<ServiceResponse<List<TCancelOrder>>> GetBySalesOrderNo(int salesOrderNo)
        {
            ServiceResponse<List<TCancelOrder>> response = new ServiceResponse<List<TCancelOrder>>();
            try
            {
                List<TCancelOrder> CancelOrderData = _OrderContext.TCancelOrders.Where(w => w.SalesOrderNo == salesOrderNo).ToList();
                response.Data = CancelOrderData;
                response.TotalData = CancelOrderData.Count;

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<TCancelOrder>> GetBySalesOrderNoId(int salesOrderNo, int id)
        {
            ServiceResponse<TCancelOrder> response = new ServiceResponse<TCancelOrder>();
            try
            {
                TCancelOrder CancelOrderData = _OrderContext.TCancelOrders.Where(w => w.SalesOrderNo == salesOrderNo && w.Id == id).FirstOrDefault();
                response.Data = CancelOrderData;

                if (CancelOrderData != null)
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
