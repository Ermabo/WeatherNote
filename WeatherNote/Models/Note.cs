using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeatherNote.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

    }
}