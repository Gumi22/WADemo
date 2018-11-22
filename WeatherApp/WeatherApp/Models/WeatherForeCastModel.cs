using System;
using SQLite;

namespace WeatherApp.Models
{
    [Table("weatherforecast")]
    public class WeatherForeCastModel : ObservableObject
    {
        private string _icon;
        private int _condition;
        private string _description;
        private DateTime _time;
        private double _temperature; // in unit
        private double _pressure; // in hpA
        private double _humidity; // in percent
        private double _cloudCover; // in percent
        private double _windSpeed; // in km/h
        private double _windDirection; //in degrees
        private double _rain; //rain im mm for the last 3 hours
        private double _snow; //snow in mm for the last 3 hours

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Column("icon")]
        public string Icon { get => _icon; set => SetProperty(ref _icon, value); }
        [Column("condition")]
        public int Condition{ get => _condition; set => SetProperty(ref _condition, value); }
        [Column("description")]
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        [Column("time")]
        public DateTime Time { get => _time; set => SetProperty(ref _time, value); }
        [Column("temperature")]
        public double Temperature { get => _temperature; set => SetProperty(ref _temperature, value); }
        [Column("pressure")]
        public double Pressure { get => _pressure; set => SetProperty(ref _pressure, value); }
        [Column("humidity")]
        public double Humidity { get => _humidity; set => SetProperty(ref _humidity, value); }
        [Column("cloudcover")]
        public double CloudCover { get => _cloudCover; set => SetProperty(ref _cloudCover, value); }
        [Column("windspeed")]
        public double WindSpeed { get => _windSpeed; set => SetProperty(ref _windSpeed, value); }
        [Column("winddirection")]
        public double WindDirection { get => _windDirection; set => SetProperty(ref _windDirection, value); }
        [Column("rain")]
        public double Rain { get => _rain; set => SetProperty(ref _rain, value); }
        [Column("snow")]
        public double Snow { get => _snow; set => SetProperty(ref _snow, value); }


        public WeatherForeCastModel()
        {
            Id = 0;
            Icon = "";
            Condition = 0;
            Description = "";
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

    }
}
