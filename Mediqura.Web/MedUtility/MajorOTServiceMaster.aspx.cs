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
    public partial class MajorOTServiceMaster : BasePage
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
            Commonfunction.PopulateDdl(ddl_group, mstlookup.GetLookupsList(LookupName.MOTOTHGroups));
            Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetSubServiceTypeByGroupID(Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue)));
            Commonfunction.Insertzeroitemindex(ddl_service);
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
        protected void ddl_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_group.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetSubServiceTypeByGroupID(Convert.ToInt32(ddl_group.SelectedValue)));
                Commonfunction.Insertzeroitemindex(ddl_service);
            }
        }
        protected void ddl_subgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_subgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_service, mstlookup.GetServiceByID(Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue), Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue)));
                bindgrid();
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_service);
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
            GvMajorOT.DataSource = null;
            GvMajorOT.DataBind();
            GvMajorOT.Visible = false;
            divmsg1.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
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
                List<MinorOTServicesPayOutData> lstemp = GetMajorOTServicesPayOutDetails(0);
                if (lstemp.Count > 0)
                {
                    GvMajorOT.DataSource = lstemp;
                    GvMajorOT.DataBind();
                    GvMajorOT.Visible = true;
                    btnexport.Visible = true;
                    ddlexport.Visible = true;
                }
                else
                {
                    GvMajorOT.DataSource = null;
                    GvMajorOT.DataBind();
                    GvMajorOT.Visible = true;
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
        private List<MinorOTServicesPayOutData> GetMajorOTServicesPayOutDetails(int p)
        {
            List<MinorOTServicesPayOutData> List = new List<MinorOTServicesPayOutData>();
            MinorOTServicesPayOutBO objBO = new MinorOTServicesPayOutBO();
            MinorOTServicesPayOutData objData = new MinorOTServicesPayOutData();
            objData.GroupID = Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue);
            objData.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
            objData.ServiceID = Convert.ToInt32(ddl_service.SelectedValue == "" ? "0" : ddl_service.SelectedValue);
            objData.EmployeeID = LogData.EmployeeID;
            objData.FinancialYearID = LogData.FinancialYearID;
            objData.HospitalID = LogData.HospitalID;
            return objBO.SearchMajorOTPayOutDetails(objData);
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
                List<MinorOTServicesPayOutData> List = new List<MinorOTServicesPayOutData>();
                MinorOTServicesPayOutBO objBO = new MinorOTServicesPayOutBO();
                MinorOTServicesPayOutData objData = new MinorOTServicesPayOutData();
                int count = 0;

                foreach (GridViewRow row in GvMajorOT.Rows)
                {
                    Label ID = (Label)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label code = (Label)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("lbl_code");
                    Label serviceid = (Label)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("lbl_serviceID");
                    TextBox rate = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txtrate");
                    DropDownList sharetype = (DropDownList)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("ddl_sharetype");

                    TextBox conshare = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_consultshare");
                    TextBox stafffund = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_stafffund");
                    TextBox corpusfund = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_corpusfund");
                    TextBox surgeon1 = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_surgeon1");
                    TextBox surgeon2 = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_surgeon2");
                    TextBox surgeon3 = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_surgeon3");
                    TextBox anaesthesia = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_anesthesia");
                    TextBox consumable = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_consumable");
                    TextBox instrumentused = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_instrumentused");
                    TextBox consultshare = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_consultshare");
                    TextBox assistshare = (TextBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("txt_assistshare");

                    CheckBox chk = (CheckBox)GvMajorOT.Rows[row.RowIndex].Cells[0].FindControl("chekbox");

                    MinorOTServicesPayOutData obj1 = new MinorOTServicesPayOutData();
                    if (chk.Checked == true)
                    {
                        obj1.ID = 0;
                        obj1.ServiceCode = "0";
                        obj1.ServiceID = Convert.ToInt32(serviceid.Text == "" ? "0" : serviceid.Text);
                        obj1.GroupID = Convert.ToInt32(ddl_group.Text == "" ? "0" : ddl_group.Text);
                        obj1.SubGroupID = Convert.ToInt32(ddl_subgroup.Text == "" ? "0" : ddl_subgroup.Text);
                        obj1.ServiceCharge = Convert.ToDecimal(rate.Text == "" ? "0" : rate.Text);
                        obj1.ShareType = Convert.ToInt32(sharetype.SelectedValue == "" ? "0" : sharetype.SelectedValue);

                        if (sharetype.SelectedIndex == 1)
                        {
                            if ((Convert.ToDouble(stafffund.Text == "" ? "0.0" : stafffund.Text)
                                + Convert.ToDouble(corpusfund.Text == "" ? "0.0" : corpusfund.Text)
                                + Convert.ToDouble(surgeon1.Text == "" ? "0.0" : surgeon1.Text)
                                + Convert.ToDouble(surgeon2.Text == "" ? "0.0" : surgeon2.Text)
                                + Convert.ToDouble(surgeon3.Text == "" ? "0.0" : surgeon3.Text)
                                + Convert.ToDouble(anaesthesia.Text == "" ? "0.0" : anaesthesia.Text)
                                + Convert.ToDouble(consumable.Text == "" ? "0.0" : consumable.Text)
                                + Convert.ToDouble(instrumentused.Text == "" ? "0.0" : instrumentused.Text)) == 100)
                            {
                                obj1.ConsultantShare = Convert.ToDecimal(conshare.Text);
                                obj1.StaffFund = Convert.ToDecimal(stafffund.Text);
                                obj1.CorpusFund = Convert.ToDecimal(corpusfund.Text);
                                obj1.Surgeon1 = Convert.ToDecimal(surgeon1.Text);
                                obj1.Surgeon2 = Convert.ToDecimal(surgeon2.Text);
                                obj1.Surgeon3 = Convert.ToDecimal(surgeon3.Text);
                                obj1.AnaesthesiaShare = Convert.ToDecimal(anaesthesia.Text);
                                obj1.OTConsumableShare = Convert.ToDecimal(consumable.Text);
                                obj1.InstrumentUsedShare = Convert.ToDecimal(instrumentused.Text);
                                obj1.AssistantShare = Convert.ToDecimal(assistshare.Text);
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
                            if ((Convert.ToDouble(stafffund.Text == "" ? "0.0" : stafffund.Text)
                                + Convert.ToDouble(corpusfund.Text == "" ? "0.0" : corpusfund.Text)
                                + Convert.ToDouble(surgeon1.Text == "" ? "0.0" : surgeon1.Text)
                                + Convert.ToDouble(surgeon2.Text == "" ? "0.0" : surgeon2.Text)
                                + Convert.ToDouble(surgeon3.Text == "" ? "0.0" : surgeon3.Text)
                                + Convert.ToDouble(anaesthesia.Text == "" ? "0.0" : anaesthesia.Text)
                                + Convert.ToDouble(consumable.Text == "" ? "0.0" : consumable.Text)
                                + Convert.ToDouble(instrumentused.Text == "" ? "0.0" : instrumentused.Text)) == (Convert.ToDouble(rate.Text == "" ? "0" : rate.Text)))
                            {
                                obj1.ConsultantShare = Convert.ToDecimal(conshare.Text);
                                obj1.StaffFund = Convert.ToDecimal(stafffund.Text);
                                obj1.CorpusFund = Convert.ToDecimal(corpusfund.Text);
                                obj1.Surgeon1 = Convert.ToDecimal(surgeon1.Text);
                                obj1.Surgeon2 = Convert.ToDecimal(surgeon2.Text);
                                obj1.Surgeon3 = Convert.ToDecimal(surgeon3.Text);
                                obj1.AnaesthesiaShare = Convert.ToDecimal(anaesthesia.Text);
                                obj1.OTConsumableShare = Convert.ToDecimal(consumable.Text);
                                obj1.InstrumentUsedShare = Convert.ToDecimal(instrumentused.Text);
                                obj1.AssistantShare = Convert.ToDecimal(assistshare.Text);
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
                        count = count + 1;
                        List.Add(obj1);
                    }

                }

                objData.XMLData = XmlConvertor.MajorOTRecordDatatoXML(List).ToString();
                objData.BedClassID = Convert.ToInt32(ddl_BedClass.SelectedValue == "" ? "0" : ddl_BedClass.SelectedValue);

                objData.EmployeeID = LogData.EmployeeID;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.IPaddress = LogData.IPaddress;
                objData.HospitalID = LogData.HospitalID;

                objData.ActionType = Enumaction.Insert;

                int result = objBO.UpdateMajorOTServicesMST(objData);
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
                Response.AddHeader("content-disposition", "attachment;filename=MajorOTPayOutDetails.xlsx");
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
            List<MinorOTServicesPayOutData> OPDEmergencyDetails = GetMajorOTServicesPayOutDetails(0);
            List<MajorOTServicesPayOutDatatoExcel> ListexcelData = new List<MajorOTServicesPayOutDatatoExcel>();
            int i = 0;
            foreach (MinorOTServicesPayOutData row in OPDEmergencyDetails)
            {
                MajorOTServicesPayOutDatatoExcel ExcelSevice = new MajorOTServicesPayOutDatatoExcel();
                ExcelSevice.ServiceCode = Commonfunction.Getrounding(OPDEmergencyDetails[i].ServiceCode.ToString());
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
                ExcelSevice.ConsultantShare = Commonfunction.Getrounding(OPDEmergencyDetails[i].ConsultantShare.ToString());
                ExcelSevice.CorpusFund = Commonfunction.Getrounding(OPDEmergencyDetails[i].CorpusFund.ToString());
                ExcelSevice.StaffFund = Commonfunction.Getrounding(OPDEmergencyDetails[i].StaffFund.ToString());
                ExcelSevice.Surgeon1 = Commonfunction.Getrounding(OPDEmergencyDetails[i].Surgeon1.ToString());
                ExcelSevice.Surgeon2 = Commonfunction.Getrounding(OPDEmergencyDetails[i].Surgeon2.ToString());
                ExcelSevice.Surgeon3 = Commonfunction.Getrounding(OPDEmergencyDetails[i].Surgeon3.ToString());
                ExcelSevice.AnaesthesiaShare = Commonfunction.Getrounding(OPDEmergencyDetails[i].AnaesthesiaShare.ToString());
                ExcelSevice.OTConsumableShare = Commonfunction.Getrounding(OPDEmergencyDetails[i].OTConsumableShare.ToString());
                ExcelSevice.InstrumentUsedShare = Commonfunction.Getrounding(OPDEmergencyDetails[i].InstrumentUsedShare.ToString());
                ExcelSevice.AssistantShare = Commonfunction.Getrounding(OPDEmergencyDetails[i].AssistantShare.ToString());
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
