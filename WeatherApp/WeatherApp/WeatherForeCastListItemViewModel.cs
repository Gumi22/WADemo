using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp
{
    class WeatherForeCastListItemViewModel : BaseViewModel
    {
        private WeatherForeCast _forecast;
        private WeatherForeCastListViewModel _parent;
        
        public WeatherForeCast Forecast { get => _forecast; set => SetProperty(ref _forecast, value); }
        public WeatherForeCastListViewModel Parent { get => _parent; set => SetProperty(ref _parent, value); }

        public string Icon => Forecast.Icon;
        public string Time => Forecast.Time.ToString();
        public string Temperature => Forecast.Temperature.ToString();

        public bool IsFirst => Parent?.IsFirst(this) ?? false;

        public WeatherForeCastListItemViewModel(WeatherForeCastListViewModel parent, WeatherForeCast model)
        {
            Forecast = model;
            Parent = parent;
        }

    }
}
