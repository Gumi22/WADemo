using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherApp.Helpers;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
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

            WeatherForeCastDB.Instance.DataUpdated += DatabaseUpdated;

#pragma warning disable CS4014 // Await ist hier denke ich unnötig, weil wir nicht auf ein Ergebnis warten und wir nicht von dem Status hier abhängig sind
            ExecuteLoadItemsCommand();
#pragma warning restore CS4014
        }

        public async Task ExecuteLoadDataCommand()
        {
            if (IsBusy){
                return;
            }

            try
            {
                IsBusy = true;
                var forecasts = await new WeatherForeCastService().GetItemsAsync();
                var ids = await WeatherForeCastDB.Instance.SaveItemsAsync(forecasts);
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

        async Task ExecuteLoadItemsCommand()
        {
            var items = await WeatherForeCastDB.Instance.GetItemsAsync();
            var itemList = new List<WeatherForeCastListItemViewModel>();
            foreach (var fc in items)
            {
                itemList.Add(new WeatherForeCastListItemViewModel(this, fc));
            }
            WeatherForeCastList = new ObservableCollection<WeatherForeCastListItemViewModel>(itemList);
        }

        async void DatabaseUpdated(object sender, EventArgs e)
        {
            await ExecuteLoadItemsCommand();
        }
    }
}
