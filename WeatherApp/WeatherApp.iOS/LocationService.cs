using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLocation;
using Foundation;
using UIKit;
using WeatherApp.iOS;
using WeatherApp.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationService))]
namespace WeatherApp.iOS
{
    class LocationService : ILocationService
    {
        readonly CLLocationManager locationManager;

        public LocationService()
        {

            locationManager = new CLLocationManager();
        }
        public Task<Location> GetLocation()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                locationManager.RequestWhenInUseAuthorization();
            }

            if (locationManager.Location != null)
            {
                return Task.FromResult(new Location
                {
                    Longitude = locationManager.Location.Coordinate.Longitude,
                    Latitude = locationManager.Location.Coordinate.Latitude
                });
            }
            else
            {
                return Task.FromResult<Location>(null);
            }
            throw new NotImplementedException();
        }
    }
}