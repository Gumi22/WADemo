using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Foundation;
using UIKit;
using WeatherApp;
using WeatherApp.iOS;
using WeatherApp.iOS.Annotations;

[assembly: Xamarin.Forms.Dependency(typeof(SettingsService))]

namespace WeatherApp.iOS
{
    class SettingsService : ISettingsService
    {
        private readonly NSObject _observer;

        public SettingsService()
        {
            _observer = NSNotificationCenter.DefaultCenter.AddObserver(NSUserDefaults.DidChangeNotification,
                UserDefaultsChanged, null);
        }

        ~SettingsService()
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(_observer);
        }

        private void UserDefaultsChanged(NSNotification obj)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public bool SettingsDialogueAvailable => false;

        public void DisplaySettings()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}