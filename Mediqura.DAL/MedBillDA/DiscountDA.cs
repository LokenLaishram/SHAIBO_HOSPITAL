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
    public class DiscountDA
    {
        public List<DiscountRequestData> GetBillNoByServiceType(DiscountRequestData objData)
        {
            List<DiscountRequestData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[0].Value = objData.serviceTypeID;

                    arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[1].Value = objData.BillNo;

                   
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "GetBillNoByServiceType", arParms);
                    List<DiscountRequestData> ListBillNo = ORHelper<DiscountRequestData>.FromDataReaderToList(sqlReader);
                    result = ListBillNo;
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
        public List<DiscountRequestData> GetPatientDetailsByBillNo(DiscountRequestData objData)
        {
            List<DiscountRequestData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[0].Value = objData.serviceTypeID;

                    arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[1].Value = objData.BillNo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DISCOUNT_get_patientDetails_by_billNo", arParms);
                    List<DiscountRequestData> ListBillNo = ORHelper<DiscountRequestData>.FromDataReaderToList(sqlReader);
                    result = ListBillNo;
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
        public List<DiscountRequestData> GetServiceListByBillNo(DiscountRequestData objData)
        {
            List<DiscountRequestData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[0].Value = objData.serviceTypeID;

                    arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[1].Value = objData.BillNo;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objData.DoctorID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DISCOUNT_get_Service_by_bill_no", arParms);
                    List<DiscountRequestData> ListBillNo = ORHelper<DiscountRequestData>.FromDataReaderToList(sqlReader);
                    result = ListBillNo;
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
        public List<DiscountOutput> UpdateDiscoutRequest(DiscountRequestServiceData objData)
        {
            List<DiscountOutput> result ;
            try
            {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@XmlData", SqlDbType.Xml);
                    arParms[0].Value = objData.XMLData;

                    arParms[1] = new SqlParameter("@Remark", SqlDbType.VarChar);
                    arParms[1].Value = objData.Remarks;

                    arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[2].Value = objData.EmployeeID;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objData.HospitalID;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objData.FinancialYearID;

                    arParms[5] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[5].Value = objData.BillNo;

                    arParms[6] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[6].Value = objData.serviceTypeID;

                    arParms[7] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[7].Value = objData.UHID;

                    arParms[8] = new SqlParameter("@BillType", SqlDbType.Int);
                    arParms[8].Value = objData.BillType;

                    arParms[9] = new SqlParameter("@TotalAmount", SqlDbType.Money);
                    arParms[9].Value = objData.TotalAmount;

                    arParms[10] = new SqlParameter("@TotalRequest", SqlDbType.Money);
                    arParms[10].Value = objData.TotalDiscount;

                    arParms[11] = new SqlParameter("@BillID", SqlDbType.BigInt);
                    arParms[11].Value = objData.BillID;


                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Discount_Req_Update", arParms);
                List<DiscountOutput> ListBillNo = ORHelper<DiscountOutput>.FromDataReaderToList(sqlReader);
                result = ListBillNo;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<DiscountCount> GetDiscountNotification(int ID)
        {
            List<DiscountCount> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "MDQ_GET_discount_notification", arParms);
                    List<DiscountCount> listDiscount = ORHelper<DiscountCount>.FromDataReaderToList(sqlReader);
                    result = listDiscount;
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
        public List<DiscountRequestListData> GetDiscountList(DiscountRequestListData objData)
        {
            List<DiscountRequestListData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[0].Value = objData.serviceTypeID;

                    arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[1].Value = objData.BillNo;
                    
                    arParms[2] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[2].Value = objData.UHID;

                    arParms[3] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[3].Value = objData.PatName;

                    arParms[4] = new SqlParameter("@Address", SqlDbType.VarChar);
                    arParms[4].Value = objData.PatientAddress;

                    arParms[5] = new SqlParameter("@DiscountStatus", SqlDbType.Int);
                    arParms[5].Value = objData.DisStatus;

                    arParms[6] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[6].Value = objData.Datefrom;

                    arParms[7] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[7].Value = objData.DateTo;

                    arParms[8] = new SqlParameter("@ReqBy", SqlDbType.Int);
                    arParms[8].Value = objData.RequestedBy;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "MDQ_GET_discount_List", arParms);
                    List<DiscountRequestListData> listDiscount = ORHelper<DiscountRequestListData>.FromDataReaderToList(sqlReader);
                    result = listDiscount;
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
        public List<DiscountRequestListData> GetDiscountListAdmin(DiscountRequestListData objData)
        {
            List<DiscountRequestListData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[0].Value = objData.serviceTypeID;

                    arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[1].Value = objData.BillNo;

                    arParms[2] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[2].Value = objData.UHID;

                    arParms[3] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[3].Value = objData.PatName;

                    arParms[4] = new SqlParameter("@Address", SqlDbType.VarChar);
                    arParms[4].Value = objData.PatientAddress;

                    arParms[5] = new SqlParameter("@DiscountStatus", SqlDbType.Int);
                    arParms[5].Value = objData.DisStatus;

                    arParms[6] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[6].Value = objData.Datefrom;

                    arParms[7] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[7].Value = objData.DateTo;

                    arParms[8] = new SqlParameter("@ReqBy", SqlDbType.Int);
                    arParms[8].Value = objData.RequestedBy;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "MDQ_GET_discount_List_for_approval", arParms);
                    List<DiscountRequestListData> listDiscount = ORHelper<DiscountRequestListData>.FromDataReaderToList(sqlReader);
                    result = listDiscount;
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
        public int DeleteDiscountRequestByID(DiscountRequestListData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@RequestNo", SqlDbType.Int);
                    arParms[0].Value = objData.RequestNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objData.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objData.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Delete_Discount_by_Req_no", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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
        public List<DiscountRequestServiceData> GetServiceListByReqNo(DiscountRequestData objData)
        {
            List<DiscountRequestServiceData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ReqNo", SqlDbType.Int);
                    arParms[0].Value = objData.RequestNo;
                    

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DISCOUNT_get_Service_by_ReqNo", arParms);
                    List<DiscountRequestServiceData> ListBillNo = ORHelper<DiscountRequestServiceData>.FromDataReaderToList(sqlReader);
                    result = ListBillNo;
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
        public int UpdateDiscoutApproval(DiscountRequestServiceData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XmlData", SqlDbType.Xml);
                    arParms[0].Value = objData.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[2].Value = objData.EmployeeID;

                    arParms[3] = new SqlParameter("@reqNo", SqlDbType.Int);
                    arParms[3].Value = objData.RequestNo;

                    arParms[4] = new SqlParameter("@Remark", SqlDbType.VarChar);
                    arParms[4].Value = objData.Remarks;

                    arParms[5] = new SqlParameter("@TotalAmount", SqlDbType.Money);
                    arParms[5].Value = objData.TotalAmount;

                    arParms[6] = new SqlParameter("@TotalApprove", SqlDbType.Money);
                    arParms[6].Value = objData.TotalDiscount;

                    arParms[7] = new SqlParameter("@DiscountStatus", SqlDbType.Int);
                    arParms[7].Value = objData.DisStatus;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Discount_Approval_Update", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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
        public int UpdateApprovalShare(DiscountRequestServiceData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@XmlData", SqlDbType.Xml);
                    arParms[0].Value = objData.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[2].Value = objData.EmployeeID;

                    arParms[3] = new SqlParameter("@reqNo", SqlDbType.Int);
                    arParms[3].Value = objData.RequestNo;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Approval_share_Update", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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
        public List<DiscountRequestServiceData> GetApprovalServiceListByReqNo(DiscountRequestData objData)
        {
            List<DiscountRequestServiceData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ReqNo", SqlDbType.Int);
                    arParms[0].Value = objData.RequestNo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DISCOUNT_Approval_servicelist_by_ReqNo", arParms);
                    List<DiscountRequestServiceData> ListBillNo = ORHelper<DiscountRequestServiceData>.FromDataReaderToList(sqlReader);
                    result = ListBillNo;
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
        public List<DiscountRefundData> GetDiscountDetailsByBillID(Int64 ID)
        {
            List<DiscountRefundData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "MDQ_GET_discount_Details", arParms);
                    List<DiscountRefundData> listDiscount = ORHelper<DiscountRefundData>.FromDataReaderToList(sqlReader);
                    result = listDiscount;
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
        public List<DiscountOutput> UpdateDiscountRequestForAdmission(AdmissionDiscountData objData)
        {
            List<DiscountOutput> result;
            try
            {
                SqlParameter[] arParms = new SqlParameter[12];

                arParms[0] = new SqlParameter("@XmlData", SqlDbType.Xml);
                arParms[0].Value = objData.XMLData;

                arParms[1] = new SqlParameter("@Remark", SqlDbType.VarChar);
                arParms[1].Value = objData.Remarks;

                arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                arParms[2].Value = objData.EmployeeID;

                arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                arParms[3].Value = objData.HospitalID;

                arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[4].Value = objData.FinancialYearID;

                arParms[5] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                arParms[5].Value = objData.BillNo;

                arParms[6] = new SqlParameter("@ServiceType", SqlDbType.Int);
                arParms[6].Value = objData.ServiceType;

                arParms[7] = new SqlParameter("@UHID", SqlDbType.BigInt);
                arParms[7].Value = objData.UHID;

                arParms[8] = new SqlParameter("@BillType", SqlDbType.Int);
                arParms[8].Value = objData.BillingType;

                arParms[9] = new SqlParameter("@TotalAmount", SqlDbType.Money);
                arParms[9].Value = objData.TotalAmount;

                arParms[10] = new SqlParameter("@TotalRequest", SqlDbType.Money);
                arParms[10].Value = objData.TotalRequestAmount;

                arParms[11] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                arParms[11].Value = objData.IPNo;



                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Admission_Discount_Req_Update", arParms);
                List<DiscountOutput> ListBillNo = ORHelper<DiscountOutput>.FromDataReaderToList(sqlReader);
                result = ListBillNo;
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
