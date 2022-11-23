using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedLab;
using Mediqura.CommonData;
using Mediqura.CommonData.Common;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedLab
{
    public partial class EndoscopyReportMaker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();

            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_patient_type, mstlookup.GetLookupsList(LookupName.PatientCatagory));
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(4));
            Commonfunction.PopulateDdl(ddl_header, mstlookup.GetLookupsList(LookupName.HeaderType));
            ddl_header.SelectedIndex = 2;
        
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            reset();
            txt_UHID.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            txt_patientName.Text = "";
            ddl_labsubgroup.SelectedIndex = 0;
            ddl_patient_type.SelectedIndex = 0;

        }
        public void reset()
        {
            txt_selected_ID.Text = "";
            txt_selected_lab_sub.Text = "";
            txt_selected_patientType.Text = "";
            txt_selected_test_ID.Text = "";
            txt_selected_test_type.Text = "";
            txt_selected_UHID.Text = "";

            txt_UHID.Attributes.Remove("disabled");
            txt_patientName.Attributes.Remove("disabled");
            txtDoctor.Attributes.Remove("disabled");
            LiteralsChecked.Text = "";


            LiteralsUnChecked.Text = "";

            txtReport.InnerHtml = "";
            patientList();
            loadpatientListFromDB();

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txt_selected_test_ID.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "SelectPatient", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddl_labsubgroup.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_header.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select header type.", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_header.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                RadiologyReportBO objBO = new RadiologyReportBO();
                RadiologyReportData objdata = new RadiologyReportData();

                objdata.TestID = Convert.ToInt32(txt_selected_test_ID.Text == "" ? null : txt_selected_test_ID.Text.ToString().Trim());
                objdata.LabSubGrpID = Convert.ToInt32(txt_selected_lab_sub.Text == "" ? null : txt_selected_lab_sub.Text.ToString().Trim());
                objdata.UHID = Convert.ToInt32(txt_selected_UHID.Text == "" ? "0" : txt_selected_UHID.Text.ToString().Trim());
                objdata.LabTestID = Convert.ToInt32(txt_selected_ID.Text == "" ? "0" : txt_selected_ID.Text.ToString().Trim());
                objdata.PatientType = Convert.ToInt32(txt_selected_patientType.Text == "" ? "0" : txt_selected_patientType.Text.ToString().Trim());
                objdata.ActionType = Convert.ToInt32(txt_selected_test_type.Text == "" ? "0" : txt_selected_test_type.Text.ToString().Trim());
                objdata.HeaderID = Convert.ToInt32(ddl_header.SelectedValue == "" ? "0" : ddl_header.SelectedValue);
             
                objdata.Template = txtReport.InnerHtml.ToString();
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.FinancialYearID = LogData.FinancialYearID;

                int result = objBO.UpdateRadioReport(objdata);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    reset();

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);

            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_patient_type.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "PatientType", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            patientList();
        }
        public void patientList()
        {
            RadiologyReportBO objBO = new RadiologyReportBO();
            RadiologyReportData objdata = new RadiologyReportData();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objdata.PatientType = Convert.ToInt32(ddl_patient_type.SelectedValue == "0" ? null : ddl_patient_type.SelectedValue);
            objdata.LabSubGrpID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "0" ? null : ddl_labsubgroup.SelectedValue);
            objdata.UHID = Convert.ToInt32(txt_UHID.Text == "" ? "0" : txt_UHID.Text.ToString().Trim());
            objdata.PatientName = txt_patientName.Text == "" ? null : txt_patientName.Text.ToString().Trim();
            objdata.TestStatus = tabradioReport.ActiveTabIndex == 0 ? 0 : tabradioReport.ActiveTabIndex;
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string dateto = To.ToString("yyyy-MM-dd");
            from = Convert.ToDateTime(datefrom + " " + "12:01:00 AM");
            To = Convert.ToDateTime(dateto + " " + "11:59:00 PM");
            objdata.FromDate = from;
            objdata.ToDate = To;
            objdata.LabGrpID = 4;
            StringBuilder UIstring = new StringBuilder();
            List<RadiologyReportData> listdata = objBO.GetPatientList(objdata);
            for (int i = 0; i < listdata.Count; i++)
            {

              
                if (listdata[i].Urgency == 2)
                {
                    string temp = " <li class=\"border-left-yellow\">" +
                                                                                " <a href=\"#\" onclick=\"loadPatient('" + listdata[i].TestID + "', '" + objdata.TestStatus + "', '" + listdata[i].LabSubGrpID + "','" + listdata[i].UHID + "','" + listdata[i].ID + "','" + objdata.PatientType + "')\">" +
                                                                                " <span>" + listdata[i].PatientName + "</span>" +

                                                                                    "  <span class=\"message\">" + listdata[i].TestName + "</span>" +
                                                                                     " <span class=\"message\">" + listdata[i].InvNo + "</span>" +
                                                                                      "   <span class=\"time\">" + listdata[i].TestDate + "</span>" +
                                                                               "  </a>" +
                                                                            "  </li>";

                    UIstring.Append(temp);
                }
                else if (listdata[i].Urgency == 3)
                {
                    string temp = " <li class=\"border-left-red\">" +
                                                                             " <a href=\"#\" onclick=\"loadPatient('" + listdata[i].TestID + "', '" + objdata.TestStatus + "', '" + listdata[i].LabSubGrpID + "','" + listdata[i].UHID + "','" + listdata[i].ID + "','" + objdata.PatientType + "')\">" +
                                                                            " <span>" + listdata[i].PatientName + "</span>" +

                                                                                "  <span class=\"message\">" + listdata[i].TestName + "</span>" +
                                                                                 " <span class=\"message\">" + listdata[i].InvNo + "</span>" +
                                                                                      "   <span class=\"time\">" + listdata[i].TestDate + "</span>" +
                                                                           "  </a>" +
                                                                        "  </li>";

                    UIstring.Append(temp);
                }
                else
                {
                    string temp = " <li class=\"border-left-green\">" +
                                                                               " <a href=\"#\" onclick=\"loadPatient('" + listdata[i].TestID + "', '" + objdata.TestStatus + "', '" + listdata[i].LabSubGrpID + "','" + listdata[i].UHID + "','" + listdata[i].ID + "','" + objdata.PatientType + "')\">" +
                                                                              " <span>" + listdata[i].PatientName + "</span>" +

                                                                                  "  <span class=\"message\">" + listdata[i].TestName + "</span>" +
                                                                                   " <span class=\"message\">" + listdata[i].InvNo + "</span>" +
                                                                                    "   <span class=\"time\">" + listdata[i].TestDate + "</span>" +
                                                                             "  </a>" +
                                                                          "  </li>";

                    UIstring.Append(temp);
                }
            }

            if (objdata.TestStatus == 0)
            {
                LiteralsChecked.Text = "" + UIstring;
            }
            else
            {
                LiteralsUnChecked.Text = "" + UIstring;
            }


        }


        protected void Getpatient_Load(object sender, EventArgs e)
        {
            loadpatientListFromDB();
        }
        public void loadpatientListFromDB()
        {
            RadiologyReportBO objBO = new RadiologyReportBO();
            RadiologyReportData objdata = new RadiologyReportData();
            objdata.TestID = Convert.ToInt32(txt_selected_test_ID.Text == "" ? null : txt_selected_test_ID.Text.ToString().Trim());
            objdata.ActionType = Convert.ToInt32(txt_selected_test_type.Text == "" ? null : txt_selected_test_type.Text.ToString().Trim());
            objdata.LabSubGrpID = Convert.ToInt32(txt_selected_lab_sub.Text == "" ? null : txt_selected_lab_sub.Text.ToString().Trim());
            objdata.UHID = Convert.ToInt32(txt_selected_UHID.Text == "" ? "0" : txt_selected_UHID.Text.ToString().Trim());
            objdata.ID = Convert.ToInt32(txt_selected_ID.Text == "" ? "0" : txt_selected_ID.Text.ToString().Trim());
            objdata.PatientType = Convert.ToInt32(ddl_patient_type.SelectedValue == "0" ? null : ddl_patient_type.SelectedValue);
            objdata.LabTestID = Convert.ToInt32(txt_selected_ID.Text == "" ? "0" : txt_selected_ID.Text.ToString().Trim());
            objdata.TestStatus = Convert.ToInt32(txt_selected_test_type.Text == "" ? "0" : txt_selected_test_type.Text.ToString().Trim());
            objdata.LabGrpID = 4;
            List<RadiologyReportData> objresult = objBO.GetRadioTemplateByID(objdata);
            if (objresult.Count > 0)
            {
                if (objresult[0].Template == null) { txtReport.InnerText = null; }
                else
                {
                    txt_UHID.Text = objresult[0].UHID.ToString();
                    txt_patientName.Text = objresult[0].PatientName.ToString();
                    txtDoctor.Text = objresult[0].ConsultingDcotor.ToString();
                    txt_UHID.Attributes["disabled"] = "disabled";
                    txt_patientName.Attributes["disabled"] = "disabled";
                    txtDoctor.Attributes["disabled"] = "disabled";
                    txtReport.InnerHtml = objresult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                }

            }

        }

        public string generateTemplate(string template, RadiologyReportData objdata)
        {
            DateTime today = System.DateTime.Now;

            string header = "<table style=\"height: 86px;\" border=\"0\">" +
                            "<tbody><tr><td style=\"width: 183.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>UHID:</strong></span></td>" +
                            "<td style=\"width: 433.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.UHID + "</span></td>" +
                            "<td style=\"width: 104.688px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Referred By:</strong></span></td>" +
                            "<td style=\"width: 229.132px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.ConsultingDcotor + "</span></td></tr><tr>" +
                            "<td style=\"width: 183.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Pat. Name:</strong></span></td>" +
                            "<td style=\"width: 433.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.PatientName + "</span></td>" +
                            "<td style=\"width: 104.688px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Visit Type:</strong></span></td>" +
                            "<td style=\"width: 229.132px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.VisitType + "</span></td></tr><tr>" +
                            "<td style=\"width: 183.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Age/sex:</strong></span></td>" +
                            "<td style=\"width: 433.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.PatientAge + "/" + objdata.PatienSex + "</span></td>" +
                            "<td style=\"width: 104.688px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>IP/OP No.:</strong></span></td>" +
                            "<td style=\"width: 229.132px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.IpNo + "</span></td></tr><tr>" +
                            "<td style=\"width: 183.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Pat. Address:</strong></span></td>" +
                            "<td style=\"width: 433.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.PatientAddress + "</span></td>" +
                            "<td style=\"width: 104.688px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Procedure On:</strong></span></td>" +
                            "<td style=\"width: 229.132px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.TestDate + "</span></td></tr><tr>" +
                            "<td style=\"width: 183.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Inv. No:</strong></span></td>" +
                            "<td style=\"width: 433.576px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.InvNo + "</span></td>" +
                            "<td style=\"width: 104.688px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Report On:</strong></span></td>" +
                            "<td style=\"width: 229.132px;\"><span style =\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">[Report-Date]</td>" +
                            "</tr></tbody></table>";

            string code = Commonfunction.getBarcode(objdata.UHID.ToString());
            string barcode = "<img style=\"height:35px;\" src=\"" + code + "\"/>";
            string Result = template.Replace("[header]", header);
            Result = Result.Replace("[barcode]", barcode);
            return Result;
        }
    }
}