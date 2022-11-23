using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedGenStoreBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStore;
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

namespace Mediqura.Web.MedGenStore
{
    public partial class IndentVerifier : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                bindIndentList();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substocklist, mstlookup.GetGestockByDesignation(LogData.DesignationID, LogData.EmployeeID));
            Commonfunction.PopulateDdl(ddl_requestTypeList, mstlookup.GetLookupsList(LookupName.requestType));
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.GenStockType));
            ddl_substock.Attributes["disabled"] = "disabled";
            AutoCompleteExtender2.ContextKey = LogData.EmployeeID.ToString();
            AutoCompleteExtender2.CompletionSetCount = LogData.DesignationID;
            if (LogData.DesignationID == 93 || LogData.DesignationID == 20 || LogData.DesignationID == 122 || LogData.DesignationID == 25)
            {
                ddl_substocklist.Attributes.Remove("disabled");
                Commonfunction.Insertzeroitemindex(ddl_requested);
            }
            else
            {
                ddl_substocklist.Attributes["disabled"] = "disabled";
                ddl_substocklist.SelectedValue = LogData.GenSubStockID.ToString();
                Commonfunction.PopulateDdl(ddl_requested, mstlookup.GetGenitemRequestedEmployeeByID(Convert.ToInt32(ddl_substocklist.SelectedValue == "" ? "0" : ddl_substocklist.SelectedValue)));
            }
            if (LogData.RoleID == 1)
            {
                Commonfunction.PopulateDdl(ddl_substocklist, mstlookup.GetLookupsList(LookupName.GenStockType));
                ddl_substocklist.SelectedValue = LogData.GenSubStockID.ToString();
                ddl_substocklist.Attributes.Remove("disabled");
            }
        }
        protected void ddl_substocklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_substocklist.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_requested, mstlookup.GetGenitemRequestedEmployeeByID(Convert.ToInt32(ddl_substocklist.SelectedValue == "" ? "0" : ddl_substocklist.SelectedValue)));
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_requested);
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIndentNumbers(string prefixText, int count, string contextKey)
        {
            GenIndentData Objpaic = new GenIndentData();
            GenIndentApprovedBO objInfoBO = new GenIndentApprovedBO();
            List<GenIndentData> getResult = new List<GenIndentData>();
            Objpaic.IndentNo = prefixText;
            Objpaic.EmployeeID = Convert.ToInt32(contextKey);
            Objpaic.DesignationID = count;
            getResult = objInfoBO.GetIndentNumberbyStockID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IndentNo.ToString());
            }
            return list;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindIndentList();
        }
        protected void bindIndentList()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_substocklist.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "GenStock", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                //if (txt_from.Text == "")
                //{
                //    Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                //    divmsg2.Attributes["class"] = "FailAlert";
                //    divmsg2.Visible = true;
                //    txt_from.Focus();
                //    return;
                //}
                //else 
                if (txt_from.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_from.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_from.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                //if (txt_To.Text == "")
                //{
                //    Messagealert_.ShowMessage(txt_To, "ValidDate", 0);
                //    divmsg2.Attributes["class"] = "FailAlert";
                //    divmsg2.Visible = true;
                //    txt_To.Focus();
                //    return;
                //}
                //else 
                if (txt_To.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_To.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_To.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<GenIndentData> objdeposit = GetIndentList(0);
                if (objdeposit.Count > 0)
                {
                    txt_totalReq.Text = Commonfunction.Getrounding(objdeposit[0].TotalIndentQty.ToString());
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div3.Attributes["class"] = "SucessAlert";
                    div3.Visible = true;
                    gvIndentlist.DataSource = objdeposit;
                    gvIndentlist.DataBind();
                    gvIndentlist.Visible = true;
                }
                else
                {
                    lblresult1.Visible = false;
                    div3.Visible = false;
                    gvIndentlist.DataSource = null;
                    gvIndentlist.DataBind();
                    gvIndentlist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        public List<GenIndentData> GetIndentList(int curIndex)
        {
            GenIndentData objstock = new GenIndentData();
            GenIndentApprovedBO objBO = new GenIndentApprovedBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.GenStockID = Convert.ToInt32(ddl_substocklist.SelectedValue == "" ? "0" : ddl_substocklist.SelectedValue);
            objstock.IndentRequestID = Convert.ToInt32(ddl_requestTypeList.SelectedValue == "" ? "0" : ddl_requestTypeList.SelectedValue);
            objstock.RequestedBy = Convert.ToInt64(ddl_requested.SelectedValue == "" ? "0" : ddl_requested.SelectedValue);
            objstock.IndentNo = txt_indentnumbers.Text == "" ? null : txt_indentnumbers.Text.Trim();
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.VerifiedStatus = Convert.ToInt32(ddl_verification.SelectedValue == "" ? "0" : ddl_verification.SelectedValue);
            objstock.IndentStateID = Convert.ToInt32(ddlindentstatus.SelectedValue == "" ? "0" : ddlindentstatus.SelectedValue);
            objstock.DesignationID = LogData.DesignationID;
            objstock.EmployeeID = LogData.EmployeeID;
            return objBO.GetIndentListforVerification(objstock);
        }
        protected void gvIndentRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    GenIndentData objbill = new GenIndentData();
                    GenIndentApprovedBO objstdBO = new GenIndentApprovedBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvIndentlist.Rows[i];
                    LinkButton Indno = (LinkButton)gr.Cells[0].FindControl("lbl_Indentno");
                    Label SubStockID = (Label)gr.Cells[0].FindControl("lbl_substockID");
                    Label indentDate = (Label)gr.Cells[0].FindControl("lbl_Indentdate");
                    Label Indenttype = (Label)gr.Cells[0].FindControl("lblReqTypestatus");
                    Label requestedBy = (Label)gr.Cells[0].FindControl("lbl_AddedBy");
                    Label indentqty = (Label)gr.Cells[0].FindControl("lbl_qty");
                    Label StatusID = (Label)gr.Cells[0].FindControl("lblverifiedstatus");
                    ddl_substock.SelectedValue = SubStockID.Text.ToString();
                    txt_IssuueDate.Text = indentDate.Text;
                    txt_requesttype.Text = Indenttype.Text.ToString();
                    txt_totalindentqty.Text = indentqty.Text;
                    txt_IndentNo.Text = Indno.Text;
                    txt_requestedby.Text = requestedBy.Text;
                    if (StatusID.Text == "0")
                    {
                        btnsave.Attributes.Remove("disabled");
                    }
                    else
                    {
                        btnsave.Attributes["disabled"] = "disabled";
                        btnprint.Attributes["disabled"] = "disabled";
                    }
                    bindindentdetails(Indno.Text == "" ? "0" : Indno.Text);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void bindindentdetails(string indent)
        {
            GenIndentData objbill = new GenIndentData();
            GenIndentApprovedBO objstdBO = new GenIndentApprovedBO();
            objbill.IndentNo = indent;
            List<GenIndentData> List = new List<GenIndentData>();
            List = objstdBO.GetIndentList1(objbill);
            if (List.Count > 0)
            {
                txt_totalindentqty.Text = List[0].TotalRqty.ToString();
                tabcontainerindent.ActiveTabIndex = 1;
                GvindentDetails.DataSource = List;
                GvindentDetails.DataBind();
                GvindentDetails.Visible = true;
            }
            else
            {
                tabcontainerindent.ActiveTabIndex = 1;
                GvindentDetails.DataSource = null;
                GvindentDetails.DataBind();
                GvindentDetails.Visible = true;
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0 || LogData.GenItemVerifyEnable == 0)
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
            List<GenIndentData> List = new List<GenIndentData>();
            GenIndentApprovedBO objBO = new GenIndentApprovedBO();
            GenIndentData objrec = new GenIndentData();
            int rejectcount = 0;
            try
            {
                foreach (GridViewRow row in GvindentDetails.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ItemID = (Label)GvindentDetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    TextBox totreqstdqty = (TextBox)GvindentDetails.Rows[row.RowIndex].Cells[0].FindControl("txt_ReqQty");
                    TextBox Remark = (TextBox)GvindentDetails.Rows[row.RowIndex].Cells[0].FindControl("txt_remarks");
                    GenIndentData obj = new GenIndentData();
                    obj.IndentNo = txt_IndentNo.Text;
                    obj.ItemID = Convert.ToInt64(ItemID.Text == "" ? "0" : ItemID.Text);
                    obj.ReqdQty = Convert.ToInt32(totreqstdqty.Text == "" ? "0" : totreqstdqty.Text);
                    if (Convert.ToInt32(totreqstdqty.Text == "" ? "0" : totreqstdqty.Text) == 0)
                    {
                        if (Remark.Text == "")
                        {
                            rejectcount = rejectcount + 1;
                        }
                        Remark.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    }
                    obj.Remarks = Remark.Text;
                    List.Add(obj);
                }
                if (ddl_verifcation.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "VerificationStatus", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (rejectcount > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Rejection", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                objrec.XMLData = XmlConvertor.Gen_indentverificationDatatoXML(List).ToString();
                objrec.VerifiedBy = LogData.EmployeeID;
                objrec.VerifiedStatus = Convert.ToInt32(ddl_verifcation.SelectedValue == "" ? "0" : ddl_verifcation.SelectedValue);
                objrec.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IndentNo = txt_IndentNo.Text.Trim();
                objrec.IPaddress = LogData.IPaddress;
                int result = objBO.UpdateIndentVerification(objrec);
                if (result == 1)
                {
                    bindindentdetails(txt_IndentNo.Text);
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btnprint.Attributes.Remove("disabled");
                    btnsave.Attributes["disabled"] = "disabled";
                }
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            txt_IndentNo.Text = "";
            txt_IssuueDate.Text = "";
            txt_requestedby.Text = "";
            txt_requesttype.Text = "";
            txt_totalindentqty.Text = "";
            //Commonfunction.Insertzeroitemindex(ddl_substock);
            GvindentDetails.DataSource = null;
            GvindentDetails.DataBind();
            GvindentDetails.Visible = false;
            ddl_verifcation.SelectedIndex = 0;
            bindIndentList();
            tabcontainerindent.ActiveTabIndex = 0;
        }
        protected void txt_indentnumbers_TextChanged(object sender, EventArgs e)
        {
            bindIndentList();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvIndentlist.DataSource = null;
            gvIndentlist.DataBind();
            gvIndentlist.Visible = false;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            div3.Visible = false;
            divmsg2.Visible = false;
            ddl_requestTypeList.SelectedIndex = 0;
            txt_indentnumbers.Text = "";
            ddl_requested.SelectedIndex = 0;
            txt_from.Text = "";
            txt_To.Text = "";
            ddl_verification.SelectedIndex = 0;
            ddlindentstatus.SelectedIndex = 0;
            txt_totalReq.Text = "";
        }
    }
}