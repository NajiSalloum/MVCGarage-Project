using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGarage2.Models
{
    public class VehicleSearch
    {
        public VehicleSearch()
        {
        }

        [StringLength(10)]
        [RegularExpression("[a-zA-Z0-9]+", ErrorMessage = "Must be leters and numbers, only")]
        public string Regnr { get; set; }

        [Display(Name = "Vehicle type")]
        public VehicleType Type { get; set; }

        public Color Color { get; set; }

        [StringLength(25, ErrorMessage = "Maximum 25 chars")]
        [RegularExpression("[a-zA-Z0-9][a-zA-Z0-9 .-]*", ErrorMessage = "Must be leters and numbers, only")]
        public string Brand { get; set; }

        [Display(Name = "Fuel type")]
        public FuelType FuelType { get; set; }
    }
}