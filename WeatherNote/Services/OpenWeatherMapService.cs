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
            _forecasts = new List<Weather.List>();

            // Requests the 5 day weather forecast and puts the task result in an object for easier access to the json data
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/forecast?id=2686469&APPID=f2564c3daa8d75093a6fd379ac478c8b&units=metric");

                Task<string> jsonTask = Task.Run(async () => await httpClient.GetStringAsync(httpClient.BaseAddress));
                _jsonData = jsonTask.Result;
                _weather = JsonConvert.DeserializeObject<Weather.RootObject>(_jsonData);
            }
        }

        /// <summary>
        /// Checks if the note.date is within the span for the forecast and sets a temprature for each valid note
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public Note FindMaxTemp(Note note)
        {
            if (note.Date >= _currentDate && note.Date <= _currentDate.AddDays(4) )
            {
                _formatedNoteDate = note.Date.ToString("yyyy-MM-dd");

                foreach (var forecast in _weather.list)
                {
                    // Checks for all the "forecast timestamps" belonging to the same date and puts it in a separate list to easier access it
                    if (forecast.dt_txt.Contains(_formatedNoteDate))
                    {
                        _forecasts.Add(forecast);
                    }
                }

                double highestTemprature = _forecasts[0].main.temp_max;

                foreach (var forecast in _forecasts.Skip(1))
                {
                    // Loops through the list of tempratures from the same day and finds the max temprature before assigning it to the note
                    if (forecast.main.temp_max > highestTemprature)
                        highestTemprature = forecast.main.temp_max;
                }

                note.Temprature = highestTemprature;
                _forecasts.Clear();
            }

            return note;
        }

    }
}