using System;
using System.Collections.Generic;
using System.Text;
using libpraytt;

namespace praytt
{
    public class BugReport
    {
        
        public BugReport()
        {

        }

        public BugReport(LocDataElement loc, DateTime from, DateTime to, Prayers[] p)
        {

            location = loc;
            fromdate = from;
            todate = to;
            prayers = p;


        }

        LocDataElement location;
        public LocDataElement Location
        {
            get { return location; }
            set { location = value; }
        }

        DateTime fromdate;
        public DateTime FromDate
        {
            get { return fromdate; }
            set { fromdate = value; }
        }

        DateTime todate;
        public DateTime ToDate
        {
            get { return todate; }
            set { todate = value; }
        }

        Prayers[] prayers;
        public Prayers[] Prayers
        {
            get { return prayers; }
            set { prayers = value;}

        }	    

    }

}
