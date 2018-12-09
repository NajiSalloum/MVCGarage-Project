using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MVCGarage2.Models
{
    public class VehicleReceipt
    {
        public VehicleReceipt(int id, string regnr, string type, DateTime parkedTime, DateTime nowTime)
        {
            Id = id;
            Regnr = regnr;
            Type = type;
            ParkedTime = parkedTime;
            NowTime = nowTime;
            TimeSpan ts = NowTime - ParkedTime;
            TotalTime = $"{Math.Floor(ts.TotalDays)} days, {ts.Hours} hours and {ts.Minutes} minutes.";
            Price = (ts.TotalMinutes + 1) * 5;
        }

        public int Id { get; set; }
        public string Regnr { get; set; }

        [Display(Name = "Vehicle type")]
        public string Type { get; set; }

        [Display(Name = "Arrival time")]
        [UIHint("DateFormat")]
        public DateTime ParkedTime { get; set; }

        [Display(Name = "Check out time")]
        [UIHint("DateFormat")]
        public DateTime NowTime { get; set; }

        [Display(Name = "Total time")]
        public string TotalTime { get; set; }

        [UIHint("Currency")]
        public double Price { get; set; }
    }
}

