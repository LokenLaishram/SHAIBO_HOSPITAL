using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;
using Mediqura.Utility;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;

namespace Mediqura.Web.MedStore
{
    public partial class PurchaseApproval : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddl_Status.SelectedValue = "1";
                BindGridTab1(1);
                txtTab2_RequisitionNo.Attributes["disabled"] = "disabled";
                txtTab2_ReqType.Attributes["disabled"] = "disabled";
                txtTab2_Reqby.Attributes["disabled"] = "disabled";
                txtTab2_Reqdate.Attributes["disabled"] = "disabled";
                btnTab2Save.Attributes["disabled"] = "disabled";
                btnTab2Print.Attributes["disabled"] = "disabled";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetRQNumberAuto(string prefixText, int count, string contextKey)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            List<PurchaseRequisitionData> getResult = new List<PurchaseRequisitionData>();
            ObjPurReqData.RQNumber = prefixText;
            getResult = ObjPurReqBO.GetRQNumberAuto(ObjPurReqData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RQNumber.ToString());
            }
            return list;
        }
        protected void txt_RequisitionNo_TextChanged(object sender, EventArgs e)
        {
            BindGridTab1(1);
        }
        protected void ddl_RequisitionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridTab1(1);
        }
        protected void ddl_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridTab1(1);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindGridTab1(1);
        }
        protected void BindGridTab1(int page)
        {
            try
            {
                List<PurchaseRequisitionData> ObjPurReqList = GetPurchaseRequisitionListForApproval(page);
                if (ObjPurReqList.Count > 0)
                {

                    GvRequisitionList.VirtualItemCount = ObjPurReqList[0].MaximumRows;//total item is required for custom paging
                    GvRequisitionList.PageIndex = page - 1;
                    GvRequisitionList.DataSource = ObjPurReqList;
                    GvRequisitionList.DataBind();
                    GvRequisitionList.Visible = true;
                    Messagealert_.ShowMessage(lblResult, "Total:" + ObjPurReqList[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;

                }
                else
                {
                    GvRequisitionList.DataSource = null;
                    GvRequisitionList.DataBind();
                    GvRequisitionList.Visible = true;
                    lblResult.Visible = false;
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
        public List<PurchaseRequisitionData> GetPurchaseRequisitionListForApproval(int curIndex)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ObjPurReqData.RQNumber = txt_RequisitionNo.Text == "" ? "0" : txt_RequisitionNo.Text.Trim();
            DateTime From = txt_DateFrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_DateFrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_DateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_DateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjPurReqData.DateFrom = From;
            ObjPurReqData.DateTo = To;
            ObjPurReqData.PurchaseRequisitionTypeID = Convert.ToInt32(ddl_RequisitionType.SelectedValue == "" ? "0" : ddl_RequisitionType.SelectedValue);
            ObjPurReqData.RQStatusID = Convert.ToInt32(ddl_Status.SelectedValue == "" ? "0" : ddl_Status.SelectedValue);
            return ObjPurReqBO.GetPurchaseRequisitionListForApproval(ObjPurReqData);
        }

        protected void GvRequisitionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {
                    divmsg2.Visible = false;
                    lblmessageTab2.Text = "";
                    PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
                    PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvRequisitionList.Rows[i];
                    Label StatusID = (Label)gr.Cells[0].FindControl("lblStatusID");
                    LinkButton RQNumber = (LinkButton)gr.Cells[0].FindControl("lnkRequisitionNo");
                    ObjPurReqData.RQNumber = RQNumber.Text;
                    List<PurchaseRequisitionData> List = new List<PurchaseRequisitionData>();
                    List = ObjPurReqBO.GetPurReqListByRQNumber(ObjPurReqData);
                    if (List.Count > 0)
                    {
                        txtTab2_RequisitionNo.Text = List[0].RQNumber.ToString();
                        txtTab2_ReqType.Text = List[0].PurchaseRequisitionTypeName.ToString();
                        txtTab2_Reqdate.Text = List[0].RequestedDate.ToString();
                        txtTab2_Reqby.Text = List[0].RequestedBy.ToString();
                        lblTab2_StatusID.Text = StatusID.Text;
                        tabcontainerpurchaseapproval.ActiveTabIndex = 1;
                        GvRequisitionApproval.DataSource = List;
                        GvRequisitionApproval.DataBind();
                        GvRequisitionApproval.Visible = true;                                                  
                        Messagealert_.ShowMessage(lblResultTab2, "Total: " + List[0].MaximumRows.ToString() + " Record(s) found.", 1);
                        div2.Visible = true;
                        div2.Attributes["class"] = "SucessAlert";
                    }
                    else
                    {
                        GvRequisitionApproval.DataSource = null;
                        GvRequisitionApproval.DataBind();
                        GvRequisitionApproval.Visible = true;
                        lblResultTab2.Visible = false;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                return;
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ResetTab1();
        }

        protected void ResetTab1()
        {
            txt_RequisitionNo.Text = "";
            txt_DateFrom.Text = "";
            txt_DateTo.Text = "";
            ddl_RequisitionType.SelectedValue = "0";
            ddl_Status.SelectedValue = "0";
            GvRequisitionList.DataSource = null;
            GvRequisitionList.DataBind();
            GvRequisitionList.Visible = false;
            divmsg1.Visible = false;
            lblmessage.Text = "";
            divmsg3.Visible = false;
            lblResult.Text = "";
        }
      
        //---------------------------------End Tab 1----------------------------------
        //---------------------------------Start Tab 2----------------------------------
        
        protected void GvRequisitionApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblReqQnty = (Label)e.Row.FindControl("lblTab2RequisitionQuantity");
                TextBox txtAprvQnty = (TextBox)e.Row.FindControl("txtTab2ApprovedQuantity");

                if (lblTab2_StatusID.Text == "1")
                {
                    txtAprvQnty.Text = lblReqQnty.Text;
                    ddlTab2_Status.Attributes.Remove("disabled");  
                    btnTab2Save.Attributes.Remove("disabled");
                    btnTab2Print.Attributes.Remove("disabled"); 
                }
                else if (lblTab2_StatusID.Text == "3")
                {                     
                    ddlTab2_Status.SelectedValue = "3";
                    ddlTab2_Status.Attributes["disabled"] = "disabled"; 
                    btnTab2Save.Attributes["disabled"] = "disabled";
                    btnTab2Print.Attributes.Remove("disabled");
                }
                else
                {
                    ddlTab2_Status.SelectedValue = "2";
                    ddlTab2_Status.Attributes["disabled"] = "disabled";
                    btnTab2Save.Attributes["disabled"] = "disabled";
                    btnTab2Print.Attributes.Remove("disabled");
                }
            }
        }

        protected void btnTab2Save_Click(object sender, EventArgs e)
        {   
            if (GvRequisitionApproval.Rows.Count != 0)
            {
                List<PurchaseRequisitionData> PurchaseApprovedList = new List<PurchaseRequisitionData>();
                PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
                PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();

                try
                {
                    foreach (GridViewRow row in GvRequisitionApproval.Rows)
                    {
                        Label ItemID = (Label)GvRequisitionApproval.Rows[row.RowIndex].Cells[0].FindControl("lblTab2ItemID");
                        Label RQOuantity = (Label)GvRequisitionApproval.Rows[row.RowIndex].Cells[0].FindControl("lblTab2RequisitionQuantity");
                        TextBox ApprovedQuantity = (TextBox)GvRequisitionApproval.Rows[row.RowIndex].Cells[0].FindControl("txtTab2ApprovedQuantity");
                        TextBox Remark = (TextBox)GvRequisitionApproval.Rows[row.RowIndex].Cells[0].FindControl("txtTab2Remark");

                        if (ApprovedQuantity.Text == "")
                        {
                            ApprovedQuantity.BackColor = System.Drawing.Color.Red;
                            ApprovedQuantity.Focus();
                            Messagealert_.ShowMessage(lblmessageTab2, "Please enter Approve Quantity. ", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else if (Convert.ToInt32(RQOuantity.Text) < Convert.ToInt32(ApprovedQuantity.Text == "" ? "0" : ApprovedQuantity.Text))
                        {
                            ApprovedQuantity.ForeColor = System.Drawing.Color.Red;
                            ApprovedQuantity.Focus();
                            Messagealert_.ShowMessage(lblmessageTab2, "Approved quantity should not be greater than Requisition quantity. ", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else if (Convert.ToInt32(RQOuantity.Text) != Convert.ToInt32(ApprovedQuantity.Text) && Remark.Text == "")
                        {
                            Remark.BackColor = System.Drawing.Color.Red;
                            Remark.Focus();
                            Messagealert_.ShowMessage(lblmessageTab2, "Please enter Remark", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                            return;
                        }

                        PurchaseRequisitionData ObjPurReqList = new PurchaseRequisitionData();
                        ObjPurReqList.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                        ObjPurReqList.TotalApprovedQuantity = Convert.ToInt32(ApprovedQuantity.Text == "" ? "0" : ApprovedQuantity.Text);
                        if (ApprovedQuantity.Text == "" || ApprovedQuantity.Text == "0")
                        {
                            ObjPurReqList.ItemStatusID = 3;
                            ObjPurReqList.ItemStatusName = "Rejected";
                        }
                        else
                        {
                            ObjPurReqList.ItemStatusID = 2;
                            ObjPurReqList.ItemStatusName = "Approved";
                        }
                        ObjPurReqList.Remark = Remark.Text.Trim();
                        ObjPurReqData.RQNumber = txtTab2_RequisitionNo.Text.Trim();
                        ObjPurReqData.RQStatusID = Convert.ToInt32(ddlTab2_Status.SelectedValue);
                        ObjPurReqData.RQStatusName = ddlTab2_Status.SelectedItem.Text;
                        ObjPurReqData.EmployeeID = LogData.EmployeeID;
                        ObjPurReqData.AddedBy = LogData.UserName;
                        ObjPurReqData.HospitalID = LogData.HospitalID;
                        ObjPurReqData.IsActive = LogData.IsActive;
                        ObjPurReqData.IPaddress = LogData.IPaddress;
                        ObjPurReqData.FinancialYearID = LogData.FinancialYearID;
                        ObjPurReqData.ActionType = Enumaction.Update;

                        PurchaseApprovedList.Add(ObjPurReqList);
                    }
                    ObjPurReqData.XMLData = XmlConvertor.PurchaseApproveDataToXML(PurchaseApprovedList).ToString();
                    if (ddlTab2_Status.SelectedValue == "0")
                    {
                        Messagealert_.ShowMessage(lblmessageTab2, "Please select status", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        int result = ObjPurReqBO.UpdatePurchaseApproveList(ObjPurReqData);
                        if (result == 1)
                        {
                            ddlTab2_Status.Attributes["disabled"] = "disabled";
                            btnTab2Save.Attributes["disabled"] = "disabled";

                            lblmessageTab2.Visible = true;
                            Messagealert_.ShowMessage(lblmessageTab2, "save", 1);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "SucessAlert";
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage, "Error", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                        }
                    }
                }
                catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    lblmessageTab2.Text = ExceptionMessage.GetMessage(ex);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                }
            }
        }

        protected void btnTab2Reset_Click(object sender, EventArgs e)
        {
            ResetTab2();
        } 
        protected void ResetTab2()
        {
            txtTab2_RequisitionNo.Text = "";
            txtTab2_ReqType.Text = "";
            txtTab2_Reqdate.Text = "";
            txtTab2_Reqby.Text = "";
            ddlTab2_Status.SelectedValue = "0";
            btnTab2Save.Attributes["disabled"] = "disabled";
            btnTab2Print.Attributes["disabled"] = "disabled";
            divmsg2.Visible = false;
            lblmessageTab2.Text = "";
            div2.Visible = false;
            lblResultTab2.Text = "";
            GvRequisitionApproval.DataSource = null;
            GvRequisitionApproval.DataBind();
            GvRequisitionApproval.Visible = false;
        }

       
        //---------------------------------End Tab 2----------------------------------
    }
}