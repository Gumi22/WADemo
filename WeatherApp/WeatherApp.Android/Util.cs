using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.App.Job;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Debug = System.Diagnostics.Debug;

namespace WeatherApp.Droid
{
    static class Util
    {
        public static void ScheduleJob(Context context)
        {
            Debug.WriteLine(typeof(BackgroundDataLoaderJobService));
            var javaClass = Java.Lang.Class.FromType(typeof(BackgroundDataLoaderJobService));
            var jobBuilder = new JobInfo.Builder(1, new ComponentName(context, javaClass))
                .SetRequiredNetworkType(NetworkType.NotRoaming);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                Debug.WriteLine("Service min period: " + JobInfo.MinPeriodMillis);
                jobBuilder.SetPeriodic(3600000, 10000); // => 60 min +/- 10 Seconds
            }
            else
            {
                jobBuilder.SetMinimumLatency(3595000); // min => 59 min 55 Seconds
                jobBuilder.SetOverrideDeadline(3605000); // max => 60 min 05 Seconds
            }

            Debug.WriteLine("Starting Job");
            JobScheduler scheduler = context.GetSystemService(Context.JobSchedulerService) as JobScheduler;
            scheduler?.Schedule(jobBuilder.Build());
        }
    }
}