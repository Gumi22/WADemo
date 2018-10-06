using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class WeatherForeCastDetailPage : ContentPage
    {
        public WeatherForeCastDetailPage(WeatherForeCast weatherForeCast)
        {
            InitializeComponent();
            Title = "Forecast detail";
            this.BindingContext = new WeatherForeCastViewModel(weatherForeCast);
        }
    }
}
