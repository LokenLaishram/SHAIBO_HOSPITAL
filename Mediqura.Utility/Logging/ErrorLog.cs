using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.Utility.Logging
{
    /****************************************************
 Description of the class	    : ORHelper
 Created Date					: 20-07-2016
 Developer						: Loken
 Modify Date					: 
 Modified By Developer			: 
 Comments						: ()
 ***************************************************/
    public class ErorrLog
    {
        public int ErrorLineNumber
        {
            get;
            set;
        }

        public int ErrorSeverityNumber
        {
            get;
            set;
        }

        public int ErrorStateStatus
        {
            get;
            set;
        }

        public string ErrorMessageDetails
        {
            get;
            set;
        }

        public string ProcedureName
        {
            get;
            set;
        }

        public EnumErrorLogSourceTier LogSourceTier
        {
            get;
            set;
        }

        public ErorrLog(EnumErrorLogSourceTier LogSourceTier, int ErrorLineNumber, int ErrorSeverityNumber, int ErrorStateStatus, string ProcedureName, string ErrorMessageDetails)
        {
            this.ErrorLineNumber = ErrorLineNumber;
            this.ErrorSeverityNumber = ErrorSeverityNumber;
            this.ErrorStateStatus = ErrorStateStatus;
            this.ErrorMessageDetails = ErrorMessageDetails;
            this.ProcedureName = ProcedureName;
            this.LogSourceTier = LogSourceTier;
        }

        public ErorrLog(EnumErrorLogSourceTier LogSourceTier, string ErrorMessageDetails)
        {
            this.ErrorMessageDetails = ErrorMessageDetails;
            this.LogSourceTier = LogSourceTier;
        }
    }
    public enum EnumErrorLogSourceTier
    {
        Web = 1,
        BO = 2,
        DA = 3,
        DB = 4
    }
}
