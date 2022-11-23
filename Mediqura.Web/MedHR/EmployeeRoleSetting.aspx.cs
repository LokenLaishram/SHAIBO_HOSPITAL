using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedHrData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Mediqura.Web.MedHR
{
    public partial class EmployeeRoleSetting : BasePage
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
            Commonfunction.PopulateDdl(ddl_departments, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(ddl_staffcategorys, mstlookup.GetLookupsList(LookupName.StaffCategory));
            Commonfunction.PopulateDdl(ddl_employeetypes, mstlookup.GetLookupsList(LookupName.EmployeeType));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmpNo(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmployeeNo = prefixText;
            getResult = objInfoBO.GetEmployeeNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmployeeNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmpName(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetEmployeeName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetContactno(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.MobileNo = prefixText;
            getResult = objInfoBO.GetContactno(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].MobileNo.ToString());
            }
            return list;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }

            bindgrid(1);
        }
        protected void bindgrid(int page)
        {
            try
            {
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<EmployeeData> patientdetails = GetEmployeeData(page);
                if (patientdetails.Count > 0)
                {
                    GvemployeeList.VirtualItemCount = patientdetails[0].MaximumRows;//total item is required for custom paging
                    GvemployeeList.PageIndex = page - 1;

                    GvemployeeList.DataSource = patientdetails;
                    GvemployeeList.DataBind();
                    GvemployeeList.Visible = true;
                    divmsg3.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + patientdetails[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    divmsg2.Visible = false;
                }
                else
                {
                    GvemployeeList.DataSource = null;
                    GvemployeeList.DataBind();
                    GvemployeeList.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
            }
        }
        public List<EmployeeData> GetEmployeeData(int curIndex)
        {
            EmployeeData objpat = new EmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.EmployeeNo = txtempnos.Text == "" ? null : txtempnos.Text.Trim();
            //objpat.EmpName = txtemployeename.Text == "" ? null : txtemployeename.Text.Trim();
            objpat.EmployeeID = Convert.ToInt64(txtemployeename.Text.Substring(txtemployeename.Text.LastIndexOf(':') + 1) == "" ? "0" : txtemployeename.Text.Substring(txtemployeename.Text.LastIndexOf(':') + 1));
            objpat.MobileNo = txtcontactnos.Text == "" ? null : txtcontactnos.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.StaffCategoryID = Convert.ToInt32(ddl_staffcategorys.SelectedValue == "" ? "0" : ddl_staffcategorys.SelectedValue);
            objpat.EmployeeTypeID = Convert.ToInt32(ddl_employeetypes.SelectedValue == "" ? "0" : ddl_employeetypes.SelectedValue);
            objpat.DepartmentID = Convert.ToInt32(ddl_departments.SelectedValue == "" ? "0" : ddl_departments.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.CurrentIndex = curIndex;
            return objstdBO.SearchEmployeeRoleDetails(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtempnos.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            GvemployeeList.DataSource = null;
            GvemployeeList.DataBind();
            GvemployeeList.Visible = false;
            lblmessage2.Visible = false;
            txtemployeename.Text = "";
            txtcontactnos.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            divmsg3.Visible = false;
            ViewState["ID"] = null;
            ddl_departments.SelectedIndex = 0;
            ddl_employeetypes.SelectedIndex = 0;
            ddl_staffcategorys.SelectedIndex = 0;
        }
        protected void GvemployeeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }
            if (ddlexport.SelectedIndex == 1)
            {
                ExportoExcel();
            }
            else
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportType", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Employee Details");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=EmployeeRoleDetails.xlsx");
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
            List<EmployeeData> EmployeeDetails = GetEmployeeDetails(0);
            List<EmployeeDatatoExcel> ListexcelData = new List<EmployeeDatatoExcel>();
            int i = 0;
            foreach (EmployeeData row in EmployeeDetails)
            {
                EmployeeDatatoExcel Ecxeclemp = new EmployeeDatatoExcel();
                Ecxeclemp.EmployeeNo = EmployeeDetails[i].EmployeeNo;
                Ecxeclemp.EmpName = EmployeeDetails[i].EmpName;
                Ecxeclemp.AliasName = EmployeeDetails[i].AliasName;
                Ecxeclemp.AadhaarNo = EmployeeDetails[i].AadhaarNo;
                Ecxeclemp.SpouseName = EmployeeDetails[i].SpouseName;
                Ecxeclemp.GuardianName = EmployeeDetails[i].GuardianName;
                Ecxeclemp.Qualification = EmployeeDetails[i].Qualification;
                Ecxeclemp.StaffCategory = EmployeeDetails[i].StaffCategory;
                Ecxeclemp.Department = EmployeeDetails[i].Department.ToString();
                Ecxeclemp.Designation = EmployeeDetails[i].Designation.ToString();
                Ecxeclemp.WorkExp = EmployeeDetails[i].WorkExp;
                Ecxeclemp.Gender = EmployeeDetails[i].Gender;
                Ecxeclemp.DateofBirth = EmployeeDetails[i].DateofBirth.ToString();
                Ecxeclemp.DateOfJoining = EmployeeDetails[i].DateOfJoining.ToString();
                Ecxeclemp.CastName = EmployeeDetails[i].CastName;
                Ecxeclemp.CurrentAddress = EmployeeDetails[i].CurrentAddress.ToString();
                Ecxeclemp.CurrDistrict = EmployeeDetails[i].CurrDistrict.ToString();
                Ecxeclemp.CurrState = EmployeeDetails[i].CurrState.ToString();
                Ecxeclemp.CurrCountry = EmployeeDetails[i].CurrCountry.ToString();
                Ecxeclemp.CurrentPIN = EmployeeDetails[i].CurrentPIN;
                Ecxeclemp.PermAddress = EmployeeDetails[i].PermAddress.ToString();
                Ecxeclemp.PerDistrict = EmployeeDetails[i].PerDistrict.ToString();
                Ecxeclemp.PerState = EmployeeDetails[i].PerState.ToString();
                Ecxeclemp.PerCountry = EmployeeDetails[i].PerCountry.ToString();
                Ecxeclemp.PermPIN = EmployeeDetails[i].PermPIN;
                Ecxeclemp.EmailID = EmployeeDetails[i].EmailID;
                Ecxeclemp.EmployeeType = EmployeeDetails[i].EmployeeType;
                Ecxeclemp.MaritalStatus = EmployeeDetails[i].MaritalStatus.ToString();
                Ecxeclemp.MobileNo = EmployeeDetails[i].MobileNo.ToString();
                Ecxeclemp.PhoneNo = EmployeeDetails[i].PhoneNo.ToString();
                Ecxeclemp.EmpGrade = EmployeeDetails[i].EmpGrade.ToString();
                Ecxeclemp.Description = EmployeeDetails[i].Description.ToString();
                ListexcelData.Add(Ecxeclemp);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public List<EmployeeData> GetEmployeeDetails(int curIndex)
        {

            EmployeeData objpat = new EmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.EmployeeNo = txtempnos.Text == "" ? null : txtempnos.Text.Trim();
            //objpat.EmpName = txtemployeename.Text == "" ? null : txtemployeename.Text.Trim();
            objpat.EmployeeID = Convert.ToInt64(txtemployeename.Text.Substring(txtemployeename.Text.LastIndexOf(':') + 1) == "" ? "0" : txtemployeename.Text.Substring(txtemployeename.Text.LastIndexOf(':') + 1));
            objpat.MobileNo = txtcontactnos.Text == "" ? null : txtcontactnos.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.StaffCategoryID = Convert.ToInt32(ddl_staffcategorys.SelectedValue == "" ? "0" : ddl_staffcategorys.SelectedValue);
            objpat.EmployeeTypeID = Convert.ToInt32(ddl_employeetypes.SelectedValue == "" ? "0" : ddl_employeetypes.SelectedValue);
            objpat.DepartmentID = Convert.ToInt32(ddl_departments.SelectedValue == "" ? "0" : ddl_departments.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objstdBO.SearchEmployeeRoleExcel(objpat);
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
        //public void ExportToPdf()
        //{
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //        {

        //            GvemployeeList.BorderStyle = BorderStyle.None;
        //            //Hide the Column containing CheckBox
        //            GvemployeeList.Columns[9].Visible = false;
        //            GvemployeeList.RenderControl(hw);
        //            GvemployeeList.HeaderRow.Style.Add("width", "15%");
        //            GvemployeeList.HeaderRow.Style.Add("font-size", "10px");
        //            GvemployeeList.Style.Add("text-decoration", "none");
        //            GvemployeeList.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
        //            GvemployeeList.Style.Add("font-size", "8px");
        //            StringReader sr = new StringReader(sw.ToString());
        //            Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //            pdfDoc.Open();
        //            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //            pdfDoc.Close();
        //            Response.ContentType = "application/pdf";
        //            Response.AddHeader("content-disposition", "attachment;filename=EmployeeDetails.pdf");
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.Write(pdfDoc);
        //            Response.End();
        //        }
        //    }
        //}
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void GvemployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Button updateButtonField = e.Row.Cells[10].Controls[0] as Button;
                //updateButtonField.Attributes["onclick"] = string.Format("if(confirm('Are you sure to update role ?')) __doPostBack('{0}','{1}${2}'); else return false;",
                //                                  GvemployeeList.ClientID,
                //                                  updateButtonField.CommandName,
                //                                  updateButtonField.CommandArgument);

                MasterLookupBO mstlookup = new MasterLookupBO();
                Label Status = (Label)e.Row.FindControl("lblRole");
                Label StatusID = (Label)e.Row.FindControl("lblRoleID");
                DropDownList ddlRole = (DropDownList)e.Row.FindControl("ddlEmpRole");
                Commonfunction.PopulateDdl(ddlRole, mstlookup.GetLookupsList(LookupName.OTroles));
                if (Status.Text == "")
                {
                    ddlRole.SelectedIndex = 0;
                }
                else
                {
                    ddlRole.SelectedItem.Text = Status.Text;
                    ddlRole.SelectedIndex = Convert.ToInt32(StatusID.Text);
                }
            }
        }

        protected void GvemployeeList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (LogData.UpdateEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "UpdateEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            Int32 ID = Convert.ToInt32(GvemployeeList.DataKeys[e.RowIndex].Values["EmployeeID"].ToString());
            System.Web.UI.WebControls.DropDownList RoleName = (System.Web.UI.WebControls.DropDownList)GvemployeeList.Rows[e.RowIndex].FindControl("ddlEmpRole");
            System.Web.UI.WebControls.Label RoleID = (System.Web.UI.WebControls.Label)GvemployeeList.Rows[e.RowIndex].FindControl("lblRoleID");

            EmployeeData objData = new EmployeeData();
            EmployeeBO objBO = new EmployeeBO();
            objData.EmployeeID = ID;
            objData.UserLoginId = LogData.EmployeeID;
            objData.RoleID = Convert.ToInt32(RoleName.SelectedValue);
           
            if (RoleName.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "Please select working status.", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                RoleName.Focus();
                return;
            }
            int result = objBO.UpdateEmpRole(objData);
            if (result > 0)
            {
                GvemployeeList.DataSource = null;
                GvemployeeList.DataBind();
                bindgrid(1);

                Messagealert_.ShowMessage(lblresult, "update", 1);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
    }
}