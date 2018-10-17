using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            LoadDataCommand.Execute(this);
        }

        public async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                var forecasts = await new WeatherForeCastService().GetItemsAsync();
                WeatherForeCastList = new ObservableCollection<WeatherForeCast>(forecasts);
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
