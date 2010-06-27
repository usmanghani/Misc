using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace DotFermion.Qiblah
{
    public class GeoCoder
    {
        public GeoCoder(string key)
            : this(key, "", false)
        {
        }
        public GeoCoder(string key, string address)
            : this(key, address, false)
        {
        }
        public GeoCoder(string key, string address, bool sensorUsed)
        {
            this.Key = key;
            this.Address = address;
            this.SensorUsed = sensorUsed;
        }

        public static GeoCoder InitializeFromConfig()
        {
            return new GeoCoder(System.Configuration.ConfigurationSettings.AppSettings["GoogleMapsApiKey"]);
        }

        public string Key { get; set; }
        public string Address { get; set; }
        public bool SensorUsed { get; set; }
        private int _statusCode;
        public int StatusCode
        {
            get
            {
                return _statusCode;
            }
        }

        public Placemark FetchSimpleGeoCodeData()
        {
            WebRequest req = HttpWebRequest.Create(ConstructUrl(GeoCodeDataFormat.Csv));
            GeoCodeDataParser parser = new GMapsDataParser();
            GMapsParseData pd = (GMapsParseData)parser.Parse(GeoCodeDataFormat.Csv, req.GetResponse().GetResponseStream());
            this._statusCode = pd.StatusCode;
            return ((IList<Placemark>)pd.Placemarks)[0];
        }

        public IAsyncResult BeginFetchSimpleGeoCodeData(AsyncCallback callback, object state)
        {
            WebRequest req = HttpWebRequest.Create(ConstructUrl(GeoCodeDataFormat.Csv));
            return req.BeginGetResponse(new AsyncCallback(callback), req);
        }
        public Placemark EndFetchSimpleGeoCodeData(IAsyncResult ar)
        {
            return ResponseCallback(ar);
        }

        public List<Placemark> FetchExtendedGeoCodeData()
        {
            List<Placemark> placemarks = new List<Placemark>();
            WebRequest req = HttpWebRequest.Create(ConstructUrl(GeoCodeDataFormat.Xml));
            GeoCodeDataParser parser = new GMapsDataParser();
            GMapsParseData pd = (GMapsParseData)parser.Parse(GeoCodeDataFormat.Xml, req.GetResponse().GetResponseStream());
            this._statusCode = pd.StatusCode;
            return (List<Placemark>)pd.Placemarks;
        }

        private Placemark ResponseCallback(IAsyncResult iar)
        {
            WebResponse res = ((WebRequest)iar.AsyncState).EndGetResponse(iar);
            GeoCodeDataParser parser = new GMapsDataParser();
            GMapsParseData pd = (GMapsParseData)parser.Parse(GeoCodeDataFormat.Csv, res.GetResponseStream());
            this._statusCode = pd.StatusCode;
            return ((IList<Placemark>)pd.Placemarks)[0];
        }

        private const string Url = "http://maps.google.com/maps/geo?sensor={0}&output={3}&key={1}&q={2}";

        private string ConstructUrl(GeoCodeDataFormat format)
        {
            string result = string.Empty;
            switch (format)
            {
                case GeoCodeDataFormat.Xml:
                    result = string.Format(GeoCoder.Url, this.SensorUsed.ToString(),
                        this.Key, System.Web.HttpUtility.UrlEncode(this.Address), format.ToString().ToLower());
                    break;
              
                case GeoCodeDataFormat.Csv:
                default:
                    result = string.Format(GeoCoder.Url, this.SensorUsed.ToString(),
                        this.Key, System.Web.HttpUtility.UrlEncode(this.Address), format.ToString().ToLower());
                    break;
            }
            return result;
        }
    }
}
