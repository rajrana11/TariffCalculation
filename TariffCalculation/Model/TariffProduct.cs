namespace TariffCalculation.Model
{
    public class TariffProduct
    {
        public string? Name { get; set; }
        public int Type { get; set; }
        public decimal BaseCost { get; set; } // Base cost per month in EUR
        public decimal AdditionalKwhCost { get; set; } // Additional cost per kWh in EUR (for type 1 and type 2)
        public int? IncludedKwh { get; set; } // Included kWh (for type 2 tariffs, optional)
    }

}
