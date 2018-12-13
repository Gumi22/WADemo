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
            Debug.WriteLine("lululululu" + id);
            InitializeComponent();

            Options.IsEnabled = DependencyService.Get<ISettingsService>().SettingsDialogueAvailable;

            DependencyService.Get<ISettingsService>().PropertyChanged += (sender, e) =>
            {
                DemoList.BeginRefresh();
                DemoList.RefreshCommand.Execute(DemoList);
                DemoList.EndRefresh();
            }; 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("lalalalala" + _mId);
            if (_mId >= 0)
            {
                var id = _mId;
                _mId = -1;
                var forecast = WeatherForeCastDB.Instance.GetItemAsync(id).Result;
                Debug.WriteLine("lololololololololo");
                Task.Delay(5000).RunSynchronously();
                Debug.WriteLine("lololololololololo");
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
