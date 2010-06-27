using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DotFermion.Qiblah;

namespace QiblahCalculator
{
    // NOTE: If you change the class name "QiblahCalculator" here, you must also update the reference to "QiblahCalculator" in Web.config.
    public class QiblahCalculatorService : IQiblahCalculatorService
    {
        #region IQiblahCalculatorService Members

        //IAsyncResult IQiblahCalculatorService.BeginFindQiblah(string address, bool gpsUsed, AsyncCallback callback, object state)
        //{
        //    GeoCoder geoCoder = new GeoCoder(System.Configuration.ConfigurationSettings.AppSettings["GoogleMapsApiKey"], address, gpsUsed);
        //    AsyncState asyncState = new AsyncState();
        //    asyncState.GeoCoder = geoCoder;
        //    asyncState.State = state;
        //    return geoCoder.BeginFetchGeoCodeData(new AsyncCallback(callback), asyncState);
        //}

        //double IQiblahCalculatorService.EndFindQiblah(IAsyncResult ar)
        //{
        //    double result = double.NaN;
        //    GeoCoder geoCoder = (ar.AsyncState as AsyncState).GeoCoder;
        //    if (geoCoder.StatusCode != 200)
        //    {
        //        throw new ApplicationException("Address not found");
        //    }
        //    else
        //    {
        //        DotFermion.Qiblah.QiblahCalculator calc =
        //            new DotFermion.Qiblah.QiblahCalculator(geoCoder.Latitude,
        //                geoCoder.Longitude);
        //        result = calc.Qiblah;
        //    }
        //    return result;
        //}

        #endregion
        #region IQiblahCalculatorService Members

        double IQiblahCalculatorService.FindQiblah(string address, bool gpsUsed)
        {
            GeoCoder geoCoder = Utils.CreateGeoCoder(address, gpsUsed);
            //GeoCoder geoCoder = new GeoCoder(System.Configuration.ConfigurationSettings.AppSettings["GoogleMapsApiKey"], address, gpsUsed);
            geoCoder.FetchGeoCodeData();
            return Utils.CalculateQiblah(geoCoder);
        }
        #endregion
    }
    class AsyncState
    {
        public object State { get; set; }
        public GeoCoder GeoCoder { get; set; }
    }

}
