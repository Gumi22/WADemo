using System;
using Android.App;
using Android.Content;
using Android.OS;
using WeatherApp.Droid;
using Plugin.CurrentActivity;
using WeatherApp.Helpers;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationService))]
namespace WeatherApp.Droid
{
    public class NotificationService : INotificationService
    {
        static readonly String CHANNEL_ID = "my_channel_01";// The id of the channel.

        public NotificationService()
        {
        }

        public void Init()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel mChannel = new NotificationChannel(CHANNEL_ID, CHANNEL_ID, NotificationImportance.High);
                NotificationManager notificationManager =
                    CrossCurrentActivity.Current.Activity.GetSystemService(Context.NotificationService) as NotificationManager;
                notificationManager.CreateNotificationChannel(mChannel);
            }
        }

        public void SendNewForecastNotification(WeatherForeCastModel forecast)
        {
            Notification.Builder notificationBuilder = null;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                notificationBuilder = new Notification.Builder(CrossCurrentActivity.Current.Activity, CHANNEL_ID);
            }
            else
            {
#pragma warning disable CS0618 // Type or member is obsolete
                notificationBuilder = new Notification.Builder(CrossCurrentActivity.Current.Activity);
#pragma warning restore CS0618 // Type or member is obsolete
            }

            notificationBuilder
                .SetContentTitle("New  Forecast for " + forecast.Time)
                .SetContentText($"{forecast.Temperature} {SystemSettings.MeasurementUnitTemperature}, {forecast.Description}")
                .SetSmallIcon(Android.App.Application.Context.Resources.GetIdentifier("pic" + forecast.Icon, "drawable", "com.companyname.WeatherApp"))
                .SetAutoCancel(true);

            // When the user clicks the notification, SecondActivity will start up.
            Intent resultIntent = new Intent(CrossCurrentActivity.Current.Activity, typeof(MainActivity));
            resultIntent.PutExtra("ID", forecast.Id);

            // Construct a back stack for cross-task navigation:
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(CrossCurrentActivity.Current.Activity);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            PendingIntent resultPendingIntent =
                stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);
            notificationBuilder.SetContentIntent(resultPendingIntent);

            Notification notification = notificationBuilder.Build();
            NotificationManager notificationManager =
                CrossCurrentActivity.Current.Activity.GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.Notify(forecast.Id, notification);

        }
    }
}
