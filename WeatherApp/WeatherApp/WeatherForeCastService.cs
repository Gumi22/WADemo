using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp
{
    class WeatherForeCastService
    {
        private string _apiKey = "APPID=5eb65027eb18fb0a14326ba27bfc58bc";
        private string _apiCity = "Vienna,at";
        private string _apiUnitFormat = "metric";
        private string _apiBaseUrl = "https://api.openweathermap.org/data/2.5/forecast";

        public string ApiUrl => $"{_apiBaseUrl}?q={_apiCity}&{_apiKey}&units={_apiUnitFormat}";
        public string ApiCity { get => _apiCity; set => _apiCity = value; }
        public string ApiUnitFormat { get => _apiUnitFormat; set => _apiUnitFormat = value; }

        public WeatherForeCastService()
        {}
        
        public async Task<List<WeatherForeCastModel>> GetItemsAsync()
        {
            try
            {
                var request = WebRequest.Create($"{_apiBaseUrl}?q={_apiCity}&{_apiKey}&units={_apiUnitFormat}");
                request.ContentType = "application/json";
                request.Method = "GET";

                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    var ret = await Task.Run(() =>
                    {

                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            Debug.WriteLine($"Error fetching data. Server returned status code {response.StatusCode}");
                            return null;
                        }

                        return new JsonForeCastConverter().GetForeCasts(response.GetResponseStream());
                    });

                    return ret;
                }
            }
            catch (WebException webException)
            {
                Debug.WriteLine($"Error while loading data: {webException.Message} : {webException.StackTrace}");
                return null;
            }
        }

    }
}
