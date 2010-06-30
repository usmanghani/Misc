using System;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using libpraytt;

namespace libpraytt
{
	public struct TimeListElement

	{
		public DateTime Date;
		public TimeSpan Fajr, Sunrise, Midday, Zuhr, AsrS, AsrH, Sunset, Isha;
	}
	public enum Prayers
	{
		Fajr, Sunrise, Midday, Zuhr, AsrS, AsrH, Sunset, Isha

	}
    public delegate void ProgressCallback ( int progress );
	public class TimeCalculator
	{

		/*
		 * This class is a comprehensive repository of functions that can be used to obtain prayer
		 * timings for different prayers on different dates.
		 * This class utilizes a thread pool to delegate tasks to worker threads that calculate the
		 * processor intensive declination tables for each yr.
		 * The availableYears hash contains the list of yrs for which the declination tables have been
		 * generated, the values are the tables themselves.
		 * The tableThreads contain pointers to currently executing threads.
		 * Prepare method is recommended to be used before calling any other function as it spawns a new thread
		 * calculate parameters in the background for that yr.
		 * The parameterized constructor should be used whenever possible.
		 * Parameters can be calculated for different levels of granularity.
		 * */

		//Hashtable threadData = new Hashtable ();
		//Hashtable availableYears = new Hashtable ();
		//bool tablesThreadDone = false;
		//Hashtable tableThreads = new Hashtable ();
		//Thread tableThread = new Thread (new ThreadStart(tableThreadProc));
		double latitude;
		//string latdir;
		double longitude;
		//string longdir;
		double daylightadjustment;
        //int dltadjsign;
		double gmtdiff ;

