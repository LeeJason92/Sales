using Microsoft.AspNetCore.Mvc;
using ProductServices.Interfaces;
using ProductServices.Models;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private IProductType _ProductTypeRepository;
        private readonly ILogger<ProductTypeController> _logger;

        public ProductTypeController(IProductType ProductTypeRepository, ILogger<ProductTypeController> logger)
        {
            _ProductTypeRepository = ProductTypeRepository;
            _logger = logger;
        }

        [HttpGet("all-producttype")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MProductType>>>> GetAllProductType()
        {
            var response = await _ProductTypeRepository.GetAllProductType();
            return Ok(response);
        }

        // GET api/<ProductTypeController>/5
        [HttpGet("load-producttype-by-id")]
        public async Task<ActionResult<ServiceResponse<MProductType>>> GetById(int id)
        {
            var response = await _ProductTypeRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-producttype-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-producttype-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // POST api/<ProductTypeController>
        [HttpPost("add-producttype")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MProductType mProductType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mProductType.ProductTypeNo != 0)
            {
                response = await _ProductTypeRepository.AddProductType(mProductType);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-producttype ProductTypeCode " + mProductType.ProductTypeNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-producttype ProductTypeCode " + mProductType.ProductTypeNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : ProductType Code is Null"));
                _logger.LogError("POST : add-producttype ProductTypeCode " + mProductType.ProductTypeNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<ProductTypeController>/5
        [HttpPut("edit-producttype")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] MProductType mProductType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _ProductTypeRepository.EditProductType(id, mProductType);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-producttype ProductTypeCode " + mProductType.ProductTypeNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-producttype ProductTypeCode " + mProductType.ProductTypeNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-producttype ProductTypeCode " + mProductType.ProductTypeNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : ProductType Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<ProductTypeController>/5
        [HttpDelete("delete-producttype")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _ProductTypeRepository.DeleteProductType(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-producttype ProductTypeCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-producttype ProductTypeCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : ProductType Code is Null"));
                _logger.LogError("DELETE : delete-producttype ProductTypeCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}