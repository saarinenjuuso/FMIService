using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMIService.Models
{
    public class Wind
    {
        public double WindDirection { get; set; }
        public double WindSpeed { get; set; }
        public double WindGust { get; set; }

        public Wind(double windDirection, double windSpeed, double windGust)
        {
            WindDirection = windDirection;
            WindSpeed = windSpeed;
            WindGust = windGust;
        }
    }
}