		#region This is kachra
		//		private bool waitForThread ( Thread t, int timeout, int times, string message )
		//		{	
		//			int joinCount = 0;
		//			bool joinResult = false;
		//			//wait times * timeout seconds before throwing an exception
		//			while ( (joinResult == false) && (joinCount < times) )
		//			{
		//				joinResult = t.Join (timeout);
		//				joinCount++;
		//			}
		//			if ( (joinResult == false) && joinCount == times )
		//			{
		//				throw new Exceptions.TableThreadTimeOutException(message);
		//			}
		//			return joinResult;
		//
		//		}
		//		private void tableThreadProc (  ) 
		//		{
		//			Hashtable dateData = new Hashtable ();
		//			Thread thread = Thread.CurrentThread;
		//			int hash = thread.GetHashCode ();
		//			ArrayList list = threadData [ hash ] as ArrayList;
		//			int yr = (int)list [ 0 ];
		//			double latitude = (double)list [ 1 ];
		//			double longitude = (double)list [ 2 ];
		//			double daylightadjustment = (double)list [ 3 ];
		//			string longdir = (string)list[4];
		//			string latdir = (string)list[5];
		//
		//			latitude = latitude/15;
		//			if (longdir.ToUpper() != "EAST")
		//			{
		//				latitude = -latitude;
		//			}
		//
		//			double lo_adjust = 0;
		//			int monthnum2 = 0;
		//			double timehours = 0;
		//			double inputdate = 0;
		//			double eotrise = 0;
		//			double decrise = 0;
		//			double decset = 0;
		//			double eotset = 0;
		//			for ( DateTime currentdate = DateTime.Parse ( "01/01/" + yr.ToString () ); currentdate < DateTime.Parse ( "01/01/" + yr.ToString () ).AddYears (1) ; currentdate = currentdate.AddDays ( 1 ) )
		//			{
		//				int monthnum = currentdate.Month;
		//				if (  monthnum2 != monthnum )
		//				{
		//					inputdate = 1;
		//					monthnum2 = currentdate.Month;
		//				}
		//					
		//				else
		//				{
		//					
		//					inputdate ++;
		//				}
		//				for (  int aa = 1 ; aa <= 2 ; aa ++ )
		//				{
		//
		//					if (  aa == 1  )
		//						timehours = 6 - latitude;
		//					else
		//						timehours = 18 - latitude;
		//
		//					double timeminutes = 0;				
		//					double inputminutesaftermidnight =  timehours * 60 + timeminutes;
		//
		//
		//					double solarminutesaftermidnight = inputminutesaftermidnight + lo_adjust + daylightadjustment;
		//					int correctedyear = 0;
		//					int correctedmonth = 0;					
		//				
		//					if (  monthnum > 2 )
		//					{
		//
		//						correctedyear = yr;
		//						correctedmonth = monthnum - 3;
		//					}
		//
		//					else
		//					{
		//
		//						correctedyear = yr - 1;
		//						correctedmonth = monthnum  + 9;
		//					}
		//                
		//				
		//
		//					double t = ((solarminutesaftermidnight / 60.0 / 24.0) + inputdate + Math.Floor (30.6 * correctedmonth + 0.5) + Math.Floor (365.25 * (correctedyear - 1976)) - 8707.5) / 36525.0;
		//
		//
		//					double G = 357.528 + 35999.05 * t;
		//					G = normalizeto360 (G);
		//
		//					double C = (1.915 * Math.Sin(G * Math.PI/180)) + (0.020 * Math.Sin (2.0 * G * Math.PI/180));
		//
		//					double l = 280.460 + (36000.770 * t) + C;
		//					l = normalizeto360 (l);
		//
		//					double alpha = l - 2.466 * Math.Sin (2.0 * l * Math.PI/180) + 0.053 *  Math.Sin (4.0 * l * Math.PI / 180);
		//
		//					double eotadjustment = (l - C - alpha) / 15.0 * 60.0;
		//					if ( aa == 1 ) 
		//						eotrise = eotadjustment;
		//					else 
		//						eotset = eotadjustment;
		//
		//
		//					double obliquity = 23.4393 - 0.013 * t;
		//					double declination = Math.Atan (Math.Tan (obliquity * Math.PI/180) * Math.Sin (alpha * Math.PI/180)) * 180 / Math.PI;
		//					if ( aa == 1 ) 
		//						decrise = declination;
		//					else 
		//						decset = declination;
		//
		//	
		//				}	
		//
		//				string s1 = currentdate.ToString();
		//				double s2 = decrise; //TimeSpan.FromHours ( decrise ).ToString();
		//				double s3 = decset ; //TimeSpan.FromHours ( decset ).ToString();
		//				double s4 = eotrise ; //TimeSpan.FromHours ( eotrise ).ToString();
		//				double s5 = eotset ; //TimeSpan.FromHours ( eotset ).ToString();
		//				Hashtable decData = new Hashtable();
		//				decData [ "Decl6" ] = s2;
		//				decData [ "Decl18" ] = s3;
		//				decData [ "Sunrise" ] = s4;
		//				decData [ "Sunset" ] = s5;
		//				dateData [ s1 ] = decData;
		//
		//			}
		//			lock ( availableYears.SyncRoot )
		//			{
		//				availableYears [ yr.ToString () ] = dateData;
		//			}
		//			lock ( tableThreads.SyncRoot )
		//			{
		//				if ( tableThreads.ContainsKey ( hash ) )
		//				{
		//					tableThreads.Remove ( hash );
		//				}
		//
		//			}
		//			lock ( threadData.SyncRoot )
		//			{
		//				if ( threadData.ContainsKey ( hash ) )
		//				{
		//					threadData.Remove ( hash ) ;
		//				}
		//			}
		//		}
		//		private void prayThreadProc ()
		//		{
		//
		//		}

		#endregion

		#region private methods

