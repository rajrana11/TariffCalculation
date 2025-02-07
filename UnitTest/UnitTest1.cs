using System;
using Microsoft.AspNetCore.Routing;
using TariffCalculation.Model;
using TariffCalculation.service;

namespace UnitTest
{
    public class UnitTest1
    {
        private TariffComparisonService _tariffComparisonService;
        
        public UnitTest1()
        {
            _tariffComparisonService = new TariffComparisonService();
        }
        [Fact]
        public void Test1()
        {
            int consumptionKWh = 5000;

            // Act
            var results = _tariffComparisonService.CompareTariffs(consumptionKWh);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(2, results.Count);
            Assert.Equal("Basic electricity tariff", results[1].TariffName);
            Assert.Equal("Packaged tariff", results[0].TariffName);
            Assert.True(results[0].AnnualCost <= results[1].AnnualCost);
        }
        [Fact]
        public void CompareTariffs_ShouldCalculateCorrectAnnualCostForBasicElectricityTariff()
        {
            // Arrange
            var tariffProducts = new List<TariffProduct>
            {
                new TariffProduct
                {
                    Name = "Basic Electricity Tariff",
                    Type = 1, // BasicElectricityTariff
                    BaseCost = 5,
                    AdditionalKwhCost = 0.22m,
                    ProducttypeName = "Basic Electricity Tariff"
                }
            };
            var service = new TariffComparisonServiceMock(tariffProducts);
            int consumptionKWh = 5000;

            // Act
            var results = service.CompareTariffs(consumptionKWh);

            // Assert
            var basicElectricityTariffResult = results.Find(x => x.TariffName == "Basic electricity tariff")?.AnnualCost ;
            decimal expectedAnnualCost = (5 * 12) + (5000 * 0.22m);
            Assert.Equal(expectedAnnualCost, basicElectricityTariffResult);
        }

        [Fact]
        public void CompareTariffs_ShouldCalculateCorrectAnnualCostForPackagedTariff()
        {
            // Arrange
            var tariffProducts = new List<TariffProduct>
            {
                new TariffProduct
                {
                    Name = "Packaged Tariff",
                    Type = 2, // PackagedTariff
                    BaseCost = 800,
                    AdditionalKwhCost = 0.30m,
                    IncludedKwh = 4000,
                    ProducttypeName = "Packaged Tariff"
                }
            };
            var service = new TariffComparisonServiceMock(tariffProducts);
            int consumptionKWh = 5000;

            // Act
            var results = service.CompareTariffs(consumptionKWh);

            // Assert
            var packagedTariffResult = results.Find(x => x.TariffName == "Packaged tariff")?.AnnualCost;
            decimal expectedAnnualCost = 800 + ((5000 - 4000) * 0.30m);
            Assert.Equal(expectedAnnualCost, packagedTariffResult);
        }
    }

    // Mock service to inject test data
    public class TariffComparisonServiceMock : TariffComparisonService
    {
        private readonly List<TariffProduct> _tariffProducts;

        public TariffComparisonServiceMock(List<TariffProduct> tariffProducts)
        {
            _tariffProducts = tariffProducts;
        }

        // Override _tariffProducts for testing purposes
        public List<TariffProduct> TariffProducts => _tariffProducts;
    }
}
