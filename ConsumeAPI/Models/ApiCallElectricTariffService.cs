using System.Text.Json;

namespace ConsumeAPI.Models
{
    public class ApiCallElectricTariffService
    {
        private readonly HttpClient _httpClient;
        public ApiCallElectricTariffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<TariffCalculationResult[]?> GetTariffComparisonAsync(int consumptionKWh)
        {
            var response = await _httpClient.GetAsync($"api/tariffcomparison?consumptionKWh={consumptionKWh}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TariffCalculationResult[]>(responseContent);
                return result;
            }
            return null;
        }
    }
}


public class TariffCalculationResult
{
    public string? TariffName { get; set; }
    public decimal AnnualCost { get; set; }
}

