using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedNurseBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedNurseData;
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

namespace Mediqura.Web.MedNurse
{
    public partial class AsstNursingSp : BasePage
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
            Commonfunction.PopulateDdl(ddl_Superintendent, mstlookup.GetLookupsList(LookupName.Superintendent));
            Commonfunction.PopulateDdl(ddl_supervisor, mstlookup.GetLookupsList(LookupName.Supervisor));
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Superintendent.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Astnusingsuperitenednt", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_Superintendent.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                if (ddl_supervisor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Supervisor", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_supervisor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                SupervisorData objAsstSupervisorMSTData = new SupervisorData();
                NurseSupervisorBO objSupervisorMSTBO = new NurseSupervisorBO();
                objAsstSupervisorMSTData.AsstSuperitendantID = Convert.ToInt64(ddl_Superintendent.SelectedValue == "" ? "0" : ddl_Superintendent.SelectedValue);
                objAsstSupervisorMSTData.SupervisorID = Convert.ToInt64(ddl_supervisor.SelectedValue == "" ? "0" : ddl_supervisor.SelectedValue);
                objAsstSupervisorMSTData.ActionType = Enumaction.Update;

                int result = objSupervisorMSTBO.UpdateAsstSupervisorDetails(objAsstSupervisorMSTData);  // funtion at DAL
                if (result == 1)
                {
                    bindgrid();
                    btnsave.Text = "Add";
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
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
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            ddl_supervisor.SelectedIndex = 0;
            ddl_Superintendent.SelectedIndex = 0;
            GVAsstNurseSupervisor.DataSource = null;
            GVAsstNurseSupervisor.DataBind();
            GVAsstNurseSupervisor.Visible = true;
            lblresult.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            btnsave.Text = "Add";
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {

            bindgrid();
        }
        private void bindgrid()
        {
            try
            {
                List<SupervisorData> lstemp = GetAssNursingSP(0);
                if (lstemp.Count > 0)
                {
                    GVAsstNurseSupervisor.DataSource = lstemp;
                    GVAsstNurseSupervisor.DataBind();
                    GVAsstNurseSupervisor.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GVAsstNurseSupervisor.DataSource = null;
                    GVAsstNurseSupervisor.DataBind();
                    GVAsstNurseSupervisor.Visible = true;
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
            }
        }
        private List<SupervisorData> GetAssNursingSP(int p)
        {
            SupervisorData objsupervisorMSTData = new SupervisorData();
            NurseSupervisorBO objSupervisorMSTBO = new NurseSupervisorBO();
            objsupervisorMSTData.AsstSuperitendantID = Convert.ToInt64(ddl_Superintendent.SelectedValue == "" ? "0" : ddl_Superintendent.SelectedValue);
            objsupervisorMSTData.SupervisorID = Convert.ToInt64(ddl_supervisor.SelectedValue == "" ? "0" : ddl_supervisor.SelectedValue);
            return objSupervisorMSTBO.SearchAsstNurseSupervisorDetails(objsupervisorMSTData);
        }
        protected void GVAsstNurseSupervisor_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    SupervisorData objData = new SupervisorData();
                    NurseSupervisorBO objBO = new NurseSupervisorBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVAsstNurseSupervisor.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    Label SupervisorID = (Label)gr.Cells[0].FindControl("lbl_supervisorID");
                    objData.AsstSuperitendantID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objData.SupervisorID = Convert.ToInt64(SupervisorID.Text == "" ? "0" : SupervisorID.Text);
                    objData.ActionType = Enumaction.Select;
                    List<SupervisorData> GetResult = objBO.SearchAsstNurseSupervisorDetails(objData);
                    if (GetResult.Count > 0)
                    {
                        ddl_Superintendent.SelectedValue = GetResult[0].EmployeeID.ToString();
                        ddl_supervisor.SelectedValue = GetResult[0].SupervisorID.ToString();
                        btnsave.Text = "Update";
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
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "AsstNursingSP");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Asst.NursingSP.xlsx");
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
            List<SupervisorData> TypeDetails = GetAssNursingSP(0);
            List<AsstNursingSPDatatoEcxel> ListexcelData = new List<AsstNursingSPDatatoEcxel>();
            int i = 0;
            foreach (SupervisorData row in TypeDetails)
            {
                AsstNursingSPDatatoEcxel ExcelSevice = new AsstNursingSPDatatoEcxel();
                ExcelSevice.AsstSuperitendant = TypeDetails[i].AsstSuperitendant;
                ExcelSevice.SupervisorName = TypeDetails[i].SupervisortName;
                GVAsstNurseSupervisor.Columns[3].Visible = false;
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
        }
    }
}