using Mediqura.CommonData.Common;
using Mediqura.DAL.CommonDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.CommonBO
{
  public  class PageControlBO
    {
      public List<PageControlsData> GetControlList(PageControlsData objcontrols)
      {
          List<PageControlsData> controlist = null;
          try
          {
              PageControlDA objcontrolDA = new PageControlDA();
              List<PageControlsData> controls = objcontrolDA.GetControlList(objcontrols);
              controlist = controls;
          }
          catch (Exception ex) //Exception of the business layer(itself)//unhandle
          {
              PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
              LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
              throw new BusinessProcessException("4000001", ex);
          }
          return controlist;
      }
      public List<PageControlsData> GetControlEnabledetails(PageControlsData objcontrols)
      {
          List<PageControlsData> controlist = null;
          try
          {
              PageControlDA objcontrolDA = new PageControlDA();
              List<PageControlsData> controls = objcontrolDA.GetControlEnabledetails(objcontrols);
              controlist = controls;
          }
          catch (Exception ex) //Exception of the business layer(itself)//unhandle
          {
              PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
              LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
              throw new BusinessProcessException("4000001", ex);
          }
          return controlist;
      }
      public List<PageControlsData> GetPageurls(PageControlsData objcontrols)
      {
          List<PageControlsData> controlist = null;
          try
          {
              PageControlDA objcontrolDA = new PageControlDA();
              List<PageControlsData> controls = objcontrolDA.GetPageurls(objcontrols);
              controlist = controls;
          }
          catch (Exception ex) //Exception of the business layer(itself)//unhandle
          {
              PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
              LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
              throw new BusinessProcessException("4000001", ex);
          }
          return controlist;
      }
      public int Updatepagecontrols(PageControlsData objcontrols)
      {
          int result = 0;
          try
          {
              PageControlDA objcontrolDA = new PageControlDA();
              result = objcontrolDA.Updatepagecontrols(objcontrols);
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
