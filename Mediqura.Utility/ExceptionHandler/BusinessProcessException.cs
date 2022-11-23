using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.Utility.ExceptionHandler
{
    [Serializable]
    public class BusinessProcessException : ApplicationException
    {
        // Default constructor 
        public BusinessProcessException()
            : base()
        {

        }
        //New 

        // Constructor with message 
        public BusinessProcessException(string message)
            : base(message)
        {
        }


        // Constructor with message, inner Exception 
        public BusinessProcessException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        // Protected constructor to de-serialize data 
        protected BusinessProcessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
