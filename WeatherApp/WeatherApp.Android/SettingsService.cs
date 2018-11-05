using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using WeatherApp.Droid;
using WeatherApp.Droid.Annotations;

[assembly: Xamarin.Forms.Dependency(typeof(SettingsService))]

namespace WeatherApp.Droid
{
    class SettingsService : Java.Lang.Object, ISettingsService, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        public bool SettingsDialogueAvailable => true;

        public SettingsService()
        {
            ISharedPreferences sharedPreferences =
                PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);
            sharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
        }

        ~SettingsService()
        {
            ISharedPreferences sharedPreferences =
                PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);
            sharedPreferences.UnregisterOnSharedPreferenceChangeListener(this);
        }

        public void DisplaySettings()
        {
            CrossCurrentActivity.Current.Activity.StartActivity(typeof(SettingsActivity));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
        }
    }
}