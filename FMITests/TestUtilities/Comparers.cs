using FMIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMITests.TestUtilities
{
    public class Comparers
    {
        /// <summary>
        /// Compares two Wind objects for equality.
        /// </summary>
        public class WindDataEqualityComparer : IEqualityComparer<Wind>
        {
            /// <summary>
            /// Determines whether two Wind objects are equal.
            /// </summary>
            /// <param name="x">The first Wind object to compare.</param>
            /// <param name="y">The second Wind object to compare.</param>
            /// <returns>true if the specified Wind objects are equal; otherwise, false.</returns>
            public bool Equals(Wind x, Wind y)
            {
                if (object.ReferenceEquals(x, y)) return true;

                if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

                // double.IsNaN comparison 
                return (x.WindSpeed == y.WindSpeed || double.IsNaN(x.WindSpeed) && double.IsNaN(y.WindSpeed))
                    && (x.WindDirection == y.WindDirection || double.IsNaN(x.WindDirection) && double.IsNaN(y.WindDirection))
                    && (x.WindGust == y.WindGust || double.IsNaN(x.WindGust) && double.IsNaN(y.WindGust));
            }

            /// <summary>
            /// Returns a hash code for the specified Wind object.
            /// </summary>
            /// <param name="obj">The Wind object for which to get a hash code.</param>
            /// <returns>A hash code for the specified Wind object.</returns>
            public int GetHashCode(Wind obj)
            {
                if (object.ReferenceEquals(obj, null)) return 0;

                int hashCodeSpeed = obj.WindSpeed.GetHashCode();
                int hashCodeDirection = obj.WindDirection.GetHashCode();
                int hasCodeGust = obj.WindGust.GetHashCode();

                return hashCodeDirection ^ hashCodeSpeed ^ hasCodeGust;
            }
        }

        /// <summary>
        /// Compares two Position objects for equality.
        /// </summary>
        public class PositionDataEqualityComparer : IEqualityComparer<Position>
        {
            /// <summary>
            /// Determines whether two Position objects are equal.
            /// </summary>
            /// <param name="x">The first Position object to compare.</param>
            /// <param name="y">The second Position object to compare.</param>
            /// <returns>true if the specified Position objects are equal; otherwise, false.</returns>
            public bool Equals(Position x, Position y)
            {
                if (object.ReferenceEquals(x, y)) return true;

                if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

                // If datetime comparison fails, check the milliseconds difference between timestamps
                //int result = DateTime.Compare(x.Timestamp, y.Timestamp);
                //bool isEqual = result == 0;

                // double is a value, Datetime object is a reference. Comparison has to be done with the .Equals() method
                return x.Latitude == y.Latitude && x.Longitude == y.Longitude && x.Timestamp.Equals(y.Timestamp);
                //return x.Latitude == y.Latitude && x.Longitude == y.Longitude && isEqual;
            }

            /// <summary>
            /// Returns a hash code for the specified Position object.
            /// </summary>
            /// <param name="obj">The Position object for which to get a hash code.</param>
            /// <returns>A hash code for the specified Position object.</returns>
            public int GetHashCode(Position obj)
            {
                if (object.ReferenceEquals(obj, null)) return 0;

                int hashCodeLatitude = obj.Latitude.GetHashCode();
                int hashCodeLongitude = obj.Longitude.GetHashCode();
                int hasCodeTimestamp = obj.Timestamp.GetHashCode();

                return hashCodeLatitude ^ hashCodeLongitude ^ hasCodeTimestamp;
            }
        }

        /// <summary>
        /// Compares two WindMeasurement objects for equality.
        /// </summary>
        public class MergeDataEqualityComparer : IEqualityComparer<WindMeasurement>
        {
            /// <summary>
            /// Determines whether two WindMeasurement objects are equal.
            /// </summary>
            /// <param name="x">The first WindMeasurement object to compare.</param>
            /// <param name="y">The second WindMeasurement object to compare.</param>
            /// <returns>true if the specified WindMeasurement objects are equal; otherwise, false.</returns>
            public bool Equals(WindMeasurement x, WindMeasurement y)
            {
                if (object.ReferenceEquals(x, y)) return true;

                if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

                // If datetime comparison fails, check the milliseconds difference between timestamps
                //int result = DateTime.Compare(x.Timestamp, y.Timestamp);
                //bool isEqual = result == 0;

                // double is a value, Datetime object is a reference. Comparison has to be done with the .Equals() method
                return x.Latitude == y.Latitude && x.Longitude == y.Longitude && x.Timestamp.Equals(y.Timestamp)
                    && (x.WindSpeed == y.WindSpeed || double.IsNaN(x.WindSpeed) && double.IsNaN(y.WindSpeed))
                    && (x.WindDirection == y.WindDirection || double.IsNaN(x.WindDirection) && double.IsNaN(y.WindDirection))
                    && (x.WindGust == y.WindGust || double.IsNaN(x.WindGust) && double.IsNaN(y.WindGust));
                //return x.Latitude == y.Latitude && x.Longitude == y.Longitude && isEqual;
            }

            /// <summary>
            /// Returns a hash code for the specified WindMeasurement object.
            /// </summary>
            /// <param name="obj">The WindMeasurement object for which to get a hash code.</param>
            /// <returns>A hash code for the specified WindMeasurement object.</returns>
            public int GetHashCode(WindMeasurement obj)
            {
                if (object.ReferenceEquals(obj, null)) return 0;

                int hashCodeLatitude = obj.Latitude.GetHashCode();
                int hashCodeLongitude = obj.Longitude.GetHashCode();
                int hasCodeTimestamp = obj.Timestamp.GetHashCode();
                int hashCodeSpeed = obj.WindSpeed.GetHashCode();
                int hashCodeDirection = obj.WindDirection.GetHashCode();
                int hasCodeGust = obj.WindGust.GetHashCode();

                return hashCodeLatitude ^ hashCodeLongitude ^ hasCodeTimestamp ^ hashCodeDirection ^ hashCodeSpeed ^ hasCodeGust;
            }
        }

    }
}
