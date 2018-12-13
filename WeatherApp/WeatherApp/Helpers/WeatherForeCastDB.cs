using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp.Helpers
{
    public class WeatherForeCastDB
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

        public async Task<List<WeatherForeCastModel>> GetItemsAscTimeAsync()
        {
            var list = await database.QueryAsync<WeatherForeCastModel>("SELECT * FROM weatherforecast ORDER BY time ASC");
            var removed = list.RemoveAll(fc => fc.Time <= DateTime.Now.AddHours(-2));
            return list;
        }

        public async Task<WeatherForeCastModel> GetItemAsync(int id)
        {
            var list = await database.QueryAsync<WeatherForeCastModel>("SELECT * FROM weatherforecast");
            //ToDo: get right item from List
            return list.First();
        } 

        public async Task<int> SaveItemsAsync(List<WeatherForeCastModel> items)
        {
            //ToDo: remove this line:
            //await database.DeleteAllAsync<WeatherForeCastModel>();


            var updatedItems = new List<WeatherForeCastModel>();
            var insertedItems = new List<WeatherForeCastModel>();
            if (items.FindAll(item => item.Id != 0).Count == 0)
            {
                await database.InsertAllAsync(items);
                insertedItems = items;
            }
            else if (items.FindAll(item => item.Id <= 0).Count == 0)
            {
                await database.UpdateAllAsync(items);
                updatedItems = items;
            }
            else
            {
                foreach (var item in items)
                {
                    if (item.Id <= 0)
                    {
                        insertedItems.Add(item);
                        await database.InsertAsync(item);
                    }
                    else
                    {
                        updatedItems.Add(item);
                        await database.UpdateAsync(item);
                    }
                }
            }
            if (updatedItems.Any() || insertedItems.Any())
                NotifyDataUpdated(updatedItems, insertedItems);
            return insertedItems.Count + updatedItems.Count;
        }

        void NotifyDataUpdated(List<WeatherForeCastModel> updatedItems, List<WeatherForeCastModel> insertedItems)
        {
            var handler = DataUpdated;
            handler?.Invoke(this, EventArgs.Empty);
            foreach (var forecast in insertedItems)
            {
                Debug.WriteLine("Sending new Notification");
                DependencyService.Get<INotificationService>().SendNewForecastNotification(forecast);
            }
        }
    }
}
