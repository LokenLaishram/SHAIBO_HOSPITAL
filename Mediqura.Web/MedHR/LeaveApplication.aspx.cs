using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedHrData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedHR
{
	public partial class LeaveApplication : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				MasterLookupBO mstlookup = new MasterLookupBO();
				Commonfunction.PopulateDdl(ddl_department, mstlookup.GetLookupsList(LookupName.Department));
				Commonfunction.Populatechkbox(ddl_leaveapprover, mstlookup.GetLookupsList(LookupName.LeaveApprover));
				listleaveapprover(LogData.DepartmentID);
				lblmessage.Visible = false;
				btnsend.Attributes["disabled"] = "disabled";
				lbladvanceavailable.Text = "";
				lbleaveconsumed.Text = "";
				lblmaxleavemonth.Text = "";
				lblmaxleaveyear.Text = "";
				lblleaverequestenable.Text = "";
				lblleavecarriedforward.Text = "";
				txt_NoofDays.Text = "";
				ddl_department.SelectedValue = LogData.DepartmentID.ToString();
				leavevalidation();
				bindddl();
				divapproved.Visible = false;
				
				if (Session["Leave_Record"] != null)
				{
					
					lblleaverecord.Text = Session["Leave_Record"].ToString();
					loadleaverecord(Convert.ToInt64(lblleaverecord.Text.Trim()));
					 if (Request.QueryString["ID"] == "2")
					{
						leavevalidation();
						txt_leaveremark.Visible = true;
						btnapproved.Visible = false;
						btnsend.Visible = false;
						btnsearch.Visible = false;
						btnresets.Visible = false;
						btnforward.Visible = true;
						GvLeave.Visible = false;
						divapproved.Visible = true;
						divrequest.Visible = false;
						hdrpastleave.Visible = false;
						btnredirect.Visible = true;
					}
					Session["Leave_Record"] = null;
				}
				else
				{
					LeaveRecord();
				}
											
			}
		}
		private void listleaveapprover(Int64 depID)
		{
			LeaveApplicationBO objleaveBO = new LeaveApplicationBO();
			List<LeaveApplicationData> lstapprover = objleaveBO.GetLeaveApproverByDeptID(depID);

			foreach (ListItem li in ddl_leaveapprover.Items)
			{
				if (lstapprover.Count>0)
				{
				for (int i = 0; i < lstapprover.Count; i++)
				{
					if (li.Value == lstapprover[i].LeaveApproverID.ToString())
					{
						li.Selected = true;
						i=lstapprover.Count;
					}
					else {
						li.Selected = false;
					}
					
				}
				}
				else
				{
						li.Selected = false;
				}

				
			}
		}
		private void leavevalidation()
		{
			txtdatefrom.Attributes["disabled"] = "disabled";
			txtto.Attributes["disabled"] = "disabled";
			txt_reason.Attributes["disabled"] = "disabled";
			ddlleavecategory.Attributes["disabled"] = "disabled";
			if (LogData.LeaveApproveEnable == 0)
			{
				ddl_department.Attributes["disabled"] = "disabled";

			}
			else
			{
				ddl_department.Attributes.Remove("disabled");
			}
			if (LogData.LeaveRequestEnable == 0)
			{
				ddlleavetype.Attributes["disabled"] = "disabled";
			}
			else
			{
				ddlleavetype.Attributes.Remove("disabled");
			}
		}
		private void loadleaverecord(Int64 LeaverecordID)    //for redirected page
		{
			LeaveApplicationData objdata = new LeaveApplicationData();
			LeaveApplicationBO objBO = new LeaveApplicationBO();
			objdata.LeaveRecordID = LeaverecordID;
			objdata.EmployeeID = LogData.EmployeeID;
			List<LeaveApplicationData> lstApplicantData = new List<LeaveApplicationData>();

			lstApplicantData = objBO.GetEmployeeLeaveRecordByID(objdata);
			if (lstApplicantData.Count > 0)
			{
				btnsend.Attributes["disabled"] = "disabled";

				if (lstApplicantData[0].messagetype == 1)
				{
					Messagealert_.ShowMessage(lblmessage, lstApplicantData[0].OutputMessage, lstApplicantData[0].messagetype);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					return;
				}
				else
				{
					lblmessage.Visible = false;
					ddl_department.SelectedValue = lstApplicantData[0].DepartmentID.ToString();
					ddlleavetype.SelectedValue = lstApplicantData[0].LeaveID.ToString();
					txtdatefrom.Text = lstApplicantData[0].datefrom.ToString("dd/MM/yyyy");
					txtto.Text = lstApplicantData[0].dateto.ToString("dd/MM/yyyy");
					ddlleavecategory.SelectedValue = lstApplicantData[0].LeaveCategoryID.ToString();
					txt_reason.Text = lstApplicantData[0].Leavereason.ToString();
					txt_leaveConsumed.Text = lstApplicantData[0].Leaveconsumed.ToString();
					txt_leavebalance.Text = lstApplicantData[0].Leaveavailable.ToString();
					txt_NoofDays.Text = lstApplicantData[0].Noofdays.ToString();
					List<String> itemCC_IDs = lstApplicantData[0].CC_IDs.ToString().Split(',').ToList<String>();
					foreach (ListItem li in ddl_leaveapprover.Items)
					{
						if (itemCC_IDs.Count > 0)
						{
							for (int j = 0; j < itemCC_IDs.Count; j++)
							{
								if (li.Value == itemCC_IDs[j])
								{
									li.Selected = true;
									j = itemCC_IDs.Count;
								}
								else
								{
									li.Selected = false;
								}

							}
						}
						else
						{
							li.Selected = false;
						}
					}
					ViewState["LeaveRecordID"] = lstApplicantData[0].LeaveRecordID.ToString();
					ViewState["LeaveEmployeeID"] = lstApplicantData[0].EmployeeID.ToString();
					ddl_department.Attributes["disabled"] = "disabled";
					ddlleavetype.Attributes["disabled"] = "disabled";
					txtdatefrom.Attributes.Remove("disabled");
					txtto.Attributes.Remove("disabled");
					txt_reason.Attributes["disabled"] = "disabled";
					if (ddlleavecategory.SelectedValue == "2")
					{
						ddlleavecategory.Attributes["disabled"] = "disabled";
					}
					else
					{
						ddlleavecategory.Attributes.Remove("disabled");
					}
					txt_leaveremark.Visible = true;
					btnapproved.Visible = true;
					btnsend.Visible = false;
					btnsearch.Visible = false;
					btnresets.Visible = false;
					btnforward.Visible = false;
					GvLeave.Visible = false;
					divapproved.Visible = true;
					divrequest.Visible = false;
					hdrpastleave.Visible = false;
					btnredirect.Visible = true;

				}
				leavetypevalidation();
			}
		
		
		}
		private void leavetypevalidation()
		{
			if (Convert.ToInt32(ddlleavetype.SelectedValue) > 0)
			{
				LeaveApplicationData objleavedata = new LeaveApplicationData();
				LeaveApplicationBO objleaveBO = new LeaveApplicationBO();
				objleavedata.LeaveID = Convert.ToInt32(ddlleavetype.SelectedValue == "" ? "0" : ddlleavetype.SelectedValue);
				objleavedata.EmployeeID = LogData.EmployeeID;
				objleavedata.MonthID = DateTime.Now.Month;
				List<LeaveApplicationData> leavedetails = objleaveBO.GetEmployeeLeaveDetailsByID(objleavedata);
				if (leavedetails.Count > 0)
				{
					txt_leaveConsumed.Text = leavedetails[0].Leaveconsumed.ToString() == "" ? "0" : leavedetails[0].Leaveconsumed.ToString();
					txt_leavebalance.Text = leavedetails[0].Leaveavailable.ToString() == "" ? "0" : leavedetails[0].Leaveavailable.ToString();
					lbladvanceavailable.Text = leavedetails[0].IsAvailedAdvance.ToString() == "" ? "0" : leavedetails[0].IsAvailedAdvance.ToString();
					lblmaxleavemonth.Text = leavedetails[0].MaxLeaveMonth.ToString() == "" ? "0" : leavedetails[0].MaxLeaveMonth.ToString();
					lblmaxleaveyear.Text = leavedetails[0].MaxLeaveYear.ToString() == "" ? "0" : leavedetails[0].MaxLeaveYear.ToString();
					lblleavecarriedforward.Text = leavedetails[0].Leavecarriedforward.ToString() == "" ? "0" : leavedetails[0].Leavecarriedforward.ToString();
					lblhalfday.Text = leavedetails[0].AllowHalfDay.ToString() == "" ? "0" : leavedetails[0].AllowHalfDay.ToString();
					if (lblhalfday.Text == "1")
					{
						ddlleavecategory.Attributes.Remove("disabled");
					}
					else
					{
						ddlleavecategory.Attributes["disabled"] = "disabled";
					}
					if (txt_leavebalance.Text == txt_leaveConsumed.Text)
					{
						txtdatefrom.Attributes["disabled"] = "disabled";
						txtto.Attributes["disabled"] = "disabled";
						txt_reason.Attributes["disabled"] = "disabled";
						txtdatefrom.Text = "";
						txtto.Text = "";
						txt_NoofDays.Text = "";
						btnsend.Attributes["disabled"] = "disabled";
						Messagealert_.ShowMessage(lblmessage, "Exceed available leave, consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 0);
						div1.Attributes["class"] = "FailAlert";
						div1.Visible = true;
						ddlleavetype.Focus();
						return;
					}
					else
					{
						txtdatefrom.Attributes.Remove("disabled");
						txtto.Attributes.Remove("disabled");
						txt_reason.Attributes.Remove("disabled");
						txtdatefrom.Text = txtdatefrom.Text == "" ? DateTime.Now.ToString("dd/MM/yyyy") : txtdatefrom.Text;
						txtto.Text = txtto.Text == "" ? DateTime.Now.ToString("dd/MM/yyyy") : txtto.Text;
						txt_NoofDays.Text = txt_NoofDays.Text == "" ? "1" : txt_NoofDays.Text;
						btnsend.Attributes.Remove("disabled");
						Messagealert_.ShowMessage(lblmessage, "consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 1);
						div1.Attributes["class"] = "SucessAlert";
						div1.Visible = true;
						return;
					}

				}
				else
				{
					txt_leaveConsumed.Text = "";
					txt_leavebalance.Text = "";
					lbladvanceavailable.Text = "";
					lblmaxleavemonth.Text = "";
					lblmaxleaveyear.Text = "";
					lblleavecarriedforward.Text = "";
					lblhalfday.Text = "";
					txt_NoofDays.Text = "";
					Messagealert_.ShowMessage(lblmessage, "consumed : 0 available : 0" , 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
					txtdatefrom.Attributes["disabled"] = "disabled";
					txtto.Attributes["disabled"] = "disabled";
					ddlleavecategory.Attributes["disabled"] = "disabled";
					txt_reason.Attributes["disabled"] = "disabled";
					btnsend.Attributes["disabled"] = "disabled";
					return;
				}

				

			}
			else
			{
				txtdatefrom.Attributes["disabled"] = "disabled";
				txtto.Attributes["disabled"] = "disabled";
				txt_reason.Attributes["disabled"] = "disabled";
				txtdatefrom.Text = "";
				txtto.Text = "";
				txt_leaveConsumed.Text = "";
				txt_leavebalance.Text = "";
				lbladvanceavailable.Text = "";
				lblmaxleavemonth.Text = "";
				lblmaxleaveyear.Text = "";
				lblleavecarriedforward.Text = "";
				lblhalfday.Text = "";
				txt_NoofDays.Text = "";
				div1.Visible = false;
				lblmessage.Text = "";
			}
		}
		protected void btnapproved_Click(object sender, EventArgs e)
		{
			if (txt_leaveremark.Text == "")
			{
				Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
				div1.Attributes["class"] = "FailAlert";
				txt_leaveremark.Focus();
				div1.Visible = true;
				return;
			}
			else
			{
				sendrequest(txt_leaveremark.Text);
				lblmessage.Visible = false;
				div1.Visible = false;
				LeaveApplicationData objleaveData = new LeaveApplicationData();
				LeaveApplicationBO objleaveBO = new LeaveApplicationBO();

				objleaveData.LeaveRecordID = Convert.ToInt64(ViewState["LeaveRecordID"].ToString());
				objleaveData.LeaveEmployeeID = Convert.ToInt64(ViewState["LeaveEmployeeID"].ToString());
				objleaveData.EmployeeID = LogData.EmployeeID;
				objleaveData.Remarks = txt_leaveremark.Text;
				div1.Visible = false;
				List<LeaveApplicationData> outmessage = objleaveBO.ApproveEmployeeLeaveRecordByID(objleaveData);
				lblmessage.Visible = true;
				if (outmessage.Count > 0)
				{

					Messagealert_.ShowMessage(lblmessage, outmessage[0].OutputMessage, outmessage[0].messagetype);
					if (outmessage[0].messagetype == 1)
					{
						div1.Attributes["class"] = "SucessAlert";
						GetLeaveRecord(2);
					}
					else
					{
						div1.Attributes["class"] = "FailAlert";

					}
					div1.Visible = true;

					return;
				}

				else
				{
					Messagealert_.ShowMessage(lblmessage, "system", 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
					return;
				}
			}
					
		}
		protected void btnforward_Click(object sender, EventArgs e)
		{
			if (txt_leaveremark.Text == "")
			{
				Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
				div1.Attributes["class"] = "FailAlert";
				txt_leaveremark.Focus();
				div1.Visible = true;
				return;
			}
			else
			{
				sendrequest(txt_leaveremark.Text);
			}
		}
		protected void btnredirect_Click(object sender, EventArgs e)
		{
			ViewState["LeaveRecordID"] = null;
			ViewState["LeaveEmployeeID"] = null;
			Response.Redirect("/MedHR/LeaveApproval.aspx", false);
		}
		private void bindddl()
		{
			MasterLookupBO mstlookup = new MasterLookupBO();
			Commonfunction.PopulateDdl(ddlleavetype, mstlookup.GetLeaveTypeByEmpID(LogData.EmployeeID));
			ddlleavetype.SelectedIndex = 0;
			
		}
		protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
		{

			if (Convert.ToInt32(ddl_department.SelectedValue) > 0)
			{
				listleaveapprover(Convert.ToInt32(ddl_department.SelectedValue));
			}
		}
		protected void ddlleavetype_SelectedIndexChanged(object sender, EventArgs e)
		{
			leavetypevalidation();
		}
		protected void txtdatefrom_TextChanged(object sender, EventArgs e)
		{
			var inputdate = DateTime.Parse(txtdatefrom.Text.Trim()).ToString("dd/MM/yyyy");
			var todaydate = DateTime.Now.ToString("dd/MM/yyyy");
					
				txtto.Text = "";
				txt_NoofDays.Text = "";
				Messagealert_.ShowMessage(lblmessage, "consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 1);
				div1.Attributes["class"] = "SucessAlert";
			

		}
		protected void txtto_TextChanged(object sender, EventArgs e)
		{
			var datefrom = DateTime.Parse(txtdatefrom.Text.Trim()).ToString("dd/MM/yyyy");
			var Maxvalidatedatepermonth = DateTime.Parse(txtdatefrom.Text.Trim()).AddDays(Convert.ToInt32(lblmaxleavemonth.Text) - 1).ToString("dd/MM/yyyy");
			var Maxvalidatedateperyear = DateTime.Parse(txtdatefrom.Text.Trim()).AddDays(Convert.ToInt32(lblmaxleaveyear.Text) - 1).ToString("dd/MM/yyyy");
			var dateto = DateTime.Parse(txtto.Text.Trim()).ToString("dd/MM/yyyy");
			if (Convert.ToDateTime(dateto) < Convert.ToDateTime(datefrom))
			{
				Messagealert_.ShowMessage(lblmessage, "Please enter valid date.", 0);
				div1.Attributes["class"] = "FailAlert";
				div1.Visible = true;
				txtto.Focus();
				txt_NoofDays.Text = "";
				return;
			}
			else
			{
				Messagealert_.ShowMessage(lblmessage, "consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 1);
				div1.Attributes["class"] = "SucessAlert";
				double daycount = 0;
				int noofdays=((Convert.ToDateTime(dateto)).Date -(Convert.ToDateTime(datefrom)).Date).Days +1;
				if (ddlleavecategory.SelectedValue == "2")
				{
					daycount = noofdays / 2;
					txt_NoofDays.Text = daycount.ToString();
				}
				else 
				{
					txt_NoofDays.Text = noofdays.ToString();
				}
				
				

			}
			var inputdate = DateTime.Parse(txtdatefrom.Text.Trim()).ToString("dd/MM/yyyy");
			var todaydate = DateTime.Now.ToString("dd/MM/yyyy");


			if (lbladvanceavailable.Text == "0")
			{
				if (Convert.ToDateTime(Maxvalidatedatepermonth) < Convert.ToDateTime(dateto))
				{
					Messagealert_.ShowMessage(lblmessage, "Exceed maximum leave.", 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
					txtto.Focus();
					return;
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 1);
					div1.Attributes["class"] = "SucessAlert";

				}
			}
			else if (lbladvanceavailable.Text == "1")
			{
				if (lblleavecarriedforward.Text == "0")
				{
					if (Convert.ToDateTime(Maxvalidatedateperyear).Year < Convert.ToDateTime(dateto).Year)
					{
						Messagealert_.ShowMessage(lblmessage, "Exceed maximum leave.", 0);
						div1.Attributes["class"] = "FailAlert";
						div1.Visible = true;
						txtto.Focus();
						return;
					}
					else
					{
						Messagealert_.ShowMessage(lblmessage, "consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 1);
						div1.Attributes["class"] = "SucessAlert";
					}

				}
				if (Convert.ToDateTime(Maxvalidatedateperyear) < Convert.ToDateTime(dateto))
				{
					Messagealert_.ShowMessage(lblmessage, "Exceed maximum leave.", 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
					txtto.Focus();
					return;
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 1);
					div1.Attributes["class"] = "SucessAlert";
				}


			}

		}
		protected void ddlleavecategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			ddlleavecategory.Attributes.Remove("disabled");
			double daycount = 0.0; 
			double daycounts=Convert.ToDouble(txt_NoofDays.Text.Trim());
			if (ddlleavecategory.SelectedValue == "2")
			{
				daycount = daycounts / 2;
				txt_NoofDays.Text = daycount.ToString();
				ddlleavecategory.Attributes["disabled"] = "disabled";
			}
		}
		protected void txt_reason_TextChanged(object sender, EventArgs e)
		{

			btnsend.Attributes.Remove("disabled");

		}
		private void sendrequest(string remark)
		{
			try
			{
				if (ddlleavetype.SelectedIndex == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "Please Select Leave Type.", 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
					ddlleavetype.Focus();
					return;
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 1);
					div1.Attributes["class"] = "SucessAlert";
				}
				var datefrom = DateTime.Parse(txtdatefrom.Text.Trim()).ToString("dd/MM/yyyy");
				var Maxvalidatedate = DateTime.Parse(txtdatefrom.Text.Trim()).AddDays(Convert.ToInt32(lblmaxleavemonth.Text)).ToString("dd/MM/yyyy");
				var dateto = DateTime.Parse(txtto.Text.Trim()).ToString("dd/MM/yyyy");
				var todaydate = DateTime.Now.ToString("dd/MM/yyyy");
				if (txtdatefrom.Text != "")
				{
					//if (Convert.ToDateTime(datefrom) < Convert.ToDateTime(todaydate))
					//{
					//    Messagealert_.ShowMessage(lblmessage, "Please enter valid date.", 0);
					//    div1.Attributes["class"] = "FailAlert";
					//    div1.Visible = true;
					//    txtdatefrom.Focus();
					//    return;
					//}
					//else
					//{
					//    div1.Visible = false;
					//    lblmessage.Visible = false;
					//}
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "Please enter date From.", 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
					txtdatefrom.Focus();
					return;
				}
				if (txtto.Text != "")
				{
					if (Convert.ToDateTime(dateto) < Convert.ToDateTime(datefrom))
					{
						Messagealert_.ShowMessage(lblmessage, "Please enter valid date.", 0);
						div1.Attributes["class"] = "FailAlert";
						div1.Visible = true;
						txtto.Focus();
						return;
					}
					else
					{
						Messagealert_.ShowMessage(lblmessage, "consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 1);
						div1.Attributes["class"] = "SucessAlert";
					}
					//if (Convert.ToDateTime(Maxvalidatedate) < Convert.ToDateTime(dateto))
					//{
					//    Messagealert_.ShowMessage(lblmessage, "Exceed maximum leave.", 0);
					//    div1.Attributes["class"] = "FailAlert";
					//    div1.Visible = true;
					//    txtto.Focus();
					//    return;
					//}
					//else
					//{
					//    div1.Visible = false;
					//    lblmessage.Visible = false;
					//}
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "Please enter date to.", 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
					txtto.Focus();
					return;
				}

				if (txt_reason.Text.Length == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "Please enter reason.", 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
					txt_reason.Focus();
					return;
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "consumed : " + txt_leaveConsumed.Text + " available : " + txt_leavebalance.Text, 1);
					div1.Attributes["class"] = "SucessAlert";
				}
				LeaveApplicationData objleaveData = new LeaveApplicationData();
				LeaveApplicationBO objleaveBO = new LeaveApplicationBO();
				IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
				objleaveData.LeaveID = Convert.ToInt32(ddlleavetype.SelectedValue == "" ? "0" : ddlleavetype.SelectedValue);
				DateTime Leavedatefrom = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
				objleaveData.datefrom = Leavedatefrom;
				DateTime Leavedateto = txtto.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
				objleaveData.dateto = Leavedateto;
				objleaveData.LeaveCategoryID = Convert.ToInt32(ddlleavecategory.SelectedValue == "" ? "1" : ddlleavecategory.SelectedValue);
				objleaveData.Noofdays = Convert.ToDouble(txt_NoofDays.Text.Trim());
				objleaveData.Leavereason = txt_reason.Text.Trim();
				objleaveData.EmployeeID = LogData.EmployeeID;
				objleaveData.HospitalID = LogData.HospitalID;
				objleaveData.FinancialYearID = LogData.FinancialYearID;
				objleaveData.AddedBy = LogData.UserName;
				objleaveData.Leaveconsumed = Convert.ToDouble(txt_leaveConsumed.Text.Trim());
				objleaveData.Leaveavailable = Convert.ToDouble(txt_leavebalance.Text.Trim());
				objleaveData.DepartmentID = Convert.ToInt32(ddl_department.SelectedValue);
				String CC_To = "";
				String CC_IDs = "";
				foreach (ListItem li in ddl_leaveapprover.Items)
				{
					if (li.Selected)
					{
						CC_IDs += li.Value.ToString() + ',';
						CC_To += li.Text.ToString() + ',';
					}

				}
				if (CC_IDs.Length > 0)
				{
					objleaveData.CC_IDs = CC_IDs.Remove(CC_IDs.Length - 1);
					objleaveData.CC_TO = CC_To.Remove(CC_To.Length - 1);
				}
				else
				{
					objleaveData.CC_TO = "";
					objleaveData.CC_IDs = "";
				}
				objleaveData.ActionType = Enumaction.Insert;
				if (ViewState["LeaveRecordID"] != null)
				{
					if (LogData.UpdateEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";
						return;
					}
					else
					{
						lblmessage.Visible = false;
						if (ViewState["LeaveEmployeeID"] == null)
						{
							objleaveData.ActionType = Enumaction.Update;
						}
						else
						{
							objleaveData.ActionType = Enumaction.Forward;
						}
						objleaveData.Remarks = remark;
						objleaveData.LeaveRecordID = Convert.ToInt64(ViewState["LeaveRecordID"].ToString() == "" ? "0" : ViewState["LeaveRecordID"].ToString());
					}
				}
				List<LeaveApplicationData> outmessage = objleaveBO.UpdateLeaveDetails(objleaveData);

				if (outmessage.Count > 0)
				{
					btnsend.Attributes["disabled"] = "disabled";
					Messagealert_.ShowMessage(lblmessage, outmessage[0].OutputMessage, outmessage[0].messagetype);
					if (outmessage[0].messagetype == 1)
					{
						div1.Attributes["class"] = "SucessAlert";
						LeaveRecord();
					}
					else
					{
						div1.Attributes["class"] = "FailAlert";
						GetLeaveRecord(1);
					}
					div1.Visible = true;

					return;
				}

				else
				{
					Messagealert_.ShowMessage(lblmessage, "system", 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
					return;
				}
			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
				div1.Attributes["class"] = "FailAlert";
				div1.Visible = true;
			}
		}
		protected void btnsend_Click(object sender, EventArgs e)
		{
			string req = "";
			sendrequest(req);
		}
		protected void btnsearch_Click(object sender, EventArgs e)
		{
			try
			{
				List<LeaveApplicationData> lstLeaveRecord = GetLeaveRecord(0);
				if (lstLeaveRecord.Count > 0)
				{
					GvLeave.DataSource = lstLeaveRecord;
					GvLeave.DataBind();
					GvLeave.Visible = true;

				}
				else
				{
					GvLeave.DataSource = null;
					GvLeave.DataBind();
					GvLeave.Visible = true;
					lblresult.Visible = true;

				}
			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}

		}
		private List<LeaveApplicationData> GetLeaveRecord(int searchtype)
		{
			LeaveApplicationData objleaveData = new LeaveApplicationData();
			LeaveApplicationBO objleaveBO = new LeaveApplicationBO();
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			objleaveData.LeaveID = Convert.ToInt32(ddlleavetype.SelectedValue == "" ? "0" : ddlleavetype.SelectedValue);
			DateTime Leavedatefrom = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			objleaveData.datefrom = Leavedatefrom;
			DateTime Leavedateto = txtto.Text.Trim() == "" ? new DateTime(DateTime.Now.Year,12,31) : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			objleaveData.dateto = Leavedateto;
			objleaveData.LeaveCategoryID = Convert.ToInt32(ddlleavecategory.SelectedValue == "" ? "0" : ddlleavecategory.SelectedValue);
			objleaveData.Leavereason = txt_reason.Text.Trim();
			objleaveData.EmployeeID = LogData.EmployeeID;
			objleaveData.HospitalID = LogData.HospitalID;
			objleaveData.FinancialYearID = LogData.FinancialYearID;
			objleaveData.SearchType = Convert.ToInt32(ddlleavetype.SelectedValue == "0" ? 1 : 0);
			return objleaveBO.GetLeaveRecord(objleaveData);
		}
		private void LeaveRecord()
		{
			List<LeaveApplicationData> lstLeaveRecord = GetLeaveRecord(1);
			if (lstLeaveRecord.Count > 0)
			{
				GvLeave.DataSource = lstLeaveRecord;
				GvLeave.DataBind();
				GvLeave.Visible = true;

			}
			else
			{
				GvLeave.DataSource = null;
				GvLeave.DataBind();
				GvLeave.Visible = true;
				lblresult.Visible = true;

			}
		}
		protected void btnresets_Click(object sender, EventArgs e)
		{
			leavevalidation();
			txt_reason.Text = "";
			txtdatefrom.Text = "";
			txtto.Text = "";
			ddlleavecategory.SelectedIndex = 0;
			ddlleavetype.SelectedIndex = 0;
			GvLeave.DataSource = null;
			txt_leavebalance.Text = "0";
			txt_leaveConsumed.Text = "0";
			txt_NoofDays.Text = "0";
			ViewState["LeaveRecordID"] = null;
			ViewState["LeaveEmployeeID"] = null;
			btnsend.Attributes["disabled"] = "disabled";
			lblmessage.Text = "";
			lblmessage.Visible = false;
			div1.Visible = false;
			GvLeave.DataBind();
			LeaveRecord();
		}
		protected void GvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
		{

			if (e.Row.RowType == DataControlRowType.DataRow)
			{

				LinkButton lnkEdit = (e.Row.FindControl("lnkEdit") as LinkButton);
				LinkButton lnkDelete = (e.Row.FindControl("lnkDelete") as LinkButton);
				Label lblLeaveStatus = (e.Row.FindControl("lblLeaveStatus") as Label);
				Label lblleaveapproval = (e.Row.FindControl("lblleaveapproval") as Label);
				Label lbldisableEdit = (e.Row.FindControl("lbldisableEdit") as Label);
				Label lbldisableDelete = (e.Row.FindControl("lbldisableDelete") as Label);

				if (lblLeaveStatus.Text == "1" || lblLeaveStatus.Text == "0")
				{
					lnkEdit.Visible = false;
					lnkDelete.Visible = false;
					lbldisableEdit.Visible = true;
					lbldisableDelete.Visible = true;
					lbldisableEdit.ToolTip = "Cannot Edit";
					lbldisableDelete.ToolTip = "Cannot Delete";
					if (lblLeaveStatus.Text == "1")
					{
						lblleaveapproval.ForeColor = System.Drawing.Color.Green;
					}
					else
					{
						lblleaveapproval.ForeColor = System.Drawing.Color.Red;
					}
				}
				else
				{
					lnkEdit.Visible = true;
					lnkDelete.Visible = true;
					lbldisableEdit.Visible = false;
					lbldisableDelete.Visible = false;
					lnkEdit.ToolTip = "Edit";
					lnkDelete.ToolTip = "Delete";
					lblleaveapproval.ForeColor = System.Drawing.Color.Blue;
				}



			}
		}
		protected void GvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				if (e.CommandName == "Edits")
				{
					if (LogData.EditEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";
						return;
					}
					else
					{
						lblmessage.Visible = false;
					}
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = GvLeave.Rows[i];
					Label lblleaveRecordID = (Label)gr.Cells[0].FindControl("lblleaveRecordID");
					Int64 LeaveRecordID = Convert.ToInt64(lblleaveRecordID.Text);
					LeaveApplicationData objdata = new LeaveApplicationData();
					LeaveApplicationBO objBO = new LeaveApplicationBO();
					objdata.LeaveRecordID = LeaveRecordID;
					objdata.EmployeeID = LogData.EmployeeID;
					List<LeaveApplicationData> lstApplicantData = new List<LeaveApplicationData>();

					lstApplicantData = objBO.GetEmployeeLeaveRecordByID(objdata);
					if (lstApplicantData.Count > 0)
					{

						btnsend.Attributes.Remove("disabled");
						if (lstApplicantData[0].messagetype == 1)
						{
							Messagealert_.ShowMessage(lblmessage, lstApplicantData[0].OutputMessage, lstApplicantData[0].messagetype);
							div1.Visible = true;
							div1.Attributes["class"] = "FailAlert";
							return;
						}
						else
						{
							lblmessage.Visible = false;
							MasterLookupBO mstlookup = new MasterLookupBO();
							Commonfunction.PopulateDdl(ddlleavetype, mstlookup.GetLookupsList(LookupName.Leavetype));
							Commonfunction.PopulateDdl(ddl_department, mstlookup.GetLookupsList(LookupName.Department));
							Commonfunction.Populatechkbox(ddl_leaveapprover, mstlookup.GetLookupsList(LookupName.LeaveApprover));
							ddl_department.SelectedValue = lstApplicantData[0].DepartmentID.ToString();
							ddlleavetype.SelectedValue = lstApplicantData[0].LeaveID.ToString();
							txtdatefrom.Text = lstApplicantData[0].datefrom.ToString("dd/MM/yyyy");
							txtto.Text = lstApplicantData[0].dateto.ToString("dd/MM/yyyy");
							ddlleavecategory.SelectedValue = lstApplicantData[0].LeaveCategoryID.ToString();
							txt_reason.Text = lstApplicantData[0].Leavereason.ToString();
							txt_leaveConsumed.Text = lstApplicantData[0].Leaveconsumed.ToString();
							txt_leavebalance.Text = lstApplicantData[0].Leaveavailable.ToString();
							txt_NoofDays.Text = lstApplicantData[0].Noofdays.ToString();
							List<String> itemCC_IDs = lstApplicantData[0].CC_IDs.ToString().Split(',').ToList<String>();
							foreach (ListItem li in ddl_leaveapprover.Items)
							{
								if (itemCC_IDs.Count > 0)
								{
									for (int j = 0; j < itemCC_IDs.Count; j++)
									{
										if (li.Value == itemCC_IDs[j])
										{
											li.Selected = true;
											j = itemCC_IDs.Count;
										}
										else
										{
											li.Selected = false;
										}

									}
								}
								else
								{
									li.Selected = false;
								}
							}
							ViewState["LeaveRecordID"] = lstApplicantData[0].LeaveRecordID.ToString();
							ViewState["LeaveEmployeeID"] = lstApplicantData[0].EmployeeID.ToString();
							leavetypevalidation();
						}
					}
				}
				if (e.CommandName == "Deletes")
				{
					if (LogData.DeleteEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";
						return;
					}
					else
					{
						lblmessage.Visible = false;
					}
					LeaveApplicationData objdata = new LeaveApplicationData();
					LeaveApplicationBO objBO = new LeaveApplicationBO();
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = GvLeave.Rows[i];
					Label lblleaveRecordID = (Label)gr.Cells[0].FindControl("lblleaveRecordID");
					Int64 LeaveRecordID = Convert.ToInt64(lblleaveRecordID.Text);
					TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
					Label lblactionremarks = (Label)gr.Cells[0].FindControl("lblactionremarks");

					txtremarks.Visible = true;
					txtremarks.Enabled = true;
					if (txtremarks.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
						div1.Attributes["class"] = "FailAlert";
						txtremarks.Focus();
						div1.Visible = true;
						txtremarks.Visible = true;
						lblactionremarks.Visible = false;
						return;
					}
					else
					{
						lblactionremarks.Visible = false;
						objdata.Remarks = txtremarks.Text;
						div1.Visible = false;
					}
					objdata.LeaveRecordID = LeaveRecordID;
					objdata.EmployeeID = LogData.EmployeeID;
					lblactionremarks.Visible = true;
					List<LeaveApplicationData> outmessage = objBO.DeleteEmployeeLeaveRecordByID(objdata);

					if (outmessage.Count > 0)
					{
						btnsend.Attributes["disabled"] = "disabled";
						Messagealert_.ShowMessage(lblmessage, outmessage[0].OutputMessage, outmessage[0].messagetype);
						if (outmessage[0].messagetype == 1)
						{
							div1.Attributes["class"] = "SucessAlert";
							LeaveRecord();
						}
						else
						{
							div1.Attributes["class"] = "FailAlert";
							GetLeaveRecord(1);
						}
						div1.Visible = true;

						return;
					}

					else
					{
						Messagealert_.ShowMessage(lblmessage, "system", 0);
						div1.Attributes["class"] = "FailAlert";
						div1.Visible = true;
						return;
					}
					

				}

			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
				div1.Attributes["class"] = "FailAlert";
				div1.Visible = true;
				return;
			}
		}

	}
}