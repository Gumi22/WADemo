using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    class JsonForeCastConverter
    {
        public List<WeatherForeCast> GetForeCasts(Stream JsonStream)
        {
            var forecasts = new List<WeatherForeCast>();

            using (StreamReader sr = new StreamReader(JsonStream))
            {
                var jsonString = sr.ReadToEnd();
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    Debug.WriteLine("Response body was empty");
                    return null;
                }
                else
                {
                    Debug.WriteLine($"Response was {jsonString}");

                    var jsonForecasts = JObject.Parse(jsonString)["list"];
                    foreach (var jsonForecast in jsonForecasts)
                    {
                        WeatherForeCast forecast = new WeatherForeCast();
                        Debug.WriteLine(jsonForecast["rain"]);

                        //Horrible, change this:
                        try
                        {
                            forecast.CloudCover = jsonForecast["clouds"]?["all"]?.Value<double>() ?? 0f;
                            forecast.Condition = jsonForecast["weather"]?[0]?["id"]?.Value<int>() ?? 0;
                            forecast.Humidity = jsonForecast["main"]?["humidity"]?.Value<double>() ?? 0f;
                            forecast.Icon = jsonForecast["weather"]?[0]?["icon"]?.Value<string>() ?? "";
                            forecast.Pressure = jsonForecast["main"]?["pressure"]?.Value<double>() ?? 0f;
                            forecast.Rain = jsonForecast["rain"]?["3h"]?.Value<double>() ?? 0f;
                            forecast.Snow = jsonForecast["snow"]?["3h"]?.Value<double>() ?? 0f;
                            forecast.Temperature = jsonForecast["main"]?["temp"]?.Value<double>() ?? 0f;
                            forecast.WindDirection = jsonForecast["wind"]?["deg"]?.Value<double>() ?? 0f;
                            forecast.WindSpeed = jsonForecast["wind"]?["speed"]?.Value<double>() ?? 0f;
                        }
                        catch(Exception e)
                        {
                            Debug.WriteLine("An Error occured while parsing json: " + e.StackTrace);
                        }

                        forecasts.Add(forecast);
                    }
                }
            }
            return forecasts;
        }
    }
}
