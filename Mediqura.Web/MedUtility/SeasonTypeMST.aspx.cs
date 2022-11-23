using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saplin.Controls;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.Common;
using Mediqura.BOL.MedUtilityBO;
using System.Drawing;
using Mediqura.BOL.CommonBO;

namespace Mediqura.Web.MedUtility
{
	public partial class SeasonTypeMST : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				MasterLookupBO mstlookup = new MasterLookupBO();
				Commonfunction.PopulateDdl(ddl_season, mstlookup.GetLookupsList(LookupName.season));
				Commonfunction.PopulateCheckbox(ddl_Month, mstlookup.GetLookupsList(LookupName.month));
			
				
			}
		}
		
		private void bindgrid()
		{
			try
			{
				List<SeasonTypeData> lstseason = Getseason(0);
				if (lstseason.Count > 0)
				{
					GvSeason.DataSource = lstseason;
					GvSeason.DataBind();
					GvSeason.Visible = true;
					Messagealert_.ShowMessage(lblresult, "Total: " + lstseason.Count + " Record found", 1);
					divmsg3.Attributes["class"] = "SucessAlert";
					divmsg3.Visible = true;
					
				}
				else
				{
					GvSeason.DataSource = null;
					GvSeason.DataBind();
					GvSeason.Visible = true;
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
		private List<SeasonTypeData> Getseason(int p)
		{
			SeasonTypeData objseasonData = new SeasonTypeData();
			SeasonTypeBO objseasonBO = new SeasonTypeBO();
			objseasonData.SeasonID = Convert.ToInt32(ddl_season.SelectedValue == "" ? "0" : ddl_season.SelectedValue);
			
			return objseasonBO.SearchSeasonDetails(objseasonData);
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
		protected void GvSeason_RowDataBound(object sender, GridViewRowEventArgs e)
		{

			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				try
				{

					Label lbl_january = (Label)e.Row.FindControl("lbl_january");
					Label janstatus = (Label)e.Row.FindControl("janstatus");
					Label lbl_february = (Label)e.Row.FindControl("lbl_february");
					Label febstatus = (Label)e.Row.FindControl("febstatus");
					Label lbl_march = (Label)e.Row.FindControl("lbl_march");
					Label marstatus = (Label)e.Row.FindControl("marstatus");
					Label lbl_april = (Label)e.Row.FindControl("lbl_april");
					Label aprilstatus = (Label)e.Row.FindControl("aprilstatus");
					Label lbl_may = (Label)e.Row.FindControl("lbl_may");
					Label maystatus = (Label)e.Row.FindControl("maystatus");
					Label lbl_june = (Label)e.Row.FindControl("lbl_june");
					Label junestatus = (Label)e.Row.FindControl("junestatus");
					Label lbl_july = (Label)e.Row.FindControl("lbl_july");
					Label julystatus = (Label)e.Row.FindControl("julystatus");
					Label lbl_august = (Label)e.Row.FindControl("lbl_august");
					Label augstatus = (Label)e.Row.FindControl("augstatus");
					Label lbl_september = (Label)e.Row.FindControl("lbl_september");
					Label septstatus = (Label)e.Row.FindControl("septstatus");
					Label lbl_october = (Label)e.Row.FindControl("lbl_october");
					Label octstatus = (Label)e.Row.FindControl("octstatus");
					Label lbl_november = (Label)e.Row.FindControl("lbl_november");
					Label novstatus = (Label)e.Row.FindControl("novstatus");
					Label lbl_december = (Label)e.Row.FindControl("lbl_december");
					Label decstatus = (Label)e.Row.FindControl("decstatus");
					

					
					if (lbl_january.Text == "1")
					{
						janstatus.Text = "<i class='fa fa-check'></i>";
						janstatus.ForeColor = Color.Green;

					}
					else
					{
						janstatus.Text = "<i class='fa fa-times'></i>";
						janstatus.ForeColor = Color.Red;
					}
					if (lbl_february.Text == "2")
					{
						febstatus.Text = "<i class='fa fa-check'></i>";
						febstatus.ForeColor = Color.Green;
					}
					else
					{
						febstatus.Text = "<i class='fa fa-times'></i>";
						febstatus.ForeColor = Color.Red;
					}
					if (lbl_march.Text == "3")
					{
						marstatus.Text = "<i class='fa fa-check'></i>";
						marstatus.ForeColor = Color.Green;
					}
					else
					{
						marstatus.Text = "<i class='fa fa-times'></i>";
						marstatus.ForeColor = Color.Red;
					}
					if (lbl_april.Text == "4")
					{
						aprilstatus.Text = "<i class='fa fa-check'></i>";
						aprilstatus.ForeColor = Color.Green;
					}
					else
					{
						aprilstatus.Text = "<i class='fa fa-times'></i>";
						aprilstatus.ForeColor = Color.Red;
					}
					if (lbl_may.Text == "5")
					{
						maystatus.Text = "<i class='fa fa-check'></i>";
						maystatus.ForeColor = Color.Green;
					}
					else
					{
						maystatus.Text = "<i class='fa fa-times'></i>";
						maystatus.ForeColor = Color.Red;
					}
					if (lbl_june.Text == "6")
					{
						junestatus.Text = "<i class='fa fa-check'></i>";
						junestatus.ForeColor = Color.Green;
					}
					else
					{
						junestatus.Text = "<i class='fa fa-times'></i>";
						junestatus.ForeColor = Color.Red;
					}
					if (lbl_july.Text == "7")
					{
						julystatus.Text = "<i class='fa fa-check'></i>";
						julystatus.ForeColor = Color.Green;

					}
					else
					{
						julystatus.Text = "<i class='fa fa-times'></i>";
						julystatus.ForeColor = Color.Red;
					}
					if (lbl_august.Text == "8")
					{
						augstatus.Text = "<i class='fa fa-check'></i>";
						augstatus.ForeColor = Color.Green;
					}
					else
					{
						augstatus.Text = "<i class='fa fa-times'></i>";
						augstatus.ForeColor = Color.Red;
					}
					if (lbl_september.Text == "9")
					{
						septstatus.Text = "<i class='fa fa-check'></i>";
						septstatus.ForeColor = Color.Green;
					}
					else
					{
						septstatus.Text = "<i class='fa fa-times'></i>";
						septstatus.ForeColor = Color.Red;
					}
					if (lbl_october.Text == "10")
					{
						octstatus.Text = "<i class='fa fa-check'></i>";
						octstatus.ForeColor = Color.Green;
					}
					else
					{
						octstatus.Text = "<i class='fa fa-times'></i>";
						octstatus.ForeColor = Color.Red;
					}
					if (lbl_november.Text == "11")
					{
						novstatus.Text = "<i class='fa fa-check'></i>";
						novstatus.ForeColor = Color.Green;
					}
					else
					{
						novstatus.Text = "<i class='fa fa-times'></i>";
						novstatus.ForeColor = Color.Red;
					}
					if (lbl_december.Text == "12")
					{
						decstatus.Text = "<i class='fa fa-check'></i>";
						decstatus.ForeColor = Color.Green;
					}
					else
					{
						decstatus.Text = "<i class='fa fa-times'></i>";
						decstatus.ForeColor = Color.Red;
					}
				}

				catch (Exception ex)
				{
					PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
					LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
					Messagealert_.ShowMessage(lblmessage, "system", 0);
					div1.Attributes["class"] = "FailAlert";
					div1.Visible = true;
				}
			}
		}
		protected void GvSeason_RowCommand(object sender, GridViewCommandEventArgs e)
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
					GridViewRow gr = GvSeason.Rows[i];
					Label lblseasonID = (Label)gr.Cells[0].FindControl("lblseasonID");
					Int32 SeasonID = Convert.ToInt32(lblseasonID.Text);
					SeasonTypeData objseason = new SeasonTypeData();
					objseason.SeasonID = SeasonID;
					SeasonTypeBO objseasonBO = new SeasonTypeBO();
					List<SeasonTypeData> lstseason = objseasonBO.GetseasonDetailsByID(objseason);
					if (lstseason.Count > 0)
					{
						MasterLookupBO mstlookup = new MasterLookupBO();
						Commonfunction.PopulateDdl(ddl_season, mstlookup.GetLookupsList(LookupName.season));
						Commonfunction.PopulateCheckbox(ddl_Month, mstlookup.GetLookupsList(LookupName.month));
						ddl_season.SelectedValue = lstseason[0].SeasonID.ToString();
						foreach (ListItem li in ddl_Month.Items)
						{
							if (lstseason[0].January == 1)
							{
								ddl_Month.Items[0].Selected = true;
							}
							if (lstseason[0].February == 2)
							{
								ddl_Month.Items[1].Selected = true;
							}
							if (lstseason[0].March == 3)
							{
								ddl_Month.Items[2].Selected = true;
							}
							if (lstseason[0].April == 4)
							{
								ddl_Month.Items[3].Selected = true;
							}
							if (lstseason[0].May == 5)
							{
								ddl_Month.Items[4].Selected = true;
							}
							if (lstseason[0].June == 6)
							{
								ddl_Month.Items[5].Selected = true;
							}
							if (lstseason[0].July == 7)
							{
								ddl_Month.Items[6].Selected = true;
							}
							if (lstseason[0].August == 8)
							{
								ddl_Month.Items[7].Selected = true;
							}
							if (lstseason[0].September == 9)
							{
								ddl_Month.Items[8].Selected = true;
							}
							if (lstseason[0].October == 10)
							{
								ddl_Month.Items[9].Selected = true;
							}
							if (lstseason[0].November == 11)
							{
								ddl_Month.Items[10].Selected = true;
							}
							if (lstseason[0].December == 12)
							{
								ddl_Month.Items[11].Selected = true;
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
				div1.Attributes["class"] = "FailAlert";
				div1.Visible = true;
				return;
			}
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
				
				if (ddl_season.SelectedValue == "0")
				{
					Messagealert_.ShowMessage(lblmessage, "Please select season", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					ddl_season.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}

				SeasonTypeData objseason = new SeasonTypeData();
				int numSelected = 0;

				foreach (ListItem li in ddl_Month.Items)
				{
					if (li.Selected)
					{
						if (li.Selected && li.Enabled)
						{
							numSelected = numSelected + 1;
						}
						if (li.Value == "1" )
						{
							objseason.January = 1;
						}
						
						if (li.Value == "2")
						{
							objseason.February = 2;
						}
						
						if (li.Value == "3" )
						{
							objseason.March = 3;
						}
						
						if (li.Value == "4" )
						{
							objseason.April = 4;
						}
						if (li.Value == "5")
						{
							objseason.May = 5;
						}
						if (li.Value == "6" )
						{
							objseason.June = 6;
						}
						if (li.Value == "7")
						{
							objseason.July = 7;
						}
						if (li.Value == "8")
						{
							objseason.August = 8;
						}
						if (li.Value == "9")
						{
							objseason.September = 9;
						}
						if (li.Value == "10")
						{
							objseason.October = 10;
						}
						if (li.Value == "11" )
						{
							objseason.November = 11;
						}
						if (li.Value == "12")
						{
							objseason.December = 12;
						}
						
					}
				}
				if (numSelected == 0 )
				{
					Messagealert_.ShowMessage(lblmessage, "Please select at least one month.", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					ddl_Month.Focus();
					return;
				}
				objseason.SeasonID = Convert.ToInt32(ddl_season.SelectedValue == "" ? "0" : ddl_season.SelectedValue);
				objseason.EmployeeID = LogData.EmployeeID;
		        objseason.HospitalID = LogData.HospitalID;
                objseason.FinancialYearID = LogData.FinancialYearID;
                objseason.ActionType = Enumaction.Update;
				SeasonTypeBO objseasonBO=new SeasonTypeBO();
				int result = objseasonBO.UpdateSeasonDetails(objseason);

				if (result == 2)
				{
					lblmessage.Visible = true;
					Messagealert_.ShowMessage(lblmessage, "save", 1);
					div1.Visible = true;
					div1.Attributes["class"] = "SucessAlert";
					ViewState["ID"] = null;
					//bindgrid();
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
		protected void btnreset_Click(object sender, EventArgs e)
		{
			ddl_season.SelectedIndex = 0;
			ddl_Month.SelectedIndex = 0;
			GvSeason.Visible = false;
			div1.Visible = false;
			lblmessage.Visible = false;
			divmsg3.Visible = false;
			lblresult.Visible = false;
		}
	}
}