using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace WeatherApp
{
    class WeatherForeCastService
    {
        private string _apiKey = "APPID=5eb65027eb18fb0a14326ba27bfc58bc";
        private string _apiCity = "q=Vienna,at";
        private string _apiUnitFormat = "units=metric";
        private string _apiBaseUrl = "https://api.openweathermap.org/data/2.5/forecast";

        public string ApiUrl { get; set; }

        

        public WeatherForeCastService()
        {
            ApiUrl = $"{_apiBaseUrl}?{_apiCity}&{_apiKey}&{_apiUnitFormat}";
        }

        public async Task<IEnumerable<WeatherForeCast>> GetItemsAsync()
        {
            try
            {
                var request = WebRequest.Create(ApiUrl);
                request.ContentType = "application/json";
                request.Method = "GET";

                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    IEnumerable<WeatherForeCast> ret = await Task.Run(() =>
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
                Debug.WriteLine($"Error while loading data: {webException.Message}");
                return null;
            }
        }

    }
}
