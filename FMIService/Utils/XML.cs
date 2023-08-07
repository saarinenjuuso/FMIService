using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace FMIService.Utils
{
    public class XML
    {
        public static string GetDataFromMultiPointCoverage(string xmlString)
        {
            try
            {
                XElement xml = XElement.Parse(xmlString);
                XNamespace gml = "http://www.opengis.net/gml/3.2";

                // Get the "doubleOrNilReasonTupleList
                string doubleOrNilReasonTupleList = xml.Descendants(gml + "doubleOrNilReasonTupleList")
                    .FirstOrDefault()?.Value;

                return doubleOrNilReasonTupleList;

            }
            catch (XmlException ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static string GetDataFromSimpleMultiPoint(string xmlString)
        {
            try
            {
                XElement xml = XElement.Parse(xmlString);
                XNamespace gmlcov = "http://www.opengis.net/gmlcov/1.0";

                string positions = xml.Descendants(gmlcov + "SimpleMultiPoint")
                    .Elements(gmlcov + "positions")
                    .FirstOrDefault()?.Value;

                return positions;

            }
            catch (XmlException ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
