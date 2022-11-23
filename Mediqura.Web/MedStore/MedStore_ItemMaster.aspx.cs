using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;
using System.Drawing;
using System.Data;
using System.Reflection;
using iTextSharp.text;



namespace Mediqura.Web.MedStore
{
    public partial class MedStore_ItemMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_group, mstlookup.GetLookupsList(LookupName.Groups));
            //Commonfunction.PopulateDdl(ddl_unit, mstlookup.GetLookupsList(LookupName.PhrUnit));
            //Commonfunction.PopulateDdl(ddl_sound, mstlookup.GetLookupsList(LookupName.PHR_sounds));
            //Commonfunction.PopulateDdl(ddl_looks, mstlookup.GetLookupsList(LookupName.PHR_looks));
            Commonfunction.PopulateDdl(ddl_mfgcompnay, mstlookup.GetLookupsList(LookupName.Mfgcompany));
            Commonfunction.Insertzeroitemindex(ddl_subgroup);
            //Span77.Visible = false;
            //Span78.Visible = false;
            txt_pageno.Text = "1";
            int P = Convert.ToInt32(txt_pageno.Text);
            bindgrid(P);

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
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
                if (ddl_group.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Group", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_group.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_subgroup.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Subgroup", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_subgroup.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_itemName.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_itemName.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
              
                if (ddl_mfgcompnay.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "mfgcompany", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_mfgcompnay.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                ItemMasterData objitemData = new ItemMasterData();
                MedStore_ItemMasterBO objitemBO = new MedStore_ItemMasterBO();
                objitemData.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "0" ? null : ddl_group.SelectedValue);
                objitemData.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "0" ? null : ddl_subgroup.SelectedValue);
                objitemData.ItemName = txt_itemName.Text == "" ? null : txt_itemName.Text;
                objitemData.EmployeeID = LogData.EmployeeID;
                objitemData.HospitalID = LogData.HospitalID;
                objitemData.IPaddress = LogData.IPaddress;
               
                objitemData.FinancialYearID = LogData.FinancialYearID;
                objitemData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objitemData.ActionType = Enumaction.Insert;
                objitemData.MfgCompanyID = Convert.ToInt32(ddl_mfgcompnay.SelectedValue == "0" ? null : ddl_mfgcompnay.SelectedValue);
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        objitemData.ActionType = Enumaction.Update;
                        objitemData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objitemBO.UpdateItemMasterDetails(objitemData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    //btnsave.Attributes["disabled"] = "disabled";
                    int P = Convert.ToInt32(txt_pageno.Text == "" || txt_pageno.Text == "0" ? "1" : txt_pageno.Text);
                    clear();
                    bindgrid(P);
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
        }
    
        private void bindgrid(int page)
        {
            try
            {

                List<ItemMasterData> lstemp = GetItemMaster(page);

                if (lstemp.Count > 0)
                {
                    GvItemMaster.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvItemMaster.PageIndex = page - 1;
                    GvItemMaster.DataSource = lstemp;
                    GvItemMaster.DataBind();
                    GvItemMaster.Visible = true;
                    //for custom paging
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                    GvItemMaster.DataSource = null;
                    GvItemMaster.DataBind();
                    GvItemMaster.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
        }
        private List<ItemMasterData> GetItemMaster(int p)
        {
            ItemMasterData objItemMasterData = new ItemMasterData();
            MedStore_ItemMasterBO objitemMasterBO = new MedStore_ItemMasterBO();
            objItemMasterData.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue);
            objItemMasterData.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
            objItemMasterData.ItemName = txt_itemName.Text == "" ? "" : txt_itemName.Text;
            objItemMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objItemMasterData.MfgCompanyID = Convert.ToInt32(ddl_mfgcompnay.SelectedValue == "0" ? null : ddl_mfgcompnay.SelectedValue);
            objItemMasterData.CurrentIndex = p;
            return objitemMasterBO.SearchItemMasterDetails(objItemMasterData);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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
            int pagenumber = Convert.ToInt32(txt_itemName.Text != "" || txt_pageno.Text == "" || txt_pageno.Text == "0" ? "1" : txt_pageno.Text);
            bindgrid(pagenumber);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            Commonfunction.Insertzeroitemindex(ddl_subgroup);
          
            btnsave.Attributes.Remove("disabled");
            txt_itemName.Text = "";
            ddl_mfgcompnay.SelectedIndex = 0;
            // clear();
            bindgrid(1);
            txt_pageno.Text = "";
        }
        protected void ddl_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_group.SelectedIndex > 0)
            {
                btnsave.Attributes.Remove("disabled");
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetItemSubGroupByItemGroupID(Convert.ToInt32(ddl_group.SelectedValue)));
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
            }
           
        }
        private void clearall()
        {
            ddl_group.SelectedIndex = 0;
            ddl_subgroup.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            txt_itemName.Text = "";
            GvItemMaster.DataSource = null;
            GvItemMaster.DataBind();
            GvItemMaster.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;

        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvItemMaster.AllowPaging = false;
                    GvItemMaster.DataSource = GetItemDetails(0);
                    GvItemMaster.DataBind();
                    GvItemMaster.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvItemMaster.Columns[9].Visible = false;
                    GvItemMaster.Columns[10].Visible = false;
                    GvItemMaster.Columns[11].Visible = false;

                    GvItemMaster.RenderControl(hw);
                    GvItemMaster.HeaderRow.Style.Add("width", "15%");
                    GvItemMaster.HeaderRow.Style.Add("font-size", "10px");
                    GvItemMaster.Style.Add("text-decoration", "none");
                    GvItemMaster.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvItemMaster.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ItemMasterDetails.pdf");
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
                wb.Worksheets.Add(dt, "Item Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ItemMasterDetails.xlsx");
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

            List<ItemMasterData> ItemMasterDetails = GetItemDetails(0);
            List<ItemMasterDatatoExcel> ListexcelData = new List<ItemMasterDatatoExcel>();
            int i = 0;
            foreach (ItemMasterData row in ItemMasterDetails)
            {
                ItemMasterDatatoExcel ExcelSevice = new ItemMasterDatatoExcel();
                ExcelSevice.ID = ItemMasterDetails[i].ID;
                ExcelSevice.Groups = ItemMasterDetails[i].Groups;
                ExcelSevice.SubGroup = ItemMasterDetails[i].SubGroup;
                ExcelSevice.PHRSounds = ItemMasterDetails[i].PHRSounds;
                ExcelSevice.PHRLooks = ItemMasterDetails[i].PHRLooks;
                ExcelSevice.ItemName = ItemMasterDetails[i].ItemName;
                ExcelSevice.PhrUnit = ItemMasterDetails[i].PhrUnit;
                ExcelSevice.DaycountStart = ItemMasterDetails[i].DaycountStart;
                ExcelSevice.DaycountEnd = ItemMasterDetails[i].DaycountEnd;
                ExcelSevice.AddedBy = ItemMasterDetails[i].EmpName;
                ExcelSevice.AddedDate = ItemMasterDetails[i].AddedDate;
                GvItemMaster.Columns[5].Visible = false;
                GvItemMaster.Columns[6].Visible = false;
                GvItemMaster.Columns[7].Visible = false;
                GvItemMaster.Columns[8].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }

        private List<ItemMasterData> GetItemDetails(int p)
        {
            ItemMasterData objItemMasterData = new ItemMasterData();
            MedStore_ItemMasterBO objitemMasterBO = new MedStore_ItemMasterBO();
            objItemMasterData.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue);
            objItemMasterData.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
            objItemMasterData.ItemName = txt_itemName.Text == "" ? "" : txt_itemName.Text;
            objItemMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objitemMasterBO.SearchItemDetails(objItemMasterData);

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
        protected void clear()
        {
            ddl_group.SelectedIndex = 0;
            ddl_subgroup.SelectedIndex = 0;
          
            txt_itemName.Text = "";
            ddlstatus.SelectedIndex = 0;
            ddl_mfgcompnay.SelectedIndex = 0;
        }
        protected void GvItemMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ddl_group.SelectedIndex = 0;
            ddl_subgroup.SelectedIndex = 0;
           
            //txt_itemName.Text = "";
            ddlstatus.SelectedIndex = 0;
            ddl_mfgcompnay.SelectedIndex = 0;
            txt_pageno.Text = (e.NewPageIndex + 1).ToString();
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
    }
}