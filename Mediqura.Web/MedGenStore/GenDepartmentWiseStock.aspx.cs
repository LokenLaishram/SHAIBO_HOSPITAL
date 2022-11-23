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
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.BOL.MedGenStoreBO;

namespace Mediqura.Web.MedGenStore
{
    public partial class GenDepartmentWiseStock : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                btn_update.Attributes["disabled"] = "disabled";
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetGestockByDesignationforIndent(LogData.DesignationID, LogData.EmployeeID));
            ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
            txtItemName.Focus();
            if (LogData.DesignationID == 93 || LogData.DesignationID == 20 || LogData.DesignationID == 122 || LogData.DesignationID == 25 || LogData.RoleID == 1)
            {
                ddl_substock.Attributes.Remove("disabled");
            }
            else
            {
                ddl_substock.Attributes["disabled"] = "disabled";
            }
            if (LogData.RoleID == 1 || LogData.RoleID == 25)
            {
                Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.GenStockType));
                ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
                ddl_substock.Attributes.Remove("disabled");
            }
            AutoCompleteExtender2.ContextKey = ddl_substock.SelectedValue;
            Commonfunction.PopulateDdl(ddl_stocstaus, mstlookup.GetLookupsList(LookupName.StockStatus));
            ddl_stocstaus.SelectedIndex = 1;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            GenIndentData Objpaic = new GenIndentData();
            GenIndentBO objInfoBO = new GenIndentBO();
            List<GenIndentData> getResult = new List<GenIndentData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNameListInStore(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);

            }
            return list;
        }
        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
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
                List<GenIndentData> objdeposit = GetIndentItemList(0);
                if (objdeposit.Count > 0)
                {

                    gvDeptWise.DataSource = objdeposit;
                    gvDeptWise.DataBind();
                    gvDeptWise.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
                    btn_update.Visible = true;
                    btn_update.Attributes.Remove("disabled");
                    lblmessage2.Visible = false;

                }
                else
                {
                    gvDeptWise.DataSource = null;
                    gvDeptWise.DataBind();
                    gvDeptWise.Visible = true;
                    lblresult.Visible = false;
                    div1.Visible = false;
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
        public List<GenIndentData> GetIndentItemList(int curIndex)
        {
            GenIndentData objstock = new GenIndentData();
            GenIndentBO objBO = new GenIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
            objstock.StatusID = Convert.ToInt32(ddl_stocstaus.SelectedValue == "" ? "0" : ddl_stocstaus.SelectedValue);
            var source = txtItemName.Text.ToString();
            if (source.Contains(":"))
            {
                string ID = source.Substring(source.LastIndexOf(':') + 1);
                objstock.ItemID = Convert.ToInt32(ID);
            }
            else
            {
                objstock.ItemID = 0;
            }
            DateTime from = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = To;
            return objBO.GetDeptStockList(objstock);
        }
        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "SaveEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }
            List<GenIndentData> ListStock = new List<GenIndentData>();
            GenIndentData objStock = new GenIndentData();
            GenIndentBO objBO = new GenIndentBO();
            try
            {
                int itemcount = 0;
                foreach (GridViewRow row in gvDeptWise.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label SubStock = (Label)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_subStock1");
                    Label stockNo = (Label)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_StockNo");
                    Label avail = (Label)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("lbl_available");
                    TextBox Condemn = (TextBox)gvDeptWise.Rows[row.RowIndex].Cells[0].FindControl("txtcondemnquantity");
                    GenIndentData ObjDetails = new GenIndentData();

                    ObjDetails.SubStockID = Convert.ToInt64(SubStock.Text == "" ? "0" : SubStock.Text);
                    ObjDetails.StockNo = stockNo.Text == "" ? null : stockNo.Text;
                    if (Convert.ToInt32(Condemn.Text == "" ? "0" : Condemn.Text) > Convert.ToInt32(avail.Text == "" ? "0" : avail.Text))
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Condemn Qty should not be greater than Available.", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        return;
                    }
                    else
                    {
                        ObjDetails.CondemnQty = Convert.ToInt32(Condemn.Text == "" ? "0" : Condemn.Text);
                    }
                    itemcount = itemcount + 1;
                    ListStock.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.GEN_DeptStockDatatoXML(ListStock).ToString();
                objStock.HospitalID = LogData.HospitalID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                int result = objBO.UpdateDeptStock(objStock);
                if (result == 1)
                {
                    bindgrid();
                    lblmessage2.Visible = true;
                    Messagealert_.ShowMessage(lblmessage2, "update", 1);
                    divmsg2.Attributes["class"] = "SucessAlert";
                    divmsg2.Visible = true;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage2, msg, 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
            }
        }
        protected void gvstockstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label StockStaus = (Label)e.Row.FindControl("lblstockstatus");
                Label Label1 = (Label)e.Row.FindControl("lbl_available");
                if (StockStaus.Text == "1")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Green;
                    Label1.ForeColor = System.Drawing.Color.White;
                }
                if (StockStaus.Text == "2")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                    Label1.ForeColor = System.Drawing.Color.Black;
                }
                if (StockStaus.Text == "3")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.White;
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
        }
    }
}