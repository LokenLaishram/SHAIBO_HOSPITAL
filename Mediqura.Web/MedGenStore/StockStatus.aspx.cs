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
using Mediqura.BOL.MedGenStoreBO;

namespace Mediqura.Web.MedGenStore
{
    public partial class StockStatus : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                lblmessage.Visible = false;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_StType, mstlookup.GetLookupsList(LookupName.GenStockType));
            ddl_StType.SelectedValue = "1";
            Commonfunction.PopulateDdl(ddl_stocstaus, mstlookup.GetLookupsList(LookupName.StockStatus));
            ddl_stocstaus.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_itemgroup, mstlookup.GetLookupsList(LookupName.GenGroups));
            Commonfunction.PopulateDdl(ddlmfgcompany, mstlookup.GetLookupsList(LookupName.Mfgcompany));
            Commonfunction.PopulateDdl(ddlsupplier, mstlookup.GetLookupsList(LookupName.Supplier));
            Commonfunction.PopulateDdl(ddlmonth, mstlookup.GetLookupsList(LookupName.month));
            Commonfunction.PopulateDdl(ddlrecivedyear, mstlookup.GetLookupsList(LookupName.Year));
            Commonfunction.Insertzeroitemindex(ddl_subgroup);
            txt_totarecievedqty.Text = "0";
            txttotalsold.Text = "0";
            txt_totalcondemn.Text = "0";
            txt_totalbalance.Text = "0";
            txt_totalCP.Text = "0.0";
            txt_totalMRP.Text = "0.0";
            txt_MRPCD.Text = "0.0";
            txt_totavendorreturnqty.Text = "0.0";
            txt_totastockreturnqty.Text = "0.0";
            AutoCompleteExtender2.CompletionSetCount = 0;
            AutoCompleteExtender2.ContextKey = "0";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.ItemName = prefixText;
            Objpaic.GroupID = Convert.ToInt32(contextKey);
            Objpaic.SubGroupID = Convert.ToInt32(count);
            getResult = objInfoBO.GetGenItemName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetBatchNo(string prefixText, int count, string contextKey)
        {
            StockStatusData Objpaic = new StockStatusData();
            GenStockStatusBO objInfoBO = new GenStockStatusBO();
            List<StockStatusData> getResult = new List<StockStatusData>();
            Objpaic.BatchNo = prefixText;
            getResult = objInfoBO.GetBatchNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BatchNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetRecieptNumber(string prefixText, int count, string contextKey)
        {
            StockStatusData Objpaic = new StockStatusData();
            GenStockStatusBO objInfoBO = new GenStockStatusBO();
            List<StockStatusData> getResult = new List<StockStatusData>();
            Objpaic.ReceiptNo = prefixText;
            getResult = objInfoBO.GetRecNumber(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ReceiptNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPoNumber(string prefixText, int count, string contextKey)
        {
            StockStatusData Objpaic = new StockStatusData();
            GenStockStatusBO objInfoBO = new GenStockStatusBO();
            List<StockStatusData> getResult = new List<StockStatusData>();
            Objpaic.POno = prefixText;
            getResult = objInfoBO.GetPoNumbers(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].POno.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetStockNumbers(string prefixText, int count, string contextKey)
        {
            StockStatusData Objpaic = new StockStatusData();
            GenStockStatusBO objInfoBO = new GenStockStatusBO();
            List<StockStatusData> getResult = new List<StockStatusData>();
            Objpaic.StockNo = prefixText;
            getResult = objInfoBO.GetStockNumbers(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].StockNo.ToString());
            }
            return list;
        }
        protected void bindStockStatus()
        {
            try
            {
                if (ddl_StType.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_StType.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

                if (ddlrecivedyear.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Year", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddlrecivedyear.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (ddl_stocstaus.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "StockStatus", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_stocstaus.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                List<StockStatusData> objdeposit = Get_StockStatus(0);
                if (objdeposit.Count > 0)
                {
                    gvstockstatus.DataSource = objdeposit;
                    gvstockstatus.DataBind();
                    gvstockstatus.Visible = true;
                    txt_totarecievedqty.Text = Commonfunction.Getrounding(objdeposit[0].TotalRecievedQty.ToString());
                    txttotalsold.Text = Commonfunction.Getrounding(objdeposit[0].TotalIsssuedQty.ToString());
                    txt_totalcondemn.Text = Commonfunction.Getrounding(objdeposit[0].TotalCondmQty.ToString());
                    txt_totalbalance.Text = Commonfunction.Getrounding(objdeposit[0].TotalbalaQty.ToString());
                    txt_totalCP.Text = Commonfunction.Getrounding(objdeposit[0].TotalRCP.ToString());
                    txt_totalMRP.Text = Commonfunction.Getrounding(objdeposit[0].TotalRMRP.ToString());
                    txt_MRPCD.Text = Commonfunction.Getrounding(objdeposit[0].TotalCMRP.ToString());
                    txt_totavendorreturnqty.Text = Commonfunction.Getrounding(objdeposit[0].TotalVendorReturnQty.ToString());
                    txt_totastockreturnqty.Text = Commonfunction.Getrounding(objdeposit[0].TotalSubstockReturnQty.ToString());
                    Messagealert_.ShowMessage(lblresult, "Total: " + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    btn_update.Visible = true;
                    btn_update.Attributes.Remove("disabled");
                }
                else
                {
                    txt_totarecievedqty.Text = "0";
                    txttotalsold.Text = "0";
                    txt_totalcondemn.Text = "0";
                    txt_totalbalance.Text = "0";
                    txt_totalCP.Text = "0.0";
                    txt_totalMRP.Text = "0.0";
                    txt_MRPCD.Text = "0.0";
                    gvstockstatus.DataSource = null;
                    gvstockstatus.DataBind();
                    gvstockstatus.Visible = true;
                    divmsg3.Visible = false;
                    btn_update.Visible = false;
                    btn_update.Attributes["disabled"] = "disabled";
                    txt_totavendorreturnqty.Text = "0.0";
                    txt_totastockreturnqty.Text = "0.0";

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }

        }
        public List<StockStatusData> Get_StockStatus(int curIndex)
        {
            StockStatusData objpat = new StockStatusData();
            GenStockStatusBO objbillingBO = new GenStockStatusBO();

            objpat.ItemName = txt_itemname.Text.ToString() == "" ? "" : txt_itemname.Text.ToString();
            objpat.BatchNo = txt_batchNo.Text == "" ? null : txt_batchNo.Text.Trim();
            objpat.StockTypeID = Convert.ToInt32(ddl_StType.SelectedValue == "" ? "0" : ddl_StType.SelectedValue);
            objpat.ReceiptNo = txt_recdno.Text == "" ? null : txt_recdno.Text;
            objpat.POno = txtPONo.Text == "" ? null : txtPONo.Text;
            objpat.StockNo = txtstockno.Text == "" ? null : txtstockno.Text;
            objpat.GroupID = Convert.ToInt32(ddl_itemgroup.SelectedValue == "" ? "0" : ddl_itemgroup.SelectedValue);
            objpat.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
            string ID;
            var source = txt_itemname.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objpat.ItemID = Convert.ToInt32(ID);
            }
            else
            {
                objpat.ItemID = 0;
                txt_itemname.Text = "";
            }
            objpat.BatchNo = txt_batchNo.Text == "" ? null : txt_batchNo.Text;
            objpat.Availbalancefrom = Convert.ToInt32(txt_avaifro.Text == "" ? "0" : txt_avaifro.Text);
            objpat.Availbalanceto = Convert.ToInt32(txt_availto.Text == "" ? "100000000" : txt_availto.Text);
            objpat.ExpiryDayfrom = Convert.ToInt32(txt_expdaysfro.Text == "" ? "0" : txt_expdaysfro.Text);
            objpat.ExpiryDayto = Convert.ToInt32(txt_expdaysto.Text == "" ? "100000000" : txt_expdaysfro.Text);
            objpat.StockStatus = Convert.ToInt32(ddl_stocstaus.Text == "" ? "100000000" : ddl_stocstaus.Text);
            objpat.Recievedyear = Convert.ToInt32(ddlrecivedyear.SelectedValue == "" ? "0" : ddlrecivedyear.SelectedValue);
            objpat.Recievedmonth = Convert.ToInt32(ddlmonth.SelectedValue == "" ? "0" : ddlmonth.SelectedValue);
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_recdfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_recdfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_recdTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_recdTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.CompanyID = Convert.ToInt32(ddlmfgcompany.SelectedValue == "" ? "0" : ddlmfgcompany.SelectedValue);
            objpat.SupplierID = Convert.ToInt32(ddlsupplier.SelectedValue == "" ? "0" : ddlsupplier.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objbillingBO.Get_StockStatus(objpat);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindStockStatus();
        }
        protected void txt_itemname_TextChanged(object sender, EventArgs e)
        {
            if (txt_itemname.Text != "")
            {
                bindStockStatus();

            }
        }
        protected void ddl_itemgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_itemgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetItemSubGroupByItemGroupID(Convert.ToInt32(ddl_itemgroup.SelectedValue)));
                AutoCompleteExtender2.ContextKey = ddl_itemgroup.SelectedValue;
            }
            else
            {
                AutoCompleteExtender2.ContextKey = "0";
            }
        }
        protected void ddl_itemsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.CompletionSetCount = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_batchNo.Text = "";
            txt_itemname.Text = "";
            gvstockstatus.DataSource = null;
            gvstockstatus.DataBind();
            gvstockstatus.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            lblresult.Text = "";
            ddlexport.Visible = false;
            btnexport.Visible = false;
            btn_update.Visible = false;
            txt_avaifro.Text = "";
            txt_availto.Text = "";
            txt_expdaysfro.Text = "";
            txt_expdaysto.Text = "";
            txt_totalbalance.Text = "";
            txt_totalcondemn.Text = "";
            txt_totalCP.Text = "";
            txt_totalMRP.Text = "";
            txtPONo.Text = "";
            txtstockno.Text = "";
            txttotalsold.Text = "";
            txt_totarecievedqty.Text = "";
            ddlsupplier.SelectedIndex = 0;
            ddlrecivedyear.SelectedIndex = 0;
            ddlmfgcompany.SelectedIndex = 0;
            ddlmonth.SelectedIndex = 0;
            ddl_stocstaus.SelectedIndex = 1;
            txt_recdfrom.Text = "";
            txt_recdTo.Text = "";
            txt_recdno.Text = "";
            txt_MRPCD.Text = "";
            btnexport.Visible = false;
            ddlexport.Visible = false;
            ddl_itemgroup.SelectedIndex = 0;
            ddl_subgroup.SelectedIndex = 0;
            txt_totarecievedqty.Text = "0";
            txttotalsold.Text = "0";
            txt_totalcondemn.Text = "0";
            txt_totalbalance.Text = "0";
            txt_totalCP.Text = "0.0";
            txt_totalMRP.Text = "0.0";
            txt_MRPCD.Text = "0.0";
        }

        protected void ddl_StType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_StType.SelectedIndex > 0)
            {
                bindStockStatus();
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                List<StockStatusData> Liststock = new List<StockStatusData>();
                GenStockStatusBO objLabSampleBO = new GenStockStatusBO();
                StockStatusData objSampleData = new StockStatusData();
                int countcond = 0;
                foreach (GridViewRow row in gvstockstatus.Rows)
                {
                    Label StockID = (Label)gvstockstatus.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    TextBox Condem = (TextBox)gvstockstatus.Rows[row.RowIndex].Cells[0].FindControl("txt_cndmn");
                    Label SubstockID = (Label)gvstockstatus.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    Label StockType = (Label)gvstockstatus.Rows[row.RowIndex].Cells[0].FindControl("lbl_stocktype");
                    TextBox RecvDate = (TextBox)gvstockstatus.Rows[row.RowIndex].Cells[0].FindControl("txt_reciveddate");
                    TextBox ExpireDate = (TextBox)gvstockstatus.Rows[row.RowIndex].Cells[0].FindControl("lbl_expdate");
                    TextBox Available = (TextBox)gvstockstatus.Rows[row.RowIndex].Cells[0].FindControl("txt_balance");
                    Label subheading = (Label)gvstockstatus.Rows[row.RowIndex].Cells[0].FindControl("lbl_Subheading");
                    StockStatusData ObjDetails = new StockStatusData();
                    if (subheading.Text == "0")
                    {
                        ObjDetails.TotalCondemnQty = Convert.ToInt32(Condem.Text == "" ? null : Condem.Text);
                        ObjDetails.StockID = Convert.ToInt64(StockID.Text == "" ? null : StockID.Text);
                        ObjDetails.SubStockID = Convert.ToInt64(SubstockID.Text == "" ? null : SubstockID.Text);
                        ObjDetails.StockTypeID = Convert.ToInt32(StockType.Text == "" ? null : StockType.Text);
                        ObjDetails.RecivedDates = RecvDate.Text == "" ? "" : RecvDate.Text;
                        ObjDetails.ExpireDates = ExpireDate.Text == "" ? "" : ExpireDate.Text;
                        ObjDetails.BalStock = Convert.ToInt32(Available.Text == "" ? "0" : Available.Text);
                        Liststock.Add(ObjDetails);
                    }
                }
                objSampleData.XMLData = XmlConvertor.StockStatustoXML(Liststock).ToString();
                objSampleData.ActionType = Enumaction.Insert;
                int result = objLabSampleBO.UpdateStockIssueDetails(objSampleData);
                if (result == 1)
                {
                    bindStockStatus();
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
                    //btn_update.Attributes["disabled"] = "disabled";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        protected void gvstockstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label StockStaus = (Label)e.Row.FindControl("lblstockstatus");
                TextBox Label1 = (TextBox)e.Row.FindControl("lbl_nodays");
                Label subheading = (Label)e.Row.FindControl("lbl_Subheading");
                Label itemName = (Label)e.Row.FindControl("lblitemname");
                Label recvqty = (Label)e.Row.FindControl("lbl_totalrecdquantity");
                Label issueqty = (Label)e.Row.FindControl("lbl_issuedQty");
                Label stockreturnqty = (Label)e.Row.FindControl("lbl_subtockreturn");
                Label vendorretuen = (Label)e.Row.FindControl("lbl_vendorreturnqty");
                TextBox condemnQty = (TextBox)e.Row.FindControl("txt_cndmn");
                TextBox tocondemnQty = (TextBox)e.Row.FindControl("txt_totalcodemn");
                TextBox BalQty = (TextBox)e.Row.FindControl("txt_balance");
                Label Cp = (Label)e.Row.FindControl("lbl_cp");
                Label MRP = (Label)e.Row.FindControl("lbl_totalmrp");
                Label cpperqty = (Label)e.Row.FindControl("lbl_cpqty");
                Label MRPperqty = (Label)e.Row.FindControl("lbl_mrpqty");
                TextBox expiryday = (TextBox)e.Row.FindControl("lbl_nodays");
                if (StockStaus.Text == "1")
                {
                    e.Row.Cells[17].BackColor = System.Drawing.Color.Green;
                    Label1.ForeColor = System.Drawing.Color.White;
                }
                if (StockStaus.Text == "2")
                {
                    e.Row.Cells[17].BackColor = System.Drawing.Color.Yellow;
                    Label1.ForeColor = System.Drawing.Color.Black;
                }
                if (StockStaus.Text == "3")
                {
                    e.Row.Cells[17].BackColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.White;
                }
                TextBox servicedate = (TextBox)e.Row.FindControl("txt_reciveddate");
                TextBox servicedateenddate = (TextBox)e.Row.FindControl("lbl_expdate");
                if (servicedate.Text == "01/01/0001 12:00:00 AM" || servicedate.Text == "01/01/01 00:00:00" || servicedate.Text == "1/1/0001 12:00:00 AM")
                {
                    servicedate.Text = "";
                }
                if (servicedateenddate.Text == "01/01/0001 12:00:00 AM" || servicedateenddate.Text == "01/01/01 00:00:00" || servicedateenddate.Text == "1/1/0001 12:00:00 AM")
                {
                    servicedateenddate.Text = "";
                }
                if (subheading.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#33aa99");
                    itemName.ForeColor = System.Drawing.Color.White;
                    recvqty.ForeColor = System.Drawing.Color.White;
                    issueqty.ForeColor = System.Drawing.Color.White;
                    stockreturnqty.ForeColor = System.Drawing.Color.White;
                    vendorretuen.ForeColor = System.Drawing.Color.White;
                    // BalQty.ForeColor = System.Drawing.Color.White;
                    Cp.ForeColor = System.Drawing.Color.White;
                    MRP.ForeColor = System.Drawing.Color.White;
                    servicedate.Visible = false;
                    servicedateenddate.Visible = false;
                    condemnQty.Visible = false;
                    cpperqty.Visible = false;
                    MRPperqty.Visible = false;
                    BalQty.ReadOnly = true;
                    expiryday.Text = "";
                }
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                div1.Visible = false;
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
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg3.Attributes["class"] = "FailAlert";
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
                    gvstockstatus.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvstockstatus.Columns[6].Visible = false;
                    //gvstockstatus.Columns[8].Visible = false;
                    //gvstockstatus.Columns[10].Visible = false;
                    //gvstockstatus.Columns[11].Visible = false;

                    gvstockstatus.RenderControl(hw);
                    gvstockstatus.HeaderRow.Style.Add("width", "15%");
                    gvstockstatus.HeaderRow.Style.Add("font-size", "10px");
                    gvstockstatus.Style.Add("text-decoration", "none");
                    gvstockstatus.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvstockstatus.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StockStatusDetails.pdf");
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
                wb.Worksheets.Add(dt, "Stock Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StockStatusDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<StockStatusData> StockStatus = Get_StockStatus(0);
            List<StockStatusListDataTOeXCEL> ListexcelData = new List<StockStatusListDataTOeXCEL>();
            int i = 0;
            foreach (StockStatusData row in StockStatus)
            {
                StockStatusListDataTOeXCEL Ecxeclpat = new StockStatusListDataTOeXCEL();
                Ecxeclpat.StockNo = StockStatus[i].StockNo;
                Ecxeclpat.BatchNo = StockStatus[i].BatchNo;
                Ecxeclpat.ItemName = StockStatus[i].ItemName;
                Ecxeclpat.CPPerQty = StockStatus[i].CPPerQty;
                Ecxeclpat.CP = StockStatus[i].CP;
                Ecxeclpat.MRPperqty = StockStatus[i].MRPPerQty;
                Ecxeclpat.Nodaystoexpire = StockStatus[i].Nodaystoexpire;
                Ecxeclpat.TotalMRP = StockStatus[i].TotalMRP;
                Ecxeclpat.TotalRecdQty = StockStatus[i].TotalRecdQty;
                Ecxeclpat.TotalSale = StockStatus[i].TotalSale;
                Ecxeclpat.TotalIssued = StockStatus[i].TotalIssued;
                Ecxeclpat.SupplierName = StockStatus[i].SupplierName;
                Ecxeclpat.SubReceivedDate = StockStatus[i].SubReceivedDate;
                Ecxeclpat.ReceivedDate = StockStatus[i].ReceivedDate;
                Ecxeclpat.TotalCondemnQty = StockStatus[i].TotalCondemnQty;
                Ecxeclpat.BalStock = StockStatus[i].BalStock;
                Ecxeclpat.StockType = StockStatus[i].StockType;
                Ecxeclpat.CompanyName = StockStatus[i].CompanyName;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
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

        protected void gvstockstatus_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvstockstatus.PageIndex = e.NewPageIndex;
            bindStockStatus();
        }

        protected void txt_batchNo_TextChanged(object sender, EventArgs e)
        {
            if (txt_batchNo.Text != "")
            {
                bindStockStatus();
            }
        }

    }
}