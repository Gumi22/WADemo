using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    class WeatherForeCastListItemViewModel : WeatherForeCastViewModel
    {
        private WeatherForeCastListViewModel _parent;
        public WeatherForeCastListViewModel Parent { get => _parent; set => SetProperty(ref _parent, value); }

        public bool IsFirst => Parent?.IsFirst(this) ?? false;

        public WeatherForeCastListItemViewModel(WeatherForeCastListViewModel parent, WeatherForeCastModel model)
        {
            WeatherForeCastModel = model;
            Parent = parent;
        }

    }
}
