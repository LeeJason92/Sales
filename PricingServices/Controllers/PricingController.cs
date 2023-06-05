using Microsoft.AspNetCore.Mvc;
using PricingServices.Interfaces;
using PricingServices.Models;

namespace PricingServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private IPricing _PricingRepository;
        private readonly ILogger<PricingController> _logger;

        public PricingController(IPricing PricingRepository, ILogger<PricingController> logger)
        {
            _PricingRepository = PricingRepository;
            _logger = logger;
        }

        [HttpGet("all-pricing")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MPricing>>>> GetAllPricing()
        {
            var response = await _PricingRepository.GetAllPricing();
            return Ok(response);
        }

        // GET api/<PricingController>/5
        [HttpGet("load-pricing-by-pricing-id")]
        public async Task<ActionResult<ServiceResponse<MPricing>>> GetByPricingId(int pricingId)
        {
            var response = await _PricingRepository.GetByPricingId(pricingId);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-pricing-by-pricing-id " + pricingId + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-pricing-by-pricing-id " + pricingId + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // GET api/<PricingController>/5
        [HttpGet("load-pricing-by-product-id")]
        public async Task<ActionResult<ServiceResponse<MPricing>>> GetByProductId(int productId)
        {
            var response = await _PricingRepository.GetByProductId(productId);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-pricing-by-product-id " + productId + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-pricing-by-product-id " + productId + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-pricing-by-store-id")]
        public async Task<ActionResult<ServiceResponse<MPricing>>> GetByStoreId(int storeId)
        {
            var response = await _PricingRepository.GetByStoreId(storeId);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-pricing-by-store-id " + storeId + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-pricing-by-store-id " + storeId + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-pricing-by-customer-type-id")]
        public async Task<ActionResult<ServiceResponse<MPricing>>> GetByCustomerTypeId(int customerTypeId)
        {
            var response = await _PricingRepository.GetByCustomerType(customerTypeId);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-pricing-by-customer-type-id " + customerTypeId + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-pricing-by-customer-type-id " + customerTypeId + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<PricingController>
        [HttpPost("add-pricing")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MPricing mPricing)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mPricing.ProductNo != 0)
            {
                response = await _PricingRepository.AddPricing(mPricing);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-pricing ProductCode " + mPricing.ProductNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-pricing ProductCode " + mPricing.ProductNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Pricing Code is Null"));
                _logger.LogError("POST : add-pricing ProductCode " + mPricing.ProductNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<PricingController>/5
        [HttpPut("edit-pricing")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int productId, int storeId, int customerTypeId, DateTime validFrom, DateTime validTo, [FromBody] MPricing mPricing)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (productId != 0 && storeId != 0 && customerTypeId != 0)
            {
                response = await _PricingRepository.EditPricing(productId, storeId, customerTypeId, validFrom, validTo, mPricing);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-pricing ProductCode " + mPricing.ProductNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-pricing ProductCode " + mPricing.ProductNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-pricing ProductCode " + mPricing.ProductNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : Pricing Code is Null"));
                return BadRequest(response);
            }
        }

        [HttpPut("edit-pricing-by-id")]
        public async Task<ActionResult<ServiceResponse<string>>> PutById(int pricingId, [FromBody] MPricing mPricing)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (pricingId != 0)
            {
                response = await _PricingRepository.EditByPricingId(pricingId, mPricing);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-product-by-id PricingId " + mPricing.Id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-product-by-id PricingId " + mPricing.Id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-product-by-id PricingId " + mPricing.Id + " Error : " + response.Message);
                response.Fail(new Exception("Error : Pricing Id is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<PricingController>/5
        [HttpDelete("delete-pricing")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int productId, int storeId, int customerTypeId, DateTime validFrom, DateTime validTo)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (productId != 0 && storeId != 0 && customerTypeId != 0)
            {
                response = await _PricingRepository.DeletePricing(productId, storeId, customerTypeId, validFrom, validTo);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-pricing ProductCode " + productId + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-pricing ProductCode " + productId + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Pricing Code is Null"));
                _logger.LogError("DELETE : delete-pricing ProductCode " + productId + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpDelete("delete-pricing-by-id")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteById(int pricingId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (pricingId != 0)
            {
                response = await _PricingRepository.DeleteByPricingId(pricingId);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : delete-product-by-id PricingId " + pricingId + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : delete-product-by-id PricingId " + pricingId + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : deletes-product-by-id PricingId " + pricingId + " Error : " + response.Message);
                response.Fail(new Exception("Error : Pricing Id is Null"));
                return BadRequest(response);
            }
        }
    }
}