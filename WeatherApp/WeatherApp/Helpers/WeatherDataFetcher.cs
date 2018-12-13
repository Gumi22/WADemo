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
            newForecasts.Sort((x, y) => x.Time.CompareTo(y.Time));
            var changedForecasts = new List<WeatherForeCastModel>();
            var oldForeCasts = await WeatherForeCastDB.Instance.GetItemsAscTimeAsync();
            
            for (int i = 0, y = 0; i < newForecasts.Count; i ++)
            {
                //ToDo: compare first new element to first old, if Date 3hours after: y++, else: add to changed, but if it is too similar, copy its id
                changedForecasts = newForecasts;
            }

            return changedForecasts;
        }
    }
}
