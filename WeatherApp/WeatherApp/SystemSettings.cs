using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace WeatherApp
{
    class SystemSettings
    {
        private static ISettings AppSettings => CrossSettings.Current;
        
        public static string City
        {
            get => AppSettings.GetValueOrDefault("preference_city", "Vienna,at");
            set => AppSettings.GetValueOrDefault("preference_city", value);
        }

        public static string MeasurementUnit
        {
            get
            {
                string units = AppSettings.GetValueOrDefault("preference_measurement_unit", "2");
                switch (units)
                {
                    case "1":
                        return "imperial";
                    case "2":
                        return "metric";
                    case "3":
                        return "kelvin";
                    default:
                        return "metric";
                }
            }
            set => AppSettings.GetValueOrDefault("preference_measurement_unit", value);
        }

        public static string MeasurementUnitTemperature
        {
            get
            {
                string units = AppSettings.GetValueOrDefault("preference_measurement_unit", "2");
                switch (units)
                {
                    case "1":
                        return "°F";
                    case "2":
                        return "°C";
                    case "3":
                        return "°K";
                    default:
                        return "°C";
                }
            }
        }

        public static string MeasurementUnitLength
        {
            get
            {
                string units = AppSettings.GetValueOrDefault("preference_measurement_unit", "2");
                switch (units)
                {
                    case "1":
                        return "in";
                    case "2":
                        return "mm";
                    case "3":
                        return "mm";
                    default:
                        return "mm";
                }
            }
        }

        public static string MeasurementUnitSpeed
        {
            get
            {
                string units = AppSettings.GetValueOrDefault("preference_measurement_unit", "2");
                switch (units)
                {
                    case "1":
                        return "mph";
                    case "2":
                        return "kph";
                    case "3":
                        return "kph";
                    default:
                        return "kph";
                }
            }
        }


    }
}
