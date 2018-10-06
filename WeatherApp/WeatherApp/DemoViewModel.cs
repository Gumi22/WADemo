using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using WeatherApp;
using Xamarin.Forms;

namespace WeatherApp
{
    class DemoViewModel:BaseViewModel
    {
        private ObservableCollection<string> _demoList;

        public ObservableCollection<string> DemoList
        {
            get => _demoList;
            set => SetProperty(ref _demoList, value);
        }

        public Command LoadDataCommand { get; set; }

        public DemoViewModel()
        {
            Title = "DemoList";
            DemoList = new ObservableCollection<string>{"Alpha", "Beta", "Gamma"};
            LoadDataCommand = new Command(ExecuteLoadDataCommand);
        }

        private void ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                DemoList.Add("Omega");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ohhh no, an error occured: " + e.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
