using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
    public partial class ItemreturntoMainStock : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                btnsave.Attributes["disabled"] = "disabled";
                btnprint.Attributes["disabled"] = "disabled";
                Session["ReturnList"] = null;
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.SubStockType));
            ddl_substock.SelectedValue = LogData.MedSubStockID.ToString();
            Commonfunction.PopulateDdl(ddl_substocklist, mstlookup.GetLookupsList(LookupName.SubStockType));
            ddl_substocklist.SelectedValue = LogData.MedSubStockID.ToString();
            if (LogData.RoleID == 1)
            {
                ddl_substock.Attributes.Remove("disabled");
                ddl_substock.Attributes.Remove("disabled");
            }
            else
            {
                ddl_substock.Attributes["disabled"] = "disabled";
                ddl_substock.Attributes["disabled"] = "disabled";
            }
            AutoCompleteExtender2.ContextKey = ddl_substock.SelectedValue;
        }
        protected void ddl_substock_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddl_substock.SelectedValue;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {

            MedIndentData Objpaic = new MedIndentData();
            MedStoreIndentBO objInfoBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            Objpaic.ItemName = prefixText;
            Objpaic.MedSubStockID = Convert.ToInt64(contextKey == "" ? "0" : contextKey);
            getResult = objInfoBO.GetreturnItemNameListInStore(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            additem();
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
            List<MedIndentData> ListStock = new List<MedIndentData>();
            MedIndentData objStock = new MedIndentData();
            MedStoreIndentBO objBO = new MedStoreIndentBO();
            try
            {
                foreach (GridViewRow row in gvStockReturn.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label returnQty = (Label)gvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_return");
                    Label ItemID = (Label)gvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_itemID");
                    Label subStockID = (Label)gvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lblsubStockID");

                    MedIndentData ObjDetails = new MedIndentData();

                    ObjDetails.ReturnQty = Convert.ToInt32(returnQty.Text == "" ? "0" : returnQty.Text);
                    ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                    ObjDetails.SubStockID = Convert.ToInt32(subStockID.Text == "" ? "0" : subStockID.Text);
                    ListStock.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.Med_StockReturnDatatoXML(ListStock).ToString();
                objStock.MedSubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                objStock.TotReturnQty = Convert.ToInt32(txt_totReturnQty.Text == "" ? "0" : txt_totReturnQty.Text);
                objStock.ReturnBy = LogData.EmployeeID;
                objStock.HospitalID = LogData.HospitalID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;

                List<MedIndentData> Result = objBO.UpdateStockReturnDetails(objStock);

                if (Result.Count > 0)
                {
                    txt_returnNo.Text = Result[0].ReturnNo.ToString();
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    Session["ReturnList"] = null;
                    btnsave.Attributes["disabled"] = "disabled";
                    btnprint.Attributes.Remove("disabled");
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
            gvStockReturn.DataSource = null;
            gvStockReturn.DataBind();
            gvStockReturn.Visible = false;
            lblresult.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            divmsg1.Visible = false;
            txtItemName.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            txt_returnNo.Text = "";
            Session["ReturnList"] = null;
            txt_totReturnQty.Text = "";
            txt_available.Text = "";
            txtquantity.Text = "";
            Session["ReturnList"] = null;
            txtItemName.Focus();
        }
        protected void gvStockReturn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvStockReturn.Rows[i];
                    List<MedIndentData> ItemList = Session["ReturnList"] == null ? new List<MedIndentData>() : (List<MedIndentData>)Session["ReturnList"];
                    Label Qtyreturn = (Label)gr.Cells[0].FindControl("lbl_return");
                    txt_totReturnQty.Text = (Convert.ToInt32(txt_totReturnQty.Text == "" ? "0" : txt_totReturnQty.Text) - Convert.ToInt32(Qtyreturn.Text.Trim() == "" ? "0" : Qtyreturn.Text.Trim())).ToString();
                    ItemList.RemoveAt(i);
                    Session["ReturnList"] = ItemList;
                    gvStockReturn.DataSource = ItemList;
                    gvStockReturn.DataBind();

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
        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            txtquantity.Focus();
        }
        private void additem()
        {
            if (txtItemName.Text == "" || !txtItemName.Text.Contains(":"))
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                txtItemName.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtItemName.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_substock.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "GenStock", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_substock.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txtquantity.Text == "")
            {

                Messagealert_.ShowMessage(lblmessage, "Quantity", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtquantity.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            List<MedIndentData> ReturnList = Session["ReturnList"] == null ? new List<MedIndentData>() : (List<MedIndentData>)Session["ReturnList"];
            MedIndentData objStock = new MedIndentData();

            string source = txtItemName.Text.ToString();
            string StockNo = source.Substring(source.LastIndexOf(':') + 1);
            string avail = source.Split('>', '#')[1];
            if (StockNo != "")
            {

                foreach (GridViewRow row in gvStockReturn.Rows)
                {
                    Label StockID = (Label)gvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lblsubStockID");

                    if (Convert.ToInt64(StockID.Text) == Convert.ToInt64(StockNo))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtItemName.Focus();
                        txtItemName.Text = "";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
            }
            else
            {
                txtItemName.Text = "";
                return;
            }
            MedIndentData Objpaic = new MedIndentData();
            MedStoreIndentBO objInfoBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            Objpaic.SubStockID = Convert.ToInt64(StockNo == "" ? "0" : StockNo);
            getResult = objInfoBO.GetItemDetailsByItemID(Objpaic);
            if (getResult.Count > 0)
            {
                objStock.BalStock = getResult[0].BalStock;
                if (Convert.ToInt32(txtquantity.Text.Trim() == "" ? "0" : txtquantity.Text.Trim()) > objStock.BalStock)
                {
                    Messagealert_.ShowMessage(lblmessage, "UseRecord", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtquantity.Text = "";
                    txtquantity.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            objStock.ItemID = getResult[0].ItemID;
            objStock.ItemName = getResult[0].ItemName;
            objStock.SubStockID = Convert.ToInt64(StockNo == "" ? "0" : StockNo);
            objStock.ReturnQty = Convert.ToInt32(txtquantity.Text.Trim() == "" ? "0" : txtquantity.Text.Trim());
            ReturnList.Add(objStock);
            if (ReturnList.Count > 0)
            {
                gvStockReturn.DataSource = ReturnList;
                gvStockReturn.DataBind();
                gvStockReturn.Visible = true;
                Session["ReturnList"] = ReturnList;
                txt_totReturnQty.Text = (Convert.ToInt32(txt_totReturnQty.Text == "" ? "0" : txt_totReturnQty.Text) + Convert.ToInt32(txtquantity.Text.Trim() == "" ? "0" : txtquantity.Text.Trim())).ToString();
                clearall();
                txtItemName.Focus();
                txtItemName.Text = "";
                btnsave.Attributes.Remove("disabled");

            }
            else
            {
                gvStockReturn.DataSource = null;
                gvStockReturn.DataBind();
                gvStockReturn.Visible = true;
            }

        }
        protected void clearall()
        {
            txtItemName.Text = "";
            txt_available.Text = "";
            txtquantity.Text = "";
        }
        //-------------------------------------------------Tab2--------------------------------------
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvreturnlist.DataSource = null;
            gvreturnlist.DataBind();
            gvreturnlist.Visible = false;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            ddl_substocklist.SelectedIndex = 0;
            ddl_user.SelectedIndex = 0;
            txt_returnNoList.Text = "";
            txt_from.Text = "";
            txt_To.Text = "";
            txt_TotreturnList.Text = "";
        }
        protected void bindgrid()
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
                    lblmessage2.Visible = false;
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
                    divmsg2.Visible = false;
                }
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
                    divmsg2.Visible = false;
                }
                List<MedIndentData> objdeposit = GetReturnItemList(0);
                if (objdeposit.Count > 0)
                {
                    txt_TotreturnList.Text = Commonfunction.Getrounding(objdeposit[0].SumReturnQty.ToString());
                    gvreturnlist.DataSource = objdeposit;
                    gvreturnlist.DataBind();
                    gvreturnlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div3.Attributes["class"] = "SucessAlert";
                    div3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage.Visible = false;
                    btn_print.Attributes.Remove("disabled");
                }
                else
                {
                    gvreturnlist.DataSource = null;
                    gvreturnlist.DataBind();
                    gvreturnlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    div3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<MedIndentData> GetReturnItemList(int curIndex)
        {
            MedIndentData objstock = new MedIndentData();
            MedStoreIndentBO objBO = new MedStoreIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.ReturnNo = txt_returnNoList.Text.ToString() == "" ? "" : txt_returnNoList.Text.ToString();
            objstock.MedSubStockID = Convert.ToInt32(ddl_substocklist.SelectedValue == "" ? "0" : ddl_substocklist.SelectedValue);
            objstock.ReturnBy = Convert.ToInt64(ddl_user.SelectedValue == "" ? "0" : ddl_user.SelectedValue); ;
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetReturnItemList(objstock);
        }
        protected void gvreturnlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    MedIndentData objData = new MedIndentData();
                    MedStoreIndentBO objBO = new MedStoreIndentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvreturnlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label ReturnNo = (Label)gr.Cells[0].FindControl("lbl_ReturnNo");
                    Label qty = (Label)gr.Cells[0].FindControl("lbl_TotRetnqty");
                    objData.ID = Convert.ToInt64(ID.Text);
                    objData.ReturnNo = ReturnNo.Text;
                    objData.ReturnQty = Convert.ToInt32(qty.Text);
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Remarks", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objData.Remarks = txtremarks.Text;
                    }
                    MedStoreIndentBO objIndentStatusBO1 = new MedStoreIndentBO();
                    int Result = objBO.DeleteStockReurnByID(objData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";

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
        protected void gvreturnlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvreturnlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvreturnlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvreturnlist.Columns[4].Visible = false;
                    gvreturnlist.Columns[5].Visible = false;
                    gvreturnlist.Columns[6].Visible = false;
                    gvreturnlist.Columns[7].Visible = false;
                    gvreturnlist.RenderControl(hw);
                    gvreturnlist.HeaderRow.Style.Add("width", "15%");
                    gvreturnlist.HeaderRow.Style.Add("font-size", "10px");
                    gvreturnlist.Style.Add("text-decoration", "none");
                    gvreturnlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvreturnlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StockReturnDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Stock Return");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StockReturnDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private DataTable GetDatafromDatabase()
        {
            List<MedIndentData> DesignationTypeDetails = GetReturnItemList(0);
            List<MedIndentDatatoExcel> ListexcelData = new List<MedIndentDatatoExcel>();
            int i = 0;
            foreach (MedIndentData row in DesignationTypeDetails)
            {
                MedIndentDatatoExcel ExcelSevice = new MedIndentDatatoExcel();
                ExcelSevice.ID = DesignationTypeDetails[i].ID;
                ExcelSevice.ReturnNo = DesignationTypeDetails[i].ReturnNo;
                ExcelSevice.ReturnQty = DesignationTypeDetails[i].TotReturnQty;
                ExcelSevice.ReturnDate = DesignationTypeDetails[i].AddedDate;
                ExcelSevice.ReturnBy = DesignationTypeDetails[i].EmpName;
                ListexcelData.Add(ExcelSevice);
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
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
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
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";

                ddlexport.Focus();
                return;
            }
        }

    }
}