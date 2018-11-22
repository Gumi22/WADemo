using System;
using System.IO;
using WeatherApp.Droid;
using WeatherApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathService))]
namespace WeatherApp.Droid
{
    class FilePathService : IFilePathService
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}