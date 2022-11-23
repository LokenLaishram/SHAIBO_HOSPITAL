using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.Utility.ExceptionHandler
{
    [Serializable]
    public class DataAccessException : ApplicationException
    {
        // Default constructor 
        public DataAccessException()
            : base()
        {

        }
        //New 

        // Constructor with message 
        public DataAccessException(string message)
            : base(message)
        {
        }

        // Constructor with message, inner Exception 
        public DataAccessException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        // Protected constructor to de-serialize data 
        protected DataAccessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
