using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Models;

namespace WeatherApp.Interfaces
{
    public interface INotificationService
    {
        void Init();
        void SendNewForecastNotification(WeatherForeCastModel forecast);
    }
}
