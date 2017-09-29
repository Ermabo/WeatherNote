using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WeatherNote.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;

namespace WeatherNote.Services
{
    public class OpenWeatherMapService
    {
        private Weather.RootObject _weather;
        private string _jsonData;
        private DateTime _currentDate;
        private string _formatedNoteDate;
        private List<Weather.List> _forecasts;

        public OpenWeatherMapService()
        {
            _currentDate = DateTime.Today;
            //_currentDateString = _currentDate.ToString("yyyy-MM-dd");
            _forecasts = new List<Weather.List>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/forecast?id=2686469&APPID=f2564c3daa8d75093a6fd379ac478c8b&units=metric");

                Task<string> jsonTask = Task.Run(async () => await httpClient.GetStringAsync(httpClient.BaseAddress));
                _jsonData = jsonTask.Result;
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
            if (note.Date >= _currentDate && note.Date <= _currentDate.AddDays(4) )
            {
                _formatedNoteDate = note.Date.ToString("yyyy-MM-dd");

                foreach (var forecast in _weather.list)
                {
                    if (forecast.dt_txt.Contains(_formatedNoteDate))
                    {
                        _forecasts.Add(forecast);
                    }
                }

                double highestTemprature = _forecasts[0].main.temp_max;

                foreach (var forecast in _forecasts.Skip(1))
                {
                    if (forecast.main.temp_max > highestTemprature)
                        highestTemprature = forecast.main.temp_max;
                }

                note.Temprature = highestTemprature;
            }

            return note;
        }

    }
}