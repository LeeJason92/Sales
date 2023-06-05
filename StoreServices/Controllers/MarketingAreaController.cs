using Microsoft.AspNetCore.Mvc;
using StoreServices.Interfaces;
using StoreServices.Models;
using StoreServices.Repositories;

namespace StoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingAreaController : ControllerBase
    {
        private IMarketingArea _MarketingAreaRepository;
        private readonly ILogger<MarketingAreaController> _logger;

        public MarketingAreaController(IMarketingArea MarketingAreaRepository, ILogger<MarketingAreaController> logger)
        {
            _MarketingAreaRepository = MarketingAreaRepository;
            _logger = logger;
        }

        [HttpGet("all-marketingarea")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MMarketingArea>>>> GetAllMarketingArea()
        {
            var response = await _MarketingAreaRepository.GetAllMarketingArea();
            return Ok(response);
        }

        // GET api/<MarketingAreaController>/5
        [HttpGet("load-marketingarea-by-id")]
        public async Task<ActionResult<ServiceResponse<MMarketingArea>>> GetById(int id)
        {
            var response = await _MarketingAreaRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-marketingarea-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-marketingarea-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<MarketingAreaController>
        [HttpPost("add-marketingarea")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MMarketingArea mMarketingArea)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mMarketingArea.AreaNo != 0)
            {
                response = await _MarketingAreaRepository.AddMarketingArea(mMarketingArea);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-marketingarea MarketingAreaCode " + mMarketingArea.AreaNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-marketingarea MarketingAreaCode " + mMarketingArea.AreaNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : MarketingArea Code is Null"));
                _logger.LogError("POST : add-marketingarea MarketingAreaCode " + mMarketingArea.AreaNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<MarketingAreaController>/5
        [HttpPut("edit-marketingarea")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] MMarketingArea mMarketingArea)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _MarketingAreaRepository.EditMarketingArea(id, mMarketingArea);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-marketingarea MarketingAreaCode " + mMarketingArea.AreaNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-marketingarea MarketingAreaCode " + mMarketingArea.AreaNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-marketingarea MarketingAreaCode " + mMarketingArea.AreaNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : MarketingArea Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<MarketingAreaController>/5
        [HttpDelete("delete-marketingarea")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _MarketingAreaRepository.DeleteMarketingArea(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-marketingarea MarketingAreaCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-marketingarea MarketingAreaCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : MarketingArea Code is Null"));
                _logger.LogError("DELETE : delete-marketingarea MarketingAreaCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}