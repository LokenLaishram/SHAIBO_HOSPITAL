using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedBillData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.DAL.MedBillDA
{
    public class TestWiseDA
    {
        public List<TestWiseCollectionData> GetServiceName(TestWiseCollectionData objservice)
        {
            List<TestWiseCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Test_Services", arParms);
                    List<TestWiseCollectionData> lstServiceDetails = ORHelper<TestWiseCollectionData>.FromDataReaderToList(sqlReader);
                    result = lstServiceDetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<TestWiseCollectionData> GetNestedData(TestWiseCollectionData objpat)
        {
            List<TestWiseCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objpat.ServiceName;

                    //arParms[0] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    //arParms[0].Value = objpat.ServiceID;

                    SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetTestWiseNesteGrid", arParms);
                   // sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetTestWiseNesteGridNEW", arParms);
                    List<TestWiseCollectionData> listpatientdetails = ORHelper<TestWiseCollectionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<TestWiseCollectionData> GetServicesList(TestWiseCollectionData objbill)
        {
            List<TestWiseCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ServiceName;


                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@docID", SqlDbType.Int);
                    arParms[3].Value = objbill.docID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetTestWiseGrid", arParms);
                    List<TestWiseCollectionData> listpatientdetails = ORHelper<TestWiseCollectionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
    }
}
