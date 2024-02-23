using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Encodings.Web;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        string APIKEY = "d941d87444ee23e1119d8028be0c4c6a";
        static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Index Page
        /// </summary>
        /// <param name="searchInput">User inputed search query</param>
        /// <returns>Requested view</returns>
        public async Task<IActionResult> Index(string searchInput)
        {
            CurrentWeather currentWeather = new CurrentWeather();
            Forecast forecast = new Forecast();

            // if the user query not null
            if (searchInput != null)
            {
                // prevents user input from containing malicious inputs
                searchInput = HtmlEncoder.Default.Encode(searchInput);

                // get the weather information from the user's query
                currentWeather = await ReturnCurrentWeather(searchInput);
                forecast = await ReturnForecast(searchInput);

                // if the information was not returned sucessfully, act as if the search query was null
                if (currentWeather == null || forecast == null)
                    return View();

                // combine the weather and forecast information
                WeatherAndForecast weatherAndForecast = new WeatherAndForecast(forecast, currentWeather);

                // return the view with the weather/forecast model
                return View(weatherAndForecast);

            }

            // return the view
            return View();
        }

        /// <summary>
        /// Returns the current weather information for the search query
        /// </summary>
        /// <param name="searchInput">User inputed search query</param>
        /// <returns>Current weather information</returns>
        async Task<CurrentWeather> ReturnCurrentWeather(string searchInput)
        {
            try
            {
                string responseBody = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={searchInput}&appid={APIKEY}&units=imperial");


                if (responseBody == null)
                {
                    Console.WriteLine("Unable to retrieve response from API");
                    return null;
                }

                CurrentWeather? currentWeather = JsonConvert.DeserializeObject<CurrentWeather>(responseBody);

                return currentWeather!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return null;
            }
        }

        /// <summary>
        /// Returns the projected forecast information for the search query
        /// </summary>
        /// <param name="searchInput">User inputed search query</param>
        /// <returns>Projected forecast information</returns>
        async Task<Forecast> ReturnForecast(string searchInput)
        {
            try
            {
                string responseBody = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?q={searchInput}&appid={APIKEY}&units=imperial");


                if (responseBody == null)
                {
                    Console.WriteLine("Unable to retrieve response from API");
                    return null;
                }

                Forecast? forecast = JsonConvert.DeserializeObject<Forecast>(responseBody);

                return forecast!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return null;
            }
        }
    }
}
