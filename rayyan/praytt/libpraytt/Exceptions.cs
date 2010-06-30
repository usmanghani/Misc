using System;
namespace libpraytt
{
	namespace Exceptions
	{
        public class TimeCalculatorException : Exception
        {
            private Exception originalException;

            public Exception OriginalException
            {
                get { return originalException; }
                set { originalException = value; }
            }

            public TimeCalculatorException():base ( "Time Calculator Crashed." ) {}
            public TimeCalculatorException(string message) : base(message) { }

        }

		public class TableThreadTimeOutException : Exception
		{
			public TableThreadTimeOutException():base("TableThread Timed Out"){}
			public TableThreadTimeOutException ( string message ):base(message){}
		}

	}

}
