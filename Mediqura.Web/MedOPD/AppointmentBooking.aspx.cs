using Mediqura.BOL.CommonBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;

using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.CommonData.MedBillData;
using Mediqura.BOL.MedBillBO;
using System.Collections.Specialized;
using Mediqura.CommonData.PatientData;

namespace Mediqura.Web.MedOPD
{
    public partial class AppointmentBooking : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 bindddl();
                AddNewRowToGrid();
                txtdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                Session["BookingDataList"] = null;
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }
 
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlconsultant, mstlookup.GetLookupsList(LookupName.Doctor));
         }
        private void AddNewRowToGrid()
        {
            List<BookingData> BookingDataList = Session["BookingDataList"] == null ? new List<BookingData>() : (List<BookingData>)Session["BookingDataList"];
            BookingData Obj = new BookingData();
            Obj.DoctorID = Convert.ToInt64(ddlconsultant.SelectedValue);
            Obj.ID = 0;
            Obj.RowNo = ((gvbookingdetails.Rows.Count) + 1);
            BookingDataList.Add(Obj);
            if (BookingDataList.Count > 0)
            {
                gvbookingdetails.DataSource = BookingDataList;
                gvbookingdetails.DataBind();
                gvbookingdetails.Visible = true;
                Session["BookingDataList"] = BookingDataList;
            }
            else
            {
                gvbookingdetails.DataSource = null;
                gvbookingdetails.DataBind();
                gvbookingdetails.Visible = true;
            }

        }
      
        protected void txt_remarks_TextChanged(object sender, EventArgs e)
        {
            try
            {  
                TextBox txt = sender as TextBox;
                GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
                int rowIndex = currentrow.RowIndex;
                BookingData Objpaic = new BookingData();
                RegistrationBO objInfoBO = new RegistrationBO();
                TextBox box1 = (TextBox)gvbookingdetails.Rows[rowIndex].Cells[1].FindControl("txt_name");
                TextBox box2 = (TextBox)gvbookingdetails.Rows[rowIndex].Cells[2].FindControl("txtaddress");
                TextBox box3 = (TextBox)gvbookingdetails.Rows[rowIndex].Cells[3].FindControl("txt_contact");
                TextBox box4 = (TextBox)gvbookingdetails.Rows[rowIndex].Cells[4].FindControl("txt_age");
                TextBox box5 = (TextBox)gvbookingdetails.Rows[rowIndex].Cells[5].FindControl("txt_time");
                TextBox box6 = (TextBox)gvbookingdetails.Rows[rowIndex].Cells[6].FindControl("txt_remarks");
                Label ID = (Label)gvbookingdetails.Rows[rowIndex].Cells[5].FindControl("lbl_ID");

                if (ddlconsultant.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Consultant", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;

                }
                else
                {
                    divmsg1.Visible = false;
                }


                if (box1.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter name.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    box1.Focus();
                    return;

                }
                else
                {
                    divmsg1.Visible = false;
                }
                if (box2.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter address.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    box2.Focus();
                    return;

                }
                else
                {
                    divmsg1.Visible = false;
                }
                if (box3.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter contact no.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    box3.Focus();
                    return;

                }
                else
                {
                    divmsg1.Visible = false;
                } if (box4.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter age.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    box4.Focus();
                    return;

                }
                else
                {
                    divmsg1.Visible = false;
                }
                if (txtdate.Text != "" )
                {
                    if (Commonfunction.isValidDate(txtdate.Text) == false || Commonfunction.ChecklowerAppointmentDate(txtdate.Text))
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdate.Focus();
                        return;
                    }
                }
                    Objpaic.PatientName = box1.Text;
                    Objpaic.Address = box2.Text;
                    Objpaic.ContactNo = box3.Text;
                    Objpaic.Age = Convert.ToInt32(box4.Text);
                    Objpaic.Remarks = box6.Text;
                    Objpaic.Time = box5.Text;

                    Objpaic.DoctorID = Convert.ToInt32(ddlconsultant.SelectedValue == "" ? "0" : ddlconsultant.SelectedValue);
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime bookingdate = txtdate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    Objpaic.BookingDate = bookingdate;
                    Objpaic.FinancialYearID = LogData.FinancialYearID;
                    Objpaic.HospitalID = LogData.HospitalID;
                    Objpaic.EmployeeID = LogData.EmployeeID;
                    Objpaic.IPaddress = LogData.IPaddress;
                    Objpaic.ID = Convert.ToInt64(ID.Text);
                   // if (Objpaic.Time != null)
                   // {
                   //      if (Commonfunction.ChecklowerTime(Objpaic.Time)==true)
                   //         {
                   //              Messagealert_.ShowMessage(lblmessage, "Please enter time for appointment.", 0);
                   //             divmsg1.Attributes["class"] = "FailAlert";
                   //             divmsg1.Visible = true;
                   //             box5.Focus();
                   //             return;
                   //         }
                   //         else
                   //         {
                   //             divmsg1.Visible = false;
                   //         }
                           
                   //}
                   //else
                   //{
                   //     if (box5.Text == System.DateTime.Now.ToString("hh:mm:ss tt") || Commonfunction.isValidTime(box5.Text) == false || Commonfunction.ChecklowerTime(box5.Text)==true)
                   //     {
                   //             Messagealert_.ShowMessage(lblmessage, "Please enter time for appointment.", 0);
                   //             divmsg1.Attributes["class"] = "FailAlert";
                   //             divmsg1.Visible = true;
                   //             box5.Focus();
                   //             return;
                   //      }
                   //      else
                   //      {
                   //             divmsg1.Visible = false;
                   //       }
                   //}

                  

                    int result = objInfoBO.InsertBookingdetails(Objpaic);
                    if (result == 1 || result == 2)
                    {
                        lblmessage.Visible = true;
                        Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        ViewState["ID"] = null;
                        gvbookingdetails.DataSource = null;
                        gvbookingdetails.DataBind();
                        gvbookingdetails.Visible = true;

                        bindgrid();
                    }
            
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                    } 
                }
              
          
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
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
                       
                if (ddlconsultant.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Consultant", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;

                }
                else
                {
                    divmsg1.Visible = false;
                }
                if (txtdate.Text == "")
                {
                    if (Commonfunction.isValidDate(txtdate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdate.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg1.Visible = false;
                }
                List<BookingData> obj = GetPatientList(0);
                if (obj.Count > 0)
                {
                    List<BookingData> BookingDataList = Session["BookingDataList"] == null ? new List<BookingData>() : (List<BookingData>)Session["BookingDataList"];
                    Session["BookingDataList"] = obj;
                    gvbookingdetails.DataSource = Session["BookingDataList"];
           

                    gvbookingdetails.DataSource = obj;
                    gvbookingdetails.DataBind();
                    gvbookingdetails.Visible = true;

                }
                else
                {
                    gvbookingdetails.DataSource = null;
                    gvbookingdetails.DataBind();
                    gvbookingdetails.Visible = true;
                }
                AddNewRowToGrid();

            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<BookingData> GetPatientList(int p)
        {
            BookingData objstock = new BookingData();
            RegistrationBO objBO = new RegistrationBO();
            objstock.DoctorID = Convert.ToInt32(ddlconsultant.SelectedValue == "" ? "0" : ddlconsultant.SelectedValue);
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime bookingdate = txtdate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.BookingDate = bookingdate;
            return objBO.GetPatientList(objstock);

        }
        protected void gvbookingdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            foreach (GridViewRow row in gvbookingdetails.Rows)
            {
                try
                {
                    BookingData obj = new BookingData();
                    Label ID = (Label)gvbookingdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    TextBox box1 = (TextBox)gvbookingdetails.Rows[row.RowIndex].Cells[0].FindControl("txt_time");
                    TextBox box2 = (TextBox)gvbookingdetails.Rows[row.RowIndex].Cells[0].FindControl("txt_name");
                    TextBox box3 = (TextBox)gvbookingdetails.Rows[row.RowIndex].Cells[0].FindControl("txt_remarks");
                    if (ID.Text == "0")
                    {
                         box1.Text = System.DateTime.Now.ToString("hh:mm:ss tt");
                    }
                    box2.Focus();
                }
                catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvbookingdetails.DataSource = null;
            gvbookingdetails.DataBind();
            gvbookingdetails.Visible = true;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlconsultant, mstlookup.GetLookupsList(LookupName.Doctor));
            Session["BookingDataList"] = null;
            txtdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void gvbookingdetails_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvbookingdetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    if (ID.Text == "0")
                    {
                        List<BookingData> BookingDataList = Session["BookingDataList"] == null ? new List<BookingData>() : (List<BookingData>)Session["BookingDataList"];
                        BookingDataList.RemoveAt(i);
                        if (BookingDataList.Count > 0)
                        {
                            Session["BookingDataList"] = BookingDataList;
                            gvbookingdetails.DataSource = BookingDataList;
                            gvbookingdetails.DataBind();
                        }
                        else
                        {
                            Session["NurseNotesDataList"] = null;
                            gvbookingdetails.DataSource = null;
                            gvbookingdetails.DataBind();
                        }
                 

                    }
                       

                     else
                    {
                        PatientData objstock = new PatientData();
                        RegistrationBO objBO = new RegistrationBO();
                        objstock.ID = Convert.ToInt32(ID.Text);
                        objstock.EmployeeID = LogData.EmployeeID;
                        int Result = objBO.CancelAppointment(objstock);
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

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<BookingData> DepositDetails = GetPatientList(0);
            List<PatientListDataTOeXCEL> ListexcelData = new List<PatientListDataTOeXCEL>();
            int i = 0;
            foreach (BookingData row in DepositDetails)
            {
                PatientListDataTOeXCEL Ecxeclpat = new PatientListDataTOeXCEL();
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Address = DepositDetails[i].Address;
                Ecxeclpat.ContactNo = DepositDetails[i].ContactNo;
                Ecxeclpat.Age = DepositDetails[i].Age;
                Ecxeclpat.Remarks = DepositDetails[i].Remarks;
                Ecxeclpat.BookingDate = DepositDetails[i].BookingDate;
                Ecxeclpat.Time = DepositDetails[i].Time;
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName;

                ListexcelData.Add(Ecxeclpat);
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

                // Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {

                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);

                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {

                        //       inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);

                    }

                    dataTable.Rows.Add(values);

                }

                //     put a breakpoint here and check datatable

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
                wb.Worksheets.Add(dt, "Item CheckList");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AppointmentBookingDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblmessage, "Exported", 1);
                divmsg1.Attributes["class"] = "SucessAlert";
            }
        }
        protected void gvbookingdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvbookingdetails.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}

