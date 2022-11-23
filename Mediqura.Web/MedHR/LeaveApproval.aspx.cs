using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedHrData;
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

namespace Mediqura.Web.MedHR
{
	public partial class LeaveApproval : BasePage
	{
		string empID = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				tap2btnupdate.Attributes["disabled"] = "disabled";
				ddladjustment.Attributes["disabled"] = "disabled";
					//AutoCompleteExtender1.ContextKey = ;
					lblmessage.Visible = false;
					ddlstatus.SelectedIndex = 0;
					bindddl();
					

				
			}
		}
		[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
		public static List<string> GetEmployeeDetails(string prefixText, int count, string contextKey)
		{

			EmployeeData Objempdata = new EmployeeData();
			EmployeeBO objjempBO = new EmployeeBO();
			List<EmployeeData> getResult = new List<EmployeeData>();
			Objempdata.EmpName = prefixText;
			Objempdata.EmployeeID = Convert.ToInt32(contextKey);
			getResult = objjempBO.GetEmpdetails(Objempdata);
			List<String> list = new List<String>();
			for (int i = 0; i < getResult.Count; i++)
			{
				list.Add(getResult[i].EmpName.ToString());
			}
			return list;
		}

		private void bindddl()
		{
			MasterLookupBO mstlookup = new MasterLookupBO();
			Commonfunction.PopulateDdl(ddlleavetype, mstlookup.GetLookupsList(LookupName.Leavetype));
			ddlleavetype.SelectedIndex = 0;
			List<LeaveApplicationData> lstLeaveRecord = GetLeaveRecord(2);
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
		protected void btnsearch_Click(object sender, EventArgs e)
		{
			try
			{
				List<LeaveApplicationData> lstLeaveRecord = GetLeaveRecord(2);
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
			var source1 = txt_employeeDetails.Text.ToString();
			if (source1.Contains(":"))
			{
				empID = source1.Substring(source1.LastIndexOf(':') + 1);

			}
			else
			{
				empID = LogData.EmployeeID.ToString();
			}
			LeaveApplicationData objleaveData = new LeaveApplicationData();
			LeaveApplicationBO objleaveBO = new LeaveApplicationBO();
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			objleaveData.LeaveEmployeeID = Convert.ToInt64(empID == "" ? "0" : empID);
			objleaveData.LeaveID = Convert.ToInt32(ddlleavetype.SelectedValue == "" ? "0" : ddlleavetype.SelectedValue);
			DateTime Leavedatefrom = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			objleaveData.datefrom = Leavedatefrom;
			DateTime Leavedateto = txtto.Text.Trim() == "" ? new DateTime(DateTime.Now.Year, 12, 31) : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			objleaveData.dateto = Leavedateto;
			objleaveData.LeaveCategoryID = Convert.ToInt32(ddlleavecategory.SelectedValue == "" ? "0" : ddlleavecategory.SelectedValue);
			objleaveData.leaveaction = Convert.ToInt32(ddlstatus.SelectedValue);
			objleaveData.EmployeeID = LogData.EmployeeID;
			objleaveData.HospitalID = LogData.HospitalID;
			objleaveData.FinancialYearID = LogData.FinancialYearID;
			objleaveData.SearchType = searchtype;
			return objleaveBO.GetLeaveRecord(objleaveData);
		}

		protected void GvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
		{

			if (e.Row.RowType == DataControlRowType.DataRow)
			{

				LinkButton lnkEdit = (e.Row.FindControl("lnkEdit") as LinkButton);
				LinkButton lnkreject = (e.Row.FindControl("lnkreject") as LinkButton);
				LinkButton lnkforward = (e.Row.FindControl("lnkforward") as LinkButton);
				LinkButton lnkapproved = (e.Row.FindControl("lnkapproved") as LinkButton);
				LinkButton lnkadjust = (e.Row.FindControl("lnkadjust") as LinkButton);
				Label lblLeaveStatus = (e.Row.FindControl("lblLeaveStatus") as Label);
				Label lblleaveaction = (e.Row.FindControl("lblleaveaction") as Label);
				Label lbldisableEdit = (e.Row.FindControl("lbldisableEdit") as Label);
				Label lbldisableDelete = (e.Row.FindControl("lbldisableDelete") as Label);
				Label lbldisableforward = (e.Row.FindControl("lbldisableforward") as Label);

				if (lblLeaveStatus.Text != "2" )
				{
					lnkEdit.Visible = false;
					lnkreject.Visible = false;
					lnkforward.Visible = false;
					lbldisableEdit.ToolTip = "Cannot Edit";
					lbldisableDelete.ToolTip = "Cannot Reject";
					lbldisableforward.ToolTip = "Cannot Forward";
					lbldisableEdit.Visible = true;
					lbldisableDelete.Visible = true;
					lbldisableforward.Visible = true;
				}
					if (lblLeaveStatus.Text == "1")
					{
						lnkadjust.Visible = true;
						lnkapproved.Visible = false;
						lblleaveaction.ForeColor = System.Drawing.Color.Green;
					}
					else if (lblLeaveStatus.Text == "2")
					{
						lblleaveaction.ForeColor = System.Drawing.Color.Blue;
					}
					else if (lblLeaveStatus.Text == "3")
					{
						lblleaveaction.ForeColor = System.Drawing.Color.Red;
					}
				}
		}

		protected void GvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				if (e.CommandName == "Edits")
				{
					if (LogData.LeaveApproveEnable == 0)
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
					Label lblrequestempid = (Label)gr.Cells[0].FindControl("lblrequestempid");
					Int64 LeaveRecordID = Convert.ToInt64(lblleaveRecordID.Text);
					LeaveApplicationData objdata = new LeaveApplicationData();
					LeaveApplicationBO objBO = new LeaveApplicationBO();
					objdata.LeaveRecordID = LeaveRecordID;
					objdata.EmployeeID = Convert.ToInt64(lblrequestempid.Text.Trim());
					List<LeaveApplicationData> lstApplicantData = new List<LeaveApplicationData>();

					lstApplicantData = objBO.GetEmployeeLeaveRecordByID(objdata);
					if (lstApplicantData.Count > 0)
					{


						if (lstApplicantData[0].messagetype == 1)
						{
							Messagealert_.ShowMessage(lblmessage, lstApplicantData[0].OutputMessage, lstApplicantData[0].messagetype);
							div1.Visible = true;
							div1.Attributes["class"] = "FailAlert";
							return;
						}
					
						else
						{
							Session["Leave_Record"] = lstApplicantData[0].LeaveRecordID.ToString();
							Response.Redirect("/MedHR/LeaveApplication.aspx?ID=1", false); //Edit denote by 1

						}
					}
				}
				if (e.CommandName == "Reject")
				{
					if (LogData.LeaveApproveEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage, "LeaveRejectEnable", 0);
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
						lblactionremarks.Visible = false;
						return;
					}
					else
					{
						objdata.Remarks = txtremarks.Text;
						div1.Visible = false;
						lblactionremarks.Visible = false;
					}
					objdata.LeaveRecordID = LeaveRecordID;
					objdata.EmployeeID = LogData.EmployeeID;
					lblactionremarks.Visible = true;
					txtremarks.Visible = false;
					txtremarks.Enabled = false;
					List<LeaveApplicationData> outmessage = objBO.RejectEmployeeLeaveRecordByID(objdata);

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
				if (e.CommandName == "Forward")
				{
					if (LogData.LeaveApproveEnable == 0)
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
					Label lblrequestempid = (Label)gr.Cells[0].FindControl("lblrequestempid");
					Int64 LeaveRecordID = Convert.ToInt64(lblleaveRecordID.Text);
					LeaveApplicationData objdata = new LeaveApplicationData();
					LeaveApplicationBO objBO = new LeaveApplicationBO();
					objdata.LeaveRecordID = LeaveRecordID;
					objdata.EmployeeID = Convert.ToInt64(lblrequestempid.Text.Trim());
					List<LeaveApplicationData> lstApplicantData = new List<LeaveApplicationData>();

					lstApplicantData = objBO.GetEmployeeLeaveRecordByID(objdata);
					if (lstApplicantData.Count > 0)
					{


						if (lstApplicantData[0].messagetype == 1)
						{
							Messagealert_.ShowMessage(lblmessage, lstApplicantData[0].OutputMessage, lstApplicantData[0].messagetype);
							div1.Visible = true;
							div1.Attributes["class"] = "FailAlert";
							return;
						}

						else
						{
							Session["Leave_Record"] = lstApplicantData[0].LeaveRecordID.ToString();
							Response.Redirect("/MedHR/LeaveApplication.aspx?ID=2", false);   //forward denote by 2

						}
					}
				}
				if (e.CommandName == "Approved")
				{
					if (LogData.LeaveApproveEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage, "LeaveApproveEnable", 0);
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
					Label lblrequestempid = (Label)gr.Cells[0].FindControl("lblrequestempid");
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
						return;
					}
					else
					{
						objdata.Remarks = txtremarks.Text;
						div1.Visible = false;
						lblactionremarks.Visible = false;
					}
					objdata.LeaveRecordID = LeaveRecordID;
					objdata.LeaveEmployeeID = Convert.ToInt64(lblrequestempid.Text.Trim());
					objdata.EmployeeID = LogData.EmployeeID;
					lblactionremarks.Visible = true;
					txtremarks.Visible = false;
					txtremarks.Enabled = false;
					List<LeaveApplicationData> outmessage = objBO.ApproveEmployeeLeaveRecordByID(objdata);

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
				if (e.CommandName == "Adjust")
				{
					if (LogData.LeaveApproveEnable == 0)
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
					Label lblrequestempid = (Label)gr.Cells[0].FindControl("lblrequestempid");
					Int64 LeaveRecordID = Convert.ToInt64(lblleaveRecordID.Text);
					LeaveApplicationData objdata = new LeaveApplicationData();
					LeaveApplicationBO objBO = new LeaveApplicationBO();
					objdata.LeaveRecordID = LeaveRecordID;
					objdata.EmployeeID = Convert.ToInt64(LogData.EmployeeID);
					objdata.LeaveEmployeeID = Convert.ToInt64(lblrequestempid.Text.Trim());
					objdata.datefrom = DateTime.Now;  //dummy 
					objdata.dateto = DateTime.Now;  //dummy 
					objdata.SearchType = 3;//adjustpage
					List<LeaveApplicationData> lstApplicantData = new List<LeaveApplicationData>();

					lstApplicantData = objBO.GetLeaveRecord(objdata);
					if (lstApplicantData.Count > 0)
					{
						if (lstApplicantData[0].messagetype == 1)
						{
							Messagealert_.ShowMessage(lblmessage, lstApplicantData[0].OutputMessage, lstApplicantData[0].messagetype);
							div1.Visible = true;
							div1.Attributes["class"] = "FailAlert";
							return;
						}

						else
						{
							GvLeave2.DataSource = lstApplicantData;
							GvLeave2.DataBind();
							tap2lblrequestedemployeeID.Text = lstApplicantData[0].EmployeeID.ToString();
							tap2lblleaveRecordID.Text = lstApplicantData[0].LeaveRecordID.ToString();
							List<LeaveApplicationData> lstLeaveRecord = objBO.GetLeaveRecordDetails(objdata);
							if (lstLeaveRecord.Count > 0)
							{
								GvLeave2result.DataSource = lstLeaveRecord;
								GvLeave2result.DataBind();
								GvLeave2result.Visible = true;
								ddladjustment.Attributes.Remove("disabled");
								tap2btnupdate.Attributes.Remove("disabled");
								tap2btnupdate.Focus();

							}
							else
							{
								GvLeave2result.DataSource = null;
								GvLeave2result.DataBind();
								GvLeave2result.Visible = true;
								lblresult.Visible = true;
								ddladjustment.Attributes["disabled"] = "disabled";
								tap2btnupdate.Attributes["disabled"] = "disabled";
							}
							tabcontainerLeave.ActiveTabIndex = 1;

						}
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

		protected void GvLeave2_RowDataBound(object sender, GridViewRowEventArgs e)
		{

			if (e.Row.RowType == DataControlRowType.DataRow)
			{

				Label tap2lblleaveaction = (e.Row.FindControl("tap2lblleaveaction") as Label);
				Label tap2lblLeaveStatus = (e.Row.FindControl("tap2lblLeaveStatus") as Label);

				if  (tap2lblLeaveStatus.Text == "1")
				{
					tap2lblleaveaction.ForeColor = System.Drawing.Color.Green;
				}
				else if (tap2lblLeaveStatus.Text == "2")
				{
					tap2lblleaveaction.ForeColor = System.Drawing.Color.Blue;
				}
				else if (tap2lblLeaveStatus.Text == "3")
				{
					tap2lblleaveaction.ForeColor = System.Drawing.Color.Red;
				}
			}
		}

		protected void GvLeave2result_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			MasterLookupBO mstlookup = new MasterLookupBO();
			List<LookupItem> lookupleave = mstlookup.GetLookupsList(LookupName.Leavetype);
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Label tap2resultlblleavetype = (Label)e.Row.FindControl("tap2resultlblleavetype");
				Label tap2resultlblLeavedate = (Label)e.Row.FindControl("tap2resultlblLeavedate");
				Label tap2resultlblleavecategory = (Label)e.Row.FindControl("tap2resultlblleavecategory");
				Label tap2resultlblLeaveStatus = (Label)e.Row.FindControl("tap2resultlblLeaveStatus");
				DropDownList tap2resultddlleavetype = (DropDownList)e.Row.FindControl("tap2resultddlleavetype");
				Commonfunction.PopulateDdl(tap2resultddlleavetype, lookupleave);
				DropDownList tap2resultddlleavecat = (DropDownList)e.Row.FindControl("tap2resultddlleavecat");
				DropDownList tap2resultddllaction = (DropDownList)e.Row.FindControl("tap2resultddllaction");
				DateTime date = DateTime.Parse(tap2resultlblLeavedate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
				tap2resultddlleavetype.SelectedValue = tap2resultlblleavetype.Text == "" ? "0" : tap2resultlblleavetype.Text;
				tap2resultddlleavecat.SelectedValue = tap2resultlblleavecategory.Text == "" ? "0" : tap2resultlblleavecategory.Text;
				tap2resultddllaction.SelectedValue = tap2resultlblLeaveStatus.Text == "" ? "0" : tap2resultlblLeaveStatus.Text;
				if (tap2resultddllaction.SelectedValue == "3" || DateTime.Now.Date > date.Date)
				{
					tap2resultddlleavetype.Attributes["disabled"] = "disabled";
					tap2resultddlleavecat.Attributes["disabled"] = "disabled";
					tap2resultddllaction.Attributes["disabled"] = "disabled";
				}
				else
				{
					tap2resultddlleavetype.Attributes.Remove("disabled");
					tap2resultddlleavecat.Attributes.Remove("disabled");
					tap2resultddllaction.Attributes.Remove("disabled");

				}

			}

		}

		protected void ddladjustment_SelectedIndexChanged(object sender, EventArgs e)
		{
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			foreach (GridViewRow row in GvLeave2result.Rows)
			{
				try
				{
					DropDownList tap2resultddlleavetype = (DropDownList)row.Cells[0].FindControl("tap2resultddlleavetype");
					DropDownList tap2resultddlleavecat = (DropDownList)row.Cells[0].FindControl("tap2resultddlleavecat");
					DropDownList tap2resultddllaction = (DropDownList)row.Cells[0].FindControl("tap2resultddllaction");
					Label tap2resultlblLeavedate = (Label)row.Cells[0].FindControl("tap2resultlblLeavedate");
					Label tap2resultlblLeaveStatus = (Label)row.Cells[0].FindControl("tap2resultlblLeaveStatus");

					DateTime date = DateTime.Parse(tap2resultlblLeavedate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
					if (DateTime.Now.Date > date.Date)
					{
						tap2resultddlleavetype.Attributes["disabled"] = "disabled";
						tap2resultddlleavecat.Attributes["disabled"] = "disabled";
						tap2resultddllaction.Attributes["disabled"] = "disabled";
					}
					 if (tap2resultlblLeaveStatus.Text == "3")
					{
						tap2resultddlleavetype.Attributes["disabled"] = "disabled";
						tap2resultddlleavecat.Attributes["disabled"] = "disabled";
						tap2resultddllaction.Attributes["disabled"] = "disabled";
					}
					 if (ddladjustment.SelectedValue == "1")
					{
						tap2resultddlleavetype.Attributes.Remove("disabled");
						tap2resultddlleavecat.Attributes["disabled"] = "disabled";
						tap2resultddllaction.Attributes["disabled"] = "disabled";
					}
					else if (ddladjustment.SelectedValue == "2")
					{
						tap2resultddlleavetype.Attributes["disabled"] = "disabled";
						tap2resultddlleavecat.Attributes.Remove("disabled");
						tap2resultddllaction.Attributes["disabled"] = "disabled";
					}
					else if (ddladjustment.SelectedValue == "3")
					{
						tap2resultddlleavetype.Attributes["disabled"] = "disabled";
						tap2resultddlleavecat.Attributes["disabled"] = "disabled";
						tap2resultddllaction.Attributes.Remove("disabled");

					}
					else
					{
						tap2resultddlleavetype.Attributes.Remove("disabled");
						tap2resultddlleavecat.Attributes.Remove("disabled");
						tap2resultddllaction.Attributes.Remove("disabled");

					}

				}
				catch (Exception ex) //Exception in agent layer itself
				{
					PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
					LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
					Messagealert_.ShowMessage(lblmessage, "system", 0);
				}

			}
		}

		protected void tap2resultddllaction_SelectedIndexChanged(object sender, EventArgs e)
		{

			DropDownList tap2resultddllaction = (DropDownList)sender;
			if (tap2resultddllaction.SelectedValue == "3")
			{

				GridViewRow rows = (GridViewRow)tap2resultddllaction.NamingContainer;
				foreach (GridViewRow row in GvLeave2result.Rows)
				{
					if (row.RowIndex >= rows.RowIndex)
					{
						DropDownList tap2resultddlleavetype = (DropDownList)row.Cells[0].FindControl("tap2resultddlleavetype");
						DropDownList tap2resultddlleavecat = (DropDownList)row.Cells[0].FindControl("tap2resultddlleavecat");
						DropDownList tap2resultddllactions = (DropDownList)row.Cells[0].FindControl("tap2resultddllaction");
						tap2resultddlleavetype.Attributes["disabled"] = "disabled";
						tap2resultddlleavecat.Attributes["disabled"] = "disabled";
						tap2resultddllactions.Attributes["disabled"] = "disabled";
						tap2resultddllactions.SelectedValue = "3";

					}
				}
			}


		}
		protected void tap2btnupdate_Click(object sender, EventArgs e)
		{
			try
			{
				if (LogData.LeaveApproveEnable == 0)
				{
					Messagealert_.ShowMessage(tap2lblmessage, "UpdateEnable", 0);
					tap2div.Visible = true;
					tap2div.Attributes["class"] = "FailAlert";
					return;
				}
				else
				{
					tap2lblmessage.Visible = false;
				}
				if (tap2adjustremark.Text == "")
				{
					Messagealert_.ShowMessage(tap2lblmessage, "Remark", 0);
					tap2div.Visible = true;
					tap2div.Attributes["class"] = "FailAlert";
					return;
				}
				else
				{
					tap2lblmessage.Visible = false;
				}
				LeaveApplicationData objleaveData = new LeaveApplicationData();
				List<LeaveApplicationData> LstleaveApproveData = new List<LeaveApplicationData>();
				List<LeaveApplicationData> LstleaveRejectData = new List<LeaveApplicationData>();
				LeaveApplicationBO objleaveBO = new LeaveApplicationBO();
				IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
				
				string leavecategory = "";
				string leavetype = "";
				foreach (GridViewRow row in GvLeave2result.Rows)
				{

					Label tap2resultlblempID = (Label)row.Cells[0].FindControl("tap2resultlblempID");
					Label tap2resultlblleaveRecordID = (Label)row.Cells[0].FindControl("tap2resultlblleaveRecordID");
					Label tap2resultlblLeavedate = (Label)row.Cells[0].FindControl("tap2resultlblLeavedate");
					DropDownList tap2resultddlleavetype = (DropDownList)row.Cells[0].FindControl("tap2resultddlleavetype");
					DropDownList tap2resultddlleavecat = (DropDownList)row.Cells[0].FindControl("tap2resultddlleavecat");
					DropDownList tap2resultddllaction = (DropDownList)row.Cells[0].FindControl("tap2resultddllaction");
					LeaveApplicationData objData = new LeaveApplicationData();

					objData.LeaveRecordID = Convert.ToInt64(tap2resultlblleaveRecordID.Text);
					DateTime date = DateTime.Parse(tap2resultlblLeavedate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
					objData.date = date;
					objData.LeaveID = Convert.ToInt32(tap2resultddlleavetype.SelectedValue);
					objData.LeaveCategoryID = Convert.ToInt32(tap2resultddlleavecat.SelectedValue);
					objData.Status = Convert.ToInt32(tap2resultddllaction.SelectedValue);
					objData.LeaveEmployeeID = Convert.ToInt64(tap2resultlblempID.Text.Trim());
					if (tap2resultddllaction.SelectedValue == "1")
					{

						if (leavetype.Split(',').Contains(tap2resultddlleavetype.SelectedItem.Text.ToString()))
						{
						}
						else
						{
							
							leavetype = tap2resultddlleavetype.SelectedItem.Text.ToString() + ",";

						}
						if (leavecategory.Split(',').Contains(tap2resultddlleavecat.SelectedItem.Text.ToString()))
						{
						}
						else
						{
							leavecategory += tap2resultddlleavecat.SelectedItem.Text.ToString() + ",";
							

						}
					}

											
					LstleaveApproveData.Add(objData);
				

				}
				objleaveData.XMLadjustApproveleave = XmlConvertor.AdjustApproveleavetoXML(LstleaveApproveData).ToString();
				objleaveData.Remarks = tap2adjustremark.Text.Trim();
				objleaveData.LeaveRecordID = Convert.ToInt64(tap2lblleaveRecordID.Text.Trim());
				objleaveData.LeaveEmployeeID = Convert.ToInt64(tap2lblrequestedemployeeID.Text.Trim());
				objleaveData.EmployeeID = LogData.EmployeeID;
				if (leavetype.Length > 0)
				{
					
					objleaveData.LeaveAdjustedtypes = leavetype.Remove(leavetype.Length - 1);
				}
				else
				{					
					objleaveData.LeaveAdjustedtypes = "";
				}
				if (leavecategory.Length > 0)
				{
					objleaveData.LeaveAdjustcategory = leavecategory.Remove(leavecategory.Length - 1);
				}
				else 
				{
					objleaveData.LeaveAdjustcategory = "";
				}

				List<LeaveApplicationData> outmessage = objleaveBO.AdjustApproveleaveRecord(objleaveData);
				if (outmessage.Count > 0)
				{

					Messagealert_.ShowMessage(tap2lblmessage, outmessage[0].OutputMessage, outmessage[0].messagetype);
					if (outmessage[0].messagetype == 1)
					{
						tap2div.Attributes["class"] = "SucessAlert";

						List<LeaveApplicationData> lstLeaveRecord = objleaveBO.GetLeaveRecordDetails(objleaveData);
						if (lstLeaveRecord.Count > 0)
						{
							GvLeave2result.DataSource = lstLeaveRecord;
							GvLeave2result.DataBind();
							GvLeave2result.Visible = true;
							

						}
						
							
					}
					else
					{
						tap2div.Attributes["class"] = "FailAlert";

					}
					tap2div.Visible = true;

					return;
				}

				else
				{
					Messagealert_.ShowMessage(tap2lblmessage, "system", 0);
					tap2div.Attributes["class"] = "FailAlert";
					tap2div.Visible = true;
					return;
				}
					
			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
		}
	}
}