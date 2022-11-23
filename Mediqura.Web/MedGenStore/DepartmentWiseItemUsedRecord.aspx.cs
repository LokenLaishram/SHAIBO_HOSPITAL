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
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;

namespace Mediqura.Web.MedGenStore
{
    public partial class DepartmentWiseItemUsedRecord : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                btnsave.Attributes["disabled"] = "disabled";
                Session["UsedItemList"] = null;
                txt_totqty.Text = "0";
                btnprints.Attributes["disabled"] = "disabled";
                btn_print.Attributes["disabled"] = "disabled";
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetGestockByDesignationforIndent(LogData.DesignationID, LogData.EmployeeID));
            ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
            Commonfunction.PopulateDdl(ddl_substocklist, mstlookup.GetGestockByDesignationforIndent(LogData.DesignationID, LogData.EmployeeID));
            ddl_substocklist.SelectedValue = LogData.GenSubStockID.ToString();
            txtItemName.Focus();
            if (LogData.DesignationID == 93 || LogData.DesignationID == 20 || LogData.DesignationID == 122 || LogData.DesignationID == 25 || LogData.RoleID == 1)
            {
                ddl_substock.Attributes.Remove("disabled");
                ddl_substocklist.Attributes.Remove("disabled");
            }
            else
            {
                ddl_substock.Attributes["disabled"] = "disabled";
                ddl_substocklist.Attributes["disabled"] = "disabled";
            }
            if (LogData.RoleID == 1 || LogData.RoleID == 25)
            {
                Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.GenStockType));
                ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
                Commonfunction.PopulateDdl(ddl_substocklist, mstlookup.GetLookupsList(LookupName.GenStockType));
                ddl_substocklist.SelectedValue = LogData.GenSubStockID.ToString();
                ddl_substocklist.Attributes.Remove("disabled");
                ddl_substock.Attributes.Remove("disabled");
            }
            AutoCompleteExtender2.ContextKey = ddl_substock.SelectedValue;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            GenDeptWiseUsedItemData Objpaic = new GenDeptWiseUsedItemData();
            GenDeptWiseUsedItemBO objInfoBO = new GenDeptWiseUsedItemBO();
            List<GenDeptWiseUsedItemData> getResult = new List<GenDeptWiseUsedItemData>();
            Objpaic.ItemName = prefixText;
            Objpaic.GenStockID = Convert.ToInt64(contextKey == "" ? "0" : contextKey);
            getResult = objInfoBO.GetItemNameListInStore(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetRecordNo(string prefixText, int count, string contextKey)
        {
            GenDeptWiseUsedItemData Objpaic = new GenDeptWiseUsedItemData();
            GenDeptWiseUsedItemBO objInfoBO = new GenDeptWiseUsedItemBO();
            List<GenDeptWiseUsedItemData> getResult = new List<GenDeptWiseUsedItemData>();
            Objpaic.RecordNo = prefixText;
            getResult = objInfoBO.GetRecordNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RecordNo);
            }
            return list;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            additem();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetIPpatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        private void additem()
        {
            if (ddl_substock.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "GenSubStock", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_substock.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txtItemName.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                txtItemName.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtItemName.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_qty.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Quantity", 0);
                txt_qty.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_qty.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_remarks.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
                txt_qty.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_remarks.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            List<GenDeptWiseUsedItemData> UsedItemList = Session["UsedItemList"] == null ? new List<GenDeptWiseUsedItemData>() : (List<GenDeptWiseUsedItemData>)Session["UsedItemList"];
            GenDeptWiseUsedItemData objStock = new GenDeptWiseUsedItemData();
            string source = txtItemName.Text.ToString();
            string StockNo = source.Substring(source.LastIndexOf(':') + 1);
            string avail = source.Split('>', '#')[1];
            if (source.Contains(":"))
            {
                // Check Duplicate data 
                foreach (GridViewRow row in gvDeptwiseRecord.Rows)
                {
                    Label SubTockID = (Label)gvDeptwiseRecord.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    if (Convert.ToInt64(SubTockID.Text) == Convert.ToInt64(StockNo == "" ? "0" : StockNo))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtItemName.Focus();
                        txtItemName.Text = "";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
            }
            else
            {
                txtItemName.Text = "";
                return;
            }
            GenDeptWiseUsedItemData Objpaic = new GenDeptWiseUsedItemData();
            GenDeptWiseUsedItemBO objInfoBO = new GenDeptWiseUsedItemBO();
            List<GenDeptWiseUsedItemData> getResult = new List<GenDeptWiseUsedItemData>();
            Objpaic.SubStockID = Convert.ToInt64(StockNo == "" ? "0" : StockNo);
            getResult = objInfoBO.GetItemDetailsByItemID(Objpaic);
            if (getResult.Count > 0)
            {
                objStock.BalStock = getResult[0].BalStock;
                if (Convert.ToInt32(txt_qty.Text.Trim() == "" ? "0" : txt_qty.Text.Trim()) > objStock.BalStock)
                {
                    Messagealert_.ShowMessage(lblmessage, "UseRecord", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_qty.Text = "";
                    txt_qty.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            objStock.ItemID = getResult[0].ItemID;
            objStock.ItemName = getResult[0].ItemName;
            objStock.SubStockID = Convert.ToInt64(StockNo == "" ? "0" : StockNo);
            objStock.QtyUsed = Convert.ToInt32(txt_qty.Text.Trim() == "" ? "0" : txt_qty.Text.Trim());
            objStock.Remarks = txt_remarks.Text.Trim();
            txt_totqty.Text = (Convert.ToInt32(txt_totqty.Text.Trim() == "" ? "0" : txt_totqty.Text.Trim()) + Convert.ToInt32(txt_qty.Text.Trim() == "" ? "0" : txt_qty.Text.Trim())).ToString();
            string patName = txt_PatientName.Text.ToString();
            if (patName.Contains(':'))
            {
                objStock.PatientName = patName;
                // objStock.IPno = patName.Substring(source.LastIndexOf(':') + 1);
            }
            UsedItemList.Add(objStock);
            if (UsedItemList.Count > 0)
            {
                gvDeptwiseRecord.DataSource = UsedItemList;
                gvDeptwiseRecord.DataBind();
                gvDeptwiseRecord.Visible = true;
                Session["UsedItemList"] = UsedItemList;
                txt_qty.Text = "";
                txtItemName.Focus();
                txtItemName.Text = "";
                txt_PatientName.Text = "";
                txt_remarks.Text = "";
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                gvDeptwiseRecord.DataSource = null;
                gvDeptwiseRecord.DataBind();
                gvDeptwiseRecord.Visible = true;
            }
        }
        protected void clearall()
        {
            txtItemName.Text = "";
            txt_totalusedqty.Text = "";
            txt_qty.Text = "";
        }
        protected void gvIndentRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
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
            List<GenDeptWiseUsedItemData> ListStock = new List<GenDeptWiseUsedItemData>();
            GenDeptWiseUsedItemData objStock = new GenDeptWiseUsedItemData();
            GenDeptWiseUsedItemBO objBO = new GenDeptWiseUsedItemBO();
            try
            {
                foreach (GridViewRow row in gvDeptwiseRecord.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ItemID = (Label)gvDeptwiseRecord.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label usedqty = (Label)gvDeptwiseRecord.Rows[row.RowIndex].Cells[0].FindControl("lbl_usedqty");
                    Label substockid = (Label)gvDeptwiseRecord.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    Label patName = (Label)gvDeptwiseRecord.Rows[row.RowIndex].Cells[0].FindControl("lbl_patientName");
                    Label Remarks = (Label)gvDeptwiseRecord.Rows[row.RowIndex].Cells[0].FindControl("lbl_remarks");

                    GenDeptWiseUsedItemData ObjDetails = new GenDeptWiseUsedItemData();
                    ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                    ObjDetails.QtyUsed = Convert.ToInt32(usedqty.Text == "" ? "0" : usedqty.Text);
                    ObjDetails.SubStockID = Convert.ToInt64(substockid.Text == "" ? "0" : substockid.Text);
                    ObjDetails.PatientName = patName.Text.Trim();
                    ObjDetails.Remarks = Remarks.Text.Trim();
                    ListStock.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.GEN_IndentDetailsDeptWiseDatatoXML(ListStock).ToString();
                objStock.GenStockID = Convert.ToInt64(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                objStock.TotalUsedQty = Convert.ToInt32(txt_totqty.Text.Trim() == "" ? "0" : txt_totqty.Text.Trim());
                objStock.HospitalID = LogData.HospitalID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;
                List<GenDeptWiseUsedItemData> result = new List<GenDeptWiseUsedItemData>();
                result = objBO.UpdateDepartmentWiseItemUsedRecordDetails(objStock);
                if (result != null)
                {
                    txt_recordno.Text = result[0].RecordNo.ToString();
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    Session["UsedItemList"] = null;
                    btnsave.Attributes["disabled"] = "disabled";
                    btnprints.Attributes.Remove("disabled");
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage, msg, 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvDeptwiseRecord.DataSource = null;
            gvDeptwiseRecord.DataBind();
            gvDeptwiseRecord.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            divmsg1.Visible = false;
            txtItemName.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            Session["UsedItemList"] = null;
            txtItemName.Focus();
            txt_totqty.Text = "";
            txt_recordno.Text = "";
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvDeptWiseItemlist.DataSource = null;
            gvDeptWiseItemlist.DataBind();
            gvDeptWiseItemlist.Visible = false;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            txt_from.Text = "";
            txt_To.Text = "";
            txt_recordno.Text = "";
            txt_totalusedqty.Text = "";
            ViewState["TotalReq"] = null;
            txt_patientNames.Text = "";
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
                //if (txt_from.Text == "")
                //{
                //    if (Commonfunction.isValidDate(txt_from.Text) == false)
                //    {
                //        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                //        divmsg2.Attributes["class"] = "FailAlert";
                //        divmsg2.Visible = true;
                //        txt_from.Focus();
                //        return;
                //    }
                //}
                //else
                //{
                //    divmsg2.Visible = false;
                //}
                //if (txt_To.Text == "")
                //{
                //    if (Commonfunction.isValidDate(txt_To.Text) == false)
                //    {
                //        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                //        divmsg2.Attributes["class"] = "FailAlert";
                //        divmsg2.Visible = true;
                //        txt_To.Focus();
                //        return;
                //    }
                //}
                //else
                //{
                //    divmsg2.Visible = false;
                //}
                List<GenDeptWiseUsedItemData> objdeposit = GetDeptWiseItemList(0);
                if (objdeposit.Count > 0)
                {
                    txt_totalusedqty.Text = Commonfunction.Getrounding(objdeposit[0].TotalUsedQty.ToString());
                    gvDeptWiseItemlist.DataSource = objdeposit;
                    gvDeptWiseItemlist.DataBind();
                    gvDeptWiseItemlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div3.Attributes["class"] = "SucessAlert";
                    div3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage.Visible = false;
                    btn_print.Attributes.Remove("disabled");
                }
                else
                {
                    gvDeptWiseItemlist.DataSource = null;
                    gvDeptWiseItemlist.DataBind();
                    gvDeptWiseItemlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    div3.Visible = false;
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
        public List<GenDeptWiseUsedItemData> GetDeptWiseItemList(int curIndex)
        {
            GenDeptWiseUsedItemData objstock = new GenDeptWiseUsedItemData();
            GenDeptWiseUsedItemBO objBO = new GenDeptWiseUsedItemBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.RecordNo = txtrecordNo.Text.ToString() == "" ? "" : txtrecordNo.Text.ToString();
            objstock.GenStockID = Convert.ToInt32(ddl_substocklist.SelectedValue == "" ? "0" : ddl_substocklist.SelectedValue);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.PatientName = txt_patientNames.Text == "" ? null : txt_patientNames.Text;
            string ItmID;
            string source = txt_item.Text.ToString();
            if (source.Contains(":"))
            {
                ItmID = source.Substring(source.LastIndexOf(':') + 1);
                objstock.ItemID = Convert.ToInt64(ItmID);

            }
            return objBO.GetDeptWiseItemList(objstock);
        }
        protected void gvIndentlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDeptWiseItemlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void gvDeptwiseRecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvDeptwiseRecord.Rows[i];
                    List<GenDeptWiseUsedItemData> UsedItemList = Session["UsedItemList"] == null ? new List<GenDeptWiseUsedItemData>() : (List<GenDeptWiseUsedItemData>)Session["UsedItemList"];
                    Label used = (Label)gr.Cells[0].FindControl("lbl_usedqty");
                    txt_totqty.Text = (Convert.ToInt32(txt_totqty.Text == "" ? "0" : txt_totqty.Text) - Convert.ToInt32(used.Text == "" ? "0" : used.Text)).ToString();
                    UsedItemList.RemoveAt(i);
                    Session["UsedItemList"] = UsedItemList;
                    gvDeptwiseRecord.DataSource = UsedItemList;
                    gvDeptwiseRecord.DataBind();

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }
        }
        protected void gvDeptWiseItemlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    GenDeptWiseUsedItemData Objpaic = new GenDeptWiseUsedItemData();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvDeptWiseItemlist.Rows[i];
                    Label recno = (Label)gr.Cells[0].FindControl("lbl_recordno");
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label Qty = (Label)gr.Cells[0].FindControl("lbl_usedqty");
                    Objpaic.RecordNo = recno.Text;
                    Objpaic.EmployeeID = LogData.EmployeeID;
                    Objpaic.QtyUsed = Convert.ToInt32(Qty.Text == "" ? "0" : Qty.Text);
                    Objpaic.ID = LogData.EmployeeID;
                    Objpaic.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Remarks", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";

                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        Objpaic.Remarks = txtremarks.Text;
                    }

                    GenDeptWiseUsedItemBO objInfoBO = new GenDeptWiseUsedItemBO();
                    int Result = objInfoBO.DeleteGenDeptWiseUsedItemDetailsByRecNo(Objpaic);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";

                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }
        }

        protected void gvIndentlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label IndentID = (Label)e.Row.FindControl("lbl_Indentno");
                Label status = (Label)e.Row.FindControl("lblReqTypestatus");
                if (status.Text.Contains("Urgency"))
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.YellowGreen;
                }
            }
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
                Messagealert_.ShowMessage(lblresult1, "ExportType", 0);
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvDeptWiseItemlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvIndentlist.Columns[4].Visible = false;
                    //gvIndentlist.Columns[5].Visible = false;
                    gvDeptWiseItemlist.Columns[7].Visible = false;
                    gvDeptWiseItemlist.Columns[8].Visible = false;

                    gvDeptWiseItemlist.RenderControl(hw);
                    gvDeptWiseItemlist.HeaderRow.Style.Add("width", "15%");
                    gvDeptWiseItemlist.HeaderRow.Style.Add("font-size", "10px");
                    gvDeptWiseItemlist.Style.Add("text-decoration", "none");
                    gvDeptWiseItemlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvDeptWiseItemlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StoreIndentRequestList.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Patient Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StoreIndentRequestList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private DataTable GetDatafromDatabase()
        {
            List<GenDeptWiseUsedItemData> DepositDetails = GetDeptWiseItemList(0);
            List<GenDeptWiseUsedItemToExcel> ListexcelData = new List<GenDeptWiseUsedItemToExcel>();
            int i = 0;
            foreach (GenDeptWiseUsedItemData row in DepositDetails)
            {
                GenDeptWiseUsedItemToExcel Ecxeclpat = new GenDeptWiseUsedItemToExcel();
                Ecxeclpat.RecordNo = DepositDetails[i].RecordNo;
                Ecxeclpat.ItemName = DepositDetails[i].ItemName;
                Ecxeclpat.TotalUsedQty = DepositDetails[i].TotalUsedQty;
                Ecxeclpat.AddedDate = DepositDetails[i].AddedDate;
                Ecxeclpat.AddedBy = DepositDetails[i].AddedBy;


                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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
        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            txt_qty.Focus();
        }

        protected void txt_qty_TextChanged(object sender, EventArgs e)
        {
            additem();
        }

        protected void ddl_substock_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddl_substock.SelectedValue;
        }
    }
}