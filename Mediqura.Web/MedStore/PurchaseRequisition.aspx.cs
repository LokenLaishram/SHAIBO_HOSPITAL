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
    public partial class PurchaseRequisition : BasePage
    {
        static int rowcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["PurchaseRequisitionList"] = null;
                btnTab1Save.Attributes["disabled"] = "disabled";
                btnTab1Print.Attributes["disabled"] = "disabled";
            }
        }


        protected void ddl_RequisitionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_RequisitionType.SelectedValue == "2")
            {
                //lbl_FileAttch_Mandatory.Visible = true;
                ddl_RequisitionType.Attributes["disabled"] = "disabled";
            }
            else
            {
                //lbl_FileAttch_Mandatory.Visible = false;
                ddl_RequisitionType.Attributes["disabled"] = "disabled";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemNameAuto(string prefixText, int count, string contextKey)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            List<PurchaseRequisitionData> getResult = new List<PurchaseRequisitionData>();
            ObjPurReqData.ItemName = prefixText;
            getResult = ObjPurReqBO.GetItemNameAuto(ObjPurReqData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }

        protected void txt_ItemName_TextChanged(object sender, EventArgs e)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            List<PurchaseRequisitionData> getResult = new List<PurchaseRequisitionData>();
            bool IsNumeric = txt_ItemName.Text.All(char.IsDigit);
            if (IsNumeric == false)
            {
                if (txt_ItemName.Text.Contains(":"))
                {
                    bool IsItemNameNumeric = txt_ItemName.Text.Substring(txt_ItemName.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    ObjPurReqData.ItemID = IsItemNameNumeric ? Convert.ToInt32(txt_ItemName.Text.Contains(":") ? txt_ItemName.Text.Substring(txt_ItemName.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    txt_ItemName.Text = "";
                    txt_ItemName.Focus();
                    return;
                }
            }
            else
            {
                ObjPurReqData.ItemID = Convert.ToInt32(txt_ItemName.Text == "" ? "0" : txt_ItemName.Text);
            }
            getResult = ObjPurReqBO.GetUnitDescriptionByID(ObjPurReqData);
            if (getResult.Count > 0)
            {
                txt_Unit.Text = getResult[0].UnitDescription;
                lbl_HdnItemName.Text = getResult[0].ItemName;
                lbl_HdnAvailableQuantity.Text = getResult[0].TotalAvailableQuantity.ToString();
                txt_ItemName.Attributes["disabled"] = "disabled";
                txt_RequisitionQuantity.Focus();
            }
            else
            {
                ddl_RequisitionType.SelectedIndex = 0;
                txt_ItemName.Text = "";
                txt_Unit.Text = "";
                txt_RequisitionQuantity.Text = "";
            }
        }
        protected void txt_RequisitionQuantity_TextChanged(object sender, EventArgs e)
        {
            BindPurchaseList();
        }
        protected void BindPurchaseList()
        {
            if (txt_ItemName.Text != "")
            {
                PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
                var source = txt_ItemName.Text.Trim();
                if (source.Contains(":"))
                {
                    string ID = source.Substring(source.LastIndexOf(':') + 1);
                    ObjPurReqData.ItemID = Convert.ToInt32(ID);

                    // Check Duplicate data 
                    foreach (GridViewRow row in GvPurReqList.Rows)
                    {
                        IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);

                        Label ItemID = (Label)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                        if (Convert.ToInt32(ItemID.Text) == Convert.ToInt32(ID))
                        {
                            txt_ItemName.Attributes.Remove("disabled");
                            Messagealert_.ShowMessage(lblmessage, "Already added to the list", 0);
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                            txt_ItemName.Attributes["disabled"] = "disabled";
                        }
                    }
                }
                List<PurchaseRequisitionData> PurchaseRequisitionList = Session["PurchaseRequisitionList"] == null ? new List<PurchaseRequisitionData>() : (List<PurchaseRequisitionData>)Session["PurchaseRequisitionList"];
                PurchaseRequisitionData ObjPurReqList = new PurchaseRequisitionData();
                ObjPurReqData.ItemName = lbl_HdnItemName.Text.Trim();
                ObjPurReqData.UnitDescription = txt_Unit.Text.Trim();
                ObjPurReqData.TotalAvailableQuantity = Convert.ToInt32(lbl_HdnAvailableQuantity.Text.Trim());
                ObjPurReqData.PurchaseRequisitionTypeName = ddl_RequisitionType.SelectedItem.Text;
                ObjPurReqData.PurchaseRequisitionQuantity = Convert.ToInt32(txt_RequisitionQuantity.Text == "" ? "0" : txt_RequisitionQuantity.Text);
                ObjPurReqData.ProbableRate = 0;

                PurchaseRequisitionList.Add(ObjPurReqData);

                if (PurchaseRequisitionList.Count > 0)
                {
                    GvPurReqList.DataSource = PurchaseRequisitionList;
                    GvPurReqList.DataBind();
                    GvPurReqList.Visible = true;
                    BindTotalProbableRate();
                    Session["PurchaseRequisitionList"] = PurchaseRequisitionList;
                    txt_ItemName.Text = "";
                    txt_ItemName.Attributes.Remove("disabled");
                    txt_RequisitionQuantity.Text = "";
                    txt_Unit.Text = "";
                    lbl_HdnItemName.Text = "";
                    lbl_HdnAvailableQuantity.Text = "";
                    txt_ItemName.Focus();
                    btnTab1Save.Attributes.Remove("disabled");
                }
                else
                {
                    GvPurReqList.DataSource = null;
                    GvPurReqList.DataBind();
                    GvPurReqList.Visible = true;
                    btnTab1Save.Attributes["disabled"] = "disabled";
                }
            }

        }
        protected void btnTab1Add_Click(object sender, EventArgs e)
        {
            BindPurchaseList();
        }
        protected void GvPurReqList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //BindTotalProbableRate();
        }
        protected void txt_ProbableRate_TextChanged(object sender, EventArgs e)
        {
            List<PurchaseRequisitionData> PurchaseRequisitionList = Session["PurchaseRequisitionList"] == null ? new List<PurchaseRequisitionData>() : (List<PurchaseRequisitionData>)Session["PurchaseRequisitionList"];
            TextBox ProbableRate = (TextBox)sender;
            GridViewRow GvRow = (GridViewRow)ProbableRate.NamingContainer;
            rowcount = GvPurReqList.Rows.Count;
            int index = GvRow.RowIndex;
            if (rowcount == index + 1)
            {
                btnTab1Save.Focus();
            }
            else
            {
                GridViewRow NextRow = GvPurReqList.Rows[index + 1];
                TextBox ProRate = (TextBox)NextRow.Cells[0].FindControl("txt_ProbableRate");
                ProRate.Focus();
            }

            PurchaseRequisitionList[index].ProbableRate = Convert.ToDecimal(ProbableRate.Text.Trim());
            Session["PurchaseRequisitionList"] = PurchaseRequisitionList;
            BindTotalProbableRate();
        }
        protected void BindTotalProbableRate()
        {
            int total = 0;
            foreach (GridViewRow row in GvPurReqList.Rows)
            {
                TextBox ProbableRate = (TextBox)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("txt_ProbableRate");
                if (ProbableRate != null)
                {
                    total += Convert.ToInt32(ProbableRate.Text);
                    GvPurReqList.FooterRow.Cells[4].Text = "Total Probale Rate";
                    GvPurReqList.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                    GvPurReqList.FooterRow.Cells[5].Text = total.ToString();
                }
            }
        }
        protected void GvPurReqList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    List<PurchaseRequisitionData> PurchaseRequisitionList = Session["PurchaseRequisitionList"] == null ? new List<PurchaseRequisitionData>() : (List<PurchaseRequisitionData>)Session["PurchaseRequisitionList"];
                    PurchaseRequisitionList.RemoveAt(i);
                    Session["PurchaseRequisitionList"] = PurchaseRequisitionList;
                    GvPurReqList.DataSource = PurchaseRequisitionList;
                    GvPurReqList.DataBind();
                    BindTotalProbableRate();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }

        }

        protected void btnTab1Save_Click(object sender, EventArgs e)
        {
            if (ViewState["ID"] == null)
            {
                if (ddl_RequisitionType.SelectedValue == "0")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select purchase requisition type.", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if (GvPurReqList.Rows.Count != 0)
            {
                List<PurchaseRequisitionData> PurchaseRequisitionList = new List<PurchaseRequisitionData>();
                PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
                PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();

                try
                {
                    foreach (GridViewRow row in GvPurReqList.Rows)
                    {
                        IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                        Label ItemID = (Label)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                        Label ItemName = (Label)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemName");
                        Label AvailableQuantity = (Label)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_AvailableItem");
                        Label RequisitionQuantity = (Label)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_RQQuantity");
                        TextBox ProbableRate = (TextBox)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("txt_ProbableRate");

                        PurchaseRequisitionData ObjPurReqList = new PurchaseRequisitionData();
                        ObjPurReqList.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                        ObjPurReqList.ItemName = ItemName.Text.Trim() == "" ? null : ItemName.Text.Trim();
                        ObjPurReqList.TotalAvailableQuantity = Convert.ToInt32(AvailableQuantity.Text == "" ? "0" : AvailableQuantity.Text);
                        ObjPurReqList.PurchaseRequisitionQuantity = Convert.ToInt32(RequisitionQuantity.Text == "" ? "0" : RequisitionQuantity.Text);
                        if (ViewState["ID"] != null)
                        {
                            TextBox txtRequisitionQuantity = (TextBox)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("txt_RQQuantity");
                            ObjPurReqList.PurchaseRequisitionQuantity = Convert.ToInt32(txtRequisitionQuantity.Text == "" ? "0" : txtRequisitionQuantity.Text);
                        }
                        ObjPurReqList.ProbableRate = Convert.ToDecimal(ProbableRate.Text == "" ? "0" : ProbableRate.Text);
                        ObjPurReqData.PurchaseRequisitionTypeID = Convert.ToInt32(ddl_RequisitionType.SelectedValue);
                        ObjPurReqData.PurchaseRequisitionTypeName = ddl_RequisitionType.SelectedItem.Text;
                        ObjPurReqData.EmployeeID = LogData.EmployeeID;
                        ObjPurReqData.AddedBy = LogData.UserName;
                        ObjPurReqData.HospitalID = LogData.HospitalID;
                        ObjPurReqData.IsActive = LogData.IsActive;
                        ObjPurReqData.IPaddress = LogData.IPaddress;
                        ObjPurReqData.FinancialYearID = LogData.FinancialYearID;
                        ObjPurReqData.ActionType = Enumaction.Insert;

                        PurchaseRequisitionList.Add(ObjPurReqList);
                    }
                    ObjPurReqData.XMLData = XmlConvertor.PurchaseRequisitionDataToXML(PurchaseRequisitionList).ToString();
                    if (ViewState["ID"] == null)
                    {
                        ObjPurReqData.ActionType = Enumaction.Insert;
                        List<PurchaseRequisitionData> result = ObjPurReqBO.SavePurchaseRequisitionList(ObjPurReqData);
                        if (result.Count > 0)
                        {
                            Session.Remove("PurchaseRequisitionList");
                            GvPurReqList.DataSource = null;
                            GvPurReqList.DataBind();
                            GvPurReqList.Visible = false;
                            lbl_PurReqNo.Text = result[0].RQNumber;
                            ddl_RequisitionType.Attributes.Remove("disabled");
                            ddl_RequisitionType.SelectedValue = "0";
                            txt_ItemName.Text = "";
                            txt_ItemName.Attributes.Remove("disabled");
                            txt_RequisitionQuantity.Text = "";
                            txt_Unit.Text = "";
                            lbl_HdnItemName.Text = "";
                            lbl_HdnAvailableQuantity.Text = "";
                            btnTab1Save.Attributes["disabled"] = "disabled";
                            btnTab1Print.Attributes.Remove("disabled");

                            lblmessage.Visible = true;
                            Messagealert_.ShowMessage(lblmessage, "save", 1);
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

                    if (ViewState["ID"] != null)
                    {
                        if (LogData.UpdateEnable == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                            div1.Visible = true;
                            div1.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }

                        ObjPurReqData.ActionType = Enumaction.Update;
                        ObjPurReqData.RQNumber = lbl_PurReqNo.Text.Trim();
                        List<PurchaseRequisitionData> resultUpdate = ObjPurReqBO.UpdatePurchaseRequisitionList(ObjPurReqData);
                        if (resultUpdate.Count > 0)
                        {
                            Session.Remove("PurchaseRequisitionList");
                            GvPurReqList.DataSource = null;
                            GvPurReqList.DataBind();
                            GvPurReqList.Visible = false;
                            lbl_PurReqNo.Text = resultUpdate[0].RQNumber;
                            ddl_RequisitionType.Attributes.Remove("disabled");
                            ddl_RequisitionType.SelectedValue = "0";
                            txt_ItemName.Text = "";
                            txt_ItemName.Attributes.Remove("disabled");
                            txt_RequisitionQuantity.Text = "";
                            txt_Unit.Text = "";
                            lbl_HdnItemName.Text = "";
                            lbl_HdnAvailableQuantity.Text = "";
                            btnTab1Save.Attributes["disabled"] = "disabled";
                            btnTab1Print.Attributes.Remove("disabled");
                            lblmessage.Visible = true;
                            Messagealert_.ShowMessage(lblmessage, "update", 1);
                            div1.Visible = true;
                            div1.Attributes["class"] = "SucessAlert";
                            ViewState["ID"] = null;
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage, "Error", 0);
                            div1.Visible = true;
                            div1.Attributes["class"] = "FailAlert";
                        }
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
            else
            {
                btnTab1Save.Attributes["disabled"] = "disabled";
            }
        }

        protected void btnTab1Print_Click(object sender, EventArgs e)
        {

        }

        protected void btnTab1Reset_Click(object sender, EventArgs e)
        {
            Tab1Reset();
        }

        protected void Tab1Reset()
        {
            ddl_RequisitionType.Attributes.Remove("disabled");
            ddl_RequisitionType.SelectedValue = "0";
            txt_ItemName.Text = "";
            txt_ItemName.Attributes.Remove("disabled");
            txt_RequisitionQuantity.Text = "";
            txt_Unit.Text = "";
            lbl_HdnItemName.Text = "";
            lbl_HdnAvailableQuantity.Text = "";
            lbl_PurReqNo.Text = "";
            btnTab1Save.Attributes["disabled"] = "disabled";
            btnTab1Print.Attributes["disabled"] = "disabled";
            div1.Visible = false;
            lblmessage.Text = "";
            ViewState["ID"] = null;
            Session["PurchaseRequisitionList"] = null;
            GvPurReqList.DataSource = null;
            GvPurReqList.DataBind();
            GvPurReqList.Visible = false;
        }

        //------------------------------------- End Tab 1 ---------------------------------------
        //------------------------------------- Start Tab 2 ---------------------------------------
        protected void ddlTab2_PageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int size = 0;
            if (ddlTab2_PageSize.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlTab2_PageSize.SelectedItem.Value.ToString());
                GvRequisitionList.PageSize = size;
                BindGridTab2(1);
            }
        }
        protected void ddlTab2_RequisitionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridTab2(1);
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
        protected void txtTab2_RequisitionNo_TextChanged(object sender, EventArgs e)
        {
            BindGridTab2(1);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemNameTab2Auto(string prefixText, int count, string contextKey)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            List<PurchaseRequisitionData> getResult = new List<PurchaseRequisitionData>();
            ObjPurReqData.ItemName = prefixText;
            getResult = ObjPurReqBO.GetItemNameAuto(ObjPurReqData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        protected void txtTab2_ItemName_TextChanged(object sender, EventArgs e)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            List<PurchaseRequisitionData> getResult = new List<PurchaseRequisitionData>();
            bool IsNumeric = txtTab2_ItemName.Text.All(char.IsDigit);
            if (IsNumeric == false)
            {
                if (txtTab2_ItemName.Text.Contains(":"))
                {
                    bool IsItemNameNumeric = txtTab2_ItemName.Text.Substring(txtTab2_ItemName.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    ObjPurReqData.ItemID = IsItemNameNumeric ? Convert.ToInt32(txtTab2_ItemName.Text.Contains(":") ? txtTab2_ItemName.Text.Substring(txtTab2_ItemName.Text.LastIndexOf(':') + 1) : "0") : 0;
                    lblTab2_HdnItemID.Text = ObjPurReqData.ItemID.ToString();
                }
                else
                {
                    txtTab2_ItemName.Text = "";
                    txt_ItemName.Focus();
                    return;
                }
            }
            else
            {
                ObjPurReqData.ItemID = Convert.ToInt32(txtTab2_ItemName.Text == "" ? "0" : txtTab2_ItemName.Text);
                ObjPurReqData.ItemID = 0;
                lblTab2_HdnItemID.Text = "0";
            }
            BindGridTab2(1);
        }
        protected void ddlTab2_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridTab2(1);
        }
        protected void btnTab2Search_Click(object sender, EventArgs e)
        {
            BindGridTab2(1);
        }
        protected void BindGridTab2(int page)
        {
            try
            {
                List<PurchaseRequisitionData> ObjPurReqList = GetPurchaseRequisitionList(page);
                if (ObjPurReqList.Count > 0)
                {

                    GvRequisitionList.VirtualItemCount = ObjPurReqList[0].MaximumRows;//total item is required for custom paging
                    GvRequisitionList.PageIndex = page - 1;
                    GvRequisitionList.DataSource = ObjPurReqList;
                    GvRequisitionList.DataBind();
                    GvRequisitionList.Visible = true;
                    Messagealert_.ShowMessage(lblResultTab2, "Total:" + ObjPurReqList[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvRequisitionList.DataSource = null;
                    GvRequisitionList.DataBind();
                    GvRequisitionList.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblResultTab2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessageTab2, "system", 0);
                DivMsgTab2.Attributes["class"] = "FailAlert";
                DivMsgTab2.Visible = true;
            }
        }

        public List<PurchaseRequisitionData> GetPurchaseRequisitionList(int curIndex)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ObjPurReqData.PurchaseRequisitionTypeID = Convert.ToInt32(ddlTab2_RequisitionType.SelectedValue == "" ? "0" : ddlTab2_RequisitionType.SelectedValue);
            ObjPurReqData.RQNumber = txtTab2_RequisitionNo.Text == "" ? "0" : txtTab2_RequisitionNo.Text.Trim();
            ObjPurReqData.ItemID = Convert.ToInt32(lblTab2_HdnItemID.Text == "" ? "0" : lblTab2_HdnItemID.Text);
            ObjPurReqData.ItemName = txtTab2_ItemName.Text == "" ? "0" : txtTab2_ItemName.Text.Trim();
            DateTime From = txtTab2_DateFrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtTab2_DateFrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtTab2_DateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtTab2_DateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjPurReqData.DateFrom = From;
            ObjPurReqData.DateTo = To;
            ObjPurReqData.RQStatusID = Convert.ToInt32(ddlTab2_Status.SelectedValue == "" ? "0" : ddlTab2_Status.SelectedValue);
            return ObjPurReqBO.GetPurchaseRequisitionList(ObjPurReqData);
        }
        protected void GvRequisitionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridTab2(Convert.ToInt32(e.NewPageIndex + 1));
        }

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */
        //}

        protected void GvRequisitionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblStatus = (Label)e.Row.FindControl("lblStatusID");
                LinkButton LnkBtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEditTab2");
                LinkButton LnkBtnDelete = (LinkButton)e.Row.FindControl("lnkBtnDeleteTab2");

                if (LblStatus.Text == "1")
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.FromName("#fec337");
                    LnkBtnEdit.Attributes.Remove("disabled");
                    LnkBtnDelete.Attributes.Remove("disabled");
                }
                if (LblStatus.Text == "2")
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.FromName("#63d590");
                    LnkBtnEdit.Visible = false;
                    LnkBtnDelete.Visible = false;
                }
                if (LblStatus.Text == "3")
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.FromName("#ee4e42");
                    LnkBtnEdit.Visible = false;
                    LnkBtnDelete.Visible = false;
                }
            }
        }

        protected void GvRequisitionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvRequisitionList.Rows[i];
                    Label Number = (Label)gr.Cells[0].FindControl("lblRequisitionNo");
                    Label ID = (Label)gr.Cells[0].FindControl("lblItemID");
                    Label ItemName = (Label)gr.Cells[0].FindControl("lblItemName");
                    Label AvailableQnty = (Label)gr.Cells[0].FindControl("lblAvailableQuantity");
                    Label RequisitionQnty = (Label)gr.Cells[0].FindControl("lblRequisitionQuantity");
                    Label ProbableRate = (Label)gr.Cells[0].FindControl("lblProbableQuantity");
                    int ItemID = Convert.ToInt32(ID.Text);
                    String RQNumber = Convert.ToString(Number.Text);

                    List<PurchaseRequisitionData> PurchaseRequisitionList = new List<PurchaseRequisitionData>();
                    PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
                    lbl_PurReqNo.Text = RQNumber;
                    lbl_HdnItemID.Text = ItemID.ToString();
                    ObjPurReqData.ItemID = ItemID;
                    ObjPurReqData.ItemName = ItemName.Text.Trim();
                    ObjPurReqData.TotalAvailableQuantity = Convert.ToInt32(AvailableQnty.Text);
                    ObjPurReqData.PurchaseRequisitionQuantity = Convert.ToInt32(RequisitionQnty.Text == "" ? "0" : RequisitionQnty.Text);
                    ObjPurReqData.ProbableRate = Convert.ToDecimal(ProbableRate.Text == "" ? "0" : ProbableRate.Text);
                    ViewState["ID"] = lbl_PurReqNo.Text;

                    PurchaseRequisitionList.Add(ObjPurReqData);
                    Session["PurchaseRequisitionList"] = PurchaseRequisitionList;
                    GvPurReqList.DataSource = PurchaseRequisitionList;
                    GvPurReqList.DataBind();
                    EditPurReqItem();
                    GvPurReqList.Visible = true;

                    TabContainerPurchaseRequisition.ActiveTabIndex = 0;
                }
                if (e.CommandName == "Remove")
                {
                    PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
                    PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvRequisitionList.Rows[i];
                    Label RQNumber = (Label)gr.Cells[0].FindControl("lblRequisitionNo");
                    Label ItemID = (Label)gr.Cells[0].FindControl("lblItemID");
                    TextBox txtRemark = (TextBox)gr.Cells[0].FindControl("txtRemark");
                    if (txtRemark.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessageTab2, "Remarks", 0);
                        DivMsgTab2.Attributes["class"] = "FailAlert";
                        DivMsgTab2.Visible = true;
                        txtRemark.Focus();
                        return;
                    }
                    else
                    {
                        ObjPurReqData.Remark = txtRemark.Text;
                    }
                    ObjPurReqData.RQNumber = RQNumber.Text;
                    ObjPurReqData.ItemID = Convert.ToInt32(ItemID.Text);
                    ObjPurReqData.EmployeeID = LogData.EmployeeID;
                    ObjPurReqData.FinancialYearID = LogData.FinancialYearID;
                    ObjPurReqData.IPaddress = LogData.IPaddress;
                    ObjPurReqData.HospitalID = LogData.HospitalID;
                    ObjPurReqData.AddedBy = LogData.UserName;

                    int Result = ObjPurReqBO.DeletePurchaseRequisitionByID(ObjPurReqData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessageTab2, "delete", 1);
                        DivMsgTab2.Attributes["class"] = "SucessAlert";
                        DivMsgTab2.Visible = true;
                        BindGridTab2(1);
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessageTab2, "system", 0);
                        DivMsgTab2.Attributes["class"] = "FailAlert";
                        DivMsgTab2.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessageTab2, "system", 0);
                DivMsgTab2.Attributes["class"] = "FailAlert";
                DivMsgTab2.Visible = true;
            }
        }
        protected void EditPurReqItem()
        {
            foreach (GridViewRow row in GvPurReqList.Rows)
            {
                Label lblReqQnty = (Label)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_RQQuantity");
                TextBox txtReqQnty = (TextBox)GvPurReqList.Rows[row.RowIndex].Cells[0].FindControl("txt_RQQuantity");
                lblReqQnty.Visible = false;
                txtReqQnty.Visible = true;
                txtReqQnty.Enabled = true;
            }
            ddl_RequisitionType.Attributes["disabled"] = "disabled";
            txt_ItemName.Attributes["disabled"] = "disabled";
            txt_RequisitionQuantity.Attributes["disabled"] = "disabled";
            btnTab1Save.Attributes.Remove("disabled");
        }

        protected void btnTab2Print_Click(object sender, EventArgs e)
        {

        }

        protected void btnTab2Reset_Click(object sender, EventArgs e)
        {
            Tab2Reset();
        }
        protected void Tab2Reset()
        {
            ddlTab2_RequisitionType.SelectedIndex = 0;
            txtTab2_RequisitionNo.Text = "";
            txtTab2_ItemName.Text = "";
            txtTab2_DateFrom.Text = "";
            txtTab2_DateTo.Text = "";
            ddlTab2_Status.SelectedIndex = 0;
            lblmessageTab2.Text = "";
            lblmessageTab2.Visible = false;
            lblResultTab2.Text = "";
            lblResultTab2.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            GvRequisitionList.DataSource = null;
            GvRequisitionList.DataBind();
            GvRequisitionList.Visible = false;
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            //if (LogData.ExportEnable == 0)
            //{
            //    Messagealert_.ShowMessage(lblmessageTab2, "ExportEnable", 0);
            //    DivMsgTab2.Visible = true;
            //    DivMsgTab2.Attributes["class"] = "FailAlert";
            //    return;
            //}
            //else
            //{
            //    lblmessageTab2.Visible = false;
            //}
            if (ddlexport.SelectedIndex == 1)
            {
                ExportoExcel();
            }
            else
            {
                Messagealert_.ShowMessage(lblmessageTab2, "ExportType", 0);
                DivMsgTab2.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Purchase Requisition Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PurchaseRequisitionDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblmessageTab2, "Exported", 1);
                // divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<PurchaseRequisitionData> RequisitionDetailsData = GetPurchaseRequisitionList(0);
            List<PurchaseRequisitionDataToExcel> RequisitionList = new List<PurchaseRequisitionDataToExcel>();
            int i = 0;
            foreach (PurchaseRequisitionData row in RequisitionDetailsData)
            {
                PurchaseRequisitionDataToExcel RequisitionExcelList = new PurchaseRequisitionDataToExcel();
                RequisitionExcelList.RQNumber = RequisitionDetailsData[i].RQNumber;
                RequisitionExcelList.ItemID = RequisitionDetailsData[i].ItemID;
                RequisitionExcelList.ItemName = RequisitionDetailsData[i].ItemName;
                RequisitionExcelList.UnitDescription = RequisitionDetailsData[i].UnitDescription;
                RequisitionExcelList.TotalAvailableQuantity = Convert.ToInt32(RequisitionDetailsData[i].TotalAvailableQuantity.ToString("0"));
                RequisitionExcelList.PurchaseRequisitionTypeName = RequisitionDetailsData[i].PurchaseRequisitionTypeName;
                RequisitionExcelList.PurchaseRequisitionQuantity = Convert.ToInt32(RequisitionDetailsData[i].PurchaseRequisitionQuantity.ToString("0"));
                RequisitionExcelList.ProbableRate = Convert.ToDecimal(RequisitionDetailsData[i].ProbableRate.ToString("0.00"));
                RequisitionExcelList.RQStatusName = RequisitionDetailsData[i].RQStatusName;
                RequisitionExcelList.RequestedBy = RequisitionDetailsData[i].RequestedBy;
                RequisitionExcelList.ApprovedBy = RequisitionDetailsData[i].ApprovedBy;
                RequisitionList.Add(RequisitionExcelList);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(RequisitionList);
            return dt;
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);

                // Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //       inserting property values to datatable rows  
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //     put a breakpoint here and check datatable    
                return dataTable;
            }
        }


        //public List<PurchaseRequisitionData> GetEditPatientDetails(Int64 ID)
        //{
        //    PatientData objpat = new PatientData();
        //    RegistrationBO objpatBO = new RegistrationBO();
        //    objpat.ID = ID;
        //    return objpatBO.GetPtientDeatilbyID(objpat);
        //}


        //------------------------------------- End Tab 2 ---------------------------------------
    }
}