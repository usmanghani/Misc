using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Log;
using System.Linq;
using System.Text;

namespace GreHelper
{
    public class Logger
    {
        private const string DATETIME_FORMAT_STRING = "MM.dd.yyyy-HH.mm.ss.ffff";
        private FileRecordSequence logger = null;
        public Logger()
        {
            string fileName = Properties.Settings.Default.LogFilePath;
            logger = new FileRecordSequence(fileName, FileAccess.ReadWrite);
        }
        
        public void Write(string str)
        {
            Write(str, false);
        }
        
        public void Write(string format, params object[] parameters)
        {
            Write(String.Format(format, parameters));
        }

        public void Write(string str, bool forceFlush)
        {
            RecordAppendOptions options = RecordAppendOptions.None;
            if(forceFlush)
            {
                options = RecordAppendOptions.ForceFlush;
            }

            logger.Append(CreateLogRecord(str), SequenceNumber.Invalid, SequenceNumber.Invalid, options);
        }

        // Create a log record.
        private static IList<ArraySegment<byte>> CreateLogRecord(string str)
        {
            Encoding enc = Encoding.Unicode;

            string internalString = DateTime.UtcNow.ToString(DATETIME_FORMAT_STRING) + " -- " + str;
            byte[] array = enc.GetBytes(internalString);

            ArraySegment<byte>[] segments = new ArraySegment<byte>[1];
            segments[0] = new ArraySegment<byte>(array);

            return Array.AsReadOnly<ArraySegment<byte>>(segments);
        }

        // Read the records added to the log. 
        public string[] ReadRecords()
        {
            List<string> logRecords = new List<string>();
            Encoding enc = Encoding.Unicode;
            foreach (LogRecord record in logger.ReadLogRecords(logger.BaseSequenceNumber, LogRecordEnumeratorType.Next))
            {
                byte[] data = new byte[record.Data.Length];
                record.Data.Read(data, 0, (int)record.Data.Length);
                string mystr = enc.GetString(data);
                logRecords.Add(mystr);
            }
            return logRecords.ToArray();
        }
    }
}
