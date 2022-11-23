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

namespace Mediqura.DAL.POCreationDA
{
    public class POCreationDA
    {
        public List<GENStrData> GetItemCheckList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ItemID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetCheckItemList", arParms);
                    List<GENStrData> listpatientdetails = ORHelper<GENStrData>.FromDataReaderToList(sqlReader);
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
        public int UpdatePurchaseCheckItemList(GENStrData objstock)
        {
            int result = 0;
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

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objstock.FinancialYearID;

                    arParms[5] = new SqlParameter("@TotalUsable", SqlDbType.Int);
                    arParms[5].Value = objstock.TotalUsable;

                    arParms[6] = new SqlParameter("@TotalCondemnedQty", SqlDbType.Int);
                    arParms[6].Value = objstock.TotalCondemnedQty;

                    arParms[7] = new SqlParameter("@TotalExpiry", SqlDbType.Int);
                    arParms[7].Value = objstock.TotalExpiry;

                    arParms[8] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[8].Value = objstock.CP;

                    arParms[9] = new SqlParameter("@TotalQtyReqd", SqlDbType.Int);
                    arParms[9].Value = objstock.TotalQuantity;

                    arParms[10] = new SqlParameter("@PurchaseMethodID", SqlDbType.Int);
                    arParms[10].Value = objstock.PuchaseMethodID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Update_Purchase_ItemCheckList", arParms);
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
        public List<GENStrData> GetItemList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ItemName;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetItemList", arParms);
                    List<GENStrData> listpatientdetails = ORHelper<GENStrData>.FromDataReaderToList(sqlReader);
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
        public List<GENStrData> GetPurchaseList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.PONo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetPurchaseList", arParms);
                    List<GENStrData> listpatientdetails = ORHelper<GENStrData>.FromDataReaderToList(sqlReader);
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
        public int DeleteItemListByID(GENStrData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];


                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[1].Value = objbill.Remarks;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[3] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[3].Value = objbill.ItemName;

                    arParms[4] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.ID;

                    arParms[5] = new SqlParameter("@Available", SqlDbType.Int);
                    arParms[5].Value = objbill.BalStock;

                    arParms[6] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[6].Value = objbill.CP;

                    arParms[7] = new SqlParameter("@ApprovalListNo", SqlDbType.VarChar);
                    arParms[7].Value = objbill.ApprovalListNo;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_DeletePurchaseItemListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[0].Value);
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
