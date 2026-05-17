using System.Net;

namespace App.Models
{
    public class Tempo {
        public double Lon { get; set; }
        public double Lat { get; set; }
        public int Humidity { get; set; }
        //public string? name { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public string? Main { get; set; }
        public string? Description { get; set; }
        public double Speed { get; set; }
        //public int deg { get; set; }
        public int Visibility { get; set; }
        public HttpStatusCode? HttpCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
