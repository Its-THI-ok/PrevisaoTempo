using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeatherProxyApi.Services
{
    public class WeatherService(HttpClient httpClient, IConfiguration config)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string? _apiKey = config["ApiSettings:WeatherApiKey"];

        public async Task<HttpResponseMessage> GetWeatherAsync(string cidade)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={_apiKey}";
                return await _httpClient.GetAsync(url);
            }
            catch (HttpRequestException ex)
            {
                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Erro ao obter dados do tempo: {ex.Message}")
                };
                return errorResponse;
            }
        }
    }
}