		private double normalizeto360 ( double input )
		{
			return input - Math.Floor ( input / 360 ) * 360;

		}
		private double[] calcTableVal ( DateTime date , double latitude, double longitude, double daylightadjustment )
		{

            try
            {
                string longdir = longitude < 0 ? "WEST" : "EAST";
                latitude = Math.Abs(latitude);
                latitude = latitude / 15;
                if (longdir.ToUpper() != "EAST")
                {
                    latitude = -latitude;
                }

                double lo_adjust = 0;
                double timehours = 0;
                double inputdate = date.Day;
                double eotrise = 0;
                double decrise = 0;
                double decset = 0;
                double eotset = 0;
                int monthnum = date.Month;
                for (int aa = 1; aa <= 2; aa++)
                {

                    if (aa == 1)
                        timehours = 6 - latitude;
                    else
                        timehours = 18 - latitude;
                    double timeminutes = 0;
                    double inputminutesaftermidnight = timehours * 60 + timeminutes;
                    double solarminutesaftermidnight = inputminutesaftermidnight + lo_adjust + daylightadjustment;
                    int correctedyear = 0;
                    int correctedmonth = 0;
                    if (monthnum > 2)
                    {
                        correctedyear = date.Year;
                        correctedmonth = monthnum - 3;
                    }

                    else
                    {
                        correctedyear = date.Year - 1;
                        correctedmonth = monthnum + 9;
                    }
                    double t = ((solarminutesaftermidnight / 60.0 / 24.0) + inputdate + Math.Floor(30.6 * correctedmonth + 0.5) + Math.Floor(365.25 * (correctedyear - 1976)) - 8707.5) / 36525.0;
                    double G = 357.528 + 35999.05 * t;
                    G = normalizeto360(G);
                    double C = (1.915 * Math.Sin(G * Math.PI / 180)) + (0.020 * Math.Sin(2.0 * G * Math.PI / 180));
                    double l = 280.460 + (36000.770 * t) + C;
                    l = normalizeto360(l);
                    double alpha = l - 2.466 * Math.Sin(2.0 * l * Math.PI / 180) + 0.053 * Math.Sin(4.0 * l * Math.PI / 180);
                    double eotadjustment = (l - C - alpha) / 15.0 * 60.0;
                    if (aa == 1)
                        eotrise = eotadjustment;
                    else
                        eotset = eotadjustment;
                    double obliquity = 23.4393 - 0.013 * t;
                    double declination = Math.Atan(Math.Tan(obliquity * Math.PI / 180) * Math.Sin(alpha * Math.PI / 180)) * 180 / Math.PI;
                    if (aa == 1)
                        decrise = declination;
                    else
                        decset = declination;

                }

                double[] data = new double[] {
											   decrise,
											   decset,
											   eotrise,
											   eotset
										   };

                return data;

            }
            catch (Exception ex)
            {

                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

            }
		}

		double degreesToRadians ( double d )
		{
			return d * Math.PI / 180;
		}
		double radiansToDegrees ( double d )
		{
			return d * 180 / Math.PI;
		}

