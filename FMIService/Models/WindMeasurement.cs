using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMIService.Models
{
    public class WindMeasurement
    {
        public double WindDirection { get; set; }
        public double WindSpeed { get; set; }
        public double WindGust { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }

        public WindMeasurement(Wind wind, Position position)
        {
            WindDirection = wind.WindDirection;
            WindSpeed = wind.WindSpeed;
            WindGust = wind.WindGust;
            Latitude = position.Latitude;
            Longitude = position.Longitude;
            Timestamp = position.Timestamp;
        }
    }
}
