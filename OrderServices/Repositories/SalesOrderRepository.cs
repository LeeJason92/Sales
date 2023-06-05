using OrderServices.Context;
using OrderServices.Interfaces;
using OrderServices.Models;
using System.Transactions;

namespace OrderServices.Repositories
{
    public class SalesOrderRepository : ISalesOrder
    {
        private OrderContext _OrderContext;
        private IDeliveryOrder _deliveryOrderRepository;

        public SalesOrderRepository(OrderContext OrderContext, IDeliveryOrder deliveryOrderRepository)
        {
            _OrderContext = OrderContext;
            _deliveryOrderRepository = deliveryOrderRepository;
        }

        //asumsi : quantity pada sales order telah disesuaikan dengan sisa stok pada master produk (pada UI / aplikasi), sehingga tidak mungkin
        //ada request api dengan quantity melebihi sisa stok produk
        public async Task<ServiceResponse<string>> AddOrder(TSalesOrder salesOrder)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _OrderContext.AddAsync(salesOrder);
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

        public async Task<ServiceResponse<string>> DeleteOrder(int OrderId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(OrderId);

                var checkDo = await _deliveryOrderRepository.GetBySalesOrderNo(OrderId);

                if(responseData.isSuccess && checkDo.isSuccess)
                {
                    if(checkDo.Data.Count > 0)
                    {
                        throw new Exception("Can't Delete Order that Have Been Delivered");
                    }
                    else
                    {
                        _OrderContext.TSalesOrders.Remove(responseData.Data);
                        Commit();
                        response.Data = "Delete Success";
                        response.Success();
                    }
                }
                else 
                {
                    if(responseData.isSuccess)
                    {
                        throw new Exception(checkDo.Message);
                    }
                    else
                    {
                        throw new Exception(responseData.Message);
                    }
                }

            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<string>> EditOrder(int OrderId, TSalesOrder TSalesOrderNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(OrderId);

                var checkDo = await _deliveryOrderRepository.GetBySalesOrderNo(OrderId);

                if (responseData.isSuccess && checkDo.isSuccess)
                {
                    if (checkDo.Data.Count > 0)
                    {
                        throw new Exception("Can't Update Order that Have Been Delivered");
                    }
                    else
                    {
                        TSalesOrder TSalesOrderOld = responseData.Data;
                        ModelHelper.CopySalesOrderProperty(TSalesOrderNew, ref TSalesOrderOld);
                        Commit();
                        response.Data = "Edit Success";
                        response.Success();
                    }
                }
                else
                {
                    if (responseData.isSuccess)
                    {
                        throw new Exception(checkDo.Message);
                    }
                    else
                    {
                        throw new Exception(responseData.Message);
                    }
                }
            }
            catch (Exception e)
            {
                response.Fail(e);
            }
            await Task.Yield();

            return response;
        }

        public async Task<IQueryable<TSalesOrder>> GetAllOrder()
        {
            await Task.Yield();
            return _OrderContext.TSalesOrders;
        }

