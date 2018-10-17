using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp;
using Xamarin.Forms;

namespace WeatherApp
{
    public class WeatherForeCast : ObservableObject
    {
        private string _icon;
        private int _condition;
        private DateTime _time;
        private double _temperature; // in C°
        private double _pressure; // in hpA
        private double _humidity; // in percent
        private double _cloudCover; // in percent
        private double _windSpeed; // in km/h
        private double _windDirection; //in degrees
        private double _rain; //rain im mm for the last 3 hours
        private double _snow; //snow in mm for the last 3 hours

        public string Icon { get => _icon; set => SetProperty(ref _icon, value); }
        public int Condition { get => _condition; set => SetProperty(ref _condition ,value); }
        public DateTime Time { get => _time; set => SetProperty(ref _time, value); }
        public double Temperature { get => _temperature; set => SetProperty(ref _temperature, value); }
        public double Pressure { get => _pressure; set => SetProperty(ref _pressure, value); }
        public double Humidity { get => _humidity; set => SetProperty(ref _humidity, value); }
        public double CloudCover { get => _cloudCover; set => SetProperty(ref _cloudCover, value); }
        public double WindSpeed { get => _windSpeed; set => SetProperty(ref _windSpeed, value); }
        public double WindDirection { get => _windDirection; set => SetProperty(ref _windDirection, value); }
        public double Rain { get => _rain; set => SetProperty(ref _rain, value); }
        public double Snow { get => _snow; set => SetProperty(ref _snow, value); }


        public WeatherForeCast()
        {
            Icon = "";
            Condition = 0;
            Time = DateTime.Now;
            Temperature = 0f;
            Humidity = 0f;
            Pressure = 0f;
            CloudCover = 0f;
            WindDirection = 0f;
            WindSpeed = 0f;
            Rain = 0f;
            Snow = 0f;
        }

        private void SetRandomValues()
        {
            Random rnd = new Random();
            Icon = "C://" + rnd.Next();
            Condition = rnd.Next();
            Time = RandomDay(rnd);
            Temperature = rnd.NextDouble();
            Humidity = rnd.NextDouble();
            Pressure = rnd.NextDouble();
            CloudCover = rnd.NextDouble();
            WindDirection = rnd.NextDouble();
            WindSpeed = rnd.NextDouble();
            Rain = rnd.NextDouble();
            Snow = rnd.NextDouble();
        }

        private DateTime RandomDay(Random gen)
        {
            DateTime start = DateTime.Now;
            int range = 60 * 24 * 31;
            return start.AddMinutes(gen.Next(range));
        }

    }
}
