using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace WeatherApp
{
    public interface ISettingsService : INotifyPropertyChanged
    {
        bool SettingsDialogueAvailable { get; }

        void DisplaySettings();
    }
}
