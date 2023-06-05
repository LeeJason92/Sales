using Microsoft.AspNetCore.Mvc;
using CustomerService.Interfaces;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer _CustomerRepository;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomer CustomerRepository, ILogger<CustomerController> logger)
        {
            _CustomerRepository = CustomerRepository;
            _logger = logger;
        }

        [HttpGet("all-customer")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MCustomer>>>> GetAllCustomer()
        {
            var response = await _CustomerRepository.GetAllCustomer();
            return Ok(response);
        }

        // GET api/<CustomerController>/5
        [HttpGet("load-customer-by-id")]
        public async Task<ActionResult<ServiceResponse<MCustomer>>> GetById(int id)
        {
            var response = await _CustomerRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-customer-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-customer-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-customer-by-name")]
        public async Task<ActionResult<ServiceResponse<MCustomer>>> GetByName(string keyword)
        {
            var response = await _CustomerRepository.GetByName(keyword);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-customer-by-name " + keyword + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-customer-by-name " + keyword + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<CustomerController>
        [HttpPost("add-customer")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MCustomer mCustomer)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mCustomer.CustomerNo != 0)
            {
                response = await _CustomerRepository.AddCustomer(mCustomer);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-customer CustomerCode " + mCustomer.CustomerNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-customer CustomerCode " + mCustomer.CustomerNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Customer Code is Null"));
                _logger.LogError("POST : add-customer CustomerCode " + mCustomer.CustomerNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<CustomerController>/5
        [HttpPut("edit-customer")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] MCustomer mCustomer)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _CustomerRepository.EditCustomer(id, mCustomer);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-customer CustomerCode " + mCustomer.CustomerNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-customer CustomerCode " + mCustomer.CustomerNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-customer CustomerCode " + mCustomer.CustomerNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : Customer Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("delete-customer")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _CustomerRepository.DeleteCustomer(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-customer CustomerCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-customer CustomerCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Customer Code is Null"));
                _logger.LogError("DELETE : delete-customer CustomerCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}