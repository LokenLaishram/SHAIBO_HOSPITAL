using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
	public partial class PHRAccountGroupMaster : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				bindddl();
				//initialize();

			}

		}
		private void initialize()
		{
			string IP = Commonfunction.GetClientIPAddress();
			string URL = "http://" + IP + ":9000";
			Boolean flag = Commonfunction.isValidURL(URL);
			if (flag)
			{
				btnSync.Visible = true;
				txt_tally.Text = "ONLINE";
			}
			else
			{
				btnSync.Visible = false;
				txt_tally.Text = "OFFLINE";
			}

		}
		private void bindddl()
		{
			MasterLookupBO mstlookup = new MasterLookupBO();
			Commonfunction.PopulateDdl(ddl_groupUnder, mstlookup.GetLookupsList(LookupName.PHRAccountGroup));
			Commonfunction.PopulateDdl(ddl_group_nature, mstlookup.GetLookupsList(LookupName.AccountGroupNature));
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
				Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
				div1.Attributes["class"] = "FailAlert";
				ddlexport.Focus();
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
				if (ddl_groupUnder.SelectedIndex == 0)
				{

					Messagealert_.ShowMessage(lblmessage, "AccountGroupUnder", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					ddl_groupUnder.Focus();
					return;

				}
				else
				{
					lblmessage.Visible = false;
				}
				if (txtGroup.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "AccountGroupName", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					txtGroup.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (ddl_group_nature.SelectedIndex == 0)
				{

					Messagealert_.ShowMessage(lblmessage, "AccountGroupNature", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					ddl_group_nature.Focus();
					return;

				}
				else
				{
					lblmessage.Visible = false;
				}
				PHRAcountData objdata = new PHRAcountData();
				PHRAccountBO objstdBO = new PHRAccountBO();
				objdata.GroupUnderID = Convert.ToInt32(ddl_groupUnder.SelectedValue == "0" ? null : ddl_groupUnder.SelectedValue);
				objdata.GroupName = txtGroup.Text == "" ? "" : txtGroup.Text.Trim();
				objdata.NatureID = Convert.ToInt32(ddl_group_nature.SelectedValue == "0" ? null : ddl_group_nature.SelectedValue);
				objdata.EmployeeID = LogData.EmployeeID;
				objdata.HospitalID = LogData.HospitalID;
				objdata.FinancialYearID = LogData.FinancialYearID;
				objdata.ActionType = Enumaction.Insert;
				if (ViewState["ID"] != null)
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
					}

					objdata.ActionType = Enumaction.Update;
					objdata.ID = Convert.ToInt32(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
				}
				else
				{
					objdata.ID = 0;
				}
				int result = objstdBO.UpdateAccntGrpMaster(objdata);
				if (result == 1 || result == 2)
				{
					Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
					div1.Visible = true;
					div1.Attributes["class"] = "SucessAlert";
					//reset();
					bindgrid();
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "system", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
				}
			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				string msg = ex.ToString();
				Messagealert_.ShowMessage(lblmessage, msg, 0);
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			bindgrid();
		}

		protected void btnreset_Click(object sender, EventArgs e)
		{
			reset();
			MasterLookupBO mstlookup = new MasterLookupBO();
			Commonfunction.PopulateDdl(ddl_groupUnder, mstlookup.GetLookupsList(LookupName.AccountGroup));
			Commonfunction.PopulateDdl(ddl_group_nature, mstlookup.GetLookupsList(LookupName.AccountGroupNature));
		}
		protected void reset()
		{
			ddl_group_nature.SelectedIndex = 0;
			ddl_groupUnder.SelectedIndex = 0;
			txtGroup.Text = "";
			ViewState["ID"] = null;
			bindgrid();
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
				List<PHRAccountGroupMasterData> GroupList = GetGroupData(0);
				if (GroupList.Count > 0)
				{
					GVGroup.DataSource = GroupList;
					GVGroup.DataBind();
					GVGroup.Visible = true;
					Messagealert_.ShowMessage(lblresult, "Total:" + GroupList[0].MaximumRows.ToString() + " Record(s) found", 1);
					divmsg3.Attributes["class"] = "SucessAlert";
					divmsg3.Visible = true;
					ddlexport.Visible = true;
					btnexport.Visible = true;
					lblresult.Visible = false;
					div1.Visible = false;


				}
				else
				{

					divmsg3.Visible = false;
					GVGroup.DataSource = null;
					GVGroup.DataBind();
					GVGroup.Visible = true;
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
		protected void GvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
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
					GridViewRow gr = GVGroup.Rows[i];
					Label ID = (Label)gr.Cells[0].FindControl("lblGroupID");
					Int32 GroupID = Convert.ToInt32(ID.Text);
					ViewState["ID"] = GroupID;
					Label grpName = (Label)gr.Cells[0].FindControl("lblGroupName");
					Label grpUnder = (Label)gr.Cells[0].FindControl("lblUnderID");
					Label grpNature = (Label)gr.Cells[0].FindControl("lblgrpNatureID");
					txtGroup.Text = grpName.Text;
					ddl_group_nature.SelectedIndex = Convert.ToInt32(grpNature.Text);
					ddl_groupUnder.SelectedIndex = Convert.ToInt32(grpUnder.Text);
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

					PHRAcountData objDAta = new PHRAcountData();
					PHRAccountBO objstdBO = new PHRAccountBO();
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = GVGroup.Rows[i];
					Label ID = (Label)gr.Cells[0].FindControl("lblGroupID");
					TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
					txtremarks.Enabled = true;
					if (txtremarks.Text == "")
					{
						Messagealert_.ShowMessage(lblresult, "Remarks", 0);
						div1.Attributes["class"] = "FailAlert";
						div1.Visible = true;
						txtremarks.Focus();
						return;
					}
					else
					{
						objDAta.Remarks = txtremarks.Text;
					}
					objDAta.ID = Convert.ToInt32(ID.Text);
					objDAta.EmployeeID = LogData.UserLoginId;
					objDAta.HospitalID = LogData.HospitalID;
					objDAta.IPaddress = LogData.IPaddress;
					int Result = objstdBO.DeleteAccountGroupByID(objDAta);
					if (Result == 1)
					{
						Messagealert_.ShowMessage(lblmessage, "delete", 1);
						div1.Attributes["class"] = "SucessAlert";
						div1.Visible = true;
						bindgrid();
					}
					else
					{
						Messagealert_.ShowMessage(lblmessage, "system", 0);
						div1.Attributes["class"] = "FailAlert";
						div1.Visible = true;
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
			}
		}
		public List<PHRAccountGroupMasterData> GetGroupData(int curIndex)
		{

			PHRAcountData objdata = new PHRAcountData();
			PHRAccountBO objstdBO = new PHRAccountBO();
			objdata.GroupUnderID = Convert.ToInt32(ddl_groupUnder.SelectedValue == "0" ? null : ddl_groupUnder.SelectedValue);
			objdata.GroupName = txtGroup.Text == "" ? "" : txtGroup.Text.Trim();
			objdata.NatureID = Convert.ToInt32(ddl_group_nature.SelectedValue == "0" ? null : ddl_group_nature.SelectedValue);

			return objstdBO.GetAccntGrpList(objdata);

		}
		public void ExportToPdf()
		{
			using (StringWriter sw = new StringWriter())
			{
				using (HtmlTextWriter hw = new HtmlTextWriter(sw))
				{
					GVGroup.BorderStyle = BorderStyle.None;
					//Hide the Column containing CheckBox
					GVGroup.Columns[6].Visible = false;
					GVGroup.Columns[7].Visible = false;

					GVGroup.RenderControl(hw);
					GVGroup.HeaderRow.Style.Add("width", "15%");
					GVGroup.HeaderRow.Style.Add("font-size", "10px");
					GVGroup.Style.Add("text-decoration", "none");
					GVGroup.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
					GVGroup.Style.Add("font-size", "8px");
					StringReader sr = new StringReader(sw.ToString());
					Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
					pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
					PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
					pdfDoc.Open();
					iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
					pdfDoc.Close();
					Response.ContentType = "application/pdf";
					Response.AddHeader("content-disposition", "attachment;filename=AccountGroup.pdf");
					Response.Cache.SetCacheability(HttpCacheability.NoCache);
					Response.Write(pdfDoc);
					Response.End();
					Messagealert_.ShowMessage(lblresult, "Exported", 1);
					divmsg3.Attributes["class"] = "SucessAlert";
				}
			}
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
			/* Verifies that the control is rendered */
		}
		protected void ExportoExcel()
		{

			DataTable dt = GetDatafromDatabase();
			using (XLWorkbook wb = new XLWorkbook())
			{
				wb.Worksheets.Add(dt, "AccountGroup");

				Response.Clear();
				Response.Buffer = true;
				Response.Charset = "";
				Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				Response.AddHeader("content-disposition", "attachment;filename=AccountGroup.xlsx");
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
		protected DataTable GetDatafromDatabase()
		{
			List<PHRAccountGroupMasterData> GrpData = GetGroupData(0);
			List<PHRAccountGroupMasterDataExcel> ListexcelData = new List<PHRAccountGroupMasterDataExcel>();
			int i = 0;
			foreach (PHRAccountGroupMasterData row in GrpData)
			{
				PHRAccountGroupMasterDataExcel Ecxeclpat = new PHRAccountGroupMasterDataExcel();
				Ecxeclpat.GroupID = GrpData[i].GroupID;
				Ecxeclpat.GroupName = GrpData[i].GroupName;
				Ecxeclpat.Under = GrpData[i].Under;
				Ecxeclpat.Nature = GrpData[i].Nature;

				ListexcelData.Add(Ecxeclpat);
				i++;
			}
			ListtoDataTableConverter converter = new ListtoDataTableConverter();
			DataTable dt = converter.ToDataTable(ListexcelData);
			return dt;
		}
		public string SendReqst(string pWebRequstStr)
		{
			string IP = Commonfunction.GetClientIPAddress();
			String lResponseStr = "";
			String lResult = "";
			string URL = "http://" + IP + ":9000";

			try
			{
				String lTallyLocalHost = URL;
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(lTallyLocalHost);
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentLength = (long)pWebRequstStr.Length;
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				StreamWriter lStrmWritr = new StreamWriter(httpWebRequest.GetRequestStream());
				lStrmWritr.Write(pWebRequstStr);
				lStrmWritr.Close();
				HttpWebResponse lhttpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream lreceiveStream = lhttpResponse.GetResponseStream();
				StreamReader lStreamReader = new StreamReader(lreceiveStream, Encoding.UTF8);
				lResponseStr = lStreamReader.ReadToEnd();
				lhttpResponse.Close();
				lStreamReader.Close();
			}
			catch (Exception)
			{

				throw;
			}
			lResult = lResponseStr;
			return lResult;
		}
		protected void btnSync_Click(object sender, EventArgs e)
		{
			string IP = Commonfunction.GetClientIPAddress();
			string URL = "http://" + IP + ":9000";
			Boolean flag = Commonfunction.isValidURL(URL);
			if (flag)
			{
				lblmessage.Visible = false;
				List<PHRAccountGroupMasterData> Listobjdata = new List<PHRAccountGroupMasterData>();
				foreach (GridViewRow row in GVGroup.Rows)
				{
					Label lblGroupName = (Label)GVGroup.Rows[row.RowIndex].Cells[0].FindControl("lblGroupName");
					Label lblGroupUnder = (Label)GVGroup.Rows[row.RowIndex].Cells[0].FindControl("lblGroupUnder");
					Label lblGroupType = (Label)GVGroup.Rows[row.RowIndex].Cells[0].FindControl("lblGroupType");

					if (Convert.ToInt32(lblGroupType.Text) == 0)
					{

						PHRAccountGroupMasterData objsubdata = new PHRAccountGroupMasterData();
						objsubdata.GroupName = lblGroupName.Text == "" ? "" : lblGroupName.Text;
						objsubdata.Under = lblGroupUnder.Text == "" ? "" : lblGroupUnder.Text;

						Listobjdata.Add(objsubdata);
					}

				}
				string xml = XmlConvertor.PHRGroupDatatoXML(Listobjdata).ToString();
				String response = SendReqst(xml);
				lblResponse.Text = Commonfunction.TallyResponse(response);
				MDResponse.Show();
			}
			else
			{
				btnSync.Visible = false;
				txt_tally.Text = "OFFLINE";
				Messagealert_.ShowMessage(lblmessage, "Tally is offline ", 0);
				div1.Visible = true;
				div1.Attributes["class"] = "FailAlert";
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

		protected void btnSample_Click(object sender, EventArgs e)
		{

		}
	}
}