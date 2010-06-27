using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace DotFermion.Qiblah
{

    public enum GeoCodeDataFormat
    {
        Csv, Xml, Json
    }

    public enum Accuracy
    {
        Unknown = 0, Country, Region, SubRegion, Town, PostCode, Street, Intersection, Address, Premise
    }

    public class Placemark
    {
        public string Id
        { get; set; }
        public string Address
        { get; set; }
        public Accuracy Accuracy
        { get; set; }
        public double Longitude
        { get; set; }
        public double Latitude
        { get; set; }
        public double Altitude
        { get; set; }
    }

    public class GeoCodeParseData
    {
    }

    public class GMapsParseData : GeoCodeParseData
    {
        public int StatusCode { get; set; }
        public IEnumerable<Placemark> Placemarks { get; set; }
    }

    public interface GeoCodeDataParser
    {
        GeoCodeParseData Parse(GeoCodeDataFormat format, System.IO.Stream dataStream);
    }

    public class GMapsDataParser : GeoCodeDataParser
    {
        #region GeoCodeDataParser Members

        GeoCodeParseData GeoCodeDataParser.Parse(GeoCodeDataFormat format, System.IO.Stream dataStream)
        {
            GMapsParseData pd = null;
            switch (format)
            {
                case GeoCodeDataFormat.Csv:
                    pd = ParseCsv(dataStream);
                    break;
                case GeoCodeDataFormat.Xml:
                    pd = ParseXml(dataStream);
                    break;
            }
            return pd;
        }

        #endregion

        private GMapsParseData ParseCsv(System.IO.Stream dataStream)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
            string csvData = reader.ReadToEnd();
            
            GMapsParseData pd = new GMapsParseData();
            
            string[] tokens = csvData.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            pd.StatusCode = int.Parse(tokens[0]);
            
            Placemark p = new Placemark() { 
                Accuracy = (Accuracy)int.Parse(tokens[1]), 
                Latitude = double.Parse(tokens[2]), 
                Longitude = double.Parse(tokens[3]) };

            pd.Placemarks = new List<Placemark>() { p };
            return pd;
        }

        private GMapsParseData ParseXml(System.IO.Stream dataStream)
        {
            GMapsParseData pd = new GMapsParseData();
            ///////////////////////////////////////////////////////////
            //HACK: This is a work around for this:
            //XDocument doc = XDocument.Load(new System.Xml.XmlTextReader(dataStream), LoadOptions.SetLineInfo);
            //This or any other use of a stream directly in XDocument.Load fails with invalid character
            //error for some locations, e.g. France. The stream has to be converted to a string first
            //before using it.
            TextReader sr = new StreamReader(dataStream);
            string xmlText = sr.ReadToEnd();
            sr = new StringReader(xmlText);
            XDocument doc = XDocument.Load(sr, LoadOptions.SetLineInfo);
            ////////////////////////////////////////////////////////////
            pd.StatusCode = GetStatusCode(doc);
            pd.Placemarks = GetPlacemarkList(doc);
            return pd;
            //XmlSerializer ser = new XmlSerializer(typeof(kml));
            //kml kml = (kml)ser.Deserialize(dataStream);
            //pd.StatusCode = int.Parse(kml.Response[0].Status[0].code);
            //foreach (kmlResponsePlacemark kp in kml.Response[0].Placemark)
            //{
            //    Placemark p = new Placemark();
            //    p.Id = kp.id;
            //    p.Accuracy = GetAccuracyFromAddressDetails(kp.AddressDetails);
            //    string[] tokens = kp.Point[0].coordinates.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            //    p.Latitude = double.Parse(tokens[1]);
            //    p.Longitude = double.Parse(tokens[0]);
            //    p.Address = kp.address;
            //    placemarks.Add(p);
            //}

        }

        private List<Placemark> GetPlacemarkList(XDocument doc)
        {
            List<Placemark> placemarks = new List<Placemark>();
            List<string> placemarkXmls = GetPlacemarkXmlList(doc);
            foreach (var placemarkXml in placemarkXmls)
            {
                string id = GetPlacemarkIdFromPlacemarkXml(XElement.Parse(placemarkXml));
                Accuracy accuracy = GetAccuracyFromPlacemarkXml(XElement.Parse(placemarkXml));
                string address = GetAddressFromPlacemarkXml(XElement.Parse(placemarkXml));
                string coords = GetStringCoordinatesFromPlacemarkXml(XElement.Parse(placemarkXml));
                Coords c = ParseCoordsFromString(coords);
                Placemark p = new Placemark() { Id = id, Address = address, Accuracy = accuracy, Longitude = c.Longitude, Latitude = c.Latitude, Altitude = c.Altitude };
                placemarks.Add(p);
            }
            return placemarks;
        }

        private List<string> GetPlacemarkXmlList(XDocument doc)
        {
            XElement xelement = doc.Root;
            var query = from response in xelement.Elements(ns1 + "Response")
                        from placemark in response.Elements(ns1 + "Placemark")
                        select placemark.ToString();

            return query.ToList();
        }

        private string GetPlacemarkIdFromPlacemarkXml(XElement xelement)
        {
            var query = from id in xelement.Attributes("id")
                        select id.Value;
            return query.First();
        }

        private Accuracy GetAccuracyFromPlacemarkXml(XElement xelement)
        {
            var query = from ad in xelement.Elements(ns2 + "AddressDetails")
                        from accuracy in ad.Attributes("Accuracy")
                        select accuracy.Value;
            return (Accuracy)int.Parse(query.First());

        }

        private string GetStringCoordinatesFromPlacemarkXml(XElement xelement)
        {
            var query = from point in xelement.Elements(ns1 + "Point")
                        from coords in point.Elements(ns1 + "coordinates")
                        select coords.Value;
            return query.First();
        }


        private string GetAddressFromPlacemarkXml(XElement xelement)
        {
            var query = from address in xelement.Elements(ns1 + "address")
                        select address.Value;
            return query.First();
        }

        private Coords ParseCoordsFromString(string coordString)
        {
            string[] tokens = coordString.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return new Coords() { Longitude = double.Parse(tokens[0]), Latitude = double.Parse(tokens[1]), Altitude = double.Parse(tokens[2]) };
        }

        private string GetInputAddress(XDocument doc)
        {
            XElement xelement = doc.Root;
            var query = from response in xelement.Elements(ns1 + "Response")
                        from name in response.Elements(ns1 + "name")
                        select name.Value;

            if (query.ToArray().Length > 0)
            {
                return query.ToArray()[0];
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetStringCoordinates(XDocument doc)
        {
            XElement xelement = doc.Root;
            var query = from response in xelement.Elements(ns1 + "Response")
                        from placemark in response.Elements(ns1 + "Placemark")
                        from point in placemark.Elements(ns1 + "Point")
                        from coord in point.Elements(ns1 + "coordinates")
                        select coord.Value;
            if (query.ToArray().Length > 0)
            {
                return query.ToArray()[0];
            }
            else
            {
                return string.Empty;
            }
        }

        private int GetStatusCode(XDocument doc)
        {
            XElement xelement = doc.Root;
            var query = from response in xelement.Elements(ns1 + "Response")
                        from status in response.Elements(ns1 + "Status")
                        from code in status.Elements(ns1 + "code")
                        select code.Value;
            if (query.ToArray().Length > 0)
            {
                return int.Parse(query.ToArray()[0]);
            }
            else
            {
                return GeoErrors.G_GEO_SUCCESS;
            }
        }

        private Accuracy GetAccuracy(XDocument doc)
        {
            Accuracy acc = Accuracy.Unknown;
            XElement xelement = doc.Root;
            var query = from response in xelement.Elements(ns1 + "Response")
                        from placemark in response.Elements(ns1 + "Placemark")
                        from ad in placemark.Elements(ns2 + "AddressDetails")
                        from accuracy in ad.Attributes("Accuracy")
                        select accuracy.Value;

            if (query.ToArray().Length > 0)
            {
                acc = (Accuracy)int.Parse(query.ToArray()[0]);
            }
            else
            {
                acc = Accuracy.Unknown;
            }
            return acc;
        }
        private string GetReturnedAddress(XDocument doc)
        {
            XElement xelement = doc.Root;
            var query = from response in xelement.Elements(ns1 + "Response")
                        from placemark in response.Elements(ns1 + "Placemark")
                        from address in placemark.Elements(ns1 + "address")
                        select address.Value;
            if (query.ToArray().Length > 0)
            {
                return query.ToArray()[0];
            }
            else
            {
                return string.Empty;
            }

        }

        private string GetCountryName(XDocument doc)
        {
            XElement xelement = doc.Root;
            var query = from response in xelement.Elements(ns1 + "Response")
                        from placemark in response.Elements(ns1 + "Placemark")
                        from ad in placemark.Elements(ns2 + "AddressDetails")
                        from country in ad.Elements(ns2 + "Country")
                        from countryName in country.Elements(ns2 + "CountryName")
                        select countryName.Value;
            if (query.ToArray().Length > 0)
            {
                return query.ToArray()[0];
            }
            else
            {
                return string.Empty;
            }

        }

        private string GetPlacemarkId(XDocument doc)
        {
            XElement xelement = doc.Root;
            var query = from response in xelement.Elements(ns1 + "Response")
                        from placemark in response.Elements(ns1 + "Placemark")
                        from id in placemark.Attributes("id")
                        select id.Value;
            if (query.ToArray().Length > 0)
            {
                return query.ToArray()[0];
            }
            else
            {
                return string.Empty;
            }
            
        }

        //private Accuracy GetAccuracyFromAddressDetails(kmlResponsePlacemarkAddressDetails ad)
        //{
        //    if (ad == null) return Accuracy.Unknown;
        //    else return (Accuracy)int.Parse(ad.Accuracy);
        //}

        XNamespace ns1 = "http://earth.google.com/kml/2.0";
        XNamespace ns2 = "urn:oasis:names:tc:ciq:xsdschema:xAL:2.0";

    }

    public struct Coords
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
    }

}
