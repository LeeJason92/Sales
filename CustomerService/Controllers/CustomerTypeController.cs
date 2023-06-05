using Microsoft.AspNetCore.Mvc;
using CustomerService.Interfaces;
using CustomerService.Models;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private ICustomerType _CustomerTypeRepository;
        private readonly ILogger<CustomerTypeController> _logger;

        public CustomerTypeController(ICustomerType CustomerTypeRepository, ILogger<CustomerTypeController> logger)
        {
            _CustomerTypeRepository = CustomerTypeRepository;
            _logger = logger;
        }

        [HttpGet("all-customertype")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MCustomerType>>>> GetAllCustomerType()
        {
            var response = await _CustomerTypeRepository.GetAllCustomerType();
            return Ok(response);
        }

        // GET api/<CustomerTypeController>/5
        [HttpGet("load-customertype-by-id")]
        public async Task<ActionResult<ServiceResponse<MCustomerType>>> GetById(int id)
        {
            var response = await _CustomerTypeRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-customertype-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-customertype-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // POST api/<CustomerTypeController>
        [HttpPost("add-customertype")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MCustomerType mCustomerType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mCustomerType.CustomerTypeNo != 0)
            {
                response = await _CustomerTypeRepository.AddCustomerType(mCustomerType);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-customertype CustomerTypeCode " + mCustomerType.CustomerTypeNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-customertype CustomerTypeCode " + mCustomerType.CustomerTypeNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : CustomerType Code is Null"));
                _logger.LogError("POST : add-customertype CustomerTypeCode " + mCustomerType.CustomerTypeNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<CustomerTypeController>/5
        [HttpPut("edit-customertype")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] MCustomerType mCustomerType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _CustomerTypeRepository.EditCustomerType(id, mCustomerType);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-customertype CustomerTypeCode " + mCustomerType.CustomerTypeNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-customertype CustomerTypeCode " + mCustomerType.CustomerTypeNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-customertype CustomerTypeCode " + mCustomerType.CustomerTypeNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : CustomerType Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<CustomerTypeController>/5
        [HttpDelete("delete-customertype")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _CustomerTypeRepository.DeleteCustomerType(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-customertype CustomerTypeCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-customertype CustomerTypeCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : CustomerType Code is Null"));
                _logger.LogError("DELETE : delete-customertype CustomerTypeCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}