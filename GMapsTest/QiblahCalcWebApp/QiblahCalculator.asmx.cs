using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using DotFermion.Qiblah;

namespace QiblahCalculator
{
    /// <summary>
    /// Summary description for QiblahCalculator1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class QiblahCalculatorWS : System.Web.Services.WebService
    {
        [WebMethod]
        public double FindQiblah(string address, bool gpsUsed)
        {
            GeoCoder geoCoder = Utils.CreateGeoCoder(address, gpsUsed);
            return Utils.CalculateQiblah(geoCoder, geoCoder.FetchSimpleGeoCodeData());
        }
        [WebMethod]
        public string FindQiblahString(string address, bool gpsUsed)
        {
            GeoCoder geoCoder = Utils.CreateGeoCoder(address, gpsUsed);
            return Utils.StringizeQiblah(Utils.CalculateQiblah(geoCoder, geoCoder.FetchSimpleGeoCodeData()));
        }
        [WebMethod]
        public List<QiblahLocation> FindQiblahExtended(string address, bool gpsUsed)
        {
            GeoCoder geoCoder = Utils.CreateGeoCoder(address, gpsUsed);
            List<Placemark> placemarks = geoCoder.FetchExtendedGeoCodeData();
            var query = from p in placemarks
                        select new QiblahLocation()
                        {
                            Address = p.Address,
                            Qiblah = Utils.CalculateQiblah(geoCoder, p),
                            QiblahString = Utils.StringizeQiblah(Utils.CalculateQiblah(geoCoder, p))
                        };
            return query.ToList();
        }
    }
}
