using System;
using System.IO;
using WeatherApp.iOS;
using WeatherApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathService))]
namespace WeatherApp.iOS
{
    class FilePathService : IFilePathService
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, filename);
        }
    }
}