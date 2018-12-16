using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp
{
    public interface ILocationService
    {
        Task<Location> GetLocation();
    }
}
