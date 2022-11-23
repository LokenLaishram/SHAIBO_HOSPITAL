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

namespace Mediqura.Web.MedAdmission
{
    public partial class PatAdmissionToWard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                bindgrid(0);
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.Insertzeroitemindex(ddl_floor);
            Commonfunction.Insertzeroitemindex(ddl_ward);
            // txt_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
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
            List<PatAdmToWardData> objdeposit = GetPatAdmToWardDetails(0);
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNoWithName(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNoWithNameAgeNAddress(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        private List<PatAdmToWardData> GetPatAdmToWardDetails(int p)
        {
            PatAdmToWardData objpat = new PatAdmToWardData();
            PatAdmToWardBO objbillingBO = new PatAdmToWardBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objpat.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            objpat.AdmToWardStatusID = Convert.ToInt16(ddl_workstatus.SelectedValue == "0" ? "0" : ddl_workstatus.SelectedValue);
            DateTime date = txt_date.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_date.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.Date = date;
            if (txtautoIPNo.Text != "")
            {
                string IPNo;
                var source = txtautoIPNo.Text.ToString();
                if (source.Contains(":"))
                {
                    IPNo = source.Substring(source.LastIndexOf(':') + 1);
                    objpat.IPNo = IPNo.ToString();
                }

                else
                {
                    objpat.IPNo = txtautoIPNo.Text.Trim() == "" ? "" : txtautoIPNo.Text.Trim();
                }
            }
            else
            {
                objpat.IPNo = txtautoIPNo.Text.Trim() == "" ? "" : txtautoIPNo.Text.Trim();
            }

            return objbillingBO.GetPatAdmToWardDetails(objpat);
        }
        protected void GvBedStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button updateButtonField = e.Row.Cells[10].Controls[0] as Button;
                updateButtonField.Attributes["onclick"] = string.Format("if(confirm('Are you sure to update received status?')) __doPostBack('{0}','{1}${2}'); else return false;",
                                                  GvBedStatus.ClientID,
                                                  updateButtonField.CommandName,
                                                  updateButtonField.CommandArgument);
                Label AdmittedStatus = (Label)e.Row.FindControl("lbladmittedstatus");
                Label Status = (Label)e.Row.FindControl("lblstatus");
                if (AdmittedStatus.Text == "0")
                {
                    Status.Text = "NO";

                }
                else if (AdmittedStatus.Text == "1")
                {
                    Status.Text = "YES";
                }
            }
        }
        protected void GvBedStatus_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (LogData.UpdateEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            Int32 ID = Convert.ToInt32(GvBedStatus.DataKeys[e.RowIndex].Values["ID"].ToString());
            System.Web.UI.WebControls.Label admittedstatusID = (System.Web.UI.WebControls.Label)GvBedStatus.Rows[e.RowIndex].FindControl("lbladmittedstatus");
            System.Web.UI.WebControls.Label admittedstatus = (System.Web.UI.WebControls.Label)GvBedStatus.Rows[e.RowIndex].FindControl("lblstatus");


            PatAdmToWardData objpat = new PatAdmToWardData();
            PatAdmToWardBO objBO = new PatAdmToWardBO();
            objpat.ID = ID;
            objpat.EmployeeID = LogData.EmployeeID;
            if (Convert.ToInt16(admittedstatusID.Text) == 0 || Convert.ToInt16(admittedstatusID.Text) == 2)
            {
                objpat.AdmToWardStatusID = 1;
            }

            if (Convert.ToInt16(admittedstatusID.Text) == 1)
            {
                Messagealert_.ShowMessage(lblmessage, "The patient is already received in the ward.", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            int result = objBO.UpdateAdmittedToWardStatus(objpat);
            if (result > 0)
            {
                bindgrid(0);
            }
            else
            {
                divmsg3.Visible = false;

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
            btnexport.Visible = false;
            ddlexport.Visible = false;
            ddl_workstatus.SelectedIndex = 0;
            //txt_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtautoIPNo.Text = "";

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
                wb.Worksheets.Add(dt, "Admitted To Ward Status Details");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AdmittedToWardStatusDetails.xlsx");
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
            List<PatAdmToWardData> BedStatusDetails = GetPatAdmToWardDetails(0);
            List<PatAdmToWardDataToExcel> ListexcelData = new List<PatAdmToWardDataToExcel>();
            int i = 0;
            foreach (PatAdmToWardData row in BedStatusDetails)
            {
                PatAdmToWardDataToExcel Ecxeclemp = new PatAdmToWardDataToExcel();
                Ecxeclemp.IPNo = BedStatusDetails[i].IPNo;
                Ecxeclemp.PatientName = BedStatusDetails[i].PatientName;
                Ecxeclemp.Block = BedStatusDetails[i].Block;
                Ecxeclemp.Floor1 = BedStatusDetails[i].Floor1;
                Ecxeclemp.Ward = BedStatusDetails[i].Ward;
                Ecxeclemp.Room = BedStatusDetails[i].Room;
                Ecxeclemp.BedNo = BedStatusDetails[i].BedNo;
                if (BedStatusDetails[i].AdmToWardStatusID == 1)
                {
                    Ecxeclemp.AdmToWardStatus = "YES";
                }
                else if (BedStatusDetails[i].AdmToWardStatusID == 0)
                {
                    Ecxeclemp.AdmToWardStatus = "NO";
                }
                else
                {
                    Ecxeclemp.AdmToWardStatus = " ";
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
