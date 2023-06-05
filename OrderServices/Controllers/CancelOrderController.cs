using Microsoft.AspNetCore.Mvc;
using OrderServices.Interfaces;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelOrderController : ControllerBase
    {
        private ICancelOrder _CancelOrderRepository;
        private readonly ILogger<CancelOrderController> _logger;

        public CancelOrderController(ICancelOrder CancelOrderRepository, ILogger<CancelOrderController> logger)
        {
            _CancelOrderRepository = CancelOrderRepository;
            _logger = logger;
        }

        [HttpGet("all-cancelorder")]
        public async Task<ActionResult<ServiceResponse<IQueryable<TCancelOrder>>>> GetAllCancelOrder()
        {
            var response = await _CancelOrderRepository.GetAllCancelOrder();
            return Ok(response);
        }

        // GET api/<CancelOrderController>/5
        [HttpGet("load-cancelorder-by-sales-order-no")]
        public async Task<ActionResult<ServiceResponse<TCancelOrder>>> GetBySalesOrderNo(int salesOrderNo)
        {
            var response = await _CancelOrderRepository.GetBySalesOrderNo(salesOrderNo);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-cancelorder-by-sales-order-no " + salesOrderNo + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-cancelorder-by-sales-order-no " + salesOrderNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-cancelorder-by-sales-order-no-id")]
        public async Task<ActionResult<ServiceResponse<List<TCancelOrder>>>> GetBySalesOrderNoId(int salesOrderNo, int id)
        {
            var response = await _CancelOrderRepository.GetBySalesOrderNoId(salesOrderNo,id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-cancelorder-by-sales-order-no-id Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-cancelorder-by-sales-order-no-id Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<CancelOrderController>
        [HttpPost("add-cancelorder")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] TCancelOrder TCancelOrder)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (TCancelOrder.SalesOrderNo != 0)
            {
                response = await _CancelOrderRepository.AddCancelOrder(TCancelOrder);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-cancelorder CancelOrderCode " + TCancelOrder.SalesOrderNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-cancelorder CancelOrderCode " + TCancelOrder.SalesOrderNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : CancelOrder Code is Null"));
                _logger.LogError("POST : add-cancelorder CancelOrderCode " + TCancelOrder.SalesOrderNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<CancelOrderController>/5
        [HttpPut("edit-cancelorder")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int salesOrderNo, int id, [FromBody] TCancelOrder TCancelOrder)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (salesOrderNo != 0)
            {
                response = await _CancelOrderRepository.EditCancelOrder(salesOrderNo, id, TCancelOrder);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-cancelorder CancelOrderCode " + TCancelOrder.SalesOrderNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-cancelorder CancelOrderCode " + TCancelOrder.SalesOrderNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-cancelorder CancelOrderCode " + TCancelOrder.SalesOrderNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : CancelOrder Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<CancelOrderController>/5
        [HttpDelete("delete-cancelorder")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int salesOrderNo, int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (salesOrderNo != 0)
            {
                response = await _CancelOrderRepository.DeleteCancelOrder(salesOrderNo,id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-cancelorder CancelOrderCode " + salesOrderNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-cancelorder CancelOrderCode " + salesOrderNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : CancelOrder Code is Null"));
                _logger.LogError("DELETE : delete-cancelorder CancelOrderCode " + salesOrderNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}