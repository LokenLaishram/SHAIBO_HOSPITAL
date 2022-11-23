using Mediqura.BOL.MedLab;
using Mediqura.BOL.MedLabBO;
using Mediqura.CommonData;
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
    public partial class LabReportCollectionSample : System.Web.UI.Page
    {
        public string GroupID;
        public string HeaderID;
        protected void Page_Load(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            String print = Request.QueryString["p"];
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
                        string barcode = "<img style=\"height:35px;\" src=\"" + code + "\"/>";


                        string qrString = "<UHID>" + GetResult[0].UHID.ToString() + "</UHID>"
                                           + "<INV> " + GetResult[0].InVnumber.ToString() + "</INV>"
                                           + "<NAME> " + GetResult[0].PatientName.ToString() + "</NAME>";

                        string QR = Commonfunction.getQR(qrString);
                        string QRCODE = "<img style=\"height:50px;\" src=\"" + QR + "\"/>";

                        Result = Result.Replace("[barcode]", barcode);
                        Result = Result.Replace("[qr]", QRCODE);
                        if (objdata1.HeaderID != 0)
                        {
                            ltReport.Text = Result.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&").Replace("[Report-Date]", GetResult[0].ReportDate.ToString()) + GetResult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&").Replace("[sign]", GetResult[0].Designation);
                        }
                        else
                        {
                            ltReport.Text = GetResult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&").Replace("[sign]", GetResult[0].Designation.Replace("[Report-Date]", GetResult[0].ReportDate.ToString()));

                        }
                    }
                }
            }
        }


        public string generateTemplate(string template, RadioLabReportVerificationData objdata)
        {
            string Result = template.Replace("[Report-Date]", objdata.ReportDate.ToString()).Replace("[sign]", objdata.Designation.ToString());
            return Result;
        }
    }
}