
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedHrData;
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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedUtility
{
    public partial class ServicewisedoctorPercentage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlbind();
               
            }
            checkSelect();
        }
        public void checkSelect(){
            if (ddl_servicetype.SelectedIndex == 0)
            {
                txt_service.ReadOnly = true;
            }
            else {
                txt_service.ReadOnly = false;
            }
            if (ddl_doctorType.SelectedIndex == 0)
            {
                txt_doctor.ReadOnly = true;
            }
            else
            {
                txt_doctor.ReadOnly = false;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.ServiceType));
            Commonfunction.PopulateDdl(ddl_doctorType, mstlookup.GetLookupsList(LookupName.DoctorType));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServiceName(string prefixText, int count, string contextKey)
        {
            ServicesData ObjServiceRange = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            ObjServiceRange.ServiceName = prefixText;
            ObjServiceRange.ServiceTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.SearchServiceDetailsByType(ObjServiceRange);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
         [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDoctor(string prefixText, int count, string contextKey)
        {

                DoctorMasterData ObjDoctorData = new DoctorMasterData();
                DoctorMasterBO objDoctorBO = new DoctorMasterBO();
                List<DoctorMasterData> getResult = new List<DoctorMasterData>();
                ObjDoctorData.DoctorName = prefixText;
                ObjDoctorData.DoctorType = Convert.ToInt32(contextKey);
                getResult = objDoctorBO.SearchDoctorByType(ObjDoctorData);
                List<String> list = new List<String>();
                for (int i = 0; i < getResult.Count; i++)
                {
                    list.Add(getResult[i].DoctorName.ToString());
                }
           

            return list;
        }

        protected void ddl_servicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            auto_service.ContextKey = ddl_servicetype.SelectedValue;
        }

        protected void ddl_doctorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            auto_doctor.ContextKey = ddl_doctorType.SelectedValue;
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                CommissionMasterData objCommissionData = new CommissionMasterData();
                CommissionMasterBO objCommissionBO = new CommissionMasterBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "FailAlert";
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
                if (ddl_doctorType.SelectedIndex == 0)
                {

                    Messagealert_.ShowMessage(lblmessage, "DoctorType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_doctorType.Focus();
                    return;

                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txt_service.Text == "" || !txt_service.Text.ToString().Contains(":") || !txt_service.Text.ToString().Contains(">"))
                {
                    txt_service.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Service", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_service.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_doctor.Text == "" || !txt_doctor.Text.ToString().Contains(":"))
                {
                    txt_doctor.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "DoctorName", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_doctor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_tax.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Tax", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_tax.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_commission.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Commission", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_commission.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                
               
                String serviceText = txt_service.Text == "" ? null : txt_service.Text.ToString().Trim();
                String[] service = serviceText.Split(new[] { ":" }, StringSplitOptions.None);
                String[] serviceCharge = service[0].Trim().Split(new[] { ">","ID" }, StringSplitOptions.None);

                String doctorText = txt_doctor.Text == "" ? null : txt_doctor.Text.ToString().Trim();
                String[] doctor = doctorText.Split(new[] { ":" }, StringSplitOptions.None);
                decimal serCharge = Convert.ToDecimal(serviceCharge[1]);
                decimal comCharge = Convert.ToDecimal(txt_commission.Text.ToString());
                if (comCharge>serCharge)
                {
                    Messagealert_.ShowMessage(lblmessage, "CommissionCheck", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_commission.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                objCommissionData.Servicetype = Convert.ToInt32(ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue);
                objCommissionData.ServiceID = Convert.ToInt32(service[1]);
                objCommissionData.DoctorID = Convert.ToInt32(doctor[1]);
                objCommissionData.Doctortype = Convert.ToInt32(ddl_doctorType.SelectedValue == "" ? "0" : ddl_doctorType.SelectedValue);
                objCommissionData.ServiceCharge = Convert.ToDecimal(serviceCharge[1]);
                objCommissionData.Commission = Convert.ToDecimal(txt_commission.Text == "" ? null : txt_commission.Text.ToString());
                objCommissionData.Tax = Convert.ToDecimal(txt_tax.Text == "" ? null : txt_tax.Text.ToString());
               
                objCommissionData.AddedBy = LogData.AddedBy;
                objCommissionData.EmployeeID = LogData.EmployeeID;
                objCommissionData.FinancialYearID = LogData.FinancialYearID;
                objCommissionData.HospitalID = LogData.HospitalID;
                objCommissionData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        //ddlsalute.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    objCommissionData.ActionType = Enumaction.Update;
                    objCommissionData.CommissionID = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                }
                int results = objCommissionBO.SaveCommissionData(objCommissionData);

                if (results == 1 || results == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, results == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    bindgrid();
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
                string msg=ex.ToString();
                Messagealert_.ShowMessage(lblmessage, msg, 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {

                List<CommissionMasterData> commissionDetails = GetCommissionData(0);
                if (commissionDetails.Count > 0)
                {
                    GvCommission.DataSource = commissionDetails;
                    GvCommission.DataBind();
                    GvCommission.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + commissionDetails[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                  

                }
                else
                {
                    
                    divmsg3.Visible = false;
                    GvCommission.DataSource = null;
                    GvCommission.DataBind();
                    GvCommission.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<CommissionMasterData> GetCommissionData(int curIndex)
        {

            CommissionMasterData objCommissionData = new CommissionMasterData();
            CommissionMasterBO objCommissionBO = new CommissionMasterBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            String serviceText = txt_service.Text == "" ? null : txt_service.Text.ToString().Trim();
            String serviceid = "0";
            String servicecharge = "0";
            String doctorid = "0";
            if (serviceText != null)
            {
                String[] service = serviceText.Split(new[] { ":" }, StringSplitOptions.None);
                serviceid = service[1];
                String[] serviceCharge = service[0].Trim().Split(new[] { ">", "ID" }, StringSplitOptions.None);
                servicecharge = serviceCharge[1];
            }
            String doctorText = txt_doctor.Text == "" ? null : txt_doctor.Text.ToString().Trim();
            if (doctorText != null)
            {
                String[] doctor = doctorText.Split(new[] { ":" }, StringSplitOptions.None);
                doctorid = doctor[1];
            }
            objCommissionData.Servicetype = Convert.ToInt32(ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue);
            objCommissionData.ServiceID = Convert.ToInt32(serviceid);
            objCommissionData.DoctorID = Convert.ToInt32(doctorid);
            objCommissionData.Doctortype = Convert.ToInt32(ddl_doctorType.SelectedValue == "" ? "0" : ddl_doctorType.SelectedValue);
           // objCommissionData.ServiceCharge = Convert.ToDouble(servicecharge);
               
            return objCommissionBO.SearchCommssiontDetails(objCommissionData);

        }
        protected void GvCommission_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvCommission.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Int64 ComID = Convert.ToInt64(ID.Text);
                    EditCommssion(ComID);
                  
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    CommissionMasterData objCommissionData = new CommissionMasterData();
                    CommissionMasterBO objCommissionBO = new CommissionMasterBO();
                    
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvCommission.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = false;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objCommissionData.Remarks = txtremarks.Text;
                    }
                    objCommissionData.CommissionID = Convert.ToInt64(ID.Text);
                    objCommissionData.EmployeeID = LogData.UserLoginId;
                    objCommissionData.HospitalID = LogData.HospitalID;

                    int Result = objCommissionBO.DeleteCommissionDetailsByID(objCommissionData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg3.Attributes["class"] = "SucessAlert";
                        divmsg3.Visible = true;
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                divmsg3.Attributes["class"] = "FailAlert";
                divmsg3.Visible = true;
            }
        }
        protected void EditCommssion(Int64 comID)
        {
            try
            {
                List<CommissionMasterData> objCommissionData = GetEditCommissionDetails(comID);
                if (objCommissionData.Count > 0)
                {
                    ddl_servicetype.SelectedValue = objCommissionData[0].Servicetype.ToString();
                    ddl_doctorType.SelectedValue = objCommissionData[0].Doctortype.ToString();
                    txt_service.Text = objCommissionData[0].ServiceName.ToString();
                    txt_doctor.Text = objCommissionData[0].DoctorName.ToString();
                    txt_commission.Text = Commonfunction.Getrounding((Convert.ToDecimal(objCommissionData[0].Commission.ToString())).ToString());
                    txt_tax.Text = Commonfunction.Getrounding((Convert.ToDecimal(objCommissionData[0].Tax.ToString())).ToString());
                    ViewState["ID"] = objCommissionData[0].CommissionID.ToString();
                    lblmessage.Visible = false;
                    ddl_servicetype.Attributes["disabled"] = "disabled";
                    ddl_doctorType.Attributes["disabled"] = "disabled";
                    txt_service.ReadOnly = true;
                    txt_doctor.ReadOnly = true;
                    
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        public List<CommissionMasterData> GetEditCommissionDetails(Int64 ID)
        {
            CommissionMasterData objCommissionData = new CommissionMasterData();
            CommissionMasterBO objCommissionBO = new CommissionMasterBO();
            objCommissionData.CommissionID = ID;
            return objCommissionBO.GetCommissionDeatilbyID(objCommissionData);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            resetdata();
        }
        public void resetdata() {
            ddl_servicetype.SelectedIndex = 0;
            ddl_doctorType.SelectedIndex = 0;
            txt_service.Text = "" ;
            txt_doctor.Text = "";
            txt_commission.Text = "";
            txt_tax.Text = "";
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            GvCommission.DataSource = null;
            GvCommission.DataBind();
            GvCommission.Visible = true;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            divmsg3.Visible = false;
            checkSelect();
            ddl_servicetype.Attributes.Remove("disabled") ;
            ddl_doctorType.Attributes.Remove("disabled");
        }
        
        protected DataTable GetDatafromDatabase()
        {
            List<CommissionMasterData> CommissionDetails = GetCommissionData(0);
            List<CommissionMasterDataToExcel> ListexcelData = new List<CommissionMasterDataToExcel>();
            int i = 0;
            foreach (CommissionMasterData row in CommissionDetails)
            {
                CommissionMasterDataToExcel EcxeclCom = new CommissionMasterDataToExcel();
                EcxeclCom.DoctorName = CommissionDetails[i].DoctorName;
                EcxeclCom.ServicetypeName = CommissionDetails[i].ServicetypeName;
                EcxeclCom.ServiceName = CommissionDetails[i].ServiceName;
                EcxeclCom.ServiceCharge = CommissionDetails[i].ServiceCharge;
                EcxeclCom.Commission = CommissionDetails[i].Commission;
                EcxeclCom.Tax = CommissionDetails[i].Tax;
                EcxeclCom.Hospitalcharge = CommissionDetails[i].Hospitalcharge;

                ListexcelData.Add(EcxeclCom);
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
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DoctorWisePercentage");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DoctorWisePercentage.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvCommission.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvCommission.Columns[11].Visible = false;
                    GvCommission.Columns[12].Visible = false;
                    GvCommission.Columns[13].Visible = false;

                    GvCommission.RenderControl(hw);
                    GvCommission.HeaderRow.Style.Add("width", "15%");
                    GvCommission.HeaderRow.Style.Add("font-size", "10px");
                    GvCommission.Style.Add("text-decoration", "none");
                    GvCommission.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvCommission.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DoctorWisePercentage.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    Messagealert_.ShowMessage(lblresult, "Exported", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
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
            else if (ddlexport.SelectedIndex == 2)
            {
                ExportToPdf();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }

    }
}