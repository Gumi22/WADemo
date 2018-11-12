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
        private ObservableCollection<WeatherForeCastListItemViewModel> _weatherForeCastList;

        public ObservableCollection<WeatherForeCastListItemViewModel> WeatherForeCastList
        {
            get => _weatherForeCastList;
            set => SetProperty(ref _weatherForeCastList, value);
        }

        public bool IsFirst(WeatherForeCastListItemViewModel vm)
        {
            return _weatherForeCastList.IndexOf(vm) == 0;
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
                var itemList = new List<WeatherForeCastListItemViewModel>();
                foreach (var fc in forecasts)
                {
                    itemList.Add(new WeatherForeCastListItemViewModel(this, fc));
                }
                WeatherForeCastList = new ObservableCollection<WeatherForeCastListItemViewModel>(itemList);
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
