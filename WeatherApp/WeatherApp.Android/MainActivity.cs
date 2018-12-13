using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;

namespace WeatherApp.Droid
{
    [Activity(Label = "WeatherApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Util.ScheduleJob(this);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            var id = Intent.Extras?.GetInt("ID", -1) ?? -1;
            System.Diagnostics.Debug.WriteLine("ID: " + id);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(id));
        }

        public Context AppContext => this.ApplicationContext;
    }
}