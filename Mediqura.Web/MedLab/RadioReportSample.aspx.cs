using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedLab;
using Mediqura.BOL.MedLabBO;
using Mediqura.CommonData;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedLab;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedLab
{
    public partial class RadioReportSample : System.Web.UI.Page
    {

        public string id;
        public string billID;
        public string GroupID;
        public string HeaderID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddlverifyby, mstlookup.GetLookupsList(LookupName.Radiologist));

                msgDiv.Visible = false;
            }
            id = Request.QueryString["id"];
            billID = Request.QueryString["billID"];
            GroupID = Request.QueryString["GroupID"];
            HeaderID = Request.QueryString["HeaderID"];
            RadioLabReportVerificationData objData = new RadioLabReportVerificationData();
            RadioLabReportVerificationBO objBO = new RadioLabReportVerificationBO();
            objData.LabID = Convert.ToInt64(id);


            List<RadioLabReportVerificationData> GetResult = objBO.GetRadioTemplateByID(objData);
            if (GetResult.Count > 0)
            {
                RadiologyReportBO objBO1 = new RadiologyReportBO();
                RadiologyReportData objdata1 = new RadiologyReportData();
                objdata1.LabGrpID = Convert.ToInt32(GroupID);
                objdata1.HeaderID = Convert.ToInt32(HeaderID);
                List<RadiologyReportData> objresult = objBO1.GetHeaderTemplateByID(objdata1);
                if (objresult.Count > 0)
                {
                    if (objresult[0].Template != null)
                    {
                        string Result = objresult[0].Template.Replace("[UHID]", GetResult[0].UHID.ToString()).Replace("[PatientName]", GetResult[0].PatientName.ToString()).Replace("[Age]", GetResult[0].PatientAge.ToString()).Replace("[PatientAddress]", GetResult[0].PatientAddress.ToString()).Replace("[TestDate]", GetResult[0].TestOn.ToString()).Replace("[InvNo]", GetResult[0].InVnumber.ToString()).Replace("[Gender]", GetResult[0].PatienSex.ToString()).Replace("[ReferalDoctor]", GetResult[0].ConsultingDcotor.ToString()).Replace("[Visit-Type]", GetResult[0].VisitType.ToString()).Replace("[IPNo]", GetResult[0].IpNo.ToString());
                        string code = Commonfunction.getBarcode(objdata1.UHID.ToString());
                        string barcode = "<img style=\"height:30px;\" src=\"" + code + "\"/>";
                        string qrString = "<UHID>" + GetResult[0].UHID.ToString() + "</UHID>"
                                           + "<INV> " + GetResult[0].InVnumber.ToString() + "</INV>"
                                           + "<NAME> " + GetResult[0].PatientName.ToString() + "</NAME>";
                        string QR = Commonfunction.getQR(qrString);
                        string QRCODE = "<img style=\"height:50px;\" src=\"" + QR + "\"/>";

                        Result = Result.Replace("[barcode]", barcode);
                        Result = Result.Replace("[qr]", QRCODE);

                        if (GetResult[0].isVerified == 1)
                        {
                            btn_verify.Visible = false;
                            ddlverifyby.Visible = false;
                            if (objdata1.HeaderID != 0)
                            {
                                ltReport.Text = Result.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&").Replace("[Report-Date]", GetResult[0].ReportDate.ToString()) + GetResult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&").Replace("[sign]", GetResult[0].Designation);
                            }
                            else
                            {
                                ltReport.Text = GetResult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&").Replace("[sign]", GetResult[0].Designation).Replace("[Report-Date]", GetResult[0].ReportDate.ToString());
                            }
                        }
                        else
                        {
                            btn_verify.Visible = true;
                            ddlverifyby.Visible = true;
                            if (objdata1.HeaderID != 0)
                            {
                                ltReport.Text = Result.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&") + GetResult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&").Replace("[sign]", GetResult[0].Designation);
                            }
                            else
                            {
                                ltReport.Text = GetResult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&").Replace("[sign]", GetResult[0].Designation).Replace("[Report-Date]", GetResult[0].ReportDate.ToString());

                            }
                        }

                    }
                }

            }

        }
        protected void btn_verify_Click(object sender, EventArgs e)
        {
            RadioLabReportVerificationData objData = new RadioLabReportVerificationData();
            RadioLabReportVerificationBO objBO = new RadioLabReportVerificationBO();

            objData.ID = Convert.ToInt32(id);
            objData.billID = Convert.ToInt64(billID);
            objData.VerifyBy = Convert.ToInt64(ddlverifyby.SelectedValue == "" ? "0" : ddlverifyby.SelectedValue);
            int result = objBO.UpdateRadioReportVerification(objData);
            if (result == 1 || result == 2)
            {
                Messagealert_.ShowMessage(lblMessage, result == 1 ? "save" : "update", 1);
                msgDiv.Visible = true;
                btn_verify.Visible = false;
                ddlverifyby.Visible = false;


            }
        }
    }
}