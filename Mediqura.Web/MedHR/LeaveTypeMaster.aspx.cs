using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedHrData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using Saplin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedHR
{
	public partial class LeaveTypeMaster : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{


		}
		private void bindgrid()
		{
			try
			{
				List<LeaveTypeData> lstLeaveType = GetLeaveType(0);
				if (lstLeaveType.Count > 0)
				{
					GvLeave.DataSource = lstLeaveType;
					GvLeave.DataBind();
					GvLeave.Visible = true;
					Messagealert_.ShowMessage(lblresult, "Total: " + lstLeaveType.Count + " Record found", 1);
					divmsg3.Attributes["class"] = "SucessAlert";
					divmsg3.Visible = true;
					btnupdate.Attributes.Remove("disabled");
				}
				else
				{
					GvLeave.DataSource = null;
					GvLeave.DataBind();
					btnupdate.Attributes["disabled"] = "disabled";
					GvLeave.Visible = true;
					lblresult.Visible = false;

				}
			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
		}
		private List<LeaveTypeData> GetLeaveType(int p)
		{
			LeaveTypeData objLeaveMSTData = new LeaveTypeData();
			LeaveTypeBO objLeaveMSTBO = new LeaveTypeBO();
			objLeaveMSTData.LeaveCode = txt_Leavecode.Text == "" ? "" : txt_Leavecode.Text;
			objLeaveMSTData.Leavedescp = txt_LeaveDescp.Text == "" ? "" : txt_LeaveDescp.Text;
			objLeaveMSTData.MaxLeaveMonth = Convert.ToInt32(txt_MaxLeaveMonth.Text == "" ? "0" : txt_MaxLeaveMonth.Text);
			objLeaveMSTData.MaxLeaveYear = Convert.ToInt32(txt_MaxLeaveYear.Text == "" ? "0" : txt_MaxLeaveYear.Text);
			objLeaveMSTData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
			return objLeaveMSTBO.SearchLeaveTypeDetails(objLeaveMSTData);
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
				if (txt_Leavecode.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "Code", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					txt_Leavecode.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (txt_LeaveDescp.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "Description", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					txt_LeaveDescp.Focus();
					return;
				}
				{
					lblmessage.Visible = false;
				}
				if (Convert.ToInt32(txt_MaxLeaveMonth.Text == "" ? "0" : txt_MaxLeaveMonth.Text) == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "Please enter max leave per month.", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					txt_Leavecode.Focus();
					return;
				}
				else
				{
					if (Convert.ToInt32(txt_MaxLeaveMonth.Text) > 31)
					{
						Messagealert_.ShowMessage(lblmessage, "Enter max leave per month is out of range.", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";
						txt_Leavecode.Focus();
						return;
					}
					else
					{

						lblmessage.Visible = false;
					}
				}
				if (Convert.ToInt32(txt_MaxLeaveYear.Text == "" ? "0" : txt_MaxLeaveYear.Text) == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "Please enter max leave per year.", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					txt_MaxLeaveYear.Focus();
					return;
				}
				else
				{
					if (Convert.ToInt32(txt_MaxLeaveMonth.Text) > Convert.ToInt32(txt_MaxLeaveYear.Text))
					{
						Messagealert_.ShowMessage(lblmessage, "Enter max leave per year cannot be less then max leave per month.", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";
						txt_Leavecode.Focus();
						return;
					}
					else if (Convert.ToInt32(txt_MaxLeaveYear.Text) > 255)
					{
						Messagealert_.ShowMessage(lblmessage, "Enter max leave per year is out of range.", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";
						txt_Leavecode.Focus();
						return;
					}
					else
					{

						lblmessage.Visible = false;
					}


				}
				LeaveTypeData objLeaveMSTData = new LeaveTypeData();
				LeaveTypeBO objLeaveMSTBO = new LeaveTypeBO();
				objLeaveMSTData.LeaveCode = txt_Leavecode.Text == "" ? "" : txt_Leavecode.Text;
				objLeaveMSTData.Leavedescp = txt_LeaveDescp.Text == "" ? "" : txt_LeaveDescp.Text;
				objLeaveMSTData.MaxLeaveMonth = Convert.ToInt32(txt_MaxLeaveMonth.Text == "" ? "0" : txt_MaxLeaveMonth.Text);
				objLeaveMSTData.MaxLeaveYear = Convert.ToInt32(txt_MaxLeaveYear.Text == "" ? "0" : txt_MaxLeaveYear.Text);
				objLeaveMSTData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
				objLeaveMSTData.EmployeeID = LogData.EmployeeID;
				objLeaveMSTData.HospitalID = LogData.HospitalID;
				objLeaveMSTData.FinancialYearID = LogData.FinancialYearID;
				objLeaveMSTData.ActionType = Enumaction.Insert;
				if (ViewState["ID"] != null)
				{
					if (LogData.UpdateEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";
						txt_Leavecode.Focus();
						return;
					}
					else
					{
						lblmessage.Visible = false;
					}
					objLeaveMSTData.ActionType = Enumaction.Update;
					objLeaveMSTData.LeaveID = Convert.ToInt32(ViewState["ID"].ToString());
				}
				int result = objLeaveMSTBO.UpdateLeaveTypeDetails(objLeaveMSTData);  // funtion at DAL
				if (result == 1 || result == 2)
				{
					lblmessage.Visible = true;
					Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
					div1.Visible = true;
					div1.Attributes["class"] = "SucessAlert";
					ViewState["ID"] = null;
					bindgrid();
				}
				else if (result == 5)
				{
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
				}
				else
					Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
		}

		protected void btnresets_Click(object sender, EventArgs e)
		{
			ViewState["ID"] = null;
			clearall();
			lblmessage.Visible = false;
			lblresult.Visible = false;
			btnupdate.Attributes["disabled"] = "disabled";
		}
		private void clearall()
		{
			txt_Leavecode.Text = "";
			txt_LeaveDescp.Text = "";
			txt_MaxLeaveMonth.Text = "";
			txt_MaxLeaveYear.Text = "";
			ddlstatus.SelectedIndex = 0;
			GvLeave.DataSource = null;
			GvLeave.DataBind();
			GvLeave.Visible = false;
		}
		protected void btnsearch_Click(object sender, EventArgs e)
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
			bindgrid();
		}
		protected void GVLeave_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			MasterLookupBO mstlookup = new MasterLookupBO();
			List<LookupItem> lookupemployeetype = mstlookup.GetLookupsList(LookupName.EmployeeType);
			List<LookupItem> lookupleavetype = mstlookup.GetLookupsList(LookupName.Leavetype);
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Label lbl_leaveforward = (Label)e.Row.FindControl("lbl_leaveforward");
				CheckBox chkforward = (CheckBox)e.Row.FindControl("chekboxleaveforward");
				Label lbl_leavecounttype = (Label)e.Row.FindControl("lbl_leavecounttype");
				DropDownList ddlleavecountDay = (DropDownList)e.Row.FindControl("ddlleavecountDay");
				Label lbl_leaveavailadvance = (Label)e.Row.FindControl("lbl_leaveavailadvance");
				CheckBox chekboxleaveavailadvance = (CheckBox)e.Row.FindControl("chekboxleaveavailadvance");
				DropDownCheckBoxes ddl_Eligibility = (DropDownCheckBoxes)e.Row.FindControl("ddl_Eligibility");
				Commonfunction.PopulateCheckbox(ddl_Eligibility, lookupemployeetype);
				DropDownCheckBoxes ddl_Combined = (DropDownCheckBoxes)e.Row.FindControl("ddl_Combined");
				Commonfunction.PopulateCheckbox(ddl_Combined, lookupleavetype);
				Label lbl_leaveeligible = (Label)e.Row.FindControl("lbl_leaveeligible");
				Label lbl_leaveHalfday = (Label)e.Row.FindControl("lbl_leaveHalfday");
				CheckBox chekboxleaveHalfday = (CheckBox)e.Row.FindControl("chekboxleaveHalfday");
				Label lbl_leavedocument = (Label)e.Row.FindControl("lbl_leavedocument");
				CheckBox chekboxleavedocument = (CheckBox)e.Row.FindControl("chekboxleavedocument");
				Label lbl_combinedleave = (Label)e.Row.FindControl("lbl_combinedleave");
				CheckBox chekboxcombinedleave = (CheckBox)e.Row.FindControl("chekboxcombinedleave");
				List<String> itememptype = lbl_leaveeligible.Text.Split(',').ToList<String>();
				List<String> itemleavetype = lbl_combinedleave.Text.Split(',').ToList<String>();
				foreach (ListItem li in ddl_Eligibility.Items)
				{
					for (int i = 0; i < itememptype.Count; i++)
					{
						if (li.Value == itememptype[i])
						{
							li.Selected = true;
						}
						
					}
				}
				foreach (ListItem li in ddl_Combined.Items)
				{
					for (int i = 0; i < itemleavetype.Count; i++)
					{
						if (li.Value == itemleavetype[i])
						{
							li.Selected = true;
						}

					}
				}
				if (Convert.ToInt32(lbl_leaveforward.Text) == 1)
				{
					chkforward.Checked = true;
				}
				else
				{
					chkforward.Checked = false;
				}
				if (Convert.ToInt32(lbl_leavecounttype.Text) == 1)
				{
					ddlleavecountDay.SelectedValue = "1";
				}
				else if (Convert.ToInt32(lbl_leavecounttype.Text) == 2)
				{
					ddlleavecountDay.SelectedValue = "2";
				}
				else
				{
					ddlleavecountDay.SelectedValue = "1";
				}
				if (Convert.ToInt32(lbl_leaveavailadvance.Text) == 1)
				{
					chekboxleaveavailadvance.Checked = true;
				}
				else
				{
					chekboxleaveavailadvance.Checked = false;
				}

				if (Convert.ToInt32(lbl_leaveHalfday.Text) == 1)
				{
					chekboxleaveHalfday.Checked = true;
				}
				else
				{
					chekboxleaveHalfday.Checked = false;
				}

				if (Convert.ToInt32(lbl_leavedocument.Text) == 1)
				{
					chekboxleavedocument.Checked = true;
				}
				else
				{
					chekboxleavedocument.Checked = false;
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
					LeaveTypeData objLeaveMSTData = new LeaveTypeData();
					LeaveTypeBO objLeaveMSTBO = new LeaveTypeBO();
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow pt = GvLeave.Rows[i];
					Label LeaveID = (Label)pt.Cells[0].FindControl("LeaveID");
					objLeaveMSTData.LeaveID = Convert.ToInt32(LeaveID.Text);
					objLeaveMSTData.ActionType = Enumaction.Select;

					List<LeaveTypeData> GetResult = objLeaveMSTBO.GetLeaveTypeDetailsByID(objLeaveMSTData);
					if (GetResult.Count > 0)
					{
						txt_Leavecode.Text = GetResult[0].LeaveCode;
						txt_LeaveDescp.Text = GetResult[0].Leavedescp;
						txt_MaxLeaveMonth.Text = GetResult[0].MaxLeaveMonth.ToString();
						txt_MaxLeaveYear.Text = GetResult[0].MaxLeaveYear.ToString();
						ViewState["ID"] = GetResult[0].LeaveID;
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
					LeaveTypeData objLeaveMSTData = new LeaveTypeData();
					LeaveTypeBO objLeaveMSTBO = new LeaveTypeBO();
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = GvLeave.Rows[i];
					Label LeaveID = (Label)gr.Cells[0].FindControl("LeaveID");
					objLeaveMSTData.LeaveID = Convert.ToInt32(LeaveID.Text);
					objLeaveMSTData.EmployeeID = LogData.EmployeeID;
					objLeaveMSTData.ActionType = Enumaction.Delete;
					TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
					txtremarks.Enabled = true;
					if (txtremarks.Text == "")
					{
						Messagealert_.ShowMessage(lblresult, "Remarks", 0);
						divmsg3.Visible = true;
						divmsg3.Attributes["class"] = "FailAlert";
						txtremarks.Focus();
						return;
					}
					else
					{
						objLeaveMSTData.Remarks = txtremarks.Text;
					}

					int Result = objLeaveMSTBO.DeleteLeaveTypeDetailsByID(objLeaveMSTData);
					if (Result == 1)
					{
						Messagealert_.ShowMessage(lblmessage, "delete", 1);
						div1.Visible = true;
						div1.Attributes["class"] = "SucessAlert";
						bindgrid();
					}
					else
					{
						Messagealert_.ShowMessage(lblmessage, "system", 0);
						div1.Visible = true;
						div1.Attributes["class"] = "FailAlert";

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

		protected void GvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GvLeave.PageIndex = e.NewPageIndex;
			bindgrid();
		}
		protected void btnupdate_Click(object sender, EventArgs e)
		{

			List<LeaveTypeData> LstLeaveMSTData = new List<LeaveTypeData>();
			LeaveTypeData objLeaveMSTData = new LeaveTypeData();
			LeaveTypeBO objLeaveMSTBO = new LeaveTypeBO();
			int index = 0;
			//get all the record from the gridview
			try
			{
				foreach (GridViewRow row in GvLeave.Rows)
				{
					Label LeaveID = (Label)row.FindControl("LeaveID");
					CheckBox chekboxleaveforward = (CheckBox)row.FindControl("chekboxleaveforward");
					DropDownList ddlleavecountDay = (DropDownList)row.FindControl("ddlleavecountDay");
					CheckBox chekboxleaveavailadvance = (CheckBox)row.FindControl("chekboxleaveavailadvance");
					CheckBox chekboxleaveHalfday = (CheckBox)row.FindControl("chekboxleaveHalfday");
					CheckBox chekboxleavedocument = (CheckBox)row.FindControl("chekboxleavedocument");
					DropDownCheckBoxes ddl_Eligibility = (DropDownCheckBoxes)row.FindControl("ddl_Eligibility");
					DropDownCheckBoxes ddl_Combined = (DropDownCheckBoxes)row.FindControl("ddl_Combined");
					LeaveTypeData objLeaveData = new LeaveTypeData();

					objLeaveData.LeaveID = Convert.ToInt32(LeaveID.Text);
					objLeaveData.Leavecarriedforward = Convert.ToInt32(chekboxleaveforward.Checked ? 1 : 0);
					objLeaveData.LeaveCountID = Convert.ToInt32(ddlleavecountDay.SelectedValue);
					objLeaveData.LeaveAvailinAdvance = Convert.ToInt32(chekboxleaveavailadvance.Checked ? 1 : 0);
					objLeaveData.leaveHalfday = Convert.ToInt32(chekboxleaveHalfday.Checked ? 1 : 0);
					objLeaveData.LeaveDocument = Convert.ToInt32(chekboxleavedocument.Checked ? 1 : 0);
					String leavecombined = "";
					String LeaveEligible = "";
					foreach (ListItem li in ddl_Eligibility.Items)
					{
						if (li.Selected)
						{
						 LeaveEligible += li.Value.ToString() + ',';
						}
						
					}
					if (LeaveEligible.Length > 0)
					{
						objLeaveData.LeaveEligible = LeaveEligible.Remove(LeaveEligible.Length - 1);
					}
					else
					{
						objLeaveData.LeaveEligible = "";
					}
					foreach (ListItem li in ddl_Combined.Items)
					{
						if (li.Selected)
						{
							leavecombined += li.Value.ToString() + ',';
						}

					}
					if (leavecombined.Length > 0)
					{
						objLeaveData.leavecombined = leavecombined.Remove(leavecombined.Length - 1);
					}
					else
					{
						objLeaveData.leavecombined = "";
					}
						LstLeaveMSTData.Add(objLeaveData);
				}
				objLeaveMSTData.XMLLeavecarriedforward = XmlConvertor.CarriedforwardleavetoXML(LstLeaveMSTData).ToString();

				int results = objLeaveMSTBO.UpdateLeaveDetailsList(objLeaveMSTData);
				if (results == 1)
				{
					bindgrid();
					btnupdate.Attributes["disable"] = "disabled";
					lblmessage.Visible = true;
					Messagealert_.ShowMessage(lblmessage, "update", 1);
					div1.Visible = true;
					div1.Attributes["class"] = "SucessAlert";

				}
				else
				{
					btnupdate.Attributes.Remove("disable");
					Messagealert_.ShowMessage(lblmessage, "Error", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
				}
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
		}

	}
}