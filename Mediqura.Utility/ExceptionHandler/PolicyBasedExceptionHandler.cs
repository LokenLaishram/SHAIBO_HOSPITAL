using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Mediqura.Utility.ExceptionHandler
{
    public class PolicyBasedExceptionHandler
    {

        public enum PolicyName
        {
            BusinessProcessExceptionPolicy = 1,
            DataAccessExceptionPolicy = 2,
            UIExceptionPolicy = 3,
            ReportsExceptionPolicy = 4,
            ServiceAgentExceptionPolicy = 5,
            ServiceExceptionPolicy = 6
        }
        // <summary> 
        // This method accepts Policyname and Exception and then according to policy defined in configuration file 
        // </summary> 
        // <param name="policyName">enum PolicyName </param> 
        // <param name="ex">Exception</param> 
        public static void HandleException(PolicyName objPolicyName, System.Exception ex, string strResourceId)
        {
            string strMessage = "ResourceId : " + strResourceId + " : " + ex.Message; 
            Exception objEx = new Exception(strMessage, ex);

            switch (objPolicyName)
            {
                case PolicyName.BusinessProcessExceptionPolicy:
                    ExceptionPolicy.HandleException(objEx, "BusinessProcessExceptionPolicy");
                    break;
                case PolicyName.DataAccessExceptionPolicy:
                    ExceptionPolicy.HandleException(objEx, "DataAccessExceptionPolicy");
                    break;
                case PolicyName.UIExceptionPolicy:
                    ExceptionPolicy.HandleException(objEx, "UIExceptionPolicy");
                    break;
                case PolicyName.ReportsExceptionPolicy:
                    ExceptionPolicy.HandleException(objEx, "ReportsExceptionPolicy");
                    break;
            }

        }

    }
}
