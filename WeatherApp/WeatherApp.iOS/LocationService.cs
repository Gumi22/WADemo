using System;
using System.Threading.Tasks;
using CoreLocation;
using UIKit;
using WeatherApp.iOS;
using WeatherApp.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationService))]
namespace WeatherApp.iOS
{
    class LocationService : ILocationService
    {
        readonly CLLocationManager _locationManager;

        public LocationService()
        {

            _locationManager = new CLLocationManager();
        }
        public Task<Location> GetLocation()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                _locationManager.RequestWhenInUseAuthorization();
            }

            if (_locationManager.Location != null)
            {
                return Task.FromResult(new Location
                {
                    Longitude = _locationManager.Location.Coordinate.Longitude,
                    Latitude = _locationManager.Location.Coordinate.Latitude
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