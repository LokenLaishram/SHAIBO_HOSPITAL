using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.DAL.CommonDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.CommonBO
{
    public class MasterLookupBO
    {
        public List<LookupItem> GetLookupsList(LookupName lookupName)
        {
            List<LookupItem> listLookUpList = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> listLookup = objMasterLookup.GetLookupsList(lookupName);
                listLookUpList = listLookup;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return listLookUpList;
        }
        public List<LookupItem> GetGenderBySalutationID(int ID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderList = objMasterLookup.GetGenderBySalutationID(ID);
                GenderLists = GenderList;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderLists;
        }
        public List<LookupItem> GetDoctorByDoctorTypeID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetDoctorByDoctorTypeID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetMenuSubheaderByHeaderID(int ID)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetMenuSubheaderByHeaderID(ID);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetGestockByDesignation(int ID, Int64 EmployeeID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderList = objMasterLookup.GetGestockByDesignation(ID,EmployeeID);
                GenderLists = GenderList;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderLists;
        }
        public List<LookupItem> GetGestockByDesignationforIndent(int ID, Int64 EmployeeID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderList = objMasterLookup.GetGestockByDesignationforIndent(ID, EmployeeID);
                GenderLists = GenderList;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderLists;
        }
        public List<LookupItem> GetMedstocktypeByEmployeeID(Int64 EmployeeID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderList = objMasterLookup.GetMedstocktypeByEmployeeID(EmployeeID);
                GenderLists = GenderList;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderLists;
        }
        public List<LookupItem> GetStateByCountryID(int ID)
        {
            List<LookupItem> GenderDistrictList = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderDistrictLists = objMasterLookup.GetStateByCountryID(ID);
                GenderDistrictList = GenderDistrictLists;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderDistrictList;
        }
        public List<LookupItem> GetDistrictByStateD(int ID)
        {
            List<LookupItem> GenderDistrictList = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderDistrictLists = objMasterLookup.GetDistrictByStateD(ID);
                GenderDistrictList = GenderDistrictLists;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderDistrictList;
        }
        public List<LookupItem> GetStockType(int ID)
        {
            List<LookupItem> Stocktypelists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Stocktypelist = objMasterLookup.GetStockType(ID);
                Stocktypelists = Stocktypelist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return Stocktypelists;
        }
        public List<LookupItem> GetEmployeeByRoleID(int ID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderList = objMasterLookup.GetEmployeeByRoleID(ID);
                GenderLists = GenderList;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderLists;
        }

        public List<LookupItem> GetRackByID(int ID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderList = objMasterLookup.GetRackByID(ID);
                GenderLists = GenderList;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderLists;
        }
        public List<LookupItem> GetSubRackByID(int ID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderList = objMasterLookup.GetSubRackByID(ID);
                GenderLists = GenderList;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderLists;
        }

        public List<LookupItem> GetPagesByRoleID(int ID)
        {
            List<LookupItem> GenderLists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> GenderList = objMasterLookup.GetPagesByRoleID(ID);
                GenderLists = GenderList;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return GenderLists;
        }
        public List<LookupItem> GetDoctorBydepartmentID(int ID, int doctortype)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetDoctorBydepartmentID(ID, doctortype);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetGenitemHandoverEmployeeByID(int genstockID)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetGenitemHandoverEmployeeByID(genstockID);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetGenitemRequestedEmployeeByID(int genstockID)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetGenitemRequestedEmployeeByID(genstockID);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetDepartmentDoctor(int ID)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetDepartmentDoctor(ID);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetReferalBySourceID(int ID)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetReferalBySourceID(ID);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetAppoinmnetDateByMonthID(int MonthID, Int64 DoctorID, int Year)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetAppoinmnetDateByMonthID(MonthID, DoctorID, Year);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetDOCtype(int ID)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetDOCtype(ID);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetOTemployees(int ID)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetOTemployees(ID);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetReferalemployees(int ID)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetReferalemployees(ID);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetItemNameBySubGroupID(int subgroupID)
        {
            List<LookupItem> itemlists = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> itemlist = objMasterLookup.GetItemNameBySubGroupID(subgroupID);
                itemlists = itemlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return itemlists;
        }
        public List<LookupItem> GetIPDoctorBydepartmentID(int ID)
        {
            List<LookupItem> doctlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Doctorlist = objMasterLookup.GetIPDoctorBydepartmentID(ID);
                doctlist = Doctorlist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return doctlist;
        }
        public List<LookupItem> GetOT_Status(int ID)
        {
            List<LookupItem> OTlist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> OTStatuslist = objMasterLookup.GetOT_Status(ID);
                OTlist = OTStatuslist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return OTlist;
        }
        public List<LookupItem> GetSubGroupByGroupID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetSubGroupByGroupID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetSubServiceTypeByGroupID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetSubServiceTypeByGroupID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetServiceByID(int GrpID, int SbGrpID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetServiceByID(GrpID, SbGrpID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }

        public List<LookupItem> GetTestNameBySubGroupID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetTestNameBySubGroupID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }

        public List<LookupItem> GetfloorByblockID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetfloorByblockID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetWardByStationID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetWardByStationID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }


        public List<LookupItem> GetWardByFloorID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetWardByFloorID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetIPDWardByFloorID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetIPDWardByFloorID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetItemSubGroupByItemGroupID(int ID)
        {
            List<LookupItem> ItemSubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> ItemSublist = objMasterLookup.GetItemSubGroupByItemGroupID(ID);
                ItemSubGrouplist = ItemSublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return ItemSubGrouplist;
        }
        public List<LookupItem> GetGENItemSubRackByItemRackID(int ID)
        {
            List<LookupItem> ItemSubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> ItemSublist = objMasterLookup.GetGENItemSubRackByItemRackID(ID);
                ItemSubGrouplist = ItemSublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return ItemSubGrouplist;
        }
        public List<LookupItem> GetGENItemSubGroupByItemGroupID(int ID)
        {
            List<LookupItem> ItemSubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> ItemSublist = objMasterLookup.GetGENItemSubGroupByItemGroupID(ID);
                ItemSubGrouplist = ItemSublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return ItemSubGrouplist;
        }
        //public List<LookupItem> GetGENItemSubGroupByItemGroupID(int ID)
        //{
        //    List<LookupItem> ItemSubGrouplist = null;
        //    try
        //    {
        //        MasterLookupDA objMasterLookup = new MasterLookupDA();
        //        List<LookupItem> ItemSublist = objMasterLookup.GetGENItemSubGroupByItemGroupID(ID);
        //        ItemSubGrouplist = ItemSublist;
        //    }
        //    catch (Exception ex) //Exception of the business layer(itself)//unhandle
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
        //        throw new BusinessProcessException("4000001", ex);
        //    }
        //    return ItemSubGrouplist;
        //}
        public List<LookupItem> GetNurseByNurseType(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetNurseByNurseType(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> Getweek(int ID, int year)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.Getweek(ID, year);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetMsbRelation(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetMsbRelation(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetDoctorNameByServiceTypeForBill(DiscountRequestData objData)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetDoctorNameByServiceTypeForBill(objData);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetServiceNameByGroupID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetServiceNameByGroupID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetDependentByempID(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetDependentByempID(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetOtDocByIPNo(string IPNo)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetOtDocByIPNo(IPNo);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetEmployeeByDep(int ID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetEmployeeByDep(ID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetCollectedUserID(Int32 DeptID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetCollectedUserID(DeptID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;
        }
        public List<LookupItem> GetUserByDeptID(Int32 DeptID)
        {
            List<LookupItem> SubGrouplist = null;
            try
            {
                MasterLookupDA objMasterLookup = new MasterLookupDA();
                List<LookupItem> Sublist = objMasterLookup.GetCollectedUserID(DeptID);
                SubGrouplist = Sublist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return SubGrouplist;

        }
		//---------Leave---------------------
		public List<LookupItem> GetLeaveTypeByEmpID(Int64 ID)
		{
			List<LookupItem> LeaveTypeLists = null;
			try
			{
				MasterLookupDA objMasterLookup = new MasterLookupDA();
				List<LookupItem> LeaveTypeList = objMasterLookup.GetLeaveTypeByEmpID(ID);
				LeaveTypeLists = LeaveTypeList;
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return LeaveTypeLists;
		}


		//---------shiftType---------------------
		public List<LookupItem> GetShiftByRosterID(int ID)
		{
			List<LookupItem> ShiftLists = null;
			try
			{
				MasterLookupDA objMasterLookup = new MasterLookupDA();
				List<LookupItem> ShiftList = objMasterLookup.GetShiftByRosterID(ID);
				ShiftLists = ShiftList;
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return ShiftLists;
		}

		//---------Designation---------------------
		public List<LookupItem> GetDesignationByDepartmentID(int ID)
		{
			List<LookupItem> DesignationLists = null;
			try
			{
				MasterLookupDA objMasterLookup = new MasterLookupDA();
				List<LookupItem> DesignationList = objMasterLookup.GetDesignationByDepartmentID(ID);
				DesignationLists = DesignationList;
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return DesignationLists;
		}
		public List<LookupItem> GetEmployeeTypeByDepartmentID(int ID)
		{
			List<LookupItem> EmployeeTypeLists = null;
			try
			{
				MasterLookupDA objMasterLookup = new MasterLookupDA();
				List<LookupItem> EmployeeTypeList = objMasterLookup.GetEmployeeTypeByDepartmentID(ID);
				EmployeeTypeLists = EmployeeTypeList;
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return EmployeeTypeLists;
		}
    }

}
