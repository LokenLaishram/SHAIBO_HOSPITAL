using Mediqura.CommonData.Common;
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

namespace Mediqura.DAL.CommonDA
{
    public class MasterLookupDA : DataAccessObjectBase
    {
        public List<LookupItem> GetLookupsList(LookupName lookupName)
        {
            List<LookupItem> lstObject = null;
            try
            {
                SqlParameterHelper paraColl = new SqlParameterHelper();
                paraColl.Add(Mediqura.DAL.DatabaseConstants.LookupConstants.Parameters.LookupsName, SqlDbType.VarChar, lookupName.ToString());
                SqlDataReader dataReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetMasterLookup", paraColl.ToArray());
                lstObject = ORHelper<LookupItem>.FromDataReaderToList(dataReader);
                dataReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return lstObject;
        }
        public List<LookupItem> GetDoctorByDoctorTypeID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDoctorByDoctorType", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetGenderBySalutationID(int ID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetGenderByID", arParms);
                GenderLists = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return GenderLists;
        }
        public List<LookupItem> GetMenuSubheaderByHeaderID(int ID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;
                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetMenuSubeheaderByID", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetGestockByDesignation(int ID, Int64 EmployeeID)
        {
            List<LookupItem> GenStockList = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];

                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[1].Value = EmployeeID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetGenStockByDesignation", arParms);
                GenStockList = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return GenStockList;
        }
        public List<LookupItem> GetGestockByDesignationforIndent(int ID, Int64 EmployeeID)
        {
            List<LookupItem> GenStockList = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];

                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[1].Value = EmployeeID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetGenStockByDesignationForIndent", arParms);
                GenStockList = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return GenStockList;
        }
        public List<LookupItem> GetMedstocktypeByEmployeeID(Int64 EmployeeID)
        {
            List<LookupItem> GenStockList = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];

                arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[0].Value = EmployeeID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_MedStocktypeByEmployeeID", arParms);
                GenStockList = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return GenStockList;
        }
        public List<LookupItem> GetStateByCountryID(int ID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetStateByCountryID", arParms);
                GenderLists = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return GenderLists;
        }
        public List<LookupItem> GetDistrictByStateD(int ID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDistrictByStateID", arParms);
                GenderLists = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return GenderLists;
        }
        public List<LookupItem> GetEmployeeByRoleID(int ID)
        {
            List<LookupItem> Employeelist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetEmployeeByRoleID", arParms);
                Employeelist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Employeelist;
        }

        public List<LookupItem> GetRackByID(int ID)
        {
            List<LookupItem> Employeelist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetRackByID", arParms);
                Employeelist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Employeelist;
        }
        public List<LookupItem> GetSubRackByID(int ID)
        {
            List<LookupItem> Employeelist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetSubRackByID", arParms);
                Employeelist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Employeelist;
        }
        public List<LookupItem> GetStockType(int ID)
        {
            List<LookupItem> StockTypelist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetStockType", arParms);
                StockTypelist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return StockTypelist;
        }
        public List<LookupItem> GetPagesByRoleID(int ID)
        {
            List<LookupItem> Pagelist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetPagesByRoleID", arParms);
                Pagelist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Pagelist;
        }
        public List<LookupItem> GetDoctorBydepartmentID(int ID, int doctortype)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;
                arParms[1] = new SqlParameter("@doctortype", SqlDbType.Int);
                arParms[1].Value = doctortype;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDoctorByDepartmentID", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetDepartmentDoctor(int ID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDepartmentDoctor", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetReferalBySourceID(int ID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@SourceID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetReferals", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetGenitemHandoverEmployeeByID(int genstockID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@genstockID", SqlDbType.Int);
                arParms[0].Value = genstockID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetGenStockEmployeeList", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetGenitemRequestedEmployeeByID(int genstockID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@genstockID", SqlDbType.Int);
                arParms[0].Value = genstockID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetGenRequestStockEmployeeList", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetAppoinmnetDateByMonthID(int MonthID, Int64 DoctorID, int Year)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[3];

                arParms[0] = new SqlParameter("@MonthID", SqlDbType.Int);
                arParms[0].Value = MonthID;

                arParms[1] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                arParms[1].Value = DoctorID;

                arParms[2] = new SqlParameter("@Year", SqlDbType.Int);
                arParms[2].Value = Year;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDatebyDoctorID", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetDOCtype(int ID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDocType", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetOTemployees(int ID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetOTEmployeeByRoleID", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetReferalemployees(int ID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetReferalEmployeeByRoleID", arParms);
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDocByDocType", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetItemNameBySubGroupID(int subgroupID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                arParms[0].Value = subgroupID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetItemNameBySubGroupID", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }

        public List<LookupItem> GetOPDoctorBydepartmentID(int ID, int doctortype)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;
                arParms[1] = new SqlParameter("@doctortype", SqlDbType.Int);
                arParms[1].Value = doctortype;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDoctorByDepartmentID", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetIPDoctorBydepartmentID(int ID)
        {
            List<LookupItem> Doclist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetIPDoctorByDepartmentID", arParms);
                Doclist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return Doclist;
        }
        public List<LookupItem> GetOT_Status(int ID)
        {
            List<LookupItem> OTlist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_OT_Statusgridddl", arParms);
                OTlist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return OTlist;
        }
        public List<LookupItem> GetSubGroupByGroupID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetSubGroupByGroupID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetSubServiceTypeByGroupID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetSubServiceTypeByGroupID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetServiceByID(int GrpID, int SbGrpID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];

                arParms[0] = new SqlParameter("@GrpID", SqlDbType.Int);
                arParms[0].Value = GrpID;

                arParms[1] = new SqlParameter("@SbGrpID", SqlDbType.Int);
                arParms[1].Value = SbGrpID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetServiceeByID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }

        public List<LookupItem> GetTestNameBySubGroupID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetTestNameBySubGroupID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetWardByStationID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetWardByStationID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }

        public List<LookupItem> GetfloorByblockID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetFloorByBlockID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetWardByFloorID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetWardByWardID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetIPDWardByFloorID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetIPDWardByWardID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }

        public List<LookupItem> GetItemSubGroupByItemGroupID(int ID)
        {
            List<LookupItem> ItemSubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetItemSubGroupByGroupID", arParms);
                ItemSubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return ItemSubGrouplist;
        }
        public List<LookupItem> GetGENItemSubRackByItemRackID(int ID)
        {
            List<LookupItem> ItemSubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_CMS_GetItemSubRackByRackID", arParms);
                ItemSubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return ItemSubGrouplist;
        }
        public List<LookupItem> GetGENItemSubGroupByItemGroupID(int ID)
        {
            List<LookupItem> ItemSubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_CMS_GetItemSubGroupByGroupID", arParms);
                ItemSubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return ItemSubGrouplist;
        }
        //public List<LookupItem> GetGENItemSubGroupByItemGroupID(int ID)
        //{
        //    List<LookupItem> ItemSubGrouplist = null;
        //    try
        //    {
        //        SqlParameter[] arParms = new SqlParameter[1];
        //        arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
        //        arParms[0].Value = ID;

        //        SqlDataReader sqlReader = null;
        //        sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetGENItemSubGroupByGroupID", arParms);
        //        ItemSubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
        //        sqlReader.Close();
        //    }
        //    catch (Exception ex) //Exception of the business layer(itself)//unhandle
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
        //        throw new BusinessProcessException("5000001", ex);
        //    }
        //    return ItemSubGrouplist;
        //}
        public List<LookupItem> GetNurseByNurseType(int ID)
        {
            List<LookupItem> ItemSubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetNurseByType", arParms);
                ItemSubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return ItemSubGrouplist;
        }
        public List<LookupItem> Getweek(int ID, int year)
        {
            List<LookupItem> ItemSubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];
                arParms[0] = new SqlParameter("@MonthID", SqlDbType.Int);
                arParms[0].Value = ID;

                arParms[1] = new SqlParameter("@Year", SqlDbType.Int);
                arParms[1].Value = year;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_ADM_YearCounter", arParms);
                ItemSubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return ItemSubGrouplist;
        }

        public List<LookupItem> GetMsbRelation(int ID)
        {
            List<LookupItem> MsbRelation = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@Marital", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetMsbRelation", arParms);
                MsbRelation = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return MsbRelation;
        }
        public List<LookupItem> GetDoctorNameByServiceTypeForBill(DiscountRequestData objData)
        {
            List<LookupItem> DoctorList = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];

                arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
                arParms[0].Value = objData.serviceTypeID;

                arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                arParms[1].Value = objData.BillNo;
                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "GetDoctorForServiceByBillNo", arParms);
                DoctorList = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return DoctorList;
        }
        public List<LookupItem> GetServiceNameByGroupID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "GetServiceNameByGroupID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetDependentByempID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDependent", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetOtDocByIPNo(string IPNo)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                arParms[0].Value = IPNo;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetOtDoctorByIPNO", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetEmployeeByDep(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;
                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetEmployeeByDepID", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetCollectedUserID(Int32 DeptID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@DeptID", SqlDbType.Int);
                arParms[0].Value = DeptID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetUserOfdeptId", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetUserByDeptID(Int32 ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = ID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetUserOfdeptId", arParms);
                SubGrouplist = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
                sqlReader.Close();
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new BusinessProcessException("5000001", ex);
            }
            return SubGrouplist;
        }

		public List<LookupItem> GetShiftByRosterID(int ID)
		{
			List<LookupItem> ShiftLists = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[1];
				arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
				arParms[0].Value = ID;

				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetShiftByRosterID", arParms);
				ShiftLists = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
				sqlReader.Close();
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new BusinessProcessException("5000001", ex);
			}
			return ShiftLists;
		}

		public List<LookupItem> GetDesignationByDepartmentID(int ID)
		{
			List<LookupItem> DesignationLists = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[1];
				arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
				arParms[0].Value = ID;

				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDesignationByDepartmentID", arParms);
				DesignationLists = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
				sqlReader.Close();
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new BusinessProcessException("5000001", ex);
			}
			return DesignationLists;
		}

		public List<LookupItem> GetEmployeeTypeByDepartmentID(int ID)
		{
			List<LookupItem> EmployeeTypeLists = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[1];
				arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
				arParms[0].Value = ID;



				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeTypeByDepartmentID", arParms);
				EmployeeTypeLists = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
				sqlReader.Close();
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new BusinessProcessException("5000001", ex);
			}
			return EmployeeTypeLists;
		}

		public List<LookupItem> GetLeaveTypeByEmpID(Int64 ID)
		{
			List<LookupItem> LeaveTypeLists = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[1];
				arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
				arParms[0].Value = ID;



				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLeaveTypeByEmpID", arParms);
				LeaveTypeLists = ORHelper<LookupItem>.FromDataReaderToList(sqlReader);
				sqlReader.Close();
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new BusinessProcessException("5000001", ex);
			}
			return LeaveTypeLists;
		}

    }
}


