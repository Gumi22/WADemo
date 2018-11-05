using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Options.IsEnabled = DependencyService.Get<ISettingsService>().SettingsDialogueAvailable;

            DependencyService.Get<ISettingsService>().PropertyChanged += async (sender, e) =>
            {
                DemoList.BeginRefresh();

                WeatherForeCastService service = new WeatherForeCastService();
                service.ApiCity = SystemSettings.City;
                service.ApiUnitFormat = SystemSettings.MeasurementUnit;
                IEnumerable<WeatherForeCast> list = await service.GetItemsAsync();
                DemoList.ItemsSource = list;

                DemoList.EndRefresh();
            }; 
        }

        private void ItemClick(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = e.SelectedItem as WeatherForeCast;
                ((Application.Current as App)?.MainPage as NavigationPage)?.PushAsync(new WeatherForeCastDetailPage(item));
                DemoList.SelectedItem = null;
            }
        }

        void OpenSettings_Clicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<ISettingsService>().DisplaySettings();
        }
    }
}
