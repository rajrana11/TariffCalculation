using TariffCalculation.Model;
using TariffCalculation.service;
using static TariffCalculation.Constants.Constants;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTest
{
    public class TariffComparisonServiceTests
    {
        // Test the CompareTariffs method for Product A and Product B
        [Fact]
        public void CompareTariffs_ShouldReturnSortedResultsByAnnualCost()
        {
            // Arrange
            var calculateAnaulCost = new CalculateAnualCostbyTariif();
            var tariffComparisonService = new TariffComparisonService(calculateAnaulCost);

            // Test inputs
            int consumptionKWh = 5000;

            // Act
            var result = tariffComparisonService.CompareTariffs(consumptionKWh);

            // Assert that results are sorted by annual cost
            Assert.IsNotNull(result);
            Assert.IsTrue(result.First().AnnualCost <= result.Last().AnnualCost);

            // Further assertions to check specific expected outcomes
            var productA = result.First();
            var productB = result.Last();

            Assert.AreEqual("BasicElectricityTariff", productA.TariffName); // Assuming Product A is BasicElectricityTariff
            Assert.AreEqual("PackagedTariff", productB.TariffName); // Assuming Product B is PackagedTariff

            // Check the actual cost calculations for each tariff (optional but useful for correctness check)
            var expectedAnnualCostA = (5 * 12) + (5000 * 0.22m); // 5 base cost, 0.22 additional per kWh
            var expectedAnnualCostB = 800 + ((5000 - 4000) * 0.30m); // 800 base, 0.30 additional for excess kWh
            Assert.AreEqual(expectedAnnualCostA, productA.AnnualCost);
            Assert.AreEqual(expectedAnnualCostB, productB.AnnualCost);
        }

        // Additional test for edge cases, e.g., consumption less than the included KWh
        [Fact]
        public void CompareTariffs_ShouldHandleConsumptionLessThanIncludedKWh()
        {
            // Arrange
            var calculateAnaulCost = new CalculateAnualCostbyTariif();
            var tariffComparisonService = new TariffComparisonService(calculateAnaulCost);
            int consumptionKWh = 3000; // Less than the included 4000 kWh for Product B

            // Act
            var result = tariffComparisonService.CompareTariffs(consumptionKWh);

            // Assert that the results are sorted and Product B has the same cost as base cost
            Assert.IsNotNull(result);
            var productB = result.First(tariff => tariff.TariffName == "PackagedTariff");

            // Check that the cost of Product B is equal to the base cost since consumption is within limit
            Assert.AreEqual(800m, productB.AnnualCost); // Base cost for Product B
        }

        // Additional test to check if the tariff calculation handles edge cases correctly
        [Fact]
        public void CalculateAnnualCost_ShouldHandleNoAdditionalKWhForBasicTariff()
        {
            // Arrange
            var calculateAnaulCost = new CalculateAnualCostbyTariif();
            var tariffComparisonService = new TariffComparisonService(calculateAnaulCost);
            var basicElectricityTariff = new TariffProduct
            {
                Type = (int)TariffType.BasicElectricityTariff,
                BaseCost = 5,
                AdditionalKwhCost = 0.22m
            };
        }

            // Act
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
                        Name = "BasicElectricityTariff",
                        Type = (int)TariffType.BasicElectricityTariff,
                        BaseCost = 5,
                        AdditionalKwhCost = 0.22m
                    },
                    new TariffProduct
                    {
                        Name = "PackagedTariff",
                        Type = (int)TariffType.PackagedTariff,
                        BaseCost = 800,
                        AdditionalKwhCost = 0.30m,
                        IncludedKwh = 4000
                    }
                };
            }

            public List<TariffCalculationResult> CompareTariffs(int consumptionKWh)
            {
                var results = new List<TariffCalculationResult>();

                foreach (var product in _tariffProducts)
                {
                    var annualCost = _calculateAnaulCost.CalculateAnnualCost(product, consumptionKWh);
                    results.Add(new TariffCalculationResult
                    {
                        TariffName = product.Name,
                        AnnualCost = annualCost
                    });
                }

                return results.OrderBy(r => r.AnnualCost).ToList();
            }
        }
                   
        }

    }





