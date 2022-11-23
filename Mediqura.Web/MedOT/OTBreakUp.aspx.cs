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
using Mediqura.BOL.AdmissionBO;
using Mediqura.CommonData.OTData;
using Mediqura.BOL.OTBO;

namespace Mediqura.Web.MedOT
{
    public partial class OTBreakUp : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                bindOtpatientlist();
                btnsave.Attributes["disabled"] = "disabled";
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetLookupsList(LookupName.OTpayabledoctors));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPServiceRecordData Objpaic = new IPServiceRecordData();
            IPServiceRecordBO objInfoBO = new IPServiceRecordBO();
            List<IPServiceRecordData> getResult = new List<IPServiceRecordData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getOTRegisteredIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetotPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void bindOtpatientlist()
        {
            try
            {
                List<OTRegnData> objdeposit = Get_CompletedOtlist(0);
                if (objdeposit.Count > 0)
                {
                    Gv_CompletedOtlist.Visible = true;
                    Gv_CompletedOtlist.DataSource = objdeposit;
                    Gv_CompletedOtlist.DataBind();
                }
                else
                {
                    Gv_CompletedOtlist.DataSource = null;
                    Gv_CompletedOtlist.DataBind();
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div6.Visible = true;
                div6.Attributes["class"] = "FailAlert";
            }
        }
        public List<OTRegnData> Get_CompletedOtlist(int curIndex)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            OTRegnData objpat = new OTRegnData();
            OTRegnBO objbillingBO = new OTRegnBO();
            objpat.OTemployeeID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.OTNo = txt_patientname.Text == "" ? " " : txt_patientname.Text.Substring(txt_patientname.Text.LastIndexOf(':') + 1).Trim();
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objbillingBO.GetOT_CompletedOtlist(objpat);
        }
        protected void Gv_CompletedOtlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label status = (Label)e.Row.FindControl("lbl_otstatus");
                Label Verifystatus = (Label)e.Row.FindControl("lbl_veridystatus");
                if (status.Text == "0")
                {
                    Verifystatus.Text = "Not Verified";
                }
                if (status.Text == "1")
                {
                    Verifystatus.Text = "Verified";
                }
            }
        }
        protected void Gv_CompletedOtlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {
                    OTRegnData objData = new OTRegnData();
                    OTRegnBO objbillingBO = new OTRegnBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gv_CompletedOtlist.Rows[i];
                    LinkButton OTnumber = (LinkButton)gr.Cells[0].FindControl("lbl_otnumber");
                    Label caseID = (Label)gr.Cells[0].FindControl("lbl_CaseID");
                    objData.OTNo = OTnumber.Text.Trim();
                    objData.CaseID = Convert.ToInt32(caseID.Text == "" ? "0" : caseID.Text);
                    List<OTRegnData> Result = objbillingBO.GetCompletedOtdetailByID(objData);
                    if (Result.Count > 0)
                    {
                        txt_name.Text = Result[0].PatientName.ToString();
                        txt_case.Text = Result[0].CaseName.ToString();
                        txt_totalamount.Text = "₹ " + Commonfunction.Getrounding(Result[0].TotalAmount.ToString());
                        txt_totalsurgeon.Text = Commonfunction.Getrounding(Result[0].TotalSurgeonAmount.ToString());
                        txt_totalanasthesia.Text = Commonfunction.Getrounding(Result[0].TotalAnaesthesiaAmount.ToString());
                        GvOTBreakUp.Visible = true;
                        GvOTBreakUp.DataSource = Result;
                        GvOTBreakUp.DataBind();
                        btnsave.Attributes.Remove("disabled");
                        lblmessage.Visible = false;

                    }
                    else
                    {
                        txt_name.Text = "";
                        txt_case.Text = "";
                        txt_totalamount.Text = "";
                        txt_totalsurgeon.Text = "";
                        txt_totalanasthesia.Text = "";
                        btnsave.Attributes["disabled"] = "disabled";
                        GvOTBreakUp.DataSource = null;
                        GvOTBreakUp.DataBind();
                    }
                    LinkButton result1 = (LinkButton)Gv_CompletedOtlist.Rows[i].Cells[0].FindControl("lbl_otnumber");
                    result1.Focus();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div6.Attributes["class"] = "FailAlert";
                div6.Visible = true;
                return;
            }
        }
        protected void txt_share_TextChanged(object sender, EventArgs e)
        {
            int Lastindex = GvOTBreakUp.Rows.Count - 1;
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            if (Lastindex > index)
            {
                TextBox result1 = (TextBox)GvOTBreakUp.Rows[index + 1].Cells[0].FindControl("txtamount");
                result1.Focus();
            }
            else if (Lastindex == index)
            {
                TextBox result2 = (TextBox)GvOTBreakUp.Rows[index].Cells[0].FindControl("txtamount");
                btnsave.Focus();
            }
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            bindOtpatientlist();
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                div6.Visible = true;
                div6.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;

            }
            List<OTRegnData> List = new List<OTRegnData>();
            OTRegnBO objBO = new OTRegnBO();
            OTRegnData objrec = new OTRegnData();
            try
            {
                decimal Totalsurgeonshare = 0, TotalAnaesthShare = 0;
                // get all the record from the gridview
                foreach (GridViewRow row in GvOTBreakUp.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ID = (Label)GvOTBreakUp.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label OTno = (Label)GvOTBreakUp.Rows[row.RowIndex].Cells[0].FindControl("lbl_OTNo");
                    Label IPnumber = (Label)GvOTBreakUp.Rows[row.RowIndex].Cells[0].FindControl("lbl_IPno");
                    Label CaseID = (Label)GvOTBreakUp.Rows[row.RowIndex].Cells[0].FindControl("lbl_CaseID");
                    Label EmployeeID = (Label)GvOTBreakUp.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeeID");
                    Label RoleID = (Label)GvOTBreakUp.Rows[row.RowIndex].Cells[0].FindControl("lbl_roleID");
                    TextBox amt = (TextBox)GvOTBreakUp.Rows[row.RowIndex].Cells[0].FindControl("txtamount");

                    if (RoleID.Text == "1" || RoleID.Text == "2" || RoleID.Text == "4")
                    {
                        Totalsurgeonshare = Totalsurgeonshare + Convert.ToDecimal(amt.Text == "" ? "0" : amt.Text);
                    }
                    if (RoleID.Text == "3")
                    {
                        TotalAnaesthShare = TotalAnaesthShare + Convert.ToDecimal(amt.Text == "" ? "0" : amt.Text);
                    }
                    OTRegnData obj = new OTRegnData();
                    obj.ID = Convert.ToInt64(ID.Text);
                    obj.Amount = Convert.ToDecimal(amt.Text == "" ? "0" : amt.Text);
                    obj.OTemployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
                    obj.RoleID = Convert.ToInt32(RoleID.Text == "" ? "0" : RoleID.Text);
                    obj.CaseID = Convert.ToInt32(CaseID.Text == "" ? "0" : CaseID.Text);
                    obj.OTNo = OTno.Text;
                    obj.IPNo = IPnumber.Text;
                    List.Add(obj);
                }
                objrec.XMLData = XmlConvertor.OTBreakUpRecordDatatoXML(List).ToString();

                if (Convert.ToDecimal(txt_totalsurgeon.Text == "" ? "0" : txt_totalsurgeon.Text) != Totalsurgeonshare)
                {
                    Messagealert_.ShowMessage(lblmessage, "SurgeonShare", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (Convert.ToDecimal(txt_totalanasthesia.Text == "" ? "0" : txt_totalanasthesia.Text) != TotalAnaesthShare)
                {
                    Messagealert_.ShowMessage(lblmessage, "AnasthesShare", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                objrec.EmployeeID = LogData.EmployeeID;
                int result = objBO.UpdateOTshare(objrec);
                if (result > 0)
                {
                    bindOtpatientlist();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div6.Visible = true;
                    div6.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div6.Visible = true;
                div6.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            btnsave.Attributes["disabled"] = "disabled";
            GvOTBreakUp.DataSource = null;
            GvOTBreakUp.DataBind();
            GvOTBreakUp.Visible = false;
            lblmessage.Visible = false;
            div6.Visible = false;
            txt_case.Text = "";
            txt_name.Text = "";
            txt_totalamount.Text = "";
            txt_totalsurgeon.Text = "";
            txt_totalanasthesia.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            ddl_doctor.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            txt_patientname.Text = "";
            ddl_doctor.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            Gv_CompletedOtlist.DataSource = null;
            Gv_CompletedOtlist.Visible = false;
        }
    }
}