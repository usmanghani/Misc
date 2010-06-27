using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace QiblahCalculator
{
    // NOTE: If you change the interface name "IQiblahCalculator" here, you must also update the reference to "IQiblahCalculator" in Web.config.
    [ServiceContract]
    public interface IQiblahCalculatorService
    {
        [OperationContract]
        double FindQiblah(string address, bool gpsUsed);
        //[OperationContract(AsyncPattern=true)]
        //IAsyncResult BeginFindQiblah(string address, bool gpsUsed, AsyncCallback callback, object state);
        //double EndFindQiblah(IAsyncResult ar);
    }
}
