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
    public partial class IPDDoctorRoundPayOutMST : BasePage
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
            ddl_group.Attributes["disabled"] = "disabled";
            Commonfunction.PopulateDdl(ddl_group, mstlookup.GetLookupsList(LookupName.CommonGroups));
            ddl_group.SelectedValue = "10";
            Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetSubServiceTypeByGroupID(Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue)));
            Commonfunction.PopulateDdl(ddl_doctortype, mstlookup.GetLookupsList(LookupName.OPDoctorType));
            Commonfunction.Insertzeroitemindex(ddl_service);
            Commonfunction.Insertzeroitemindex(ddl_doctor);
            Commonfunction.Insertzeroitemindex(ddl_department);

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            ServicesData Objpaic = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.ServiceTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.Getopservices(Objpaic);
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
        protected void ddl_subgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_subgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_service, mstlookup.GetServiceByID(Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue), Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue)));
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_service);
            }
        }
        protected void ddl_doctortype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_doctortype.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_department, mstlookup.GetLookupsList(LookupName.OPDepartment));
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_department);
            }
        }
        protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_department.SelectedIndex > 0 && ddl_doctortype.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetDoctorBydepartmentID(Convert.ToInt32(ddl_department.SelectedValue == "" ? "0" : ddl_department.SelectedValue), Convert.ToInt32(ddl_doctortype.SelectedValue == "" ? "0" : ddl_doctortype.SelectedValue)));
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_doctor);
            }
        }
        protected void ddl_doctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_doctor.SelectedIndex > 0)
            {
                bindgrid();
            }
            else
            {
                GvIPDDoctorRound.Visible = true;
                GvIPDDoctorRound.DataSource = null;
                GvIPDDoctorRound.DataBind();
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            bindddl();
            ddl_BedClass.SelectedIndex = 0;
            ddl_doctortype.SelectedIndex = 0;
            GvIPDDoctorRound.DataSource = null;
            GvIPDDoctorRound.DataBind();
            GvIPDDoctorRound.Visible = false;
            divmsg1.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            divmsg3.Visible = false;
            lblresult.Visible = false;

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
                if (ddl_doctortype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "DoctorType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_doctortype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_department.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_department.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_doctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Consultant", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_doctor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_doctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_doctor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                List<OPDEmergencyPayOutData> lstemp = GetIPDDoctorRoundPayOutDetails(0);
                if (lstemp.Count > 0)
                {
                    GvIPDDoctorRound.DataSource = lstemp;
                    GvIPDDoctorRound.DataBind();
                    GvIPDDoctorRound.Visible = true;
                    btnexport.Visible = true;
                    ddlexport.Visible = true;
                }
                else
                {
                    GvIPDDoctorRound.DataSource = null;
                    GvIPDDoctorRound.DataBind();
                    GvIPDDoctorRound.Visible = true;
                    btnexport.Visible = false;
                    ddlexport.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<OPDEmergencyPayOutData> GetIPDDoctorRoundPayOutDetails(int p)
        {

            List<OPDEmergencyPayOutData> List = new List<OPDEmergencyPayOutData>();
            OPDEmergencyPayOutBO objBO = new OPDEmergencyPayOutBO();
            OPDEmergencyPayOutData objData = new OPDEmergencyPayOutData();
            objData.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue);
            objData.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
            objData.DoctorTypeID = Convert.ToInt32(ddl_doctortype.SelectedValue == "" ? "0" : ddl_doctortype.SelectedValue);
            objData.DepartmentID = Convert.ToInt32(ddl_department.SelectedValue == "" ? "0" : ddl_department.SelectedValue);
            objData.DoctorID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objData.ServiceID = Convert.ToInt32(ddl_service.SelectedValue == "" ? "0" : ddl_service.SelectedValue);
            objData.EmployeeID = LogData.EmployeeID;
            objData.FinancialYearID = LogData.FinancialYearID;
            objData.HospitalID = LogData.HospitalID;
            return objBO.SearchIPDRoundPayOutDetails(objData);
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
                if (ddl_doctortype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "DoctorType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_doctortype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_department.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_department.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_doctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Consultant", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_doctor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_doctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_doctor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                //if (ddl_BedClass.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "BedClass", 0);
                //    divmsg1.Visible = true;
                //    divmsg1.Attributes["class"] = "FailAlert";
                //    ddl_BedClass.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;

                //}
                List<OPDEmergencyPayOutData> List = new List<OPDEmergencyPayOutData>();
                OPDEmergencyPayOutBO objBO = new OPDEmergencyPayOutBO();
                OPDEmergencyPayOutData objData = new OPDEmergencyPayOutData();
                int count = 0;

                foreach (GridViewRow row in GvIPDDoctorRound.Rows)
                {
                    Label docid = (Label)GvIPDDoctorRound.Rows[row.RowIndex].Cells[0].FindControl("lblDoctorID");
                    Label serviceid = (Label)GvIPDDoctorRound.Rows[row.RowIndex].Cells[0].FindControl("lbl_serviceID");
                    TextBox rate = (TextBox)GvIPDDoctorRound.Rows[row.RowIndex].Cells[0].FindControl("txtrate");
                    DropDownList sharetype = (DropDownList)GvIPDDoctorRound.Rows[row.RowIndex].Cells[0].FindControl("ddl_sharetype");
                    TextBox hosshare = (TextBox)GvIPDDoctorRound.Rows[row.RowIndex].Cells[0].FindControl("txt_hosshare");
                    TextBox conshare = (TextBox)GvIPDDoctorRound.Rows[row.RowIndex].Cells[0].FindControl("txt_consultshare");
                    //TextBox vaildDays = (TextBox)GvIPDDoctorRound.Rows[row.RowIndex].Cells[0].FindControl("txt_validDays");
                    CheckBox chk = (CheckBox)GvIPDDoctorRound.Rows[row.RowIndex].Cells[0].FindControl("chekbox");

                    OPDEmergencyPayOutData obj1 = new OPDEmergencyPayOutData();

                    obj1.DoctorID = Convert.ToInt64(docid.Text == "" ? "0" : docid.Text);
                    obj1.ServiceID = Convert.ToInt32(serviceid.Text == "" ? "0" : serviceid.Text);
                    obj1.ServiceCharge = Convert.ToDecimal(rate.Text == "" ? "0.0" : rate.Text);
                    obj1.ShareType = Convert.ToInt32(sharetype.SelectedValue == "" ? "0" : sharetype.SelectedValue);
                    obj1.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue);
                    obj1.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
                    //obj1.ValidDays = Convert.ToInt32(vaildDays.Text == "" ? "0" : vaildDays.Text);

                    if (sharetype.SelectedIndex == 1)
                    {
                        if (((Convert.ToDouble(hosshare.Text == "" ? "0" : hosshare.Text) + Convert.ToDouble(conshare.Text == "" ? "0" : conshare.Text)) != 100))
                        {
                            Messagealert_.ShowMessage(lblmessage, "PC", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                            hosshare.Focus();
                            return;
                        }
                        else
                        {
                            obj1.HospitalShare = Convert.ToDecimal(hosshare.Text);
                            obj1.ConsultantShare = Convert.ToDecimal(conshare.Text);
                        }
                    }
                    if (sharetype.SelectedIndex == 2)
                    {
                        if ((Convert.ToDouble(hosshare.Text == "" ? "0" : hosshare.Text) + Convert.ToDouble(conshare.Text == "" ? "0" : conshare.Text)) != (Convert.ToDouble(rate.Text == "" ? "0" : rate.Text)))
                        {
                            Messagealert_.ShowMessage(lblmessage, "Share", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                            hosshare.Focus();
                            return;
                        }
                        else
                        {
                            obj1.HospitalShare = Convert.ToDecimal(hosshare.Text);
                            obj1.ConsultantShare = Convert.ToDecimal(conshare.Text);
                        }
                    }
                    if (chk.Checked == false)
                    {
                        obj1.Activate = 0;
                    }
                    else
                    {
                        obj1.Activate = 1;
                        if (sharetype.SelectedIndex == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage, "ShareType", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                            sharetype.Focus();
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                            divmsg1.Visible = false;
                        }
                    }
                    List.Add(obj1);
                }
                objData.XMLData = XmlConvertor.OPDEmergencyRecordDatatoXML(List).ToString();
                objData.BedClassID = Convert.ToInt32(ddl_BedClass.SelectedValue == "" ? "0" : ddl_BedClass.SelectedValue);
                objData.EmployeeID = LogData.EmployeeID;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.IPaddress = LogData.IPaddress;
                objData.HospitalID = LogData.HospitalID;
                objData.ActionType = Enumaction.Insert;

                int result = objBO.UpdateIPDDoctorRound(objData);
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
                wb.Worksheets.Add(dt, "IPD Doctor Round Payout");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=IODDoctorRoundPayOutDetails.xlsx");
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
            List<OPDEmergencyPayOutData> OPDEmergencyDetails = GetIPDDoctorRoundPayOutDetails(0);
            List<OPDEmergencyPayOutDatatoExcel> ListexcelData = new List<OPDEmergencyPayOutDatatoExcel>();
            int i = 0;
            foreach (OPDEmergencyPayOutData row in OPDEmergencyDetails)
            {
                OPDEmergencyPayOutDatatoExcel ExcelSevice = new OPDEmergencyPayOutDatatoExcel();
                ExcelSevice.DoctorName = OPDEmergencyDetails[i].DoctorName;
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
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";

                ddlexport.Focus();
                return;
            }
        }
        protected void GvIPDDoctorRound_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                DropDownList ddl1 = (e.Row.FindControl("ddl_sharetype") as DropDownList);
                Label activ = (Label)e.Row.FindControl("lbl_activate");
                CheckBox chk = (e.Row.FindControl("chekbox") as CheckBox);

                Label ShareTypeID = (Label)e.Row.FindControl("lbl_sharetype");

                if (ShareTypeID.Text == "2")
                {
                    ddl1.SelectedItem.Text = "PC";
                    ddl1.SelectedIndex = 1;
                }
                else if (ShareTypeID.Text == "3")
                {
                    ddl1.SelectedItem.Text = "Value";
                    ddl1.SelectedIndex = 2;

                }
                else
                {
                    ddl1.SelectedItem.Text = "--Select--";
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
    }
}