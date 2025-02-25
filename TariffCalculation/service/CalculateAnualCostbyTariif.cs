using TariffCalculation.Model;
using static TariffCalculation.Constants.Constants;

namespace TariffCalculation.service
{
    public class CalculateAnualCostbyTariif : ICalculateAnaulCost
    {
      
        public decimal CalculateAnnualCost(TariffProduct tariff, int consumptionKWh)
        {
            decimal annualCost = 0;

            if (tariff.Type == (int)TariffType.BasicElectricityTariff) // Type 1: Basic electricity tariff
            {
                annualCost = (tariff.BaseCost * 12) + (consumptionKWh * tariff.AdditionalKwhCost);
            }
            else if (tariff.Type == (int)TariffType.PackagedTariff) // Type 2: Packaged tariff
            {
                if (consumptionKWh <= tariff.IncludedKwh.GetValueOrDefault())
                {
                    annualCost = tariff.BaseCost;
                }
                else
                {
                    int additionalKWh = consumptionKWh - tariff.IncludedKwh.GetValueOrDefault();
                    annualCost = tariff.BaseCost + (additionalKWh * tariff.AdditionalKwhCost);
                }
            }

            return annualCost;
        }
    }
}
