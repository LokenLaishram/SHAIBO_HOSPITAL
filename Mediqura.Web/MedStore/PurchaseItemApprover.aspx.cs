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

namespace Mediqura.Web.MedStore
{
    public partial class PurchaseItemApprover : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_totappreqd.Text = "0";
                txt_totcp.Text = "0";
                bindddl();
                btn_generatepo.Attributes["disabled"] = "disabled";
                btnprint.Attributes["disabled"] = "disabled";
                btnprints.Attributes["disabled"] = "disabled";
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_supplier, mstlookup.GetLookupsList(LookupName.Supplier));
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
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
                if (txt_retdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_retdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_retdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_returndateTo.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_returndateTo.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_returndateTo.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<StockGRNData> objdeposit = GetItemApprovalNoList(0);
                if (objdeposit.Count > 0)
                {
                    gvitemtobeapprovedlist.DataSource = objdeposit;
                    gvitemtobeapprovedlist.DataBind();
                    gvitemtobeapprovedlist.Visible = true;
                }
                else
                {
                    gvitemtobeapprovedlist.DataSource = null;
                    gvitemtobeapprovedlist.DataBind();
                    gvitemtobeapprovedlist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        public List<StockGRNData> GetItemApprovalNoList(int curIndex)
        {
            StockGRNData objstock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_retdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_retdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_returndateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_returndateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetItemApprovalNoList(objstock);
        }
        protected void gvitemtobeapprovedlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvitemtobeapprovedlist.Rows[i];
                    Label approvallistno = (Label)gr.Cells[0].FindControl("lbl_approvalno");
                    objbill.ApprovalListNo = approvallistno.Text;
                    List<StockGRNData> List = new List<StockGRNData>();
                    List = objstdBO.GetItemApprovalList(objbill);
                    if (List.Count > 0)
                    {
                        gvpurchaseorderlist.DataSource = List;
                        gvpurchaseorderlist.DataBind();
                        gvpurchaseorderlist.Visible = true;
                        btn_generatepo.Attributes.Remove("disabled");
                        txt_totcp.Text = "0.00";
                        txt_totappreqd.Text = "0";
                        foreach (GridViewRow row1 in gvpurchaseorderlist.Rows)
                        {
                            Label CP = (Label)gvpurchaseorderlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_cp");
                            TextBox totqty = (TextBox)gvpurchaseorderlist.Rows[row1.RowIndex].Cells[0].FindControl("txt_approvedqty");

                            txt_totcp.Text = (Convert.ToDecimal(txt_totcp.Text) + Convert.ToDecimal(CP.Text)).ToString();
                            txt_totappreqd.Text = (Convert.ToInt32(txt_totappreqd.Text) + Convert.ToInt32(totqty.Text)).ToString();
                        }
                    }
                    else
                    {
                        gvpurchaseorderlist.DataSource = null;
                        gvpurchaseorderlist.DataBind();
                        gvpurchaseorderlist.Visible = true;
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
        protected void gvpurchaseorderlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvpurchaseorderlist.Rows)
            {
                CheckBox cb = (CheckBox)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                cb.Checked = false;
            }
        }
        protected void gvpurchaseorderlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void txt_approvedqty_TextChanged(object sender, EventArgs e)
        {
            txt_totcp.Text = "0.00";
            txt_totappreqd.Text = "0";
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            foreach (GridViewRow row in gvpurchaseorderlist.Rows)
            {
                TextBox qty = (TextBox)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("txt_approvedqty");
                Label CPperQty = (Label)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpperqty");
                Label CP = (Label)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
                CP.Text = ((Convert.ToDecimal(CPperQty.Text)) * (Convert.ToInt32(qty.Text))).ToString();
                txt_totcp.Text = (Convert.ToDecimal(txt_totcp.Text) + Convert.ToDecimal(CP.Text)).ToString();
                txt_totappreqd.Text = (Convert.ToInt32(txt_totappreqd.Text) + Convert.ToInt32(qty.Text)).ToString();
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_retdatefrom.Text = "";
            txt_returndateTo.Text = "";
            gvpurchaseorderlist.DataSource = null;
            gvpurchaseorderlist.DataBind();
            gvpurchaseorderlist.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            txt_totappreqd.Text = "";
            txt_totcp.Text = "";
            Commonfunction.Insertzeroitemindex(ddl_supplier);
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_supplier, mstlookup.GetLookupsList(LookupName.Supplier));
            btn_generatepo.Attributes["disabled"] = "disabled";
        }
        protected void btn_generatepo_Click(object sender, EventArgs e)
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
            if (ddl_supplier.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "supplier", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;

            }
            List<StockGRNData> List = new List<StockGRNData>();
            StockGRNBO objBO = new StockGRNBO();
            StockGRNData objrec = new StockGRNData();
            try
            {

                txt_totappreqd.Text = "0";
                txt_totcp.Text = "0.00";

                // get all the record from the gridview
                foreach (GridViewRow row in gvpurchaseorderlist.Rows)
                {
                    CheckBox cb1 = (CheckBox)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    if (cb1.Checked)
                    {
                        IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                        Label ApprovalListNo = (Label)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_approvallistno");
                        Label CPperQty = (Label)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpperqty");
                        Label ItemID = (Label)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                        Label CP = (Label)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
                        Label Tax = (Label)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_tax");
                        Label Avail = (Label)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                        Label ReqdQty = (Label)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("txt_qty");
                        TextBox ApprovedQty = (TextBox)gvpurchaseorderlist.Rows[row.RowIndex].Cells[0].FindControl("txt_approvedqty");
                        if (Convert.ToInt32(ApprovedQty.Text) == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage, "ApprovedQty", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                            return;
                        }

                        StockGRNData obj = new StockGRNData();
                        obj.ApprovalListNo = ApprovalListNo.Text;
                        obj.CPPerQty = Convert.ToDecimal(CPperQty.Text);
                        obj.ItemID = Convert.ToInt64(ItemID.Text);
                        obj.ApprovedQty = Convert.ToInt32(ApprovedQty.Text);
                        obj.BalStock = Convert.ToInt32(Avail.Text);
                        obj.Tax = Convert.ToDouble(Tax.Text);
                        obj.TotalQuantity = Convert.ToInt32(ReqdQty.Text);
                        obj.CP = Convert.ToDecimal(CPperQty.Text) * Convert.ToInt32(ApprovedQty.Text);

                        txt_totappreqd.Text = (Convert.ToInt32(txt_totappreqd.Text == "" ? "0" : txt_totappreqd.Text) + obj.ApprovedQty).ToString();
                        txt_totcp.Text = (Convert.ToDecimal(txt_totcp.Text == "" ? "0.00" : txt_totcp.Text) + obj.CP).ToString();
                        List.Add(obj);

                    }
                }
                if (List.Count == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Checked", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                objrec.XMLData = XmlConvertor.ItemPurchaseOrderRecordDatatoXML(List).ToString();
                objrec.TotalQuantity = Convert.ToInt32(txt_totappreqd.Text == "" ? "0" : txt_totappreqd.Text);
                objrec.CP = Convert.ToDecimal(txt_totcp.Text == "" ? "0.00" : txt_totcp.Text);
                objrec.SupplierID = Convert.ToInt64(ddl_supplier.SelectedValue == "0" ? null : ddl_supplier.SelectedValue);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                int result = objBO.UpdatePurchaseOrder(objrec);

                if (result > 0)
                {
                    txt_purchaseOrderNo.Text = result.ToString();
                    Messagealert_.ShowMessage(lblmessage, "generate", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btn_generatepo.Attributes["disabled"] = "disabled";
                    btnprint.Attributes.Remove("disabled");
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                    gvpurchaseorderlist.DataSource = null;
                    gvpurchaseorderlist.DataBind();
                    gvpurchaseorderlist.Visible = false;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPONo(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.PONo = prefixText;
            getResult = objInfoBO.GetautoPONo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PONo.ToString());
            }
            return list;
        }
        protected void btnsearch1_Click(object sender, EventArgs e)
        {
            bindgrid1();
        }
        protected void txtPONo_TextChanged(object sender, EventArgs e)
        {
            if (txtPONo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage1, "PO", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
                txtPONo.Focus();
                return;

            }
            else
            {
                divmsg2.Visible = false;
            }
            bindgrid1();
        }
        protected void bindgrid1()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_from.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_from.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_from.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_To.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_To.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_To.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<StockGRNData> objdeposit = GetPurchaseList(0);
                if (objdeposit.Count > 0)
                {
                    gvpurchaselist.DataSource = objdeposit;
                    gvpurchaselist.DataBind();
                    gvpurchaselist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found", 1);
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    txtPONo.Text = "";
                    btnprints.Attributes.Remove("disabled");
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }

                }
                else
                {
                    div2.Visible = false;
                    gvpurchaselist.DataSource = null;
                    gvpurchaselist.DataBind();
                    gvpurchaselist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        public List<StockGRNData> GetPurchaseList(int curIndex)
        {
            StockGRNData objstock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            objstock.PONo = txtPONo.Text.ToString() == "" ? "" : txtPONo.Text.ToString();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetPurchaseList(objstock);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtPONo.Text = "";
            txt_from.Text = "";
            txt_To.Text = "";
            lblresult1.Visible = false;
            div2.Visible = false;
            gvpurchaselist.DataSource = null;
            gvpurchaselist.DataBind();
            gvpurchaselist.Visible = false;
            lblmessage1.Visible = false;
            divmsg2.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
           
        }
        protected DataTable GetDatafromDatabase()
        {
            List<StockGRNData> DepositDetails = GetPurchaseList(0);
            List<ItemPurchaseListDataTOeXCEL> ListexcelData = new List<ItemPurchaseListDataTOeXCEL>();
            int i = 0;
            foreach (StockGRNData row in DepositDetails)
            {
                ItemPurchaseListDataTOeXCEL Ecxeclpat = new ItemPurchaseListDataTOeXCEL();
                Ecxeclpat.PONo = DepositDetails[i].PONo;
                Ecxeclpat.ItemName = DepositDetails[i].ItemName;
                Ecxeclpat.Supplier = DepositDetails[i].Supplier;
                Ecxeclpat.ApprovedQty = DepositDetails[i].ApprovedQty;
                Ecxeclpat.CP = DepositDetails[i].CP;
                Ecxeclpat.CPPerQty = DepositDetails[i].CPPerQty;
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        protected void gvpurchaselist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "DeleteEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage1.Visible = false;
                    }
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvpurchaselist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label pono = (Label)gr.Cells[0].FindControl("lbl_pono");
                    Label cp = (Label)gr.Cells[0].FindControl("lbl_cp");
                    Label ApprovedAty = (Label)gr.Cells[0].FindControl("lbl_qty");

                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Remarks", 0);
                        div2.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.ID = Convert.ToInt64(ID.Text);
                    objbill.PONo = pono.Text;
                    objbill.ApprovedQty = Convert.ToInt32(ApprovedAty.Text);


                    objbill.EmployeeID = LogData.EmployeeID;

                    int Result = objstdBO.DeletePurchaseLIstByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                        bindgrid1();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage1, "system", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage1, "system", 0);
            }
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
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage1, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage1.Visible = false;
            }
            if (ddlexport.SelectedIndex == 1)
            {
                ExportoExcel();
            }
            else if (ddlexport.SelectedIndex == 2)
            {
                ExportToPdf();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult1, "ExportType", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvpurchaselist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvpurchaselist.Columns[6].Visible = false;
                    gvpurchaselist.Columns[7].Visible = false;
                    gvpurchaselist.Columns[8].Visible = false;

                    gvpurchaselist.RenderControl(hw);
                    gvpurchaselist.HeaderRow.Style.Add("width", "15%");
                    gvpurchaselist.HeaderRow.Style.Add("font-size", "10px");
                    gvpurchaselist.Style.Add("text-decoration", "none");
                    gvpurchaselist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvpurchaselist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ItemCheckList.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Item CheckList");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ItemCheckList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult1, "Exported", 1);
                divmsg2.Attributes["class"] = "SucessAlert";
            }
        }
        protected void gvpurchaselist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpurchaselist.PageIndex = e.NewPageIndex;
            bindgrid1();
        }
    }
}

