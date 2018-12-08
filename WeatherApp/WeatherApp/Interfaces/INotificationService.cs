using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Interfaces
{
    public interface INotificationService
    {
        void Init();
        void SendNotification(string title, string id);
    }
}
