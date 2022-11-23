using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
using Mediqura.CommonData.OTData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.OTBO;
using Mediqura.BOL.MedNurseBO;
using Mediqura.CommonData.MedNurseData;

namespace Mediqura.Web.MedNurse
{
    public partial class InvestigationChart : BasePage
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
            Commonfunction.PopulateDdl(ddl_month, mstlookup.GetLookupsList(LookupName.month));
            txt_year.Text = DateTime.Now.Year.ToString();
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            InvestigationChartData objpatdata = new InvestigationChartData();
            InvestigationChartBO objpatBO = new InvestigationChartBO();

            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                string ID = source.Substring(source.LastIndexOf(':') + 1);
                //objpatdata.UHID = Convert.ToInt64(ID == "" ? "0" : ID);
                objpatdata.IPNo = ID;
            }
            else
            {
                txtpatientNames.Text = "";
                return;
            }
            List<InvestigationChartData> objpat = objpatBO.GetInvestPatientDetail(objpatdata);
            if (objpat.Count > 0)
            {
                txtage.Text = objpat[0].Age.ToString();
                txtsex.Text = objpat[0].Sex;
                txtipno.Text = objpat[0].IPNo;
                txtbedroom.Text = objpat[0].WardBedNo.ToString();
                txtdoa.Text = objpat[0].DOA.ToString();
                txtconsultant.Text = objpat[0].Consultant;
                lblUHID.Text = objpat[0].UHID.ToString();
                lblmessage.Visible = false;
            }
            else
            {
                txtage.Text = "";
                txtsex.Text = "";
                txtipno.Text = "";
                txtbedroom.Text = "";
                txtdoa.Text = "";
                txtconsultant.Text = "";
                lblmessage.Visible = true;
                Messagealert_.ShowMessage(lblmessage, "No record found.", 0);
                return;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]

        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            InvestigationChartData Objpaic = new InvestigationChartData();
            InvestigationChartBO objInfoBO = new InvestigationChartBO();
            List<InvestigationChartData> getResult = new List<InvestigationChartData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }

        protected void bindgrid()
        {
            try
            {
                InvestigationChartData objpatdata = new InvestigationChartData();
                InvestigationChartBO objpatBO = new InvestigationChartBO();
                objpatdata.MonthID = Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue);
                objpatdata.Year = Convert.ToInt32(txt_year.Text);
                objpatdata.IPNo = txtipno.Text.ToString();
                objpatdata.UHID = Convert.ToInt64(lblUHID.Text);
                var source = txtpatientNames.Text.ToString();
                //if (source.Contains(":"))
                //{
                //    string ID = source.Substring(source.LastIndexOf(':') + 1);
                //    //objpatdata.UHID = Convert.ToInt64(ID == "" ? "0" : ID);
                //    objpatdata.IPNo = ID;
                //}
                //else
                //{
                //    txtpatientNames.Text = "";
                //    return;
                //}
                List<InvestigationChartData> obj = objpatBO.SearchInvestPatientDetail(objpatdata);
                if (obj.Count > 0)
                {
                    divmsg3.Visible = false;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    GVInvestigation.DataSource = obj;
                    GVInvestigation.DataBind();
                    GVInvestigation.Visible = true;
                }
                else
                {
                    divmsg3.Visible = false;
                    GVInvestigation.DataSource = null;
                    GVInvestigation.DataBind();
                    GVInvestigation.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        protected string CheckIfTitleExists(string strval)
        {
            string title = (string)ViewState["TestName"];
            if (title == strval)
            {
                return string.Empty;
            }
            else
            {
                title = strval;
                ViewState["TestName"] = title;
                return "<br><b>" + title + "</b><br>";
            }
        }
        
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            txtpatientNames.Text = "";
            txtage.Text = "";
            txtsex.Text = "";
            txtbedroom.Text = "";
            txtipno.Text="";
            txtdoa.Text = "";
            txtconsultant.Text = "";
            txtdiagnosis.Text = "";
            ddl_month.SelectedIndex = 0;
            GVInvestigation.DataSource = null;
            GVInvestigation.DataBind();
            GVInvestigation.Visible = false;
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
            if (txtpatientNames.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_month.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Months", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_month.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_year.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Year", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_year.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            } 
            bindgrid();
        }

        private List<InvestigationChartData> GetInvestPatientDetail(int p)
        {
            InvestigationChartData objpat = new InvestigationChartData();
            InvestigationChartBO objBO = new InvestigationChartBO();
            string IPNo;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                int indexStop1 = source.LastIndexOf(')');
                int indexStop2 = source.LastIndexOf(':') + 1;
                int count = indexStop1 - indexStop2;
                IPNo = source.Substring(indexStop2, count);
                objpat.IPNo = IPNo;
            }
            return objBO.GetInvestPatientDetail(objpat);
        }

        protected void GVInvestigation_RowDataBound(object sender, GridViewRowEventArgs e)
		{           
			MasterLookupBO mstlookup = new MasterLookupBO();
          
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Label lblheading = (Label)e.Row.FindControl("lblheading");
                Label lbltest = (Label)e.Row.FindControl("lbltest");
				Label lblyear = (Label)e.Row.FindControl("lblyear");
                Label lblpara = (Label)e.Row.FindControl("lblpara");
				Label lblmonth = (Label)e.Row.FindControl("lblmonth");
                Label lblnoofdays = (Label)e.Row.FindControl("lblnoofdays");
				Label lbldate_1 = (Label)e.Row.FindControl("lbldate_1");
				Label lbldate_2 = (Label)e.Row.FindControl("lbldate_2");
				Label lbldate_3 = (Label)e.Row.FindControl("lbldate_3");
				Label lbldate_4 = (Label)e.Row.FindControl("lbldate_4");
				Label lbldate_5 = (Label)e.Row.FindControl("lbldate_5");
				Label lbldate_6 = (Label)e.Row.FindControl("lbldate_6");
				Label lbldate_7 = (Label)e.Row.FindControl("lbldate_7");
				Label lbldate_8 = (Label)e.Row.FindControl("lbldate_8");
				Label lbldate_9 = (Label)e.Row.FindControl("lbldate_9");
				Label lbldate_10 = (Label)e.Row.FindControl("lbldate_10");
				Label lbldate_11 = (Label)e.Row.FindControl("lbldate_11");
				Label lbldate_12 = (Label)e.Row.FindControl("lbldate_12");
				Label lbldate_13 = (Label)e.Row.FindControl("lbldate_13");
				Label lbldate_14 = (Label)e.Row.FindControl("lbldate_14");
				Label lbldate_15 = (Label)e.Row.FindControl("lbldate_15");
				Label lbldate_16 = (Label)e.Row.FindControl("lbldate_16");
				Label lbldate_17 = (Label)e.Row.FindControl("lbldate_17");
				Label lbldate_18 = (Label)e.Row.FindControl("lbldate_18");
				Label lbldate_19 = (Label)e.Row.FindControl("lbldate_19");
				Label lbldate_20 = (Label)e.Row.FindControl("lbldate_20");
				Label lbldate_21 = (Label)e.Row.FindControl("lbldate_21");
				Label lbldate_22 = (Label)e.Row.FindControl("lbldate_22");
				Label lbldate_23 = (Label)e.Row.FindControl("lbldate_23");
				Label lbldate_24 = (Label)e.Row.FindControl("lbldate_24");
				Label lbldate_25 = (Label)e.Row.FindControl("lbldate_25");
				Label lbldate_26 = (Label)e.Row.FindControl("lbldate_26");
				Label lbldate_27 = (Label)e.Row.FindControl("lbldate_27");
				Label lbldate_28 = (Label)e.Row.FindControl("lbldate_28");
				Label lbldate_29 = (Label)e.Row.FindControl("lbldate_29");
				Label lbldate_30 = (Label)e.Row.FindControl("lbldate_30");
				Label lbldate_31 = (Label)e.Row.FindControl("lbldate_31");

                Label lblcount1 = (Label)e.Row.FindControl("lblcount1");
                Label lblcount2 = (Label)e.Row.FindControl("lblcount2");
                Label lblcount3 = (Label)e.Row.FindControl("lblcount3");
                Label lblcount4 = (Label)e.Row.FindControl("lblcount4");
                Label lblcount5 = (Label)e.Row.FindControl("lblcount5");
                Label lblcount6 = (Label)e.Row.FindControl("lblcount6");
                Label lblcount7 = (Label)e.Row.FindControl("lblcount7");
                Label lblcount8 = (Label)e.Row.FindControl("lblcount8");
                Label lblcount9 = (Label)e.Row.FindControl("lblcount9");
                Label lblcount10 = (Label)e.Row.FindControl("lblcount10");
                Label lblcount11 = (Label)e.Row.FindControl("lblcount11");
                Label lblcount12 = (Label)e.Row.FindControl("lblcount12");
                Label lblcount13 = (Label)e.Row.FindControl("lblcount13");
                Label lblcount14 = (Label)e.Row.FindControl("lblcount14");
                Label lblcount15 = (Label)e.Row.FindControl("lblcount15");
                Label lblcount16 = (Label)e.Row.FindControl("lblcount16");
                Label lblcount17 = (Label)e.Row.FindControl("lblcount17");
                Label lblcount18 = (Label)e.Row.FindControl("lblcount18");
                Label lblcount19 = (Label)e.Row.FindControl("lblcount19");
                Label lblcount20 = (Label)e.Row.FindControl("lblcount20");
                Label lblcount21 = (Label)e.Row.FindControl("lblcount21");
                Label lblcount22 = (Label)e.Row.FindControl("lblcount22");
                Label lblcount23 = (Label)e.Row.FindControl("lblcount23");
                Label lblcount24 = (Label)e.Row.FindControl("lblcount24");
                Label lblcount25 = (Label)e.Row.FindControl("lblcount25");
                Label lblcount26 = (Label)e.Row.FindControl("lblcount26");
                Label lblcount27 = (Label)e.Row.FindControl("lblcount27");
                Label lblcount28 = (Label)e.Row.FindControl("lblcount28");
                Label lblcount29 = (Label)e.Row.FindControl("lblcount29");
                Label lblcount30 = (Label)e.Row.FindControl("lblcount30");
                Label lblcount31 = (Label)e.Row.FindControl("lblcount31");

				if (lblheading.Text == "1")
				{
                    lbltest.Font.Bold = true;
                    lbltest.Attributes.Add("Style", "text-align:center;");
					
					lbldate_1.Visible=true;
					lbldate_1.Font.Bold = true;
					lbldate_1.Attributes.Add("Style", "text-align:center;");

					lbldate_2.Visible = true;
					lbldate_2.Font.Bold = true;
					lbldate_2.Attributes.Add("Style", "text-align:center;");

					lbldate_3.Visible = true;
					lbldate_3.Font.Bold = true;
					lbldate_3.Attributes.Add("Style", "text-align:center;");

					lbldate_4.Visible = true;
					lbldate_4.Font.Bold = true;
					lbldate_4.Attributes.Add("Style", "text-align:center;");

					lbldate_5.Visible = true;
					lbldate_5.Font.Bold = true;
					lbldate_5.Attributes.Add("Style", "text-align:center;");

					lbldate_6.Visible = true;
					lbldate_6.Font.Bold = true;
					lbldate_6.Attributes.Add("Style", "text-align:center;");

					lbldate_7.Visible = true;
					lbldate_7.Font.Bold = true;
					lbldate_7.Attributes.Add("Style", "text-align:center;");

					lbldate_8.Visible = true;
					lbldate_8.Font.Bold = true;
					lbldate_8.Attributes.Add("Style", "text-align:center;");

					lbldate_9.Visible = true;
					lbldate_9.Font.Bold = true;
					lbldate_9.Attributes.Add("Style", "text-align:center;");

					lbldate_10.Visible = true;
					lbldate_10.Font.Bold = true;
					lbldate_10.Attributes.Add("Style", "text-align:center;");

					lbldate_11.Visible = true;
					lbldate_11.Font.Bold = true;
					lbldate_11.Attributes.Add("Style", "text-align:center;");

					lbldate_12.Visible = true;
					lbldate_12.Font.Bold = true;
					lbldate_12.Attributes.Add("Style", "text-align:center;");

					lbldate_13.Visible = true;
					lbldate_13.Font.Bold = true;
					lbldate_13.Attributes.Add("Style", "text-align:center;");

					lbldate_14.Visible = true;
					lbldate_14.Font.Bold = true;
					lbldate_14.Attributes.Add("Style", "text-align:center;");

					lbldate_15.Visible = true;
					lbldate_15.Font.Bold = true;
					lbldate_15.Attributes.Add("Style", "text-align:center;");

					lbldate_16.Visible = true;
					lbldate_16.Font.Bold = true;
					lbldate_16.Attributes.Add("Style", "text-align:center;");

					lbldate_17.Visible = true;
					lbldate_17.Font.Bold = true;
					lbldate_17.Attributes.Add("Style", "text-align:center;");

					lbldate_18.Visible = true;
					lbldate_18.Font.Bold = true;
					lbldate_18.Attributes.Add("Style", "text-align:center;");

					lbldate_19.Visible = true;
					lbldate_19.Font.Bold = true;
					lbldate_19.Attributes.Add("Style", "text-align:center;");

					lbldate_20.Visible = true;
					lbldate_20.Font.Bold = true;
					lbldate_20.Attributes.Add("Style", "text-align:center;");

					lbldate_21.Visible = true;
					lbldate_21.Font.Bold = true;
					lbldate_21.Attributes.Add("Style", "text-align:center;");

					lbldate_22.Visible = true;
					lbldate_22.Font.Bold = true;
					lbldate_22.Attributes.Add("Style", "text-align:center;");

					lbldate_23.Visible = true;
					lbldate_23.Font.Bold = true;
					lbldate_23.Attributes.Add("Style", "text-align:center;");

					lbldate_24.Visible = true;
					lbldate_24.Font.Bold = true;
					lbldate_24.Attributes.Add("Style", "text-align:center;");

					lbldate_25.Visible = true;
					lbldate_25.Font.Bold = true;
					lbldate_25.Attributes.Add("Style", "text-align:center;");

					lbldate_26.Visible = true;
					lbldate_26.Font.Bold = true;
					lbldate_26.Attributes.Add("Style", "text-align:center;");

					lbldate_27.Visible = true;
					lbldate_27.Font.Bold = true;
					lbldate_27.Attributes.Add("Style", "text-align:center;");

					lbldate_28.Visible = true;
					lbldate_28.Font.Bold = true;
					lbldate_28.Attributes.Add("Style", "text-align:center;");

					lbldate_29.Visible = true;
					lbldate_29.Font.Bold = true;
					lbldate_29.Attributes.Add("Style", "text-align:center;");

					lbldate_30.Visible = true;
					lbldate_30.Font.Bold = true;
					lbldate_30.Attributes.Add("Style", "text-align:center;");

					lbldate_31.Visible = true;
					lbldate_31.Font.Bold = true;
					lbldate_31.Attributes.Add("Style", "text-align:center;");
				}


                if (lblnoofdays.Text == "31")
                {
                    GVInvestigation.Columns[30].Visible = true;
                    GVInvestigation.Columns[31].Visible = true;
                    GVInvestigation.Columns[32].Visible = true;
                    GVInvestigation.Columns[33].Visible = true;
                }

                else if (lblnoofdays.Text == "30")
                {
                    GVInvestigation.Columns[30].Visible = true;
                    GVInvestigation.Columns[31].Visible = true;
                    GVInvestigation.Columns[32].Visible = true;
                    GVInvestigation.Columns[33].Visible = false;
                }

                else if (lblnoofdays.Text == "29")
                {
                    GVInvestigation.Columns[30].Visible = true;
                    GVInvestigation.Columns[31].Visible = true;
                    GVInvestigation.Columns[32].Visible = false;
                    GVInvestigation.Columns[33].Visible = false;
                }
                else if (lblnoofdays.Text == "28")
                {
                    GVInvestigation.Columns[30].Visible = true;
                    GVInvestigation.Columns[31].Visible = false;
                    GVInvestigation.Columns[32].Visible = false;
                    GVInvestigation.Columns[33].Visible = false;
                }		

                if (lblcount1.Text == "0")
                {
                    GVInvestigation.Columns[3].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[3].Visible = true;
                }
                if (lblcount2.Text == "0")
                {
                    GVInvestigation.Columns[4].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[4].Visible = true;
                }
                if (lblcount3.Text == "0")
                {
                    GVInvestigation.Columns[5].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[5].Visible = true;
                }
                if (lblcount4.Text == "0")
                {
                    GVInvestigation.Columns[6].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[6].Visible = true;
                }

                if (lblcount5.Text == "0")
                {
                    GVInvestigation.Columns[7].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[7].Visible = true;
                }
                if (lblcount6.Text == "0")
                {
                    GVInvestigation.Columns[8].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[8].Visible = true;
                }
                if (lblcount7.Text == "0")
                {
                    GVInvestigation.Columns[9].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[9].Visible = true;
                }
                if (lblcount8.Text == "0")
                {
                    GVInvestigation.Columns[10].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[10].Visible = true;
                }
                if (lblcount9.Text == "0")
                {
                    GVInvestigation.Columns[11].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[11].Visible = true;
                }
                if (lblcount10.Text == "0")
                {
                    GVInvestigation.Columns[12].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[12].Visible = true;
                }
                if (lblcount11.Text == "0")
                {
                    GVInvestigation.Columns[13].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[13].Visible = true;
                }
                if (lblcount12.Text == "0")
                {
                    GVInvestigation.Columns[14].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[14].Visible = true;
                }
                if (lblcount13.Text == "0")
                {
                    GVInvestigation.Columns[15].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[15].Visible = true;
                }
                if (lblcount14.Text == "0")
                {
                    GVInvestigation.Columns[16].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[16].Visible = true;
                }
                if (lblcount15.Text == "0")
                {
                    GVInvestigation.Columns[17].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[17].Visible = true;
                }
                if (lblcount16.Text == "0")
                {
                    GVInvestigation.Columns[18].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[18].Visible = true;
                }
                if (lblcount17.Text == "0")
                {
                    GVInvestigation.Columns[19].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[19].Visible = true;
                }
                if (lblcount18.Text == "0")
                {
                    GVInvestigation.Columns[20].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[20].Visible = true;
                }
                if (lblcount19.Text == "0")
                {
                    GVInvestigation.Columns[21].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[21].Visible = true;
                }
                if (lblcount20.Text == "0")
                {
                    GVInvestigation.Columns[22].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[22].Visible = true;
                }
                if (lblcount21.Text == "0")
                {
                    GVInvestigation.Columns[23].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[23].Visible = true;
                }
                if (lblcount22.Text == "0")
                {
                    GVInvestigation.Columns[24].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[24].Visible = true;
                }
                if (lblcount23.Text == "0")
                {
                    GVInvestigation.Columns[25].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[25].Visible = true;
                }
                if (lblcount24.Text == "0")
                {
                    GVInvestigation.Columns[26].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[26].Visible = true;
                }
                if (lblcount25.Text == "0")
                {
                    GVInvestigation.Columns[27].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[27].Visible = true;
                }
                if (lblcount26.Text == "0")
                {
                    GVInvestigation.Columns[28].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[28].Visible = true;
                }
                if (lblcount27.Text == "0")
                {
                    GVInvestigation.Columns[29].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[29].Visible = true;
                }
                if (lblcount28.Text == "0")
                {
                    GVInvestigation.Columns[30].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[30].Visible = true;
                }
                if (lblcount29.Text == "0")
                {
                    GVInvestigation.Columns[31].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[31].Visible = true;
                }
                if (lblcount30.Text == "0")
                {
                    GVInvestigation.Columns[32].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[32].Visible = true;
                }
                if (lblcount31.Text == "0")
                {
                    GVInvestigation.Columns[33].Visible = false;
                }
                else
                {
                    GVInvestigation.Columns[33].Visible = true;
                }
                			
			}
        
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            InvestigationChartData objData = new InvestigationChartData();
            InvestigationChartBO objBO = new InvestigationChartBO();
            
            Int32 year = Convert.ToInt32(txt_year.Text);
            Int32 month = Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue);
            string IPNo = txtipno.Text == "" ? "" : txtipno.Text.Trim();
            string url = "../MedNurse/Reports/ReportViewer.aspx?option=InvestigationChart&year=" + year.ToString() + "&month=" + month.ToString() + "&Ipno=" + IPNo;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}