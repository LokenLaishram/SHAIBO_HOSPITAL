using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.AdmissionData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.AdmissionDA;
using Mediqura.CommonData.MedHouseKeepingData;
using Mediqura.DAL.MedHouseKeepingDA;

namespace Mediqura.BOL.MedHouseKeepingBO
{
    public class BedStatusBO
    {
        public List<BedStatusData> GetBedStatusDetails(BedStatusData objdata)
        {
            List<BedStatusData> result = null;
            try
            {
                BedStatusDA objDA = new BedStatusDA();
                result = objDA.GetBedStatusDetails(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateBedStatus(BedStatusData objData)
        {
            int result = 0;
            try
            {
                BedStatusDA objDA = new BedStatusDA();
                result = objDA.UpdateBedStatus(objData);
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
