using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Options.IsEnabled = DependencyService.Get<ISettingsService>().SettingsDialogueAvailable;

            DependencyService.Get<ISettingsService>().PropertyChanged += (sender, e) =>
            {
                DemoList.BeginRefresh();
                DemoList.RefreshCommand.Execute(DemoList);
                DemoList.EndRefresh();
            }; 

            //ToDo:
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
