using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace Mediqura.Utility.Logging
{
  /****************************************************
   Description of the class	    : ORHelper
   Created Date					: 20-07-2016
   Developer					: Loken
   Modify Date					: 
   Modified By Developer		: 
   Comments						: ()
   ***************************************************/
    // <summary>
    /// Trace listener that writes formatted messages to the Visual Studio debugger output.
    /// </summary>
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class DebugTraceListener : CustomTraceListener
    {
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (data is LogEntry && this.Formatter != null)
            {
                this.WriteLine(this.Formatter.Format(data as LogEntry));
            }
            else
            {
                this.WriteLine(data.ToString());
            }
        }

        /// <summary>
        /// Writes a message to the debug window 
        /// </summary>
        /// <param name="message">The string to write to the debug window</param>
        public override void Write(string message)
        {
            Debug.Write(message);
        }

        /// <summary>
        /// Writes a message to the debug window 
        /// </summary>
        /// <param name="message">The string to write to the debug window</param>
        public override void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }

    }
}
