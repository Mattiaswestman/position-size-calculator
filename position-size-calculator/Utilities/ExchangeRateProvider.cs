using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace PositionSizeCalculator.Utilities
{
    public class ExchangeRateProvider
    {
        private static readonly HttpClient httpClient = new();

        private class ExchangeRateResponse
        {
            [JsonPropertyName("rates")]
            public Dictionary<string, decimal> Rates { get; set; }

            public ExchangeRateResponse()
            {
                Rates = new Dictionary<string, decimal>();
            }
        }

        public async Task<decimal> ConvertAsync(decimal amount, string fromCurrency, string toCurrency)
        {
            return await GetExchangeRateAsync(fromCurrency, toCurrency) * amount;
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            var url = $"https://api.frankfurter.app/latest?from={fromCurrency}&to={toCurrency}";

            var response = await httpClient.GetFromJsonAsync<ExchangeRateResponse>(url);

            if (response?.Rates != null && response.Rates.TryGetValue(toCurrency, out var rate))
            {
                return rate;
            }

            throw new Exception("Failed to retrieve USD to SEK exchange rate.");
        }
    }
}
