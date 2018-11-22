using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class WeatherForeCastDetailPage : ContentPage
    {
        public WeatherForeCastDetailPage(WeatherForeCastModel weatherForeCastModel)
        {
            InitializeComponent();
            Title = "Weather Details";
            this.BindingContext = new WeatherForeCastViewModel(weatherForeCastModel);
        }
    }
}
