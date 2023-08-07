using FMIService.Models;
using FMIService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FMITests.TestUtilities.Comparers;

namespace FMITests.Utils
{
    [TestClass()]
    public class UtilitiesTests
    {
        static IEnumerable<object[]> WindData
        {
            get
            {
                return new[]
                {
                    new object[] { new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 }, new List<Wind>{ new Wind(1.0, 2.0, 3.0), new Wind(4.0, 5.0, 6.0) } },
                    new object[] { new List<double> { 1.0, double.NaN, double.NaN, double.NaN, 5.0, 6.0 }, new List<Wind>{ new Wind(1.0, double.NaN, double.NaN), new Wind(double.NaN, 5.0, 6.0) } }
                };
            }
        }

        // 1672531200 1.1.2023 00:00:00 UTC+0
        // 1639564245 15.12.2021 10:30:45 UTC+0
        static IEnumerable<object[]> PositionData
        {
            get
            {
                return new[]
                {
                    new object[] {
                        new List<double> { 64.123456, 24.123456, 1672531200, 65.123456, 25.123456, 1639564245 },
                        new List<Position> { new Position(64.123456, 24.123456, new DateTime(2023,1,1,0,0,0, DateTimeKind.Utc)), new Position(65.123456, 25.123456, new DateTime(2021,12,15,10,30,45,DateTimeKind.Utc)) }
                    }
                };
            }
        }

        static IEnumerable<object[]> MergeData
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new List<Wind>{ new Wind(1.0, 2.0, 3.0), new Wind(4.0, 5.0, 6.0) },
                        new List<Position> { new Position(64.123456, 24.123456, new DateTime(2023,1,1,0,0,0, DateTimeKind.Utc)), new Position(65.123456, 25.123456, new DateTime(2021,12,15,10,30,45,DateTimeKind.Utc)) },
                        new List<WindMeasurement>
                        {
                            new WindMeasurement(new Wind(1.0, 2.0, 3.0), new Position(64.123456, 24.123456, new DateTime(2023,1,1,0,0,0, DateTimeKind.Utc))),
                            new WindMeasurement(new Wind(4.0, 5.0, 6.0), new Position(65.123456, 25.123456, new DateTime(2021,12,15,10,30,45,DateTimeKind.Utc))),
                        }
                    }
                };
            }
        }

        static IEnumerable<object[]> MergeDataWithNaN
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new List<Wind>{ new Wind(1.0, double.NaN, 3.0), new Wind(double.NaN, double.NaN, double.NaN) },
                        new List<Position> { new Position(64.123456, 24.123456, new DateTime(2023,1,1,0,0,0, DateTimeKind.Utc)), new Position(65.123456, 25.123456, new DateTime(2021,12,15,10,30,45,0,DateTimeKind.Utc)) },
                        new List<WindMeasurement>
                        {
                            new WindMeasurement(new Wind(1.0, double.NaN, 3.0), new Position(64.123456, 24.123456, new DateTime(2023,1,1,0,0,0, DateTimeKind.Utc))),
                            new WindMeasurement(new Wind(double.NaN, double.NaN, double.NaN), new Position(65.123456, 25.123456, new DateTime(2021,12,15,10,30,45,0,DateTimeKind.Utc))),
                        }
                    }
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(WindData))]
        //[DataRow(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 }, new Wind[] { new Wind(1.0, 2.0, 3.0), new Wind(4.0, 5.0, 6.0) } )]
        //public void CreateWindObjectsTest_WithValidInput_ReturnsExpectedOutput(double[] input, Wind[] expected)
        public void CreateWindObjectsTest_WithValidInput_ReturnsExpectedOutput(List<double> input, List<Wind> expected)
        {
            //Wind[] expected = new Wind[] { new Wind(1.0, 2.0, 3.0), new Wind(4.0, 5.0, 6.0) };

            List<Wind> actual = Utilities.CreateWindObjects(input);
            Assert.IsTrue(expected.SequenceEqual(actual, new WindDataEqualityComparer()));
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod()]
        [DynamicData(nameof(PositionData))]
        public void CreatePositionObjectsTest_WithValidInput_ReturnsExpectedOutput(List<double> input, List<Position> expected)
        {
            List<Position> actual = Utilities.CreatePositionObjects(input);
            Assert.IsTrue(expected.SequenceEqual(actual, new PositionDataEqualityComparer()));
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod()]
        [DynamicData(nameof(MergeData))]
        [DynamicData(nameof(MergeDataWithNaN))]
        public void MergeDataTest_WithValidInput_ReturnsExpectedOutput(List<Wind> wind, List<Position> position, List<WindMeasurement> expected)
        {
            // Act
            List<WindMeasurement> actual = Utilities.MergeData(wind, position);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.IsTrue(expected.SequenceEqual(actual, new MergeDataEqualityComparer()));
        }
    }
}
