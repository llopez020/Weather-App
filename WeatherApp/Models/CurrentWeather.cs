namespace WeatherApp.Models
{
    public class CurrentWeather
    {
        public string? name { get; set; }
        public List<Weather> weather { get; set; }
        public Main main { get; set; }
    }
}
