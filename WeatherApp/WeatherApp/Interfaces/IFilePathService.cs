using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Interfaces
{
    public interface IFilePathService
    {
        string GetLocalFilePath(string filename);
    }
}
