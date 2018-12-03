using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Helpers
{
    public static class WeatherDataFetcher
    {
        public static async Task<bool> UpdateWeatherForecastDbAsync()
        {
            try
            {
                var forecasts = await new WeatherForeCastService().GetItemsAsync();
                var ids = await WeatherForeCastDB.Instance.SaveItemsAsync(forecasts); //.Result is used here, because doing things asynchronously here wouldn't work
                if (ids > 0)
                {
                    return true;
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("No Data was updated, error was: " + e.StackTrace);
                throw;
            }

            return false;
        }
    }
}
