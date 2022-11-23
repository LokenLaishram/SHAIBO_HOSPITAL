using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedGenStoreBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStoreData;
using Mediqura.CommonData.MedStore;
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

namespace Mediqura.Web.MedGenUtility
{
    public partial class GenStoreItemMaster : BasePage
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
            Commonfunction.PopulateDdl(ddl_group, mstlookup.GetLookupsList(LookupName.GenGroups));
            Commonfunction.PopulateDdl(ddl_unit, mstlookup.GetLookupsList(LookupName.GENPhrUnit));
            //Commonfunction.PopulateDdl(ddl_rack, mstlookup.GetLookupsList(LookupName.GEN_Rack));
            Commonfunction.PopulateDdl(ddl_sound, mstlookup.GetLookupsList(LookupName.GEN_sounds));
            Commonfunction.PopulateDdl(ddl_looks, mstlookup.GetLookupsList(LookupName.GEN_looks));
            Commonfunction.Insertzeroitemindex(ddl_subgroup);
            //Commonfunction.Insertzeroitemindex(ddl_Subrack);
            Span77.Visible = false;
            Span78.Visible = false;
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
                if (ddl_unit.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SUnit", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_unit.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_category.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "producCtg", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_category.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                GenItemMasterData objitemData = new GenItemMasterData();
                GenItemMasterBO objitemBO = new GenItemMasterBO();
                objitemData.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "0" ? null : ddl_group.SelectedValue);
                objitemData.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "0" ? null : ddl_subgroup.SelectedValue);
                objitemData.ItemName = txt_itemName.Text == "" ? null : txt_itemName.Text;
                objitemData.PhrUnitID = Convert.ToInt32(ddl_unit.SelectedValue == "0" ? null : ddl_unit.SelectedValue);
                //objitemData.GenRackID = Convert.ToInt32(ddl_rack.SelectedValue == "0" ? null : ddl_rack.SelectedValue);
                //objitemData.GenSubRackID = Convert.ToInt32(ddl_Subrack.SelectedValue == "0" ? null : ddl_Subrack.SelectedValue);
                objitemData.GenSoundsID = Convert.ToInt32(ddl_sound.SelectedValue == "0" ? null : ddl_sound.SelectedValue);
                objitemData.GenLooksID = Convert.ToInt32(ddl_looks.SelectedValue == "0" ? null : ddl_looks.SelectedValue);
                objitemData.ItemCategoryID = Convert.ToInt32(ddl_category.SelectedValue == "0" ? null : ddl_category.SelectedValue);
                objitemData.Remarks = txt_remarks.Text == "" ? null : txt_remarks.Text;
                objitemData.EmployeeID = LogData.EmployeeID;
                objitemData.HospitalID = LogData.HospitalID;
                objitemData.IPaddress = LogData.IPaddress;
                objitemData.DaycountStart = Convert.ToInt32(txt_StarthightDate.Text == "" ? "0" : txt_StarthightDate.Text);
                objitemData.DaycountEnd = Convert.ToInt32(txt_enddate.Text == "" ? "0" : txt_enddate.Text);
                objitemData.FinancialYearID = LogData.FinancialYearID;
                objitemData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objitemData.ActionType = Enumaction.Insert;
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
                    cleaer();
                    bindgrid(1);
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
        protected void GvItemMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    GenItemMasterData objitemData = new GenItemMasterData();
                    GenItemMasterBO objitemBO = new GenItemMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvItemMaster.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("item");
                    objitemData.ID = Convert.ToInt32(ID.Text);
                    objitemData.ActionType = Enumaction.Select;

                    List<GenItemMasterData> GetResult = objitemBO.GetItemMasterDetailsByID(objitemData);
                    if (GetResult.Count > 0)
                    {
                        ddl_group.SelectedValue = GetResult[0].GroupID.ToString();
                        MasterLookupBO mstlookup = new MasterLookupBO();
                        Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetGENItemSubGroupByItemGroupID(Convert.ToInt32(GetResult[0].GroupID.ToString())));
                        ddl_subgroup.SelectedValue = GetResult[0].SubGroupID.ToString();
                        ddl_unit.SelectedValue = GetResult[0].PhrUnitID.ToString();

                        txt_itemName.Text = GetResult[0].ItemName;
                        txt_StarthightDate.Text = Commonfunction.Getrounding(GetResult[0].DaycountStart.ToString());
                        txt_enddate.Text = Commonfunction.Getrounding(GetResult[0].DaycountEnd.ToString());
                        txt_remarks.Text = GetResult[0].Remarks;
                        ddl_category.SelectedValue = GetResult[0].ItemCategoryID.ToString();
                        //ddl_Subrack.SelectedValue = GetResult[0].GenSubRackID.ToString();
                        ddl_sound.SelectedValue = GetResult[0].GenSoundsID.ToString();
                        ddl_looks.SelectedValue = GetResult[0].GenLooksID.ToString();
                        ViewState["ID"] = GetResult[0].ID;
                    }
                    else
                    {


                    }
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    GenItemMasterData objitemData = new GenItemMasterData();
                    GenItemMasterBO objitemBO = new GenItemMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvItemMaster.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("item");
                    objitemData.ID = Convert.ToInt32(ID.Text);
                    objitemData.EmployeeID = LogData.EmployeeID;
                    objitemData.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objitemData.Remarks = txtremarks.Text;
                    }

                    GenItemMasterBO objitemBO1 = new GenItemMasterBO();
                    int Result = objitemBO1.DeleteItemMasterDetailsByID(objitemData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SuccesAlert";
                        bindgrid(1);
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";

            }
        }
        private void bindgrid(int page)
        {
            try
            {

                List<GenItemMasterData> lstemp = GetItemMaster(page);

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
        private List<GenItemMasterData> GetItemMaster(int p)
        {
            GenItemMasterData objitemData = new GenItemMasterData();
            GenItemMasterBO objitemBO = new GenItemMasterBO();
            objitemData.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue);
            objitemData.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
            objitemData.PhrUnitID = Convert.ToInt32(ddl_unit.SelectedValue == "0" ? null : ddl_unit.SelectedValue);
            objitemData.ItemCategoryID = Convert.ToInt32(ddl_category.SelectedValue == "0" ? null : ddl_category.SelectedValue);
            objitemData.GenSoundsID = Convert.ToInt32(ddl_sound.SelectedValue == "0" ? null : ddl_sound.SelectedValue);
            objitemData.GenLooksID = Convert.ToInt32(ddl_looks.SelectedValue == "0" ? null : ddl_looks.SelectedValue);
            objitemData.ItemName = txt_itemName.Text == "" ? "" : txt_itemName.Text;
            objitemData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objitemData.CurrentIndex = p;
            return objitemBO.SearchItemMasterDetails(objitemData);
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
            bindgrid(1);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            Commonfunction.Insertzeroitemindex(ddl_subgroup);
            ddl_unit.SelectedIndex = 0;
            btnsave.Attributes.Remove("disabled");
            txt_remarks.Text = "";
            Span77.Visible = false;
            Span78.Visible = false;
            txt_itemName.Text = "";
            txt_StarthightDate.Text = "";
            txt_enddate.Text = "";
            ddl_category.SelectedIndex = 0;
        }
        protected void ddl_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_group.SelectedIndex > 0)
            {
                btnsave.Attributes.Remove("disabled");
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetGENItemSubGroupByItemGroupID(Convert.ToInt32(ddl_group.SelectedValue)));
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
            }
            if (ddl_group.SelectedValue == "1")
            {
                Span77.Visible = true;
                Span78.Visible = true;
            }
            else
            {
                Span77.Visible = false;
                Span78.Visible = false;
            }
        }
        //protected void ddl_rack_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddl_rack.SelectedIndex > 0)
        //    {
        //        btnsave.Attributes.Remove("disabled");
        //        MasterLookupBO mstlookup = new MasterLookupBO();
        //        Commonfunction.PopulateDdl(ddl_Subrack, mstlookup.GetGENItemSubRackByItemRackID(Convert.ToInt32((ddl_rack.SelectedValue))));
        //    }
        //    else
        //    {
        //        btnsave.Attributes["disabled"] = "disabled";
        //    }
        //    if (ddl_rack.SelectedValue == "1")
        //    {
        //        Span77.Visible = true;
        //        Span78.Visible = true;
        //    }
        //    else
        //    {
        //        Span77.Visible = false;
        //        Span78.Visible = false;
        //    }
        //}
        private void clearall()
        {
            ddl_group.SelectedIndex = 0;
            ddl_subgroup.SelectedIndex = 0;
            ddl_looks.SelectedIndex = 0;
            ddl_sound.SelectedIndex = 0;
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
                    Response.AddHeader("content-disposition", "attachment;filename=StoreItemMasterDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=StoreItemMasterDetails.xlsx");
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

            List<GenItemMasterData> ItemMasterDetails = GetItemDetails(0);
            List<GENItemMasterDatatoExcel> ListexcelData = new List<GENItemMasterDatatoExcel>();
            int i = 0;
            foreach (GenItemMasterData row in ItemMasterDetails)
            {
                GENItemMasterDatatoExcel ExcelSevice = new GENItemMasterDatatoExcel();
                ExcelSevice.ID = ItemMasterDetails[i].ID;
                ExcelSevice.Groups = ItemMasterDetails[i].Groups;
                ExcelSevice.SubGroup = ItemMasterDetails[i].SubGroup;
                ExcelSevice.ItemName = ItemMasterDetails[i].ItemName;
                ExcelSevice.PhrUnit = ItemMasterDetails[i].PhrUnit;
                ExcelSevice.DaycountStart = ItemMasterDetails[i].DaycountStart;
                ExcelSevice.DaycountEnd = ItemMasterDetails[i].DaycountEnd;
                ExcelSevice.AddedBy = ItemMasterDetails[i].EmpName;
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

        private List<GenItemMasterData> GetItemDetails(int p)
        {
            GenItemMasterData objitemData = new GenItemMasterData();
            GenItemMasterBO objitemBO = new GenItemMasterBO();
            objitemData.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue);
            objitemData.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
            objitemData.GenSoundsID = Convert.ToInt32(ddl_sound.SelectedValue == "0" ? null : ddl_sound.SelectedValue);
            objitemData.GenLooksID = Convert.ToInt32(ddl_looks.SelectedValue == "0" ? null : ddl_looks.SelectedValue);
            objitemData.ItemName = txt_itemName.Text == "" ? "" : txt_itemName.Text;
            objitemData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objitemBO.SearchItemDetails(objitemData);

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
        protected void cleaer()
        {
            ddl_subgroup.SelectedIndex = 0;
            ddl_unit.SelectedIndex = 0;
            ddl_sound.SelectedIndex = 0;
            ddl_looks.SelectedIndex = 0;
            ddl_looks.SelectedIndex = 0;
            txt_itemName.Text = "";
            ddlstatus.SelectedIndex = 0;
            ddl_category.SelectedIndex = 0;
        }
        protected void GvItemMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cleaer();
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