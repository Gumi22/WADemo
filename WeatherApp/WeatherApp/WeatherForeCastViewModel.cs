using System;
using System.Collections.Generic;
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
    }
}