		private TimeSpan calcTime ( DateTime date, bool rise, double boodkokab )
		{
            try
            {

                Debug.WriteLine(TimeSpan.FromHours(latitude).ToString(), "latitude");
                double[] data = calcTableVal(date, latitude, longitude, daylightadjustment);
                double declination;
                if (rise)
                    declination = data[0];
                else
                    declination = data[1];
                Debug.WriteLine(TimeSpan.FromHours(declination).ToString(), "declination");
                double sunriseset;
                if (rise)
                    sunriseset = data[2];
                else
                    sunriseset = data[3];
                Debug.WriteLine(TimeSpan.FromHours(sunriseset).ToString(), "sunriseset");
                //double boodkokab = TimeSpan.FromHours(108).TotalHours ;
                double boodfoqani = Math.Abs(latitude + (-declination));
                //			if ( (latitude < 0 && declination < 0) || (declination > 0 && latitude > 0) )
                //				boodfoqani = Math.Abs ( latitude ) - Math.Abs ( declination );
                //			else 
                //				boodfoqani = Math.Abs ( latitude ) + Math.Abs (declination);
                Debug.WriteLine(TimeSpan.FromHours(boodfoqani).ToString(), "boodfoqani");
                double meezan = boodfoqani + boodkokab;
                double nisfulmajmoo = meezan / 2;
                double hasiltafreeq = boodkokab - nisfulmajmoo;
                double majmoaarba = Math.Log10(1 / Math.Cos(degreesToRadians(Math.Abs(latitude))))
                    + Math.Log10(1 / Math.Cos(degreesToRadians(Math.Abs(declination))))
                    + Math.Log10(Math.Sin(degreesToRadians(Math.Abs(nisfulmajmoo))))
                    + Math.Log10(Math.Sin(degreesToRadians(Math.Abs(hasiltafreeq))));
                Debug.WriteLine(TimeSpan.FromHours(majmoaarba).ToString(), "majmoaarba");
                double jbgroob = 8.0 / 60 * radiansToDegrees(Math.Asin(Math.Pow(10, (majmoaarba + 20) / 2 - 10)));
                Debug.WriteLine(TimeSpan.FromHours(jbgroob).ToString(), "jbgroob");
                double jbtulu = 12 - jbgroob;
                Debug.WriteLine(TimeSpan.FromHours(jbtulu).ToString(), "jbtulu");
                sunriseset = -sunriseset;
                double tadeeltime = TimeSpan.FromMinutes(Math.Abs(longitude) * 4).TotalHours;
                double tadeelmurawaj = Math.Abs(gmtdiff) - tadeeltime;
                Debug.WriteLine(TimeSpan.FromHours(tadeeltime).ToString(), "tadeeltime");
                Debug.WriteLine(TimeSpan.FromHours(tadeelmurawaj).ToString(), "tadeel");
                double time;
                if (rise)
                    time = jbtulu + TimeSpan.FromMinutes(sunriseset).TotalHours;
                else
                    time = jbgroob + TimeSpan.FromMinutes(sunriseset).TotalHours;
                Debug.WriteLine(TimeSpan.FromHours(time).ToString(), "timebeforetadeel");
                time += tadeelmurawaj;
                Debug.WriteLine(TimeSpan.FromHours(time).ToString(), "time");
                return TimeSpan.FromHours(time);
            }
            catch (Exception ex)
            {
                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;


            }

		}
		private TimeSpan calcFajrTime ( DateTime date )
		{
			return calcTime( date, true, new TimeSpan ( 108, 0 , 0 ).TotalHours );
		}
		private TimeSpan calcTuluTime ( DateTime date )
		{
			return calcTime ( date , true , new TimeSpan ( 90, 49 , 0 ).TotalHours );

		}
		private TimeSpan calcZawalTime ( DateTime date )
		{
			TimeSpan fajr = calcFajrTime (date);
			TimeSpan ghroob = calcMaghribTime ( date );
			TimeSpan result = TimeSpan.FromHours(fajr.Add ( ghroob ).TotalHours / 2 + 6);
			return result;
		}
		private TimeSpan calcZuhrTime ( DateTime date )
		{
			TimeSpan tulu = calcTuluTime ( date );
			TimeSpan ghroob = calcMaghribTime ( date );
			TimeSpan result = TimeSpan.FromHours ( tulu.Add ( ghroob ).TotalHours / 2 + 6 );
			return result;
		}
		private TimeSpan calcAsrSTime ( DateTime date )
		{

			double [] data = calcTableVal ( date, latitude, longitude, daylightadjustment );
			double declination;
			declination = data [ 1 ];
			double boodfoqani = Math.Abs ( latitude + ( - declination ) );
			double boodkokab = radiansToDegrees(Math.Atan ( Math.Tan (  degreesToRadians(boodfoqani) ) + 1 )) ;
			return calcTime ( date, false, boodkokab );
		}
		private TimeSpan calcAsrHTime ( DateTime date )
		{
			double [] data = calcTableVal ( date, latitude, longitude, daylightadjustment );
			double declination;
			declination = data [ 1 ];
			double boodfoqani = Math.Abs ( latitude + ( - declination ) );
			double boodkokab = radiansToDegrees(Math.Atan ( Math.Tan (  degreesToRadians(boodfoqani) ) + 2 )) ;
			return calcTime ( date, false, boodkokab );

		}
		private TimeSpan calcMaghribTime ( DateTime date )
		{
			return calcTime ( date , false , new TimeSpan ( 90, 49 , 0 ).TotalHours );
		}
		private TimeSpan calcIshaTime ( DateTime date )
		{
			return calcTime( date, false, new TimeSpan ( 108, 0 , 0 ).TotalHours );	
		}

		#endregion

