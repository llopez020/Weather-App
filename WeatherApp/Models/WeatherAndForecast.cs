namespace WeatherApp.Models
{
    public class WeatherAndForecast
    {
        public Forecast forecast { get; set; }
        public CurrentWeather currentWeather { get; set; }
        public WeatherAndForecast(Forecast init_fore, CurrentWeather init_weath) { 
            forecast = init_fore; 
            currentWeather = init_weath;
        }
    }
}
