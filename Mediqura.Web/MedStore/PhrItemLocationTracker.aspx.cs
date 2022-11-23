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
    public partial class PhrItemLocationTracker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                supplementoryvalues();
            }
        }
        protected void supplementoryvalues()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Session["subracklist"] = null;
            Session["racklist"] = null;
            List<LookupItem> racklist = Session["racklist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["racklist"];
            Session["racklist"] = mstlookup.GetLookupsList(LookupName.Rack);
            List<LookupItem> subracklist = Session["subracklist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["subracklist"];
            Session["subracklist"] = mstlookup.GetLookupsList(LookupName.SubRack);
        }

        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_store, mstlookup.GetLookupsList(LookupName.StockType));
            Commonfunction.PopulateDdl(ddl_rack, mstlookup.GetLookupsList(LookupName.Rack));
            Commonfunction.PopulateDdl(ddl_subrack, mstlookup.GetLookupsList(LookupName.SubRack));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        protected void ddl_store_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_store.SelectedIndex > 0)
            {
                //btnsave.Attributes.Remove("disabled");
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_rack, mstlookup.GetRackByID(Convert.ToInt32(ddl_store.SelectedValue)));
            }
            else
            {
                //    btnsave.Attributes["disabled"] = "disabled";
            }
        }
        protected void ddl_rack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_rack.SelectedIndex > 0)
            {
                //btnsave.Attributes.Remove("disabled");
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subrack, mstlookup.GetSubRackByID(Convert.ToInt32(ddl_rack.SelectedValue)));
            }
            else
            {
                //    btnsave.Attributes["disabled"] = "disabled";
            }
        }
        protected void ddl_subrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_subrack.SelectedIndex > 0)
            {
                txt_itemname.ReadOnly = false;
                AutoCompleteExtender2.ContextKey = ddl_subrack.SelectedValue;
            }
            else
            {
                AutoCompleteExtender2.ContextKey = null;
                txt_itemname.ReadOnly = true;
            }
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
            bindgrid();
        }
        private void bindgrid()
        {
            try
            {

                List<RackMasterData> lstemp = GetItemLocationDetails(0);

                if (lstemp.Count > 0)
                {
                    GvItemLocationType.DataSource = lstemp;
                    GvItemLocationType.DataBind();
                    GvItemLocationType.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                    GvItemLocationType.DataSource = null;
                    GvItemLocationType.DataBind();
                    GvItemLocationType.Visible = true;
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
        protected void btn_save_Click(object sender, EventArgs e)
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
                if (ddl_store.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_rack.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<RackMasterData> List = new List<RackMasterData>();
                RackMasterData objdata = new RackMasterData();
                RackMasterBO objBO = new RackMasterBO();
                foreach (GridViewRow row in GvItemLocationType.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label StockID = (Label)GvItemLocationType.Rows[row.RowIndex].Cells[0].FindControl("lblid");
                    Label ItemID = (Label)GvItemLocationType.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    DropDownList rackid = (DropDownList)GvItemLocationType.Rows[row.RowIndex].Cells[0].FindControl("ddl_rack");
                    DropDownList subrackid = (DropDownList)GvItemLocationType.Rows[row.RowIndex].Cells[0].FindControl("ddl_subrack");
                    TextBox itemlocation = (TextBox)GvItemLocationType.Rows[row.RowIndex].Cells[0].FindControl("txt_itemlocation");
                    RackMasterData obj = new RackMasterData();
                    obj.StockID = Convert.ToInt64(StockID.Text);
                    obj.ItemID = Convert.ToInt64(ItemID.Text);
                    obj.RackID = Convert.ToInt32(rackid.SelectedValue);
                    obj.SubRackID = Convert.ToInt32(subrackid.SelectedValue);
                    obj.ItemLocation = itemlocation.Text;
                    List.Add(obj);
                }
                objdata.XMLData = XmlConvertor.ItemLocationRecordDatatoXML(List).ToString();
                objdata.StockTypeID = Convert.ToInt32(ddl_store.SelectedValue == "" ? "0" : ddl_store.SelectedValue);
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.IPaddress = LogData.IPaddress;
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.ActionType = Enumaction.Insert;
                int result = objBO.UpdateItemLocationDetails(objdata);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid();
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


            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            clear_all();
        }
        private void clear_all()
        {
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddl_store.SelectedValue = "0";
            ddlexport.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            ViewState["ID"] = null;
            txt_itemname.Text = "";
            GvItemLocationType.DataSource = null;
            GvItemLocationType.DataBind();
            GvItemLocationType.Visible = false;
            txtdatefrom.Text = "";
            txtto.Text = "";
        }
        protected void GvItemLocationType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             MasterLookupBO mstlookup = new MasterLookupBO();
             List<LookupItem> racklist = Session["racklist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["racklist"];
             List<LookupItem> subracklist = Session["subracklist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["subracklist"];
          
                    foreach (GridViewRow row in GvItemLocationType.Rows)
                    {  
                        try
                        {
                        DropDownList ddl1 = (DropDownList)GvItemLocationType.Rows[row.RowIndex].Cells[2].FindControl("ddl_rack");
                        DropDownList ddl2 = (DropDownList)GvItemLocationType.Rows[row.RowIndex].Cells[3].FindControl("ddl_subrack");
                        Label rackid = (Label)GvItemLocationType.Rows[row.RowIndex].Cells[2].FindControl("lbl_RackID");
                        Label subrackid = (Label)GvItemLocationType.Rows[row.RowIndex].Cells[3].FindControl("lbl_SubRackID");
                        Commonfunction.PopulateDdl(ddl1, racklist);
                        Commonfunction.PopulateDdl(ddl2, subracklist);

                        if (rackid.Text != "0")
                        {
                            ddl1.Items.FindByValue(rackid.Text).Selected = true;
                        }
                        if (subrackid.Text != "0")
                        {
                            ddl2.Items.FindByValue(subrackid.Text).Selected = true;
                        }
                    }
          
                 catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
                    }
        
        }
        private List<RackMasterData> GetItemLocationDetails(int p)
        {
            RackMasterData objItemMasterData = new RackMasterData();
            RackMasterBO objitemMasterBO = new RackMasterBO();
            objItemMasterData.StockTypeID = Convert.ToInt32(ddl_store.SelectedValue == "" ? "0" : ddl_store.SelectedValue);
            objItemMasterData.RackID = Convert.ToInt32(ddl_rack.SelectedValue == "" ? "0" : ddl_rack.SelectedValue);
            objItemMasterData.SubRackID = Convert.ToInt32(ddl_subrack.SelectedValue == "" ? "0" : ddl_subrack.SelectedValue);
            var source = txt_itemname.Text.Trim();
            if (source.Contains(":"))
            {
                string ID1 = source.Substring(source.LastIndexOf(':') + 1);
                objItemMasterData.ItemID = Convert.ToInt32(ID1);
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objItemMasterData.DateFrom = from;
            objItemMasterData.DateTo = to;

            return objitemMasterBO.SearchItemLocationDetails(objItemMasterData);
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
                Response.AddHeader("content-disposition", "attachment;filename=ItemLocationDetails.xlsx");
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
            List<RackMasterData> TypeDetails = GetItemLocationDetails(0);
            List<ItemLocationDatatoExcel> ListexcelData1 = new List<ItemLocationDatatoExcel>();
            int i = 0;
            foreach (RackMasterData row in TypeDetails)
            {
                ItemLocationDatatoExcel ExcelSevice = new ItemLocationDatatoExcel();
                ExcelSevice.ItemName = TypeDetails[i].ItemName;
                ExcelSevice.BatchNo = TypeDetails[i].BatchNo;
                ExcelSevice.StockNo = TypeDetails[i].StockNo;
                ExcelSevice.RackNumber = TypeDetails[i].RackNumber;
                ExcelSevice.SubRack = TypeDetails[i].SubRack;
                ExcelSevice.ItemLocation = TypeDetails[i].ItemLocation;
                ListexcelData1.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData1);
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
                //      ExportToPdf();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void GvItemLocationType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvItemLocationType.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}