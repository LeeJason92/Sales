using Microsoft.AspNetCore.Mvc;
using StoreServices.Interfaces;
using StoreServices.Models;

namespace StoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private IStore _StoreRepository;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IStore StoreRepository, ILogger<StoreController> logger)
        {
            _StoreRepository = StoreRepository;
            _logger = logger;
        }

        [HttpGet("all-store")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MStore>>>> GetAllStore()
        {
            var response = await _StoreRepository.GetAllStore();
            return Ok(response);
        }

        // GET api/<StoreController>/5
        [HttpGet("load-store-by-id")]
        public async Task<ActionResult<ServiceResponse<MStore>>> GetById(int id)
        {
            var response = await _StoreRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-store-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-store-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<StoreController>
        [HttpPost("add-store")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MStore mStore)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mStore.StoreNo != 0)
            {
                response = await _StoreRepository.AddStore(mStore);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-store StoreCode " + mStore.StoreNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-store StoreCode " + mStore.StoreNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Store Code is Null"));
                _logger.LogError("POST : add-store StoreCode " + mStore.StoreNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<StoreController>/5
        [HttpPut("edit-store")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] MStore mStore)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _StoreRepository.EditStore(id, mStore);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-store StoreCode " + mStore.StoreNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-store StoreCode " + mStore.StoreNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-store StoreCode " + mStore.StoreNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : Store Code is Null"));
                return BadRequest(response);
            }
        }


        // DELETE api/<StoreController>/5
        [HttpDelete("delete-store")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _StoreRepository.DeleteStore(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-store StoreCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-store StoreCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Store Code is Null"));
                _logger.LogError("DELETE : delete-store StoreCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}