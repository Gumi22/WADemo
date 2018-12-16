using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Plugin.CurrentActivity;

namespace WeatherApp.Droid
{
    [Activity(Label = "WeatherApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int RequestLocationId = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Util.ScheduleJob(this);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            GetLocationPermission();

            var id = Intent.Extras?.GetInt("ID", -1) ?? -1;
            System.Diagnostics.Debug.WriteLine("ID: " + id);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(id));
        }

        void GetLocationPermission()
        {
            var permission = Manifest.Permission.AccessCoarseLocation;
            string[] permissionLocation = {permission};
            if (ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted)
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(this, permission))
                {
                    Snackbar.Make(FindViewById(Android.Resource.Id.Content),
                            "Location access is required for showing current weather data for your current location",
                            Snackbar.LengthIndefinite)
                        .SetAction("OK",
                            v => ActivityCompat.RequestPermissions(this, permissionLocation, RequestLocationId))
                        .Show();
                }
                else
                {
                    ActivityCompat.RequestPermissions(this, permissionLocation, RequestLocationId);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            switch (requestCode)
            {
                case RequestLocationId:
                {
                    if (grantResults[0] == Permission.Granted)
                    {
                        //Permission was newly granted
                    }
                    else
                    {
                        //permission denied, show user message
                    }

                    break;
                }
            }
        }

        public Context AppContext => this.ApplicationContext;
    }
}