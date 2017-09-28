// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var data = RootObject.FromJson(jsonString);
//
// For POCOs visit quicktype.io?poco
//
namespace WeatherNote.Models
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Main
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public string dt_txt { get; set; }
    }

    public class RootObject
    {
        public List<List> list { get; set; }
    }
}