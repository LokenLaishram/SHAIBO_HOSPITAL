using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedHrData;
using Mediqura.DAL.MedHrDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
namespace Mediqura.BOL.MedHrBO
{
	public class DutyRosterBO
	{
		public List<DutyRosterData> SearchDutyRoster(DutyRosterData objdutyData)
		{
			
				List<DutyRosterData> result = null ;
			try
			{
				DutyRosterDA objdutyDA = new DutyRosterDA();
				result = objdutyDA.SearchDutyRoster(objdutyData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public int UpdateDutyRoster(DutyRosterData objdutyData)
		{
			int  result = 0;
			try
			{
				DutyRosterDA objdutyDA = new DutyRosterDA();
				result = objdutyDA.UpdateDutyRoster(objdutyData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public int UpdateEmployeesDutySchedule(DutyRosterData objdutyData)
		{
			int result = 0;
			try
			{
				DutyRosterDA objdutyDA = new DutyRosterDA();
				result = objdutyDA.UpdateEmployeesDutySchedule(objdutyData);
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