		public TimeCalculator ( double longitude, double latitude, double daylightadjustment, double gmtdiff )
		{
			this.latitude = latitude;
			this.longitude = longitude;
			this.daylightadjustment = daylightadjustment;
			this.gmtdiff = gmtdiff;

			//this.PrepareTables ( DateTime.Now.Year, latitude, latdir, longitude, longdir, daylightadjustment, dltadjsign );

		}

		public TimeCalculator ( )
		{
			
		}
//		public Thread PrepareTables ( int yr , double latitude , string latdir, double longitude, string longdir, double daylightadjustment, int dltadjsign ) 
//		{
//			// this arraylist is used to pass data to the thread being spawned.
//			// the list contains yr , lo, lat , and dltadjust in that order.
//			ArrayList list = new ArrayList ( );
//			list.Add ( yr );
//			list.Add ( latitude );
//			list.Add ( longitude );
//			list.Add ( daylightadjustment );
//			list.Add ( longdir );
//			list.Add ( latdir );
//			
//			Thread t = new Thread ( new ThreadStart ( this.tableThreadProc ) );
//			lock ( tableThreads.SyncRoot )
//			{
//				tableThreads.Add ( t.GetHashCode () , t );
//			}
//			lock ( availableYears.SyncRoot )
//			{
//				availableYears.Add ( yr.ToString () , new Hashtable () );
//			}			
//			lock (threadData.SyncRoot )
//			{
//				threadData.Add ( t.GetHashCode () , list );
//			}
//
//			t.Name = "PTCalc - " + t.GetHashCode () + " - Year : " + yr.ToString () + " - Latitude : " + latitude.ToString () + " - Longitude : " + longitude.ToString () + " - DaylightAdjustment : "  + daylightadjustment.ToString () ;
//			t.Start ();
//			return t;
//
//		}
//
		public Hashtable GetFullTimingsForYear ( int yr )
		{
            try
            {
                Hashtable datedata = new Hashtable();
                DateTime startdate = new DateTime(yr, 1, 1);
                DateTime stopdate = new DateTime(yr + 1, 1, 1);
                for (DateTime date = startdate; date < stopdate; date = date.AddDays(1))
                {
                    TimeListElement time = new TimeListElement();
                    time.Date = date;
                    time.Fajr = calcFajrTime(date);
                    time.Sunrise = calcTuluTime(date);
                    time.Midday = calcZawalTime(date);
                    time.Zuhr = calcZuhrTime(date);
                    time.AsrS = calcAsrSTime(date);
                    time.AsrH = calcAsrHTime(date);
                    time.Sunset = calcMaghribTime(date);
                    time.Isha = calcIshaTime(date);
                    datedata[date.ToShortDateString()] = time;

                }

                //			Hashtable data = null;
                //			bool result = false;
                //			lock ( availableYears.SyncRoot )
                //			{
                //				result = availableYears.ContainsKey ( yr.ToString () );
                //			}
                //			if ( result == true )
                //			{
                //				//lock so that we dont return half-baked data to the client.
                //
                //				lock ( availableYears.SyncRoot )
                //				{
                //					data = (Hashtable)availableYears[yr.ToString()];
                //				}
                //				return data;
                //               
                //			}
                //			else
                //			{
                //				Thread t = PrepareTables ( yr , latitude, latdir, longitude, longdir , daylightadjustment , dltadjsign);
                //				waitForThread ( t , 1000, 5 , "Time routine for Year : " + yr.ToString() + " timed out."  );
                //				lock ( availableYears.SyncRoot )
                //				{
                //					data = (Hashtable)availableYears[yr.ToString()];
                //				}
                //				return data;
                //                
                //			}
                //
                return datedata;

            }
            catch (Exception ex)
            {
                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

            }
		}
		public Hashtable GetFullTimingsForMonth ( int month, int yr )
		{

            try
            {
                Hashtable datedata = new Hashtable();
                DateTime startdate = new DateTime(yr, month, 1);
                DateTime stopdate = new DateTime(yr, month + 1, 1);
                for (DateTime date = startdate; date < stopdate; date = date.AddDays(1))
                {
                    TimeListElement time = new TimeListElement();
                    time.Date = date;
                    time.Fajr = calcFajrTime(date);
                    time.Sunrise = calcTuluTime(date);
                    time.Midday = calcZawalTime(date);
                    time.Zuhr = calcZuhrTime(date);
                    time.AsrS = calcAsrSTime(date);
                    time.AsrH = calcAsrHTime(date);
                    time.Sunset = calcMaghribTime(date);
                    time.Isha = calcIshaTime(date);
                    datedata[date.ToShortDateString()] = time;

                }

                //			bool result = false;
                //			lock ( availableYears.SyncRoot )
                //			{
                //				result = availableYears.ContainsKey ( yr.ToString () );
                //			}
                //			if ( result == false )
                //			{
                //				Thread t = PrepareTables ( yr , latitude, latdir, longitude, longdir , daylightadjustment, dltadjsign );
                //				waitForThread ( t , 1000 , 5 , "Time routine for Month : " 
                //					+ month.ToString () + " Year : " + yr.ToString() + " timed out." );
                //               
                //			}
                //			//lock so that we dont return half-baked data to the client.
                //			Hashtable data;
                //			lock ( availableYears.SyncRoot )
                //			{
                //				data = (Hashtable)availableYears[yr.ToString()];
                //			}
                //			return data;
                return datedata;

            }
            catch (Exception ex)
            {

                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

            }
		}
		public Hashtable GetSummaryTimingsForYear ( int yr, int granularity  )
		{
            try
            {
                Hashtable datedata = new Hashtable();
                DateTime startdate = new DateTime(yr, 1, 1);
                DateTime stopdate = new DateTime(yr + 1, 1, 1);
                for (DateTime date = startdate; date < stopdate; date = date.AddDays(granularity))
                {
                    TimeListElement time = new TimeListElement();
                    time.Date = date;
                    time.Fajr = calcFajrTime(date);
                    time.Sunrise = calcTuluTime(date);
                    time.Midday = calcZawalTime(date);
                    time.Zuhr = calcZuhrTime(date);
                    time.AsrS = calcAsrSTime(date);
                    time.AsrH = calcAsrHTime(date);
                    time.Sunset = calcMaghribTime(date);
                    time.Isha = calcIshaTime(date);
                    datedata[date.ToShortDateString()] = time;

                }
                return datedata;
            }
            catch (Exception ex)
            {

                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

            }

		}
		public Hashtable GetSummaryTimingsForMonth ( int month, int yr, int granularity )
		{

            try
            {
                Hashtable datedata = new Hashtable();
                DateTime startdate = new DateTime(yr, month, 1);
                DateTime stopdate = new DateTime(yr, month + 1, 1);
                for (DateTime date = startdate; date < stopdate; date = date.AddDays(granularity))
                {
                    TimeListElement time = new TimeListElement();
                    time.Date = date;
                    time.Fajr = calcFajrTime(date);
                    time.Sunrise = calcTuluTime(date);
                    time.Midday = calcZawalTime(date);
                    time.Zuhr = calcZuhrTime(date);
                    time.AsrS = calcAsrSTime(date);
                    time.AsrH = calcAsrHTime(date);
                    time.Sunset = calcMaghribTime(date);
                    time.Isha = calcIshaTime(date);
                    datedata[date.ToShortDateString()] = time;

                }
                return datedata;

            }
            catch (Exception ex)
            {
                
                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

            }

		}
		public Hashtable GetTimingsForDate ( DateTime date )
		{

            try
            {
                Hashtable datedata = new Hashtable();
                TimeListElement time = new TimeListElement();
                time.Date = date;
                time.Fajr = calcFajrTime(date);
                time.Sunrise = calcTuluTime(date);
                time.Midday = calcZawalTime(date);
                time.Zuhr = calcZuhrTime(date);
                time.AsrS = calcAsrSTime(date);
                time.AsrH = calcAsrHTime(date);
                time.Sunset = calcMaghribTime(date);
                time.Isha = calcIshaTime(date);
                datedata[date.ToShortDateString()] = time;
                return datedata;

            }
            catch (Exception ex)
            {

                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

            }
		}
		public Hashtable GetTimingsBetween ( DateTime startdate , DateTime stopdate )
		{

            try
            {
                Hashtable datedata = new Hashtable();
                for (DateTime date = startdate; date <= stopdate; date = date.AddDays(1))
                {
                    TimeListElement time = new TimeListElement();
                    time.Date = date;
                    time.Fajr = calcFajrTime(date);
                    time.Sunrise = calcTuluTime(date);
                    time.Midday = calcZawalTime(date);
                    time.Zuhr = calcZuhrTime(date);
                    time.AsrS = calcAsrSTime(date);
                    time.AsrH = calcAsrHTime(date);
                    time.Sunset = calcMaghribTime(date);
                    time.Isha = calcIshaTime(date);
                    datedata[date.ToShortDateString()] = time;

                }
                return datedata;


            }
            catch (Exception ex)
            {

                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

            }
        }
		public Hashtable GetPrayerTimingsForYear ( int yr, Prayers prayer  )
		{

            try
            {
                Hashtable datedata = new Hashtable();
                DateTime startdate = new DateTime(yr, 1, 1);
                DateTime stopdate = new DateTime(yr + 1, 1, 1);
                for (DateTime date = startdate; date < stopdate; date = date.AddDays(1))
                {
                    TimeListElement time = new TimeListElement();
                    time.Date = date;
                    switch (prayer)
                    {
                        case Prayers.Fajr:
                            time.Fajr = calcFajrTime(date);
                            break;
                        case Prayers.Sunrise:
                            time.Sunrise = calcTuluTime(date);
                            break;
                        case Prayers.Midday:
                            time.Midday = calcZawalTime(date);
                            break;
                        case Prayers.Zuhr:
                            time.Zuhr = calcZuhrTime(date);
                            break;
                        case Prayers.AsrS:
                            time.AsrS = calcAsrSTime(date);
                            break;
                        case Prayers.AsrH:
                            time.AsrH = calcAsrHTime(date);
                            break;
                        case Prayers.Sunset:
                            time.Sunset = calcMaghribTime(date);
                            break;
                        case Prayers.Isha:
                            time.Isha = calcIshaTime(date);
                            break;

                    }

                    datedata[date.ToShortDateString()] = time;

                }
                return datedata;

            }
            catch (Exception ex)
            {

                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

            }			
		}
		public Hashtable GetPrayerTimingsForMonth ( int month, int yr, Prayers prayer )
		{
            try
            {
                Hashtable datedata = new Hashtable();
                DateTime startdate = new DateTime(yr, month, 1);
                DateTime stopdate = new DateTime(yr, month + 1, 1);
                for (DateTime date = startdate; date < stopdate; date = date.AddDays(1))
                {
                    TimeListElement time = new TimeListElement();
                    time.Date = date;
                    switch (prayer)
                    {
                        case Prayers.Fajr:
                            time.Fajr = calcFajrTime(date);
                            break;
                        case Prayers.Sunrise:
                            time.Sunrise = calcTuluTime(date);
                            break;
                        case Prayers.Midday:
                            time.Midday = calcZawalTime(date);
                            break;
                        case Prayers.Zuhr:
                            time.Zuhr = calcZuhrTime(date);
                            break;
                        case Prayers.AsrS:
                            time.AsrS = calcAsrSTime(date);
                            break;
                        case Prayers.AsrH:
                            time.AsrH = calcAsrHTime(date);
                            break;
                        case Prayers.Sunset:
                            time.Sunset = calcMaghribTime(date);
                            break;
                        case Prayers.Isha:
                            time.Isha = calcIshaTime(date);
                            break;

                    }

                    datedata[date.ToShortDateString()] = time;

                }
                return datedata;

            }
            catch (Exception ex)
            {
                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;
            }
        }
		public Hashtable GetPrayerTimingsForDate ( DateTime date, Prayers prayer  )
		{
            try
            {
                Hashtable datedata = new Hashtable();
                TimeListElement time = new TimeListElement();
                time.Date = date;
                switch (prayer)
                {
                    case Prayers.Fajr:
                        time.Fajr = calcFajrTime(date);
                        break;
                    case Prayers.Sunrise:
                        time.Sunrise = calcTuluTime(date);
                        break;
                    case Prayers.Midday:
                        time.Midday = calcZawalTime(date);
                        break;
                    case Prayers.Zuhr:
                        time.Zuhr = calcZuhrTime(date);
                        break;
                    case Prayers.AsrS:
                        time.AsrS = calcAsrSTime(date);
                        break;
                    case Prayers.AsrH:
                        time.AsrH = calcAsrHTime(date);
                        break;
                    case Prayers.Sunset:
                        time.Sunset = calcMaghribTime(date);
                        break;
                    case Prayers.Isha:
                        time.Isha = calcIshaTime(date);
                        break;

                }

                datedata[date.ToShortDateString()] = time;
                return datedata;

            }
            catch (Exception ex)
            {
                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

            }				
		}
		public Hashtable GetPrayerTimingsBetween (DateTime startdate, DateTime stopdate, Prayers prayer)
		{
            try
            {
                Hashtable datedata = new Hashtable();
                for (DateTime date = startdate; date <= stopdate; date = date.AddDays(1))
                {
                    TimeListElement time = new TimeListElement();
                    time.Date = date;
                    switch (prayer)
                    {
                        case Prayers.Fajr:
                            time.Fajr = calcFajrTime(date);
                            break;
                        case Prayers.Sunrise:
                            time.Sunrise = calcTuluTime(date);
                            break;
                        case Prayers.Midday:
                            time.Midday = calcZawalTime(date);
                            break;
                        case Prayers.Zuhr:
                            time.Zuhr = calcZuhrTime(date);
                            break;
                        case Prayers.AsrS:
                            time.AsrS = calcAsrSTime(date);
                            break;
                        case Prayers.AsrH:
                            time.AsrH = calcAsrHTime(date);
                            break;
                        case Prayers.Sunset:
                            time.Sunset = calcMaghribTime(date);
                            break;
                        case Prayers.Isha:
                            time.Isha = calcIshaTime(date);
                            break;

                    }

                    datedata[date.ToShortDateString()] = time;

                }
                return datedata;

            }
            catch (Exception ex)
            {
                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;
            }			
		}

