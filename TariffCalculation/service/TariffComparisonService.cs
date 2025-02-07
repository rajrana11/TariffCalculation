using TariffCalculation.Model;

namespace TariffCalculation.service
{
    public class TariffComparisonService
    {
        private readonly List<TariffProduct> _tariffProducts;

        public TariffComparisonService()
        {
            // Mocking the data from the Tariff Provider
            _tariffProducts = new List<TariffProduct>
        {
            new TariffProduct
            {
                Name = "Product A",
                Type = 1,
                BaseCost = 5,
                AdditionalKwhCost = 0.22m
            },
            new TariffProduct
            {
                Name = "Product B",
                Type = 2,
                BaseCost = 800,
                AdditionalKwhCost = 0.30m,
                IncludedKwh = 4000
            }
        };
        }

        public List<TariffCalculationResult> CompareTariffs(int consumptionKWh)
        {
            var results = _tariffProducts.Select(tariff =>
            {
                var annualCost = CalculateAnnualCost(tariff, consumptionKWh);
                return new TariffCalculationResult
                {
                    TariffName = tariff.Name,
                    AnnualCost = annualCost
                };
            })
            .OrderBy(result => result.AnnualCost) // Sort by annual cost in ascending order
            .ToList();

            return results;
        }

        private decimal CalculateAnnualCost(TariffProduct tariff, int consumptionKWh)
        {
            decimal annualCost = 0;

            if (tariff.Type == 1) // Type 1: Basic electricity tariff
            {
                annualCost = (tariff.BaseCost * 12) + (consumptionKWh * tariff.AdditionalKwhCost);
            }
            else if (tariff.Type == 2) // Type 2: Packaged tariff
            {
                if (consumptionKWh <= tariff.IncludedKwh)
                {
                    annualCost = tariff.BaseCost;
                }
                else
                {
                    int additionalKWh = consumptionKWh - tariff.IncludedKwh.Value;
                    annualCost = tariff.BaseCost + (additionalKWh * tariff.AdditionalKwhCost);
                }
            }

            return annualCost;
        }
    }


}
