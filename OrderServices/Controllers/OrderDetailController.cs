using Microsoft.AspNetCore.Mvc;
using OrderServices.Interfaces;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetail _OrderDetailRepository;
        private readonly ILogger<OrderDetailController> _logger;

        public OrderDetailController(IOrderDetail OrderDetailRepository, ILogger<OrderDetailController> logger)
        {
            _OrderDetailRepository = OrderDetailRepository;
            _logger = logger;
        }

        [HttpGet("all-orderdetail")]
        public async Task<ActionResult<ServiceResponse<IQueryable<VOrderDetail>>>> GetAllOrderDetail(int personaNo)
        {
            var response = await _OrderDetailRepository.GetAllOrderDetails(personaNo);
            return Ok(response);
        }

        // GET api/<OrderDetailController>/5
        [HttpGet("load-orderdetail-by-status")]
        public async Task<ActionResult<List<VOrderDetail>>> GetByStatus(int personaNo,string status)
        {
            var response = await _OrderDetailRepository.GetByStatus(personaNo, status);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-orderdetail-by-status " + status + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-orderdetail-by-status " + status + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}