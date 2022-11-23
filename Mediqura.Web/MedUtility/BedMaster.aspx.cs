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

namespace Mediqura.Web.MedUtility
{
    public partial class BedMaster : BasePage
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
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetLookupsList(LookupName.FloorType));
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
            ViewState["ID"] = null;
        }
        protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_block.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetfloorByblockID(Convert.ToInt32(ddl_block.SelectedValue == "" ? "0" : ddl_block.SelectedValue)));
            }
        }
        protected void ddl_floor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_floor.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetWardByFloorID(Convert.ToInt32(ddl_floor.SelectedValue == "" ? "0" : ddl_floor.SelectedValue)));
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getautoroomnos(string prefixText, int count, string contextKey)
        {
            BedMasterData Objpaic = new BedMasterData();
            BedMasterBO objInfoBO = new BedMasterBO();
            List<BedMasterData> getResult = new List<BedMasterData>();
            Objpaic.Room = prefixText;
            getResult = objInfoBO.Getautorooms(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Room.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getautobednos(string prefixText, int count, string contextKey)
        {
            BedMasterData Objpaic = new BedMasterData();
            BedMasterBO objInfoBO = new BedMasterBO();
            List<BedMasterData> getResult = new List<BedMasterData>();
            Objpaic.BedNo = prefixText;
            getResult = objInfoBO.Getautobednos(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BedNo.ToString());
            }
            return list;
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
                if (ddl_block.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Block", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_block.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_floor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Floor", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_floor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_ward.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Ward", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_ward.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_room.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_room.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_bedno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Bedno", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_bedno.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_charges.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Charge", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_charges.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtnursecharge.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "NurseCharge", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_charges.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                BedMasterData objWardMasterData = new BedMasterData();
                BedMasterBO objWardMasterBO = new BedMasterBO();
                objWardMasterData.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
                objWardMasterData.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
                objWardMasterData.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
                objWardMasterData.Room = txt_room.Text == "" ? null : txt_room.Text;
                objWardMasterData.BedNo = txt_bedno.Text == "" ? null : txt_bedno.Text;
                objWardMasterData.Charges = Convert.ToDecimal(txt_charges.Text == "" ? "0.0" : txt_charges.Text);
                objWardMasterData.NuCharges = Convert.ToDecimal(txtnursecharge.Text == "" ? "0.0" : txtnursecharge.Text);
                objWardMasterData.Consumable = Convert.ToDecimal(txt_consumablecharge.Text == "" ? "0.0" : txt_consumablecharge.Text);
                objWardMasterData.EmployeeID = LogData.EmployeeID;
                objWardMasterData.IsActive = ddl_status.SelectedValue == "0" ? true : false;
                objWardMasterData.HospitalID = LogData.HospitalID;
                objWardMasterData.FinancialYearID = LogData.FinancialYearID;
                objWardMasterData.ActionType = Enumaction.Insert;
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
                        objWardMasterData.ActionType = Enumaction.Update;
                        objWardMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objWardMasterBO.UpdateBedDetails(objWardMasterData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bind_grid();
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
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
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";

            }
        }
        protected void GvBedType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    BedMasterData objBlockMasterData = new BedMasterData();
                    BedMasterBO objBlockMasterBO = new BedMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvBedType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblroomtypecodeID");
                    objBlockMasterData.ID = Convert.ToInt32(ID.Text);
                    objBlockMasterData.ActionType = Enumaction.Select;

                    List<BedMasterData> GetResult = objBlockMasterBO.GetBedTypeDetailsByID(objBlockMasterData);
                    if (GetResult.Count > 0)
                    {
                        ddl_block.SelectedValue = GetResult[0].BlockID.ToString();
                        ddl_floor.SelectedValue = GetResult[0].FloorID.ToString();
                        ddl_ward.SelectedValue = GetResult[0].WardID.ToString();
                        txt_room.Text = GetResult[0].Room;
                        txt_bedno.Text = GetResult[0].BedNo;
                        txt_charges.Text = Commonfunction.Getrounding(GetResult[0].Charges.ToString());
                        txtnursecharge.Text = Commonfunction.Getrounding(GetResult[0].NuCharges.ToString());
                        txt_consumablecharge.Text = Commonfunction.Getrounding(GetResult[0].Consumable.ToString());
                        ViewState["ID"] = GetResult[0].ID;
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
                    BedMasterData objBlockMasterData = new BedMasterData();
                    BedMasterBO objBlockMasterBO = new BedMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvBedType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblroomtypecodeID");
                    objBlockMasterData.ID = Convert.ToInt32(ID.Text);
                    objBlockMasterData.EmployeeID = LogData.EmployeeID;
                    objBlockMasterData.ActionType = Enumaction.Delete;
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
                        objBlockMasterData.Remarks = txtremarks.Text;
                    }

                    BedMasterBO objBlockMasterBO1 = new BedMasterBO();
                    int Result = objBlockMasterBO1.DeleteBedTypeDetailsByID(objBlockMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SuccesAlert";
                        bind_grid();
                    }
                    else
                    {

                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

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
        private void bind_grid()
        {
            try
            {

                List<BedMasterData> lstemp = GetBedDetails(0);

                if (lstemp.Count > 0)
                {
                  
                    GvBedType.DataSource = lstemp;
                    GvBedType.DataBind();
                    GvBedType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvBedType.DataSource = null;
                    GvBedType.DataBind();
                    GvBedType.Visible = true;
                    lblresult.Visible = false;
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
            }
        }
        private List<BedMasterData> GetBedType(int p)
        {
            BedMasterData objFloorMasterData = new BedMasterData();
            BedMasterBO objBlockMasterBO = new BedMasterBO();
            objFloorMasterData.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objFloorMasterData.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objFloorMasterData.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            objFloorMasterData.Room = txt_room.Text == "" ? null : txt_room.Text;
            objFloorMasterData.BedNo = txt_bedno.Text == "" ? "0" : txt_bedno.Text;
            objFloorMasterData.CurrentIndex = p;
            objFloorMasterData.IsActive = ddl_status.SelectedValue == "0" ? true : false;
            return objBlockMasterBO.SearchBedTypeDetails(objFloorMasterData);
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


            bind_grid();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            Response.Redirect("BedMaster.aspx",false);
        }
        private void clear_all()
        {
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddl_block.SelectedValue = "0";
            ddl_floor.SelectedValue = "0";
            ddl_ward.SelectedValue = "0";
            ddlexport.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            ViewState["ID"] = null;
            txt_bedno.Text = "";
            txt_room.Text = "";
            txt_charges.Text = "";
            GvBedType.DataSource = null;
            GvBedType.DataBind();
            GvBedType.Visible = false;
            txtnursecharge.Text = "";
            txt_consumablecharge.Text = "";
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvBedType.AllowPaging = false;
                    GvBedType.DataSource = GetBedDetails(0);
                    GvBedType.DataBind();
  
                    GvBedType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvBedType.Columns[11].Visible = false;
                    GvBedType.Columns[12].Visible = false;
                    GvBedType.Columns[13].Visible = false;
                    GvBedType.RenderControl(hw);
                    GvBedType.HeaderRow.Style.Add("width", "15%");
                    GvBedType.HeaderRow.Style.Add("font-size", "10px");
                    GvBedType.Style.Add("text-decoration", "none");
                    GvBedType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvBedType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=BlockTypeDetails.pdf");
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
                wb.Worksheets.Add(dt, "Bed Type Detail List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=BedTypeDetails.xlsx");
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
            List<BedMasterData> WardTypeDetails = GetBedDetails(0);
            List<BedDatatoExcel> ListexcelData1 = new List<BedDatatoExcel>();
            int i = 0;
            foreach (BedMasterData row in WardTypeDetails)
            {
                BedDatatoExcel ExcelSevice = new BedDatatoExcel();
                ExcelSevice.ID = WardTypeDetails[i].ID;
                ExcelSevice.Block = WardTypeDetails[i].Block;
                ExcelSevice.Floor1 = WardTypeDetails[i].Floor1;
                ExcelSevice.Ward = WardTypeDetails[i].Ward;
                ExcelSevice.Room = WardTypeDetails[i].Room;
                ExcelSevice.BedNo = WardTypeDetails[i].BedNo;
                ExcelSevice.Charges = WardTypeDetails[i].Charges;
                ExcelSevice.NuCharges = WardTypeDetails[i].NuCharges;
                ExcelSevice.AddedBy = WardTypeDetails[i].EmpName;
                GvBedType.Columns[4].Visible = false;
                GvBedType.Columns[5].Visible = false;
                GvBedType.Columns[6].Visible = false;
                GvBedType.Columns[7].Visible = false;
                ListexcelData1.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData1);
            return dt;
        }

        private List<BedMasterData> GetBedDetails(int p)
        {
            BedMasterData objFloorMasterData = new BedMasterData();
            BedMasterBO objBlockMasterBO = new BedMasterBO();
            objFloorMasterData.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objFloorMasterData.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objFloorMasterData.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            objFloorMasterData.Room = txt_room.Text == "" ? null : txt_room.Text;
            objFloorMasterData.BedNo = txt_bedno.Text == "" ? "0" : txt_bedno.Text;
            objFloorMasterData.IsActive = ddl_status.SelectedValue == "0" ? true : false;
            return objBlockMasterBO.SearchBedDetails(objFloorMasterData);

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
                ddlexport.Focus();
                return;
            }
        }
        protected void GvBedType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvBedType.PageIndex = e.NewPageIndex;
            bind_grid();
        }
    }
}