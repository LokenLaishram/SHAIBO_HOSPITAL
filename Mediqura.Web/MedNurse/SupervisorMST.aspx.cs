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
    public partial class SupervisorMST : BasePage
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
            Commonfunction.PopulateDdl(ddl_supervisor, mstlookup.GetLookupsList(LookupName.Supervisor));
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.GenStockType));

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
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
                //if (ddl_ward.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Ward", 0);
                //    divmsg1.Visible = true;
                //    divmsg1.Attributes["class"] = "FailAlert";
                //    ddl_ward.Focus();
                //    return;
                //}
                //{
                //    lblmessage.Visible = false;
                //}
                SupervisorData objSupervisorMSTData = new SupervisorData();
                NurseSupervisorBO objSupervisorMSTBO = new NurseSupervisorBO();

                objSupervisorMSTData.EmployeeID = Convert.ToInt32(ddl_supervisor.SelectedValue == "" ? "0" : ddl_supervisor.SelectedValue);
                objSupervisorMSTData.WardID = Convert.ToInt32(ddl_ward.SelectedValue == "" ? "0" : ddl_ward.SelectedValue);
                objSupervisorMSTData.ActionType = Enumaction.Update;

                int result = objSupervisorMSTBO.UpdateSupervisorDetails(objSupervisorMSTData);  // funtion at DAL
                if (result == 1)
                {
                    bindgrid();
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btnsave.Text = "Add";
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
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            btnsave.Text = "Add";
        }
        private void clearall()
        {
            ddl_supervisor.SelectedIndex = 0;
            ddl_ward.SelectedIndex = 0;
            GVNurseSupervisor.DataSource = null;
            GVNurseSupervisor.DataBind();
            GVNurseSupervisor.Visible = true;
            lblresult.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        private void bindgrid()
        {
            try
            {
                List<SupervisorData> lstemp = GetSupervisor(0);
                if (lstemp.Count > 0)
                {
                    GVNurseSupervisor.DataSource = lstemp;
                    GVNurseSupervisor.DataBind();
                    GVNurseSupervisor.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GVNurseSupervisor.DataSource = null;
                    GVNurseSupervisor.DataBind();
                    GVNurseSupervisor.Visible = true;
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
        private List<SupervisorData> GetSupervisor(int p)
        {
            SupervisorData objsupervisorMSTData = new SupervisorData();
            NurseSupervisorBO objSupervisorMSTBO = new NurseSupervisorBO();
            objsupervisorMSTData.EmployeeID = Convert.ToInt64(ddl_supervisor.SelectedValue == "" ? "0" : ddl_supervisor.SelectedValue);
            objsupervisorMSTData.WardID = Convert.ToInt32(ddl_ward.SelectedValue == "" ? "0" : ddl_ward.SelectedValue);
            return objSupervisorMSTBO.SearchNurseSupervisorDetails(objsupervisorMSTData);
        }
        protected void GVNurseSupervisor_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow gr = GVNurseSupervisor.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    Label WardID = (Label)gr.Cells[0].FindControl("lbl_wardID");

                    objData.EmployeeID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objData.WardID = Convert.ToInt32(WardID.Text == "" ? "0" : WardID.Text);
                    objData.ActionType = Enumaction.Select;
                    List<SupervisorData> GetResult = objBO.SearchNurseSupervisorDetails(objData);
                    if (GetResult.Count > 0)
                    {
                        ddl_supervisor.SelectedValue = GetResult[0].EmployeeID.ToString();
                        ddl_ward.SelectedValue = GetResult[0].WardID.ToString();
                        btnsave.Text = "Update";
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
                    SupervisorData objData = new SupervisorData();
                    NurseSupervisorBO objBO = new NurseSupervisorBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVNurseSupervisor.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    Label WardID = (Label)gr.Cells[0].FindControl("lbl_wardID");
                    objData.EmployeeID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objData.WardID = Convert.ToInt32(WardID.Text == "" ? "0" : WardID.Text);
                    objData.ActionType = Enumaction.Select;
                    int result = objBO.CancelSupervisorBoundstock(objData);
                    if (result == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        bindgrid();
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
                wb.Worksheets.Add(dt, "Item Type Detail List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Supervisors.xlsx");
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
            List<SupervisorData> TypeDetails = GetSupervisor(0);
            List<SupervisorDatatoEcxel> ListexcelData = new List<SupervisorDatatoEcxel>();
            int i = 0;
            foreach (SupervisorData row in TypeDetails)
            {
                SupervisorDatatoEcxel ExcelSevice = new SupervisorDatatoEcxel();
                ExcelSevice.SupervisortName = TypeDetails[i].SupervisortName;
                ExcelSevice.WardName = TypeDetails[i].WardName;
                GVNurseSupervisor.Columns[3].Visible = false;
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