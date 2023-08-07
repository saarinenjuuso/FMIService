using FMIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMIService.Utils
{
    public class Utilities
    {
        public static List<WindMeasurement> MergeData(List<Wind> wind, List<Position> position)
        {
            List<WindMeasurement> combined = new List<WindMeasurement>();

            if (wind.Count == position.Count)
            {
                for (int i = 0; i < wind.Count; i++)
                {
                    combined.Add(new WindMeasurement(wind[i], position[i]));
                }
                return combined;
            }
            return null;
        }

        public static List<Wind> CreateWindObjects(List<double> wind)
        {
            List<Wind> windList = new List<Wind>();

            for (int i = 0; i < wind.Count; i += 3)
            {
                windList.Add(new Wind(wind[i], wind[i + 1], wind[i + 2]));
            }

            return windList;
        }

        public static List<Position> CreatePositionObjects(List<double> position)
        {
            List<Position> positionList = new List<Position>();

            for (int i = 0; i < position.Count; i += 3)
            {
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(position[i + 2]);
                positionList.Add(new Position(position[i], position[i + 1], dateTime));
            }

            return positionList;
        }

        public static List<WindMeasurement> ParseAndMergeData(string xmlString)
        {
            string positions = XML.GetDataFromSimpleMultiPoint(xmlString);
            string doubleOrNilReasonTupleList = XML.GetDataFromMultiPointCoverage(xmlString);
            List<double> timeData = Parsers.ParseValues(positions);
            List<double> windData = Parsers.ParseValues(doubleOrNilReasonTupleList);
            List<WindMeasurement> windMeasurements = MergeData(CreateWindObjects(windData), CreatePositionObjects(timeData));
            return windMeasurements;
        }
    }
}
