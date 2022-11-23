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
	public partial class PHRAccountLedgerMaster : BasePage
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
			Commonfunction.PopulateDdl(ddl_group, mstlookup.GetLookupsList(LookupName.AccountGroup));
			Commonfunction.PopulateDdl(ddl_type, mstlookup.GetLookupsList(LookupName.AccountGroupNature));
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
				if (ddl_group.SelectedIndex == 0)
				{

					Messagealert_.ShowMessage(lblmessage, "AccountGroupUnder", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					ddl_group.Focus();
					return;

				}
				else
				{
					lblmessage.Visible = false;
				}
				if (txtaccount.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "AccountGroupName", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					txtaccount.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (ddl_type.SelectedIndex == 0)
				{

					Messagealert_.ShowMessage(lblmessage, "AccountGroupNature", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					ddl_type.Focus();
					return;

				}
				else
				{
					lblmessage.Visible = false;
				}

				if (txtSite.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "AccountSite", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					txtSite.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}

				//if (txtOpAmt.Text == "")
				//{
				//    Messagealert_.ShowMessage(lblmessage, "AccountOpeningBalance", 0);
				//    div1.Visible = true;
				//    div1.Attributes["class"] = "FailAlert";
				//    txtOpAmt.Focus();
				//    return;
				//}
				//else
				//{
				//    lblmessage.Visible = false;
				//}

				if (txtOpnDate.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "AccountOpeningDate", 0);
					div1.Visible = true;
					div1.Attributes["class"] = "FailAlert";
					txtOpnDate.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
				PHRAcountLedgerData objdata = new PHRAcountLedgerData();
				PHRAccountBO objstdBO = new PHRAccountBO();
				objdata.GroupUnderID = Convert.ToInt32(ddl_group.SelectedValue == "0" ? null : ddl_group.SelectedValue);
				objdata.AccountName = txtaccount.Text == "" ? "" : txtaccount.Text.Trim();
				objdata.NatureID = Convert.ToInt32(ddl_type.SelectedValue == "0" ? null : ddl_type.SelectedValue);
				objdata.Site = txtSite.Text == "" ? "" : txtSite.Text.Trim();
				objdata.Opnbal = Convert.ToDecimal(txtOpAmt.Text.Trim() == "" ? "0" : txtOpAmt.Text.Trim());
				objdata.OpnDate = DateTime.Parse((txtOpnDate.Text == "" ? System.DateTime.Now.ToString() : txtOpnDate.Text.Trim()), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);


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
				int result = objstdBO.UpdateAccntLedgerMaster(objdata);
				if (result == 1 || result == 2)
				{
					Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
					div1.Visible = true;
					div1.Attributes["class"] = "SucessAlert";
					//reset();

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
		}
		protected void reset()
		{
			ddl_group.SelectedIndex = 0;
			ddl_type.SelectedIndex = 0;
			txtaccount.Text = "";
			txtSite.Text = "";
			txtOpAmt.Text = "";
			txtOpnDate.Text = "";
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
				List<PHRAccountLedgerMasterData> LedgerList = GetLedgerData(0);
				if (LedgerList.Count > 0)
				{
					GVLedger.DataSource = LedgerList;
					GVLedger.DataBind();
					GVLedger.Visible = true;
					Messagealert_.ShowMessage(lblresult, "Total:" + LedgerList[0].MaximumRows.ToString() + " Record(s) found", 1);
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
					GVLedger.DataSource = null;
					GVLedger.DataBind();
					GVLedger.Visible = true;
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
		protected void GvLedger_RowCommand(object sender, GridViewCommandEventArgs e)
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
					GridViewRow gr = GVLedger.Rows[i];
					Label ID = (Label)gr.Cells[0].FindControl("lblLedgerID");
					Int32 LedId = Convert.ToInt32(ID.Text);
					ViewState["ID"] = LedId;
					Label ledName = (Label)gr.Cells[0].FindControl("lblAccountName");
					Label ledGrp = (Label)gr.Cells[0].FindControl("lblGroupUnderID");
					Label ledNature = (Label)gr.Cells[0].FindControl("lblgrpNatureID");
					Label ledSite = (Label)gr.Cells[0].FindControl("lblSite");
					Label LedBalance = (Label)gr.Cells[0].FindControl("lblOpnBal");
					Label LedDate = (Label)gr.Cells[0].FindControl("lblDate");
					txtaccount.Text = ledName.Text;
					ddl_group.SelectedValue = ledGrp.Text;
					ddl_type.SelectedValue = ledNature.Text;
					txtSite.Text = ledSite.Text;
					txtOpAmt.Text = LedBalance.Text;
					txtOpnDate.Text = LedDate.Text;
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

					PHRAcountLedgerData objDAta = new PHRAcountLedgerData();
					PHRAccountBO objstdBO = new PHRAccountBO();
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = GVLedger.Rows[i];
					Label ID = (Label)gr.Cells[0].FindControl("lblLedgerID");
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
					int Result = objstdBO.DeleteAccountLedgerByID(objDAta);
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
		public List<PHRAccountLedgerMasterData> GetLedgerData(int curIndex)
		{
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			PHRAcountLedgerData objdata = new PHRAcountLedgerData();
			PHRAccountBO objstdBO = new PHRAccountBO();
			objdata.GroupUnderID = Convert.ToInt32(ddl_group.SelectedValue == "0" ? null : ddl_group.SelectedValue);
			objdata.AccountName = txtaccount.Text == "" ? "" : txtaccount.Text.Trim();
			objdata.NatureID = Convert.ToInt32(ddl_type.SelectedValue == "0" ? null : ddl_type.SelectedValue);
			objdata.Site = txtSite.Text == "" ? "" : txtSite.Text.Trim();
			objdata.Opnbal = Convert.ToDecimal(txtOpAmt.Text == "" ? "0" : txtOpAmt.Text.Trim());
			//   objdata.OpnDate = DateTime.Parse(txtOpnDate.Text == "" ? ""+System.DateTime.Now : txtOpnDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

			return objstdBO.GetAccntLedgerList(objdata);

		}
		public void ExportToPdf()
		{
			using (StringWriter sw = new StringWriter())
			{
				using (HtmlTextWriter hw = new HtmlTextWriter(sw))
				{
					GVLedger.BorderStyle = BorderStyle.None;
					//Hide the Column containing CheckBox
					GVLedger.Columns[6].Visible = false;
					GVLedger.Columns[7].Visible = false;

					GVLedger.RenderControl(hw);
					GVLedger.HeaderRow.Style.Add("width", "15%");
					GVLedger.HeaderRow.Style.Add("font-size", "10px");
					GVLedger.Style.Add("text-decoration", "none");
					GVLedger.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
					GVLedger.Style.Add("font-size", "8px");
					StringReader sr = new StringReader(sw.ToString());
					Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
					pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
					PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
					pdfDoc.Open();
					iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
					pdfDoc.Close();
					Response.ContentType = "application/pdf";
					Response.AddHeader("content-disposition", "attachment;filename=AccountLedger.pdf");
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
				wb.Worksheets.Add(dt, "AccoutLedger");

				Response.Clear();
				Response.Buffer = true;
				Response.Charset = "";
				Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				Response.AddHeader("content-disposition", "attachment;filename=AccountLedger.xlsx");
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
		protected DataTable GetDatafromDatabase()
		{
			List<PHRAccountLedgerMasterData> LedData = GetLedgerData(0);
			List<PHRAccountLedgerMasterDataExcel> ListexcelData = new List<PHRAccountLedgerMasterDataExcel>();
			int i = 0;
			foreach (PHRAccountLedgerMasterData row in LedData)
			{
				PHRAccountLedgerMasterDataExcel Ecxeclpat = new PHRAccountLedgerMasterDataExcel();
				Ecxeclpat.LedgerID = LedData[i].LedgerID;
				Ecxeclpat.AccountName = LedData[i].AccountName;
				Ecxeclpat.GroupName = LedData[i].GroupName;
				Ecxeclpat.Site = LedData[i].Site;
				Ecxeclpat.Opnbal = LedData[i].Opnbal;
				Ecxeclpat.OpnDate = LedData[i].OpnDate;

				ListexcelData.Add(Ecxeclpat);
				i++;
			}
			ListtoDataTableConverter converter = new ListtoDataTableConverter();
			DataTable dt = converter.ToDataTable(ListexcelData);
			return dt;
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

		protected void btnSync_Click(object sender, EventArgs e)
		{
			string IP = Commonfunction.GetClientIPAddress();
			string URL = "http://" + IP + ":9000";
			Boolean flag = Commonfunction.isValidURL(URL);
			if (flag)
			{
				lblmessage.Visible = false;
				List<PHRAccountLedgerMasterData> Listobjdata = new List<PHRAccountLedgerMasterData>();
				foreach (GridViewRow row in GVLedger.Rows)
				{
					Label lblAccountName = (Label)GVLedger.Rows[row.RowIndex].Cells[0].FindControl("lblAccountName");
					Label lblGroupName = (Label)GVLedger.Rows[row.RowIndex].Cells[0].FindControl("lblGroupName");
					Label lblOpnBal = (Label)GVLedger.Rows[row.RowIndex].Cells[0].FindControl("lblOpnBal");



					PHRAccountLedgerMasterData objsubdata = new PHRAccountLedgerMasterData();
					objsubdata.AccountName = lblAccountName.Text == "" ? "" : lblAccountName.Text;
					objsubdata.GroupName = lblGroupName.Text == "" ? "" : lblGroupName.Text;
					objsubdata.Opnbal = Convert.ToDecimal(lblOpnBal.Text == "" ? "" : lblOpnBal.Text);

					Listobjdata.Add(objsubdata);

				}
				string xml = XmlConvertor.PHRLedgerDatatoXML(Listobjdata).ToString();
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

		protected void btnSample_Click(object sender, EventArgs e)
		{

		}
	}
}