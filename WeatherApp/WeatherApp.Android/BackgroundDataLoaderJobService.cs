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
using Debug = System.Diagnostics.Debug;

namespace WeatherApp.Droid
{
    [Service(Name= "com.companyname.WeatherApp.BackgroundDataLoaderJobService", Permission = "android.permissions.BIND_JOB_SERVICE")]
    class BackgroundDataLoaderJobService : JobService
    {
        public override bool OnStartJob(JobParameters @params)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
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
            return false; //No rescheduling required
        }
    }
}