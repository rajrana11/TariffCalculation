using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TariffCalculation.Model;
using TariffCalculation.service;

namespace TariffCalculation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffComparisonController : ControllerBase
    {
        
        private readonly ITariffComparisonService _tariffComparisonService;

        public TariffComparisonController(ITariffComparisonService tariffComparisonService)
        {
            _tariffComparisonService = tariffComparisonService;
        }

        [HttpGet]
        public ActionResult<List<TariffCalculationResult>> GetComparison([FromQuery] int consumptionKWh)
        {
            if (consumptionKWh <= 0)
            {
                return BadRequest("Consumption must be a positive value.");
            }

            var results = _tariffComparisonService.CompareTariffs(consumptionKWh);
            return Ok(results);
        }
    }
}

