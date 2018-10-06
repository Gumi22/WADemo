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
    }
}
