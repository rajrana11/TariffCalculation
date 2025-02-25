using TariffCalculation.Model;

namespace TariffCalculation.service
{
    public interface ITariffComparisonService
    {
        List<TariffCalculationResult> CompareTariffs(int consumptionKWh);
    }
}
