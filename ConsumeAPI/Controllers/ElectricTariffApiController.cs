using System.Diagnostics;
using ConsumeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ConsumeAPI.Service;

namespace ConsumeAPI.Controllers
{
    public class ElectricTariffApiController : Controller
    {
       
        public IActionResult ElectricTariff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTariffCalculation(int consumptionKWh)
        {           
            ApiCallElectricTariffService apiCallElectricTariffService = new ApiCallElectricTariffService();
            var tariffCalculationResults = await apiCallElectricTariffService.GetTariffComparisonAsync(consumptionKWh);
            return Json(tariffCalculationResults);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
