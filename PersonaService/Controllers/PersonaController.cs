using Microsoft.AspNetCore.Mvc;
using PersonaService.Interfaces;
using PersonaService.Models;

namespace PersonaService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private IPersona _PersonaRepository;
        private readonly ILogger<PersonaController> _logger;

        public PersonaController(IPersona PersonaRepository, ILogger<PersonaController> logger)
        {
            _PersonaRepository = PersonaRepository;
            _logger = logger;
        }

        [HttpGet("all-persona")]
        public async Task<ActionResult<ServiceResponse<IQueryable<MPersona>>>> GetAllPersona()
        {
            var response = await _PersonaRepository.GetAllPersona();
            return Ok(response);
        }

        // GET api/<PersonaController>/5
        [HttpGet("load-persona-by-id")]
        public async Task<ActionResult<ServiceResponse<MPersona>>> GetById(int id)
        {
            var response = await _PersonaRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-persona-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-persona-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // POST api/<PersonaController>
        [HttpPost("add-persona")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] MPersona mPersona)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (mPersona.PersonaNo != 0)
            {
                response = await _PersonaRepository.AddPersona(mPersona);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-persona PersonaCode " + mPersona.PersonaNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-persona PersonaCode " + mPersona.PersonaNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Persona Code is Null"));
                _logger.LogError("POST : add-persona PersonaCode " + mPersona.PersonaNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<PersonaController>/5
        [HttpPut("edit-persona")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] MPersona mPersona)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _PersonaRepository.EditPersona(id, mPersona);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-persona PersonaCode " + mPersona.PersonaNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-persona PersonaCode " + mPersona.PersonaNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-persona PersonaCode " + mPersona.PersonaNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : Persona Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<PersonaController>/5
        [HttpDelete("delete-persona")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _PersonaRepository.DeletePersona(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-persona PersonaCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-persona PersonaCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : Persona Code is Null"));
                _logger.LogError("DELETE : delete-persona PersonaCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}