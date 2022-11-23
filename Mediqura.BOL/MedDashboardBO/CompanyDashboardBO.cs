using Mediqura.CommonData.MedDashboardData;
using Mediqura.DAL.MedDashboardDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedDashboardBO
{
   public class CompanyDashboardBO
    {
       public List<CompanyDashboardData> LoadDashboard()
       {
           List<CompanyDashboardData> result = null;
           try
           {
               CompanyDashboardDA objDA = new CompanyDashboardDA();
               result = objDA.LoadDashboard();
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;
       }
    }
}
