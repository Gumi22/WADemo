using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Helpers;
using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        int _mId;
        public MainPage(int id)
        {
            _mId = id;
            InitializeComponent();

            Options.IsEnabled = DependencyService.Get<ISettingsService>().SettingsDialogueAvailable;

            DependencyService.Get<ISettingsService>().PropertyChanged += (sender, e) =>
            {
                DemoList.BeginRefresh();
                DemoList.RefreshCommand.Execute(DemoList);
                DemoList.EndRefresh();
            };

            if (WeatherDataFetcher.LastUpDate.Date <= DateTime.Now.AddDays(-2)) //is this how you use async things???
            {
                WeatherDataFetcher.UpdateWeatherForecastDbAsync();
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_mId >= 0)
            {
                var id = _mId;
                _mId = -1;
                var forecast = await WeatherForeCastDB.Instance.GetItemAsync(id);
                await Task.Delay(100);
                ((Application.Current as App)?.MainPage as NavigationPage)?.PushAsync(new WeatherForeCastDetailPage(forecast));
            }

            //ToDo: Get The Location: or change position
            var location = await DependencyService.Get<ILocationService>().GetLocation();
            try
            {
                Debug.WriteLine($"My Location: {location?.Latitude ?? double.NaN}, {location?.Longitude ?? double.NaN}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void ItemClick(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                if (e.SelectedItem is WeatherForeCastListItemViewModel item)
                {
                    if (item.WeatherForeCastModel != null)
                    {
                        ((Application.Current as App)?.MainPage as NavigationPage)?.PushAsync(new WeatherForeCastDetailPage(item.WeatherForeCastModel));
                    }
                }
                DemoList.SelectedItem = null;
            }
        }

        private void OpenSettings_Clicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<ISettingsService>().DisplaySettings();
        }
    }
}
