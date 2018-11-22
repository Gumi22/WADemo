using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp.Helpers
{
    class WeatherForeCastDB
    {
        readonly SQLiteAsyncConnection database;
        static WeatherForeCastDB _instance;
        static readonly object _syncRoot = new object();
        public event EventHandler DataUpdated;

        public WeatherForeCastDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<WeatherForeCastModel>().Wait();
        }

        public static WeatherForeCastDB Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new WeatherForeCastDB(
                                DependencyService.Get<IFilePathService>().
                                    GetLocalFilePath("WeatherForeCastDB.sqlite"));
                        }
                    }
                }
                return _instance;
            }
        }

        public Task<List<WeatherForeCastModel>> GetItemsAsync()
        {
            return database.QueryAsync<WeatherForeCastModel>("SELECT * FROM weatherforecast ORDER BY time ASC");
        }

        public async Task<int> SaveItemsAsync(List<WeatherForeCastModel> items)
        {
            int ret = 0;
            if (items.FindAll(item => item.Id != 0).Count == 0)
                ret = await database.InsertAllAsync(items);
            else if(items.FindAll(item => item.Id == 0).Count == 0)
                ret = await database.UpdateAllAsync(items);
            else
            {
                foreach (var item in items)
                {
                    if (item.Id != 0)
                        ret += await database.InsertAsync(items);
                    else
                        ret += await database.UpdateAsync(items);
                }
            }
            if (ret > 0)
                NotifyDataUpdated();
            return ret;
        }

        void NotifyDataUpdated()
        {
            var handler = DataUpdated;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
