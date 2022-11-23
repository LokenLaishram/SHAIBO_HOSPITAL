using Mediqura.BOL.CommonBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedEmergencyData;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedIPD
{
    public partial class IPDBillSetting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpatienttype, mstlookup.GetLookupsList(LookupName.DuePatientType));
            //ddlpatienttype.SelectedIndex = 1;
        }
        protected void ddlpatienttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender1.ContextKey = ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNoWithName(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
            Objpaic.IPNo = prefixText;
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.getIPNoEmrgNoWithNameAgeNAddress(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void ddl_servicecategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_servicecategory.SelectedIndex > 0)
            {
                bindgrid();
            }
        }
        private void bindgrid()
        {
            if (txtautoIPNo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtautoIPNo.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (ddlpatienttype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "PatientType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddlpatienttype.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            List<IPfinalbillData> objdeposit = GetIPDBillSetting(0);
            if (objdeposit.Count > 0)
            {
                Gv_servicelist.DataSource = objdeposit;
                Gv_servicelist.DataBind();
                Gv_servicelist.Visible = true;

            }
            else
            {
                Gv_servicelist.DataSource = null;
                Gv_servicelist.DataBind();
                Gv_servicelist.Visible = true;


            }
        }
        public List<IPfinalbillData> GetIPDBillSetting(int curIndex)
        {
            IPfinalbillData objpat = new IPfinalbillData();
            IPServiceRecordBO objBO = new IPServiceRecordBO();
            if (txtautoIPNo.Text != "")
            {
                string IPNo;
                var source = txtautoIPNo.Text.ToString();
                if (source.Contains(":"))
                {
                    IPNo = source.Substring(source.LastIndexOf(':') + 1);
                    objpat.IPNo = IPNo.ToString();
                }

                else
                {
                    objpat.IPNo = txtautoIPNo.Text.Trim() == "" ? "" : txtautoIPNo.Text.Trim();
                }
            }
            else
            {
                objpat.IPNo = txtautoIPNo.Text.Trim() == "" ? "" : txtautoIPNo.Text.Trim();
            }
            objpat.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
            objpat.PatientCategory = Convert.ToInt32(ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue);
            objpat.AmountEnable = LogData.AmountEnable;
            return objBO.GetIPDBillSetting(objpat);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();

        }
        //protected void ddl_servicecategoryList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddl_servicecategoryList.SelectedIndex > 0)
        //    {
        //        bindgridList();
        //    }
        //}
        //protected void btnsearchList_Click(object sender, EventArgs e)
        //{
        //    bindgridList();

        //}
        //protected void bindgridList()
        //{
        //    try
        //    {
        //        if (LogData.SearchEnable == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
        //            divmsg2.Visible = true;
        //            divmsg2.Attributes["class"] = "FailAlert";
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage2.Visible = false;
        //        }

        //        if (txtautoIPNoList.Text == "")
        //        {
        //            Messagealert_.ShowMessage(lblmessage2, "IPNo", 0);
        //            divmsg2.Visible = true;
        //            divmsg2.Attributes["class"] = "FailAlert";
        //            txtautoIPNoList.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage2.Visible = false;
        //            divmsg2.Visible = false;
        //        }

        //        List<IPDBillSettingData> objdeposit = GetIPDServiceListSetting(0);
        //        if (objdeposit.Count > 0)
        //        {
        //            Gv_servicelistList.DataSource = objdeposit;
        //            Gv_servicelistList.DataBind();
        //            Gv_servicelistList.Visible = true;
        //            Messagealert_.ShowMessage(lblresult2, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
        //            divmsg4.Attributes["class"] = "SucessAlert";
        //            divmsg4.Visible = true;

        //        }
        //        else
        //        {
        //            Gv_servicelistList.DataSource = null;
        //            Gv_servicelistList.DataBind();
        //            Gv_servicelistList.Visible = true;
        //            lblresult2.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
        //        Messagealert_.ShowMessage(lblmessage2, "system", 0);
        //        divmsg2.Attributes["class"] = "FailAlert";
        //        divmsg2.Visible = true;
        //    }
        //}
        //public List<IPDBillSettingData> GetIPDServiceListSetting(int curIndex)
        //{
        //    IPDBillSettingData objpat = new IPDBillSettingData();
        //    IPServiceRecordBO objBO = new IPServiceRecordBO();
        //    if (txtautoIPNoList.Text != "")
        //    {
        //        string IPNo;
        //        var source = txtautoIPNoList.Text.ToString();
        //        if (source.Contains(":"))
        //        {
        //            IPNo = source.Substring(source.LastIndexOf(':') + 1);
        //            objpat.IPNo = IPNo.ToString();
        //        }

        //        else
        //        {
        //            objpat.IPNo = txtautoIPNoList.Text.Trim() == "" ? "" : txtautoIPNoList.Text.Trim();
        //        }
        //    }
        //    else
        //    {
        //        objpat.IPNo = txtautoIPNoList.Text.Trim() == "" ? "" : txtautoIPNoList.Text.Trim();
        //    }
        //    objpat.ServiceCategoryID = Convert.ToInt32(ddl_servicecategoryList.SelectedValue == "" ? "0" : ddl_servicecategoryList.SelectedValue);
        //    return objBO.GetIPDServiceListSetting(objpat);
        //}
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtautoIPNo.Text = "";
            ddl_servicecategory.SelectedIndex = 0;
            Gv_servicelist.DataSource = null;
            Gv_servicelist.DataBind();
            Gv_servicelist.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;


        }
        //protected void btnresets_Click(object sender, EventArgs e)
        //{
        //    txtautoIPNoList.Text = "";
        //    ddl_servicecategoryList.SelectedIndex = 0;
        //    Gv_servicelistList.DataSource = null;
        //    Gv_servicelistList.DataBind();
        //    Gv_servicelistList.Visible = false;
        //    lblmessage2.Visible = false;
        //    divmsg2.Visible = false;
        //}
        protected void Gv_servicelist_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (LogData.UpdateEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            Int32 ID = Convert.ToInt32(Gv_servicelist.DataKeys[e.RowIndex].Values["ID"].ToString());
            IPDBillSettingData objpat = new IPDBillSettingData();
            IPServiceRecordBO objBO = new IPServiceRecordBO();
            objpat.ID = ID;
            objpat.EmployeeID = LogData.EmployeeID;

            int result = objBO.UpdateIPNoServiceCharge(objpat);
            if (result > 0)
            {
                Gv_servicelist.DataSource = null;
                Gv_servicelist.DataBind();
                bindgrid();

                Messagealert_.ShowMessage(lblresult, "update", 1);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "SucessAlert";
            }
            else
            {
                lblresult.Visible = false;
                lblmessage.Visible = false;
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<IPDBillSettingData> Listser = new List<IPDBillSettingData>();
            IPServiceRecordBO objiprecBO = new IPServiceRecordBO();
            IPDBillSettingData objrec = new IPDBillSettingData();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in Gv_servicelist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Ipno = (Label)Gv_servicelist.Rows[row.RowIndex].Cells[0].FindControl("lblIPNo");
                    Label serviceID = (Label)Gv_servicelist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label rate = (Label)Gv_servicelist.Rows[row.RowIndex].Cells[0].FindControl("lblrate");
                    Label qty = (Label)Gv_servicelist.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)Gv_servicelist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");

                }
                //objrec.XMLData = XmlConvertor.IPDserviceSettingRecordDatatoXML(Listser).ToString();
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;
                objrec.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                Listser = objiprecBO.UpdateIPDBillSettingRecord(objrec);
            }

            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }

        //protected void Gv_servicelistList_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Edits")
        //    {
        //        if (LogData.EditEnable == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage2, "EditEnable", 0);
        //            divmsg2.Visible = true;
        //            divmsg2.Attributes["class"] = "FailAlert";
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage2.Visible = false;
        //        }
        //        int i = Convert.ToInt16(e.CommandArgument.ToString());
        //        GridViewRow pt = Gv_servicelistList.Rows[i];
        //        Label ID = (Label)pt.Cells[0].FindControl("lblID");

        //    }
        //    if (e.CommandName == "Deletes")
        //    {
        //        if (LogData.DeleteEnable == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
        //            divmsg2.Visible = true;
        //            divmsg2.Attributes["class"] = "FailAlert";
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage2.Visible = false;
        //        }
        //        int i = Convert.ToInt16(e.CommandArgument.ToString());
        //        GridViewRow pt = Gv_servicelistList.Rows[i];
        //        Label ID = (Label)pt.Cells[0].FindControl("lblID");


        //    }

        //}

        protected void Gv_servicelist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                if (LogData.EditEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                int i = Convert.ToInt16(e.CommandArgument.ToString());
                GridViewRow pt = Gv_servicelist.Rows[i];
                Label ID = (Label)pt.Cells[0].FindControl("lblID");

            }
            if (e.CommandName == "Deletes")
            {
                if (LogData.DeleteEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                int i = Convert.ToInt16(e.CommandArgument.ToString());
                GridViewRow pt = Gv_servicelist.Rows[i];
                Label ID = (Label)pt.Cells[0].FindControl("lblID");
            }
        }
    }
}