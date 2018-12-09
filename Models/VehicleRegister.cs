using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGarage2.Models
{
    public class VehicleRegister
    {
        public VehicleRegister()
        {
            // Set min and max properties for Length property by checking Lengths Range.minimum validation
            var rng = (RangeAttribute)typeof(VehicleRegister).GetProperty("Length").GetCustomAttributes(typeof(RangeAttribute), false).First();
            double temp;
            if (!double.TryParse(rng?.Maximum?.ToString() ?? "0.0", out temp))
            {
                temp = 0.0;
            }
            MaxLength = temp;

            if (!double.TryParse(rng?.Minimum?.ToString() ?? "0.0", out temp))
            {
                temp = 0.0;
            }
            MinLength = temp;

            NrofWheels = 1;
            Length = 1;
        }
        public double MinLength { get; set; }
        public double MaxLength { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression("[a-zA-Z0-9]+", ErrorMessage = "Must be leters and numbers, only")]
        public string Regnr { get; set; }

        [Display(Name = "Vehicle type")]
        public VehicleType Type { get; set; }

        public Color Color { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Maximum 25 chars")]
        [RegularExpression("[a-zA-Z0-9][a-zA-Z0-9 .-]*", ErrorMessage = "Must be leters and numbers, only")]
        public string Brand { get; set; }

        [Required]
        [Range(1, 18)]
        [Display(Name = "Wheel count")]
        public int NrofWheels { get; set; }

        [Required]
        [Range(0.1, 30.0)]
        public double Length { get; set; }

        [Display(Name = "Fuel type")]
        public FuelType FuelType { get; set; }
    }
}