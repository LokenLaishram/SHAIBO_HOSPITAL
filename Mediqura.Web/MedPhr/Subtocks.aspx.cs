using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
    public partial class Subtocks : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();

            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.SubStockType));
            ddl_substock.SelectedValue = LogData.MedSubStockID.ToString();
            if (LogData.RoleID == 1)
            {
                ddl_substock.Attributes.Remove("disabled");
                ddl_substock.Attributes.Remove("disabled");
            }
            else
            {
                ddl_substock.Attributes["disabled"] = "disabled";
                ddl_substock.Attributes["disabled"] = "disabled";
            }
            Commonfunction.PopulateDdl(ddl_stocstaus, mstlookup.GetLookupsList(LookupName.StockStatus));
            ddl_stocstaus.SelectedIndex = 1;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            MedIndentData Objpaic = new MedIndentData();
            MedStoreIndentBO objInfoBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNameListInSubStore(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);

            }
            return list;
        }
        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            var source = txtItemName.Text.ToString();
            if (!source.Contains(":"))
            {
                txtItemName.Text = "";
                txtItemName.Focus();
                return;
            }
            bindgrid();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (ddl_substock.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "GenStock", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                List<MedIndentData> objdeposit = GetSubstockdetailist(0);
                if (objdeposit == null)
                {
                    txtItemName.Text = "";
                    txtItemName.Focus();
                    return;
                }
                if (objdeposit.Count > 0)
                {
                    gvDeptWise.DataSource = objdeposit;
                    gvDeptWise.DataBind();
                    gvDeptWise.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
                    lblmessage2.Visible = false;
                    btn_update.Visible = true;
                }
                else
                {
                    gvDeptWise.DataSource = null;
                    gvDeptWise.DataBind();
                    gvDeptWise.Visible = true;
                    lblresult.Visible = false;
                    div1.Visible = false;
                    btn_update.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<MedIndentData> GetSubstockdetailist(int curIndex)
        {
            List<MedIndentData> ret = null;
            MedIndentData objStock = new MedIndentData();
            MedStoreIndentBO objBO = new MedStoreIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            objStock.MedSubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
            objStock.StatusID = Convert.ToInt32(ddl_stocstaus.SelectedValue == "" ? "0" : ddl_stocstaus.SelectedValue);
            var source = txtItemName.Text.ToString();
            if (source.Contains(":"))
            {
                string ID = source.Substring(source.LastIndexOf(':') + 1);
                objStock.ItemID = Convert.ToInt32(ID);
            }
            else
            {
                objStock.ItemID = 0;
                txtItemName.Text = "";
                txtItemName.Focus();
            }
            DateTime from = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objStock.DateFrom = from;
            objStock.DateTo = To;
            return objBO.GetMedSubstockDetails(objStock);

        }
        protected void lbl_balance_OnTextChanged(object sender, EventArgs e)
        {
            divmsg2.Visible = false;
            lblmessage2.Text = "";
            TextBox balance = (TextBox)sender;
            GridViewRow GvRow = (GridViewRow)balance.NamingContainer;
            TextBox NoUnit = (TextBox)GvRow.Cells[0].FindControl("lbl_balance");
            Label lblEquivalentQtyPerUnit = (Label)GvRow.Cells[0].FindControl("lblEquivalentQtyPerUnit");
            if (Convert.ToInt32(lblEquivalentQtyPerUnit.Text) == 1 && NoUnit.Text.Contains("."))
            {
                Messagealert_.ShowMessage(lblmessage2, "Please enter valid integer.", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
                balance.Focus();
                return;
            }
            else
            {
                if (NoUnit.Text.Contains("."))
                {
                    string num = NoUnit.Text.Substring(NoUnit.Text.LastIndexOf('.') + 1);
                    string numright = NoUnit.Text.Substring(0, NoUnit.Text.IndexOf("."));
                    if (num.Length > 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Please enter valid integer or decimal number with 1 decimal places.", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        balance.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage2.Text = "";
                        divmsg2.Visible = false;

                    }
                }
            }
        }
        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                List<StockStatusData> Liststock = new List<StockStatusData>();
                StockStatusBO objLabSampleBO = new StockStatusBO();
                StockStatusData objSampleData = new StockStatusData();
                int chkvalidation1 = 0, chkvalidation2 = 0;
                foreach (GridViewRow row in gvDeptWise.Rows)
                {
                    Label StockID = (Label)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lblSubtockID");
                    TextBox NoUnit = (TextBox)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_balance");
                    TextBox EquivQty = (TextBox)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_Equibalance");
                    TextBox ExpiryDate = (TextBox)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_expirydate");
                    Label subheading = (Label)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_Subheading");
                    Label lblEquivalentQtyPerUnit = (Label)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lblEquivalentQtyPerUnit");

                    StockStatusData ObjDetails = new StockStatusData();
                    if (subheading.Text == "0")
                    {
                        ObjDetails.StockID = Convert.ToInt64(StockID.Text == "" ? "0" : StockID.Text);
                        ObjDetails.AvailableBalAfterUsed = Convert.ToDecimal(NoUnit.Text == "" ? "0" : NoUnit.Text);
                        ObjDetails.TotalQuantity = Convert.ToInt32(EquivQty.Text == "" ? "0" : EquivQty.Text);
                        ObjDetails.ExpireDates = ExpiryDate.Text == "" ? "" : ExpiryDate.Text;
                        if (Convert.ToInt32(lblEquivalentQtyPerUnit.Text) == 1 && NoUnit.Text.Contains("."))
                        {
                            chkvalidation1++;
                            NoUnit.Focus();
                            NoUnit.BorderColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            if (NoUnit.Text.Contains("."))
                            {
                                string num = NoUnit.Text.Substring(NoUnit.Text.LastIndexOf('.') + 1);
                                string numright = NoUnit.Text.Substring(0, NoUnit.Text.IndexOf("."));

                                if (num.Length > 1)
                                {
                                    chkvalidation2++;
                                    NoUnit.Focus();
                                    NoUnit.BorderColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    ObjDetails.Numpoint = num;
                                    ObjDetails.Numright = numright;
                                }
                            }

                            else
                            {
                                ObjDetails.Numpoint = "0";
                                ObjDetails.Numright = NoUnit.Text.Trim();
                            }
                            Liststock.Add(ObjDetails);
                        }
                    }
                }
                if (chkvalidation1 > 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "Please enter valid integer.", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    return;
                }
                if (chkvalidation2 > 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "Please enter valid integer or decimal number with 1 decimal places.", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    return;
                }
                objSampleData.XMLData = XmlConvertor.MedSubStockStatustoXML(Liststock).ToString();
                objSampleData.ActionType = Enumaction.Insert;

                int result = objLabSampleBO.UpdateSubstockStockDetails(objSampleData);
                if (result == 1)
                {
                    bindgrid();
                    lblmessage2.Visible = true;
                    Messagealert_.ShowMessage(lblmessage2, "save", 1);
                    divmsg2.Attributes["class"] = "SucessAlert";
                    divmsg2.Visible = true;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;

            }
        }
        protected void gvstockstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label StockStaus = (Label)e.Row.FindControl("lblstockstatus");
                TextBox Label1 = (TextBox)e.Row.FindControl("lbl_balance");
                Label lbl_substock = (Label)e.Row.FindControl("lblSubtockID");
                Label lbl_main = (Label)e.Row.FindControl("lbl_mainstock");
                Label lbl_itemname = (Label)e.Row.FindControl("lblitemname");
                Label lbl_recv = (Label)e.Row.FindControl("lblrec");
                Label lbl_used = (Label)e.Row.FindControl("lbl_used");
                Label lbl_return = (Label)e.Row.FindControl("lbl_return");
                TextBox lbl_equibal = (TextBox)e.Row.FindControl("lbl_Equibalance");
                Label lbl_recvdate = (Label)e.Row.FindControl("lbl_recddate");
                TextBox lbl_expirydate = (TextBox)e.Row.FindControl("lbl_expirydate");
                Label lbl_subhead = (Label)e.Row.FindControl("lbl_Subheading");

                if (StockStaus.Text == "1")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Green;
                    Label1.ForeColor = System.Drawing.Color.White;
                    Label1.BackColor = System.Drawing.Color.Green;
                }
                if (StockStaus.Text == "2")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
                    Label1.ForeColor = System.Drawing.Color.Black;
                    Label1.BackColor = System.Drawing.Color.Yellow;
                }
                if (StockStaus.Text == "3")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.White;
                    Label1.BackColor = System.Drawing.Color.Red;
                }
                if (lbl_recvdate.Text == "01/01/0001 12:00:00 AM" || lbl_recvdate.Text == "01/01/01 00:00:00" || lbl_recvdate.Text == "1/1/0001 12:00:00 AM")
                {
                    lbl_recvdate.Text = "";
                }
                if (lbl_expirydate.Text == "01/01/0001 12:00:00 AM" || lbl_expirydate.Text == "01/01/01 00:00:00" || lbl_expirydate.Text == "1/1/0001 12:00:00 AM")
                {
                    lbl_expirydate.Text = "";
                }
                if (lbl_subhead.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#33aa99");
                    lbl_itemname.ForeColor = System.Drawing.Color.White;
                    lbl_recv.ForeColor = System.Drawing.Color.White;
                    lbl_used.ForeColor = System.Drawing.Color.White;
                    lbl_return.ForeColor = System.Drawing.Color.White;
                    lbl_equibal.ForeColor = System.Drawing.Color.White;
                    Label1.ForeColor = System.Drawing.Color.White;
                    lbl_substock.Visible = false;
                    lbl_recvdate.Visible = false;
                    lbl_expirydate.Visible = false;
                    if (StockStaus.Text == "0")
                    {
                        e.Row.Cells[6].BackColor = System.Drawing.Color.Green;
                        Label1.ForeColor = System.Drawing.Color.Black;
                        Label1.BackColor = System.Drawing.Color.Green;
                        lbl_equibal.ForeColor = System.Drawing.Color.Black;
                    }

                }
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvDeptWise.DataSource = null;
            gvDeptWise.DataBind();
            gvDeptWise.Visible = false;
            txtItemName.Text = "";
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            txt_datefrom.Text = "";
            txt_dateto.Text = "";
            btn_update.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                List<StockStatusData> Liststock = new List<StockStatusData>();
                StockStatusBO objLabSampleBO = new StockStatusBO();
                StockStatusData objSampleData = new StockStatusData();
                int chkvalidation1 = 0, chkvalidation2 = 0;
                foreach (GridViewRow row in gvDeptWise.Rows)
                {
                    Label StockID = (Label)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lblSubtockID");
                    TextBox NoUnit = (TextBox)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_balance");
                    TextBox EquivQty = (TextBox)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_Equibalance");
                    TextBox ExpiryDate = (TextBox)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_expirydate");
                    Label subheading = (Label)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_Subheading");
                    Label lblEquivalentQtyPerUnit = (Label)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lblEquivalentQtyPerUnit");
                    StockStatusData ObjDetails = new StockStatusData();
                    if (subheading.Text == "0")
                    {
                        ObjDetails.StockID = Convert.ToInt64(StockID.Text == "" ? "0" : StockID.Text);
                        ObjDetails.AvailableBalAfterUsed = Convert.ToDecimal(NoUnit.Text == "" ? "0" : NoUnit.Text);
                        ObjDetails.TotalQuantity = Convert.ToInt32(EquivQty.Text == "" ? "0" : EquivQty.Text);
                        ObjDetails.ExpireDates = ExpiryDate.Text == "" ? "" : ExpiryDate.Text;
                        if (Convert.ToInt32(lblEquivalentQtyPerUnit.Text) == 1 && NoUnit.Text.Contains("."))
                        {
                            chkvalidation1++;
                            NoUnit.Focus();
                            NoUnit.BorderColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            if (NoUnit.Text.Contains("."))
                            {
                                string num = NoUnit.Text.Substring(NoUnit.Text.LastIndexOf('.') + 1);
                                string numright = NoUnit.Text.Substring(0, NoUnit.Text.IndexOf("."));

                                if (num.Length > 1)
                                {
                                    chkvalidation2++;
                                    NoUnit.Focus();
                                    NoUnit.BorderColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    ObjDetails.Numpoint = num;
                                    ObjDetails.Numright = numright;
                                }
                            }

                            else
                            {
                                ObjDetails.Numpoint = "0";
                                ObjDetails.Numright = NoUnit.Text.Trim();
                            }
                            Liststock.Add(ObjDetails);
                        }
                    }
                }
                if (chkvalidation1 > 0)
                {
                    Messagealert_.ShowMessage(lblresult, "Please enter valid integer.", 0);
                    div1.Attributes["class"] = "FailAlert";
                    div1.Visible = true;
                    return;
                }
                if (chkvalidation2 > 0)
                {
                    Messagealert_.ShowMessage(lblresult, "Please enter valid integer or decimal number with 1 decimal places.", 0);
                    div1.Attributes["class"] = "FailAlert";
                    div1.Visible = true;
                    return;
                }
                objSampleData.XMLData = XmlConvertor.MedSubStockStatustoXML(Liststock).ToString();
                objSampleData.ActionType = Enumaction.Insert;

                int result = objLabSampleBO.UpdateSubstockStockDetails(objSampleData);
                if (result == 1)
                {
                    bindgrid();
                    lblmessage2.Visible = true;
                    Messagealert_.ShowMessage(lblmessage2, "save", 1);
                    divmsg2.Attributes["class"] = "SucessAlert";
                    divmsg2.Visible = true;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;

            }
        }
    }
}