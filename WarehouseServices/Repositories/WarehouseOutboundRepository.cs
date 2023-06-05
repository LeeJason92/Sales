using WarehouseServices.Context;
using WarehouseServices.Interfaces;
using WarehouseServices.Models;
using System.Transactions;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace WarehouseServices.Repositories
{
    public class WarehouseOutboundRepository : IWarehouseOutbound
    {
        private WarehouseContext _WarehouseContext;
        private string SalesOrderApiURL = "http://localhost:5259/api/SalesOrder/load-salesorder-by-id?id=";

        public WarehouseOutboundRepository(WarehouseContext WarehouseContext)
        {
            _WarehouseContext = WarehouseContext;
        }

        public async Task<ServiceResponse<SalesOrderModel>> GetSalesOrderData(int salesOrderNo)
        {
            ServiceResponse<SalesOrderModel> responseData = new ServiceResponse<SalesOrderModel>();
            try
            {
                ServiceResponse<SalesOrderModel> responseApi = new ServiceResponse<SalesOrderModel>();

                HttpResponseMessage httpResponseMessage;
                using (HttpClient httpClient = new HttpClient())
                {
                    string apiURL = SalesOrderApiURL + salesOrderNo;
                    httpResponseMessage = await httpClient.GetAsync(apiURL);
                    responseApi = JsonConvert.DeserializeObject<ServiceResponse<SalesOrderModel>>(await httpResponseMessage.Content.ReadAsStringAsync());

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        //Map Service Response Data
                        responseData.CurrentPage = responseApi.CurrentPage;
                        responseData.DataPerPage = responseApi.DataPerPage;
                        responseData.FilterBy = responseApi.FilterBy;
                        responseData.isSuccess = responseApi.isSuccess;
                        responseData.Message = responseApi.Message;
                        responseData.OrderBy = responseApi.OrderBy;
                        responseData.StatusCode = responseApi.StatusCode;
                        responseData.TotalData = responseApi.TotalData;
                        responseData.TotalPage = responseApi.TotalPage;

                        responseData.Data = responseApi.Data;
                    }
                    else
                    {
                        responseData.Fail(new Exception(httpResponseMessage.StatusCode + " " + responseApi.Message));
                    }
                }
            }
            catch (Exception e)
            {
                responseData.Fail(new Exception(e.Message));
            }

            return responseData;
        }

        public async Task<ServiceResponse<string>> AddWarehouse(TWarehouseOutbound IWarehouse)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                if(IWarehouse != null)
                {
                    if(IWarehouse.SalesOrderNo != 0)
                    {
                        var salesOrderDataResponse = await GetSalesOrderData(IWarehouse.SalesOrderNo);
                        if (salesOrderDataResponse.isSuccess)
                        {
                            if (salesOrderDataResponse.Data != null)
                            {
                                var qty = salesOrderDataResponse.Data.Quantity;
                                var currentDataResponse = await GetBySalesOrderNo(IWarehouse.SalesOrderNo);
                                if (currentDataResponse.isSuccess)
                                {
                                    if(currentDataResponse.Data.Count > 0)
                                    {
                                        var currentQty = 0;
                                        foreach(TWarehouseOutbound item in currentDataResponse.Data)
                                        {
                                            currentQty += item.Quantity;
                                        }

                                        if(qty > currentQty)
                                        {
                                            if((currentQty + IWarehouse.Quantity) > qty)
                                            {
                                                throw new Exception("Quantity Achieved");
                                            }
                                            else
                                            {
                                                await _WarehouseContext.AddAsync(IWarehouse);
                                                Commit();
                                                response.Data = "Save Success";
                                                response.Success();
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Quantity Achieved");
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception(currentDataResponse.Message);
                                }
                            }
                            else
                            {
                                throw new Exception("Sales Order Data Not Found");
                            }
                        }
                        else
                        {
                            throw new Exception(salesOrderDataResponse.Message);
                        }
                    }
                    else
                    {
                        throw new Exception("Sales Order Data Not Found");
                    }
                }
                else
                {
                    throw new Exception("Warehouse Outbound Data Not Found");
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
            _WarehouseContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeleteWarehouse(int WarehouseId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(WarehouseId);
                _WarehouseContext.TWarehouseOutbounds.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditWarehouse(int WarehouseId, TWarehouseOutbound TWarehouseOutboundNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(WarehouseId);
                TWarehouseOutbound TWarehouseOutboundOld = responseData.Data;
                ModelHelper.CopyProperty(TWarehouseOutboundNew, ref TWarehouseOutboundOld);
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

        public async Task<IQueryable<TWarehouseOutbound>> GetAllWarehouse()
        {
            await Task.Yield();
            return _WarehouseContext.TWarehouseOutbounds;
        }

        public async Task<ServiceResponse<TWarehouseOutbound>> GetById(int WarehouseId)
        {
            ServiceResponse<TWarehouseOutbound> response = new ServiceResponse<TWarehouseOutbound>();
            try
            {
                TWarehouseOutbound WarehouseData = _WarehouseContext.TWarehouseOutbounds.Where(w => w.OutboundNo == WarehouseId).FirstOrDefault();
                response.Data = WarehouseData;

                if (WarehouseData != null)
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

        public async Task<ServiceResponse<List<TWarehouseOutbound>>> GetBySalesOrderNo(int salesOrderNo)
        {
            ServiceResponse<List<TWarehouseOutbound>> response = new ServiceResponse<List<TWarehouseOutbound>>();
            try
            {
                List<TWarehouseOutbound> WarehouseData = _WarehouseContext.TWarehouseOutbounds.Where(w => w.SalesOrderNo == salesOrderNo).ToList();
                response.Data = WarehouseData;
                response.TotalData = WarehouseData.Count;

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
