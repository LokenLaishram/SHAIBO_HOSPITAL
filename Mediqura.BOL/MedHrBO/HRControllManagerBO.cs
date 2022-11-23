using Mediqura.CommonData.MedHrData;
using Mediqura.DAL.MedHrDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedHrBO
{
	public class HRControllManagerBO
	{
		public List<HRControllManagerData> GetHRControlManagerList(HRControllManagerData objcontrols)
		{
			List<HRControllManagerData> controlist = null;
			try
			{
				HRControllManagerDA objcontrolDA = new HRControllManagerDA();
				List<HRControllManagerData> controls = objcontrolDA.GetHRControlManagerList(objcontrols);
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

		public int UpdateHRControlManagerList(HRControllManagerData objcontrols)
		{
			int result = 0;
			try
			{
				HRControllManagerDA objcontrolDA = new HRControllManagerDA();
				result = objcontrolDA.UpdateHRControlManagerList(objcontrols);
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
