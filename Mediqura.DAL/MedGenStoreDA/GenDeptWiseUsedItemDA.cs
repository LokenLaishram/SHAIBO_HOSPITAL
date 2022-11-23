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

namespace Mediqura.DAL.MedGenStoreDA
{
    public class GenDeptWiseUsedItemDA
    {
        public List<GenDeptWiseUsedItemData> GetItemNameListInStore(GenDeptWiseUsedItemData objD)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@GenStockID", SqlDbType.BigInt);
                    arParms[1].Value = objD.GenStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Get_DepartmentwiseItemList", arParms);
                    List<GenDeptWiseUsedItemData> lstresult = ORHelper<GenDeptWiseUsedItemData>.FromDataReaderToList(sqlReader);
                    result = lstresult;
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
        public List<GenDeptWiseUsedItemData> GetRecordNo(GenDeptWiseUsedItemData objD)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@RecordNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.RecordNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Get_RecordNo", arParms);
                    List<GenDeptWiseUsedItemData> lstresult = ORHelper<GenDeptWiseUsedItemData>.FromDataReaderToList(sqlReader);
                    result = lstresult;
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
        public List<GenDeptWiseUsedItemData> GetItemDetailsByItemID(GenDeptWiseUsedItemData objD)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {

                SqlParameter[] arParms = new SqlParameter[1];

                arParms[0] = new SqlParameter("@SubTockID", SqlDbType.BigInt);
                arParms[0].Value = objD.SubStockID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_GetDeptStockAvailability", arParms);
                List<GenDeptWiseUsedItemData> lstresult = ORHelper<GenDeptWiseUsedItemData>.FromDataReaderToList(sqlReader);
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
        public int UpdateAvailableBalAfterUsed(GenDeptWiseUsedItemData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[0].Value = objstock.ItemID;

                    arParms[1] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[1].Value = objstock.IndentNo;

                    arParms[2] = new SqlParameter("@QtyUsed", SqlDbType.Int);
                    arParms[2].Value = objstock.QtyUsed;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Update_AvailableBalAfterUsed", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[3].Value);
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
        public int UpdateAvailableBalAfterdeleting(GenDeptWiseUsedItemData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[0].Value = objstock.ItemID;

                    arParms[1] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[1].Value = objstock.IndentNo;

                    arParms[2] = new SqlParameter("@QtyUsed", SqlDbType.Int);
                    arParms[2].Value = objstock.QtyUsed;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Update_AvailableBalAfterDeleting", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[3].Value);
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
        public List<GenDeptWiseUsedItemData> UpdateDepartmentWiseItemUsedRecordDetails(GenDeptWiseUsedItemData objstock)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[1].Value = objstock.HospitalID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objstock.EmployeeID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[4].Value = objstock.ActionType;

                    arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[5].Value = objstock.FinancialYearID;

                    arParms[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[6].Value = objstock.IsActive;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objstock.IPaddress;

                    arParms[8] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[8].Value = objstock.GenStockID;

                    arParms[9] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[9].Value = objstock.IndentNo;

                    arParms[10] = new SqlParameter("@TotalUsedQty", SqlDbType.Int);
                    arParms[10].Value = objstock.TotalUsedQty;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Update_DeptWiseItemUsedDetails", arParms);
                    List<GenDeptWiseUsedItemData> list = ORHelper<GenDeptWiseUsedItemData>.FromDataReaderToList(sqlReader);
                    result = list;

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
        public List<GenDeptWiseUsedItemData> GetDeptWiseItemList(GenDeptWiseUsedItemData objbill)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@RecordNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.RecordNo;

                    arParms[1] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[1].Value = objbill.GenStockID;

                    arParms[2] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[2].Value = objbill.ItemID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[5].Value = objbill.PatientName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_GetDeptItemUsedRecordList", arParms);
                    List<GenDeptWiseUsedItemData> listpatientdetails = ORHelper<GenDeptWiseUsedItemData>.FromDataReaderToList(sqlReader);
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
        public int DeleteGenDeptWiseUsedItemDetailsByRecNo(GenDeptWiseUsedItemData objdata)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@RecordNo", SqlDbType.VarChar);
                    arParms[0].Value = objdata.RecordNo;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objdata.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objdata.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objdata.IPaddress;

                    arParms[5] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[5].Value = objdata.ID;

                    arParms[6] = new SqlParameter("@QtyUsed", SqlDbType.Int);
                    arParms[6].Value = objdata.QtyUsed;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_util_DeleteGenDeptWiseUsedItemDetailsByRecNo", arParms);
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

    }
}
