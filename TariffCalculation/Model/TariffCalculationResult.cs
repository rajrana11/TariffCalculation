namespace TariffCalculation.Model
{
    public class TariffCalculationResult
    {
        public string? TariffName { get; set; }
        public decimal AnnualCost { get; set; }

        public decimal CalculateAnnualCost(TariffProduct tariff, int consumptionKWh)
        {
            decimal annualCost = 0;

            if (tariff.Type == 1) // Type 1: Basic electricity tariff
            {
                // Base cost for 12 months + consumption cost (consumption * additional cost per kWh)
                annualCost = (tariff.BaseCost * 12) + (consumptionKWh * tariff.AdditionalKwhCost);
            }
            else if (tariff.Type == 2) // Type 2: Packaged tariff
            {
                // If consumption is within the included kWh
                if (consumptionKWh <= tariff.IncludedKwh)
                {
                    annualCost = tariff.BaseCost;
                }
                else
                {
                    // Base cost + additional consumption cost for the amount above included kWh
                    int additionalKWh = consumptionKWh - tariff.IncludedKwh.Value;
                    annualCost = tariff.BaseCost + (additionalKWh * tariff.AdditionalKwhCost);
                }
            }

            return annualCost;
        }

    }

}