		public Hashtable GetPrayerTimingsBetween2 (DateTime startdate, DateTime stopdate, Prayers[] prayers, bool summarize, int granularity, ProgressCallback pcb)
		{

            try
            {
                if (!summarize) granularity = 1;
                TimeSpan diff = stopdate - startdate;
                diff.Add(TimeSpan.FromDays(1));
                float progressstep = 100 / (float)diff.Days;
                float progress = 0;
                Hashtable datedata = new Hashtable();
                for (DateTime date = startdate; date <= stopdate; date = date.AddDays(granularity))
                {
                    TimeListElement time = new TimeListElement();
                    time.Date = date;
                    foreach (Prayers p in prayers)
                    {
                        switch (p)
                        {
                            case Prayers.Fajr:
                                time.Fajr = calcFajrTime(date);
                                break;
                            case Prayers.Sunrise:
                                time.Sunrise = calcTuluTime(date);
                                break;
                            case Prayers.Midday:
                                time.Midday = calcZawalTime(date);
                                break;
                            case Prayers.Zuhr:
                                time.Zuhr = calcZuhrTime(date);
                                break;
                            case Prayers.AsrS:
                                time.AsrS = calcAsrSTime(date);
                                break;
                            case Prayers.AsrH:
                                time.AsrH = calcAsrHTime(date);
                                break;
                            case Prayers.Sunset:
                                time.Sunset = calcMaghribTime(date);
                                break;
                            case Prayers.Isha:
                                time.Isha = calcIshaTime(date);
                                break;

                        }
                    }
                    progress += progressstep;
                    pcb((int)progress);
                    datedata[date.ToShortDateString()] = time;
                }
                return datedata;

            }
            catch (Exception ex)
            {
                Exceptions.TimeCalculatorException exception = new libpraytt.Exceptions.TimeCalculatorException(ex.Message);
                exception.OriginalException = ex;
                throw exception;

                                
            }	
		
		}

	}

}
