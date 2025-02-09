using System.Net.Http;
using System.Text.Json;
using ConsumeAPI.Models;

namespace ConsumeAPI.Service
{
    public class ApiCallElectricTariffService
    {       
        public ApiCallElectricTariffService()
        {
            
        }
        public async Task<List<TariffCalculationResult>?> GetTariffComparisonAsync(int consumptionKWh)
        {            
            
            using (HttpClient client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri("http://localhost:5244/");

                // Make the GET request
                HttpResponseMessage response = await client.GetAsync($"api/TariffComparison?consumptionKWh={consumptionKWh}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<TariffCalculationResult>>(responseContent);
                    return result;
                }

                return null;
            }
        }
    }
}



