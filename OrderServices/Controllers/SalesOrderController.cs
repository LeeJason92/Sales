using Microsoft.AspNetCore.Mvc;
using OrderServices.Interfaces;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private ISalesOrder _SalesOrderRepository;
        private readonly ILogger<SalesOrderController> _logger;

        public SalesOrderController(ISalesOrder SalesOrderRepository, ILogger<SalesOrderController> logger)
        {
            _SalesOrderRepository = SalesOrderRepository;
            _logger = logger;
        }

        [HttpGet("all-salesorder")]
        public async Task<ActionResult<ServiceResponse<IQueryable<TSalesOrder>>>> GetAllSalesOrder()
        {
            var response = await _SalesOrderRepository.GetAllOrder();
            return Ok(response);
        }

        // GET api/<SalesOrderController>/5
        [HttpGet("load-salesorder-by-id")]
        public async Task<ActionResult<ServiceResponse<TSalesOrder>>> GetById(int id)
        {
            var response = await _SalesOrderRepository.GetById(id);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-salesorder-by-id " + id + " Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-salesorder-by-id " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-all-salesorder-by-type")]
        public async Task<ActionResult<ServiceResponse<List<TSalesOrder>>>> GetSalesByBusinessUnit(int businessUnitType)
        {
            var response = await _SalesOrderRepository.GetSalesByBusinessUnit(businessUnitType);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-all-salesorder-by-type Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-all-salesorder-by-type Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-monthly-salesorder-by-type")]
        public async Task<ActionResult<ServiceResponse<List<TSalesOrder>>>> GetMonthlySalesByBusinessUnit(int businessUnitType, int monthNum, int year)
        {
            var response = await _SalesOrderRepository.GetMonthlySalesByBusinessUnit(businessUnitType, monthNum, year);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-monthly-salesorder-by-type Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-monthly-salesorder-by-type Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-annual-salesorder-by-type")]
        public async Task<ActionResult<ServiceResponse<List<TSalesOrder>>>> GetAnnualSalesByBusinessUnit(int businessUnitType, int year)
        {
            var response = await _SalesOrderRepository.GetAnnualSalesByBusinessUnit(businessUnitType, year);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-annual-salesorder-by-type Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-annual-salesorder-by-type Error : " + response.Message);
                return BadRequest(response);
            }
        }

        //Revenue
        [HttpGet("load-monthly-revenue-salesorder")]
        public async Task<ActionResult<ServiceResponse<TSalesOrderRevenueReport>>> GetMonthlyRevenueSales(int personaNo, int monthNum, int year)
        {
            var response = await _SalesOrderRepository.GetMonthlyRevenueSales(personaNo, monthNum, year);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-monthly-revenue-salesorder Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-monthly-revenue-salesorder Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-annual-revenue-salesorder")]
        public async Task<ActionResult<ServiceResponse<TSalesOrderRevenueReport>>> GetAnnualRevenueSales(int personaNo, int year)
        {
            var response = await _SalesOrderRepository.GetAnnualRevenueSales(personaNo, year);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-annual-revenue-salesorder Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-annual-revenue-salesorder Error : " + response.Message);
                return BadRequest(response);
            }
        }

        //Revenue per Store
        [HttpGet("load-monthly-revenue-salesorder-by-store")]
        public async Task<ActionResult<ServiceResponse<TSalesOrderRevenueReport>>> GetMonthlyRevenueSalesByStore(int personaNo, int storeNo, int monthNum, int year)
        {
            var response = await _SalesOrderRepository.GetMonthlyRevenueSalesByStore(personaNo, storeNo, monthNum, year);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-monthly-revenue-salesorder Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-monthly-revenue-salesorder Error : " + response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("load-annual-revenue-salesorder-by-store")]
        public async Task<ActionResult<ServiceResponse<TSalesOrderRevenueReport>>> GetAnnualRevenueSalesByStore(int personaNo, int storeNo, int year)
        {
            var response = await _SalesOrderRepository.GetAnnualRevenueSalesByStore(personaNo, storeNo, year);
            if (response.isSuccess)
            {
                _logger.LogInformation("GET : load-annual-revenue-salesorder Success");
                return Ok(response);
            }
            else
            {
                _logger.LogError("GET : load-annual-revenue-salesorder Error : " + response.Message);
                return BadRequest(response);
            }
        }

        // POST api/<SalesOrderController>
        //asumsi : quantity pada sales order telah disesuaikan dengan sisa stok pada master produk (pada UI / aplikasi), sehingga tidak mungkin
        //ada request api dengan quantity melebihi sisa stok produk
        [HttpPost("add-salesorder")]
        public async Task<ActionResult<ServiceResponse<string>>> Post([FromBody] TSalesOrder TSalesOrder)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (TSalesOrder.SalesOrderNo != 0)
            {
                response = await _SalesOrderRepository.AddOrder(TSalesOrder);
                if (response.isSuccess)
                {
                    _logger.LogInformation("POST : add-salesorder SalesOrderCode " + TSalesOrder.SalesOrderNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("POST : add-salesorder SalesOrderCode " + TSalesOrder.SalesOrderNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : SalesOrder Code is Null"));
                _logger.LogError("POST : add-salesorder SalesOrderCode " + TSalesOrder.SalesOrderNo + " Error : " + response.Message);
                return BadRequest(response);
            }
        }


        // PUT api/<SalesOrderController>/5
        [HttpPut("edit-salesorder")]
        public async Task<ActionResult<ServiceResponse<string>>> Put(int id, [FromBody] TSalesOrder TSalesOrder)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _SalesOrderRepository.EditOrder(id, TSalesOrder);
                if (response.isSuccess)
                {
                    _logger.LogInformation("PUT : edit-salesorder SalesOrderCode " + TSalesOrder.SalesOrderNo + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("PUT : edit-salesorder SalesOrderCode " + TSalesOrder.SalesOrderNo + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                _logger.LogError("PUT : edit-salesorder SalesOrderCode " + TSalesOrder.SalesOrderNo + " Error : " + response.Message);
                response.Fail(new Exception("Error : SalesOrder Code is Null"));
                return BadRequest(response);
            }
        }

        // DELETE api/<SalesOrderController>/5
        [HttpDelete("delete-salesorder")]
        public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (id != 0)
            {
                response = await _SalesOrderRepository.DeleteOrder(id);
                if (response.isSuccess)
                {
                    _logger.LogInformation("DELETE : delete-salesorder SalesOrderCode " + id + " Success");
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("DELETE : delete-salesorder SalesOrderCode " + id + " Error : " + response.Message);
                    return BadRequest(response);
                }
            }
            else
            {
                response.Fail(new Exception("Error : SalesOrder Code is Null"));
                _logger.LogError("DELETE : delete-salesorder SalesOrderCode " + id + " Error : " + response.Message);
                return BadRequest(response);
            }
        }
    }
}