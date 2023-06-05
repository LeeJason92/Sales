using Microsoft.AspNetCore.Mvc;
using ProductServices.Interfaces;
using ProductServices.Models;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SparepartTypeController : ControllerBase
    {
        private ISparepartType _SparepartTypeRepository;
        private readonly ILogger<SparepartTypeController> _logger;

        public SparepartTypeController(ISparepartType SparepartTypeRepository, ILogger<SparepartTypeController> logger)
        {
            _SparepartTypeRepository = SparepartTypeRepository;
            _logger = logger;
        }

        [HttpGet("all-spareparttype")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MSparepartType>>>> GetAllSparepartType()
        {
            var response = await _SparepartTypeRepository.GetAllSparepartType();
            return Ok(response);
        }

        // GET api/<SparepartTypeController>/5
        [HttpGet("load-spareparttype-by-id")]
        public async Task<ActionResult<ServiceResponse<MSparepartType>>> GetById(int id)
        {
            var response = await _SparepartTypeRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-spareparttype-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-spareparttype-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // POST api/<SparepartTypeController>
        [HttpPost("add-spareparttype")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MSparepartType mSparepartType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mSparepartType.SparpartTypeNo != 0)
            {
                response = await _SparepartTypeRepository.AddSparepartType(mSparepartType);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-sparepartType SparepartTypeCode " + mSparepartType.SparpartTypeNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-sparepartType SparepartTypeCode " + mSparepartType.SparpartTypeNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : SparepartType Code is Null"));
                _logger.LogError("POST : add-sparepartType SparepartTypeCode " + mSparepartType.SparpartTypeNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<SparepartTypeController>/5
        [HttpPut("edit-spareparttype")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] MSparepartType mSparepartType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _SparepartTypeRepository.EditSparepartType(id, mSparepartType);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-sparepartType SparepartTypeCode " + mSparepartType.SparpartTypeNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-sparepartType SparepartTypeCode " + mSparepartType.SparpartTypeNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-sparepartType SparepartTypeCode " + mSparepartType.SparpartTypeNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : SparepartType Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<SparepartTypeController>/5
        [HttpDelete("delete-spareparttype")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _SparepartTypeRepository.DeleteSparepartType(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-sparepartType SparepartTypeCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-sparepartType SparepartTypeCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : SparepartType Code is Null"));
                _logger.LogError("DELETE : delete-sparepartType SparepartTypeCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}