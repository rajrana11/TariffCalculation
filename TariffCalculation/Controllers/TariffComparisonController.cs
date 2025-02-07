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
        private readonly TariffComparisonService _tariffComparisonService;

        public TariffComparisonController()
        {
            _tariffComparisonService = new TariffComparisonService();
        }

        [HttpGet]
        public ActionResult<List<TariffCalculationResult>> GetComparison(int consumptionKWh)
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