        public async Task<ServiceResponse<TSalesOrder>> GetById(int OrderId)
        {
            ServiceResponse<TSalesOrder> response = new ServiceResponse<TSalesOrder>();
            try
            {
                TSalesOrder OrderData = _OrderContext.TSalesOrders.Where(w => w.SalesOrderNo == OrderId).FirstOrDefault();
                response.Data = OrderData;

                if(OrderData != null) 
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

        public async Task<ServiceResponse<List<TSalesOrder>>> GetSalesByBusinessUnit(int businessUnitType)
        {
            ServiceResponse<List<TSalesOrder>> response = new ServiceResponse<List<TSalesOrder>>();
            try
            {
                List<TSalesOrder> OrderData = _OrderContext.TSalesOrders.Where(w => w.BusinessUnitNo == businessUnitType).ToList();
                response.Data = OrderData;
                response.TotalData = OrderData.Count;

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<List<TSalesOrder>>> GetMonthlySalesByBusinessUnit(int businessUnitType, int monthNum, int year)
        {
            ServiceResponse<List<TSalesOrder>> response = new ServiceResponse<List<TSalesOrder>>();
            try
            {
                List<TSalesOrder> OrderData = _OrderContext.TSalesOrders.Where(w => w.BusinessUnitNo == businessUnitType && w.SalesOrderDate.Month == monthNum && w.SalesOrderDate.Year == year).ToList();
                response.Data = OrderData;
                response.TotalData = OrderData.Count;

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<List<TSalesOrder>>> GetAnnualSalesByBusinessUnit(int businessUnitType, int year)
        {
            ServiceResponse<List<TSalesOrder>> response = new ServiceResponse<List<TSalesOrder>>();
            try
            {
                List<TSalesOrder> OrderData = _OrderContext.TSalesOrders.Where(w => w.BusinessUnitNo == businessUnitType && w.SalesOrderDate.Year == year).ToList();
                response.Data = OrderData;
                response.TotalData = OrderData.Count;

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<TSalesOrderRevenueReport>> GetMonthlyRevenueSales(int personaNo, int monthNum, int year)
        {
            ServiceResponse<TSalesOrderRevenueReport> response = new ServiceResponse<TSalesOrderRevenueReport>();
            try
            {
                if(personaNo == 4 || personaNo == 5) //managers
                {
                    List<TSalesOrder> OrderData = _OrderContext.TSalesOrders.Where(w => w.SalesOrderDate.Month == monthNum 
                                                    && w.SalesOrderDate.Year == year && !_OrderContext.TCancelOrders.Any(c => c.SalesOrderNo == w.SalesOrderNo)).ToList();

                    double totalQty = 0;
                    double grandTotalPrice = 0;

                    foreach (TSalesOrder item in OrderData)
                    {
                        double totalPrice = item.Quantity * item.PricePerUnit;
                        double discount = item.DiscountRate * totalPrice;
                        item.TotalPrice = totalPrice - discount;
                        totalQty += item.Quantity;
                        grandTotalPrice += item.TotalPrice;
                    }

                    TSalesOrderRevenueReport reportData = new TSalesOrderRevenueReport();
                    reportData.ListSalesOrder = OrderData;
                    reportData.GrandTotalQuantity = totalQty;
                    reportData.GrandTotalSalesOrder = grandTotalPrice;

                    response.Data = reportData;
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

        public async Task<ServiceResponse<TSalesOrderRevenueReport>> GetAnnualRevenueSales(int personaNo, int year)
        {
            ServiceResponse<TSalesOrderRevenueReport> response = new ServiceResponse<TSalesOrderRevenueReport>();
            try
            {
                if(personaNo == 4 || personaNo == 5)
                {
                    List<TSalesOrder> OrderData = _OrderContext.TSalesOrders.Where(w => w.SalesOrderDate.Year == year && !_OrderContext.TCancelOrders.Any(c => c.SalesOrderNo == w.SalesOrderNo)).ToList();
                    double totalQty = 0;
                    double grandTotalPrice = 0;

                    foreach (TSalesOrder item in OrderData)
                    {
                        double totalPrice = item.Quantity * item.PricePerUnit;
                        double discount = item.DiscountRate * totalPrice;
                        item.TotalPrice = totalPrice - discount;
                        totalQty += item.Quantity;
                        grandTotalPrice += item.TotalPrice;
                    }

                    TSalesOrderRevenueReport reportData = new TSalesOrderRevenueReport();
                    reportData.ListSalesOrder = OrderData;
                    reportData.GrandTotalQuantity = totalQty;
                    reportData.GrandTotalSalesOrder = grandTotalPrice;

                    response.Data = reportData;
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

        public async Task<ServiceResponse<TSalesOrderRevenueReport>> GetMonthlyRevenueSalesByStore(int personaNo, int storeNo, int monthNum, int year)
        {
            ServiceResponse<TSalesOrderRevenueReport> response = new ServiceResponse<TSalesOrderRevenueReport>();
            try
            {
                if (personaNo == 4 || personaNo == 5) //managers
                {
                    List<TSalesOrder> OrderData = _OrderContext.TSalesOrders.Where(w => w.SalesOrderDate.Month == monthNum && w.StoreNo == storeNo
                                                    && w.SalesOrderDate.Year == year && !_OrderContext.TCancelOrders.Any(c => c.SalesOrderNo == w.SalesOrderNo)).ToList();

                    double totalQty = 0;
                    double grandTotalPrice = 0;

                    foreach (TSalesOrder item in OrderData)
                    {
                        double totalPrice = item.Quantity * item.PricePerUnit;
                        double discount = item.DiscountRate * totalPrice;
                        item.TotalPrice = totalPrice - discount;
                        totalQty += item.Quantity;
                        grandTotalPrice += item.TotalPrice;
                    }

                    TSalesOrderRevenueReport reportData = new TSalesOrderRevenueReport();
                    reportData.ListSalesOrder = OrderData;
                    reportData.GrandTotalQuantity = totalQty;
                    reportData.GrandTotalSalesOrder = grandTotalPrice;

                    response.Data = reportData;
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

        public async Task<ServiceResponse<TSalesOrderRevenueReport>> GetAnnualRevenueSalesByStore(int personaNo, int storeNo, int year)
        {
            ServiceResponse<TSalesOrderRevenueReport> response = new ServiceResponse<TSalesOrderRevenueReport>();
            try
            {
                if (personaNo == 4 || personaNo == 5)
                {
                    List<TSalesOrder> OrderData = _OrderContext.TSalesOrders.Where(w => w.SalesOrderDate.Year == year && w.StoreNo == storeNo
                                                    && !_OrderContext.TCancelOrders.Any(c => c.SalesOrderNo == w.SalesOrderNo)).ToList();
                    double totalQty = 0;
                    double grandTotalPrice = 0;

                    foreach (TSalesOrder item in OrderData)
                    {
                        double totalPrice = item.Quantity * item.PricePerUnit;
                        double discount = item.DiscountRate * totalPrice;
                        item.TotalPrice = totalPrice - discount;
                        totalQty += item.Quantity;
                        grandTotalPrice += item.TotalPrice;
                    }

                    TSalesOrderRevenueReport reportData = new TSalesOrderRevenueReport();
                    reportData.ListSalesOrder = OrderData;
                    reportData.GrandTotalQuantity = totalQty;
                    reportData.GrandTotalSalesOrder = grandTotalPrice;

                    response.Data = reportData;
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
