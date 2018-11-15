using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WeatherApp
{
    class WeatherForeCastListItemViewModel : WeatherForeCastViewModel
    {
        private WeatherForeCastListViewModel _parent;
        public WeatherForeCastListViewModel Parent { get => _parent; set => SetProperty(ref _parent, value); }

        public bool IsFirst => Parent?.IsFirst(this) ?? false;

        public WeatherForeCastListItemViewModel(WeatherForeCastListViewModel parent, WeatherForeCast model)
        {
            WeatherForeCast = model;
            Parent = parent;
        }

    }
}
