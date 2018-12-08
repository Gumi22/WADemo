using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WeatherApp;
using Foundation;
using UIKit;
using UserNotifications;
using WeatherApp.iOS;
using WeatherApp.Interfaces;
using Xamarin.Forms;

[assembly:Dependency(typeof(NotificationService))]
namespace WeatherApp.iOS
{
    class NotificationService : INotificationService
    {
        public void Init()
        {
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge,
                (b, error) =>
                {
                    //Handle approval
                    Debug.WriteLine("Yayyy, we have permissions to send notifications now :D" + b);
                    Debug.WriteLine("Oh nooo, we cant get the neccessary Authorization for notifications" + error);
                });
            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
        }
        public void SendNotification(string title, string id)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                UNUserNotificationCenter.Current.GetNotificationSettings(settings =>
                {
                    if (settings.AlertSetting != UNNotificationSetting.Enabled) return;

                    Debug.WriteLine("I want to notify you about sth: " + title);
                    var content = new UNMutableNotificationContent()
                    {
                        Title = "Notification",
                        Body = title
                    };
                    var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(1, false);
                    var request = UNNotificationRequest
                        .FromIdentifier(id, content, trigger);
                    UNUserNotificationCenter.Current.AddNotificationRequest(request,
                        error =>
                        {
                            if (error != null)
                            {
                                Debug.WriteLine("Upsie: " + error);
                            }
                        });
                });
            }
            else
            {
                Debug.WriteLine("The devices iOS Version should be 10 or higher", "error");
            }
        }

        internal class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
        {
            [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
            public override void WillPresentNotification(UNUserNotificationCenter center, 
                UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
            {
                Debug.WriteLine("Active Notification: " + notification);
                completionHandler(UNNotificationPresentationOptions.Alert);
            }

            [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
            public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response,
                Action completionHandler)
            {
                switch (response.ActionIdentifier)
                {
                    default:
                        if (response.IsDefaultAction)
                        {
                            Debug.WriteLine("here you should start navigation to subpage after Data is loadded from DB");
                        }else if (response.IsDismissAction)
                        {
                            Debug.WriteLine("handling custom dismiss action");
                        }

                        break;
                }
            }
        }
    }
}