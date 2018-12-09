using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGarage2.Models
{
    public class VehicleOverview
    {
        public VehicleOverview(int id, string regnr, string type, string color, DateTime parkedTime)
        {
            Id = id;
            Regnr = regnr;
            Type = type;
            Color = color;
            ParkedTime = parkedTime;
        }

        public int Id { get; set; }
        public string Regnr { get; set; }

        [Display(Name = "Vehicle type")]
        public string Type { get; set; }
        public string Color { get; set; }

        [Display(Name = "Arrival time")]
        [UIHint("DateFormat")]
        public DateTime ParkedTime { get; set; }
    }
}