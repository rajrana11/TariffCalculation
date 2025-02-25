using TariffCalculation.Model;

namespace TariffCalculation.service
{
    public interface ICalculateAnaulCost
    {
        decimal CalculateAnnualCost(TariffProduct tariff, int consumptionKWh);
    }
}
