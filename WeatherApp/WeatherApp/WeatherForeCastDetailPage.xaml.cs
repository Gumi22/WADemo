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
            Title = "Weather Details";
            this.BindingContext = new WeatherForeCastViewModel(weatherForeCast);
        }
    }
}
