using System.Threading.Tasks;
using Android;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Support.V4.Content;
using Android.Util;
using Plugin.CurrentActivity;
using WeatherApp.Droid;
using WeatherApp.Models;
using Xamarin.Forms;


[assembly: Dependency(typeof(LocationService))]
namespace WeatherApp.Droid
{
    public class LocationService : ILocationService
    {
        public static readonly int RcInstallGooglePlayServices = 1000;
        public static readonly string Tag = "WeatherApp";
        private readonly FusedLocationProviderClient _fusedLocationProviderClient;

        public LocationService()
        {
            //ToDo: check if play services are available
            if (TestIfGooglePlayServicesIsInstalled())
            {
                _fusedLocationProviderClient =
                    LocationServices.GetFusedLocationProviderClient(CrossCurrentActivity.Current.AppContext);
            }
        }

        public Task<Location> GetLocation()
        {
            if (ContextCompat.CheckSelfPermission(CrossCurrentActivity.Current.AppContext,
                    Manifest.Permission.AccessCoarseLocation) == Permission.Granted)
            {
                return GetLocationInternAsync();
            }
            return Task.FromResult<Location>(null);
        }

        async Task<Location> GetLocationInternAsync()
        {
            //ToDo: fusedLocationProviderClient always returns null, no matter what I do -.-
            var location = await _fusedLocationProviderClient.GetLastLocationAsync();

            if (location == null)
            {
                return null;
            }
            else
            {
                return new Location() {Latitude = location.Latitude, Longitude = location.Longitude};
            }
        }

        bool TestIfGooglePlayServicesIsInstalled()
        {
            var queryResult =
                GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(CrossCurrentActivity.Current.AppContext);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info(Tag, "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error(Tag, "There is a problem with Google Play Services on this device: {0} - {1}", queryResult,
                    errorString);
            }

            return false;
        }
    }
}