using Microsoft.OpenApi.Extensions;
using TariffCalculation.Model;
using static TariffCalculation.Constants.Constants;

namespace TariffCalculation.service
{
    public class TariffComparisonService : ITariffComparisonService
    {
        private readonly ICalculateAnaulCost _calculateAnaulCost;
        private readonly List<TariffProduct> _tariffProducts;
        public TariffComparisonService(ICalculateAnaulCost calculateAnaulCost)
        {
            _calculateAnaulCost = calculateAnaulCost;
            _tariffProducts = new List<TariffProduct>
        {
            new TariffProduct
            {
                Name = "Product A",
                Type = (int)TariffType.BasicElectricityTariff,
                BaseCost = 5,
                AdditionalKwhCost = 0.22m,
                ProducttypeName=BasicElectricityTariff
            },
            new TariffProduct
            {
                Name = "Product B",
                Type = (int)TariffType.PackagedTariff,
                BaseCost = 800,
                AdditionalKwhCost = 0.30m,
                IncludedKwh = 4000,
                ProducttypeName=PackagedTariff
            }
        };
        }
        public List<TariffCalculationResult> CompareTariffs(int consumptionKWh)
        {
                       
            var results = _tariffProducts.Select(tariff =>
            {
                var annualCost = _calculateAnaulCost.CalculateAnnualCost(tariff, consumptionKWh);
                return new TariffCalculationResult
                {
                    TariffName = tariff.ProducttypeName,
                    AnnualCost = annualCost
                };
            })
            .OrderBy(result => result.AnnualCost) // Sort by annual cost in ascending order
            .ToList();
            return results;
        }       
    }

}
