using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WeatherApp;

namespace WeatherApp
{
    public class WeatherForeCastViewModel : BaseViewModel
    {
        private WeatherForeCast _weatherForeCast;

        public WeatherForeCast WeatherForeCast { get => _weatherForeCast; set => SetProperty(ref _weatherForeCast, value); }

        public WeatherForeCastViewModel(WeatherForeCast wf)
        {
            WeatherForeCast = wf;
        }

        public WeatherForeCastViewModel()
        {
            WeatherForeCast = new WeatherForeCast();
        }

        public string Icon => "http://openweathermap.org/img/w/" + _weatherForeCast.Icon + ".png";
        public string Condition => _weatherForeCast.Condition.ToString();
        public string Description => _weatherForeCast.Description;
        public string Time => _weatherForeCast.Time.ToString(CultureInfo.CurrentCulture);
        public string Temperature => _weatherForeCast.Temperature.ToString(CultureInfo.CurrentCulture) + " " + SystemSettings.MeasurementUnitTemperature;
        public string Pressure => _weatherForeCast.Pressure.ToString(CultureInfo.CurrentCulture) + " hPa";
        public string Humidity => _weatherForeCast.Humidity.ToString(CultureInfo.CurrentCulture) + " %";
        public string CloudCover => _weatherForeCast.CloudCover.ToString(CultureInfo.CurrentCulture) + " %";
        public string WindSpeed => _weatherForeCast.WindSpeed.ToString(CultureInfo.CurrentCulture) + " " + SystemSettings.MeasurementUnitSpeed;
        public string WindDirection => _weatherForeCast.WindDirection.ToString(CultureInfo.CurrentCulture) + " deg";
        public string Rain => _weatherForeCast.Rain.ToString(CultureInfo.CurrentCulture) + " " + SystemSettings.MeasurementUnitLength;
        public string Snow => _weatherForeCast.Snow.ToString(CultureInfo.CurrentCulture) + " " + SystemSettings.MeasurementUnitLength;
    }
}
