using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using App.Models;
using System.Diagnostics;

namespace App.Services
{
    public class DataService {
        private readonly HttpClient _httpClient;
        public DataService()
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:5032/")
                };
            }

        public async Task<Tempo?> GetPrevisao(string cidade) {
            // Inicializa o objeto Tempo como não atribuído (removida atribuição desnecessária)
            Tempo? tempo;

            try
            {
                // Parte da API
                var response = await _httpClient.GetAsync($"https://weatherproxyapi-dta3gghze9b0g5bp.brazilsouth-01.azurewebsites.net/api/weather/{Uri.EscapeDataString(cidade)}");

                string body = await response.Content.ReadAsStringAsync();
                // Parte da DataService
                if (response.IsSuccessStatusCode) {
                    // Lê a resposta JSON e formata para o objeto Tempo

                    var jsonFormated = JObject.Parse(body);

                    //Variáveis de datas
                    long sunriseUnix = jsonFormated["sys"]?["sunrise"]?.Value<long>() ?? -1;
                    long sunsetUnix = jsonFormated["sys"]?["sunset"]?.Value<long>() ?? -1;

                    // Datas Convertidas de Unix Timestamp para DateTime
                    DateTime sunrise = DateTimeOffset.FromUnixTimeSeconds((long)sunriseUnix).ToLocalTime().DateTime;
                    DateTime sunset = DateTimeOffset.FromUnixTimeSeconds(sunsetUnix).ToLocalTime().DateTime;

                    // Depuração por CONSOLE para verificar os valores de sunrise e sunset
                    Console.WriteLine($"Sunrise: {sunrise}, Sunset: {sunset}");

                    // Cria o objeto Tempo com os dados formatados
                    tempo = new Tempo()
                    {
                        Lat = jsonFormated["coord"]?["lat"]?.Value<double>() ?? double.NaN,
                        Lon = jsonFormated["coord"]?["lon"]?.Value<double>() ?? double.NaN,
                        Description = jsonFormated["weather"]?[0]?["description"]?.Value<string>() ?? string.Empty,
                        Main = jsonFormated["weather"]?[0]?["main"]?.Value<string>() ?? string.Empty,
                        Temp_min = jsonFormated["main"]?["temp_min"]?.Value<double>() ?? double.NaN,
                        Temp_max = jsonFormated["main"]?["temp_max"]?.Value<double>() ?? double.NaN,
                        Humidity = jsonFormated["main"]?["humidity"]?.Value<int>() ?? -1,
                        Visibility = jsonFormated["visibility"]?.Value<int>() ?? -1,
                        Speed = jsonFormated["wind"]?["speed"]?.Value<double>() ?? double.NaN,
                        Sunrise = sunrise,
                        Sunset = sunset
                    };
                } else {
                    tempo = new Tempo {
                        HttpCode = response.StatusCode,
                        ErrorMessage = $"Erro ao obter dados: {response.ReasonPhrase}"
                    };
                }
                // Tratamento de exceções para erros de requisição HTTP e outros erros gerais
            } catch (HttpRequestException ex) {
                tempo = new Tempo {
                    HttpCode = null,
                    ErrorMessage = $"Erro de requisição: {ex.Message}"
                };
            }
            catch (Exception ex) {
                tempo = new Tempo {
                    HttpCode = null,
                    ErrorMessage = $"Erro de inesperado: {ex.Message}"
                };
            }
            return tempo;
        }
    }
}
