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
    public partial class InterStockTransfer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                btnsave.Attributes["disabled"] = "disabled";

            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            StockIssueData Objpaic = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            List<StockIssueData> getResult = new List<StockIssueData>();
            Objpaic.ItemName = prefixText;
            Objpaic.GroupID = Convert.ToInt32(contextKey);
            Objpaic.SubGroupID = count;
            getResult = objInfoBO.GetItemNameListinSubStock(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_stockfrom, mstlookup.GetLookupsList(LookupName.SubStockType));
            Commonfunction.PopulateDdl(ddltransferfrom, mstlookup.GetLookupsList(LookupName.SubStockType));
            Commonfunction.PopulateDdl(ddl_group, mstlookup.GetLookupsList(LookupName.Groups));

        }
        protected void ddl_stockfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_stockfrom.SelectedIndex > 0)
            {
                StockGRNData objstock = new StockGRNData();
                objstock.StockType = Convert.ToInt32(ddl_stockfrom.SelectedValue == "0" ? null : ddl_stockfrom.SelectedValue);

                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_stock_to, mstlookup.GetStockType(Convert.ToInt32(ddl_stockfrom.SelectedValue)));
            }
            else
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_stock_to, mstlookup.GetLookupsList(LookupName.SubStockType));
            }
        }
        protected void ddl_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddl_group.SelectedValue;
            if (ddl_group.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetItemSubGroupByItemGroupID(Convert.ToInt32(ddl_group.SelectedValue)));
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_subgroup);
            }
        }
        protected void ddl_subgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.CompletionSetCount = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
        }
        protected void ddltransferfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddltransferfrom.SelectedIndex) == 0)
            {
                Messagealert_.ShowMessage(lblmessage1, "Transfer", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                bindgrid1();
            }
        }

        protected void bindgrid()
        {
            try
            {

                List<StockGRNData> objdeposit = GetItemList(0);
                if (objdeposit.Count > 0)
                {
                    gvitemlist.DataSource = objdeposit;
                    gvitemlist.DataBind();
                    gvitemlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found", 1);
                    lblresult.Visible = false;
                    btnsave.Attributes.Remove("disabled");
                 }
                else
                {
                    gvitemlist.DataSource = null;
                    gvitemlist.DataBind();
                    gvitemlist.Visible = true;
                }

              
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        public List<StockGRNData> GetItemList(int curIndex)
        {
            StockGRNData objstock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            objstock.ItemName = txtItemName.Text == "" ? "" : txtItemName.Text;
            objstock.StockType = Convert.ToInt32(ddl_stockfrom.SelectedValue == "" ? "0" : ddl_stockfrom.SelectedValue);
            objstock.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue);
            objstock.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);

            return objBO.GetStockItemTransferList(objstock);
        }
        protected void gvitemlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvitemlist.Rows)
            {
                TextBox txt = (TextBox)gvitemlist.Rows[row.RowIndex].Cells[0].FindControl("txt_transferqty"); //find the CheckBox
                txt.Focus();
            }
        }
        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
           
                bindgrid();
          
        }
        protected void btn_save_Click(object sender, EventArgs e)
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
            List<StockGRNData> List = new List<StockGRNData>();
            StockGRNBO objBO = new StockGRNBO();
            StockGRNData objrec = new StockGRNData();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvitemlist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label SubStockID = (Label)gvitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    Label StockNo = (Label)gvitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblstockno");
                    Label IssueNo = (Label)gvitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_issueno");
                    Label IndentNo = (Label)gvitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_indentno");
                    Label MRP = (Label)gvitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_mrp");
                    Label CP = (Label)gvitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
                    Label Avail = (Label)gvitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");

                    TextBox Qty = (TextBox)gvitemlist.Rows[row.RowIndex].Cells[0].FindControl("txt_transferqty");
                    if (Convert.ToInt32(Qty.Text) == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "TransferQty", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        Qty.Focus();
                        return;
                    }
                    if((Convert.ToInt32(Qty.Text)) > (Convert.ToInt32(Avail.Text)))
                    {
                        Messagealert_.ShowMessage(lblmessage, "IssueQty", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        Qty.Focus();
                        return;
                    }
                    StockGRNData obj = new StockGRNData();
                    obj.StockNo = StockNo.Text;
                    obj.IndentNo = IndentNo.Text;
                    obj.IssueNo = Convert.ToInt32(IssueNo.Text);
                    obj.MRP = Convert.ToDecimal(MRP.Text);
                    obj.CP = Convert.ToDecimal(CP.Text);
                    obj.SubStockID = Convert.ToInt32(SubStockID.Text);
                    obj.TotalQuantity = Convert.ToInt32(Qty.Text);
                    List.Add(obj);
                }
                objrec.XMLData = XmlConvertor.StockTransferRecordDatatoXML(List).ToString();
                objrec.StockTo = Convert.ToInt32(ddl_stock_to.SelectedValue == "" ? "0" : ddl_stock_to.SelectedValue);
                objrec.StockFrom = Convert.ToInt32(ddl_stockfrom.SelectedValue == "" ? "0" : ddl_stockfrom.SelectedValue);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                int result = objBO.UpdateStockTransfer(objrec);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    gvitemlist.DataSource = null;
                    gvitemlist.DataBind();
                    gvitemlist.Visible = false;
                    Session["IndentList"] = null;
                    gvitemlist.Attributes["disabled"] = "disabled";

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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvitemlist.DataSource = null;
            gvitemlist.DataBind();
            gvitemlist.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            txtItemName.Text = "";
            Commonfunction.Insertzeroitemindex(ddl_stockfrom);
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_stockfrom, mstlookup.GetLookupsList(LookupName.SubStockType));
            Commonfunction.Insertzeroitemindex(ddl_stock_to);
            Commonfunction.Insertzeroitemindex(ddl_group);
            Commonfunction.Insertzeroitemindex(ddl_subgroup);
            MasterLookupBO mstlookup1 = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_group, mstlookup1.GetLookupsList(LookupName.Groups));
            btnsave.Attributes["disabled"] = "disabled";
        }
        protected void btnsearch1_Click(object sender, EventArgs e)
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
            bindgrid1();
        }
        protected void bindgrid1()
        {
            try
            {

                if (Convert.ToInt32(ddltransferfrom.SelectedIndex) == 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "Transfer", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    divmsg2.Visible = false;
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
                List<StockGRNData> objdeposit = GetStockTransferList(0);
                if (objdeposit.Count > 0)
                {
                    
                    gvinterstocklist.DataSource = objdeposit;
                    gvinterstocklist.DataBind();
                    gvinterstocklist.Visible = true;
                    divmsg2.Visible = false;
                    lblmessage1.Visible = false;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found", 1);
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    lblresult1.Visible = false;
                    div2.Visible = false;
                    gvinterstocklist.DataSource = null;
                    gvinterstocklist.DataBind();
                    gvinterstocklist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        public List<StockGRNData> GetStockTransferList(int curIndex)
        {
            StockGRNData objstock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.StockFrom = Convert.ToInt32(ddltransferfrom.SelectedValue == "" ? "0" : ddltransferfrom.SelectedValue);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetStockTransferList(objstock);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_from.Text = "";
            txt_To.Text = "";
            Commonfunction.Insertzeroitemindex(ddltransferfrom);
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddltransferfrom, mstlookup.GetLookupsList(LookupName.SubStockType));
            lblresult1.Visible = false;
            div2.Visible = false;
            gvinterstocklist.DataSource = null;
            gvinterstocklist.DataBind();
            gvinterstocklist.Visible = false;
            lblmessage1.Visible = false;
            divmsg2.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            
        }
        protected DataTable GetDatafromDatabase()
        {
            List<StockGRNData> DepositDetails = GetStockTransferList(0);
            List<ItemTransferListDataTOeXCEL> ListexcelData = new List<ItemTransferListDataTOeXCEL>();
            int i = 0;
            foreach (StockGRNData row in DepositDetails)
            {
                ItemTransferListDataTOeXCEL Ecxeclpat = new ItemTransferListDataTOeXCEL();
                Ecxeclpat.IssueNo = DepositDetails[i].IssueNo;
                Ecxeclpat.ItemName = DepositDetails[i].ItemName;
                Ecxeclpat.Stock_From = DepositDetails[i].Stock_From;
                Ecxeclpat.Stock_To = DepositDetails[i].Stock_To;
                Ecxeclpat.TotalQuantity = DepositDetails[i].TotalQuantity;
                Ecxeclpat.AddedDate = DepositDetails[i].AddedDate;

                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        protected void gvinterstocklist_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow gr = gvinterstocklist.Rows[i];
                    Label issueno = (Label)gr.Cells[0].FindControl("lbl_issueno");
                    Label Qty = (Label)gr.Cells[0].FindControl("lbl_qty");
                    Label itemID = (Label)gr.Cells[0].FindControl("lbl_itemID");

                    objbill.ItemID = Convert.ToInt32(itemID.Text);
                    objbill.IssueNo = Convert.ToInt32(issueno.Text);
                    objbill.TotalQuantity = Convert.ToInt32(Qty.Text);


                    objbill.EmployeeID = LogData.EmployeeID;

                    int Result = objstdBO.DeleteStockTransferByIssueno(objbill);
                    if (Result == 1)
                    {
                        bindgrid1();
                        Messagealert_.ShowMessage(lblmessage1, "cancel", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
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
                    gvinterstocklist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvinterstocklist.Columns[7].Visible = false;
                    gvinterstocklist.RenderControl(hw);
                    gvinterstocklist.HeaderRow.Style.Add("width", "15%");
                    gvinterstocklist.HeaderRow.Style.Add("font-size", "10px");
                    gvinterstocklist.Style.Add("text-decoration", "none");
                    gvinterstocklist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvinterstocklist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=InterStockTransferList.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=InterStockTransferList.xlsx");
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
        protected void gvinterstocklist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvinterstocklist.PageIndex = e.NewPageIndex;
            bindgrid1();
        }

    }
}