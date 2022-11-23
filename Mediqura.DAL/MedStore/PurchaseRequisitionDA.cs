using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;

namespace Mediqura.DAL.MedStore
{
    public class PurchaseRequisitionDA
    {

        public List<PurchaseRequisitionData> GetItemNameAuto(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                arParms[0].Value = ObjPurReqData.ItemName;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetItemNameAuto", arParms);
                List<PurchaseRequisitionData> lstresult = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
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
        public List<PurchaseRequisitionData> GetUnitDescriptionByID(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[0].Value = ObjPurReqData.ItemID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetUnitDescriptionByID", arParms);
                    List<PurchaseRequisitionData> lstresult = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
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
        public List<PurchaseRequisitionData> SavePurchaseRequisitionList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[7];

                arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                arParms[0].Value = ObjPurReqData.XMLData; ;

                arParms[1] = new SqlParameter("@PurchaseRequisitionTypeID", SqlDbType.Int);
                arParms[1].Value = ObjPurReqData.PurchaseRequisitionTypeID;

                arParms[2] = new SqlParameter("@PurchaseRequisitionTypeName", SqlDbType.VarChar);
                arParms[2].Value = ObjPurReqData.PurchaseRequisitionTypeName;

                arParms[3] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                arParms[3].Value = ObjPurReqData.AddedBy;

                arParms[4] = new SqlParameter("@ActionType", SqlDbType.Int);
                arParms[4].Value = ObjPurReqData.ActionType;

                arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                arParms[5].Value = ObjPurReqData.HospitalID;

                arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[6].Value = ObjPurReqData.FinancialYearID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_SavePurchaseRequisitionList", arParms);
                List<PurchaseRequisitionData> RequisitionList = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                result = RequisitionList;

            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<PurchaseRequisitionData> UpdatePurchaseRequisitionList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[8];

                arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                arParms[0].Value = ObjPurReqData.XMLData; ;

                arParms[1] = new SqlParameter("@PurchaseRequisitionTypeID", SqlDbType.Int);
                arParms[1].Value = ObjPurReqData.PurchaseRequisitionTypeID;

                arParms[2] = new SqlParameter("@PurchaseRequisitionTypeName", SqlDbType.VarChar);
                arParms[2].Value = ObjPurReqData.PurchaseRequisitionTypeName;

                arParms[3] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                arParms[3].Value = ObjPurReqData.AddedBy;

                arParms[4] = new SqlParameter("@ActionType", SqlDbType.Int);
                arParms[4].Value = ObjPurReqData.ActionType;

                arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                arParms[5].Value = ObjPurReqData.HospitalID;

                arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[6].Value = ObjPurReqData.FinancialYearID;

                arParms[7] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                arParms[7].Value = ObjPurReqData.RQNumber;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_UpdatePurchaseRequisitionList", arParms);
                List<PurchaseRequisitionData> RequisitionList = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                result = RequisitionList;

            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        //------------------------------------- End Tab 1 ---------------------------------------
        //------------------------------------- Start Tab 2 ---------------------------------------

        public List<PurchaseRequisitionData> GetRQNumberAuto(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                arParms[0].Value = ObjPurReqData.RQNumber;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetRQNumberAuto", arParms);
                List<PurchaseRequisitionData> lstresult = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
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
        public List<PurchaseRequisitionData> GetPurchaseRequisitionList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@PurchaseRequisitionTypeID", SqlDbType.Int);
                    arParms[0].Value = ObjPurReqData.PurchaseRequisitionTypeID;

                    arParms[1] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[1].Value = ObjPurReqData.RQNumber;

                    arParms[2] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[2].Value = ObjPurReqData.ItemID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = ObjPurReqData.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = ObjPurReqData.DateTo;

                    arParms[5] = new SqlParameter("@RQStatusID", SqlDbType.Int);
                    arParms[5].Value = ObjPurReqData.RQStatusID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetPurchaseRequisitionList", arParms);
                    List<PurchaseRequisitionData> PurchaseRequisitionList = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                    result = PurchaseRequisitionList;
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
        public int DeletePurchaseRequisitionByID(PurchaseRequisitionData ObjPurReqData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[0].Value = ObjPurReqData.RQNumber;

                    arParms[1] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[1].Value = ObjPurReqData.ItemID;

                    arParms[2] = new SqlParameter("@Remark", SqlDbType.VarChar);
                    arParms[2].Value = ObjPurReqData.Remark;

                    arParms[3] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                    arParms[3].Value = ObjPurReqData.AddedBy;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[4].Value = ObjPurReqData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = ObjPurReqData.HospitalID;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = ObjPurReqData.IPaddress;

                    arParms[7] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[7].Value = ObjPurReqData.FinancialYearID;

                    arParms[8] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[8].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_DeletePurchaseRequisitionByID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[8].Value);
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

        //------------------------------------- End Purchase Requisition Page Tab 2 ---------------------------------------

        //------------------------------------- Start Purchase Approval Page Tab 1 ---------------------------------------
        public List<PurchaseRequisitionData> GetPurchaseRequisitionListForApproval(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@PurchaseRequisitionTypeID", SqlDbType.Int);
                    arParms[0].Value = ObjPurReqData.PurchaseRequisitionTypeID;

                    arParms[1] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[1].Value = ObjPurReqData.RQNumber;

                    arParms[2] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[2].Value = ObjPurReqData.ItemName;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = ObjPurReqData.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = ObjPurReqData.DateTo;

                    arParms[5] = new SqlParameter("@RQStatusID", SqlDbType.Int);
                    arParms[5].Value = ObjPurReqData.RQStatusID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetPurchaseRequisitionListForApproval", arParms);
                    List<PurchaseRequisitionData> PurchaseRequisitionList = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                    result = PurchaseRequisitionList;
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

        public List<PurchaseRequisitionData> GetPurReqListByRQNumber(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[0].Value = ObjPurReqData.RQNumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetPurReqListByRQNumber", arParms);
                    List<PurchaseRequisitionData> PurchaseRequisitionList = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                    result = PurchaseRequisitionList;
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
        //------------------------------------- End Purchase Approval Page Tab 1 ---------------------------------------
        //------------------------------------- Start Purchase Approval Page Tab 2 ---------------------------------------
        public int UpdatePurchaseApproveList(PurchaseRequisitionData ObjPurReqData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = ObjPurReqData.XMLData;

                    arParms[1] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[1].Value = ObjPurReqData.RQNumber;

                    arParms[2] = new SqlParameter("@RQStatusID", SqlDbType.Int);
                    arParms[2].Value = ObjPurReqData.RQStatusID;

                    arParms[3] = new SqlParameter("@RQStatusName", SqlDbType.VarChar);
                    arParms[3].Value = ObjPurReqData.RQStatusName;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[4].Value = ObjPurReqData.EmployeeID;

                    arParms[5] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                    arParms[5].Value = ObjPurReqData.AddedBy;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = ObjPurReqData.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = ObjPurReqData.IPaddress;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = ObjPurReqData.FinancialYearID;

                    arParms[9] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[9].Value = ObjPurReqData.ActionType;

                    arParms[10] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[10].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_UpdatePurchaseApproveList", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[10].Value);
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

        //------------------------------------- End Purchase Approval Page Tab 2 ---------------------------------------
        //------------------------------------- Start Purchase Order Page Tab 1 ---------------------------------------

        public List<PurchaseRequisitionData> GetPONumberAuto(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                arParms[0].Value = ObjPurReqData.PONumber;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetPONumberAuto", arParms);
                List<PurchaseRequisitionData> lstresult = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
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
        public List<PurchaseRequisitionData> GetPurchaseRequisitionListForPO(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[0].Value = ObjPurReqData.RQNumber;

                    arParms[1] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                    arParms[1].Value = ObjPurReqData.PONumber;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = ObjPurReqData.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = ObjPurReqData.DateTo;

                    arParms[4] = new SqlParameter("@POStatusID", SqlDbType.Int);
                    arParms[4].Value = ObjPurReqData.POStatusID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetPurchaseRequisitionListForPO", arParms);
                    List<PurchaseRequisitionData> PurchaseRequisitionList = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                    result = PurchaseRequisitionList;
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
        public List<PurchaseRequisitionData> GetPurReqListByRQNumberForPO(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[0].Value = ObjPurReqData.RQNumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetPurReqListByRQNumberForPO", arParms);
                    List<PurchaseRequisitionData> PurchaseRequisitionList = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                    result = PurchaseRequisitionList;
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
        //------------------------------------- End Purchase Order Page Tab 1 ---------------------------------------
        //------------------------------------- Start Purchase Order Page Tab 2 ---------------------------------------
        public List<PurchaseRequisitionData> GeneratePurchaseOrderList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = ObjPurReqData.XMLData;

                    arParms[1] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[1].Value = ObjPurReqData.RQNumber;

                    arParms[2] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    arParms[2].Value = ObjPurReqData.SupplierID;

                    arParms[3] = new SqlParameter("@SupplierName", SqlDbType.VarChar);
                    arParms[3].Value = ObjPurReqData.SupplierName;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[4].Value = ObjPurReqData.EmployeeID;

                    arParms[5] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                    arParms[5].Value = ObjPurReqData.AddedBy;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = ObjPurReqData.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = ObjPurReqData.IPaddress;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = ObjPurReqData.FinancialYearID;

                    arParms[9] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[9].Value = ObjPurReqData.ActionType;

                    arParms[10] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[10].Direction = ParameterDirection.Output;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GeneratePurchaseOrderList", arParms);
                    List<PurchaseRequisitionData> PurchaseOrder = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                    result = PurchaseOrder;
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

        //------------------------------------- End Purchase Order Page Tab 2 ---------------------------------------
        //------------------------------------- Start Purchase Cross Checking Page Tab 1 ---------------------------------------
        public List<PurchaseRequisitionData> GetPurchaseOrderList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[0].Value = ObjPurReqData.RQNumber;

                    arParms[1] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                    arParms[1].Value = ObjPurReqData.PONumber;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = ObjPurReqData.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = ObjPurReqData.DateTo;

                    arParms[4] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    arParms[4].Value = ObjPurReqData.SupplierID;

                    arParms[5] = new SqlParameter("@RecievedStatus", SqlDbType.Int);
                    arParms[5].Value = ObjPurReqData.RecievedStatus;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetPurchaseOrderList", arParms);
                    List<PurchaseRequisitionData> PurchaseRequisitionList = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                    result = PurchaseRequisitionList;
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

        public List<PurchaseRequisitionData> GetPurchaseOrderListByRQNumber(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[0].Value = ObjPurReqData.RQNumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_GetPurchaseOrderListByRQNumber", arParms);
                    List<PurchaseRequisitionData> PurchaseRequisitionList = ORHelper<PurchaseRequisitionData>.FromDataReaderToList(sqlReader);
                    result = PurchaseRequisitionList;
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
        //------------------------------------- End Purchase Cross Checking Page Tab 1 ---------------------------------------
        //------------------------------------- Start Purchase Cross Checking Page Tab 2 ---------------------------------------
        public int UpdatePurchaseOrderRecievedList(PurchaseRequisitionData ObjPurReqData)
        {
            int result = 0;     
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = ObjPurReqData.XMLData;

                    arParms[1] = new SqlParameter("@RQNumber", SqlDbType.VarChar);
                    arParms[1].Value = ObjPurReqData.RQNumber;

                    arParms[2] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                    arParms[2].Value = ObjPurReqData.PONumber;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[3].Value = ObjPurReqData.EmployeeID;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                    arParms[4].Value = ObjPurReqData.AddedBy;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = ObjPurReqData.HospitalID;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = ObjPurReqData.IPaddress;

                    arParms[7] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[7].Value = ObjPurReqData.FinancialYearID;

                    arParms[8] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[8].Value = ObjPurReqData.ActionType;

                    arParms[9] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[9].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Purchase_UpdatePurchaseOrderRecievedList", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[9].Value);
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


        //------------------------------------- End Purchase Cross Checking Page Tab 2 ---------------------------------------
    }
}
