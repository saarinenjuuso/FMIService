using FMIService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMITests.Utils
{
    [TestClass()]
    public class ParsersTests
    {
        [DataTestMethod]
        [DataRow("1.0 2.3 3.0", new double[] { 1.0, 2.3, 3.0 })]
        [DataRow("1.0 \n2.3\n 3.0\n  \n", new double[] { 1.0, 2.3, 3.0 })]
        [DataRow("2.3", new double[] { 2.3 })]
        [DataRow("1.0 NaN 3.0", new double[] { 1.0, double.NaN, 3.0 })]
        [DataRow("NaN NaN NaN", new double[] { double.NaN, double.NaN, double.NaN })]
        [DataRow("\nNaN", new double[] { double.NaN })]
        [DataRow("NaN              NaN", new double[] { double.NaN, double.NaN })]
        [DataRow("NaN \n  NaN \n\n", new double[] { double.NaN, double.NaN })]
        [DataRow("", new double[] { double.NaN })]
        [DataRow("\n\n\n", new double[] { double.NaN })]
        public void ParseValues_WithValidInput_ReturnsExpectedOutput(string values, double[] expected)
        {
            // Arrange
            //string values = "1.0 2.3 3.0";
            //List<double> expected = new List<double> { 1.0, 2.3, 3.0 };
            // Get the values from the DataRow

            // Act
            List<double> actual = Parsers.ParseValues(values);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        //[DataTestMethod]
        //[DataRow("1.0 NaN 3.0", new double[] { 1.0, double.NaN, 3.0 })]
        //[DataRow("NaN NaN NaN", new double[] { double.NaN, double.NaN, double.NaN })]
        //public void ParsersTest(string values, double[] expected)
        //{
        //    // Arrange
        //    //string values = "1.0 2.3 3.0";
        //    //List<double> expected = new List<double> { 1.0, 2.3, 3.0 };
        //    // Get the values from the DataRow

        //    // Act
        //    List<double> actual = Parsers.ParseValues(values);

        //    // Assert
        //    CollectionAssert.AreEqual(expected, actual);
        //}

        //[TestMethod]
        //[DataRow("1.0 2.3 3.0", new double[] { 1.0, 2.3, 3.0 })]
        //[DataRow("2.3 0.0 4.0", new double[] { 2.3, 0.0, 4.0 })]
        //public void ParsersTest(string values, double[] expected)
        //{
        //    // Arrange
        //    //string values = "1.0 2.3 3.0";
        //    //List<double> expected = new List<double> { 1.0, 2.3, 3.0 };
        //    // Get the values from the DataRow

        //    // Act
        //    List<double> actual = Parsers.ParseValues(values);

        //    // Assert
        //    CollectionAssert.AreEqual(expected, actual);
        //}
    }
}
