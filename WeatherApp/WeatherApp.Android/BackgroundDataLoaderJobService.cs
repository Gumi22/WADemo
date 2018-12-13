using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.App.Job;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WeatherApp.Helpers;
using Xamarin.Forms;
using Debug = System.Diagnostics.Debug;

namespace WeatherApp.Droid
{
    [Service(Name = "com.companyname.WeatherApp.BackgroundDataLoaderJobService", Permission = "android.permission.BIND_JOB_SERVICE", Exported = true)]
    class BackgroundDataLoaderJobService : JobService
    {
        private bool _reschedule = true;
        private Task _downloadTask;

        public override bool OnStartJob(JobParameters @params)
        {
            _reschedule = true;
            _downloadTask = Task.Run(async () =>
            {
                Debug.WriteLine("FETCH");
                try
                {
                    bool success = await WeatherDataFetcher.UpdateWeatherForecastDbAsync();
                    if (!success)
                    {
                        Debug.WriteLine("No new Data was fetched during background task");
                        _reschedule = true;
                    }

                    Debug.WriteLine("Background fetching SUCCESS");
                    _reschedule = false;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error occured during background fetching: " + e.StackTrace);
                    _reschedule = true;
                }

                Debug.WriteLine("Job finished @ " + DateTime.Now);
                JobFinished(@params, false);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
                {
                    Util.ScheduleJob(this);
                }
            });
            Debug.WriteLine("Service running @ " + DateTime.Now);
            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            Debug.WriteLine("Stopped Job");
            _downloadTask.Dispose();
            return _reschedule;
        }
    }
}