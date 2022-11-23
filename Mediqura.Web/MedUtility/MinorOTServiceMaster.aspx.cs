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
    public partial class MinorOTServiceMaster : BasePage
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
            Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.MinorOTServiceType));
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
        protected void ddlservicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_servicetype.SelectedIndex > 0)
            {
                txtservice.ReadOnly = false;
                AutoCompleteExtender3.ContextKey = ddl_servicetype.SelectedValue;
                txtservice.Text = "";
            
            }
            else
            {
                AutoCompleteExtender3.ContextKey = null;
                txtservice.ReadOnly = true;
                txtservice.Text = "";
           
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddl_BedClass.SelectedIndex = 0;
            txtservice.Text = "";
            ddl_patienttype.SelectedIndex = 0;
            GvMinorOT.DataSource = null;
            GvMinorOT.DataBind();
            GvMinorOT.Visible = false;
            divmsg1.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            divmsg3.Visible = false;
            lblresult.Visible = false;
            ddl_servicetype.SelectedIndex = 0;
         
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

                if (ddl_servicetype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ServiceType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_servicetype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;

                }
                if (txtservice.Text == " ")
                {
                    Messagealert_.ShowMessage(lblmessage, "Service", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtservice.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;

                }
                if (ddl_patienttype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "PatientType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_patienttype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            

                List<MinorOTServicesPayOutData> lstemp = GetMinorOTServicesPayOutDetails(0);
                if (lstemp.Count > 0)
                {
                    GvMinorOT.DataSource = lstemp;
                    GvMinorOT.DataBind();
                    GvMinorOT.Visible = true;
                    btnexport.Visible = true;
                    ddlexport.Visible = true;
                }
                else
                {
                    GvMinorOT.DataSource = null;
                    GvMinorOT.DataBind();
                    GvMinorOT.Visible = true;
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
        private List<MinorOTServicesPayOutData> GetMinorOTServicesPayOutDetails(int p)
        {

            List<MinorOTServicesPayOutData> List = new List<MinorOTServicesPayOutData>();
            MinorOTServicesPayOutBO objBO = new MinorOTServicesPayOutBO();
            MinorOTServicesPayOutData objData = new MinorOTServicesPayOutData();
            objData.ServiceTypeID = Convert.ToInt32(ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue);
            string ID;
            var source = txtservice.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objData.ServiceID = Convert.ToInt32(ID);
            }
            objData.PatientTypeID = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
        
       
            objData.EmployeeID = LogData.EmployeeID;
            objData.FinancialYearID = LogData.FinancialYearID;
            objData.HospitalID = LogData.HospitalID;
            return objBO.SearchMinorOTPayOutDetails(objData);
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
                if (ddl_BedClass.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "BedClass", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_BedClass.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;

                }
                List<MinorOTServicesPayOutData> List = new List<MinorOTServicesPayOutData>();
                MinorOTServicesPayOutBO objBO = new MinorOTServicesPayOutBO();
                MinorOTServicesPayOutData objData = new MinorOTServicesPayOutData();
            
                foreach (GridViewRow row in GvMinorOT.Rows)
                {
                    Label ID = (Label)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label code = (Label)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("lbl_code");
                    Label serviceid = (Label)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("lbl_serviceID");
                    TextBox rate = (TextBox)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("txtrate");
                    DropDownList sharetype = (DropDownList)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("ddl_sharetype");

                    TextBox hosshare = (TextBox)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_hosshare");
                    TextBox conshare = (TextBox)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_consultshare");
                    TextBox stafffund = (TextBox)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_stafffund");
                    TextBox corpusfund = (TextBox)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_corpusfund");
             
                    
                    CheckBox chk = (CheckBox)GvMinorOT.Rows[row.RowIndex].Cells[0].FindControl("chekbox");

                    MinorOTServicesPayOutData obj1 = new MinorOTServicesPayOutData();
                    if (chk.Checked == true)
                    {
                        obj1.ID = Convert.ToInt64(ID.Text);
                        obj1.ServiceCode = code.Text;
                        obj1.ServiceID = Convert.ToInt32(serviceid.Text);
                        obj1.ServiceCharge = Convert.ToInt32(rate.Text);
                        obj1.ShareType = Convert.ToInt32(sharetype.SelectedValue);
                      
                        if (sharetype.SelectedIndex == 1) 
                        {
                            if ((Convert.ToDouble(hosshare.Text) <= 100) && (Convert.ToDouble(stafffund.Text) <=100) && (Convert.ToDouble(corpusfund.Text) <=100) 
                                && (Convert.ToDouble(conshare.Text) <= 100) && (Convert.ToDouble(hosshare.Text) + Convert.ToDouble(conshare.Text) + Convert.ToDouble(stafffund.Text) + Convert.ToDouble(corpusfund.Text)) <= 100)
                            {
                                obj1.HospitalShare = Convert.ToDecimal(hosshare.Text);
                                obj1.ConsultantShare = Convert.ToDecimal(conshare.Text);
                                obj1.StaffFund = Convert.ToDecimal(stafffund.Text);
                                obj1.CorpusFund = Convert.ToDecimal(corpusfund.Text);
                            }
                            else
                            {
                                Messagealert_.ShowMessage(lblmessage, "PC", 0);
                                divmsg1.Visible = true;
                                divmsg1.Attributes["class"] = "FailAlert";
                                stafffund.Focus();
                                return;
                            }
                        }
                        else if (sharetype.SelectedIndex == 2)
                        {
                            if ((Convert.ToDouble(hosshare.Text) + Convert.ToDouble(conshare.Text) + Convert.ToDouble(stafffund.Text) + Convert.ToDouble(corpusfund.Text)) <= (Convert.ToInt32(rate.Text)))
                            {
                                obj1.HospitalShare = Convert.ToDecimal(hosshare.Text);
                                obj1.ConsultantShare = Convert.ToDecimal(conshare.Text);
                                obj1.StaffFund = Convert.ToInt32(stafffund.Text);
                                obj1.CorpusFund = Convert.ToInt32(corpusfund.Text);
                           }
                            else
                            {
                                Messagealert_.ShowMessage(lblmessage, "Share", 0);
                                divmsg1.Visible = true;
                                divmsg1.Attributes["class"] = "FailAlert";
                                stafffund.Focus();
                                return;

                            }
                        }

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
                        List.Add(obj1);
                    }

                }


                objData.XMLData = XmlConvertor.MinorOTRecordDatatoXML(List).ToString();
                objData.BedClassID = Convert.ToInt32(ddl_BedClass.SelectedValue == "" ? "0" : ddl_BedClass.SelectedValue);

                objData.EmployeeID = LogData.EmployeeID;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.IPaddress = LogData.IPaddress;
                objData.HospitalID = LogData.HospitalID;

                objData.ActionType = Enumaction.Insert;

                int result = objBO.UpdateMinorOTServicesMST(objData);
                if (result == 1)
                {
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
                wb.Worksheets.Add(dt, "Department Type Detail List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=MinorOTPayOutDetails.xlsx");
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
            List<MinorOTServicesPayOutData> OPDEmergencyDetails = GetMinorOTServicesPayOutDetails(0);
            List<MinorOTServicesPayOutDatatoExcel> ListexcelData = new List<MinorOTServicesPayOutDatatoExcel>();
            int i = 0;
            foreach (MinorOTServicesPayOutData row in OPDEmergencyDetails)
            {
                MinorOTServicesPayOutDatatoExcel ExcelSevice = new MinorOTServicesPayOutDatatoExcel();
                ExcelSevice.ServiceCode = OPDEmergencyDetails[i].ServiceCode;
                ExcelSevice.ServiceName = OPDEmergencyDetails[i].ServiceName;
                ExcelSevice.ServiceCharge =Commonfunction.Getrounding(OPDEmergencyDetails[i].ServiceCharge.ToString());
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
                ExcelSevice.CorpusFund = Commonfunction.Getrounding(OPDEmergencyDetails[i].CorpusFund.ToString());
                ExcelSevice.StaffFund = Commonfunction.Getrounding(OPDEmergencyDetails[i].StaffFund.ToString());

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
        protected void GvMinorOT_RowDataBound(object sender, GridViewRowEventArgs e)
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

