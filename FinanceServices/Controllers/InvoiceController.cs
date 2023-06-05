using Microsoft.AspNetCore.Mvc;
using FinanceServices.Interfaces;
using FinanceServices.Models;

namespace FinanceServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IInvoice _InvoiceRepository;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IInvoice InvoiceRepository, ILogger<InvoiceController> logger)
        {
            _InvoiceRepository = InvoiceRepository;
            _logger = logger;
        }

        [HttpGet("all-invoice")]
        public async Task<ActionResult<ServiceResponse<IQueryable<TInvoice>>>> GetAllInvoice()
        {
            var response = await _InvoiceRepository.GetAllInvoice();
            return Ok(response);
        }

        // GET api/<InvoiceController>/5
        [HttpGet("load-invoice-by-id")]
        public async Task<ActionResult<ServiceResponse<TInvoice>>> GetById(int id)
        {
            var response = await _InvoiceRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-invoice-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-invoice-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-invoice-by-delivery-id")]
        public async Task<ActionResult<ServiceResponse<TInvoice>>> GetByDeliveryOrderId(int deliveryOrderId)
        {
            var response = await _InvoiceRepository.GetByDeliveryOrderId(deliveryOrderId);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-invoice-by-delivery-id " + deliveryOrderId + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-invoice-by-delivery-id " + deliveryOrderId + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<InvoiceController>
        [HttpPost("add-invoice")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] TInvoice TInvoice)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (TInvoice.InvoiceNo != 0)
            {
                response = await _InvoiceRepository.AddInvoice(TInvoice);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-invoice InvoiceCode " + TInvoice.InvoiceNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-invoice InvoiceCode " + TInvoice.InvoiceNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Invoice Code is Null"));
                _logger.LogError("POST : add-invoice InvoiceCode " + TInvoice.InvoiceNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<InvoiceController>/5
        [HttpPut("edit-invoice")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] TInvoice TInvoice)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _InvoiceRepository.EditInvoice(id, TInvoice);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-invoice InvoiceCode " + TInvoice.InvoiceNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-invoice InvoiceCode " + TInvoice.InvoiceNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-invoice InvoiceCode " + TInvoice.InvoiceNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : Invoice Code is Null"));
                return BadRequest(response);
            }
        }


        // DELETE api/<InvoiceController>/5
        [HttpDelete("delete-invoice")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _InvoiceRepository.DeleteInvoice(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-invoice InvoiceCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-invoice InvoiceCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Invoice Code is Null"));
                _logger.LogError("DELETE : delete-invoice InvoiceCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}