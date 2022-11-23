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
using Mediqura.Web.MedCommon;
using Mediqura.CommonData.Common;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.MedBillData;
using Mediqura.Utility;
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Mediqura.Web.MedHR
{
    public partial class EmployeeDiscount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                SetInitialRow();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmpdetails(string prefixText, int count, string contextKey)
        {

            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            Objpaic.EmployeeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetEmpdetails(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                DropDownList ddl1 = (e.Row.FindControl("ddl_relationship") as DropDownList);
                DropDownList ddl2 = (e.Row.FindControl("ddl_gender") as DropDownList);

                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl1, mstlookup.GetLookupsList(LookupName.Relationship));
                Commonfunction.PopulateDdl(ddl2, mstlookup.GetLookupsList(LookupName.Gender));

            }
        }


        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("DependentName", typeof(string)));
            dt.Columns.Add(new DataColumn("DependentID", typeof(string)));

            dt.Columns.Add(new DataColumn("ItemID", typeof(string)));
            dt.Columns.Add(new DataColumn("DOB", typeof(string)));
            dt.Columns.Add(new DataColumn("Age", typeof(string)));
            dt.Columns.Add(new DataColumn("AppDt", typeof(string)));
            dt.Columns.Add(new DataColumn("IssueDt", typeof(string)));
            dt.Columns.Add(new DataColumn("ValidDt", typeof(string)));
            dt.Columns.Add(new DataColumn("SurDt", typeof(string)));

            dt.Columns.Add(new DataColumn("EmployeeID", typeof(string)));
            dt.Columns.Add(new DataColumn("DiscountIPD", typeof(string)));
            dt.Columns.Add(new DataColumn("DiscountOPD", typeof(string)));
            dt.Columns.Add(new DataColumn("DiscountOPDLab", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;

            dt.Rows.Add(dr);


            ViewState["CurrentTable"] = dt;

            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;

            DropDownList ddl1 = (DropDownList)GridView1.Rows[0].Cells[0].FindControl("ddl_relationship");
            DropDownList ddl2 = (DropDownList)GridView1.Rows[0].Cells[0].FindControl("ddl_gender");

            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl1, mstlookup.GetLookupsList(LookupName.Relationship));
            Commonfunction.PopulateDdl(ddl2, mstlookup.GetLookupsList(LookupName.Gender));

        }
        private void AddNewRowToGrid()
        {
            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add("DependentID");
                dt.Columns.Add("DependentName");
                dt.Columns.Add("DOB");
                dt.Columns.Add("Age");
                dt.Columns.Add("AppDt");
                dt.Columns.Add("IssueDt");
                dt.Columns.Add("ValidDt");
                dt.Columns.Add("SurDt");

                dt.Columns.Add("DiscountIPD");
                dt.Columns.Add("DiscountOPD");
                dt.Columns.Add("DiscountOPDLab");

                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["DependentID"] = ((Label)gvRow.FindControl("hdnID")).Text;
                    dr1["DependentName"] = ((TextBox)gvRow.FindControl("txt_name")).Text;
                    dr1["DOB"] = ((TextBox)gvRow.FindControl("txt_DOB")).Text;
                    dr1["AppDt"] = ((TextBox)gvRow.FindControl("txt_app")).Text;

                    dr1["IssueDt"] = ((TextBox)gvRow.FindControl("txt_IssueDt")).Text;
                    dr1["ValidDt"] = ((TextBox)gvRow.FindControl("txt_val")).Text;
                    dr1["SurDt"] = ((TextBox)gvRow.FindControl("txt_sur")).Text;

                    dr1["Age"] = ((TextBox)gvRow.FindControl("txtage")).Text;
                    dr1["DiscountIPD"] = ((TextBox)gvRow.FindControl("txtdiscountipd")).Text;
                    dr1["DiscountOPD"] = ((TextBox)gvRow.FindControl("txtdiscountopd")).Text;
                    dr1["DiscountOPDLab"] = ((TextBox)gvRow.FindControl("txtdiscountopdlab")).Text;

                    dt.Rows.Add(dr1);
                }
                DataRow dr2 = dt.NewRow();
                dr2["DependentID"] = "";

                dr2["DependentName"] = "";
                dr2["DOB"] = "";
                dr2["Age"] = "";
                dr2["AppDt"] = "";
                dr2["IssueDt"] = "";
                dr2["ValidDt"] = "";
                dr2["SurDt"] = "";

                dr2["DiscountIPD"] = "";
                dr2["DiscountOPD"] = "";
                dr2["DiscountOPDLab"] = "";

                dt.Rows.Add(dr2);



                DropDownList ddl1 = (DropDownList)GridView1.Rows[0].Cells[0].FindControl("ddl_relationship");
                DropDownList ddl2 = (DropDownList)GridView1.Rows[0].Cells[0].FindControl("ddl_gender");

                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl1, mstlookup.GetLookupsList(LookupName.Relationship));
                Commonfunction.PopulateDdl(ddl2, mstlookup.GetLookupsList(LookupName.Gender));

                GridView1.DataSource = dt;
                GridView1.DataBind();
                //   GridView1.Visible = true;
            }

            else
            {
                SetInitialRow();

            }



        }
        protected void ddl_relationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewDetails.Show();
        }
        private List<EmployeeData> GetDependentList(int p)
        {
            EmployeeData objstock = new EmployeeData();
            EmployeeBO objBO = new EmployeeBO();
            foreach (GridViewRow row in gvemployeedetails.Rows)
            {
                Label ID = (Label)gvemployeedetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                objstock.EmployeeID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);

            }
            return objBO.GetDependentList(objstock);
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            //Hide the modal popup extender
            GridViewDetails.Hide();
        }

        protected void btn_save_Click(object sender, EventArgs e)
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
            List<EmployeeData> List = new List<EmployeeData>();
            EmployeeBO objBO = new EmployeeBO();
            EmployeeData objrec = new EmployeeData();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvemployeedetails.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label empid = (Label)gvemployeedetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");

                    Label Name = (Label)gvemployeedetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_name");
                    TextBox ipd = (TextBox)gvemployeedetails.Rows[row.RowIndex].Cells[0].FindControl("txtdiscountipd");
                    TextBox opd = (TextBox)gvemployeedetails.Rows[row.RowIndex].Cells[0].FindControl("txtdiscountopd");
                    TextBox inv = (TextBox)gvemployeedetails.Rows[row.RowIndex].Cells[0].FindControl("txtdiscountopdlab");

                    EmployeeData obj1 = new EmployeeData();
                    obj1.EmployeeID = Convert.ToInt64(empid.Text);
                    obj1.EmpName = Name.Text;
                    obj1.DiscountIPD = Convert.ToDouble(ipd.Text);
                    obj1.DiscountOPD = Convert.ToDouble(opd.Text);
                    obj1.DiscountOPDLab = Convert.ToDouble(inv.Text);
                    List.Add(obj1);

                }
                objrec.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                objrec.XMLData = XmlConvertor.EmployeeDiscountRecordDatatoXML(List).ToString();
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.UserLoginId = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                int result = objBO.UpdateEmployeeDiscount(objrec);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
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
           List<EmployeeData> List = new List<EmployeeData>();
            EmployeeBO objBO = new EmployeeBO();
            EmployeeData objrec = new EmployeeData();
            try
            {
                EmployeeData Objpaic = new EmployeeData();
                EmployeeBO objInfoBO = new EmployeeBO();
                List<EmployeeData> getResult = new List<EmployeeData>();
                foreach (GridViewRow row in gvemployeedetails.Rows)
                {
                    Label ID = (Label)gvemployeedetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Objpaic.EmployeeID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                }
                getResult = objInfoBO.GetDependentList(Objpaic);

                for (int i = getResult.Count; i < GridView1.Rows.Count ; i++)
                {

                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    TextBox Name = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_name");

                    DropDownList reln = (DropDownList)GridView1.Rows[i].Cells[0].FindControl("ddl_relationship");
                    DropDownList gen = (DropDownList)GridView1.Rows[i].Cells[0].FindControl("ddl_gender");

                    TextBox DOB = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_DOB");
                    TextBox Age = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txtage");
                    TextBox app = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_app");
                    TextBox iss = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_IssueDt");
                    TextBox val = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_val");
                    TextBox sur = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_sur");

                    TextBox ipd = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txtdiscountipd");
                    TextBox opd = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txtdiscountopd");
                    TextBox inv = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txtdiscountopdlab");
                    EmployeeData obj1 = new EmployeeData();
                    obj1.DependentName = Name.Text;
                    obj1.Relation = Convert.ToInt32(reln.SelectedValue);
                    obj1.Gender = gen.SelectedValue;

                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime dob = DOB.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(DOB.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    DateTime appdt = app.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(app.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    DateTime issuedt = iss.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(iss.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    DateTime valdt = val.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(val.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    DateTime surdt = sur.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(sur.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                    obj1.DOB = Convert.ToDateTime(dob);
                    obj1.AppDt = Convert.ToDateTime(appdt);
                    obj1.IssueDt = Convert.ToDateTime(issuedt);
                    obj1.ValidDt = Convert.ToDateTime(valdt);
                    obj1.SurDt = Convert.ToDateTime(surdt);

                    obj1.Age = Convert.ToInt32(Age.Text);
                    obj1.DiscountIPD = Convert.ToDouble(ipd.Text);
                    obj1.DiscountOPD = Convert.ToDouble(opd.Text);
                    obj1.DiscountOPDLab = Convert.ToDouble(inv.Text);
                    List.Add(obj1);

                }
                foreach (GridViewRow row in gvemployeedetails.Rows)
                {
                    Label ID = (Label)gvemployeedetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    objrec.EmployeeID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                }
                objrec.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                objrec.XMLData = XmlConvertor.EmployeeDependentRecordDatatoXML(List).ToString();
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.UserLoginId = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                int result = objBO.UpdateEmployeeDependent(objrec);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "save", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        protected void bindgrid()
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

                if (ddldepartment.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;

                }
                else
                {
                    divmsg1.Visible = false;
                }

                List<EmployeeData> obj = GetEmployeeList(0);
                if (obj.Count > 0)
                {

                    gvemployeedetails.DataSource = obj;
                    gvemployeedetails.DataBind();
                    gvemployeedetails.Visible = true;

                }
                else
                {
                    gvemployeedetails.DataSource = null;
                    gvemployeedetails.DataBind();
                    gvemployeedetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }

        private List<EmployeeData> GetEmployeeList(int p)
        {
            EmployeeData objstock = new EmployeeData();
            EmployeeBO objBO = new EmployeeBO();
            var source1 = txt_empname.Text.ToString();
            string ID1;
            if (source1.Contains(":"))
            {
                ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                objstock.EmployeeID = Convert.ToInt64(ID1);

            }
            objstock.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            return objBO.GetEmployeeList(objstock);

        }

        protected void txt_empname_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();

        }
        protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
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
                    PatientData objstock = new PatientData();
                    RegistrationBO objBO = new RegistrationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GridView1.Rows[i];

                    Label ID = (Label)gr.Cells[0].FindControl("hdnID");

                    objstock.ID = Convert.ToInt32(ID.Text);

                    objstock.EmployeeID = LogData.EmployeeID;

                    int Result = objBO.DeleteDependent(objstock);
                    if (Result == 1)
                    {
                        bindgrid();
                        AddNewRowToGrid();

                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        div1.Attributes["class"] = "SucessAlert";
                        div1.Visible = true;
                        GridViewDetails.Show();
                        List<EmployeeData> obj = GetDependentList(0);
                        if (obj.Count > 0)
                        {

                            GridView1.DataSource = obj;
                            GridView1.DataBind();
                            GridView1.Visible = true;
                            AddNewRowToGrid();

                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            GridView1.Visible = true;
                            SetInitialRow();
                        }

            
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                    }

                }
                if (e.CommandName == "Add")
                {
                    AddNewRowToGrid();
                     GridViewDetails.Show();


                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void gvemployeedetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
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
                    PatientData objstock = new PatientData();
                    RegistrationBO objBO = new RegistrationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvemployeedetails.Rows[i];

                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");

                    objstock.ID = Convert.ToInt32(ID.Text);

                    objstock.EmployeeID = LogData.EmployeeID;

                    int Result = objBO.DeleteEmployeeDiscount(objstock);
                    if (Result == 1)
                    {
                        bindgrid();
                        AddNewRowToGrid();

                        Messagealert_.ShowMessage(lblmessage, "cancel", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                    }

                }

                if (e.CommandName == "Select")
                {
                    GridViewDetails.Show();
                    List<EmployeeData> obj = GetDependentList(0);
                    if (obj.Count > 0)
                    {

                        GridView1.DataSource = obj;
                        GridView1.DataBind();
                        GridView1.Visible = true;
                        AddNewRowToGrid();

                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        GridView1.Visible = true;
                        SetInitialRow();
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

        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddldepartment.SelectedIndex = 0;
            txt_empname.Text = "";
            gvemployeedetails.DataSource = null;
            gvemployeedetails.DataBind();
            gvemployeedetails.Visible = false;
            divmsg1.Visible = false;


        }


    }
}