using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherNote.Models;

namespace WeatherNote.ViewModels
{
    public class WeatherNoteViewModel
    {
        public IEnumerable<Note> Notes { get; set; }
        public Weather.RootObject Weather { get; set;  }

        public WeatherNoteViewModel()
        {
            
        }
    }
}