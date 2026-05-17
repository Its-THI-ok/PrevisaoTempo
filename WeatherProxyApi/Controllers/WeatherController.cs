using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherProxyApi.Services;

namespace WeatherProxyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController(WeatherService weatherService) : ControllerBase
    {
        private readonly WeatherService _weatherService = weatherService;

        // HttpGet para obter a previsão do tempo de uma cidade específica
        [HttpGet("{cidade}")]
        public async Task<IActionResult> Get(string cidade)
        {
            var response = await _weatherService.GetWeatherAsync(cidade);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Content(body, "application/json");
            else
                return StatusCode((int)response.StatusCode, body);
        }
    }
}
