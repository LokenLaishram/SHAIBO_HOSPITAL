
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using Mediqura.BOL.AdmissionBO;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.AdmissionData;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Reflection;




namespace Mediqura.Web.MedIPD
{
    public partial class DischargeReport :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                btnsave.Attributes["disabled"] = "disabled";
            }

        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_DisType, mstlookup.GetLookupsList(LookupName.DisType));
            Commonfunction.PopulateDdl(ddl_DisTypeList, mstlookup.GetLookupsList(LookupName.DisType));
            
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            DischargeData objpat = new DischargeData();
            DischargeBO objBO = new DischargeBO();
            List<DischargeData> getResult = new List<DischargeData>();
            objpat.IPNo = prefixText;
            getResult = objBO.getIPNoDishList(objpat);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNoDischarge(string prefixText, int count, string contextKey)
        {
            DischargeData objpat = new DischargeData();
            DischargeBO objBO = new DischargeBO();
            List<DischargeData> getResult = new List<DischargeData>();
            objpat.IPNo = prefixText;
            getResult = objBO.getIPNoDish(objpat);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            bindList();
        }
      
        public void bindList()
        {
            try
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
				if (txtIPNo.Text == "")
				{
					if (txtdatefrom.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage, "DateRange", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";
						txtdatefrom.Focus();
						return;
					}
					else
					{
						lblmessage.Visible = false;
					}
					if (txtto.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage, "DateRange", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";
						txtto.Focus();
						return;
					}
					else
					{
						lblmessage.Visible = false;
					}
					if (txtdatefrom.Text != "")
					{
						if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
						{
							Messagealert_.ShowMessage(lblmessage, "ValidDatefrom", 0);
							div1.Attributes["class"] = "FailAlert";
							div1.Visible = true;
							txtdatefrom.Focus();
							return;
						}
					}
					else
					{
						lblmessage.Visible = false;
					}
					if (txtto.Text != "")
					{
						if (Commonfunction.isValidDate(txtto.Text) == false)
						{
							Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
							div1.Attributes["class"] = "FailAlert";
							div1.Visible = true;
							txtto.Focus();
							return;
						}
					}
					else
					{
						lblmessage.Visible = false;
					}
				}
				else
				{
					lblmessage.Visible = false;
				}
                 List<DischargeData> objdischarge = GetFnalBillList(0);
                 if (objdischarge.Count > 0)
                 {
                     gvrecord.DataSource = objdischarge;
                     gvrecord.DataBind();
                     gvrecord.Visible = true;
                    
                 }
                 else
                 {
                     gvrecord.DataSource = null;
                     gvrecord.DataBind();
                     gvrecord.Visible = true;
                   
                 }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        public List<DischargeData> GetFnalBillList(int curIndex)
        {

            DischargeData objpat = new DischargeData();
            DischargeBO objBO = new DischargeBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.IPNo = (txtIPNo.Text == "" ? null : txtIPNo.Text.ToString().Trim());
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objBO.GetFnalBillList(objpat);
        }
        public List<DischargeData> GetSummaryList(int curIndex)
        {

            DischargeData objpat = new DischargeData();
            DischargeBO objBO = new DischargeBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefromList.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefromList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txttoList.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txttoList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.DischargeTypeID = Convert.ToInt32(ddl_DisTypeList.SelectedValue == "0" ? null : ddl_DisTypeList.SelectedValue);
            objpat.IPNo = (txt_IPNo.Text == "" ? null : txt_IPNo.Text.ToString().Trim());
            return objBO.GetSummaryList(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtto.Text = "";
            txtdatefrom.Text = "";
            ddl_DisType.SelectedIndex = 0;
            txtReport.InnerHtml = "";
            txtIPNo.Text = "";
            btnPrint.Visible = false;
            txtdatefrom.Text = "";
            txtto.Text ="";
            gvrecord.DataSource = null;
            gvrecord.DataBind();
            gvrecord.Visible = false;
            div1.Visible = false;
            lblmessage.Visible = false;
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
            if (ddl_DisType.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "DischargeType", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";

                ddl_DisType.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            DischargeBO objBO = new DischargeBO();
            DischargeData objdata = new DischargeData();
            objdata.DischargeTypeID = Convert.ToInt32(ddl_DisType.SelectedValue == "0" ? null : ddl_DisType.SelectedValue);
            objdata.IPNo = (lblSelectedIpNo.Text == "" ? null : lblSelectedIpNo.Text.ToString().Trim()); objdata.Template = txtReport.InnerHtml.ToString();
            objdata.EmployeeID = LogData.EmployeeID;
            objdata.HospitalID = LogData.HospitalID;
            objdata.FinancialYearID = LogData.FinancialYearID;
            int result = objBO.UpdateSummaryReport(objdata);
            if (result == 1 || result == 2)
            {
                Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                div1.Visible = true;
                div1.Attributes["class"] = "SucessAlert";
                btnPrint.Visible = true;

            }
            else
            {
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                    btnPrint.Visible = false;
                }

            }
          catch (Exception ex) //Exception in agent layer itself
          {
              PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
              LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
              Messagealert_.ShowMessage(lblmessage, "system", 0);

          }


        }
    
        public string generateTemplate(string template, DischargeData objdata)
        {
            DateTime today = System.DateTime.Now;
            string header = "<table style=\"height: 146px;\" width=\"100%\"><tbody><tr>"
                                +"<td style=\"width: 12%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Patient's Name :</strong></span></td>"
                                + "<td style=\"width: 41.0085%;\" colspan=\"3\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;"+objdata.PatientName+"</span></td>"
                                + "<td style=\"width: 16.9915%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Age/Sex :<br /></strong></span></td>"
                                + "<td style=\"width: 15%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.Agecount + "/"+objdata.GenderName+"</span></td>"
                                + "</tr><tr>"
                                + "<td style=\"width: 12%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Address :</strong></span></td>"
                                + "<td style=\"width: 41.0085%;\" colspan=\"3\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.PatientAddress + "</span></td>"
                                + "<td style=\"width: 16.9915%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>District :<br /></strong></span></td>"
                                + "<td style=\"width: 15%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.District + "</span></td>"
                                + "</tr><tr>"
                                + "<td style=\"width: 12%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>State :</strong></span></td>"
                                + "<td style=\"width: 16%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.State + "</span></td>"
                                + "<td style=\"width: 14%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Pin Code :<br /></strong></span></td>"
                                + "<td style=\"width: 11.0085%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.Pincode + "</span></td>"
                                + "<td style=\"width: 16.9915%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Contact No. :<br /></strong></span></td>"
                                + "<td style=\"width: 15%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.Contact + "</span></td>"
                                + "</tr><tr>"
                                + "<td style=\"width: 12%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>UHID No. :<br /></strong></span></td>"
                                + "<td style=\"width: 16%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.UHID + "</span></td>"
                                + "<td style=\"width: 14%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>IP No. :<br /></strong></span></td>"
                                + "<td style=\"width: 11.0085%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.IPNo + "</span></td>"
                                + "<td style=\"width: 16.9915%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Treating Dr. :<br /></strong></span></td>"
                                + "<td style=\"width: 15%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.DoctorName + "</span></td>"
                                + "</tr><tr>"
                                + "<td style=\"width: 12%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Department :<br /></strong></span></td>"
                                + "<td style=\"width: 16%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.DepartmentName + "</span></td>"
                                + "<td style=\"width: 14%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Date of Admission :<br /></strong></span></td>"
                                + "<td style=\"width: 11.0085%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.AdmissionDate.ToString("MMM dd yyyy hh:mm tt") + "</span></td>"
                                + "<td style=\"width: 16.9915%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Date of Discharge :<br /></strong></span></td>"
                                + "<td style=\"width: 15%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;" + objdata.DischargeDate + "</span></td>"
                                + "</tr><tr>"
                                + "<td style=\"width: 12%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>MLC No. :<br /></strong></span></td>"
                                + "<td style=\"width: 16%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;</span></td>"
                                + "<td style=\"width: 14%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>Informed to Police :<br /></strong></span></td>"
                                + "<td style=\"width: 11.0085%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;</span></td>"
                                + "<td style=\"width: 16.9915%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\"><strong>&nbsp;FIR No. :<br /></strong></span></td>"
                                + "<td style=\"width: 15%;\"><span style=\"font-family: arial, helvetica,  sans-serif; font-size: 11pt;\">&nbsp;</span></td>"
                                + "</tr></tbody></table>";


            string code = Commonfunction.getBarcode(objdata.UHID.ToString());
            string barcode = "<img style=\"height:35px;\" src=\"" + code + "\"/>";
            string Result = template.Replace("[header]", header);
            Result = Result.Replace("[barcode]", barcode);


            return Result;
        }

        protected void gvrecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (ddl_DisType.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "DischargeType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddl_DisType.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (e.CommandName == "Select")
                {
                    btnPrint.Visible = false;
                    DischargeData objdata = new DischargeData();
                    DischargeBO objstdBO = new DischargeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvrecord.Rows[i];
                    Label Ipno = (Label)gr.Cells[0].FindControl("lblIPNo");
                    lblSelectedIpNo.Text = Ipno.Text;
                    objdata.DischargeTypeID = Convert.ToInt32(ddl_DisType.SelectedValue == "0" ? null : ddl_DisType.SelectedValue);
                    objdata.IPNo = (Ipno.Text == "" ? null : Ipno.Text.ToString().Trim());
                    List<DischargeData> objresult = objstdBO.GetDischargeTemplateByIPNO(objdata);
                     if (objresult.Count > 0)
                     {
                        DischargeData ObjData = objresult[0];
                        
                         btnsave.Attributes.Remove("disabled");
                         if (objresult[0].Template == null)
                         { 
                             txtReport.InnerText = null;
                         }
                         else
                         {
                         if (objresult[0].IPNo.ToString() != null)
                             {
                                 txtReport.InnerHtml = generateTemplate(objresult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&"), ObjData);
                             }
                             else
                             {
                                 txtReport.InnerHtml = objresult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                               
                             }
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

        protected void btnsearchList_Click(object sender, EventArgs e)
        {
            BindSummaryList();
        }
        protected void BindSummaryList()
        {
            try
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
				if (txt_IPNo.Text == "")
				{
					if (txtdatefromList.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage2, "DateRange", 0);
						divmsg2.Visible = true;
						divmsg2.Attributes["class"] = "FailAlert";
						txtdatefromList.Focus();
						return;
					}
					else
					{
						lblmessage2.Visible = false;
					}
					if (txttoList.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage2, "DateRange", 0);
						divmsg2.Visible = true;
						divmsg2.Attributes["class"] = "FailAlert";
						txttoList.Focus();
						return;
					}
					else
					{
						lblmessage2.Visible = false;
					}
					if (txtdatefromList.Text != "")
					{
						if (Commonfunction.isValidDate(txtdatefromList.Text) == false)
						{
							Messagealert_.ShowMessage(lblmessage2, "ValidDatefrom", 0);
							divmsg2.Attributes["class"] = "FailAlert";
							divmsg2.Visible = true;
							txtdatefromList.Focus();
							return;
						}
					}
					else
					{
						lblmessage2.Visible = false;
					}
					if (txttoList.Text != "")
					{
						if (Commonfunction.isValidDate(txttoList.Text) == false)
						{
							Messagealert_.ShowMessage(lblmessage2, "ValidDateto", 0);
							divmsg2.Attributes["class"] = "FailAlert";
							divmsg2.Visible = true;
							txttoList.Focus();
							return;
						}
					}
					else
					{
						divmsg2.Visible = false;
					}
				}
				else
				{
					divmsg2.Visible = false;
				}
                List<DischargeData> objdischarge = GetSummaryList(0);
                if (objdischarge.Count > 0)
                {
                    gvSummaryList.DataSource = objdischarge;
                    gvSummaryList.DataBind();
                    gvSummaryList.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + objdischarge[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    gvSummaryList.DataSource = null;
                    gvSummaryList.DataBind();
                    gvSummaryList.Visible = true;
                    lblresult.Visible = false;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        protected void btnresetList_Click(object sender, EventArgs e)
        {
            txttoList.Text = "";
            txtdatefromList.Text = "";
            ddl_DisTypeList.SelectedIndex = 0;
            txtReport.InnerHtml = "";
            gvSummaryList.DataSource = null;
            gvSummaryList.DataBind();
            gvSummaryList.Visible = false;
            txt_IPNo.Text = "";
            txtdatefromList.Text = "";
            txttoList.Text = "";
            ddlexport.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
        }

        protected void gvSummaryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSummaryList.PageIndex = e.NewPageIndex;
            BindSummaryList();
        }

        protected void gvSummaryList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    DischargeData objdata = new DischargeData();
                    DischargeBO objstdBO = new DischargeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvSummaryList.Rows[i];
                    Label lblIPNo = (Label)gr.Cells[0].FindControl("lblIPNo");
                    objdata.IPNo = lblIPNo.Text;
                    List<DischargeData> objresult = objstdBO.GetDischargeTemplate(objdata);
                    txtReport.InnerText = null;
                    if (objresult.Count == 1)
                    {
                        txtReport.InnerHtml = objresult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                        lblSelectedIpNo.Text = objresult[0].IPNo.ToString();
                        tabdisSummary.ActiveTabIndex = 0;
                        btnsave.Attributes.Remove("disabled");

                    }
                    else
                    {
                       lblSelectedIpNo.Text = null;
                       txtReport.InnerText = null;
                    }
                  

                }
                if (e.CommandName == "Print")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvSummaryList.Rows[i];
                    Label lblIPNo = (Label)gr.Cells[0].FindControl("lblIPNo");
                   
                    string url = "../MedIPD/DischargeReportViewer.aspx?id=" + lblIPNo.Text;
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }

        }

      
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
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
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvSummaryList.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvSummaryList.Columns[4].Visible = false;
                    //gvSummaryList.Columns[5].Visible = false;
                    gvSummaryList.Columns[6].Visible = false;
                    gvSummaryList.Columns[7].Visible = false;

                    gvSummaryList.RenderControl(hw);
                    gvSummaryList.HeaderRow.Style.Add("width", "15%");
                    gvSummaryList.HeaderRow.Style.Add("font-size", "10px");
                    gvSummaryList.Style.Add("text-decoration", "none");
                    gvSummaryList.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvSummaryList.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DischargeSummaryList.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Patient Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DischargeSummaryList.xlsx");
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
            List<DischargeData> OTRoleDetails = GetSummaryList(0);
            List<DischargeDatatoExcel> ListexcelData = new List<DischargeDatatoExcel>();
            int i = 0;
            foreach (DischargeData row in OTRoleDetails)
            {
                DischargeDatatoExcel ExcelSevice = new DischargeDatatoExcel();
                ExcelSevice.IPNo = OTRoleDetails[i].IPNo;
                ExcelSevice.PatientName = OTRoleDetails[i].PatientName;
                ExcelSevice.DischargeTypedescp = OTRoleDetails[i].DischargeTypedescp;
                ExcelSevice.AddedBy = OTRoleDetails[i].AddedBy;
                ExcelSevice.DischargeTypedescp = OTRoleDetails[i].DischargeTypedescp;
                ExcelSevice.AddedDate = OTRoleDetails[i].AddedDate;
                gvSummaryList.Columns[4].Visible = false;
                gvSummaryList.Columns[5].Visible = false;
                gvSummaryList.Columns[6].Visible = false;
                gvSummaryList.Columns[7].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string url = "../MedIPD/DischargeReportViewer.aspx?id=" + lblSelectedIpNo.Text;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}