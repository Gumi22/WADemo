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
            var javaClass = Java.Lang.Class.FromType(typeof(BackgroundDataLoaderJobService));
            var jobBuilder = new JobInfo.Builder(1, new ComponentName(context, javaClass));

            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                Debug.WriteLine("Service min period: " + JobInfo.MinPeriodMillis);
                jobBuilder.SetPeriodic(900000, 1000); //15 min +/- 1 Sekunde
            }
            else
            {
                jobBuilder.SetMinimumLatency(5000);
                jobBuilder.SetOverrideDeadline(10000);
            }

            Debug.WriteLine("Starting Job");
            JobScheduler scheduler = context.GetSystemService(Context.JobSchedulerService) as JobScheduler;
            scheduler?.Schedule(jobBuilder.Build());
        }
    }
}