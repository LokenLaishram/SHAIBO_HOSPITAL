using Mediqura.CommonData.MedStore;
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

namespace Mediqura.DAL.MedStore
{
    public class IPReturnDA
    {
        public List<IPReturnData> GetadvanceSearchIPNo(IPReturnData ObjData)
        {
            List<IPReturnData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@PatientDetails", SqlDbType.VarChar);
                arParms[0].Value = ObjData.PatientDetails;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetadvanceSearchReturnIssueIPNo", arParms);
                List<IPReturnData> lstresult = ORHelper<IPReturnData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

		public List<IPReturnData> GetadvanceSearchAfterBillingIPNo(IPReturnData ObjData)
		{
			List<IPReturnData> result = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[1];
				arParms[0] = new SqlParameter("@PatientDetails", SqlDbType.VarChar);
				arParms[0].Value = ObjData.PatientDetails;

				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetadvanceSearchReturnIPAfterBilling", arParms);
				List<IPReturnData> lstresult = ORHelper<IPReturnData>.FromDataReaderToList(sqlReader);
				result = lstresult;
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new DataAccessException("5000001", ex);
			}
			return result;
		}

        public List<IPReturnData> GetIPReturnItem(IPReturnData ObjData)
        {
            List<IPReturnData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];
                arParms[0] = new SqlParameter("@IPReturnItemDetails", SqlDbType.VarChar);
                arParms[0].Value = ObjData.IPReturnItemDetails;

                arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                arParms[1].Value = ObjData.IPNo;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPReturnItem", arParms);
                List<IPReturnData> lstresult = ORHelper<IPReturnData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

        public List<IPReturnData> GetItemIssueByIPDrgIssueNo(IPReturnData ObjData)
        {
            List<IPReturnData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
				arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
				arParms[0].Value = ObjData.ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetItemIssueByIPDrgIssueNo", arParms);
                List<IPReturnData> lstresult = ORHelper<IPReturnData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

        public List<IPReturnData> UpdateIPReturnDetails(IPReturnData objdata)
        {
            List<IPReturnData> result = null;
            try
            {

                SqlParameter[] arParms = new SqlParameter[10];

                arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                arParms[0].Value = objdata.XMLData;

                arParms[1] = new SqlParameter("@HandOver", SqlDbType.Int);
                arParms[1].Value = objdata.HandOver;

                arParms[2] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[2].Value = objdata.FinancialYearID;

                arParms[3] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                arParms[3].Value = objdata.IPaddress;

                arParms[4] = new SqlParameter("@totalreturnQty", SqlDbType.Decimal);
                arParms[4].Value = objdata.totalreturnQty;

                arParms[5] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                arParms[5].Value = objdata.Remarks;

                arParms[6] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                arParms[6].Value = objdata.IPNo;

                arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.VarChar);
                arParms[7].Value = objdata.EmployeeID;

                arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Int);
                arParms[8].Value = objdata.HospitalID;

				arParms[9] = new SqlParameter("@TotalReturnAmt", SqlDbType.Decimal);
				arParms[9].Value = objdata.TotalReturnAmt;
				

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IPReturnDetails", arParms);
                List<IPReturnData> lstresult = ORHelper<IPReturnData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

		public List<IPReturnData> UpdateIPReturnAfterBillingDetails(IPReturnData objdata)
        {
            List<IPReturnData> result = null;
            try
            {

                SqlParameter[] arParms = new SqlParameter[10];

                arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                arParms[0].Value = objdata.XMLData;

                arParms[1] = new SqlParameter("@HandOver", SqlDbType.Int);
                arParms[1].Value = objdata.HandOver;

                arParms[2] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[2].Value = objdata.FinancialYearID;

                arParms[3] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                arParms[3].Value = objdata.IPaddress;

                arParms[4] = new SqlParameter("@totalreturnQty", SqlDbType.Decimal);
                arParms[4].Value = objdata.totalreturnQty;

                arParms[5] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                arParms[5].Value = objdata.Remarks;

                arParms[6] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                arParms[6].Value = objdata.IPNo;

                arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.VarChar);
                arParms[7].Value = objdata.EmployeeID;

                arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Int);
                arParms[8].Value = objdata.HospitalID;

				arParms[9] = new SqlParameter("@TotalReturnAmt", SqlDbType.Decimal);
				arParms[9].Value = objdata.TotalReturnAmt;
				

                SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IPReturnAfterBillingDetails", arParms);
                List<IPReturnData> lstresult = ORHelper<IPReturnData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

        public List<IPReturnData> GetIPReturnNo(IPReturnData ObjData)
        {
            List<IPReturnData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                arParms[0].Value = ObjData.ReturnNo;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPReturnNo", arParms);
                List<IPReturnData> lstresult = ORHelper<IPReturnData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

        public List<IPReturnData> GetiPReturnList1(IPReturnData objdata)
        {
            List<IPReturnData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[5];
                arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                arParms[0].Value = objdata.ReturnNo;

                arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                arParms[1].Value = objdata.DateFrom;

                arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                arParms[2].Value = objdata.DateTo;

                arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                arParms[3].Value = objdata.IsActive;

				arParms[4] = new SqlParameter("@IPNo", SqlDbType.VarChar);
				arParms[4].Value = objdata.IPNo;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_GetiPReturnList", arParms);
                List<IPReturnData> lstresult = ORHelper<IPReturnData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }


		public List<IPReturnData> GetiPReturnListAfterBill1(IPReturnData objdata)
        {
            List<IPReturnData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[5];
                arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                arParms[0].Value = objdata.ReturnNo;

                arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                arParms[1].Value = objdata.DateFrom;

                arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                arParms[2].Value = objdata.DateTo;

                arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                arParms[3].Value = objdata.IsActive;

				arParms[4] = new SqlParameter("@IPNo", SqlDbType.VarChar);
				arParms[4].Value = objdata.IPNo;

                SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_GetiPReturnListAfterBilling", arParms);
                List<IPReturnData> lstresult = ORHelper<IPReturnData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

        public int DeleteIPReturnItemByReturnNo(IPReturnData objdata)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[0].Value = objdata.ReturnNo;

                    arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[1].Value = objdata.Remarks;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objdata.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteIPReturnItemByReturnNo", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[2].Value);
                    }
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

		public int CheckdischargestatusByIPNO(IPReturnData ObjData)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[2];

					arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
					arParms[0].Value = ObjData.IPNo;

					arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[1].Direction = ParameterDirection.Output;

					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_CheckdischargestatusByIPNO", arParms);
					if (result_ > 0 || result_ == -1)
					{
						result = Convert.ToInt32(arParms[1].Value);
					}
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
