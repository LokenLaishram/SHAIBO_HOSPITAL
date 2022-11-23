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
    public class MedStoreIndentDA
    {

		public List<MedIndentData> GetIndentnotification()
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = 1;


                    SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "MDQ_PHR_Indent_notification", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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

        public List<MedIndentData> GetItemAvailabilty(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objD.ID;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.MedSubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_Get_ItemAvailibilty", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetMainStockitemAvailabilty(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objD.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_Get_ItemAvailibilty", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetItemNameListInStore(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_ItemName", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetItemNameListInStoreByBatchNo(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_ItemNameByBatch", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetItemNameListInSubStore(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_Substock_ItemName", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetItemNameListInStoreBySubstockID(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.MedSubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_ItemNameBySubtckID", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> SearchDruglistByComposition(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Composition", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.MedSubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_SaercItemNameByComposition", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetreturnItemNameListInStore(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.MedSubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medretun_ItemNamewithSubtockwise", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetItemDetailsByItemID(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@SubStockID", SqlDbType.BigInt);
                    arParms[0].Value = objD.SubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_GetStockAvailability", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetItemDetailsByBatchNo(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.BatchNo;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.VarChar);
                    arParms[1].Value = objD.MedSubStockID;

					arParms[2] = new SqlParameter("@ItemID", SqlDbType.BigInt);
					arParms[2].Value = objD.ItemID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_GetItemDetailByBatch", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> UpdateIndentItemDetails(MedIndentData objstock)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[13];

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

                    arParms[8] = new SqlParameter("@TotRequestQty", SqlDbType.Int);
                    arParms[8].Value = objstock.TotRequestQty;

                    arParms[9] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[9].Value = objstock.IndentRequestID;

                    arParms[10] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[10].Value = objstock.MedSubStockID;

                    arParms[11] = new SqlParameter("@IndentRaiseDate", SqlDbType.DateTime);
                    arParms[11].Value = objstock.IndentRaiseDate;

                    arParms[12] = new SqlParameter("@RequestedBy", SqlDbType.BigInt);
                    arParms[12].Value = objstock.RequestedBy;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_Update_IndentItemDetails", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> UpdateMainShortIteemList(MedIndentData objstock)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[1].Value = objstock.HospitalID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objstock.EmployeeID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objstock.FinancialYearID;

                    arParms[4] = new SqlParameter("@TotRequestQty", SqlDbType.Int);
                    arParms[4].Value = objstock.TotalRqty;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_Update_MainStockShortageItemList", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetIndentItemList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.IndentNo;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objbill.MedSubStockID;

                    arParms[2] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[2].Value = objbill.IndentRequestID;

                    arParms[3] = new SqlParameter("@ReceivedBy", SqlDbType.BigInt);
                    arParms[3].Value = objbill.ReceivedBy;

                    arParms[4] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[4].Value = objbill.DateFrom;

                    arParms[5] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[5].Value = objbill.DateTo;

                    arParms[6] = new SqlParameter("@IndentStatusID", SqlDbType.Int);
                    arParms[6].Value = objbill.IndentStatusID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_GetIndentItemList", arParms);
                    List<MedIndentData> listpatientdetails = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetSubstockItemRequestableList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[0].Value = objbill.ItemID;

                    arParms[1] = new SqlParameter("@SubstockID", SqlDbType.Int);
                    arParms[1].Value = objbill.MedSubStockID;

                    arParms[2] = new SqlParameter("@PC", SqlDbType.Money);
                    arParms[2].Value = objbill.PC;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_Med_Get_Subtock_ItemRequirmentList", arParms);
                    List<MedIndentData> listpatientdetails = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GeMainStockSortItemList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[0].Value = objbill.ItemID;

                    arParms[1] = new SqlParameter("@PC", SqlDbType.Money);
                    arParms[1].Value = objbill.PC;

                    arParms[2] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    arParms[2].Value = objbill.SupplierID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_Med_Get_Main_Stock_Short_Item_List", arParms);
                    List<MedIndentData> listpatientdetails = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public int DeleteIndentReqByID(MedIndentData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];


                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[1].Value = objbill.Remarks;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[3] = new SqlParameter("@IndentID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.IndentID;

                    arParms[4] = new SqlParameter("@IndentNo", SqlDbType.NVarChar);
                    arParms[4].Value = objbill.IndentNo;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_DeleteIndRequestListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[0].Value);
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
        public List<MedIndentData> CheckStockitemavailable(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[0].Value = objD.ItemID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_MedCheckStockItemAvailablity", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetAutoStockAvailability(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[0].Value = objD.ItemID;

                    arParms[1] = new SqlParameter("@StockNo", SqlDbType.VarChar);
                    arParms[1].Value = objD.StockNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_MedAutoStockAvailability", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetIndentList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[0].Value = objbill.MedSubStockID;

                    arParms[1] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[1].Value = objbill.IndentRequestID;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IndentStatus", SqlDbType.Int);
                    arParms[4].Value = objbill.IndentStateID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_MedGetIndentReqestList", arParms);
                    List<MedIndentData> listpatientdetails = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetIndentDetailsByIndentNo(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IndentNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_GetIndentReqDetail", arParms);
                    List<MedIndentData> listpatientdetails = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetPreapproveditemlistbyindentnumber(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[0].Value = objD.ItemID;

                    arParms[1] = new SqlParameter("@IndentNumber", SqlDbType.VarChar);
                    arParms[1].Value = objD.IndentNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_MedPreApproveItemListByIndentNumber", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetItemDetailsByStockNumbers(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@StockNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.StockNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_MedItemDetailsByStockNumber", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> UpdateMedIndentApprovedQtyDetail(MedIndentData objstock)
        {
            List<MedIndentData> result = null;
            try
            {

                SqlParameter[] arParms = new SqlParameter[7];

                arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                arParms[0].Value = objstock.ItemID;

                arParms[1] = new SqlParameter("@StockNo", SqlDbType.VarChar);
                arParms[1].Value = objstock.StockNo;

                arParms[2] = new SqlParameter("@IndentNumber", SqlDbType.VarChar);
                arParms[2].Value = objstock.IndentNo;

                arParms[3] = new SqlParameter("@ApprovedQty", SqlDbType.Int);
                arParms[3].Value = objstock.ApprovedQty;

                arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[4].Value = objstock.EmployeeID;

                arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[5].Value = objstock.FinancialYearID;

                arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                arParms[6].Value = objstock.HospitalID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Med_IndentApprvQtyDetails", arParms);
                List<MedIndentData> ItemList = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
                result = ItemList;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public int DeletepreapprovedItem(MedIndentData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objstock.SubStockID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objstock.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Delete_MedNoneapprovedSubtocks", arParms);
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
        public int UpdateMedIndentDetail(MedIndentData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@TotApproved", SqlDbType.Int);
                    arParms[2].Value = objstock.TotApproved;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objstock.EmployeeID;

                    arParms[4] = new SqlParameter("@ApprvBy", SqlDbType.BigInt);
                    arParms[4].Value = objstock.ApprvBy;

                    arParms[5] = new SqlParameter("@HandOverTo", SqlDbType.BigInt);
                    arParms[5].Value = objstock.HandOverTo;

                    arParms[6] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[6].Value = objstock.IndentNo;

                    arParms[7] = new SqlParameter("@IndentStatus", SqlDbType.Int);
                    arParms[7].Value = objstock.IndentStateID;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objstock.FinancialYearID;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = objstock.HospitalID;

                    arParms[10] = new SqlParameter("@MedSubtockID", SqlDbType.Int);
                    arParms[10].Value = objstock.MedSubStockID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Med_IndentApprvDetails", arParms);
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
        public List<MedIndentData> GetMedSubstockDetails(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[0].Value = objbill.MedSubStockID;

                    arParms[1] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[1].Value = objbill.ItemID;

                    arParms[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                    arParms[2].Value = objbill.StatusID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_MedGetSubStockStatus", arParms);
                    List<MedIndentData> listpatientdetails = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> UpdateStockReturnDetails(MedIndentData objstock)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[1].Value = objstock.HospitalID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objstock.EmployeeID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objstock.FinancialYearID;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objstock.IPaddress;

                    arParms[5] = new SqlParameter("@TotReturnQty", SqlDbType.Decimal);
                    arParms[5].Value = objstock.TotReturnQty;

                    arParms[6] = new SqlParameter("@ReturnBy", SqlDbType.BigInt);
                    arParms[6].Value = objstock.ReturnBy;

                    arParms[7] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[7].Value = objstock.MedSubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_Update_StockReturnDetails", arParms);
                    List<MedIndentData> lstresult = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public List<MedIndentData> GetReturnItemList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReturnNo;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objbill.MedSubStockID;

                    arParms[2] = new SqlParameter("@ReturnBy", SqlDbType.BigInt);
                    arParms[2].Value = objbill.ReturnBy;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp__MDQ_Med_GetStockReturnList", arParms);
                    List<MedIndentData> listpatientdetails = ORHelper<MedIndentData>.FromDataReaderToList(sqlReader);
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
        public int DeleteStockReurnByID(MedIndentData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[1].Value = objbill.Remarks;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[3] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[3].Value = objbill.ReturnNo;

                    arParms[4] = new SqlParameter("@ReturnQty", SqlDbType.Int);
                    arParms[4].Value = objbill.ReturnQty;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_DeleteStockReturnListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[0].Value);
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
