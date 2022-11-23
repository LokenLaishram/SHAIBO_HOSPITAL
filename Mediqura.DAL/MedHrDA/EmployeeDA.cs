using Mediqura.CommonData.MedHrData;
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

namespace Mediqura.DAL.MedHrDA
{
    public class EmployeeDA
    {
        public int UpdateEmployeeDetails(EmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[49];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@EmployeeNo", SqlDbType.VarChar);
                    arParms[1].Value = objemp.EmployeeNo;

                    arParms[2] = new SqlParameter("@SalutationID", SqlDbType.Int);
                    arParms[2].Value = objemp.SalutationID;

                    arParms[3] = new SqlParameter("@EmpName", SqlDbType.VarChar);
                    arParms[3].Value = objemp.EmpName;

                    arParms[4] = new SqlParameter("@DOB", SqlDbType.DateTime);
                    arParms[4].Value = objemp.DOB;

                    arParms[5] = new SqlParameter("@DOJ", SqlDbType.DateTime);
                    arParms[5].Value = objemp.DOJ;

                    arParms[6] = new SqlParameter("@NationalityID", SqlDbType.Int);
                    arParms[6].Value = objemp.NationalityID;

                    arParms[7] = new SqlParameter("@ReligionID", SqlDbType.Int);
                    arParms[7].Value = objemp.ReligionID;

                    arParms[8] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[8].Value = objemp.DepartmentID;

                    arParms[9] = new SqlParameter("@DesignationID", SqlDbType.Int);
                    arParms[9].Value = objemp.DesignationID;

                    arParms[10] = new SqlParameter("@CountryID", SqlDbType.Int);
                    arParms[10].Value = objemp.CurrentCountryID;

                    arParms[11] = new SqlParameter("@StateID", SqlDbType.Int);
                    arParms[11].Value = objemp.CurrentStateID;

                    arParms[12] = new SqlParameter("@DistrictID", SqlDbType.Int);
                    arParms[12].Value = objemp.CurrentDistrictID;

                    arParms[13] = new SqlParameter("@Pin", SqlDbType.Int);
                    arParms[13].Value = objemp.CurrentPIN;

                    arParms[14] = new SqlParameter("@Address", SqlDbType.VarChar);
                    arParms[14].Value = objemp.CurrentAddress;

                    arParms[15] = new SqlParameter("@EmployeeType", SqlDbType.Int);
                    arParms[15].Value = objemp.EmployeeTypeID;

                    arParms[16] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                    arParms[16].Value = objemp.EmailID;

                    arParms[17] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                    arParms[17].Value = objemp.MobileNo;

                    arParms[18] = new SqlParameter("@PhoneNo", SqlDbType.VarChar);
                    arParms[18].Value = objemp.PhoneNo;

                    arParms[19] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[19].Direction = ParameterDirection.Output;

                    arParms[20] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[20].Value = objemp.EmployeeID;

                    arParms[21] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[21].Value = objemp.HospitalID;

                    arParms[22] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[22].Value = objemp.FinancialYearID;

                    arParms[23] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[23].Value = objemp.ActionType;

                    arParms[24] = new SqlParameter("@IDmark", SqlDbType.VarChar);
                    arParms[24].Value = objemp.IDmarks;

                    arParms[25] = new SqlParameter("@MaritalStatusID", SqlDbType.Int);
                    arParms[25].Value = objemp.MaritalStatusID;

                    arParms[26] = new SqlParameter("@Qualification", SqlDbType.VarChar);
                    arParms[26].Value = objemp.Qualification;

                    arParms[27] = new SqlParameter("@GradeID", SqlDbType.Int);
                    arParms[27].Value = objemp.EmpGradeID;

                    arParms[28] = new SqlParameter("@CountryID1", SqlDbType.Int);
                    arParms[28].Value = objemp.CurrentCountryID;

                    arParms[29] = new SqlParameter("@StateID1", SqlDbType.Int);
                    arParms[29].Value = objemp.CurrentStateID;

                    arParms[30] = new SqlParameter("@DistrictID1", SqlDbType.Int);
                    arParms[30].Value = objemp.CurrentDistrictID;

                    arParms[31] = new SqlParameter("@Pin1", SqlDbType.Int);
                    arParms[31].Value = objemp.CurrentPIN;

                    arParms[32] = new SqlParameter("@Address1", SqlDbType.VarChar);
                    arParms[32].Value = objemp.CurrentAddress;

                    arParms[33] = new SqlParameter("@Spouse", SqlDbType.VarChar);
                    arParms[33].Value = objemp.SpouseName;

                    arParms[34] = new SqlParameter("@Caste", SqlDbType.Int);
                    arParms[34].Value = objemp.CastID;

                    arParms[35] = new SqlParameter("@Guardian", SqlDbType.VarChar);
                    arParms[35].Value = objemp.GuardianName;

                    arParms[36] = new SqlParameter("@BloodGrp", SqlDbType.Int);
                    arParms[36].Value = objemp.BloodGroupID;

                    arParms[37] = new SqlParameter("@StaffCategoryID", SqlDbType.Int);
                    arParms[37].Value = objemp.StaffCategoryID;

                    arParms[38] = new SqlParameter("@WrkExp", SqlDbType.VarChar);
                    arParms[38].Value = objemp.WorkExp;

                    arParms[39] = new SqlParameter("@ImageFile", SqlDbType.Image);
                    arParms[39].Value = objemp.ImageFile;

                    arParms[40] = new SqlParameter("@SignatureFile", SqlDbType.Image);
                    arParms[40].Value = objemp.SignatureFile;

                    arParms[41] = new SqlParameter("@SignatureLocation", SqlDbType.VarChar);
                    arParms[41].Value = objemp.DigitalSignatureLocation;

                    arParms[42] = new SqlParameter("@ImageLocation", SqlDbType.VarChar);
                    arParms[42].Value = objemp.EmployeePhotoLocation;

                    arParms[43] = new SqlParameter("@AliasName", SqlDbType.VarChar);
                    arParms[43].Value = objemp.AliasName;

                    arParms[44] = new SqlParameter("@Status", SqlDbType.Bit);
                    arParms[44].Value = objemp.IsActive;

                    arParms[45] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[45].Value = objemp.Remarks;

                    arParms[46] = new SqlParameter("@ExcludeMsb", SqlDbType.Int);
                    arParms[46].Value = objemp.ExcludeMsb;

                    arParms[47] = new SqlParameter("@AadhaarNo", SqlDbType.VarChar);
                    arParms[47].Value = objemp.AadhaarNo;

                    arParms[48] = new SqlParameter("@FPData", SqlDbType.VarChar);
                    arParms[48].Value = objemp.FPData;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateEmployeeDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[19].Value);
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
        public List<EmployeeData> GetEmployeeNo(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmployeeNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmployeeNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeNo", arParms);
                    List<EmployeeData> lstresult = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> GetEmployeeName(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmpName", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmpName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeName", arParms);
                    List<EmployeeData> lstresult = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> GetNurseName(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmpName", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmpName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetNurseName", arParms);
                    List<EmployeeData> lstresult = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> GetdiscountBy(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmpName", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmpName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDiscountBY", arParms);
                    List<EmployeeData> lstresult = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> GetContactno(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.MobileNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetContactNos", arParms);
                    List<EmployeeData> lstresult = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> SearchEmployeetDetails(EmployeeData objemp)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@EmployeeNo", SqlDbType.VarChar);
                    arParms[0].Value = objemp.EmployeeNo;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objemp.EmployeeID;

                    arParms[2] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                    arParms[2].Value = objemp.MobileNo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objemp.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objemp.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objemp.IsActive;

                    arParms[6] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[6].Value = objemp.EmployeeID;

                    arParms[7] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[7].Value = objemp.DepartmentID;

                    arParms[8] = new SqlParameter("@EmployeeTypeID", SqlDbType.Int);
                    arParms[8].Value = objemp.EmployeeTypeID;

                    arParms[9] = new SqlParameter("@StaffCategoryID", SqlDbType.Int);
                    arParms[9].Value = objemp.StaffCategoryID;

                    arParms[10] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[10].Value = objemp.CurrentIndex;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeDetailsLIST", arParms);
                    List<EmployeeData> listpatientdetails = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public int Updategenstockemployee(GenStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@GenSubStockID", SqlDbType.Int);
                    arParms[1].Value = objemp.GenSubStockID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateGenStockEmployee", arParms);
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
        public int UpdateMedEmployees(MedStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objemp.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_Updateemployess", arParms);
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
        public int Updatemedstockemployee(MedStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objemp.MedSubStockID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateMedStockEmployee", arParms);
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
        public int UpdategenstockemployeeRequestEnable(GenStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@GenItemRequestEnable", SqlDbType.Int);
                    arParms[1].Value = objemp.GenItemRequestEnable;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateGenStockEmployeeRequestEnable", arParms);
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
        public int UpdatemedstockemployeeRequestEnable(MedStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@MedItemRequestEnable", SqlDbType.Int);
                    arParms[1].Value = objemp.MedItemRequestEnable;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateMedStockEmployeeRequestEnable", arParms);
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
        public int UpdategenstockemployeeVerifyEnable(GenStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@GenItemVerifyEnable", SqlDbType.Int);
                    arParms[1].Value = objemp.GenItemVerifyEnable;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateGenStockEmployeeVerifyEnable", arParms);
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
        public int UpdatemedstockemployeeVerifyEnable(MedStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@MedItemVerifyEnable", SqlDbType.Int);
                    arParms[1].Value = objemp.MedItemVerifyEnable;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateMedStockEmployeeVerifyEnable", arParms);
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
        public int UpdategenstockemployeeApproveEnable(GenStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@GenItemApproveEnable", SqlDbType.Int);
                    arParms[1].Value = objemp.GenItemApproveEnable;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateGenStockEmployeeApproveEnable", arParms);
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
        public int UpdatemedstockemployeeApproveEnable(MedStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@MedItemApproveEnable", SqlDbType.Int);
                    arParms[1].Value = objemp.MedItemApproveEnable;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateMedStockEmployeeApproveEnable", arParms);
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
        public int UpdategenstockemployeeHandoverEnable(GenStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@GenItemHandoverEnable", SqlDbType.Int);
                    arParms[1].Value = objemp.GenItemHandoverEnable;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateGenStockEmployeeHandoverEnable", arParms);
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
        public int UpdatemedstockemployeeHandoverEnable(MedStockEmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@MedItemHandoverEnable", SqlDbType.Int);
                    arParms[1].Value = objemp.MedItemHandoverEnable;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateMedStockEmployeeHandoverEnable", arParms);
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
        public List<GenStockEmployeeData> GetGenStockEmployees(GenStockEmployeeData objemp)
        {
            List<GenStockEmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@GenSubStockID", SqlDbType.Int);
                    arParms[0].Value = objemp.GenSubStockID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objemp.EmployeeID;

                    arParms[2] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[2].Value = objemp.DepartmentID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetGenstockEmployeeList", arParms);
                    List<GenStockEmployeeData> listpatientdetails = ORHelper<GenStockEmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<MedStockEmployeeData> GetMedStockEmployees(MedStockEmployeeData objemp)
        {
            List<MedStockEmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[0].Value = objemp.MedSubStockID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objemp.EmployeeID;

                    arParms[2] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[2].Value = objemp.DepartmentID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetMedstockEmployeeList", arParms);
                    List<MedStockEmployeeData> listpatientdetails = ORHelper<MedStockEmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> SearchEmployeeDetails(EmployeeData objemp)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@EmployeeNo", SqlDbType.VarChar);
                    arParms[0].Value = objemp.EmployeeNo;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objemp.EmployeeID;

                    arParms[2] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                    arParms[2].Value = objemp.MobileNo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objemp.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objemp.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objemp.IsActive;

                    arParms[6] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[6].Value = objemp.EmployeeID;

                    arParms[7] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[7].Value = objemp.DepartmentID;

                    arParms[8] = new SqlParameter("@EmployeeTypeID", SqlDbType.Int);
                    arParms[8].Value = objemp.EmployeeTypeID;

                    arParms[9] = new SqlParameter("@StaffCategoryID", SqlDbType.Int);
                    arParms[9].Value = objemp.StaffCategoryID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeDetails", arParms);
                    List<EmployeeData> listpatientdetails = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> GetEmployeeList(EmployeeData objemp)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@Dept", SqlDbType.VarChar);
                    arParms[1].Value = objemp.DepartmentID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeDiscountDetailsByID", arParms);
                    List<EmployeeData> listpatientdetails = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public int DeleteEmployeeDetailsByID(EmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;
                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;
                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objemp.Remarks;
                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objemp.UserLoginId;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEmployeeDetailsbyID", arParms);
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
        public List<EmployeeData> GetEmployeeDetailbyID(EmployeeData objemp)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeDetailsByID", arParms);
                    List<EmployeeData> listpatientdetails = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> GetEmpdetails(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@EmpName", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmpName;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.VarChar);
                    arParms[1].Value = objD.EmployeeID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeDetails", arParms);
                    List<EmployeeData> lstresult = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> GetDependentList(EmployeeData objemp)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeDependentList", arParms);
                    List<EmployeeData> listpatientdetails = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public int UpdateEmployeeDependent(EmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objemp.EmployeeID;

                    arParms[1] = new SqlParameter("@XMLData", SqlDbType.VarChar);
                    arParms[1].Value = objemp.XMLData;

                    arParms[2] = new SqlParameter("@DeptID", SqlDbType.Int);
                    arParms[2].Value = objemp.DepartmentID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[4].Value = objemp.UserLoginId;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objemp.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objemp.FinancialYearID;

                    arParms[7] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[7].Value = objemp.ActionType;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateEmployeeDependentDetails", arParms);
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
        public int UpdateEmployeeDiscount(EmployeeData objemp)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.VarChar);
                    arParms[0].Value = objemp.XMLData;

                    arParms[1] = new SqlParameter("@DeptID", SqlDbType.Int);
                    arParms[1].Value = objemp.DepartmentID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[3].Value = objemp.UserLoginId;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objemp.HospitalID;

                    arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[5].Value = objemp.FinancialYearID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[6].Value = objemp.ActionType;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateEmployeeDiscountDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[2].Value);
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
        public int UpdateEmpRole(EmployeeData obj)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = obj.EmployeeID;

                    arParms[1] = new SqlParameter("@UserLoginId", SqlDbType.BigInt);
                    arParms[1].Value = obj.UserLoginId;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[3].Value = obj.RoleID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateEmpRoleStatus", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[2].Value);
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
        public List<EmployeeData> SearchEmployeeRoleDetails(EmployeeData objemp)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@EmployeeNo", SqlDbType.VarChar);
                    arParms[0].Value = objemp.EmployeeNo;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objemp.EmployeeID;

                    arParms[2] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                    arParms[2].Value = objemp.MobileNo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objemp.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objemp.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objemp.IsActive;

                    arParms[6] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[6].Value = objemp.EmployeeID;

                    arParms[7] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[7].Value = objemp.DepartmentID;

                    arParms[8] = new SqlParameter("@EmployeeTypeID", SqlDbType.Int);
                    arParms[8].Value = objemp.EmployeeTypeID;

                    arParms[9] = new SqlParameter("@StaffCategoryID", SqlDbType.Int);
                    arParms[9].Value = objemp.StaffCategoryID;

                    arParms[10] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[10].Value = objemp.CurrentIndex;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeRoleDetailsLIST", arParms);
                    List<EmployeeData> listpatientdetails = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> SearchEmployeeRoleExcel(EmployeeData objemp)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@EmployeeNo", SqlDbType.VarChar);
                    arParms[0].Value = objemp.EmployeeNo;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objemp.EmployeeID;

                    arParms[2] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                    arParms[2].Value = objemp.MobileNo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objemp.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objemp.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objemp.IsActive;

                    arParms[6] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[6].Value = objemp.EmployeeID;

                    arParms[7] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[7].Value = objemp.DepartmentID;

                    arParms[8] = new SqlParameter("@EmployeeTypeID", SqlDbType.Int);
                    arParms[8].Value = objemp.EmployeeTypeID;

                    arParms[9] = new SqlParameter("@StaffCategoryID", SqlDbType.Int);
                    arParms[9].Value = objemp.StaffCategoryID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeRoleExcel", arParms);
                    List<EmployeeData> listpatientdetails = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
