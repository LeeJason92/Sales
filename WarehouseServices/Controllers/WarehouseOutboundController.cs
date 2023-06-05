using Microsoft.AspNetCore.Mvc;
using WarehouseServices.Interfaces;
using WarehouseServices.Models;

namespace WarehouseServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseOutboundController : ControllerBase
    {
        private IWarehouseOutbound _WarehouseOutboundRepository;
        private readonly ILogger<WarehouseOutboundController> _logger;

        public WarehouseOutboundController(IWarehouseOutbound WarehouseOutboundRepository, ILogger<WarehouseOutboundController> logger)
        {
            _WarehouseOutboundRepository = WarehouseOutboundRepository;
            _logger = logger;
        }

        [HttpGet("all-warehouseoutbound")]
        public async Task<ActionResult<ServiceResponse<IQueryable<TWarehouseOutbound>>>> GetAllWarehouseOutbound()
        {
            var response = await _WarehouseOutboundRepository.GetAllWarehouse();
            return Ok(response);
        }

        // GET api/<WarehouseOutboundController>/5
        [HttpGet("load-warehouseoutbound-by-id")]
        public async Task<ActionResult<ServiceResponse<TWarehouseOutbound>>> GetById(int id)
        {
            var response = await _WarehouseOutboundRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-warehouseoutbound-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-warehouseoutbound-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<WarehouseOutboundController>
        [HttpPost("add-warehouseoutbound")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] TWarehouseOutbound TWarehouseOutbound)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (TWarehouseOutbound.OutboundNo != 0)
            {
                response = await _WarehouseOutboundRepository.AddWarehouse(TWarehouseOutbound);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-warehouseoutbound WarehouseOutboundCode " + TWarehouseOutbound.OutboundNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-warehouseoutbound WarehouseOutboundCode " + TWarehouseOutbound.OutboundNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : WarehouseOutbound Code is Null"));
                _logger.LogError("POST : add-warehouseoutbound WarehouseOutboundCode " + TWarehouseOutbound.OutboundNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<WarehouseOutboundController>/5
        [HttpPut("edit-warehouseoutbound")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] TWarehouseOutbound TWarehouseOutbound)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _WarehouseOutboundRepository.EditWarehouse(id, TWarehouseOutbound);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-warehouseoutbound WarehouseOutboundCode " + TWarehouseOutbound.OutboundNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-warehouseoutbound WarehouseOutboundCode " + TWarehouseOutbound.OutboundNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-warehouseoutbound WarehouseOutboundCode " + TWarehouseOutbound.OutboundNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : WarehouseOutbound Code is Null"));
                return BadRequest(response);
            }
        }


        // DELETE api/<WarehouseOutboundController>/5
        [HttpDelete("delete-warehouseoutbound")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _WarehouseOutboundRepository.DeleteWarehouse(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-warehouseoutbound WarehouseOutboundCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-warehouseoutbound WarehouseOutboundCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : WarehouseOutbound Code is Null"));
                _logger.LogError("DELETE : delete-warehouseoutbound WarehouseOutboundCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}