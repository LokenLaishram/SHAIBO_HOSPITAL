using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
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
    public partial class CondemnApproval :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
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
                    Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_from.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_from.Focus();
                    return;
                }
                else if (txt_from.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_from.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_from.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_To.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_To.Focus();
                    return;
                }
                else if (txt_To.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_To.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_To.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<CondemnReqApprovedData> objdeposit = GetCondemnList(0);
                if (objdeposit.Count > 0)
                {
                    gvCondemnRequest.DataSource = objdeposit;
                    gvCondemnRequest.DataBind();
                    gvCondemnRequest.Visible = true;
                }
                else
                {
                    gvCondemnRequest.DataSource = null;
                    gvCondemnRequest.DataBind();
                    gvCondemnRequest.Visible = true;

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
        public List<CondemnReqApprovedData> GetCondemnList(int curIndex)
        {
            CondemnReqApprovedData objCondemnstock = new CondemnReqApprovedData();
            CondemnReqApprovedBO objBO = new CondemnReqApprovedBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);           
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objCondemnstock.DateFrom = from;
            objCondemnstock.DateTo = to;
            objCondemnstock.CondemnRequestNo = txt_condemnrequestno.Text.Trim() == "" ? "0" : txt_condemnrequestno.Text.Trim();
            objCondemnstock.CondemnStatus = Convert.ToInt32(ddlcondemnstatus.SelectedValue == "" ? "0" : ddlcondemnstatus.SelectedValue);
            return objBO.GetCondemnList(objCondemnstock);
        }
        protected void gvCondemnRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {                
                 
                    CondemnReqApprovedData objcondemn = new CondemnReqApprovedData();
                    CondemnReqApprovedBO objstdBO = new CondemnReqApprovedBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvCondemnRequest.Rows[i];
                    Label CondemnNo = (Label)gr.Cells[0].FindControl("lbl_Condemnregno");
                    Label lblreqno = (Label)gr.Cells[0].FindControl("lblreqno");
                    txt_CondemnNumber.Text = lblreqno.Text;
                    txt_Condemnregno.Text = CondemnNo.Text;
                    bindcondemndetails(CondemnNo.Text);
                   
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void bindcondemndetails(string condemn) 
        {
            CondemnReqApprovedData objcondemn = new CondemnReqApprovedData();
            CondemnReqApprovedBO objstdBO = new CondemnReqApprovedBO();
            objcondemn.CondemnRegNo = Convert.ToInt64(condemn);
            List<CondemnReqApprovedData> List = new List<CondemnReqApprovedData>();
            List = objstdBO.GetCondemnDetailsList(objcondemn);
            if (List.Count > 0)
            {
                tabcontainerCondemn.ActiveTabIndex = 1;
                gvCondemnDetail.DataSource = List;
                gvCondemnDetail.DataBind();
                gvCondemnDetail.Visible = true;
                txt_GrandTotalCondemnQty.Text = List[0].GrandTotalCondemnQty.ToString();  
            }
            else
            {
                tabcontainerCondemn.ActiveTabIndex = 1;
                gvCondemnDetail.DataSource = null;
                gvCondemnDetail.DataBind();              
                gvCondemnDetail.Visible = true;
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
            List<CondemnReqApprovedData> CondemnApprovedList = new List<CondemnReqApprovedData>();
            CondemnReqApprovedData objConAppr = new CondemnReqApprovedData();
            CondemnReqApprovedBO objBO = new CondemnReqApprovedBO();          
            try
            {
                // get all the record from the gridview
                int itemcount = 0;
                foreach (GridViewRow row in gvCondemnDetail.Rows)
                {
                    Label condrequestno = (Label)gvCondemnDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_condrequestno");
                    Label CondemnItemID = (Label)gvCondemnDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label SubstockID = (Label)gvCondemnDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    Label CondemnQty = (Label)gvCondemnDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_comreqqty");
                    CheckBox CheckItem = (CheckBox)gvCondemnDetail.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect");
                    CondemnReqApprovedData ObjDetails = new CondemnReqApprovedData();
                    if (CheckItem.Checked)
                    {
                        ObjDetails.CondemnRegNo = Convert.ToInt32(condrequestno.Text.Trim());
                        ObjDetails.ItemID = Convert.ToInt32(CondemnItemID.Text.Trim());
                        ObjDetails.SubStockID = Convert.ToInt32(SubstockID.Text.Trim());
                        ObjDetails.CondemnQty = Convert.ToInt32(CondemnQty.Text.Trim());
                        CondemnApprovedList.Add(ObjDetails);
                        itemcount = itemcount + 1;
                    } 
                }
                objConAppr.XMLData = XmlConvertor.CondemnApprovedDatatoXML(CondemnApprovedList).ToString();
                if (itemcount == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ItemCount", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }              
                objConAppr.ApprovedStatusID = Convert.ToInt32(ddl_approvedtype.SelectedValue == "" ? "0" : ddl_approvedtype.SelectedValue);
                objConAppr.HospitalID = LogData.HospitalID;
                objConAppr.EmployeeID = LogData.EmployeeID;
                objConAppr.FinancialYearID = LogData.FinancialYearID;
                objConAppr.ActionType = Enumaction.Insert;

                int result = objBO.UpdateCondemnApptrovedItem(objConAppr);
                if (result == 1)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage, msg, 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_from.Text = "";
            txt_To.Text = "";
            txt_condemnrequestno.Text = "";
            ddlcondemnstatus.SelectedIndex = 0;
            gvCondemnRequest.DataSource = null;
            gvCondemnRequest.DataBind();
            gvCondemnRequest.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_condemndate.Text = "";          
            gvCondemnDetail.DataSource = null;
            gvCondemnDetail.DataBind();
            gvCondemnDetail.Visible = false;
            lblmessage2.Visible = false;
            divmsg2.Visible = false;
            div3.Visible = false;
            lblresult1.Visible = false;           
            btnprint.Attributes["disabled"] = "disabled";
            txt_CondemnNumber.Text = "";
            txt_GrandTotalCondemnQty.Text = "";
        }
     
    }
}