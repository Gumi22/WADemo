using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Location;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using WeatherApp.Droid;
using WeatherApp.Models;

[assembly: Dependency(typeof(LocationService))]

namespace WeatherApp.Droid
{
    public class LocationService : ILocationService
    {
        private FusedLocationProviderClient fusedLocationProviderClient;

        public LocationService()
        {
            //ToDo: check if play services are available
            fusedLocationProviderClient =
                LocationServices.GetFusedLocationProviderClient(CrossCurrentActivity.Current.AppContext);
        }

        public Task<Location> GetLocation()
        {
            if(ContextCompat.CheckSelfPermission(CrossCurrentActivity.Current.AppContext, Manifest.Permission.AccessCoarseLocation) == Permission.Granted)
                return GetLocationInternAsync();
            return Task.FromResult<Location>(null);
        }

        async Task<Location> GetLocationInternAsync()
        {
            var location = await fusedLocationProviderClient.GetLastLocationAsync();

            if (location == null)
            {
                return null;
            }
            else
            {
                return new Location() {Latitude = location.Latitude, Longitude = location.Longitude};
            }
        }
    }
}