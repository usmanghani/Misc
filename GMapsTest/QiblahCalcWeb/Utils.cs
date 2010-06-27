using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceHosting.ServiceRuntime;
using DotFermion.Qiblah;

namespace QiblahCalculator
{
    public class Utils
    {
        public static GeoCoder CreateGeoCoder(string address, bool gpsUsed)
        {
            string key = string.Empty;
            if (RoleManager.IsRoleManagerRunning)
            {
                key = RoleManager.GetConfigurationSetting("GoogleMapsApiKey");
                RoleManager.WriteToLog("Information", "Using GMaps Key: " + key);
            }
            else
            {
                key = System.Configuration.ConfigurationSettings.AppSettings["GoogleMapsApiKey"];
                try
                {
                    //if (!System.Diagnostics.EventLog.SourceExists("QiblahCalc"))
                    //System.Diagnostics.EventLog.CreateEventSource("QiblahCalc", "Application");

                }
                catch (Exception)
                {
                }
                finally
                {
                    //System.Diagnostics.EventLog.WriteEntry("QiblahCalc", "Using GMaps Key: " + key);
                }

            }
            return new GeoCoder(key, address, gpsUsed);
        }

        public static double CalculateQiblah(GeoCoder g, Placemark p)
        {
            double result = double.NaN;
            if (g.StatusCode != GeoErrors.G_GEO_SUCCESS)
            {
                throw new ApplicationException("Cannot locate Qiblah for your location.");
            }
            else
            {
                DotFermion.Qiblah.QiblahCalculator calc =
                    new DotFermion.Qiblah.QiblahCalculator(p.Latitude,
                        p.Longitude);
                result = calc.Qiblah;
            }
            return result;
        }

        public static double CalculateQiblah(double longitude, double latitude)
        {
            double result = double.NaN;
            DotFermion.Qiblah.QiblahCalculator calc = new DotFermion.Qiblah.QiblahCalculator(latitude, longitude);
            result = calc.Qiblah;
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
