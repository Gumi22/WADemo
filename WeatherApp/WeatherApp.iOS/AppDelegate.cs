﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Foundation;
using UIKit;
using WeatherApp.Helpers;

namespace WeatherApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(60*60); // => 3600 seconds

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(-1));

            return base.FinishedLaunching(app, options);
        }

        [Export("application:performFetchWithCompletionHandler:")]
        public override void PerformFetch(UIApplication app, Action<UIBackgroundFetchResult> completionHandler)
        {
            Debug.WriteLine("FETCH");
            try
            {
                bool success = WeatherDataFetcher.UpdateWeatherForecastDbAsync().Result;
                if (!success)
                {
                    completionHandler(UIBackgroundFetchResult.NoData);
                }

                completionHandler(UIBackgroundFetchResult.NewData);
            }
            catch(Exception e)
            {
                Debug.WriteLine("Error occured during Background Fetching: " + e.StackTrace);
            }
        }
    }
}