using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMIService.Utils
{
    public class Parsers
    {
        public static List<double> ParseValues(string values)
        {

            string cleanedDouble = string.Join(" ", values.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries));

            List<string> list = cleanedDouble.Split(' ').ToList();

            List<double> doubles = list.Select(s => double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : double.NaN).ToList();

            if (doubles.Count == 0)
            {
                return new List<double>();
            }

            return doubles;
        }

        public static List<double> ParsePosition(string values)
        {

            string cleanedDouble = string.Join(" ", values.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries));

            List<string> list = cleanedDouble.Split(' ').ToList();

            List<double> doubles = list.Select(s => double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : double.NaN).ToList();

            return doubles;
        }
    }
}
