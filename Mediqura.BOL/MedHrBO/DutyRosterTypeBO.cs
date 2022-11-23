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
	public class DutyRosterTypeBO
	{

		public int UpdateDutyRosterType(DutyRosterTypeData objdutyData)
		{
			int result = 0;
			try
			{
				DutyRosterTypeDA objdutyDA = new DutyRosterTypeDA();
				result = objdutyDA.UpdateDutyRosterType(objdutyData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<DutyRosterTypeData> SearchRosterType(DutyRosterTypeData objdutyData)
		{
			List<DutyRosterTypeData> result = null ;
			try
			{
				DutyRosterTypeDA objdutyDA = new DutyRosterTypeDA();
				result = objdutyDA.SearchRosterType(objdutyData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<DutyRosterTypeData> GetRosterDetailsByID(DutyRosterTypeData objdata)
		{
			throw new NotImplementedException();
		}

		public int DeleteRosterDetailsByID(DutyRosterTypeData objdata)
		{
			throw new NotImplementedException();
		}
	}
}
