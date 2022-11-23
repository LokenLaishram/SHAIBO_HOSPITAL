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
	public class EmgReturnDA
	{
		public List<EmgReturnData> GetadvanceSearchEmergNo(EmgReturnData ObjData)
		{
			List<EmgReturnData> result = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[1];
				arParms[0] = new SqlParameter("@PatientDetails", SqlDbType.VarChar);
				arParms[0].Value = ObjData.PatientDetails;

				SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetadvanceSearchReturnIssueEmergNo", arParms);
				List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
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

		public List<EmgReturnData> GetEmergReturnItem(EmgReturnData ObjData)
		{
			List<EmgReturnData> result = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[2];

				arParms[0] = new SqlParameter("@EmergReturnItemDetails", SqlDbType.VarChar);
				arParms[0].Value = ObjData.EmergReturnItemDetails;

				arParms[1] = new SqlParameter("@EmergNo", SqlDbType.VarChar);
				arParms[1].Value = ObjData.EmergNo;

				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmergReturnItem", arParms);
				List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
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

		public List<EmgReturnData> GetItemIssueByEmergDrgIssueNo(EmgReturnData ObjData)
		{
			List<EmgReturnData> result = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
				arParms[0].Value = ObjData.ID;

				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetItemIssueByEmergDrgIssueNo", arParms);
				List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
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

        public List<EmgReturnData> UpdateEmergReturnDetails(EmgReturnData objdata)
		{
            List<EmgReturnData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[10];

					arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
					arParms[0].Value = objdata.XMLData;

					arParms[1] = new SqlParameter("@HandOver", SqlDbType.Int);
					arParms[1].Value = objdata.HandOver;

					arParms[2] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
					arParms[2].Value = objdata.FinancialYearID;

					arParms[3] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
					arParms[3].Value = objdata.IPaddress;

					arParms[4] = new SqlParameter("@totalreturnQty", SqlDbType.Decimal);
					arParms[4].Value = objdata.totalreturnQty;

					arParms[5] = new SqlParameter("@Remarks", SqlDbType.VarChar);
					arParms[5].Value = objdata.Remarks;

					arParms[6] = new SqlParameter("@EmergNo", SqlDbType.VarChar);
					arParms[6].Value = objdata.EmergNo;

					arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.VarChar);
					arParms[7].Value = objdata.EmployeeID;

                    arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[8].Value = objdata.HospitalID;

					arParms[9] = new SqlParameter("@TotalReturnAmt", SqlDbType.Decimal);
					arParms[9].Value = objdata.TotalReturnAmt;

			        SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_EmergReturnDetails", arParms);
                    List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
                    result = lstresult;
				}
			}
			catch (Exception ex) //Excepon of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new DataAccessException("5000001", ex);
			}
			return result;
		}

		public List<EmgReturnData> GetEmergReturnNo(EmgReturnData ObjData)
		{
			List<EmgReturnData> result = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[1];
				arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
				arParms[0].Value = ObjData.ReturnNo;

				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmergReturnNo", arParms);
				List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
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

		public List<EmgReturnData> GetEmergReturnList1(EmgReturnData objdata)
		{
			List<EmgReturnData> result = null;
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

				arParms[4] = new SqlParameter("@EmergNo", SqlDbType.VarChar);
				arParms[4].Value = objdata.EmergNo;


				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_GetEmergReturnList", arParms);
				List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
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

		public int DeleteEmergReturnItemByReturnNo(EmgReturnData objdata)
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

					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEmergReturnItemByReturnNo", arParms);
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
               
        //-----------------------------------------------Start Tab 1 Emergency Return After Billing-------------------------------------------
        public List<EmgReturnData> GetadvanceSearchAfterBillingEmergNo(EmgReturnData ObjData)
        {
            List<EmgReturnData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@PatientDetails", SqlDbType.VarChar);
                arParms[0].Value = ObjData.PatientDetails;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetadvanceSearchReturnIssueAfterBillingEmergNo", arParms);
                List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
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

        public List<EmgReturnData> UpdateEmergReturnAfterBillingDetails(EmgReturnData objdata)
        {
            List<EmgReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objdata.XMLData;

                    arParms[1] = new SqlParameter("@HandOver", SqlDbType.Int);
                    arParms[1].Value = objdata.HandOver;

                    arParms[2] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[2].Value = objdata.FinancialYearID;

                    arParms[3] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[3].Value = objdata.IPaddress;

                    arParms[4] = new SqlParameter("@totalreturnQty", SqlDbType.Decimal);
                    arParms[4].Value = objdata.totalreturnQty;

                    arParms[5] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[5].Value = objdata.Remarks;

                    arParms[6] = new SqlParameter("@EmergNo", SqlDbType.VarChar);
                    arParms[6].Value = objdata.EmergNo;

                    arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.VarChar);
                    arParms[7].Value = objdata.EmployeeID;

                    arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[8].Value = objdata.HospitalID;

                    arParms[9] = new SqlParameter("@TotalReturnAmt", SqlDbType.Decimal);
                    arParms[9].Value = objdata.TotalReturnAmt;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_EmergReturnAfterBillingDetails", arParms);
                    List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
                    result = lstresult;
                }
            }
            catch (Exception ex) //Excepon of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

        //-----------------------------------------------End Tab 1 Emergency Return After Billing-------------------------------------------

        //-----------------------------------------------Start Tab 2 Emergency Return After Billing-------------------------------------------
        public List<EmgReturnData> GetEmergAfterBilligReturnNo(EmgReturnData ObjData)
        {
            List<EmgReturnData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                arParms[0].Value = ObjData.ReturnNo;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmergAfterBillingReturnNo", arParms);
                List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
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
        public List<EmgReturnData> GetEmergReturnListAfterBill1(EmgReturnData objdata)
        {
            List<EmgReturnData> result = null;
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

                arParms[4] = new SqlParameter("@EmergNo", SqlDbType.VarChar);
                arParms[4].Value = objdata.EmergNo;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_GetEmergReturnListAfterBilling", arParms);
                List<EmgReturnData> lstresult = ORHelper<EmgReturnData>.FromDataReaderToList(sqlReader);
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
        public int DeleteEmergReturnAfterBillingItemByReturnNo(EmgReturnData objdata)
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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEmergReturnAfterBillingItemByReturnNo", arParms);
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

        //-----------------------------------------------End Tab 2 Emergency Return After Billing-------------------------------------------
	}
}
