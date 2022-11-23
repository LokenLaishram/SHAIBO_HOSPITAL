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
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.Utility;
using Mediqura.CommonData.MedHouseKeepingData;
using Mediqura.BOL.MedHouseKeepingBO;

namespace Mediqura.Web.MedHouseKeeping
{
    public partial class BedStatus :  BasePage
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
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.Insertzeroitemindex(ddl_floor);
            Commonfunction.Insertzeroitemindex(ddl_ward);
            ddl_bedstatus.SelectedIndex = 1;
            //ddl_bedstatus.Attributes["disabled"] = "disabled";
            bindgrid(0);
        }
        protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_block.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetfloorByblockID(Convert.ToInt32(ddl_block.SelectedValue)));
            }
        }
        protected void ddl_floor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_floor.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetWardByFloorID(Convert.ToInt32(ddl_floor.SelectedValue)));
            }
        }
        //protected void ddl_workstatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddl_workstatus.SelectedIndex == 1)
        //    {
        //        ddl_bedstatus.SelectedIndex = 0;
        //    }
        //    else if(ddl_workstatus.SelectedIndex ==0)
        //    {
        //        ddl_bedstatus.SelectedIndex = 1;
        //    }
        //}
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
            bindgrid(0);

        }
        private void bindgrid(int p)
        {
            List<BedStatusData> objdeposit = GetBedStatusDetails(0);
            for (int i = 0; i < objdeposit.Count; i++)
            {
                //if (objdeposit[i].bed_status == 1)
                //{
                //    objdeposit[i].bedstatus = "Vacant";
                //}
                //if (objdeposit[i].bed_status == 2)
                //{
                //    objdeposit[i].bedstatus = "Occupied";
                //} 
                //if (objdeposit[i].bed_status == 3)
                //{
                //    objdeposit[i].bedstatus = "Discharge process";
                //}
                if (objdeposit[i].bed_status == 4)
                {
                    objdeposit[i].bedstatus = "Under maintenance";
                }
            }
            if (objdeposit.Count > 0)
            {
                GvBedStatus.DataSource = objdeposit;
                GvBedStatus.DataBind();
                GvBedStatus.Visible = true;
                Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record found", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
                divmsg3.Visible = true;
                btnexport.Visible = true;
                ddlexport.Visible = true;

            }
            else
            {
                GvBedStatus.DataSource = null;
                GvBedStatus.DataBind();
                GvBedStatus.Visible = true;
                divmsg3.Visible = false;
                lblresult.Visible = false;
                btnexport.Visible = false;
                ddlexport.Visible = false;

            }
        }
        private List<BedStatusData> GetBedStatusDetails(int p)
        {
            BedStatusData objpat = new BedStatusData();
            BedStatusBO objbillingBO = new BedStatusBO();
            objpat.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            objpat.bed_status = Convert.ToInt16(ddl_bedstatus.SelectedValue == "0" ? null : ddl_bedstatus.SelectedValue);
            //objpat.WorkingStatusID = Convert.ToInt16(ddl_workstatus.SelectedValue == "0" ? null : ddl_workstatus.SelectedValue);
            return objbillingBO.GetBedStatusDetails(objpat);
        }
        protected void GvBedStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Bedstatus = (Label)e.Row.FindControl("lbl_status");
                Label workstatus = (Label)e.Row.FindControl("lblworkstatus");
                Button status = (Button)e.Row.FindControl("btnstatus");
                Label lblbedstatus = (Label)e.Row.FindControl("lblbedstatus");
                
                if (workstatus.Text == "1")
                {
                    status.Text = "Complete";
                    status.Enabled = true;
                }
                else if (workstatus.Text == "0")
                {
                    status.Text = "Start";
                    status.Enabled = true;
                }
                         
                if (Bedstatus.Text == "4")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Blue;
                    e.Row.Cells[6].ForeColor = Color.White;
                    lblbedstatus.ForeColor = Color.White;
              
                }
             }
        }
        protected void GvBedStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "statusupdate")
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
                    }
                    BedStatusData obj = new BedStatusData();
                    BedStatusBO objInfoBO = new BedStatusBO();
                    int i = Convert.ToInt32(e.CommandArgument.ToString());
                    GridViewRow pt = GvBedStatus.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblID");
                    Label status = (Label)pt.Cells[0].FindControl("lblworkstatus");
                    Button btnstatus = (Button)pt.Cells[0].FindControl("btnstatus");
                                 
                    obj.BedID = Convert.ToInt32(ID.Text);
                    obj.EmployeeID = LogData.EmployeeID;

                    if (Convert.ToInt32(status.Text)== 2)
                    {
                        obj.WorkingStatusID = 0;
                    }
                    else if (Convert.ToInt32(status.Text) == 1)
                    {
                        obj.WorkingStatusID = 2;
                    }
                    else if (Convert.ToInt32(status.Text) == 0)
                    {
                        obj.WorkingStatusID = 1;
                    }

                    int result = objInfoBO.UpdateBedStatus(obj);
                    if (result > 0)
                    {
                        
                        Messagealert_.ShowMessage(lblresult, "update", 1);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "SucessAlert";
                        bindgrid(0);
                    }
                    else
                    {
                        GvBedStatus.DataSource = null;
                        GvBedStatus.DataBind();
                        divmsg3.Visible = false;
                     
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
        protected void btnresets_Click(object sender, EventArgs e)
        {
            GvBedStatus.DataSource = null;
            GvBedStatus.DataBind();
            GvBedStatus.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            divmsg1.Visible = false;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.Insertzeroitemindex(ddl_floor);
            Commonfunction.Insertzeroitemindex(ddl_ward);
            ddl_bedstatus.SelectedIndex = 1;
            btnexport.Visible = false;
            ddlexport.Visible = false;
        //    ddl_bedstatus.Attributes["disabled"] = "disabled";
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
            else
            {
                Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Bed Status Details");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=BedStatusDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<BedStatusData> BedStatusDetails = GetBedStatusDetails(0);
            List<BedStatusDataToExcel> ListexcelData = new List<BedStatusDataToExcel>();
            int i = 0;
            foreach (BedStatusData row in BedStatusDetails)
            {
                BedStatusDataToExcel Ecxeclemp = new BedStatusDataToExcel();
                Ecxeclemp.Block = BedStatusDetails[i].Block;
                Ecxeclemp.Floor1 = BedStatusDetails[i].Floor1;
                Ecxeclemp.Ward = BedStatusDetails[i].Ward;
                Ecxeclemp.Room = BedStatusDetails[i].Room;
                Ecxeclemp.BedNo = BedStatusDetails[i].BedNo;
                if (BedStatusDetails[i].bed_status == 1)
                {
                    Ecxeclemp.bedstatus = "Vacant";
                }
                if (BedStatusDetails[i].bed_status == 2)
                {
                    Ecxeclemp.bedstatus = "Occupied";
                }
                if (BedStatusDetails[i].bed_status == 3)
                {
                    Ecxeclemp.bedstatus = "Discharge process";
                }
                if (BedStatusDetails[i].bed_status == 4)
                {
                    Ecxeclemp.bedstatus = "Under maintenance";
                }
                if (BedStatusDetails[i].WorkingStatusID == 1)
                {
                    Ecxeclemp.WorkingStatus = "Started";
                }
                else if (BedStatusDetails[i].WorkingStatusID ==2)
                {
                    Ecxeclemp.WorkingStatus = "Completed";
                }
                else
                {
                    Ecxeclemp.WorkingStatus = " ";
                }

                ListexcelData.Add(Ecxeclemp);
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

  
    }
}