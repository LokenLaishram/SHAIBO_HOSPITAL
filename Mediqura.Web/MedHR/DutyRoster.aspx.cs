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
    public partial class DutyRoster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
				MasterLookupBO mstlookup = new MasterLookupBO();
				Commonfunction.PopulateDdl(ddl_Department, mstlookup.GetLookupsList(LookupName.Department));
				Commonfunction.PopulateDdl(ddl_Month, mstlookup.GetLookupsList(LookupName.month));
				txt_year.Text = DateTime.Now.Year.ToString();
			}

        }
		protected void btnsearch_Click(object sender, EventArgs e)
		{
			if (ddl_Department.SelectedIndex == 0)

			{
				Messagealert_.ShowMessage(lblmessage, "Department", 0);
				divmsg1.Visible = true;
				divmsg1.Attributes["class"] = "FailAlert";
				ddl_Department.Focus();
				return;
			}
			else
			{
				lblmessage.Visible = false;
			}

			if (ddl_Month.SelectedIndex==0)
			{
				Messagealert_.ShowMessage(lblmessage, "Months", 0);
				divmsg1.Visible = true;
				divmsg1.Attributes["class"] = "FailAlert";
				ddl_Month.Focus();
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
		private void bindgrid()
		{
			lblmessage.Text = "";
			lblmessage.Visible = false;
			DutyRosterData objdutyData = new DutyRosterData();
			List<DutyRosterData> LstdutyData = new List<DutyRosterData>();
			DutyRosterBO objdutyBO = new DutyRosterBO();
			objdutyData.DepartmentID = Convert.ToInt32(ddl_Department.SelectedValue == "" ? "0" : ddl_Department.SelectedValue);
			objdutyData.Month = Convert.ToInt32(ddl_Month.SelectedValue == "" ? "0" : ddl_Month.SelectedValue);
			objdutyData.Year = Convert.ToInt32(txt_year.Text);
			LstdutyData = objdutyBO.SearchDutyRoster(objdutyData);
			if (LstdutyData.Count > 0)
			{
				divmsg3.Visible = true;
				Messagealert_.ShowMessage(lblresult, "Total:" + (LstdutyData.Count-1) + " Record(s) found.", 1);
				divmsg3.Attributes["class"] = "SucessAlert";
				GvRosterList.DataSource = LstdutyData;
				GvRosterList.DataBind();
				btnsave.Attributes.Remove("disabled");
			}
			else
			{
				divmsg3.Visible = false;
				GvRosterList.DataSource = null;
				GvRosterList.DataBind();
				btnsave.Attributes["disabled"] = "disabled";
			}
		}
		protected string checkifempexists(string empname)
		{
			string name = (string)ViewState["EmpName"];
			if (name == empname)
			{
				return string.Empty;

			}
			else
			{
			
				ViewState["EmpName"] = empname;
				return empname;
			}
		}
		protected void GvRosterList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			MasterLookupBO mstlookup = new MasterLookupBO();
			List<LookupItem> lookuproster = mstlookup.GetLookupsList(LookupName.Roster);


			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Label lblheading = (Label)e.Row.FindControl("lblheading");
				Label lblempname = (Label)e.Row.FindControl("lblempname");
				Label lbldepartment = (Label)e.Row.FindControl("lbldepartment");
				Label lblempID = (Label)e.Row.FindControl("lblempID");
				Label lblyear = (Label)e.Row.FindControl("lblyear");
				Label lblSeasonID = (Label)e.Row.FindControl("lblSeasonID");
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
				DropDownList ddl_Rosterdate_1 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_1");
				Commonfunction.PopulateDdl(ddl_Rosterdate_1, lookuproster);
				DropDownList ddl_Rosterdate_2 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_2");
				Commonfunction.PopulateDdl(ddl_Rosterdate_2, lookuproster);
				DropDownList ddl_Rosterdate_3 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_3");
				Commonfunction.PopulateDdl(ddl_Rosterdate_3, lookuproster);
				DropDownList ddl_Rosterdate_4 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_4");
				Commonfunction.PopulateDdl(ddl_Rosterdate_4, lookuproster);
				DropDownList ddl_Rosterdate_5 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_5");
				Commonfunction.PopulateDdl(ddl_Rosterdate_5, lookuproster);
				DropDownList ddl_Rosterdate_6 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_6");
				Commonfunction.PopulateDdl(ddl_Rosterdate_6, lookuproster);
				DropDownList ddl_Rosterdate_7 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_7");
				Commonfunction.PopulateDdl(ddl_Rosterdate_7, lookuproster);
				DropDownList ddl_Rosterdate_8 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_8");
				Commonfunction.PopulateDdl(ddl_Rosterdate_8, lookuproster);
				DropDownList ddl_Rosterdate_9 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_9");
				Commonfunction.PopulateDdl(ddl_Rosterdate_9, lookuproster);
				DropDownList ddl_Rosterdate_10 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_10");
				Commonfunction.PopulateDdl(ddl_Rosterdate_10, lookuproster);
				DropDownList ddl_Rosterdate_11 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_11");
				Commonfunction.PopulateDdl(ddl_Rosterdate_11, lookuproster);
				DropDownList ddl_Rosterdate_12 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_12");
				Commonfunction.PopulateDdl(ddl_Rosterdate_12, lookuproster);
				DropDownList ddl_Rosterdate_13 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_13");
				Commonfunction.PopulateDdl(ddl_Rosterdate_13, lookuproster);
				DropDownList ddl_Rosterdate_14 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_14");
				Commonfunction.PopulateDdl(ddl_Rosterdate_14, lookuproster);
				DropDownList ddl_Rosterdate_15 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_15");
				Commonfunction.PopulateDdl(ddl_Rosterdate_15, lookuproster);
				DropDownList ddl_Rosterdate_16 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_16");
				Commonfunction.PopulateDdl(ddl_Rosterdate_16, lookuproster);
				DropDownList ddl_Rosterdate_17 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_17");
				Commonfunction.PopulateDdl(ddl_Rosterdate_17, lookuproster);
				DropDownList ddl_Rosterdate_18 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_18");
				Commonfunction.PopulateDdl(ddl_Rosterdate_18, lookuproster);
				DropDownList ddl_Rosterdate_19 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_19");
				Commonfunction.PopulateDdl(ddl_Rosterdate_19, lookuproster);
				DropDownList ddl_Rosterdate_20 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_20");
				Commonfunction.PopulateDdl(ddl_Rosterdate_20, lookuproster);
				DropDownList ddl_Rosterdate_21 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_21");
				Commonfunction.PopulateDdl(ddl_Rosterdate_21, lookuproster);
				DropDownList ddl_Rosterdate_22 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_22");
				Commonfunction.PopulateDdl(ddl_Rosterdate_22, lookuproster);
				DropDownList ddl_Rosterdate_23 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_23");
				Commonfunction.PopulateDdl(ddl_Rosterdate_23, lookuproster);
				DropDownList ddl_Rosterdate_24 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_24");
				Commonfunction.PopulateDdl(ddl_Rosterdate_24, lookuproster);
				DropDownList ddl_Rosterdate_25 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_25");
				Commonfunction.PopulateDdl(ddl_Rosterdate_25, lookuproster);
				DropDownList ddl_Rosterdate_26 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_26");
				Commonfunction.PopulateDdl(ddl_Rosterdate_26, lookuproster);
				DropDownList ddl_Rosterdate_27 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_27");
				Commonfunction.PopulateDdl(ddl_Rosterdate_27, lookuproster);
				DropDownList ddl_Rosterdate_28 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_28");
				Commonfunction.PopulateDdl(ddl_Rosterdate_28, lookuproster);
				DropDownList ddl_Rosterdate_29 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_29");
				Commonfunction.PopulateDdl(ddl_Rosterdate_29, lookuproster);
				DropDownList ddl_Rosterdate_30 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_30");
				Commonfunction.PopulateDdl(ddl_Rosterdate_30, lookuproster);
				DropDownList ddl_Rosterdate_31 = (DropDownList)e.Row.FindControl("ddl_Rosterdate_31");
				Commonfunction.PopulateDdl(ddl_Rosterdate_31, lookuproster);
				if (lblheading.Text == "1")
				{
					lblempname.Font.Bold = true;
					lblempname.Attributes.Add("Style", "text-align:center;");

					lbldate_1.Visible = true;
					lbldate_1.Font.Bold = true;
					lbldate_1.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_1.Visible = false;

					lbldate_2.Visible = true;
					lbldate_2.Font.Bold = true;
					lbldate_2.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_2.Visible = false;

					lbldate_3.Visible = true;
					lbldate_3.Font.Bold = true;
					lbldate_3.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_3.Visible = false;

					lbldate_4.Visible = true;
					lbldate_4.Font.Bold = true;
					lbldate_4.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_4.Visible = false;

					lbldate_5.Visible = true;
					lbldate_5.Font.Bold = true;
					lbldate_5.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_5.Visible = false;

					lbldate_6.Visible = true;
					lbldate_6.Font.Bold = true;
					lbldate_6.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_6.Visible = false;

					lbldate_7.Visible = true;
					lbldate_7.Font.Bold = true;
					lbldate_7.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_7.Visible = false;

					lbldate_8.Visible = true;
					lbldate_8.Font.Bold = true;
					lbldate_8.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_8.Visible = false;

					lbldate_9.Visible = true;
					lbldate_9.Font.Bold = true;
					lbldate_9.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_9.Visible = false;

					lbldate_10.Visible = true;
					lbldate_10.Font.Bold = true;
					lbldate_10.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_10.Visible = false;

					lbldate_11.Visible = true;
					lbldate_11.Font.Bold = true;
					lbldate_11.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_11.Visible = false;

					lbldate_12.Visible = true;
					lbldate_12.Font.Bold = true;
					lbldate_12.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_12.Visible = false;

					lbldate_13.Visible = true;
					lbldate_13.Font.Bold = true;
					lbldate_13.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_13.Visible = false;

					lbldate_14.Visible = true;
					lbldate_14.Font.Bold = true;
					lbldate_14.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_14.Visible = false;

					lbldate_15.Visible = true;
					lbldate_15.Font.Bold = true;
					lbldate_15.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_15.Visible = false;

					lbldate_16.Visible = true;
					lbldate_16.Font.Bold = true;
					lbldate_16.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_16.Visible = false;

					lbldate_17.Visible = true;
					lbldate_17.Font.Bold = true;
					lbldate_17.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_17.Visible = false;

					lbldate_18.Visible = true;
					lbldate_18.Font.Bold = true;
					lbldate_18.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_18.Visible = false;

					lbldate_19.Visible = true;
					lbldate_19.Font.Bold = true;
					lbldate_19.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_19.Visible = false;

					lbldate_20.Visible = true;
					lbldate_20.Font.Bold = true;
					lbldate_20.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_20.Visible = false;

					lbldate_21.Visible = true;
					lbldate_21.Font.Bold = true;
					lbldate_21.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_21.Visible = false;

					lbldate_22.Visible = true;
					lbldate_22.Font.Bold = true;
					lbldate_22.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_22.Visible = false;

					lbldate_23.Visible = true;
					lbldate_23.Font.Bold = true;
					lbldate_23.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_23.Visible = false;

					lbldate_24.Visible = true;
					lbldate_24.Font.Bold = true;
					lbldate_24.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_24.Visible = false;

					lbldate_25.Visible = true;
					lbldate_25.Font.Bold = true;
					lbldate_25.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_25.Visible = false;

					lbldate_26.Visible = true;
					lbldate_26.Font.Bold = true;
					lbldate_26.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_26.Visible = false;

					lbldate_27.Visible = true;
					lbldate_27.Font.Bold = true;
					lbldate_27.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_27.Visible = false;

					lbldate_28.Visible = true;
					lbldate_28.Font.Bold = true;
					lbldate_28.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_28.Visible = false;

					lbldate_29.Visible = true;
					lbldate_29.Font.Bold = true;
					lbldate_29.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_29.Visible = false;

					lbldate_30.Visible = true;
					lbldate_30.Font.Bold = true;
					lbldate_30.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_30.Visible = false;

					lbldate_31.Visible = true;
					lbldate_31.Font.Bold = true;
					lbldate_31.Attributes.Add("Style", "text-align:center;");
					ddl_Rosterdate_31.Visible = false;
				}
				else
				{
					if (lbldate_1.Text == "OnLeave")
					{
						lbldate_1.Font.Bold = true;
						lbldate_1.Attributes.Add("Style", "text-align:center !important; color:red !important;");
						lbldate_1.Visible = true;
						ddl_Rosterdate_1.Visible = false;
						ddl_Rosterdate_1.SelectedValue = "0";
					}
					else
					{
						lbldate_1.Visible = false;
						ddl_Rosterdate_1.Visible = true;
						ddl_Rosterdate_1.SelectedValue = lbldate_1.Text;
					}
					if (lbldate_2.Text == "OnLeave")
					{
						lbldate_2.Font.Bold = true;
						lbldate_2.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						lbldate_2.Visible = true;
						ddl_Rosterdate_2.Visible = false;
						ddl_Rosterdate_2.SelectedValue = "0";
					}
					else
					{
						lbldate_2.Visible = false;
						ddl_Rosterdate_2.Visible = true;
						ddl_Rosterdate_2.SelectedValue = lbldate_2.Text;
					}
					if (lbldate_3.Text == "OnLeave")
					{
						lbldate_3.Font.Bold = true;
						lbldate_3.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						lbldate_3.Visible = true;
						ddl_Rosterdate_3.Visible = false;
						ddl_Rosterdate_3.SelectedValue = "0";
					}
					else
					{
						lbldate_3.Visible = false;
						ddl_Rosterdate_3.Visible = true;
						ddl_Rosterdate_3.SelectedValue = lbldate_3.Text;
					}
					if (lbldate_4.Text == "OnLeave")
					{
						lbldate_4.Font.Bold = true;
						lbldate_4.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						lbldate_4.Visible = true;
						ddl_Rosterdate_4.Visible = false;
						ddl_Rosterdate_4.SelectedValue = "0";
					}
					else
					{
						lbldate_4.Visible = false;
						ddl_Rosterdate_4.Visible = true;
						ddl_Rosterdate_4.SelectedValue = lbldate_4.Text;
					}
					if (lbldate_5.Text == "OnLeave")
					{
						lbldate_5.Font.Bold = true;
						lbldate_5.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						lbldate_5.Visible = true;
						ddl_Rosterdate_5.Visible = false;
						ddl_Rosterdate_5.SelectedValue = "0";
					}
					else
					{
						lbldate_5.Visible = false;
						ddl_Rosterdate_5.Visible = true;
						ddl_Rosterdate_5.SelectedValue = lbldate_5.Text;
					}
					if (lbldate_6.Text == "OnLeave")
					{
						lbldate_6.Font.Bold = true;
						lbldate_6.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						lbldate_6.Visible = true;
						ddl_Rosterdate_6.Visible = false;
						ddl_Rosterdate_6.SelectedValue = "0";
					}
					else
					{
						lbldate_6.Visible = false;
						ddl_Rosterdate_6.Visible = true;
						ddl_Rosterdate_6.SelectedValue = lbldate_6.Text;
					}
					if (lbldate_7.Text == "OnLeave")
					{
						lbldate_7.Visible = true;
						lbldate_7.Font.Bold = true;
						lbldate_7.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_7.Visible = false;
						ddl_Rosterdate_7.SelectedValue = "0";
					}
					else
					{
						lbldate_7.Visible = false;
						ddl_Rosterdate_7.Visible = true;
						ddl_Rosterdate_7.SelectedValue = lbldate_7.Text;
					}
					if (lbldate_8.Text == "OnLeave")
					{
						lbldate_8.Visible = true;
						lbldate_8.Font.Bold = true;
						lbldate_8.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_8.Visible = false;
						ddl_Rosterdate_8.SelectedValue = "0";
					}
					else
					{
						lbldate_8.Visible = false;
						ddl_Rosterdate_8.Visible = true;
						ddl_Rosterdate_8.SelectedValue = lbldate_8.Text;
					}
					if (lbldate_9.Text == "OnLeave")
					{
						lbldate_9.Visible = true;
						lbldate_9.Font.Bold = true;
						lbldate_9.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_9.Visible = false;
						ddl_Rosterdate_9.SelectedValue = "0";
					}
					else
					{
						lbldate_9.Visible = false;
						ddl_Rosterdate_9.Visible = true;
						ddl_Rosterdate_9.SelectedValue = lbldate_9.Text;
					}
					if (lbldate_10.Text == "OnLeave")
					{
						lbldate_10.Visible = true;
						lbldate_10.Font.Bold = true;
						lbldate_10.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_10.Visible = false;
						ddl_Rosterdate_10.SelectedValue = "0";
					}
					else
					{
						lbldate_10.Visible = false;
						ddl_Rosterdate_10.Visible = true;
						ddl_Rosterdate_10.SelectedValue = lbldate_10.Text;
					}
					if (lbldate_11.Text == "OnLeave")
					{
						lbldate_11.Font.Bold = true;
						lbldate_11.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						lbldate_11.Visible = true;
						ddl_Rosterdate_11.Visible = false;
						ddl_Rosterdate_11.SelectedValue = "0";
					}
					else
					{
						lbldate_11.Visible = false;
						ddl_Rosterdate_11.Visible = true;
						ddl_Rosterdate_11.SelectedValue = lbldate_11.Text;
					}
					if (lbldate_12.Text == "OnLeave")
					{
						lbldate_12.Visible = true;
						lbldate_12.Font.Bold = true;
						lbldate_12.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_12.Visible = false;
						ddl_Rosterdate_12.SelectedValue = "0";
					}
					else
					{
						lbldate_12.Visible = false;
						ddl_Rosterdate_12.Visible = true;
						ddl_Rosterdate_12.SelectedValue = lbldate_12.Text;
					}
					if (lbldate_13.Text == "OnLeave")
					{
						lbldate_13.Visible = true;
						lbldate_13.Font.Bold = true;
						lbldate_13.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_13.Visible = false;
						ddl_Rosterdate_13.SelectedValue = "0";
					}
					else
					{
						lbldate_13.Visible = false;
						ddl_Rosterdate_13.Visible = true;
						ddl_Rosterdate_13.SelectedValue = lbldate_13.Text;
					}
					if (lbldate_14.Text == "OnLeave")
					{
						lbldate_14.Visible = true;
						lbldate_14.Font.Bold = true;
						lbldate_14.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_14.Visible = false;
						ddl_Rosterdate_14.SelectedValue = "0";
					}
					else
					{
						lbldate_14.Visible = false;
						ddl_Rosterdate_14.Visible = true;
						ddl_Rosterdate_14.SelectedValue = lbldate_14.Text;
					}
					if (lbldate_15.Text == "OnLeave")
					{
						lbldate_15.Visible = true;
						lbldate_15.Font.Bold = true;
						lbldate_15.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_15.Visible = false;
						ddl_Rosterdate_15.SelectedValue = "0";
					}
					else
					{
						lbldate_15.Visible = false;
						ddl_Rosterdate_15.Visible = true;
						ddl_Rosterdate_15.SelectedValue = lbldate_15.Text;
					}
					if (lbldate_16.Text == "OnLeave")
					{
						lbldate_16.Visible = true;
						lbldate_16.Font.Bold = true;
						lbldate_16.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_16.Visible = false;
						ddl_Rosterdate_16.SelectedValue = "0";
					}
					else
					{
						lbldate_16.Visible = false;
						ddl_Rosterdate_16.Visible = true;
						ddl_Rosterdate_16.SelectedValue = lbldate_16.Text;
					}
					if (lbldate_17.Text == "OnLeave")
					{
						lbldate_17.Visible = true;
						lbldate_17.Font.Bold = true;
						lbldate_17.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_17.Visible = false;
						ddl_Rosterdate_17.SelectedValue = "0";
					}
					else
					{
						lbldate_17.Visible = false;
						ddl_Rosterdate_17.Visible = true;
						ddl_Rosterdate_17.SelectedValue = lbldate_17.Text;
					}
					if (lbldate_18.Text == "OnLeave")
					{
						lbldate_18.Visible = true;
						lbldate_18.Font.Bold = true;
						lbldate_18.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_18.Visible = false;
						ddl_Rosterdate_18.SelectedValue = "0";
					}
					else
					{
						lbldate_18.Visible = false;
						ddl_Rosterdate_18.Visible = true;
						ddl_Rosterdate_18.SelectedValue = lbldate_18.Text;
					}
					if (lbldate_19.Text == "OnLeave")
					{
						lbldate_19.Visible = true;
						lbldate_19.Font.Bold = true;
						lbldate_19.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_19.Visible = false;
						ddl_Rosterdate_19.SelectedValue = "0";
					}
					else
					{
						lbldate_19.Visible = false;
						ddl_Rosterdate_19.Visible = true;
						ddl_Rosterdate_19.SelectedValue = lbldate_19.Text;
					}
					if (lbldate_20.Text == "OnLeave")
					{
						lbldate_20.Visible = true;
						lbldate_20.Font.Bold = true;
						lbldate_20.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_20.Visible = false;
						ddl_Rosterdate_20.SelectedValue = "0";
					}
					else
					{
						lbldate_20.Visible = false;
						ddl_Rosterdate_20.Visible = true;
						ddl_Rosterdate_20.SelectedValue = lbldate_20.Text;
					}
					if (lbldate_21.Text == "OnLeave")
					{
						lbldate_21.Visible = true;
						lbldate_21.Font.Bold = true;
						lbldate_21.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_21.Visible = false;
						ddl_Rosterdate_21.SelectedValue = "0";
					}
					else
					{
						lbldate_21.Visible = false;
						ddl_Rosterdate_21.Visible = true;
						ddl_Rosterdate_21.SelectedValue = lbldate_21.Text;
					}
					if (lbldate_22.Text == "OnLeave")
					{
						lbldate_22.Font.Bold = true;
						lbldate_22.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						lbldate_22.Visible = true;
						ddl_Rosterdate_22.Visible = false;
						ddl_Rosterdate_22.SelectedValue = "0";
					}
					else
					{
						lbldate_22.Visible = false;
						ddl_Rosterdate_22.Visible = true;
						ddl_Rosterdate_22.SelectedValue = lbldate_22.Text;
					}
					if (lbldate_23.Text == "OnLeave")
					{
						lbldate_23.Visible = true;
						lbldate_23.Font.Bold = true;
						lbldate_23.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_23.Visible = false;
						ddl_Rosterdate_23.SelectedValue = "0";
					}
					else
					{
						lbldate_23.Visible = false;
						ddl_Rosterdate_23.Visible = true;
						ddl_Rosterdate_23.SelectedValue = lbldate_23.Text;
					}
					if (lbldate_24.Text == "OnLeave")
					{
						lbldate_24.Visible = true;
						lbldate_24.Font.Bold = true;
						lbldate_24.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_24.Visible = false;
						ddl_Rosterdate_24.SelectedValue = "0";
					}
					else
					{
						lbldate_24.Visible = false;
						ddl_Rosterdate_24.Visible = true;
						ddl_Rosterdate_24.SelectedValue = lbldate_24.Text;
					}
					if (lbldate_25.Text == "OnLeave")
					{
						lbldate_25.Visible = true;
						lbldate_25.Font.Bold = true;
						lbldate_25.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_25.Visible = false;
						ddl_Rosterdate_25.SelectedValue = "0";
					}
					else
					{
						lbldate_25.Visible = false;
						ddl_Rosterdate_25.Visible = true;
						ddl_Rosterdate_25.SelectedValue = lbldate_25.Text;
					}
					if (lbldate_26.Text == "OnLeave")
					{
						lbldate_26.Visible = true;
						lbldate_26.Font.Bold = true;
						lbldate_26.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_26.Visible = false;
						ddl_Rosterdate_26.SelectedValue = "0";
					}
					else
					{
						lbldate_26.Visible = false;
						ddl_Rosterdate_26.Visible = true;
						ddl_Rosterdate_26.SelectedValue = lbldate_26.Text;
					}
					if (lbldate_27.Text == "OnLeave")
					{
						lbldate_27.Visible = true;
						lbldate_27.Font.Bold = true;
						lbldate_27.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_27.Visible = false;
						ddl_Rosterdate_27.SelectedValue = "0";
					}
					else
					{
						lbldate_27.Visible = false;
						ddl_Rosterdate_27.Visible = true;
						ddl_Rosterdate_27.SelectedValue = lbldate_27.Text;
					}
					if (lbldate_28.Text == "OnLeave")
					{
						lbldate_28.Visible = true;
						lbldate_28.Font.Bold = true;
						lbldate_28.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_28.Visible = false;
						ddl_Rosterdate_28.SelectedValue = "0";
					}
					else
					{
						lbldate_28.Visible = false;
						ddl_Rosterdate_28.Visible = true;
						ddl_Rosterdate_28.SelectedValue = lbldate_28.Text;
					}
					if (lbldate_29.Text == "OnLeave")
					{
						lbldate_29.Visible = true;
						lbldate_29.Font.Bold = true;
						lbldate_29.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_29.Visible = false;
						ddl_Rosterdate_29.SelectedValue = "0";
					}
					else
					{
						lbldate_29.Visible = false;
						ddl_Rosterdate_29.Visible = true;
						ddl_Rosterdate_29.SelectedValue = lbldate_29.Text;
					}
					if (lbldate_30.Text == "OnLeave")
					{
						lbldate_30.Visible = true;
						lbldate_30.Font.Bold = true;
						lbldate_30.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_30.Visible = false;
						ddl_Rosterdate_30.SelectedValue = "0";
					}
					else
					{
						lbldate_30.Visible = false;
						ddl_Rosterdate_30.Visible = true;
						ddl_Rosterdate_30.SelectedValue = lbldate_30.Text;
					}
					if (lbldate_31.Text == "OnLeave")
					{
						lbldate_31.Visible = true;
						lbldate_31.Font.Bold = true;
						lbldate_31.Attributes.Add("Style", "text-align:center !important; color:red !important;margin-left: 20% !important;");
						ddl_Rosterdate_31.Visible = false;
						ddl_Rosterdate_31.SelectedValue = "0";
					}
					else
					{
						lbldate_31.Visible = false;
						ddl_Rosterdate_31.Visible = true;
						ddl_Rosterdate_31.SelectedValue = lbldate_31.Text;
					}
				}
					if (lblnoofdays.Text == "31")
					{
						GvRosterList.Columns[28].Visible = true;
						GvRosterList.Columns[29].Visible = true;
						GvRosterList.Columns[30].Visible = true;
						GvRosterList.Columns[31].Visible = true;
					}

					else if (lblnoofdays.Text == "30")
					{
						GvRosterList.Columns[28].Visible = true;
						GvRosterList.Columns[29].Visible = true;
						GvRosterList.Columns[30].Visible = true;
						GvRosterList.Columns[31].Visible = false;
					}

					else if (lblnoofdays.Text == "29")
					{
						GvRosterList.Columns[28].Visible = true;
						GvRosterList.Columns[29].Visible = true;
						GvRosterList.Columns[30].Visible = false;
						GvRosterList.Columns[31].Visible = false;
					}
					else if (lblnoofdays.Text == "28")
					{
						GvRosterList.Columns[28].Visible = true;
						GvRosterList.Columns[29].Visible = false;
						GvRosterList.Columns[30].Visible = false;
						GvRosterList.Columns[31].Visible = false;
					}
					
			}

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

				if (ddl_Department.SelectedIndex == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "Department", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					ddl_Department.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}

				if (ddl_Month.SelectedIndex == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "Months", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					ddl_Month.Focus();
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
				DutyRosterData objdutyData = new DutyRosterData();
				List<DutyRosterData> LstdutyData = new List<DutyRosterData>();
				int i=0;
				
				IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
				foreach (GridViewRow row in GvRosterList.Rows)
				{
					if (i > 0)
					{
						Label lblheading = (Label)row.Cells[0].FindControl("lblheading");
						Label lblempname = (Label)row.Cells[0].FindControl("lblempname");
						Label lbldepartment = (Label)row.Cells[0].FindControl("lbldepartment");
						Label lblempID = (Label)row.Cells[0].FindControl("lblempID");
						Label lblyear = (Label)row.Cells[0].FindControl("lblyear");
						Label lblSeasonID = (Label)row.Cells[0].FindControl("lblSeasonID");
						Label lblmonth = (Label)row.Cells[0].FindControl("lblmonth");
						Label lblnoofdays = (Label)row.Cells[0].FindControl("lblnoofdays");
						Label lbldate_1 = (Label)row.Cells[0].FindControl("lbldate_1");
						Label lbldate_2 = (Label)row.Cells[0].FindControl("lbldate_2");
						Label lbldate_3 = (Label)row.Cells[0].FindControl("lbldate_3");
						Label lbldate_4 = (Label)row.Cells[0].FindControl("lbldate_4");
						Label lbldate_5 = (Label)row.Cells[0].FindControl("lbldate_5");
						Label lbldate_6 = (Label)row.Cells[0].FindControl("lbldate_6");
						Label lbldate_7 = (Label)row.Cells[0].FindControl("lbldate_7");
						Label lbldate_8 = (Label)row.Cells[0].FindControl("lbldate_8");
						Label lbldate_9 = (Label)row.Cells[0].FindControl("lbldate_9");
						Label lbldate_10 = (Label)row.Cells[0].FindControl("lbldate_10");
						Label lbldate_11 = (Label)row.Cells[0].FindControl("lbldate_11");
						Label lbldate_12 = (Label)row.Cells[0].FindControl("lbldate_12");
						Label lbldate_13 = (Label)row.Cells[0].FindControl("lbldate_13");
						Label lbldate_14 = (Label)row.Cells[0].FindControl("lbldate_14");
						Label lbldate_15 = (Label)row.Cells[0].FindControl("lbldate_15");
						Label lbldate_16 = (Label)row.Cells[0].FindControl("lbldate_16");
						Label lbldate_17 = (Label)row.Cells[0].FindControl("lbldate_17");
						Label lbldate_18 = (Label)row.Cells[0].FindControl("lbldate_18");
						Label lbldate_19 = (Label)row.Cells[0].FindControl("lbldate_19");
						Label lbldate_20 = (Label)row.Cells[0].FindControl("lbldate_20");
						Label lbldate_21 = (Label)row.Cells[0].FindControl("lbldate_21");
						Label lbldate_22 = (Label)row.Cells[0].FindControl("lbldate_22");
						Label lbldate_23 = (Label)row.Cells[0].FindControl("lbldate_23");
						Label lbldate_24 = (Label)row.Cells[0].FindControl("lbldate_24");
						Label lbldate_25 = (Label)row.Cells[0].FindControl("lbldate_25");
						Label lbldate_26 = (Label)row.Cells[0].FindControl("lbldate_26");
						Label lbldate_27 = (Label)row.Cells[0].FindControl("lbldate_27");
						Label lbldate_28 = (Label)row.Cells[0].FindControl("lbldate_28");
						Label lbldate_29 = (Label)row.Cells[0].FindControl("lbldate_29");
						Label lbldate_30 = (Label)row.Cells[0].FindControl("lbldate_30");
						Label lbldate_31 = (Label)row.Cells[0].FindControl("lbldate_31");
						DropDownList ddl_Rosterdate_1 = (DropDownList)row.Cells[1].FindControl("ddl_Rosterdate_1");
						DropDownList ddl_Rosterdate_2 = (DropDownList)row.Cells[2].FindControl("ddl_Rosterdate_2");
						DropDownList ddl_Rosterdate_3 = (DropDownList)row.Cells[3].FindControl("ddl_Rosterdate_3");
						DropDownList ddl_Rosterdate_4 = (DropDownList)row.Cells[4].FindControl("ddl_Rosterdate_4");
						DropDownList ddl_Rosterdate_5 = (DropDownList)row.Cells[5].FindControl("ddl_Rosterdate_5");
						DropDownList ddl_Rosterdate_6 = (DropDownList)row.Cells[6].FindControl("ddl_Rosterdate_6");
						DropDownList ddl_Rosterdate_7 = (DropDownList)row.Cells[7].FindControl("ddl_Rosterdate_7");
						DropDownList ddl_Rosterdate_8 = (DropDownList)row.Cells[8].FindControl("ddl_Rosterdate_8");
						DropDownList ddl_Rosterdate_9 = (DropDownList)row.Cells[9].FindControl("ddl_Rosterdate_9");
						DropDownList ddl_Rosterdate_10 = (DropDownList)row.Cells[10].FindControl("ddl_Rosterdate_10");
						DropDownList ddl_Rosterdate_11 = (DropDownList)row.Cells[11].FindControl("ddl_Rosterdate_11");
						DropDownList ddl_Rosterdate_12 = (DropDownList)row.Cells[12].FindControl("ddl_Rosterdate_12");
						DropDownList ddl_Rosterdate_13 = (DropDownList)row.Cells[13].FindControl("ddl_Rosterdate_13");
						DropDownList ddl_Rosterdate_14 = (DropDownList)row.Cells[14].FindControl("ddl_Rosterdate_14");
						DropDownList ddl_Rosterdate_15 = (DropDownList)row.Cells[15].FindControl("ddl_Rosterdate_15");
						DropDownList ddl_Rosterdate_16 = (DropDownList)row.Cells[16].FindControl("ddl_Rosterdate_16");
						DropDownList ddl_Rosterdate_17 = (DropDownList)row.Cells[17].FindControl("ddl_Rosterdate_17");
						DropDownList ddl_Rosterdate_18 = (DropDownList)row.Cells[18].FindControl("ddl_Rosterdate_18");
						DropDownList ddl_Rosterdate_19 = (DropDownList)row.Cells[19].FindControl("ddl_Rosterdate_19");
						DropDownList ddl_Rosterdate_20 = (DropDownList)row.Cells[20].FindControl("ddl_Rosterdate_20");
						DropDownList ddl_Rosterdate_21 = (DropDownList)row.Cells[21].FindControl("ddl_Rosterdate_21");
						DropDownList ddl_Rosterdate_22 = (DropDownList)row.Cells[22].FindControl("ddl_Rosterdate_22");
						DropDownList ddl_Rosterdate_23 = (DropDownList)row.Cells[23].FindControl("ddl_Rosterdate_23");
						DropDownList ddl_Rosterdate_24 = (DropDownList)row.Cells[24].FindControl("ddl_Rosterdate_24");
						DropDownList ddl_Rosterdate_25 = (DropDownList)row.Cells[25].FindControl("ddl_Rosterdate_25");
						DropDownList ddl_Rosterdate_26 = (DropDownList)row.Cells[26].FindControl("ddl_Rosterdate_26");
						DropDownList ddl_Rosterdate_27 = (DropDownList)row.Cells[27].FindControl("ddl_Rosterdate_27");
						DropDownList ddl_Rosterdate_28 = (DropDownList)row.Cells[28].FindControl("ddl_Rosterdate_28");
						DropDownList ddl_Rosterdate_29 = (DropDownList)row.Cells[29].FindControl("ddl_Rosterdate_29");
						DropDownList ddl_Rosterdate_30 = (DropDownList)row.Cells[30].FindControl("ddl_Rosterdate_30");
						DropDownList ddl_Rosterdate_31 = (DropDownList)row.Cells[31].FindControl("ddl_Rosterdate_31");
						DutyRosterData objrosterData = new DutyRosterData();

						objrosterData.DepartmentID = Convert.ToInt32(lbldepartment.Text.Trim());
						objrosterData.Month = Convert.ToInt32(lblmonth.Text.Trim());
						objrosterData.Year = Convert.ToInt32(lblyear.Text.Trim());
						objrosterData.EmpID = Convert.ToInt64(lblempID.Text.Trim());
						objrosterData.SeasonID = Convert.ToInt32(lblSeasonID.Text.Trim());
						objrosterData.No_Of_Days = Convert.ToInt32(lblnoofdays.Text.Trim());
						objrosterData.RosterDetails_Day1 = lbldate_1.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_1.SelectedValue.ToString();
						DateTime date_1 = DateTime.Parse("01/"+lblmonth.Text+"/"+lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day1 = date_1;
						objrosterData.RosterDetails_Day2 =lbldate_2.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_2.SelectedValue.ToString();
						DateTime date_2 = DateTime.Parse("02/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day2 = date_2;
						objrosterData.RosterDetails_Day3=lbldate_3.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_3.SelectedValue.ToString();
						DateTime date_3 = DateTime.Parse("03/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day3 = date_3;
						objrosterData.RosterDetails_Day4=lbldate_4.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_4.SelectedValue.ToString();
						DateTime date_4 = DateTime.Parse("04/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day4 = date_4;
						objrosterData.RosterDetails_Day5=lbldate_5.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_5.SelectedValue.ToString();
						DateTime date_5 = DateTime.Parse("05/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day5 = date_5;
						objrosterData.RosterDetails_Day6=lbldate_6.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_6.SelectedValue.ToString();
						DateTime date_6 = DateTime.Parse("06/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day6 = date_6;
						objrosterData.RosterDetails_Day7=lbldate_7.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_7.SelectedValue.ToString();
						DateTime date_7 = DateTime.Parse("07/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day7 = date_7;
						objrosterData.RosterDetails_Day8=lbldate_8.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_8.SelectedValue.ToString();
						DateTime date_8 = DateTime.Parse("08/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day8 = date_8;
						objrosterData.RosterDetails_Day9=lbldate_9.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_9.SelectedValue.ToString();
						DateTime date_9 = DateTime.Parse("09/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day9 = date_9;
						objrosterData.RosterDetails_Day10=lbldate_10.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_10.SelectedValue.ToString();
						DateTime date_10 = DateTime.Parse("10/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day10 = date_10;
						objrosterData.RosterDetails_Day11=lbldate_11.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_11.SelectedValue.ToString();
						DateTime date_11 = DateTime.Parse("11/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day11 = date_11;
						objrosterData.RosterDetails_Day12=lbldate_12.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_12.SelectedValue.ToString();
						DateTime date_12 = DateTime.Parse("12/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day12 = date_12;
						objrosterData.RosterDetails_Day13=lbldate_13.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_13.SelectedValue.ToString();
						DateTime date_13 = DateTime.Parse("13/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day13 = date_13;
						objrosterData.RosterDetails_Day14=lbldate_14.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_14.SelectedValue.ToString();
						DateTime date_14 = DateTime.Parse("14/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day14 = date_14;
						objrosterData.RosterDetails_Day15=lbldate_15.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_15.SelectedValue.ToString();
						DateTime date_15 = DateTime.Parse("15/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day15 = date_15;
						objrosterData.RosterDetails_Day16=lbldate_16.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_16.SelectedValue.ToString();
						DateTime date_16 = DateTime.Parse("16/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day16 = date_16;
						objrosterData.RosterDetails_Day17=lbldate_17.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_17.SelectedValue.ToString();
						DateTime date_17 = DateTime.Parse("17/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day17 = date_17;
						objrosterData.RosterDetails_Day18=lbldate_18.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_18.SelectedValue.ToString();
						DateTime date_18 = DateTime.Parse("18/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day18 = date_18;
						objrosterData.RosterDetails_Day19=lbldate_19.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_19.SelectedValue.ToString();
						DateTime date_19 = DateTime.Parse("19/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day19 = date_19;
						objrosterData.RosterDetails_Day20=lbldate_20.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_20.SelectedValue.ToString();
						DateTime date_20 = DateTime.Parse("20/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day20 = date_20;
						objrosterData.RosterDetails_Day21=lbldate_21.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_21.SelectedValue.ToString();
						DateTime date_21 = DateTime.Parse("21/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day21 = date_21;
						objrosterData.RosterDetails_Day22=lbldate_22.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_22.SelectedValue.ToString();
						DateTime date_22 = DateTime.Parse("22/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day22 = date_22;
						objrosterData.RosterDetails_Day23=lbldate_23.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_23.SelectedValue.ToString();
						DateTime date_23 = DateTime.Parse("23/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day23 = date_23;
						objrosterData.RosterDetails_Day24=lbldate_24.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_24.SelectedValue.ToString();
						DateTime date_24 = DateTime.Parse("24/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day24 = date_24;
						objrosterData.RosterDetails_Day25=lbldate_25.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_25.SelectedValue.ToString();
						DateTime date_25 = DateTime.Parse("25/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day25 = date_25;
						objrosterData.RosterDetails_Day26=lbldate_26.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_26.SelectedValue.ToString();
						DateTime date_26 = DateTime.Parse("26/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day26 = date_26;
						objrosterData.RosterDetails_Day27=lbldate_27.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_27.SelectedValue.ToString();
						DateTime date_27 = DateTime.Parse("27/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day27 = date_27;
						objrosterData.RosterDetails_Day28=lbldate_28.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_28.SelectedValue.ToString();
						DateTime date_28 = DateTime.Parse("28/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						objrosterData.Date_Day28 = date_28;

						if ( lblnoofdays.Text == "29")
						{
							objrosterData.RosterDetails_Day29=lbldate_29.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_29.SelectedValue.ToString();
							DateTime date_29 = DateTime.Parse("29/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
							objrosterData.Date_Day29 = date_29;
							objrosterData.RosterDetails_Day30 = "0";
							DateTime date_30 = GlobalConstant.MinSQLDateTime;
							objrosterData.Date_Day30 = date_30;
							objrosterData.RosterDetails_Day31 = "0";
							DateTime date_31 = GlobalConstant.MinSQLDateTime;
							objrosterData.Date_Day31 = date_31;
						}
						else if ( lblnoofdays.Text == "30")
						{
							objrosterData.RosterDetails_Day29=lbldate_29.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_29.SelectedValue.ToString();
							DateTime date_29 = DateTime.Parse("29/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
							objrosterData.Date_Day29 = date_29;
							objrosterData.RosterDetails_Day30=lbldate_30.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_30.SelectedValue.ToString();
							DateTime date_30 = DateTime.Parse("30/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
							objrosterData.Date_Day30 = date_30;
							objrosterData.RosterDetails_Day31 = "0";
							DateTime date_31 = GlobalConstant.MinSQLDateTime;
							objrosterData.Date_Day31 = date_31;
						}
						else if (lblnoofdays.Text == "31")
						{
							objrosterData.RosterDetails_Day29=lbldate_29.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_29.SelectedValue.ToString();
							DateTime date_29 = DateTime.Parse("29/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
							objrosterData.Date_Day29 = date_29;
							objrosterData.RosterDetails_Day30=lbldate_30.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_30.SelectedValue.ToString();
							DateTime date_30 = DateTime.Parse("30/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
							objrosterData.Date_Day30 = date_30;
							objrosterData.RosterDetails_Day31=lbldate_31.Text == "OnLeave" ? "OnLeave" : ddl_Rosterdate_31.SelectedValue.ToString();
							DateTime date_31 = DateTime.Parse("31/" + lblmonth.Text + "/" + lblyear.Text, option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
							objrosterData.Date_Day31 = date_31;
						}
						else {
							objrosterData.RosterDetails_Day29 = "0";
							DateTime date_29 = GlobalConstant.MinSQLDateTime;
							objrosterData.Date_Day29 = date_29;
							objrosterData.RosterDetails_Day30 = "0";
							DateTime date_30 = GlobalConstant.MinSQLDateTime;
							objrosterData.Date_Day30 = date_30;
							objrosterData.RosterDetails_Day31 = "0";
							DateTime date_31 = GlobalConstant.MinSQLDateTime;
							objrosterData.Date_Day31 = date_31;
						    }
							LstdutyData.Add(objrosterData);
					}
					i++;
				}
				objdutyData.XMLEmployeeDuty = XmlConvertor.DutySchedulartoXML(LstdutyData).ToString();
				objdutyData.DepartmentID = Convert.ToInt32(ddl_Department.SelectedValue == "" ? "0" : ddl_Department.SelectedValue);
				objdutyData.Month = Convert.ToInt32(ddl_Month.SelectedValue == "" ? "0" : ddl_Month.SelectedValue);
				objdutyData.Year = Convert.ToInt32(txt_year.Text);
				objdutyData.EmployeeID = LogData.EmployeeID;
				objdutyData.HospitalID = LogData.HospitalID;
				objdutyData.FinancialYearID = LogData.FinancialYearID;
				DutyRosterBO objdutyBO = new DutyRosterBO();
				int results = objdutyBO.UpdateDutyRoster(objdutyData);
	
				if (results == 1 )
				{
					Messagealert_.ShowMessage(lblmessage,  "updating .....", 1);
					divmsg1.Attributes["class"] = "SucessAlert";
					divmsg1.Visible = true;
					ddl_Department.Focus();
					int result = objdutyBO.UpdateEmployeesDutySchedule(objdutyData);
					if (result == 1)
					{
						
						bindgrid();
						Messagealert_.ShowMessage(lblmessage, "update", 1);
						divmsg1.Attributes["class"] = "SucessAlert";
						divmsg1.Visible = true;
						btnsave.Attributes["disabled"] = "disabled";
						return;
					}
					else
					{
						Messagealert_.ShowMessage(lblmessage, "system", 0);
						divmsg1.Attributes["class"] = "FailAlert";
						divmsg1.Visible = true;
						return;
					}
					
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

		protected void btnreset_Click(object sender, EventArgs e)
		{
			ddl_Department.SelectedIndex = 0;
			ddl_Month.SelectedIndex = 0;
			txt_year.Text = DateTime.Now.Year.ToString();
			lblmessage.Visible = false;
			lblmessage.Text = "";
			GvRosterList.DataSource = null;
			GvRosterList.DataBind();

		}
		protected String GetRoster(Int64 EmpID)
		{
			DutyRosterData objdutyData = new DutyRosterData();
			List<DutyRosterData> LstdutyData = new List<DutyRosterData>();
			return "EventID=25,StartDate=2018/08/08,EndDate=2018/08/08,EventName=morning";
		}
		
    }
}