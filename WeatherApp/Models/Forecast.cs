namespace WeatherApp.Models
{
    public class Forecast
    {
        public List<ForecastList> list {  get; set; }
    }

    public class ForecastList
    {
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public string dt_txt { get; set; }
    }
}
