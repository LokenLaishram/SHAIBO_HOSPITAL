using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedHrData;
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
	public partial class HRControllManager : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				MasterLookupBO mstlookup = new MasterLookupBO();
				Commonfunction.PopulateDdl(ddldepartmentID, mstlookup.GetLookupsList(LookupName.Department));
				Setddldisabled();
				ddldepartmentID.SelectedIndex = 1;
				getcascadingDropdown();
				ddldepartmentID.SelectedIndex = 0;
			}

		}

		private void getcascadingDropdown()
		{ 
			MasterLookupBO mstlookup = new MasterLookupBO();
			Commonfunction.PopulateDdl(ddldesignation, mstlookup.GetDesignationByDepartmentID(Convert.ToInt32(ddldepartmentID.SelectedValue=="" ? "0" :ddldepartmentID.SelectedValue)));
			
			Commonfunction.PopulateDdl(ddlemployeetype, mstlookup.GetEmployeeTypeByDepartmentID(Convert.ToInt32(ddldepartmentID.SelectedValue == "" ? "0" : ddldepartmentID.SelectedValue)));
			ddldesignation.SelectedIndex = 0;
			ddlemployeetype.SelectedIndex = 0;
		
		}
		protected void ddldepartmentID_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddldepartmentID.SelectedIndex > 0)
			{
				getcascadingDropdown();
				removeddldisabled();
			}
			else
			{
				Setddldisabled();
			}
		}


		private void Setddldisabled()
		{
			
			ddldesignation.Attributes["disabled"] = "disabled";
			ddlemployeetype.Attributes["disabled"] = "disabled";

		}
		private void removeddldisabled()
		{
			ddldesignation.Attributes.Remove("disabled");
			ddlemployeetype.Attributes.Remove("disabled");
		}
		protected void btnsearch_Click(object sender, EventArgs e)
		{
			bindgrid();
		}
		private void bindgrid()
		{
			List<HRControllManagerData> listcontrols = GetControls(0);
			if (listcontrols.Count > 0)
			{
				GvHRcontrolList.DataSource = listcontrols;
				GvHRcontrolList.DataBind();
				GvHRcontrolList.Visible = true;
				btnupdate.Visible = true;
			}
			else
			{
				GvHRcontrolList.DataSource = null;
				GvHRcontrolList.DataBind();
				btnupdate.Visible = false;
			}
		}
		public List<HRControllManagerData> GetControls(int curIndex)
		{
			HRControllManagerData objcontrols = new HRControllManagerData();
			HRControllManagerBO objcontrolBO = new HRControllManagerBO();
			objcontrols.DepartmentID = Convert.ToInt32(ddldepartmentID.SelectedValue == "" ? "0" : ddldepartmentID.SelectedValue);
			objcontrols.DesignationID = Convert.ToInt32(ddldesignation.SelectedValue == "" ? "0" : ddldesignation.SelectedValue);
			objcontrols.EmployeeTypeID = Convert.ToInt32(ddlemployeetype.SelectedValue == "" ? "0" : ddlemployeetype.SelectedValue);
			return objcontrolBO.GetHRControlManagerList(objcontrols);
		}
		protected void btncancel_Click(object sender, EventArgs e)
		{
			ddldepartmentID.SelectedIndex = 0;
			Commonfunction.Insertzeroitemindex(ddldesignation);
			Commonfunction.Insertzeroitemindex(ddlemployeetype);
			GvHRcontrolList.Visible = false;
			btnupdate.Visible = false;
			lblmessage.Visible = false;

		}
		protected void GvHRcontrolList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Label lblempID = (Label)e.Row.FindControl("lblempID");
				Label lbldesignationID = (Label)e.Row.FindControl("lbldesignationID");
				Label lbldepartmentID = (Label)e.Row.FindControl("lbldepartmentID");
				Label lblemployeetypeID = (Label)e.Row.FindControl("lblemployeetypeID");
				Label lblleaverequest = (Label)e.Row.FindControl("lblleaverequest");
				CheckBox chekboxselect_leaverequest = (CheckBox)e.Row.FindControl("chekboxselect_leaverequest");
				Label lblleaveapprove = (Label)e.Row.FindControl("lblleaveapprove");
				CheckBox chekboxselect_leaveapprove = (CheckBox)e.Row.FindControl("chekboxselect_leaveapprove");
				Label lblrosterupdate = (Label)e.Row.FindControl("lblrosterupdate");
				CheckBox chekboxselect_rosterupdate = (CheckBox)e.Row.FindControl("chekboxselect_rosterupdate");
				Label lblrosterchangerequest = (Label)e.Row.FindControl("lblrosterchangerequest");
				CheckBox chekboxselect_rosterchangerequest = (CheckBox)e.Row.FindControl("chekboxselect_rosterchangerequest");
				Label lblrosterchangeapprove = (Label)e.Row.FindControl("lblrosterchangeapprove");
				CheckBox chekboxselect_rosterchangeapprove = (CheckBox)e.Row.FindControl("chekboxselect_rosterchangeapprove");
				if (lblleaverequest.Text == "1")
				{
					chekboxselect_leaverequest.Checked = true;
				}
				else
				{
					chekboxselect_leaverequest.Checked = false;
				}

				if (lblleaveapprove.Text == "1")
				{
					chekboxselect_leaveapprove.Checked = true;
				}
				else
				{
					chekboxselect_leaveapprove.Checked = false;
				}
				if (lblrosterupdate.Text == "1")
				{
					chekboxselect_rosterupdate.Checked = true;
				}
				else
				{
					chekboxselect_rosterupdate.Checked = false;
				}
				if (lblrosterupdate.Text == "1")
				{
					chekboxselect_rosterupdate.Checked = true;
				}
				else
				{
					chekboxselect_rosterupdate.Checked = false;
				}
				if (lblrosterchangerequest.Text == "1")
				{
					chekboxselect_rosterchangerequest.Checked = true;
				}
				else
				{
					chekboxselect_rosterchangerequest.Checked = false;
				}
				if (lblrosterchangeapprove.Text == "1")
				{
					chekboxselect_rosterchangeapprove.Checked = true;
				}
				else
				{
					chekboxselect_rosterchangeapprove.Checked = false;
				}
				
			}
		}
		protected void btnupdate_Click(object sender, EventArgs e)
		{

			List<HRControllManagerData> listcontrols = new List<HRControllManagerData>();
			HRControllManagerBO objcontrolsBO = new HRControllManagerBO();
			HRControllManagerData objcontrols = new HRControllManagerData();
			//DepositBO objstdBO = new DepositBO();
			// int index = 0;
			try
			{
				// get all the record from the gridview
				foreach (GridViewRow row in GvHRcontrolList.Rows)
				{
					IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
					Label lblempID = (Label)row.FindControl("lblempID");
					Label lbldesignationID = (Label)row.FindControl("lbldesignationID");
					Label lbldepartmentID = (Label)row.FindControl("lbldepartmentID");
					Label lblemployeetypeID = (Label)row.FindControl("lblemployeetypeID");
					CheckBox chekboxselect_leaverequest = (CheckBox)row.FindControl("chekboxselect_leaverequest");
					CheckBox chekboxselect_leaveapprove = (CheckBox)row.FindControl("chekboxselect_leaveapprove");
					CheckBox chekboxselect_rosterupdate = (CheckBox)row.FindControl("chekboxselect_rosterupdate");
					CheckBox chekboxselect_rosterchangerequest = (CheckBox)row.FindControl("chekboxselect_rosterchangerequest");
					CheckBox chekboxselect_rosterchangeapprove = (CheckBox)row.FindControl("chekboxselect_rosterchangeapprove");

					HRControllManagerData ObjDetails = new HRControllManagerData();
					ObjDetails.EmployeeID = Convert.ToInt64(lblempID.Text == "" ? "0" : lblempID.Text);
					ObjDetails.DesignationID = Convert.ToInt32(lbldesignationID.Text == "" ? "0" : lbldesignationID.Text);
					ObjDetails.DepartmentID = Convert.ToInt32(lbldepartmentID.Text == "" ? "0" : lbldepartmentID.Text);
					ObjDetails.EmployeeTypeID = Convert.ToInt32(lblemployeetypeID.Text == "" ? "0" : lblemployeetypeID.Text);
					ObjDetails.LeaveRequestEnable = Convert.ToInt32(chekboxselect_leaverequest.Checked == true ? "1" : "0");
					ObjDetails.LeaveApproveEnable = Convert.ToInt32(chekboxselect_leaveapprove.Checked == true ? "1" : "0");
					ObjDetails.RosterUpdateEnable = Convert.ToInt32(chekboxselect_rosterupdate.Checked == true ? "1" : "0");
					ObjDetails.RosterChangeRequestEnable = Convert.ToInt32(chekboxselect_rosterchangerequest.Checked == true ? "1" : "0");
					ObjDetails.RosterChangeApproveEnable = Convert.ToInt32(chekboxselect_rosterchangeapprove.Checked == true ? "1" : "0");
					listcontrols.Add(ObjDetails);
				}
				objcontrols.ControllisttoXML = XmlConvertor.ControllisttoXML(listcontrols).ToString();


				int result = objcontrolsBO.UpdateHRControlManagerList(objcontrols);
				if (result > 0)
				{
					bindgrid();
					Messagealert_.ShowMessage(lblmessage, "update", 1);
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "Error", 0);
				}
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				lblmessage.Text = ExceptionMessage.GetMessage(ex);
			}
		}
	}
}