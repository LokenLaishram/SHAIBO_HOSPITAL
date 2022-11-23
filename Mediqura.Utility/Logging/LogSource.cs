using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.Utility.Logging
{/****************************************************
   Description of the class	    : ORHelper
   Created Date					: 20-07-2016
   Developer					: Loken
   Modify Date					: 
   Modified By Developer		: 
   Comments						: ()
   ***************************************************/
    public class LogSource
    {
        public string Message = "";
        public EnumLogCategory LogCategory;
        public EnumPriority Priority;
        public EnumLogEvenType EventType;
        public string Title = "";
        public object Sender = null;
        public string ClassName = "";


        public LogSource(object sender, string message, EnumLogCategory logCategory, EnumPriority priority, EnumLogEvenType eventType)
        {
            this.Sender = sender;
            this.Message = message;
            this.LogCategory = logCategory;
            this.Priority = priority;
            this.EventType = eventType;

        }

        public LogSource(string className, string message, EnumLogCategory logCategory, EnumPriority priority, EnumLogEvenType eventType)
        {
            this.ClassName = className;
            this.Message = message;
            this.LogCategory = logCategory;
            this.Priority = priority;
            this.EventType = eventType;

        }

    }
    public enum EnumPriority
    {
        High = 0,
        Low = 1,
    }
    public enum EnumLogCategory
    {
        UIEvents = 0,
        ServiceAgentEvents = 1,
        BusinessProcessEvents = 2,
        DataAccessEvents = 3,
        ServiceEvents = 4,
        Debug = 5,
        Troubleshooting = 6,
    }
    public enum EnumLogEvenType
    {
        Error = 0,
        Warning = 1,
        Information = 2,

    }
}
