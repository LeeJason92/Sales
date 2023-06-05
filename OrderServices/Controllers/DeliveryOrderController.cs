using Microsoft.AspNetCore.Mvc;
using OrderServices.Interfaces;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryOrderController : ControllerBase
    {
        private IDeliveryOrder _DeliveryOrderRepository;
        private readonly ILogger<DeliveryOrderController> _logger;

        public DeliveryOrderController(IDeliveryOrder DeliveryOrderRepository, ILogger<DeliveryOrderController> logger)
        {
            _DeliveryOrderRepository = DeliveryOrderRepository;
            _logger = logger;
        }

        [HttpGet("all-deliveryorder")]
        public async Task<ActionResult<ServiceResponse<IQueryable<TDeliveryOrder>>>> GetAllDeliveryOrder()
        {
            var response = await _DeliveryOrderRepository.GetAllDeliveryOrder();
            return Ok(response);
        }

        // GET api/<DeliveryOrderController>/5
        [HttpGet("load-deliveryorder-by-id")]
        public async Task<ActionResult<ServiceResponse<TDeliveryOrder>>> GetByDeliveryOrderNo(int id)
        {
            var response = await _DeliveryOrderRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-deliveryorder-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-deliveryorder-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-deliveryorder-by-sales-order-no")]
        public async Task<ActionResult<ServiceResponse<List<TDeliveryOrder>>>> GetBySalesOrderNo(int salesOrderNo)
        {
            var response = await _DeliveryOrderRepository.GetBySalesOrderNo(salesOrderNo);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-deliveryorder-by-sales-order-no Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-deliveryorder-by-sales-order-no Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<DeliveryOrderController>
        [HttpPost("add-deliveryorder")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] TDeliveryOrder TDeliveryOrder)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (TDeliveryOrder.DeliveryOrderNo != 0)
            {
                response = await _DeliveryOrderRepository.AddDeliveryOrder(TDeliveryOrder);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-deliveryorder DeliveryOrderCode " + TDeliveryOrder.DeliveryOrderNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-deliveryorder DeliveryOrderCode " + TDeliveryOrder.DeliveryOrderNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : DeliveryOrder Code is Null"));
                _logger.LogError("POST : add-deliveryorder DeliveryOrderCode " + TDeliveryOrder.DeliveryOrderNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<DeliveryOrderController>/5
        [HttpPut("edit-deliveryorder")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] TDeliveryOrder TDeliveryOrder)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _DeliveryOrderRepository.EditDeliveryOrder(id, TDeliveryOrder);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-deliveryorder DeliveryOrderCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-deliveryorder DeliveryOrderCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-deliveryorder DeliveryOrderCode " + id + " Error : " + response.Message);
                response.Fail(new Exception("Error : DeliveryOrder Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<DeliveryOrderController>/5
        [HttpDelete("delete-deliveryorder")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _DeliveryOrderRepository.DeleteDeliveryOrder(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-deliveryorder DeliveryOrderCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-deliveryorder DeliveryOrderCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : DeliveryOrder Code is Null"));
                _logger.LogError("DELETE : delete-deliveryorder DeliveryOrderCode " + id + " Error : " + response.Message);
                return BadRequest(response);    
            }
        }
    }
}