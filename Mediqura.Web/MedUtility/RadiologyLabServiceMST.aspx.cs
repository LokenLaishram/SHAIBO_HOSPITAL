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
    public partial class RadiologyLabServiceMST : BasePage
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
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.InvestigationGroup));
            Commonfunction.PopulateDdl(ddl_testcenter, mstlookup.GetLookupsList(LookupName.TestCenter));
            Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
            AutoCompleteExtender2.ContextKey = "0";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            ServicesData Objpaic = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.ServiceTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.Gettestservices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDoctorName(string prefixText, int count, string contextKey)
        {
            OPDEmergencyPayOutData Objpaic = new OPDEmergencyPayOutData();
            OPDEmergencyPayOutBO objInfoBO = new OPDEmergencyPayOutBO();
            List<OPDEmergencyPayOutData> getResult = new List<OPDEmergencyPayOutData>();
            Objpaic.DoctorName = prefixText;
            Objpaic.DoctorTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetDoctorName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].DoctorName.ToString());
            }
            return list;
        }
        protected void ddl_labgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue)));
                AutoCompleteExtender2.ContextKey = ddl_labgroup.SelectedValue;
            }
        }
        protected void ddl_labsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            //Commonfunction.PopulateDdl(ddl_test, mstlookup.GetTestNameBySubGroupID(Convert.ToInt32(ddl_labsubgroup.SelectedValue)));
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddl_labgroup.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
            txt_test.Text = "";
            ddl_patienttype.SelectedIndex = 0;
            ddl_BedClass.SelectedIndex = 0;
            GvRadioLab.DataSource = null;
            GvRadioLab.DataBind();
            GvRadioLab.Visible = false;
            divmsg1.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            btnsave.Visible = false;
        }
        private void bindgrid()
        {
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

                //if (ddl_labgroup.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Group", 0);
                //    divmsg1.Visible = true;
                //    divmsg1.Attributes["class"] = "FailAlert";

                //    ddl_labgroup.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                //if (ddl_labsubgroup.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Subgroup", 0);
                //    divmsg1.Visible = true;
                //    divmsg1.Attributes["class"] = "FailAlert";
                //    ddl_labgroup.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                if (txt_test.Text.Trim() != "")
                {
                    if (Commonfunction.SemicolonSeparation_String_32(txt_test.Text.ToString()) == 0)
                    {
                        txt_test.Text = "";
                        txt_test.Focus();
                        return;
                    }
                }

                List<RadilogyLabServiceMSTData> lstemp = GetRadiologyLabServiceDetails(0);
                if (lstemp.Count > 0)
                {
                    GvRadioLab.DataSource = lstemp;
                    GvRadioLab.DataBind();
                    GvRadioLab.Visible = true;
                    btnexport.Visible = true;
                    ddlexport.Visible = true;
                    btnsave.Visible = true;
                }
                else
                {
                    GvRadioLab.DataSource = null;
                    GvRadioLab.DataBind();
                    GvRadioLab.Visible = true;
                    btnexport.Visible = false;
                    ddlexport.Visible = false;
                    btnsave.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<RadilogyLabServiceMSTData> GetRadiologyLabServiceDetails(int p)
        {
            List<RadilogyLabServiceMSTData> List = new List<RadilogyLabServiceMSTData>();
            RadiologyLabServiceMSTBO objBO = new RadiologyLabServiceMSTBO();
            RadilogyLabServiceMSTData objData = new RadilogyLabServiceMSTData();
            objData.GroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue);
            objData.SubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue);
            objData.ServiceID = Commonfunction.SemicolonSeparation_String_32(txt_test.Text.ToString());
            objData.TestcenterID = Convert.ToInt32(ddl_testcenter.SelectedValue == "" ? "0" : ddl_testcenter.SelectedValue);
            objData.EmployeeID = LogData.EmployeeID;
            objData.FinancialYearID = LogData.FinancialYearID;
            objData.HospitalID = LogData.HospitalID;
            return objBO.SearchRadiologyLabServicePayOutDetails(objData);
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                lblmessage.Visible = false;
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

                //if (ddl_labgroup.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Group", 0);
                //    divmsg1.Visible = true;
                //    divmsg1.Attributes["class"] = "FailAlert";

                //    ddl_labgroup.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                //if (ddl_labsubgroup.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Subgroup", 0);
                //    divmsg1.Visible = true;
                //    divmsg1.Attributes["class"] = "FailAlert";
                //    ddl_labgroup.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                List<RadilogyLabServiceMSTData> List = new List<RadilogyLabServiceMSTData>();
                RadiologyLabServiceMSTBO objBO = new RadiologyLabServiceMSTBO();
                RadilogyLabServiceMSTData objData = new RadilogyLabServiceMSTData();
                foreach (GridViewRow row in GvRadioLab.Rows)
                {
                    Label id = (Label)GvRadioLab.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label serviceid = (Label)GvRadioLab.Rows[row.RowIndex].Cells[0].FindControl("lbl_serviceID");
                    TextBox rate = (TextBox)GvRadioLab.Rows[row.RowIndex].Cells[0].FindControl("txtrate");
                    DropDownList sharetype = (DropDownList)GvRadioLab.Rows[row.RowIndex].Cells[0].FindControl("ddl_sharetype");
                    TextBox hosshare = (TextBox)GvRadioLab.Rows[row.RowIndex].Cells[0].FindControl("txt_hosshare");
                    TextBox conshare = (TextBox)GvRadioLab.Rows[row.RowIndex].Cells[0].FindControl("txt_consultshare");
                    TextBox reportingshare = (TextBox)GvRadioLab.Rows[row.RowIndex].Cells[0].FindControl("txt_reportingshare");
                    CheckBox chk = (CheckBox)GvRadioLab.Rows[row.RowIndex].Cells[0].FindControl("chekbox");
                    Label tescenterID = (Label)GvRadioLab.Rows[row.RowIndex].Cells[0].FindControl("lbltesceterID");

                    RadilogyLabServiceMSTData obj1 = new RadilogyLabServiceMSTData();
                    obj1.ID = Convert.ToInt64(id.Text == "" ? "0" : id.Text);
                    obj1.ServiceID = Convert.ToInt32(serviceid.Text == "" ? "0" : serviceid.Text);
                    obj1.ServiceCharge = Convert.ToInt32(rate.Text == "" ? "0" : rate.Text);
                    obj1.ShareType = Convert.ToInt32(sharetype.SelectedValue);
                    obj1.GroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue);
                    obj1.SubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue);
                    obj1.TestcenterID = Convert.ToInt32(tescenterID.Text == "" ? "0" : tescenterID.Text);
                    if (sharetype.SelectedIndex == 1)
                    {
                        if ((Convert.ToDecimal(hosshare.Text == "" ? "0.0" : hosshare.Text) + Convert.ToDecimal(conshare.Text == "" ? "0.0" : conshare.Text) + Convert.ToDecimal(reportingshare.Text == "" ? "0.0" : reportingshare.Text)) == 100)
                        {
                            obj1.ServiceCharge = Convert.ToDecimal(rate.Text == "" ? "0.0" : rate.Text);
                            obj1.HospitalShare = Convert.ToDecimal(hosshare.Text == "" ? "0.0" : hosshare.Text);
                            obj1.ConsultantShare = Convert.ToDecimal(conshare.Text == "" ? "0.0" : conshare.Text);
                            obj1.Reportingshare = Convert.ToDecimal(reportingshare.Text == "" ? "0.0" : reportingshare.Text);
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage, "PC", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                            hosshare.Focus();
                            return;
                        }
                    }
                    else if (sharetype.SelectedIndex == 2)
                    {
                        if ((Convert.ToDecimal(hosshare.Text == "" ? "0.0" : hosshare.Text) + Convert.ToDecimal(conshare.Text == "" ? "0.0" : conshare.Text) + Convert.ToDecimal(reportingshare.Text == "" ? "0.0" : reportingshare.Text)) == Convert.ToDecimal(rate.Text == "" ? "0.0" : rate.Text))
                        {
                            obj1.ServiceCharge = Convert.ToDecimal(rate.Text == "" ? "0.0" : rate.Text);
                            obj1.HospitalShare = Convert.ToDecimal(hosshare.Text == "" ? "0.0" : hosshare.Text);
                            obj1.ConsultantShare = Convert.ToDecimal(conshare.Text == "" ? "0.0" : conshare.Text);
                            obj1.Reportingshare = Convert.ToDecimal(reportingshare.Text == "" ? "0.0" : reportingshare.Text);
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage, "Share", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                            hosshare.Focus();
                            return;
                        }
                    }

                    if (chk.Checked == false)
                    {
                        obj1.Activate = 0;
                    }
                    else
                    {
                        obj1.Activate = 1;
                        //if (sharetype.SelectedIndex == 0)
                        //{
                        //    Messagealert_.ShowMessage(lblmessage, "ShareType", 0);
                        //    divmsg1.Visible = true;
                        //    divmsg1.Attributes["class"] = "FailAlert";
                        //    sharetype.Focus();
                        //    return;
                        //}
                        //else
                        //{
                        //    lblmessage.Visible = false;
                        //    divmsg1.Visible = false;
                        //}
                    }

                    List.Add(obj1);
                }
                objData.XMLData = XmlConvertor.RadiologyLabRecordDatatoXML(List).ToString();
                objData.EmployeeID = LogData.EmployeeID;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.IPaddress = LogData.IPaddress;
                objData.HospitalID = LogData.HospitalID;
                objData.ActionType = Enumaction.Insert;

                int result = objBO.UpdateRadiologyLabPayOut(objData);
                if (result == 1)
                {
                    bindgrid();
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";

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

            }
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "LabServicePayOutDetails List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LabServicePayOutDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private DataTable GetDatafromDatabase()
        {
            List<RadilogyLabServiceMSTData> OPDEmergencyDetails = GetRadiologyLabServiceDetails(0);
            List<RadilogyLabServicePayOutDatatoExcel> ListexcelData = new List<RadilogyLabServicePayOutDatatoExcel>();
            int i = 0;
            foreach (RadilogyLabServiceMSTData row in OPDEmergencyDetails)
            {
                RadilogyLabServicePayOutDatatoExcel ExcelSevice = new RadilogyLabServicePayOutDatatoExcel();
                ExcelSevice.ServiceName = OPDEmergencyDetails[i].ServiceName;
                ExcelSevice.ServiceCharge = Commonfunction.Getrounding(OPDEmergencyDetails[i].ServiceCharge.ToString());
                if (OPDEmergencyDetails[i].ShareType == 2)
                {
                    ExcelSevice.ShareTypeName = "PC";
                }
                else if (OPDEmergencyDetails[i].ShareType == 3)
                {
                    ExcelSevice.ShareTypeName = "Value";
                }
                else
                {
                    ExcelSevice.ShareTypeName = "";
                }
                ExcelSevice.HospitalShare = Commonfunction.Getrounding(OPDEmergencyDetails[i].HospitalShare.ToString());
                ExcelSevice.ConsultantShare = Commonfunction.Getrounding(OPDEmergencyDetails[i].ConsultantShare.ToString());
                ExcelSevice.Reportingshare = Commonfunction.Getrounding(OPDEmergencyDetails[i].Reportingshare.ToString());
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
            else
            {
                Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void GvOPDEmergency_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Label activ = (Label)e.Row.FindControl("lbl_activate");
                CheckBox chk = (e.Row.FindControl("chekbox") as CheckBox);


                DropDownList ddl1 = (e.Row.FindControl("ddl_sharetype") as DropDownList);

                Label ShareTypeID = (Label)e.Row.FindControl("lbl_sharetype");

                if (ShareTypeID.Text == "2")
                {
                    //ddl1.SelectedItem.Text = "PC";
                    ddl1.SelectedIndex = 1;
                }
                else if (ShareTypeID.Text == "3")
                {
                    //ddl1.SelectedItem.Text = "Value";
                    ddl1.SelectedIndex = 2;

                }
                else
                {
                   // ddl1.SelectedItem.Text = "--Select--";
                    ddl1.SelectedIndex = 0;
                }
                if (activ.Text == "1")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
            }
        }
        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_test.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "LabTestName", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_test.Focus();
                    return;
                }
                else
                {
                    if (Commonfunction.SemicolonSeparation_String_32(txt_test.Text.ToString()) == 0)
                    {
                        txt_test.Text = "";
                        txt_test.Focus();
                        Messagealert_.ShowMessage(lblmessage, "LabTestName", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_test.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                if (ddl_testcenter.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "TestCenter", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_testcenter.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                RadiologyLabServiceMSTBO objBO = new RadiologyLabServiceMSTBO();
                RadilogyLabServiceMSTData objData = new RadilogyLabServiceMSTData();
                objData.ServiceID = Commonfunction.SemicolonSeparation_String_32(txt_test.Text.ToString());
                objData.TestcenterID = Convert.ToInt32(ddl_testcenter.SelectedValue == "" ? "0" : ddl_testcenter.SelectedValue);
                objData.TestcenterID = Convert.ToInt32(ddl_testcenter.SelectedValue == "" ? "0" : ddl_testcenter.SelectedValue);
                objData.EmployeeID = LogData.EmployeeID;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.HospitalID = LogData.HospitalID;

                List<RadilogyLabServiceMSTData> List = objBO.AddLabservicelist(objData);
                if (List.Count > 0)
                {
                    GvRadioLab.DataSource = List;
                    GvRadioLab.DataBind();
                    GvRadioLab.Visible = true;
                    btnexport.Visible = true;
                    ddlexport.Visible = true;
                    btnsave.Visible = true;
                }
                else
                {
                    GvRadioLab.DataSource = null;
                    GvRadioLab.DataBind();
                    GvRadioLab.Visible = true;
                    btnexport.Visible = false;
                    ddlexport.Visible = false;
                    btnsave.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);

            }
        }
        protected void txt_test_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void ddl_testcenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
    }

}