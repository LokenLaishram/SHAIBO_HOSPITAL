using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;
using Mediqura.Utility;
using Mediqura.CommonData.LoginData;
using Mediqura.BOL.MedGenStoreBO;
using Mediqura.DAL.MedGenStoreDA;

namespace Mediqura.Web.MedGenStore
{
    public partial class POCrossChecking : BasePage
    {
        int total, total1, total2, total9, total10, total11;
        decimal total3, total4, total5, total6, total7, total8, total12, total13;
        double sum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                btn_verify.Attributes["disabled"] = "disabled";
             }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_verifyby, mstlookup.GetLookupsList(LookupName.StoreEmp));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPONo(string prefixText, int count, string contextKey)
        {
            GENStrData Objpaic = new GENStrData();
            POApprovalBO objInfoBO = new POApprovalBO();
            List<GENStrData> getResult = new List<GENStrData>();
            Objpaic.PONo = prefixText;
            getResult = objInfoBO.GetautoPONo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PONo.ToString());
            }
            return list;
        }
        protected void txtPONo_TextChanged(object sender, EventArgs e)
        {
            if (txtPONo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "PO", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txtPONo.Focus();
                return;

            }
            else
            {
                divmsg1.Visible = false;
            }
            bindgrid();
        }

        protected void txtPO_TextChanged(object sender, EventArgs e)
        {
            if (txtPO.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "PO", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txtPONo.Focus();
                return;

            }
            else
            {
                divmsg1.Visible = false;
            }
            bindgrid1();
        }
        protected void gvpocrosschecklist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqty = (Label)e.Row.FindControl("lbl_qty");
                int a_qty = Int32.Parse(lblqty.Text);
                total = total + a_qty;

                TextBox txt_recdqty = (TextBox)e.Row.FindControl("txt_recdqty");
                int qty = Int32.Parse(txt_recdqty.Text);
                total1 = total1 + qty;

                TextBox txt_cpperqty = (TextBox)e.Row.FindControl("txt_cpperqty");
                decimal cpperqty = Decimal.Parse(txt_cpperqty.Text);
                total3 = total3 + cpperqty;

                Label lbl_totalprice = (Label)e.Row.FindControl("lbl_totalprice");
                decimal cp = Decimal.Parse(lbl_totalprice.Text);
                total4 = total4 + cp;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label totalqty = (Label)e.Row.FindControl("lbl_totqty");
                totalqty.Text = total.ToString();

                HiddenField totalrecdqty = (HiddenField)e.Row.FindControl("hdn_totrecdqty");
                totalrecdqty.Value = total1.ToString();

                HiddenField lbl_cpperqty = (HiddenField)e.Row.FindControl("hdn_totcpperqty");
                lbl_cpperqty.Value = total3.ToString();

                HiddenField lbl_totalprice = (HiddenField)e.Row.FindControl("hdn_totprice");
                lbl_totalprice.Value = total4.ToString();

            }
        }

        protected void btn_verify_Click(object sender, EventArgs e)
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
            List<StockGRNData> List = new List<StockGRNData>();
            POCrossCheckBO objBO = new POCrossCheckBO();
            StockGRNData objrec = new StockGRNData();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvpocrosschecklist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ItemID = (Label)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label CP = (Label)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
                    Label SupplierID = (Label)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_supplierID");
                    Label OrderedQty = (Label)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_qty");
                    Label Price = (Label)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalprice");
                    TextBox RecdQty = (TextBox)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("txt_recdqty");
                    if (Convert.ToInt32(RecdQty.Text) == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ReqdQty", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        RecdQty.Focus();
                        return;
                    }
                    StockGRNData obj = new StockGRNData();
                    obj.ItemID = Convert.ToInt32(ItemID.Text);
                    obj.SupplierID = Convert.ToInt64(SupplierID.Text);
                    obj.OrderedQty = Convert.ToInt32(OrderedQty.Text);
                    obj.CP = Convert.ToDecimal(Price.Text);
                    obj.RecvQty = Convert.ToInt32(RecdQty.Text);
                    List.Add(obj);
                }
                objrec.XMLData = XmlConvertor.POCrossCheckRecordDatatoXML(List).ToString();
                objrec.TotalQuantity = Convert.ToInt32(txt_totalorderedqty.Text == "" ? "0" : txt_totalorderedqty.Text);
                objrec.TotalRecdQty = Convert.ToInt32(txt_totalrecdqty.Text == "" ? "0" : txt_totalrecdqty.Text);
                objrec.TotalCP = Convert.ToDecimal(txt_totalprice.Text == "" ? "0" : txt_totalprice.Text);
                objrec.PONo = txtPONo.Text == "" ? null : txtPONo.Text;
                if (ddl_verifyby.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "verifyby", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_verifyby.Focus();
                    return;
                }
                objrec.EmployeeID = Convert.ToInt64(ddl_verifyby.SelectedValue == "" ? null : ddl_verifyby.SelectedValue);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;



                int result = objBO.UpdatePOCrossChecK(objrec);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "verify", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btn_verify.Attributes["disabled"] = "disabled";
                    txtPONo.Text = "";
                    txtPONo.Focus();

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        protected void txt_recdqty_TextChanged(object sender, EventArgs e)
        {
            int Lastindex = gvpocrosschecklist.Rows.Count - 1;
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            txt_totalrecdqty.Text = "0";
            txt_totalprice.Text = "0.00";
            foreach (GridViewRow row in gvpocrosschecklist.Rows)
            {
                Label qty1 = (Label)gvpocrosschecklist.Rows[Lastindex].Cells[0].FindControl("lbl_qty");
                TextBox qty = (TextBox)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("txt_recdqty");
                TextBox CPperQty = (TextBox)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("txt_cpperqty");
                Label CP = (Label)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalprice");
                CP.Text = ((Convert.ToDecimal(CPperQty.Text)) * (Convert.ToInt32(qty.Text))).ToString();
                txt_totalrecdqty.Text = (Convert.ToInt32(txt_totalrecdqty.Text == "" ? "0" : txt_totalrecdqty.Text) + Convert.ToInt32(qty.Text)).ToString();
                txt_totalprice.Text = (Convert.ToDecimal(txt_totalprice.Text == "" ? "0" : txt_totalprice.Text) + Convert.ToDecimal(CP.Text)).ToString();


                if (Lastindex > row.RowIndex)
                {
                    TextBox Qty1 = (TextBox)gvpocrosschecklist.Rows[row.RowIndex + 1].Cells[0].FindControl("txt_recdqty");
                    Qty1.Focus();
                }
                else if (Lastindex == row.RowIndex)
                {
                    TextBox Qty2 = (TextBox)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("txt_recdqty");
                    Qty2.Focus();
                }
            }
        }

        protected void txt_cpperqty_TextChanged(object sender, EventArgs e)
        {
            int Lastindex = gvpocrosschecklist.Rows.Count - 1;
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            txt_totalrecdqty.Text = "0";
            txt_totalprice.Text = "0.00";
            foreach (GridViewRow row in gvpocrosschecklist.Rows)
            {
                Label orderedqty = (Label)gvpocrosschecklist.Rows[Lastindex].Cells[0].FindControl("lbl_qty");
                TextBox actualrecdqty = (TextBox)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("txt_recdqty");
                TextBox CPperQty = (TextBox)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("txt_cpperqty");
                Label CP = (Label)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalprice");
                CP.Text = ((Convert.ToDecimal(CPperQty.Text)) * (Convert.ToInt32(actualrecdqty.Text))).ToString();
                txt_totalrecdqty.Text = (Convert.ToInt32(txt_totalrecdqty.Text == "" ? "0" : txt_totalrecdqty.Text) + Convert.ToInt32(actualrecdqty.Text)).ToString();
                txt_totalprice.Text = (Convert.ToDecimal(txt_totalprice.Text == "" ? "0" : txt_totalprice.Text) + Convert.ToDecimal(CP.Text)).ToString();


                if (Lastindex > row.RowIndex)
                {
                    TextBox Qty1 = (TextBox)gvpocrosschecklist.Rows[row.RowIndex + 1].Cells[0].FindControl("txt_cpperqty");
                    Qty1.Focus();
                }
                else if (Lastindex == row.RowIndex)
                {
                    TextBox Qty2 = (TextBox)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("txt_cpperqty");
                    Qty2.Focus();
                }
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtPONo.Text = "";
            gvpocrosschecklist.DataSource = null;
            gvpocrosschecklist.DataBind();
            gvpocrosschecklist.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            lblresult1.Visible = false;
            btn_verify.Attributes["disabled"] = "disabled";
            txt_totalorderedqty.Text = "0";
            txt_totalrecdqty.Text = "0";
            txt_totalprice.Text = "0.0";
        

        }

        protected void bindgrid()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }


                List<StockGRNData> obj = GetPurchseOrderList(0);
                if (obj.Count > 0)
                {
                    gvpocrosschecklist.DataSource = obj;
                    gvpocrosschecklist.DataBind();
                    gvpocrosschecklist.Visible = true;
                    foreach (GridViewRow row in gvpocrosschecklist.Rows)
                    {
                        Label qty1 = (Label)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_qty");
                        TextBox qty = (TextBox)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("txt_recdqty");
                        TextBox CPperQty = (TextBox)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("txt_cpperqty");
                        Label CP = (Label)gvpocrosschecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalprice");
                        CP.Text = ((Convert.ToDecimal(CPperQty.Text)) * (Convert.ToInt32(qty.Text))).ToString();
                        txt_totalorderedqty.Text = (Convert.ToInt32(txt_totalorderedqty.Text == "" ? "0" : txt_totalorderedqty.Text) + Convert.ToInt32(qty1.Text)).ToString();
                        txt_totalrecdqty.Text = (Convert.ToInt32(txt_totalrecdqty.Text == "" ? "0" : txt_totalrecdqty.Text) + Convert.ToInt32(qty.Text)).ToString();
                        txt_totalprice.Text = (Convert.ToDecimal(txt_totalprice.Text == "" ? "0" : txt_totalprice.Text) + Convert.ToDecimal(CP.Text)).ToString();
                    }
                    btn_verify.Attributes.Remove("disabled");
                    txtPONo.Focus();
                }
                else 
                {
                    gvpocrosschecklist.DataSource = null;
                    gvpocrosschecklist.DataBind();
                    gvpocrosschecklist.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "CheckedPO", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }

        private List<StockGRNData> GetPurchseOrderList(int p)
        {
            StockGRNData objstock = new StockGRNData();
            POCrossCheckBO objBO = new POCrossCheckBO();
            objstock.PONo = txtPONo.Text.ToString() == "" ? "" : txtPONo.Text.ToString();
            return objBO.GetPurchseOrderList(objstock);

        }

        protected void btnsearch1_Click(object sender, EventArgs e)
        {
            bindgrid1();
        }
        protected void bindgrid1()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "SearchEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_from.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_from.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_from.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_To.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_To.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_To.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<StockGRNData> objdeposit = GetPOCheckedList(0);
                if (objdeposit.Count > 0)
                {
                    gvpocheckedlist.DataSource = objdeposit;
                    gvpocheckedlist.DataBind();
                    gvpocheckedlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found", 1);
                    div3.Attributes["class"] = "SucessAlert";
                    div3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    txtPONo.Text = "";
                }
                else
                {
                    div3.Visible = false;
                    gvpocheckedlist.DataSource = null;
                    gvpocheckedlist.DataBind();
                    gvpocheckedlist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        public List<StockGRNData> GetPOCheckedList(int curIndex)
        {
            StockGRNData objstock = new StockGRNData();
            POCrossCheckBO objBO = new POCrossCheckBO();
            objstock.PONo = txtPO.Text.ToString() == "" ? "" : txtPO.Text.ToString();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetPOCheckedList(objstock);
        } 
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtPO.Text = "";
            txt_from.Text = "";
            txt_To.Text = "";
            lblresult.Visible = false;
            div2.Visible = false;
            gvpocheckedlist.DataSource = null;
            gvpocheckedlist.DataBind();
            gvpocheckedlist.Visible = false;
            lblmessage1.Visible = false;
            divmsg2.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected DataTable GetDatafromDatabase()
        {
            List<StockGRNData> DepositDetails = GetPOCheckedList(0);
            List<PurchaseCheckedListDataTOeXCEL> ListexcelData = new List<PurchaseCheckedListDataTOeXCEL>();
            int i = 0;
            foreach (StockGRNData row in DepositDetails)
            {
                PurchaseCheckedListDataTOeXCEL Ecxeclpat = new PurchaseCheckedListDataTOeXCEL();
                Ecxeclpat.PONo = DepositDetails[i].PONo;
                Ecxeclpat.OrderedQty = DepositDetails[i].OrderedQty;
                Ecxeclpat.RecdQty = DepositDetails[i].TotalRecdQty;
                Ecxeclpat.CP = DepositDetails[i].CP;
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        protected void gvpocheckedlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "DeleteEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage1.Visible = false;
                    }
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvpocheckedlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label pono = (Label)gr.Cells[0].FindControl("lbl_pono");
                    Label cp = (Label)gr.Cells[0].FindControl("lbl_totalprice");
                    Label OrderedQty = (Label)gr.Cells[0].FindControl("lbl_qty");
                    Label recdqty = (Label)gr.Cells[0].FindControl("lbl_recdqty");

                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        div3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.ID = Convert.ToInt64(ID.Text);
                    objbill.PONo = pono.Text;
                    objbill.OrderedQty = Convert.ToInt32(OrderedQty.Text);
                    objbill.TotalRecdQty = Convert.ToInt32(recdqty.Text);

                    objbill.CP = Convert.ToDecimal(cp.Text);


                    objbill.EmployeeID = LogData.EmployeeID;

                    int Result = objstdBO.DeletePOCheckLIstByID(objbill);
                    if (Result == 1)
                    {
                        bindgrid1();
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage1, "system", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage1, "system", 0);
            }
        }
        public class ListtoDataTableConverter
        {

            public DataTable ToDataTable<T>(List<T> items)
            {

                DataTable dataTable = new DataTable(typeof(T).Name);

                // Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {

                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);

                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {

                        //       inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);

                    }

                    dataTable.Rows.Add(values);

                }

                //     put a breakpoint here and check datatable

                return dataTable;

            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage1, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage1.Visible = false;
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
                Messagealert_.ShowMessage(lblresult1, "ExportType", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvpocheckedlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvpocheckedlist.Columns[8].Visible = false;
                    gvpocheckedlist.Columns[9].Visible = false;
                    gvpocheckedlist.Columns[10].Visible = false;

                    gvpocheckedlist.RenderControl(hw);
                    gvpocheckedlist.HeaderRow.Style.Add("width", "15%");
                    gvpocheckedlist.HeaderRow.Style.Add("font-size", "10px");
                    gvpocheckedlist.Style.Add("text-decoration", "none");
                    gvpocheckedlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvpocheckedlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ItemCheckList.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
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
                wb.Worksheets.Add(dt, "Item CheckList");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ItemCheckList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult1, "Exported", 1);
                divmsg2.Attributes["class"] = "SucessAlert";
            }
        }
        protected void gvpocheckedlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpocheckedlist.PageIndex = e.NewPageIndex;
            bindgrid1();
        }
    }
}