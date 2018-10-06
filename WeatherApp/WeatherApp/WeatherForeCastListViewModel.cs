using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherApp;
using Xamarin.Forms;

namespace WeatherApp
{
    class WeatherForeCastListViewModel : BaseViewModel
    {
        private ObservableCollection<WeatherForeCast> _weatherForeCastList;

        public ObservableCollection<WeatherForeCast> WeatherForeCastList
        {
            get => _weatherForeCastList;
            set => SetProperty(ref _weatherForeCastList, value);
        }

        public Command LoadDataCommand { get; set; }

        public WeatherForeCastListViewModel()
        {
            Title = "Forecasts";

            _weatherForeCastList = new ObservableCollection<WeatherForeCast>
            {
                new WeatherForeCast(),
                new WeatherForeCast(),
                new WeatherForeCast(),
                new WeatherForeCast(),
                new WeatherForeCast(),
                new WeatherForeCast(),
                new WeatherForeCast(),
                new WeatherForeCast(),
                new WeatherForeCast()
            };

            LoadDataCommand = new Command(ExecuteLoadDataCommand);
        }

        private void ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                WeatherForeCastList.Add(new WeatherForeCast());
            }
            catch (Exception e)
            {
                Console.WriteLine("Ohhh no, an error occured: " + e.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
