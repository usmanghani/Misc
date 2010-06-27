using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using DotFermion.Qiblah;
using GMapsTest.QiblahCalculatorService;

namespace GMapsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GeoCode(string address)
        {
            string key = System.Configuration.ConfigurationSettings.AppSettings["GoogleMapsApiKey"];
            GeoCoder gc = new GeoCoder(key, address, false);
            List<Placemark> placemarks = gc.FetchExtendedGeoCodeData();
            foreach (Placemark p in placemarks)
            {
                AppendText("Id: " + p.Id + Environment.NewLine +
                           "Address: " + p.Address + Environment.NewLine +
                           "Accuracy: " + p.Accuracy.ToString() + Environment.NewLine +
                           "Longitude: " + p.Longitude.ToString() + Environment.NewLine +
                           "Latitude: " + p.Latitude.ToString() + Environment.NewLine +
                           "Altitude: " + p.Altitude.ToString() + Environment.NewLine);

                QiblahCalculator qc = new QiblahCalculator(p.Latitude, p.Longitude);
                AppendText("Qiblah: " + qc.Qiblah + Environment.NewLine);
                AppendText(Environment.NewLine);
            }
        }

        private delegate void ClearTextDelegate();
        private void ClearTextImpl()
        {
            textBox1.Clear();
        }
        private void ClearText()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ClearTextDelegate(ClearTextImpl));
            }
            else ClearTextImpl();
        }

        private delegate void UpdateTextDelegate(string msg);
        private void UpdateText(string msg)
        {
            textBox1.AppendText(msg);
        }
        private void AppendText(string msg)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateTextDelegate(this.UpdateText), new object[] { msg });
            else
                textBox1.AppendText(msg);
        }

        private void AppendLine(string msg)
        {
            AppendText(msg + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                //GeoCode(textBox2.Text);

                string xmlText = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <kml xmlns=\"http://earth.google.com/kml/2.0\">  <Response>  <name>74800</name>  <Status>  <code>200</code>   <request>geocode</request>   </Status> <Placemark id=\"p1\">  <address>74800, France</address>  <AddressDetails Accuracy=\"5\" xmlns=\"urn:oasis:names:tc:ciq:xsdschema:xAL:2.0\"> <Country>  <CountryNameCode>FR</CountryNameCode>   <CountryName>France</CountryName>  <AdministrativeArea>  <AdministrativeAreaName>Rhône-Alpes</AdministrativeAreaName>  <SubAdministrativeArea>  <SubAdministrativeAreaName>Haute-Savoie</SubAdministrativeAreaName>  <PostalCode>  <PostalCodeNumber>74800</PostalCodeNumber>   </PostalCode>  </SubAdministrativeArea>  </AdministrativeArea>  </Country>  </AddressDetails> <ExtendedData>  <LatLonBox north=\"46.1143154\" south=\"46.0054267\" east=\"6.4063946\" west=\"6.2179458\" />   </ExtendedData> <Point>  <coordinates>6.3096284,46.0543688,0</coordinates>   </Point>  </Placemark>  </Response>  </kml>";
                XDocument doc = XDocument.Load(new StringReader(xmlText), LoadOptions.SetLineInfo);
                AppendLine(doc.Declaration.Encoding);
                XElement root = doc.Root;
                var query = from response in root.Elements(ns1 + "Response")
                            from placemark in response.Elements(ns1 + "Placemark")
                            from ad in placemark.Elements(ns2 + "AddressDetails")
                            from country in ad.Elements(ns2 + "Country")
                            from adminArea in country.Elements(ns2 + "AdministrativeArea")
                            from adminAreaName in adminArea.Elements(ns2 + "AdministrativeAreaName")
                            select adminAreaName.Value;
                AppendLine(query.First());
                //IQiblahCalculatorService svc = new QiblahCalculatorServiceClient("BasicHttpBinding_IQiblahCalculatorService");
                //AppendText(svc.FindQiblah(textBox2.Text, false).ToString());
                //QiblahCalcWSStaging.QiblahCalculatorWS svc = new GMapsTest.QiblahCalcWSStaging.QiblahCalculatorWS();
                //AppendText(svc.FindQiblahString(textBox2.Text, false));
                //string url = "http://maps.google.com/maps/geo?sensor=false&output=xml&key=abcdef&q=1234";
                //WebRequest req = WebRequest.Create(url);
                //WebResponse res = req.GetResponse();
                //XmlSerializer ser = new XmlSerializer(typeof(Schemas.kml));
                //Schemas.kml kml = (Schemas.kml)ser.Deserialize(res.GetResponseStream());
                //foreach (Schemas.kmlResponsePlacemark p in kml.Response[0].Placemark)
                //{
                //    AppendText(p.id + " ==> " + p.Point[0].coordinates + Environment.NewLine);
                //}
                
                //string xmlText = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n<kml xmlns=\"http://earth.google.com/kml/2.0\"><Response>\n  <name>redmond washington</name>\n  <Status>\n    <code>200</code>\n    <request>geocode</request>\n  </Status>\n  <Placemark id=\"p1\">\n    <address>Redmond, WA, USA</address>\n    <AddressDetails Accuracy=\"4\" xmlns=\"urn:oasis:names:tc:ciq:xsdschema:xAL:2.0\"><Country><CountryNameCode>US</CountryNameCode><CountryName>USA</CountryName><AdministrativeArea><AdministrativeAreaName>WA</AdministrativeAreaName><Locality><LocalityName>Redmond</LocalityName></Locality></AdministrativeArea></Country></AddressDetails>\n    <ExtendedData>\n      <LatLonBox north=\"47.7171650\" south=\"47.6268450\" east=\"-122.0361380\" west=\"-122.1647580\" />\n    </ExtendedData>\n    <Point><coordinates>-122.1156330,47.6722290,0</coordinates></Point>\n  </Placemark>\n</Response></kml>\n";
                //string xmlText = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><test xmlns=\"testns\"><test2 xmlns=\"test2ns\">blah</test2></test>";
                //XNamespace ns1 = "http://earth.google.com/kml/2.0";
                //XNamespace ns2 = "urn:oasis:names:tc:ciq:xsdschema:xAL:2.0";
                //XNamespace ns1 = "testns";
                //XNamespace ns2 = "test2ns";
                //XElement xelement = XElement.Parse(xmlText);
                //xelement.RemoveAnnotations(typeof(XNamespace));
                //var query = from response in xelement.Elements(ns1 + "Response") select response;
                //foreach (var p in query)
                //{
                //    AppendText(p.ToString());
                //}

                //XDocument doc = XDocument.Parse(xmlText);
                //XElement xelement = doc.Root;
                //var query = from response in xelement.Elements(ns1 + "Response")
                //            from placemark in response.Elements(ns1 + "Placemark")
                //            from point in placemark.Elements(ns1 + "Point")
                //            from coord in point.Elements(ns1 + "coordinates")
                //            select coord.Value;
                //AppendText(query.ToArray()[0] + Environment.NewLine);

                //var query2 = from response in xelement.Elements(ns1 + "Response")
                //            from placemark in response.Elements(ns1 + "Placemark")
                //            from ad in placemark.Elements(ns2 + "AddressDetails")
                //            from accuracy in ad.Attributes("Accuracy")
                //            select accuracy.Value;
                //AppendText(query2.ToArray()[0] + Environment.NewLine);

                //var query3 = from response in xelement.Elements(ns1 + "Response")
                //             from status in response.Elements(ns1 + "Status")
                //             from code in status.Elements(ns1 + "code")
                //             select code.Value;
                //AppendText(query3.ToArray()[0] + Environment.NewLine);

                //var query4 = from response in xelement.Elements(ns1 + "Response")
                //             from name in response.Elements(ns1 + "name")
                //             select name.Value;
                //AppendText(query4.ToArray()[0] + Environment.NewLine);

                //var query5 = from response in xelement.Elements(ns1 + "Response")
                //             from placemark in response.Elements(ns1 + "Placemark")
                //             from address in placemark.Elements(ns1 + "address")
                //             select address.Value;
                //AppendText(query5.ToArray()[0] + Environment.NewLine);

                //var query6 = from response in xelement.Elements(ns1 + "Response")
                //             from placemark in response.Elements(ns1 + "Placemark")
                //             from ad in placemark.Elements(ns2 + "AddressDetails")
                //             from country in ad.Elements(ns2 + "Country")
                //             from countryName in country.Elements(ns2 + "CountryName")
                //             select countryName.Value;
                //AppendText(query6.ToArray()[0] + Environment.NewLine);

                //var query7 = from response in xelement.Elements(ns1 + "Response")
                //             from placemark in response.Elements(ns1 + "Placemark")
                //             from id in placemark.Attributes("id")
                //             select id.Value;
                //AppendText(query7.ToArray()[0] + Environment.NewLine);

                //foreach (var response in GetResponses(doc))
                //{
                //    foreach (var placemark in GetPlacemarks(response))
                //    {
                //        foreach (var point in GetPoints(placemark))
                //        {
                //            foreach (var coord in GetCoordinates(point))
                //            {
                //                AppendText(coord.Value + Environment.NewLine);
                //            }
                //        }
                //    }
                //}

                //AppendText(xelement.Element(ns1 + "Response").Element(ns1 + "Placemark").Element(ns2 + "AddressDetails").Attribute("Accuracy").Value);
                //NameTable nt = new NameTable();
                //XmlNamespaceManager nsMgr = new XmlNamespaceManager(nt);
                //nsMgr.AddNamespace(string.Empty, "http://earth.google.com/kml/2.0");
                //nsMgr.AddNamespace(string.Empty, "urn:oasis:names:tc:ciq:xsdschema:xAL:2.0");
                //XmlDocument doc = new XmlDocument(nt);
                //doc.LoadXml(xmlText);
                //XmlNode node = doc.SelectSingleNode("//AddressDetails");
                //AppendText(node.InnerXml);

            }
            catch (Exception ex)
            {
                AppendText("Can't find Qiblah for: " + textBox2.Text);
                MessageBox.Show(ex.Message);
            }
        }

        XNamespace ns1 = "http://earth.google.com/kml/2.0";
        XNamespace ns2 = "urn:oasis:names:tc:ciq:xsdschema:xAL:2.0";

        private IEnumerable<XElement> GetResponses(XDocument doc)
        {
            return doc.Elements(ns1 + "Response");
        }

        private IEnumerable<XElement> GetPlacemarks(XElement xelement)
        {
            return xelement.Elements(ns1 + "Placemark");
        }

        private IEnumerable<XElement> GetPoints(XElement xelement)
        {
            return xelement.Elements(ns1 + "Point");
        }

        private IEnumerable<XElement> GetCoordinates(XElement xelement)
        {
            return xelement.Elements(ns1 + "coordinates");
        }

    }
}
