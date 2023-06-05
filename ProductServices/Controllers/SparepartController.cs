using Microsoft.AspNetCore.Mvc;
using ProductServices.Interfaces;
using ProductServices.Models;
using ProductServices.Repositories;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SparepartController : ControllerBase
    {
        private ISparepart _SparepartRepository;
        private readonly ILogger<SparepartController> _logger;

        public SparepartController(ISparepart SparepartRepository, ILogger<SparepartController> logger)
        {
            _SparepartRepository = SparepartRepository;
            _logger = logger;
        }

        [HttpGet("all-sparepart")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MSparepart>>>> GetAllSparepart()
        {
            var response = await _SparepartRepository.GetAllSparepart();
            return Ok(response);
        }

        // GET api/<SparepartController>/5
        [HttpGet("load-sparepart-by-id")]
        public async Task<ActionResult<ServiceResponse<MSparepart>>> GetById(int id)
        {
            var response = await _SparepartRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-sparepart-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-sparepart-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-sparepart-by-desc")]
        public async Task<ActionResult<ServiceResponse<List<MSparepart>>>> GetByDesc(string keyword)
        {
            var response = await _SparepartRepository.GetByDesc(keyword);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-sparepart-by-desc " + keyword + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-sparepart-by-desc " + keyword + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<SparepartController>
        [HttpPost("add-sparepart")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MSparepart mSparepart)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mSparepart.SparepartNo != 0)
            {
                response = await _SparepartRepository.AddSparepart(mSparepart);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-sparepart SparepartCode " + mSparepart.SparepartNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-sparepart SparepartCode " + mSparepart.SparepartNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Sparepart Code is Null"));
                _logger.LogError("POST : add-sparepart SparepartCode " + mSparepart.SparepartNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<SparepartController>/5
        [HttpPut("edit-sparepart")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] MSparepart mSparepart)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _SparepartRepository.EditSparepart(id, mSparepart);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-sparepart SparepartCode " + mSparepart.SparepartNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-sparepart SparepartCode " + mSparepart.SparepartNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-sparepart SparepartCode " + mSparepart.SparepartNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : Sparepart Code is Null"));
                return BadRequest(response);
            }
        }

        [HttpPut("edit-sparepart-stock")]
        public async Task<ActionResult<ServiceResponse<string>>> PutStock(int id, int countUsed)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _SparepartRepository.EditSparepartStock(id, countUsed);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-sparepart-stock SparepartCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-sparepart-stock SparepartCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-sparepart SparepartCode " + id + " Error : " + response.Message);
                response.Fail(new Exception("Error : Sparepart Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<SparepartController>/5
        [HttpDelete("delete-sparepart")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _SparepartRepository.DeleteSparepart(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-sparepart SparepartCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-sparepart SparepartCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Sparepart Code is Null"));
                _logger.LogError("DELETE : delete-sparepart SparepartCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}