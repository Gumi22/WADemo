using System;
using WeatherApp.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WeatherApp
{
    public partial class App : Application
    {
        public App(int id)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage(id));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            DependencyService.Get<INotificationService>().Init();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
