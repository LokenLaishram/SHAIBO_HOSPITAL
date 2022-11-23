using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedPharBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedPharData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
	public partial class PHRAccountMappingMaster : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{

				ddlbind();
				btnsave.Visible = false;
				checkSelect();
			}
		}
		private void ddlbind()
		{
			MasterLookupBO mstlookup = new MasterLookupBO();
			Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.CommonGroupAll));
			Commonfunction.Insertzeroitemindex(ddl_subservicetype);
		}
		public void checkSelect()
		{
			if (ddl_group_type.SelectedIndex == 0)
			{
				ddl_servicetype.Attributes["disabled"] = "disabled";

			}
			else
			{
				ddl_servicetype.Attributes.Remove("disabled");

			}
			if (ddl_servicetype.SelectedIndex == 0)
			{
				ddl_map_type.Attributes["disabled"] = "disabled";

			}
			else
			{
				ddl_map_type.Attributes.Remove("disabled");

			}
			if (ddl_map_type.SelectedIndex == 0)
			{
				ddl_subservicetype.Attributes["disabled"] = "disabled";
			}
			else if (ddl_map_type.SelectedIndex == 1)
			{
				ddl_subservicetype.Attributes["disabled"] = "disabled";

			}
			else if (ddl_map_type.SelectedIndex == 2)
			{
				ddl_subservicetype.Attributes.Remove("disabled");
			}
		}
		[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
		public static List<string> GetAccountName(string prefixText, int count, string contextKey)
		{
			PHRAcountLedgerData objData = new PHRAcountLedgerData();
			PHRAccountBO objBO = new PHRAccountBO();
			List<PHRAcountLedgerData> getResult = new List<PHRAcountLedgerData>();
			objData.AccountName = prefixText;
			getResult = objBO.SearchLedgerByName(objData);
			List<String> list = new List<String>();
			for (int i = 0; i < getResult.Count; i++)
			{
				list.Add(getResult[i].AccountName.ToString());
			}
			return list;
		}

		protected void ddl_servicetype_SelectedIndexChanged(object sender, EventArgs e)
		{

			MasterLookupBO mstlookup = new MasterLookupBO();
			if (ddl_group_type.SelectedIndex == 0)
			{

			}
			else if (ddl_group_type.SelectedIndex == 1)
			{
				if (ddl_servicetype.SelectedIndex > 0)
				{
					Commonfunction.PopulateDdl(ddl_subservicetype, mstlookup.GetSubServiceTypeByGroupID(Convert.ToInt32(ddl_servicetype.SelectedValue)));
				}
			}
			else
			{
				if (ddl_servicetype.SelectedIndex > 0)
				{
					Commonfunction.PopulateDdl(ddl_subservicetype, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_servicetype.SelectedValue)));
				}
			}
			checkSelect();
		}
		protected void btnsave_Click(object sender, EventArgs e)
		{
			List<PHRAccountMappingMasterData> Listobjdata = new List<PHRAccountMappingMasterData>();
			PHRAccountMappingMasterData objdata = new PHRAccountMappingMasterData();
			PHRAccountBO objstdBO = new PHRAccountBO();

			try
			{
				// get all the record from the gridview
				foreach (GridViewRow row in GVMapping.Rows)
				{
					Label lblServiceTypeID = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblServiceTypeID");
					Label lblsubServiceTypeID = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblsubServiceTypeID");
					Label lblServiceID = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblServiceID");
					TextBox txtDebitAcount = (TextBox)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("txt_debit_account");
					TextBox txtCreditAcount = (TextBox)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("txt_credit_accnt");
					Label lblMappingType = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblMappingType");
					Label lblGroupType = (Label)GVMapping.Rows[row.RowIndex].Cells[0].FindControl("lblGroupType");



					PHRAccountMappingMasterData objsubdata = new PHRAccountMappingMasterData();
					objsubdata.ServiceType = Convert.ToInt32(lblServiceTypeID.Text == "" ? "0" : lblServiceTypeID.Text);
					objsubdata.SubServiceType = Convert.ToInt32(lblServiceTypeID.Text == "" ? "0" : lblsubServiceTypeID.Text);
					objsubdata.ServiceID = Convert.ToInt32(lblServiceID.Text == "" ? "0" : lblServiceID.Text);
					objsubdata.MappingType = Convert.ToInt32(lblMappingType.Text == "" ? "0" : lblMappingType.Text);
					objsubdata.GroupType = Convert.ToInt32(lblGroupType.Text == "" ? "0" : lblGroupType.Text);

					String debitAcntID = "0";
					String CreditAcntID = "0";

					String debitText = txtDebitAcount.Text == "" ? null : txtDebitAcount.Text.ToString().Trim();
					if (debitText != null)
					{
						String[] debit = debitText.Split(new[] { ":" }, StringSplitOptions.None);
						debitAcntID = debit[1];
					}
					String CreditText = txtCreditAcount.Text == "" ? null : txtCreditAcount.Text.ToString().Trim();
					if (CreditText != null)
					{
						String[] credit = CreditText.Split(new[] { ":" }, StringSplitOptions.None);
						CreditAcntID = credit[1];
					}


					objsubdata.DebitID = Convert.ToInt32(debitAcntID);
					objsubdata.CreditID = Convert.ToInt32(CreditAcntID);
					Listobjdata.Add(objsubdata);

				}
				objdata.XMLData = XmlConvertor.PHRAccountMappingCollectionDatatoXML(Listobjdata).ToString();

				int result = objstdBO.UpdateAccntMappingMaster(objdata);
				if (result > 0)
				{

					Messagealert_.ShowMessage(lblmessage, "update", 1);
					div1.Visible = true;
					div1.Attributes["class"] = "SucessAlert";

				}
				else
				{

					Messagealert_.ShowMessage(lblmessage, "Error", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
				}

			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);

			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			bindgrid();
		}

		protected void btnreset_Click(object sender, EventArgs e)
		{
			ddl_servicetype.SelectedIndex = 0;
			bindgrid();
			btnsave.Visible = false;
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

			else
			{
				Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
				div1.Attributes["class"] = "FailAlert";
				ddlexport.Focus();
				return;
			}
		}
		protected void ExportoExcel()
		{

			DataTable dt = GetDatafromDatabase();
			using (XLWorkbook wb = new XLWorkbook())
			{
				wb.Worksheets.Add(dt, "Account Mapping");

				Response.Clear();
				Response.Buffer = true;
				Response.Charset = "";
				Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				Response.AddHeader("content-disposition", "attachment;filename=Account Mapping.xlsx");
				using (MemoryStream MyMemoryStream = new MemoryStream())
				{
					wb.SaveAs(MyMemoryStream);
					MyMemoryStream.WriteTo(Response.OutputStream);
					Response.Flush();
					Response.End();
					ddlexport.SelectedIndex = 0;
				}
				Messagealert_.ShowMessage(lblresult, "Exported", 1);
				divmsg3.Attributes["class"] = "SucessAlert";
			}
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
		protected void bindgrid()
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
				if (ddl_group_type.SelectedIndex == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "Select Group Type", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (ddl_servicetype.SelectedIndex == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "Select Service Type", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}

				if (ddl_map_type.SelectedIndex == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "Select Mapping Type", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					return;
				}
				else
				{
					if (ddl_map_type.SelectedIndex == 2)
					{
						if (ddl_subservicetype.SelectedIndex == 0)
						{
							Messagealert_.ShowMessage(lblmessage, "Select Sub group Type", 0);
							div1.Visible = true;
							div1.Attributes["class"] = "FailAlert";
							return;
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
					lblmessage.Visible = false;
				}
				List<PHRAccountMappingMasterData> MappingList = GetMappingData(0);
				if (MappingList.Count > 0)
				{
					btnsave.Visible = true;
					GVMapping.DataSource = MappingList;
					GVMapping.DataBind();
					GVMapping.Visible = true;
					Messagealert_.ShowMessage(lblresult, "Total:" + MappingList[0].MaximumRows.ToString() + " Record(s) found", 1);
					divmsg3.Attributes["class"] = "SucessAlert";
					divmsg3.Visible = true;
					ddlexport.Visible = true;
					btnexport.Visible = true;
					lblresult.Visible = false;
					div1.Visible = false;
				}
				else
				{
					btnsave.Visible = false;
					divmsg3.Visible = false;
					GVMapping.DataSource = null;
					GVMapping.DataBind();
					GVMapping.Visible = true;
					ddlexport.Visible = false;
					btnexport.Visible = false;
					divmsg3.Visible = false;
					lblresult.Visible = false;
				}
			}
			catch (Exception ex)
			{
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
		}
		public List<PHRAccountMappingMasterData> GetMappingData(int curIndex)
		{

			PHRAccountMappingMasterData objdata = new PHRAccountMappingMasterData();
			PHRAccountBO objstdBO = new PHRAccountBO();

			objdata.ServiceType = Convert.ToInt32(ddl_servicetype.SelectedValue == "0" ? "0" : ddl_servicetype.SelectedValue);
			objdata.SubServiceType = Convert.ToInt32(ddl_subservicetype.SelectedValue == "0" ? "0" : ddl_subservicetype.SelectedValue);
			objdata.MappingType = Convert.ToInt32(ddl_map_type.SelectedValue == "0" ? "0" : ddl_map_type.SelectedValue);
			objdata.GroupType = Convert.ToInt32(ddl_group_type.SelectedValue == "0" ? "0" : ddl_group_type.SelectedValue);

			return objstdBO.GetAcntServiceMappingList(objdata);

		}
		protected DataTable GetDatafromDatabase()
		{
			List<PHRAccountMappingMasterData> GrpData = GetMappingData(0);
			List<PHRAccountMappingMasterExcelData> ListexcelData = new List<PHRAccountMappingMasterExcelData>();
			int i = 0;
			foreach (PHRAccountMappingMasterData row in GrpData)
			{
				PHRAccountMappingMasterExcelData Ecxeclpat = new PHRAccountMappingMasterExcelData();
				Ecxeclpat.ServiceTypeName = GrpData[i].ServiceTypeName;
				Ecxeclpat.ServiceName = GrpData[i].ServiceName;
				Ecxeclpat.DebitAccount = GrpData[i].DebitAccount;
				Ecxeclpat.CreditAccount = GrpData[i].CreditAccount;

				ListexcelData.Add(Ecxeclpat);
				i++;
			}
			ListtoDataTableConverter converter = new ListtoDataTableConverter();
			DataTable dt = converter.ToDataTable(ListexcelData);
			return dt;
		}
		protected void GVMapping_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Label lblDebitMapping = (Label)e.Row.FindControl("lblDebitMapping");
				Label lblCreditMapping = (Label)e.Row.FindControl("lblCreditMapping");
				TextBox txt_debit_account = (TextBox)e.Row.FindControl("txt_debit_account");
				TextBox txt_credit_accnt = (TextBox)e.Row.FindControl("txt_credit_accnt");
				if (Convert.ToInt32(lblDebitMapping.Text == "" ? "0" : lblDebitMapping.Text) > 0)
				{
					txt_debit_account.ReadOnly = true;
				}
				else { txt_debit_account.ReadOnly = false; }

				if (Convert.ToInt32(lblCreditMapping.Text == "" ? "0" : lblCreditMapping.Text) > 0)
				{
					txt_credit_accnt.ReadOnly = true;
				}
				else { txt_credit_accnt.ReadOnly = false; }
			}
		}

		protected void txt_debit_account_TextChanged(object sender, EventArgs e)
		{
			GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
			TextBox txt = (TextBox)currentRow.FindControl("txt_debit_account");
			if (!txt.Text.ToString().Contains("ID:"))
			{

				txt.Text = "";
			}

		}

		protected void txt_credit_accnt_TextChanged(object sender, EventArgs e)
		{
			GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
			TextBox txt = (TextBox)currentRow.FindControl("txt_credit_accnt");
			if (!txt.Text.ToString().Contains("ID:"))
			{

				txt.Text = "";
			}
		}

		protected void ddl_map_type_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddl_map_type.SelectedIndex == 0)
			{
				ddl_subservicetype.Attributes["disabled"] = "disabled";
			}
			else if (ddl_map_type.SelectedIndex == 1)
			{
				ddl_subservicetype.Attributes["disabled"] = "disabled";

			}
			else if (ddl_map_type.SelectedIndex == 2)
			{
				ddl_subservicetype.Attributes.Remove("disabled");
			}
		}

		protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
		{
			MasterLookupBO mstlookup = new MasterLookupBO();
			if (ddl_group_type.SelectedIndex == 0)
			{

			}
			else if (ddl_group_type.SelectedIndex == 1)
			{
				Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.CommonGroupAll));
			}
			else
			{
				Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.LabGroupAll));
			}
			checkSelect();
		}
	}
}