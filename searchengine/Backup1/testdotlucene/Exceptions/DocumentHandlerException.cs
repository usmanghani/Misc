using System;
using System.Collections.Generic;
using System.Text;

namespace DotFermion.Exceptions
{
    class DocumentHandlerException:Exception
    {
        public DocumentHandlerException():base()
        {
        }
        public DocumentHandlerException(string message):base(message)
        {
        }
    } 
}
