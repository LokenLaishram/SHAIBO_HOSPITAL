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
    public partial class PurchaseOrder : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddl_Status.SelectedValue = "1";
                BindGridTab1(1);
                BindDdl();
                txtTab2_ReqNo.Attributes["disabled"] = "disabled";
                txtTab2_ReqDate.Attributes["disabled"] = "disabled";
                txtTab2_ApprovedBy.Attributes["disabled"] = "disabled";
                txtTab2_ApprovedDate.Attributes["disabled"] = "disabled";
                txtTab2_PONumber.Attributes["disabled"] = "disabled";
                btnTab2Save.Attributes["disabled"] = "disabled";
                btnTab2Print.Attributes["disabled"] = "disabled";
                btnTab2Reset.Attributes["disabled"] = "disabled";
            }
        }
        private void BindDdl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlTab2_Supplier, mstlookup.GetLookupsList(LookupName.GenSupplier));
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPONumberAuto(string prefixText, int count, string contextKey)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            List<PurchaseRequisitionData> getResult = new List<PurchaseRequisitionData>();
            ObjPurReqData.PONumber = prefixText;
            getResult = ObjPurReqBO.GetPONumberAuto(ObjPurReqData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PONumber.ToString());
            }
            return list;
        }
        protected void txt_PONumber_TextChanged(object sender, EventArgs e)
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
                List<PurchaseRequisitionData> ObjPurReqList = GetPurchaseRequisitionListForPO(page);
                if (ObjPurReqList.Count > 0)
                {

                    GvPOGenerationList.VirtualItemCount = ObjPurReqList[0].MaximumRows;//total item is required for custom paging
                    GvPOGenerationList.PageIndex = page - 1;
                    GvPOGenerationList.DataSource = ObjPurReqList;
                    GvPOGenerationList.DataBind();
                    GvPOGenerationList.Visible = true;
                    Messagealert_.ShowMessage(lblResult, "Total:" + ObjPurReqList[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;

                }
                else
                {
                    GvPOGenerationList.DataSource = null;
                    GvPOGenerationList.DataBind();
                    GvPOGenerationList.Visible = true;
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
        public List<PurchaseRequisitionData> GetPurchaseRequisitionListForPO(int curIndex)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ObjPurReqData.RQNumber = txt_RequisitionNo.Text == "" ? "0" : txt_RequisitionNo.Text.Trim();
            ObjPurReqData.PONumber = txt_PONumber.Text == "" ? "0" : txt_PONumber.Text.Trim();
            DateTime From = txt_DateFrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_DateFrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_DateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_DateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjPurReqData.DateFrom = From;
            ObjPurReqData.DateTo = To;
            ObjPurReqData.POStatusID = Convert.ToInt32(ddl_Status.SelectedValue == "" ? "0" : ddl_Status.SelectedValue);
            return ObjPurReqBO.GetPurchaseRequisitionListForPO(ObjPurReqData);
        }
        protected void GvPOGenerationList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton LnkBtnRQ = (LinkButton)e.Row.FindControl("lnkRequisitionNo");
                Label LblRQ = (Label)e.Row.FindControl("lblRequisitionNo");
                Label lblStatusID =(Label)e.Row.FindControl("lblStatusID");
                if (lblStatusID.Text == "2")
                {
                    LblRQ.Visible = true;
                    LnkBtnRQ.Visible = false;
                }
                else if (lblStatusID.Text == "1")
                {
                    LblRQ.Visible = false;
                    LnkBtnRQ.Visible = true;
                }
            }
        }
        protected void GvPOGenerationList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {
                    PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
                    PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPOGenerationList.Rows[i];
                    LinkButton RQNumber = (LinkButton)gr.Cells[0].FindControl("lnkRequisitionNo");
                    ObjPurReqData.RQNumber = RQNumber.Text;
                    List<PurchaseRequisitionData> List = new List<PurchaseRequisitionData>();
                    List = ObjPurReqBO.GetPurReqListByRQNumberForPO(ObjPurReqData);
                    if (List.Count > 0)
                    {
                        txtTab2_ReqNo.Text = List[0].RQNumber.ToString();
                        txtTab2_ReqDate.Text = List[0].RequestedDate.ToString();
                        txtTab2_ApprovedBy.Text = List[0].ApprovedBy.ToString();
                        txtTab2_ApprovedDate.Text = List[0].ApprovedDate.ToString();
                        TabContainerPurchasePOGenerator.ActiveTabIndex = 1;
                        GvPurchaseOrder.DataSource = List;
                        GvPurchaseOrder.DataBind();
                        GvPurchaseOrder.Visible = true;
                        ddlTab2_Supplier.Attributes.Remove("disabled");
                        btnTab2Save.Attributes.Remove("disabled");
                        Messagealert_.ShowMessage(lblResultTab2, "Total: " + List[0].MaximumRows.ToString() + " Record(s) found.", 1);
                        div2.Visible = true;
                        div2.Attributes["class"] = "SucessAlert";
                    }
                    else
                    {
                        GvPurchaseOrder.DataSource = null;
                        GvPurchaseOrder.DataBind();
                        GvPurchaseOrder.Visible = true;
                        lblResultTab2.Visible = false;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessageTab2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
                return;
            }
        }


        //---------------------------------End Tab 1----------------------------------
        protected void btnTab2Save_Click(object sender, EventArgs e)
        {
            if (ddlTab2_Supplier.SelectedValue == "0")
            {
                Messagealert_.ShowMessage(lblmessageTab2, "Please select Supplier", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                if (GvPurchaseOrder.Rows.Count != 0)
                {
                    List<PurchaseRequisitionData> PurchaseApprovedList = new List<PurchaseRequisitionData>();
                    PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
                    PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();

                    try
                    {
                        foreach (GridViewRow row in GvPurchaseOrder.Rows)
                        {
                            Label ItemID = (Label)GvPurchaseOrder.Rows[row.RowIndex].Cells[0].FindControl("lblTab2ItemID");

                            PurchaseRequisitionData ObjPurReqList = new PurchaseRequisitionData();
                            ObjPurReqList.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                            ObjPurReqData.RQNumber = txtTab2_ReqNo.Text.Trim();
                            ObjPurReqData.SupplierID = Convert.ToInt32(ddlTab2_Supplier.SelectedValue);
                            ObjPurReqData.SupplierName = ddlTab2_Supplier.SelectedItem.Text;
                            ObjPurReqData.EmployeeID = LogData.EmployeeID;
                            ObjPurReqData.AddedBy = LogData.UserName;
                            ObjPurReqData.HospitalID = LogData.HospitalID;
                            ObjPurReqData.IsActive = LogData.IsActive;
                            ObjPurReqData.IPaddress = LogData.IPaddress;
                            ObjPurReqData.FinancialYearID = LogData.FinancialYearID;
                            ObjPurReqData.ActionType = Enumaction.Insert;

                            PurchaseApprovedList.Add(ObjPurReqList);
                        }
                        ObjPurReqData.XMLData = XmlConvertor.PurchaseOrderDataToXML(PurchaseApprovedList).ToString();

                        List<PurchaseRequisitionData> result = ObjPurReqBO.GeneratePurchaseOrderList(ObjPurReqData);
                        if (result.Count > 0)
                        {
                            txtTab2_PONumber.Text = result[0].PONumber;
                            ddlTab2_Supplier.Attributes["disabled"] = "disabled";
                            btnTab2Save.Attributes["disabled"] = "disabled";
                            btnTab2Print.Attributes.Remove("disabled");

                            lblmessageTab2.Visible = true;
                            Messagealert_.ShowMessage(lblmessageTab2, "Purchase Order Generated Sucessfully", 1);
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
                        lblmessageTab2.Text = ExceptionMessage.GetMessage(ex);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                    }
                }
            }
        }

 
        //---------------------------------Start Tab 2----------------------------------
    }
}