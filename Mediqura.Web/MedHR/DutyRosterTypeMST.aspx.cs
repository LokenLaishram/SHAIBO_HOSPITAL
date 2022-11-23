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
	public partial class DutyRosterTypeMST : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				{
					lblmessage.Visible = false;
					Clearshift_I_Summer();
					Clearshift_I_Winter();
					Clearshift_II_Summer();
					Clearshift_II_Winter();
					MasterLookupBO mstlookup = new MasterLookupBO();
					Commonfunction.PopulateDdl(ddl_shifttype, mstlookup.GetLookupsList(LookupName.ShiftType));
					Commonfunction.PopulateDdl(ddl_shifttype_II, mstlookup.GetLookupsList(LookupName.ShiftType));
					Commonfunction.PopulateDdl(ddl_roster_II, mstlookup.GetLookupsList(LookupName.Roster));
					ddl_shifttype.SelectedIndex = 1;
					ddl_shifttype_II.SelectedIndex = 0;
					ddl_roster_II.SelectedIndex = 0;
					if (ddl_shifttype.SelectedValue == "1")
					{
						divclear();
					}
					bindgrid();

				}
			}

		}

		protected void ddl_shifttype_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddl_shifttype.SelectedValue == "1")
			{
				divsummer1.Visible = true;
				divsummer2.Visible = false;
				divwinter1.Visible = true;
				divwinter2.Visible = false;
				Clearshift_II_Summer();
				Clearshift_II_Winter();


			}
			else
			{
				divsummer1.Visible = true;
				divsummer2.Visible = true;
				divwinter1.Visible = true;
				divwinter2.Visible = true;

			}

		}
		protected void checksame_CheckedChanged(object sender, EventArgs e)
		{

			if (checksame.Checked)
			{
				txt_Shift_I_WinterStartTime.ReadOnly = true;
				txt_Shift_I_WinterEndTime.ReadOnly = true;
				ddl_Shift_I_WinterNextDay.Attributes["disabled"] = "disabled";
				txt_Shift_I_WinterInrelaxation.ReadOnly = true;
				txt_Shift_I_WinterOutrelaxtion.ReadOnly = true;
				txt_Shift_I_WinterStartTime.Text = txt_Shift_I_SummerStartTime.Text;
				txt_Shift_I_WinterEndTime.Text = txt_Shift_I_SummerEndTime.Text;
				ddl_Shift_I_WinterNextDay.SelectedValue = ddl_Shift_I_SummerNextDay.SelectedValue;
				txt_Shift_I_WinterInrelaxation.Text = txt_Shift_I_SummerInrelaxation.Text;
				txt_Shift_I_WinterOutrelaxtion.Text = txt_Shift_I_SummerOutrelaxation.Text;
				if (ddl_shifttype.SelectedValue == "2" || ddl_shifttype.SelectedValue == "3")
				{
					txt_Shift_II_WinterStartTime.ReadOnly = true;
					txt_Shift_II_WinterEndTime.ReadOnly = true;
					ddl_Shift_II_WinterNextDay.Attributes["disabled"] = "disabled";
					txt_Shift_II_WinterInrelaxation.ReadOnly = true;
					txt_Shift_II_WinterOutrelaxtion.ReadOnly = true;
					txt_Shift_II_WinterStartTime.Text = txt_Shift_II_SummerStartTime.Text;
					txt_Shift_II_WinterEndTime.Text = txt_Shift_II_SummerEndTime.Text;
					ddl_Shift_II_WinterNextDay.SelectedValue = ddl_Shift_II_SummerNextDay.SelectedValue;
					txt_Shift_II_WinterInrelaxation.Text = txt_Shift_II_SummerInrelaxation.Text;
					txt_Shift_II_WinterOutrelaxtion.Text = txt_Shift_II_SummerOutrelaxation.Text;
				}

			}
			else
			{
				txt_Shift_I_WinterStartTime.ReadOnly = false;
				txt_Shift_I_WinterEndTime.ReadOnly = false;
				ddl_Shift_I_WinterNextDay.Attributes.Remove("disabled");
				txt_Shift_I_WinterInrelaxation.ReadOnly = false;
				txt_Shift_I_WinterOutrelaxtion.ReadOnly = false;
				Clearshift_I_Winter();
				if (ddl_shifttype.SelectedValue == "2" || ddl_shifttype.SelectedValue == "3")
				{
					txt_Shift_II_WinterStartTime.ReadOnly = false;
					txt_Shift_II_WinterEndTime.ReadOnly = false;
					ddl_Shift_II_WinterNextDay.Attributes.Remove("disabled");
					txt_Shift_II_WinterInrelaxation.ReadOnly = false;
					txt_Shift_II_WinterOutrelaxtion.ReadOnly = false;
					Clearshift_II_Winter();
				}

			}

		}
		private void divclear()
		{
			divsummer1.Visible = true;
			divsummer2.Visible = false;
			divwinter1.Visible = true;
			divwinter2.Visible = false;
		}
		private void Clearshift_I_Summer()
		{
			txt_Shift_I_SummerStartTime.Text = "12:00 AM";
			txt_Shift_I_SummerEndTime.Text = "11:59 PM";
			ddl_Shift_I_SummerNextDay.SelectedValue = "0";
			txt_Shift_I_SummerInrelaxation.Text = "00:00:00";
			txt_Shift_I_SummerOutrelaxation.Text = "00:00:00";
		}
		private void Clearshift_II_Summer()
		{
			txt_Shift_II_SummerStartTime.Text = "12:00 AM";
			txt_Shift_II_SummerEndTime.Text = "11:59 PM";
			ddl_Shift_II_SummerNextDay.SelectedValue = "0";
			txt_Shift_II_SummerInrelaxation.Text = "00:00:00";
			txt_Shift_II_SummerOutrelaxation.Text = "00:00:00";
		}
		private void Clearshift_I_Winter()
		{
			txt_Shift_I_WinterStartTime.Text = "12:00 AM";
			txt_Shift_I_WinterEndTime.Text = "11:59 PM";
			ddl_Shift_I_WinterNextDay.SelectedValue = "0";
			txt_Shift_I_WinterInrelaxation.Text = "00:00:00";
			txt_Shift_I_WinterOutrelaxtion.Text = "00:00:00";
		}
		private void Clearshift_II_Winter()
		{
			txt_Shift_II_WinterStartTime.Text = "12:00 AM";
			txt_Shift_II_WinterEndTime.Text = "11:59 PM";
			ddl_Shift_II_WinterNextDay.SelectedValue = "0";
			txt_Shift_II_WinterInrelaxation.Text = "00:00:00";
			txt_Shift_II_WinterOutrelaxtion.Text = "00:00:00";
		}
		protected void btnreset_Click(object sender, EventArgs e)
		{
			txtcode.Text = "";
			txtdescp.Text = "";
			ddl_shifttype.SelectedValue = "1";
			ddl_status.SelectedIndex = 0;
			divclear();
			txt_Shift_I_WinterStartTime.ReadOnly = false;
			txt_Shift_I_WinterEndTime.ReadOnly = false;
			ddl_Shift_I_WinterNextDay.Attributes.Remove("disabled");
			txt_Shift_I_WinterInrelaxation.ReadOnly = false;
			txt_Shift_I_WinterOutrelaxtion.ReadOnly = false;
			txt_Shift_II_WinterStartTime.ReadOnly = false;
			txt_Shift_II_WinterEndTime.ReadOnly = false;
			ddl_Shift_II_WinterNextDay.Attributes.Remove("disabled");
			txt_Shift_II_WinterInrelaxation.ReadOnly = false;
			txt_Shift_II_WinterOutrelaxtion.ReadOnly = false;
			Clearshift_I_Summer();
			Clearshift_II_Summer();
			Clearshift_I_Winter();
			Clearshift_II_Winter();
			lblmessage.Visible = false;
			lblmessage.Text = "";
			divmsg1.Visible = false;
			btnsave.Attributes.Remove("disabled");
			checksame.Checked = false;
		}
		protected void btnsearch_II_Click(object sender, EventArgs e)
		{
			bindgrid();
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

				if (txtcode.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "Code", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txtcode.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (txtdescp.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "Description", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txtdescp.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}

				if (ddl_shifttype.SelectedIndex == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "ShiftType", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					ddl_shifttype.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}

				if (txt_Shift_I_SummerStartTime.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "SummerstartTime", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_Shift_I_SummerStartTime.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (txt_Shift_I_SummerEndTime.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "SummerendTime", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_Shift_I_SummerEndTime.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (Convert.ToInt32(ddl_shifttype.SelectedValue) > 1 && txt_Shift_II_SummerStartTime.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "SummerstartTime", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_Shift_II_SummerStartTime.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (Convert.ToInt32(ddl_shifttype.SelectedValue) > 1 && txt_Shift_II_SummerEndTime.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "SummerendTime", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_Shift_II_SummerEndTime.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (Convert.ToInt32(ddl_shifttype.SelectedValue) > 1 && ddl_Shift_I_SummerNextDay.SelectedValue == "1" && ddl_Shift_II_SummerNextDay.SelectedValue == "1")
				{
					Messagealert_.ShowMessage(lblmessage, "Rosternextday", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					ddl_Shift_I_SummerNextDay.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (txt_Shift_I_WinterStartTime.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "WinterstartTime", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_Shift_I_WinterStartTime.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (txt_Shift_I_WinterEndTime.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "WinterendTime", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_Shift_I_WinterEndTime.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (Convert.ToInt32(ddl_shifttype.SelectedValue) > 1 && txt_Shift_II_WinterStartTime.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "WinterstartTime", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_Shift_II_WinterStartTime.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (Convert.ToInt32(ddl_shifttype.SelectedValue) > 1 && txt_Shift_II_WinterEndTime.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "WinterendTime", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_Shift_II_WinterEndTime.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (Convert.ToInt32(ddl_shifttype.SelectedValue) > 1 && ddl_Shift_I_WinterNextDay.SelectedValue == "1" && ddl_Shift_II_WinterNextDay.SelectedValue == "1")
				{
					Messagealert_.ShowMessage(lblmessage, "Rosternextday", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					ddl_Shift_I_WinterNextDay.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
				DutyRosterTypeData objdutyData = new DutyRosterTypeData();
				DutyRosterTypeBO objdutyBO = new DutyRosterTypeBO();
				objdutyData.RosterCode = txtcode.Text.Trim();
				objdutyData.RosterDescp = txtdescp.Text.Trim();
				objdutyData.IsActive = ddl_status.SelectedValue == "2" ? false : true;
				objdutyData.EmployeeID = LogData.EmployeeID;
				objdutyData.HospitalID = LogData.HospitalID;
				objdutyData.ActionType = Enumaction.Insert;
				objdutyData.ShiftTypeID = Convert.ToInt32(ddl_shifttype.SelectedValue == "" ? "0" : ddl_shifttype.SelectedValue);
				DateTime Shift_I_SummerStartTime = DateTime.Today.Add(DateTime.Parse(txt_Shift_I_SummerStartTime.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_I_SummerStartTime = Shift_I_SummerStartTime;
				DateTime Shift_II_SummerStartTime = DateTime.Today.Add(DateTime.Parse(txt_Shift_II_SummerStartTime.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_II_SummerStartTime = Shift_II_SummerStartTime;
				DateTime Shift_I_SummerEndTime = DateTime.Today.Add(DateTime.Parse(txt_Shift_I_SummerEndTime.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_I_SummerEndTime = Shift_I_SummerEndTime;
				DateTime Shift_II_SummerEndTime = DateTime.Today.Add(DateTime.Parse(txt_Shift_II_SummerEndTime.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_II_SummerEndTime = Shift_II_SummerEndTime;
				objdutyData.Shift_I_SummerNextDay = Convert.ToInt32(ddl_Shift_I_SummerNextDay.SelectedValue);
				objdutyData.Shift_II_SummerNextDay = Convert.ToInt32(ddl_Shift_II_SummerNextDay.SelectedValue);
				DateTime Shift_I_SummerInrelaxation = DateTime.Today.Add(DateTime.Parse(txt_Shift_I_SummerInrelaxation.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_I_SummerInrelaxation = Shift_I_SummerInrelaxation;
				DateTime Shift_II_SummerInrelaxation = DateTime.Today.Add(DateTime.Parse(txt_Shift_II_SummerInrelaxation.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_II_SummerInrelaxation = Shift_II_SummerInrelaxation;
				DateTime Shift_I_SummerOutrelaxation = DateTime.Today.Add(DateTime.Parse(txt_Shift_I_SummerOutrelaxation.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_I_SummerOutrelaxation = Shift_I_SummerOutrelaxation;
				DateTime Shift_II_SummerOutrelaxation = DateTime.Today.Add(DateTime.Parse(txt_Shift_II_SummerOutrelaxation.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_II_SummerOutrelaxation = Shift_II_SummerOutrelaxation;
				DateTime Shift_I_WinterStartTime = DateTime.Today.Add(DateTime.Parse(txt_Shift_I_WinterStartTime.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_I_WinterStartTime = Shift_I_WinterStartTime;
				DateTime Shift_II_WinterStartTime = DateTime.Today.Add(DateTime.Parse(txt_Shift_II_WinterStartTime.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_II_WinterStartTime = Shift_II_WinterStartTime;
				DateTime Shift_I_WinterEndTime = DateTime.Today.Add(DateTime.Parse(txt_Shift_I_WinterEndTime.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_I_WinterEndTime = Shift_I_WinterEndTime;
				DateTime Shift_II_WinterEndTime = DateTime.Today.Add(DateTime.Parse(txt_Shift_II_WinterEndTime.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_II_WinterEndTime = Shift_II_WinterEndTime;
				objdutyData.Shift_I_WinterNextDay = Convert.ToInt32(ddl_Shift_I_WinterNextDay.SelectedValue);
				objdutyData.Shift_II_WinterNextDay = Convert.ToInt32(ddl_Shift_II_WinterNextDay.SelectedValue);
				DateTime Shift_I_WinterInrelaxation = DateTime.Today.Add(DateTime.Parse(txt_Shift_I_WinterInrelaxation.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_I_WinterInrelaxation = Shift_I_WinterInrelaxation;
				DateTime Shift_II_WinterInrelaxation = DateTime.Today.Add(DateTime.Parse(txt_Shift_II_WinterInrelaxation.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_II_WinterInrelaxation = Shift_II_WinterInrelaxation;
				DateTime Shift_I_WinterOutrelaxation = DateTime.Today.Add(DateTime.Parse(txt_Shift_I_WinterOutrelaxtion.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_I_WinterOutrelaxation = Shift_I_WinterOutrelaxation;
				DateTime Shift_II_WinterOutrelaxation = DateTime.Today.Add(DateTime.Parse(txt_Shift_II_WinterOutrelaxtion.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault).TimeOfDay);
				objdutyData.Shift_II_WinterOutrelaxation = Shift_II_WinterOutrelaxation;
				if (ViewState["RosterID"] != null)
				{
					if (LogData.UpdateEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
						divmsg1.Visible = true;
						divmsg1.Attributes["class"] = "FailAlert";
						return;
					}
					else
					{
						lblmessage.Visible = false;
						objdutyData.ActionType = Enumaction.Update;
						objdutyData.RosterID = Convert.ToInt64(ViewState["RosterID"].ToString() == "" ? "0" : ViewState["RosterID"].ToString());
					}
				}

				int results = objdutyBO.UpdateDutyRosterType(objdutyData);
				if (results == 1)
				{
					Messagealert_.ShowMessage(lblmessage, "save", 1);
					divmsg1.Attributes["class"] = "SucessAlert";
					divmsg1.Visible = true;
					txtcode.Focus();
				}
				else if (results == 2)
				{
					Messagealert_.ShowMessage(lblmessage, "update", 1);
					divmsg1.Attributes["class"] = "SucessAlert";
					divmsg1.Visible = true;
					txtcode.Focus();
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "system", 0);
					divmsg1.Attributes["class"] = "FailAlert";
					divmsg1.Visible = true;
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
		private void bindgrid()
		{
			DutyRosterTypeData objdutyData = new DutyRosterTypeData();
			List<DutyRosterTypeData> LstdutyData = new List<DutyRosterTypeData>();
			DutyRosterTypeBO objdutyBO = new DutyRosterTypeBO();
			objdutyData.RosterID = Convert.ToInt32(ddl_roster_II.SelectedValue == "" ? "0" : ddl_roster_II.SelectedValue);
			objdutyData.ShiftTypeID = Convert.ToInt32(ddl_shifttype_II.SelectedValue == "" ? "0" : ddl_shifttype_II.SelectedValue);
			objdutyData.IsActive = ddl_status_II.SelectedValue == "1" ? true : false;
			LstdutyData = objdutyBO.SearchRosterType(objdutyData);
			if (LstdutyData.Count > 0)
			{
				divmsg3.Visible = true;
				Messagealert_.ShowMessage(lblresult, "Total:" + LstdutyData.Count + " Record(s) found.", 1);
				divmsg3.Attributes["class"] = "SucessAlert";
				GvRosterList.DataSource = LstdutyData;
				GvRosterList.DataBind();
			}
			else
			{
				divmsg3.Visible = false;
				GvRosterList.DataSource = null;
				GvRosterList.DataBind();
			}
		}
		protected void btnreset_II_Click(object sender, EventArgs e)
		{
			ddl_roster_II.SelectedIndex = 0;
			ddl_shifttype_II.SelectedIndex = 0;
			bindgrid();
		}
		protected void GvRosterList_RowDataBound(object sender, GridViewRowEventArgs e)
		{

			if (e.Row.RowType == DataControlRowType.Header)
			{
				GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
				TableHeaderCell cell = new TableHeaderCell();
				cell.ColumnSpan = 2;

				row.Controls.Add(cell);

				cell = new TableHeaderCell();

				cell.Text = "SUMMER";
				cell.Font.Bold = true;
				cell.Attributes.Add("Style", "text-align:center;");
				cell.ColumnSpan = 5;
				row.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.Text = "WINTER";
				cell.ColumnSpan = 5;
				cell.Font.Bold = true;
				cell.Attributes.Add("Style", "text-align:center;");
				row.Controls.Add(cell);
				cell = new TableHeaderCell();
				cell.ColumnSpan = 2;

				row.Controls.Add(cell);
				GvRosterList.Controls[0].Controls.AddAt(0, row);
			}

		}
		protected void GvRosterList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				if (e.CommandName == "Edits")
				{
					if (LogData.EditEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage_II, "EditEnable", 0);
						div2.Visible = true;
						div2.Attributes["class"] = "FailAlert";
						return;
					}
					else
					{
						lblmessage.Visible = false;
					}
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = GvRosterList.Rows[i];
					Label lblID = (Label)gr.Cells[0].FindControl("lblID");
					Int64 RosterID = Convert.ToInt64(lblID.Text);
					DutyRosterTypeData objdata = new DutyRosterTypeData();
					DutyRosterTypeBO objBO = new DutyRosterTypeBO();
					objdata.RosterID = RosterID;
					objdata.EmployeeID = LogData.EmployeeID;
					List<DutyRosterTypeData> lstrosterdetails = new List<DutyRosterTypeData>();

					lstrosterdetails = objBO.GetRosterDetailsByID(objdata);
					if (lstrosterdetails.Count > 0)
					{

						lblmessage.Visible = false;
						MasterLookupBO mstlookup = new MasterLookupBO();
						txtcode.Text = lstrosterdetails[0].RosterCode.ToString();
						txtdescp.Text = lstrosterdetails[0].RosterDescp.ToString();
						ddl_shifttype.SelectedValue = lstrosterdetails[0].ShiftTypeID.ToString();
						txt_Shift_I_SummerStartTime.Text = lstrosterdetails[0].Shift_I_SummerStartTime.ToString("HH:mm tt");
						txt_Shift_II_SummerStartTime.Text = lstrosterdetails[0].Shift_II_SummerStartTime.ToString("HH:mm tt");
						txt_Shift_I_WinterStartTime.Text = lstrosterdetails[0].Shift_I_WinterStartTime.ToString("HH:mm tt");
						txt_Shift_II_WinterStartTime.Text = lstrosterdetails[0].Shift_II_WinterStartTime.ToString("HH:mm tt");
						txt_Shift_I_SummerEndTime.Text = lstrosterdetails[0].Shift_I_SummerEndTime.ToString("HH:mm tt");
						txt_Shift_II_SummerEndTime.Text = lstrosterdetails[0].Shift_II_SummerEndTime.ToString("HH:mm tt");
						txt_Shift_I_WinterEndTime.Text = lstrosterdetails[0].Shift_I_WinterEndTime.ToString("HH:mm tt");
						txt_Shift_II_WinterEndTime.Text = lstrosterdetails[0].Shift_II_WinterEndTime.ToString("HH:mm tt");
						ddl_Shift_I_SummerNextDay.SelectedValue = lstrosterdetails[0].Shift_I_SummerNextDay.ToString();
						ddl_Shift_II_SummerNextDay.SelectedValue = lstrosterdetails[0].Shift_II_SummerNextDay.ToString();
						ddl_Shift_I_WinterNextDay.SelectedValue = lstrosterdetails[0].Shift_I_WinterNextDay.ToString();
						ddl_Shift_II_WinterNextDay.SelectedValue = lstrosterdetails[0].Shift_II_WinterNextDay.ToString();
						txt_Shift_I_SummerInrelaxation.Text = lstrosterdetails[0].Shift_I_SummerInrelaxation.ToString("HH:mm:ss");
						txt_Shift_II_SummerInrelaxation.Text = lstrosterdetails[0].Shift_II_SummerInrelaxation.ToString("HH:mm:ss");
						txt_Shift_I_WinterInrelaxation.Text = lstrosterdetails[0].Shift_I_WinterInrelaxation.ToString("HH:mm:ss");
						txt_Shift_II_WinterInrelaxation.Text = lstrosterdetails[0].Shift_II_WinterInrelaxation.ToString("HH:mm:ss");
						txt_Shift_I_SummerOutrelaxation.Text = lstrosterdetails[0].Shift_I_SummerOutrelaxation.ToString("HH:mm:ss");
						txt_Shift_II_SummerOutrelaxation.Text = lstrosterdetails[0].Shift_II_SummerOutrelaxation.ToString("HH:mm:ss");
						txt_Shift_I_WinterOutrelaxtion.Text = lstrosterdetails[0].Shift_I_WinterOutrelaxation.ToString("HH:mm:ss");
						txt_Shift_II_WinterOutrelaxtion.Text = lstrosterdetails[0].Shift_II_WinterOutrelaxation.ToString("HH:mm:ss");
						ViewState["RosterID"] = lstrosterdetails[0].RosterID.ToString();
						if (lstrosterdetails[0].ShiftTypeID.ToString() == "1")
						{
							divsummer1.Visible = true;
							divsummer2.Visible = false;
							divwinter1.Visible = true;
							divwinter2.Visible = false;
							Clearshift_II_Summer();
							Clearshift_II_Winter();
						}
						else
						{
							divsummer1.Visible = true;
							divsummer2.Visible = true;
							divwinter1.Visible = true;
							divwinter2.Visible = true;
						}
						tabcontaRoster.ActiveTabIndex = 0;

					}
				}

				if (e.CommandName == "Deletes")
				{
					if (LogData.DeleteEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage_II, "DeleteEnable", 0);
						div2.Visible = true;
						div2.Attributes["class"] = "FailAlert";
						return;
					}
					else
					{
						lblmessage_II.Visible = false;
					}
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = GvRosterList.Rows[i];
					Label lblID = (Label)gr.Cells[0].FindControl("lblID");
					Int64 RosterID = Convert.ToInt64(lblID.Text);
					DutyRosterTypeData objdata = new DutyRosterTypeData();
					DutyRosterTypeBO objBO = new DutyRosterTypeBO();
					objdata.RosterID = RosterID;
					objdata.Remarks = "deleted";
					objdata.EmployeeID = LogData.EmployeeID;
					int Result = objBO.DeleteRosterDetailsByID(objdata);

					if (Result == 1)
					{
						Messagealert_.ShowMessage(lblmessage_II, "delete", 1);
						div2.Attributes["class"] = "SucessAlert";
						div2.Visible = true;
						bindgrid();
						return;
					}

					else
					{
						Messagealert_.ShowMessage(lblmessage, "system", 0);
						div2.Attributes["class"] = "FailAlert";
						div2.Visible = true;

						return;
					}

				}

			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
				div2.Attributes["class"] = "FailAlert";
				div2.Visible = true;
				return;
			}
		}
	}

}