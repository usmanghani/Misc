using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotFermion.Qiblah;

namespace QiblahCalculator
{
    public class Utils
    {
        public static GeoCoder CreateGeoCoder(string address, bool gpsUsed)
        {
            return new GeoCoder(System.Configuration.ConfigurationSettings.AppSettings["GoogleMapsApiKey"], address, gpsUsed);
        }

        public static double CalculateQiblah(GeoCoder geoCoder)
        {
            double result = double.NaN;
            if (geoCoder.StatusCode != 200)
            {
                throw new ApplicationException("Address not found");
            }
            else
            {
                DotFermion.Qiblah.QiblahCalculator calc =
                    new DotFermion.Qiblah.QiblahCalculator(geoCoder.Latitude,
                        geoCoder.Longitude);
                result = calc.Qiblah;
            }
            return result;
        }

        public static string StringizeQiblah(double qiblah)
        {
            string formatString = "##0.0#";
            if (qiblah < 0)
            {
                return Math.Abs(qiblah).ToString(formatString) + " degrees West of North";
            }
            return qiblah.ToString(formatString) + " degrees East of North";
        }
    }
}
