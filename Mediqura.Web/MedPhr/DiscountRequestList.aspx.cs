using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.MedStore;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedStore;
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

namespace Mediqura.Web.MedPhr
{
    public partial class DiscountRequestList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTab2_ApproveAmt.Attributes["disabled"] = "disabled";
                ddlTab2_Status.Attributes["disabled"] = "disabled";
                txtTab2_Remark.Attributes["disabled"] = "disabled";
                btnTab2Save.Attributes["disabled"] = "disabled";
                btnTab2Print.Attributes["disabled"] = "disabled";
                ddl_Status.SelectedValue = "1";
                BindGridTab1(1);
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetRQNumberAuto(string prefixText, int count, string contextKey)
        {
            StockGRNData ObjDisReqList = new StockGRNData();
            StockGRNBO ObjDisReqBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            ObjDisReqList.ReqNo = prefixText;
            getResult = ObjDisReqBO.GetRQNumberAuto(ObjDisReqList);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ReqNo.ToString());
            }
            return list;
        }
        protected void txt_RequestNo_TextChanged(object sender, EventArgs e)
        {
            BindGridTab1(1);
        }

        protected void ddl_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridTab1(1);
        }
        protected void ddl_RequestType_SelectedIndexChanged(object sender, EventArgs e)
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
                List<StockGRNData> ObjDisReqList = GetDiscountRequestList(page);
                if (ObjDisReqList.Count > 0)
                {
                    GvDiscountRequestList.VirtualItemCount = ObjDisReqList[0].MaximumRows;//total item is required for custom paging
                    GvDiscountRequestList.PageIndex = page - 1;
                    GvDiscountRequestList.DataSource = ObjDisReqList;
                    GvDiscountRequestList.DataBind();
                    GvDiscountRequestList.Visible = true;
                    Messagealert_.ShowMessage(lblResult, "Total:" + ObjDisReqList[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;

                }
                else
                {
                    GvDiscountRequestList.DataSource = null;
                    GvDiscountRequestList.DataBind();
                    GvDiscountRequestList.Visible = true;
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
        public List<StockGRNData> GetDiscountRequestList(int curIndex)
        {
            StockGRNData ObjDisReqData = new StockGRNData();
            StockGRNBO ObjDisReqBO = new StockGRNBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ObjDisReqData.ReqNo = txt_RequestNo.Text == "" ? "0" : txt_RequestNo.Text.Trim();
            DateTime From = txt_DateFrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_DateFrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_DateTo.Text.Trim() == "" ? GlobalConstant.MaxSQLDateTime : DateTime.Parse(txt_DateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjDisReqData.DateFrom = From;
            ObjDisReqData.DateTo = To;
            ObjDisReqData.PatientTypeID = Convert.ToInt32(ddl_PatientType.SelectedValue == "" ? "0" : ddl_PatientType.SelectedValue);
            ObjDisReqData.StatusID = Convert.ToInt32(ddl_Status.SelectedValue == "" ? "0" : ddl_Status.SelectedValue);
            ObjDisReqData.RequestTypeID = Convert.ToInt32(ddl_RequestType.SelectedValue == "" ? "0" : ddl_RequestType.SelectedValue);
            return ObjDisReqBO.GetDiscountRequestList(ObjDisReqData);
        }
        protected void GvDiscountRequestList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridTab1(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ResetTab1();
        }

        protected void ResetTab1()
        {
            txt_RequestNo.Text = "";
            txt_DateFrom.Text = "";
            txt_DateTo.Text = "";
            ddl_PatientType.SelectedValue = "0";
            ddl_Status.SelectedValue = "0";
            GvDiscountRequestList.DataSource = null;
            GvDiscountRequestList.DataBind();
            GvDiscountRequestList.Visible = false;
            divmsg1.Visible = false;
            lblmessage.Text = "";
            divmsg3.Visible = false;
            lblResult.Text = "";
        }

        protected void GvDiscountRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblStatus = (Label)e.Row.FindControl("lblStatusID");
                Label lblReqAmt = (Label)e.Row.FindControl("lblRequestAmount");

                lblReqAmt.Text = Commonfunction.Getrounding(lblReqAmt.Text);

                if (LblStatus.Text == "1")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.FromName("#fec337");
                }
                if (LblStatus.Text == "2")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.FromName("#63d590");
                }
                if (LblStatus.Text == "3")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.FromName("#ee4e42");
                }
                if (LblStatus.Text == "4")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Aqua;
                }
            }
        }

        protected void GvDiscountRequestList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {
                    divmsg2.Visible = false;
                    lblmessageTab2.Text = "";
                    StockGRNData ObjDisReqData = new StockGRNData();
                    StockGRNBO ObjDisReqBO = new StockGRNBO();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDiscountRequestList.Rows[i];
                    Label StatusID = (Label)gr.Cells[0].FindControl("lblStatusID");
                    LinkButton RQNumber = (LinkButton)gr.Cells[0].FindControl("lnkRequestNo");
                    ObjDisReqData.ReqNo = RQNumber.Text;
                    List<StockGRNData> List = new List<StockGRNData>();
                    List = ObjDisReqBO.GetRequestDetailListByRQNumber(ObjDisReqData);
                    if (List.Count > 0)
                    {
                        lblTab2_RequestNo.Text = List[0].ReqNo.ToString();
                        lblTab2_RequestedBy.Text = List[0].RequestedBy.ToString();
                        lblTab2_RequestedDate.Text = List[0].RequestedDate.ToString();
                        lblTab2_StatusID.Text = StatusID.Text;
                        lblTab2_RequestRemark.Text = List[0].Remarks.ToString();
                        lblTab2_TotalBillAmt.Text = Commonfunction.Getrounding(List[0].TotalBillAmount.ToString());
                        lblTab2_RequestAmt.Text = Commonfunction.Getrounding(List[0].RequestedAmount.ToString());
                        txtTab2_ApproveAmt.Text = Commonfunction.Getrounding(List[0].ApprovedAmount.ToString());
                        lblTab1_RequestTypeID.Text = List[0].RequestTypeID.ToString();
                        lblTab1_PatientTypeID.Text = List[0].PatientTypeID.ToString();
                        if (StatusID.Text == "2" || StatusID.Text == "3" || StatusID.Text == "4")
                        {
                            if (StatusID.Text == "1")
                            {
                                txtTab2_Remark.Text = "";
                            }
                            else if (StatusID.Text == "2")
                            {
                                txtTab2_Remark.Text = List[0].RemarksApproved.ToString();
                            }
                            else if (StatusID.Text == "3")
                            {
                                txtTab2_Remark.Text = List[0].RemarksRejected.ToString();
                            }
                            else if (StatusID.Text == "4")
                            {
                                txtTab2_Remark.Text = List[0].RemarksApproved.ToString();
                            }
                            else
                            {
                                txtTab2_Remark.Text = "";
                            }
                            txtTab2_Remark.Text = List[0].RemarksApproved.ToString();
                            txtTab2_ApproveAmt.Attributes["disabled"] = "disabled";
                            ddlTab2_Status.Attributes["disabled"] = "disabled";
                            txtTab2_Remark.Attributes["disabled"] = "disabled";
                            btnTab2Save.Attributes["disabled"] = "disabled";
                            btnTab2Print.Attributes.Remove("disabled");
                            ddlTab2_Status.SelectedValue = StatusID.Text;
                        }
                        else
                        {
                            txtTab2_ApproveAmt.Attributes.Remove("disabled");
                            ddlTab2_Status.Attributes.Remove("disabled");
                            txtTab2_Remark.Attributes.Remove("disabled");
                            btnTab2Save.Attributes.Remove("disabled");
                            btnTab2Print.Attributes["disabled"] = "disabled";
                            ddlTab2_Status.SelectedValue = "0";
                        }

                        tabcontainerDiscountRequestList.ActiveTabIndex = 1;
                        if (lblTab1_RequestTypeID.Text == "1" && lblTab1_PatientTypeID.Text == "1")
                        {
                            GvRequestDetailList.DataSource = List;
                            GvRequestDetailList.DataBind();
                            GvRequestDetailList.Visible = true;
                            Messagealert_.ShowMessage(lblResultTab2, "Total: " + List[0].MaximumRows.ToString() + " Record(s) found.", 1);
                            div2.Visible = true;
                            div2.Attributes["class"] = "SucessAlert";
                        }
                        else
                        {
                            GvRequestDetailList.DataSource = null;
                            GvRequestDetailList.DataBind();
                            GvRequestDetailList.Visible = false;
                            lblResultTab2.Visible = false;
                            div2.Visible = false;
                        }
                    }
                    else
                    {
                        GvRequestDetailList.DataSource = null;
                        GvRequestDetailList.DataBind();
                        GvRequestDetailList.Visible = true;
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

        //--------------------------------END TAB 1---------------------------------
        //--------------------------------START TAB 2-------------------------------
        protected void GvRequestDetailList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNetCharge = (Label)e.Row.FindControl("lblTab2NetCharge");

                lblNetCharge.Text = Commonfunction.Getrounding(lblNetCharge.Text);

            }
        }
        protected void txtTab2_ApproveAmt_TextChanged(object sender, EventArgs e)
        {
            string AppAmt = txtTab2_ApproveAmt.Text;
            if (!string.IsNullOrEmpty(AppAmt))
            {
                if (Convert.ToDecimal(lblTab2_RequestAmt.Text) < Convert.ToDecimal(txtTab2_ApproveAmt.Text))
                {
                    txtTab2_ApproveAmt.ForeColor = System.Drawing.Color.FromName("#ee4e42");
                    txtTab2_ApproveAmt.Focus();
                    Messagealert_.ShowMessage(lblmessageTab2, "Approve Amount shouldn't be greater than Request Amount.", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    ddlTab2_Status.Focus();
                }
            }
            else
            {
                txtTab2_ApproveAmt.BackColor = System.Drawing.Color.FromName("#ee4e42");
                txtTab2_ApproveAmt.Focus();
                Messagealert_.ShowMessage(lblmessageTab2, "Please enter Approve Amount. ", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
        }
        protected void ddlTab2_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtTab2_ApproveAmt.Text == "" ? "0" : txtTab2_ApproveAmt.Text) > 0 && ddlTab2_Status.SelectedValue == "3")
            {
                ddlTab2_Status.BackColor = System.Drawing.Color.FromName("#ee4e42");
                ddlTab2_Status.Focus();
                Messagealert_.ShowMessage(lblmessageTab2, "Please select Approved. ", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessageTab2.Visible = false;
                divmsg2.Visible = false;
            }
            if (Convert.ToDecimal(txtTab2_ApproveAmt.Text == "" ? "0" : txtTab2_ApproveAmt.Text) == 0 && ddlTab2_Status.SelectedValue == "2")
            {
                ddlTab2_Status.BackColor = System.Drawing.Color.FromName("#ee4e42");
                ddlTab2_Status.Focus();
                Messagealert_.ShowMessage(lblmessageTab2, "Please select Rejected. ", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessageTab2.Visible = false;
                divmsg2.Visible = false;
            }
            if (txtTab2_ApproveAmt.Text.Trim() == "" && ddlTab2_Status.SelectedValue == "2")
            {
                ddlTab2_Status.BackColor = System.Drawing.Color.FromName("#ee4e42");
                ddlTab2_Status.Focus();
                Messagealert_.ShowMessage(lblmessageTab2, "Please select Rejected. ", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessageTab2.Visible = false;
                divmsg2.Visible = false;
            }
        }
        protected void btnTab2Save_Click(object sender, EventArgs e)
        {
            try
            {
                string AppAmt = txtTab2_ApproveAmt.Text;
                if (!string.IsNullOrEmpty(AppAmt))
                {
                    if (Convert.ToDecimal(lblTab2_RequestAmt.Text == "" ? "0" : lblTab2_RequestAmt.Text) < Convert.ToDecimal(txtTab2_ApproveAmt.Text == "" ? "0" : txtTab2_ApproveAmt.Text))
                    {
                        txtTab2_ApproveAmt.ForeColor = System.Drawing.Color.FromName("#ee4e42");
                        txtTab2_ApproveAmt.Focus();
                        Messagealert_.ShowMessage(lblmessageTab2, "Approve Amount shouldn't be greater than Request Amount.", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessageTab2.Visible = false;
                        divmsg2.Visible = false;
                    }
                }
                if (Convert.ToDecimal(txtTab2_ApproveAmt.Text.Trim() == "" ? "0.0" : txtTab2_ApproveAmt.Text.Trim()) <= 0 && ddlTab2_Status.SelectedValue == "2")
                {
                    txtTab2_ApproveAmt.BackColor = System.Drawing.Color.FromName("#ee4e42");
                    txtTab2_ApproveAmt.Focus();
                    Messagealert_.ShowMessage(lblmessageTab2, "Please enter Approve Amount. ", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessageTab2.Visible = false;
                    divmsg2.Visible = false;
                }
                if (Convert.ToDecimal(txtTab2_ApproveAmt.Text.Trim() == "" ? "0.0" : txtTab2_ApproveAmt.Text.Trim()) > 0 && ddlTab2_Status.SelectedValue == "3")
                {
                    ddlTab2_Status.BackColor = System.Drawing.Color.FromName("#ee4e42");
                    ddlTab2_Status.Focus();
                    Messagealert_.ShowMessage(lblmessageTab2, "Please select Approved. ", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessageTab2.Visible = false;
                    divmsg2.Visible = false;
                }
                if (ddlTab2_Status.SelectedValue == "0")
                {
                    ddlTab2_Status.BackColor = System.Drawing.Color.FromName("#ee4e42");
                    ddlTab2_Status.Focus();
                    Messagealert_.ShowMessage(lblmessageTab2, "Please enter Status. ", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessageTab2.Visible = false;
                    divmsg2.Visible = false;
                }
                if (txtTab2_Remark.Text.Trim() == "")
                {
                    txtTab2_Remark.BackColor = System.Drawing.Color.FromName("#ee4e42");
                    txtTab2_Remark.Focus();
                    Messagealert_.ShowMessage(lblmessageTab2, "Please enter remark. ", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                StockGRNData ObjDisReqData = new StockGRNData();
                StockGRNBO ObjDisReqBO = new StockGRNBO();
                ObjDisReqData.ReqNo = lblTab2_RequestNo.Text.Trim();
                ObjDisReqData.ApprovedAmount = Convert.ToDecimal(txtTab2_ApproveAmt.Text.Trim());
                ObjDisReqData.StatusID = Convert.ToInt32(ddlTab2_Status.SelectedValue);
                ObjDisReqData.Remarks = txtTab2_Remark.Text.Trim();
                ObjDisReqData.EmployeeID = LogData.EmployeeID;

                int result = ObjDisReqBO.UpdateDiscountApprovedList(ObjDisReqData);
                if (result == 1)
                {
                    txtTab2_ApproveAmt.Attributes["disabled"] = "disabled";
                    ddlTab2_Status.Attributes["disabled"] = "disabled";
                    txtTab2_Remark.Attributes["disabled"] = "disabled";
                    btnTab2Save.Attributes["disabled"] = "disabled";
                    btnTab2Print.Attributes.Remove("disabled");

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
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                return;
            }
        }

        protected void btnTab2Reset_Click(object sender, EventArgs e)
        {
            ResetTab2();
        }
        protected void ResetTab2()
        {
            lblTab2_RequestNo.Text = "";
            lblTab2_RequestedBy.Text = "";
            lblTab2_RequestedDate.Text = "";
            lblTab2_RequestRemark.Text = "";
            lblTab2_TotalBillAmt.Text = "";
            lblTab2_RequestAmt.Text = "";
            txtTab2_ApproveAmt.Text = "";
            ddlTab2_Status.SelectedValue = "0";
            txtTab2_Remark.Text = "";

            btnTab2Save.Attributes["disabled"] = "disabled";
            btnTab2Print.Attributes["disabled"] = "disabled";
            divmsg2.Visible = false;
            lblmessageTab2.Text = "";
            div2.Visible = false;
            lblResultTab2.Text = "";
            GvRequestDetailList.DataSource = null;
            GvRequestDetailList.DataBind();
            GvRequestDetailList.Visible = false;
        }



        //--------------------------------END TAB 2---------------------------------

    }
}