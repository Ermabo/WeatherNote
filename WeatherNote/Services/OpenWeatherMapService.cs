using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WeatherNote.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherNote.Services
{
    public class OpenWeatherMapService
    {
        private Weather.RootObject _weather;
        private string _jsonData;

        public OpenWeatherMapService()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/forecast?id=2686469&APPID=f2564c3daa8d75093a6fd379ac478c8b&units=metric");

                Task<string> jsondata = Task.Run(async () => await httpClient.GetStringAsync(httpClient.BaseAddress));
                _jsonData = jsondata.Result;
                _weather = JsonConvert.DeserializeObject<Weather.RootObject>(_jsonData);

            }
        }

        //public async Task<Weather.RootObject> GetWeather()
        //{   
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/forecast?id=2686469&APPID=f2564c3daa8d75093a6fd379ac478c8b&units=metric");
        //        _jsonData = await httpClient.GetStringAsync(httpClient.BaseAddress);
        //        _weather = JsonConvert.DeserializeObject<Weather.RootObject>(_jsonData);
        //        return _weather;
        //    }
        //}

        public Note FindMaxTemp(Note note)
        {
            return note;
        }

    }
}