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
                        try{forecast.CloudCover = jsonForecast["clouds"]["all"].Value<double?>().Value;}
                        catch{}
                        try{forecast.Condition = jsonForecast["weather"][0]["id"].Value<int?>().Value;}
                        catch { }
                        try{forecast.Humidity = jsonForecast["main"]["humidity"].Value<double?>().Value; }
                        catch { }
                        try{forecast.Icon = jsonForecast["weather"][0]["icon"].Value<String>(); }
                        catch { }
                        try{forecast.Pressure = jsonForecast["main"]["pressure"].Value<double?>().Value; }
                        catch { }
                        try{forecast.Rain = jsonForecast["rain"]["3h"].Value<double>(); }
                        catch { }
                        try{forecast.Snow = jsonForecast["snow"]["3h"].Value<double?>().Value; }
                        catch { }
                        try{forecast.Temperature = jsonForecast["main"]["temp"].Value<double?>().Value; }
                        catch { }
                        try{forecast.Time = jsonForecast["dt"].Value<DateTime?>().Value; }
                        catch { }
                        try{forecast.WindDirection = jsonForecast["wind"]["deg"].Value<double?>().Value; }
                        catch { }
                        try{forecast.WindSpeed = jsonForecast["wind"]["speed"].Value<double?>().Value; }
                        catch { }

                        forecasts.Add(forecast);
                    }
                }
            }
            return forecasts;
        }
    }
}
