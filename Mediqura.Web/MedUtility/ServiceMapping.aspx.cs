using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedAccount;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedUtility
{
    public partial class ServiceMapping : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ddlbind();
                btnupdate.Visible = false;
                checkSelect();
                Session["ServiceList"] = null;

            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.CommonGroup));
            Commonfunction.Insertzeroitemindex(ddl_subservicetype);
        }
        public void checkSelect()
        {
            if (ddl_group_type.SelectedIndex == 0)
            {
                ddl_servicetype.Attributes["disabled"] = "disabled";

            }
            else
            {
                ddl_servicetype.Attributes.Remove("disabled");

            }
            if (ddl_servicetype.SelectedIndex == 0)
            {
                ddl_map_type.Attributes["disabled"] = "disabled";

            }
            else
            {
                ddl_map_type.Attributes.Remove("disabled");

            }
            if (ddl_map_type.SelectedIndex == 0)
            {
                ddl_subservicetype.Attributes["disabled"] = "disabled";
            }
            else if (ddl_map_type.SelectedIndex == 1)
            {
                ddl_subservicetype.Attributes["disabled"] = "disabled";

            }
            else if (ddl_map_type.SelectedIndex == 2)
            {
                ddl_subservicetype.Attributes.Remove("disabled");
            }
        }
        protected void ddl_servicetype_SelectedIndexChanged(object sender, EventArgs e)
        {

            MasterLookupBO mstlookup = new MasterLookupBO();
            if (ddl_group_type.SelectedIndex == 0)
            {

            }
            else if (ddl_group_type.SelectedIndex == 1)
            {
                if (ddl_servicetype.SelectedIndex > 0)
                {
                    Commonfunction.PopulateDdl(ddl_subservicetype, mstlookup.GetSubServiceTypeByGroupID(Convert.ToInt32(ddl_servicetype.SelectedValue)));
                    AutoCompleteExtender1.ContextKey = ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue;
                }
            }
            else
            {
                if (ddl_servicetype.SelectedIndex > 0)
                {
                    Commonfunction.PopulateDdl(ddl_subservicetype, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_servicetype.SelectedValue)));
                    AutoCompleteExtender1.ContextKey = ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue;
                }
            }
            checkSelect();
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<AccountMappingMasterData> Listobjdata = new List<AccountMappingMasterData>();
            AccountMappingMasterData objdata = new AccountMappingMasterData();
            AccountBO objstdBO = new AccountBO();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in GVMapping.Rows)
                {
                    Label lblServiceTypeID = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblServiceTypeID");
                    Label lblsubServiceTypeID = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblsubServiceTypeID");
                    Label lblServiceID = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblServiceID");
                    TextBox txtDebitAcount = (TextBox)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("txt_debit_account");
                    TextBox txtCreditAcount = (TextBox)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("txt_credit_accnt");
                    Label lblMappingType = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblMappingType");
                    Label lblGroupType = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblGroupType");



                    AccountMappingMasterData objsubdata = new AccountMappingMasterData();
                    objsubdata.ServiceType = Convert.ToInt32(lblServiceTypeID.Text == "" ? "0" : lblServiceTypeID.Text);
                    objsubdata.SubServiceType = Convert.ToInt32(lblServiceTypeID.Text == "" ? "0" : lblsubServiceTypeID.Text);
                    objsubdata.ServiceID = Convert.ToInt32(lblServiceID.Text == "" ? "0" : lblServiceID.Text);
                    objsubdata.MappingType = Convert.ToInt32(lblMappingType.Text == "" ? "0" : lblMappingType.Text);
                    objsubdata.GroupType = Convert.ToInt32(lblGroupType.Text == "" ? "0" : lblGroupType.Text);

                    String debitAcntID = "0";
                    String CreditAcntID = "0";

                    String debitText = txtDebitAcount.Text == "" ? null : txtDebitAcount.Text.ToString().Trim();
                    if (debitText != null)
                    {
                        String[] debit = debitText.Split(new[] { ":" }, StringSplitOptions.None);
                        debitAcntID = debit[1];
                    }
                    String CreditText = txtCreditAcount.Text == "" ? null : txtCreditAcount.Text.ToString().Trim();
                    if (CreditText != null)
                    {
                        String[] credit = CreditText.Split(new[] { ":" }, StringSplitOptions.None);
                        CreditAcntID = credit[1];
                    }


                    objsubdata.DebitID = Convert.ToInt32(debitAcntID);
                    objsubdata.CreditID = Convert.ToInt32(CreditAcntID);
                    Listobjdata.Add(objsubdata);

                }
                objdata.XMLData = XmlConvertor.AccountMappingCollectionDatatoXML(Listobjdata).ToString();

                int result = objstdBO.UpdateAccntMappingMaster(objdata);
                if (result > 0)
                {

                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";

                }
                else
                {

                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);

            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            ServiceMappingMasterData Objpaic = new ServiceMappingMasterData();
            ServiceMappingMasterBO objInfoBO = new ServiceMappingMasterBO();
            List<ServiceMappingMasterData> getResult = new List<ServiceMappingMasterData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.GroupType = Convert.ToInt32(contextKey == "" ? "0" : contextKey);
            getResult = objInfoBO.GetServices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void btn_add_nurse_Click(object sender, EventArgs e)
        {
            addnurse();
        }
        protected void txtNames_TextChanged(object sender, EventArgs e)
        {
            addnurse();
        }
        private void addnurse() {

            if (ddl_servicetype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select nurse station", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<ServiceMappingMasterData> ServiceList = Session["ServiceList"] == null ? new List<ServiceMappingMasterData>() : (List<ServiceMappingMasterData>)Session["ServiceList"];
            ServiceMappingMasterData objData = new ServiceMappingMasterData();
            Int32 ID = 0;

            String servicename = txtServices.Text == "" ? null : txtServices.Text.ToString().Trim();
            if (servicename != null)
            {
                String[] name = servicename.Split(new[] { ":" }, StringSplitOptions.None);
                ID = Convert.ToInt32(name[1]);
            }
            else
            {
                txtServices.Text = "";
            }
            if (ID == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select a service name!", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            foreach (GridViewRow row in GVMapping.Rows)
            {
                Label serviceID = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblserviceID");
                if (Convert.ToInt32(serviceID.Text) == ID)
                {
                    txtServices.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtServices.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            objData.ServiceID = ID;
            objData.Servicetype = ddl_servicetype.SelectedItem.Text;
            objData.ServicetypeID = Convert.ToInt32(ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue);
            objData.ServiceName = txtServices.Text;

            ServiceList.Add(objData);
            if (ServiceList.Count > 0)
            {
                GVMapping.DataSource = ServiceList;
                GVMapping.DataBind();
                GVMapping.Visible = true;
                txtServices.Text = "";
                ddl_servicetype.Attributes["disabled"] = "disabled";
                Session["ServiceList"] = ServiceList;
                txtServices.Focus();
                btnupdate.Visible = true;
               
            }
            else
            {
                GVMapping.DataSource = null;
                GVMapping.DataBind();
                GVMapping.Visible = true;
            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            List<ServiceMappingMasterData> objlist = new List<ServiceMappingMasterData>();
            ServiceMappingMasterBO objbo = new ServiceMappingMasterBO();
            ServiceMappingMasterData objdata = new ServiceMappingMasterData();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in GVMapping.Rows)
                {

                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label serviceID = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblserviceID");
                    ServiceMappingMasterData ObjDetails = new ServiceMappingMasterData();

                    ObjDetails.ServiceID = Convert.ToInt32(serviceID.Text == "" ? "0" : serviceID.Text);
                    objlist.Add(ObjDetails);

                }
                objdata.XMLData = XmlConvertor.ServiceMappingRecordDatatoXML(objlist).ToString();
                objdata.GroupType = Convert.ToInt32(ddl_group_type.SelectedValue == "" ? "0" : ddl_group_type.SelectedValue);
                objdata.ServicetypeID = Convert.ToInt32(ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue);
                objdata.MappingType = Convert.ToInt32(ddl_map_type.SelectedValue == "" ? "0" : ddl_map_type.SelectedValue);
                objdata.SubServicetype = Convert.ToInt32(ddl_subservicetype.SelectedValue == "" ? "0" : ddl_subservicetype.SelectedValue);
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.ActionType = Enumaction.Insert;
                int result = objbo.UpdateServiceMappingDetails(objdata);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";



                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddl_servicetype.SelectedIndex = 0;
            ddl_group_type.SelectedIndex = 0;
            ddl_map_type.SelectedIndex = 0;
            ddl_subservicetype.SelectedIndex = 0;
            btnupdate.Visible = false;
            GVMapping.DataSource = null;
            GVMapping.DataBind();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddl_servicetype.Attributes["disabled"] = "disabled";
            ddl_map_type.Attributes["disabled"] = "disabled";
            ddl_subservicetype.Attributes["disabled"] = "disabld";
            txtServices.Text = ""; 
        }
        protected void bindgrid()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_group_type.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Select Group Type", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_servicetype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Select Service Type", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (ddl_map_type.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Select Mapping Type", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    if (ddl_map_type.SelectedIndex == 2)
                    {
                        if (ddl_subservicetype.SelectedIndex == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage, "Select Sub group Type", 0);
                            div1.Visible = true;
                            div1.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    lblmessage.Visible = false;
                }
                List<ServiceMappingMasterData> ServiceList = GetServiceMappingDetails(0);
                if (ServiceList.Count > 0)
                {
                    btnupdate.Visible = true;
                    GVMapping.DataSource = ServiceList;
                    GVMapping.DataBind();
                    GVMapping.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + ServiceList[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    lblresult.Visible = false;
                    div1.Visible = false;
                    btnupdate.Visible = false;
                }
                else
                {
                    btnupdate.Visible = false;
                    divmsg3.Visible = false;
                    GVMapping.DataSource = null;
                    GVMapping.DataBind();
                    GVMapping.Visible = true;
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<ServiceMappingMasterData> GetServiceMappingDetails(int curIndex)
        {

            ServiceMappingMasterData objdata = new ServiceMappingMasterData();
            ServiceMappingMasterBO objstdBO = new ServiceMappingMasterBO();

            objdata.ServicetypeID = Convert.ToInt32(ddl_servicetype.SelectedValue == "0" ? "0" : ddl_servicetype.SelectedValue);
            objdata.SubServicetype = Convert.ToInt32(ddl_subservicetype.SelectedValue == "0" ? "0" : ddl_subservicetype.SelectedValue);
            objdata.MappingType = Convert.ToInt32(ddl_map_type.SelectedValue == "0" ? "0" : ddl_map_type.SelectedValue);
            objdata.GroupType = Convert.ToInt32(ddl_group_type.SelectedValue == "0" ? "0" : ddl_group_type.SelectedValue);
            Int32 ID = 0;

            String servicename = txtServices.Text == "" ? null : txtServices.Text.ToString().Trim();
            if (servicename != null)
            {
                String[] name = servicename.Split(new[] { ":" }, StringSplitOptions.None);
                ID = Convert.ToInt32(name[1]);
            }
            else
            {
                txtServices.Text = "";
            }
            objdata.ServiceID = Convert.ToInt32(ID);

            return objstdBO.GetServiceMappingDetails(objdata);

        }
        protected void GVMapping_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVMapping.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    if (ID.Text == "0")
                    {
                        List<ServiceMappingMasterData> ServiceList = Session["ServiceList"] == null ? new List<ServiceMappingMasterData>() : (List<ServiceMappingMasterData>)Session["ServiceList"];
                        ServiceList.RemoveAt(i);
                        Session["ServiceList"] = ServiceList;
                        GVMapping.DataSource = ServiceList;
                        GVMapping.DataBind();
                    }
                    else
                    {
                        ServiceMappingMasterData objData = new ServiceMappingMasterData();
                        ServiceMappingMasterBO objlabserviceBO = new ServiceMappingMasterBO();
                        objData.ID = Convert.ToInt32(ID.Text);
                        objData.EmployeeID = LogData.EmployeeID;
                        objData.ActionType = Enumaction.Delete;
                        ServiceMappingMasterBO objlabserviceBO1 = new ServiceMappingMasterBO();
                        int Result = objlabserviceBO1.DeleteServiceMappingDetailsByID(objData);
                        if (Result == 1)
                        {
                            Messagealert_.ShowMessage(lblmessage, "delete", 1);
                            div1.Visible = true;
                            div1.Attributes["class"] = "SucessAlert";
                            bindgrid();
                        }
                        else
                        {
                            Session["LabSubtestlist"] = null;
                            GVMapping.DataSource = null;
                            GVMapping.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void ddl_map_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_map_type.SelectedIndex == 0)
            {
                ddl_subservicetype.Attributes["disabled"] = "disabled";
            }
            else if (ddl_map_type.SelectedIndex == 1)
            {
                ddl_subservicetype.Attributes["disabled"] = "disabled";

            }
            else if (ddl_map_type.SelectedIndex == 2)
            {
                ddl_subservicetype.Attributes.Remove("disabled");
            }
        }
        protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            if (ddl_group_type.SelectedIndex == 0)
            {

            }
            else if (ddl_group_type.SelectedIndex == 1)
            {
                Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.CommonGroup));
                AutoCompleteExtender1.ContextKey = ddl_group_type.SelectedValue == "" ? "0" : ddl_group_type.SelectedValue;
            }
            else
            {
                Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.LabGroupAll));
                AutoCompleteExtender1.ContextKey = ddl_group_type.SelectedValue == "" ? "0" : ddl_group_type.SelectedValue;
            }
            checkSelect();
        }
    }
}