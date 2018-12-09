using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGarage2.Models
{
    public class ParkedVehicle
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        [RegularExpression("[a-zA-Z0-9]+",ErrorMessage ="Must be leters and numbers, only")]
        public string Regnr { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Vehicle type")]
        public string Type { get; set; }
        [Required]
        [StringLength(15)]
        public string Color { get; set; }
        [Required]
        [StringLength(25,ErrorMessage = "Maximum 25 chars")]
        [RegularExpression("[a-zA-Z0-9][a-zA-Z0-9 .-]*", ErrorMessage = "Must be leters and numbers, only")]
        public string Brand { get; set; }
        [Required]
        [Range(1,18)]
        [Display(Name = "Wheel count")]
        public int NrofWheels { get; set; }
        [Required]
        [Range(0.1, 30.0)]
        public double Length { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Fuel type")]
        public string FuelType { get; set; }
        [Required]
        [Display(Name = "Arrival time")]
        [UIHint("DateFormat")]
        public DateTime ParkedTime { get; set; }

    }

    public enum VehicleType
    {
        Car,
        Bike,
        Bus
    }

    public enum Color
    {
        Black,
        Blue,
        Brown,
        Gray,
        Green,
        Red,
        Silver,
        White,
        Yellow
    }

    public enum FuelType
    {
        Bensin,
        Biogas,
        Diesel,
        Ethenol
    }
}