using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedUtilityBO
{
	public class SeasonTypeBO
	{
		public int UpdateSeasonDetails(SeasonTypeData objseason)
		{
			int result = 0;
			try
			{
				SeasonTypeDA objseasonDA = new SeasonTypeDA();
				result = objseasonDA.UpdateSeasonDetails(objseason);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}
		public List<SeasonTypeData> SearchSeasonDetails(SeasonTypeData objseason)
		{
			List<SeasonTypeData> result = null;
			try
			{
				SeasonTypeDA objseasonDA = new SeasonTypeDA();
				result = objseasonDA.SearchSeasonDetails(objseason);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}
		public List<SeasonTypeData> GetseasonDetailsByID(SeasonTypeData objseason)
		{
			List<SeasonTypeData> result = null;

			try
			{
				SeasonTypeDA objseasonDA = new SeasonTypeDA();
				result = objseasonDA.GetSeasonDetailsByID(objseason);

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
