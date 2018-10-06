using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp
{
    public class BaseViewModel : ObservableObject
    {
        private string _title = string.Empty;
        private bool _busy = false;

        public string Title { get => _title; set => SetProperty( ref _title,  value); }
        public bool IsBusy { get => _busy; set => SetProperty( ref _busy, value); }
    }
}
