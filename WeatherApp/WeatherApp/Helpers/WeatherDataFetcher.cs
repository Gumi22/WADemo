using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Helpers
{
    public static class WeatherDataFetcher
    {
        public static DateTime LastUpDate { get; private set; }

        public static async Task<bool> UpdateWeatherForecastDbAsync()
        {
            try
            {
                var forecasts = await new WeatherForeCastService().GetItemsAsync();
                var ids = await WeatherForeCastDB.Instance.SaveItemsAsync(await GetDifferences(forecasts));
                if (ids > 0)
                {
                    LastUpDate = DateTime.Now;
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

        private static async Task<List<WeatherForeCastModel>> GetDifferences(List<WeatherForeCastModel> newForecasts)
        {
            var changedForecasts = new List<WeatherForeCastModel>();
            if (newForecasts == null || newForecasts.Count <= 0)
                return changedForecasts;

            newForecasts.Sort((x, y) => x.Time.CompareTo(y.Time));
            var oldForeCasts = await WeatherForeCastDB.Instance.GetItemsAscTimeAsync();
            oldForeCasts.Sort((x, y) => x.Time.CompareTo(y.Time));

            //Get the offset of the new Forecasts:
            int offset = 0;
            while (offset < oldForeCasts.Count && newForecasts[0].Time > oldForeCasts[offset].Time)
            {
                offset++;
            }

            if (offset >= oldForeCasts.Count)
                return newForecasts;


            for (int i = 0; i < newForecasts.Count; i ++)
            {
                if (offset < oldForeCasts.Count)
                {
                    newForecasts[i].Id = oldForeCasts[offset].Id;
                    changedForecasts.Add(newForecasts[i]);
                }
                else
                {
                    changedForecasts.Add(newForecasts[i]);
                }
                offset++;
            }

            return changedForecasts;
        }
    }
}
