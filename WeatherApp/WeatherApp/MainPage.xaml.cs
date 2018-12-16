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

            //ToDo: Get The Location: or change position
            var location = DependencyService.Get<ILocationService>().GetLocation().Result; //returns null be careful
            Debug.WriteLine($"Location: {location?.Latitude??double.NaN}, {location?.Longitude??double.NaN}");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_mId >= 0)
            {
                var id = _mId;
                _mId = -1;
                var forecast = WeatherForeCastDB.Instance.GetItemAsync(id).Result;
                await Task.Delay(100);
                ((Application.Current as App)?.MainPage as NavigationPage)?.PushAsync(new WeatherForeCastDetailPage(forecast));
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
