using System.Globalization;
using WeatherApp.Helpers;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class WeatherForeCastViewModel : BaseViewModel
    {
        private WeatherForeCastModel _weatherForeCastModel;

        public WeatherForeCastModel WeatherForeCastModel { get => _weatherForeCastModel; set => SetProperty(ref _weatherForeCastModel, value); }

        public WeatherForeCastViewModel(WeatherForeCastModel wf)
        {
            WeatherForeCastModel = wf;
        }

        public WeatherForeCastViewModel()
        {
            WeatherForeCastModel = new WeatherForeCastModel();
        }

        public string Icon => SystemSettings.PictureUrl + _weatherForeCastModel.Icon + SystemSettings.PictureFileEnding;
        public string Condition => _weatherForeCastModel.Condition.ToString();
        public string Description => _weatherForeCastModel.Description;
        public string Time => _weatherForeCastModel.Time.ToString(CultureInfo.CurrentCulture);
        public string Temperature => _weatherForeCastModel.Temperature.ToString(CultureInfo.CurrentCulture) + " " + SystemSettings.MeasurementUnitTemperature;
        public string Pressure => _weatherForeCastModel.Pressure.ToString(CultureInfo.CurrentCulture) + " hPa";
        public string Humidity => _weatherForeCastModel.Humidity.ToString(CultureInfo.CurrentCulture) + " %";
        public string CloudCover => _weatherForeCastModel.CloudCover.ToString(CultureInfo.CurrentCulture) + " %";
        public string WindSpeed => _weatherForeCastModel.WindSpeed.ToString(CultureInfo.CurrentCulture) + " " + SystemSettings.MeasurementUnitSpeed;
        public string WindDirection => _weatherForeCastModel.WindDirection.ToString(CultureInfo.CurrentCulture) + " deg";
        public string Rain => _weatherForeCastModel.Rain.ToString(CultureInfo.CurrentCulture) + " " + SystemSettings.MeasurementUnitLength;
        public string Snow => _weatherForeCastModel.Snow.ToString(CultureInfo.CurrentCulture) + " " + SystemSettings.MeasurementUnitLength;
    }
}
