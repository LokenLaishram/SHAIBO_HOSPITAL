using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using System.Drawing;

namespace Mediqura.Web.MedIPD
{
    public partial class CurrentAdmittedPatientList : BasePage
    {
        static int slno;
        static int activePatient;
        static int passivePatient;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                bindgrid();
                hdnroldeID.Value = LogData.RoleID.ToString();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            slno = 0;
            activePatient = 0;
            passivePatient = 0;

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


                List<CurrentAdmissionListData> objdeposit = GetPatientList(0);
                if (objdeposit.Count > 0)
                {

                    gvadmissionlist.DataSource = objdeposit;
                    gvadmissionlist.DataBind();
                    gvadmissionlist.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;

                }
                else
                {
                    gvadmissionlist.DataSource = null;
                    gvadmissionlist.DataBind();
                    gvadmissionlist.Visible = true;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<CurrentAdmissionListData> GetPatientList(int curIndex)
        {
            CurrentAdmissionListData objData = new CurrentAdmissionListData();
            CurrentAdmissionListBO objBO = new CurrentAdmissionListBO();
            objData.wardId = Convert.ToInt32(ddl_ward.SelectedValue == "" ? "0" : ddl_ward.SelectedValue);
            return objBO.GetAdmissionDetailList(objData);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvadmissionlist.DataSource = null;
            gvadmissionlist.DataBind();
            gvadmissionlist.Visible = true;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            ddl_ward.SelectedIndex = 0;
        }
        protected void gvadmissionlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSlNo = (Label)e.Row.FindControl("lblSlNo");
                Label lbluhid = (Label)e.Row.FindControl("lbluhid");
                Label lblbed = (Label)e.Row.FindControl("lblbed");
                Label lblWard = (Label)e.Row.FindControl("lblWard");
                Label lblWardTotal = (Label)e.Row.FindControl("lblWardTotal");
                Label lblSubHeading = (Label)e.Row.FindControl("lblSubHeading");
                Label lblIsRelease = (Label)e.Row.FindControl("lblIsRelease");
                Label lblPatientActive = (Label)e.Row.FindControl("lblPatientActive");
                Label lblDisChargeReady = (Label)e.Row.FindControl("lblDisChargeReady");

                if (lblDisChargeReady.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#dcf852");
                }
                if (lblPatientActive.Text == "0")
                {

                    if (lblSubHeading.Text == "1")
                    {
                    }
                    else
                    {
                        e.Row.Cells[7].BackColor = Color.FromName("#F3F00F");
                    }
                    passivePatient = passivePatient + 1;
                    slno = slno + 1;
                    lblSlNo.Text = slno.ToString();
                }
                else
                {
                    activePatient = activePatient + 1;
                    slno = slno + 1;
                    lblSlNo.Text = slno.ToString();
                }
                if (lblSubHeading.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#33aa99");
                    lblWard.ForeColor = Color.FromName("#FFFFFF");
                    lblWardTotal.ForeColor = Color.FromName("#FFFFFF");
                    GridView editGrid = sender as GridView;
                    e.Row.Cells[1].ColumnSpan = 2;
                    e.Row.Cells[2].ColumnSpan = 2;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[3].Controls.Clear();
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[9].Controls.Clear();
                    lbluhid.Visible = false;
                    lblWard.Visible = true;
                    lblWardTotal.Visible = true;
                    slno = slno - 1;
                    passivePatient = passivePatient - 1;
                    lblSlNo.Text = "";
                }
                else
                {
                    lblWard.Visible = false;
                    lblWardTotal.Visible = false;
                    lbluhid.Visible = true;

                }

            }
            Messagealert_.ShowMessage(lblresult, "Total Active Patient: " + activePatient + "        |        Total Occupied Patient: " + passivePatient + "        |        Total: " + (activePatient + passivePatient), 1);

        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            CurrentAdmissionListData objData = new CurrentAdmissionListData();
            CurrentAdmissionListBO objBO = new CurrentAdmissionListBO();
            Int32 WardID = Convert.ToInt32(ddl_ward.SelectedValue == "" ? "0" : ddl_ward.SelectedValue);
            string url = "../MedIPD/Reports/ReportViewer.aspx?option=CurrentPatientList&ward=" + WardID.ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}