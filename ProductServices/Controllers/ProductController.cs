using Microsoft.AspNetCore.Mvc;
using ProductServices.Interfaces;
using ProductServices.Models;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProduct _ProductRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProduct ProductRepository, ILogger<ProductController> logger)
        {
            _ProductRepository = ProductRepository;
            _logger = logger;
        }

        [HttpGet("all-product")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MProduct>>>> GetAllProduct()
        {
            var response = await _ProductRepository.GetAllProduct();
            return Ok(response);
        }

        // GET api/<ProductController>/5
        [HttpGet("load-product-by-id")]
        public async Task<ActionResult<ServiceResponse<MProduct>>> GetById(int id)
        {
            var response = await _ProductRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-product-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-product-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-product-by-desc")]
        public async Task<ActionResult<ServiceResponse<List<MProduct>>>> GetByDesc(string keyword)
        {
            var response = await _ProductRepository.GetByDesc(keyword);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-product-by-desc " + keyword + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-product-by-desc " + keyword + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<ProductController>
        [HttpPost("add-product")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MProduct mProduct)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mProduct.ProductNo != 0)
            {
                response = await _ProductRepository.AddProduct(mProduct);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-product ProductCode " + mProduct.ProductNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-product ProductCode " + mProduct.ProductNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Product Code is Null"));
                _logger.LogError("POST : add-product ProductCode " + mProduct.ProductNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<ProductController>/5
        [HttpPut("edit-product")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] MProduct mProduct)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _ProductRepository.EditProduct(id, mProduct);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-product ProductCode " + mProduct.ProductNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-product ProductCode " + mProduct.ProductNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-product ProductCode " + mProduct.ProductNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : Product Code is Null"));
                return BadRequest(response);
            }
        }

        [HttpPut("edit-product-stock")]
        public async Task<ActionResult<ServiceResponse<string>>> PutStock(int id, int countUsed)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _ProductRepository.EditProductStock(id, countUsed);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-product-stock ProductCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-product-stock ProductCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-product ProductCode " + id + " Error : " + response.Message);
                response.Fail(new Exception("Error : Product Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("delete-product")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _ProductRepository.DeleteProduct(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-product ProductCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-product ProductCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Product Code is Null"));
                _logger.LogError("DELETE : delete-product ProductCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}