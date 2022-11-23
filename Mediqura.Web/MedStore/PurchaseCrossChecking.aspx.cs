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

namespace Mediqura.Web.MedStore
{
    public partial class PurchaseCrossChecking : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDdl();
                txtTab2_ReqNo.Attributes["disabled"] = "disabled";
                txtTab2_ReqDate.Attributes["disabled"] = "disabled";
                txtTab2_ApprovedBy.Attributes["disabled"] = "disabled";
                txtTab2_ApprovedDate.Attributes["disabled"] = "disabled";
                txtTab2_Supplier.Attributes["disabled"] = "disabled";
                txtTab2_PONumber.Attributes["disabled"] = "disabled";
                btnTab2Save.Attributes["disabled"] = "disabled";
                btnTab2Print.Attributes["disabled"] = "disabled";
            }
        }
        private void BindDdl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_Supplier, mstlookup.GetLookupsList(LookupName.GenSupplier));
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetRQNumberAuto(string prefixText, int count, string contextKey)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            List<PurchaseRequisitionData> getResult = new List<PurchaseRequisitionData>();
            ObjPurReqData.RQNumber = prefixText;
            getResult = ObjPurReqBO.GetRQNumberAuto(ObjPurReqData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RQNumber.ToString());
            }
            return list;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPONumberAuto(string prefixText, int count, string contextKey)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            List<PurchaseRequisitionData> getResult = new List<PurchaseRequisitionData>();
            ObjPurReqData.PONumber = prefixText;
            getResult = ObjPurReqBO.GetPONumberAuto(ObjPurReqData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PONumber.ToString());
            }
            return list;
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindGridTab1(1);
        }

        protected void BindGridTab1(int page)
        {
            try
            {
                List<PurchaseRequisitionData> ObjPurReqList = GetPurchaseOrderList(page);
                if (ObjPurReqList.Count > 0)
                {           
                    GvPurchaseOrderList.VirtualItemCount = ObjPurReqList[0].MaximumRows;//total item is required for custom paging
                    GvPurchaseOrderList.PageIndex = page - 1;
                    GvPurchaseOrderList.DataSource = ObjPurReqList;
                    GvPurchaseOrderList.DataBind();
                    GvPurchaseOrderList.Visible = true;
                    Messagealert_.ShowMessage(lblResult, "Total:" + ObjPurReqList[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;    
                }
                else
                {
                    GvPurchaseOrderList.DataSource = null;
                    GvPurchaseOrderList.DataBind();
                    GvPurchaseOrderList.Visible = true;
                    lblResult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        public List<PurchaseRequisitionData> GetPurchaseOrderList(int curIndex)
        {
            PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
            PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ObjPurReqData.RQNumber = txt_RequisitionNo.Text == "" ? "0" : txt_RequisitionNo.Text.Trim();
            ObjPurReqData.PONumber = txt_PONumber.Text == "" ? "0" : txt_PONumber.Text.Trim();
            DateTime From = txt_DateFrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_DateFrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_DateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_DateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjPurReqData.DateFrom = From;
            ObjPurReqData.DateTo = To;
            ObjPurReqData.SupplierID = Convert.ToInt32(ddl_Supplier.SelectedValue == "" ? "0" : ddl_Supplier.SelectedValue);
            ObjPurReqData.RecievedStatus = Convert.ToInt32(ddl_Status.SelectedValue == "" ? "0" : ddl_Status.SelectedValue);
            return ObjPurReqBO.GetPurchaseOrderList(ObjPurReqData);
        }

        protected void GvPurchaseOrderList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {
                    PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();
                    PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPurchaseOrderList.Rows[i];
                    LinkButton RQNumber = (LinkButton)gr.Cells[0].FindControl("lnkRequisitionNo");
                    ObjPurReqData.RQNumber = RQNumber.Text;
                    List<PurchaseRequisitionData> List = new List<PurchaseRequisitionData>();
                    List = ObjPurReqBO.GetPurchaseOrderListByRQNumber(ObjPurReqData);
                    if (List.Count > 0)
                    {
                        txtTab2_ReqNo.Text = List[0].RQNumber.ToString();
                        txtTab2_ReqDate.Text = List[0].RequestedDate.ToString();
                        txtTab2_ApprovedBy.Text = List[0].ApprovedBy.ToString();
                        txtTab2_ApprovedDate.Text = List[0].ApprovedDate.ToString();
                        txtTab2_Supplier.Text = List[0].SupplierName.ToString();
                        txtTab2_PONumber.Text = List[0].PONumber.ToString();
                        TabContainerPurchaseOrderCrossChecking.ActiveTabIndex = 1;
                        GvPurchaseOrderChecking.DataSource = List;
                        GvPurchaseOrderChecking.DataBind();
                        GvPurchaseOrderChecking.Visible = true;                         
                        btnTab2Save.Attributes.Remove("disabled");
                        Messagealert_.ShowMessage(lblResultTab2, "Total: " + List[0].MaximumRows.ToString() + " Record(s) found.", 1);
                        div2.Visible = true;
                        div2.Attributes["class"] = "SucessAlert";
                    }
                    else
                    {
                        GvPurchaseOrderChecking.DataSource = null;
                        GvPurchaseOrderChecking.DataBind();
                        GvPurchaseOrderChecking.Visible = true;
                        lblResultTab2.Visible = false;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessageTab2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
                return;
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            Tab1Reset();
        }
        protected void Tab1Reset()
        {
            txt_RequisitionNo.Text = "";
            txt_PONumber.Text = "";
            txt_DateFrom.Text = "";
            txt_DateTo.Text = "";
            ddl_Supplier.SelectedValue = "0";
            GvPurchaseOrderList.DataSource = null;
            GvPurchaseOrderList.DataBind();
            GvPurchaseOrderList.Visible = false;
            divmsg3.Visible = false;
            lblResult.Text = "";
            divmsg1.Visible = false;
            lblmessage.Text = "";
        }


        //------------------------------------- End Tab 1 ---------------------------------------
        //------------------------------------- Start Tab 2 ---------------------------------------
        protected void btnTab2Save_Click(object sender, EventArgs e)
        {
            if (GvPurchaseOrderChecking.Rows.Count != 0)
            {
                List<PurchaseRequisitionData> PurchaseOrderRecievedList = new List<PurchaseRequisitionData>();
                PurchaseRequisitionBO ObjPurReqBO = new PurchaseRequisitionBO();
                PurchaseRequisitionData ObjPurReqData = new PurchaseRequisitionData();

                try
                {
                    foreach (GridViewRow row in GvPurchaseOrderChecking.Rows)
                    {
                        Label ItemID = (Label)GvPurchaseOrderChecking.Rows[row.RowIndex].Cells[0].FindControl("lblTab2ItemID");
                        TextBox RecievedQnty = (TextBox)GvPurchaseOrderChecking.Rows[row.RowIndex].Cells[0].FindControl("txtTab2RecievedQuantity");
                        Label ApprovedQnty = (Label)GvPurchaseOrderChecking.Rows[row.RowIndex].Cells[0].FindControl("lblTab2ApprovedQuantity");

                        PurchaseRequisitionData ObjPurReqList = new PurchaseRequisitionData();
                        ObjPurReqList.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                        ObjPurReqList.RecievedQuantity = Convert.ToInt32(RecievedQnty.Text == "" ? "0" : RecievedQnty.Text);
                        if (Convert.ToInt32(ApprovedQnty.Text) < Convert.ToInt32(RecievedQnty.Text))   
                        {
                            Messagealert_.ShowMessage(lblmessage, "Recieved Quantity shouldn't be greater than Aproved Quantity", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                            return;
                        }
                        if (ApprovedQnty.Text == RecievedQnty.Text)
                        {
                            ObjPurReqList.RecievedStatus = 1;
                        }
                        else
                        {
                            ObjPurReqList.RecievedStatus = 0;
                        }
                        ObjPurReqData.RQNumber = txtTab2_ReqNo.Text.Trim();
                        ObjPurReqData.PONumber = txtTab2_PONumber.Text.Trim();  
                        ObjPurReqData.EmployeeID = LogData.EmployeeID;
                        ObjPurReqData.AddedBy = LogData.UserName;
                        ObjPurReqData.HospitalID = LogData.HospitalID;
                        ObjPurReqData.IsActive = LogData.IsActive;
                        ObjPurReqData.IPaddress = LogData.IPaddress;
                        ObjPurReqData.FinancialYearID = LogData.FinancialYearID;
                        ObjPurReqData.ActionType = Enumaction.Update;
                                                             
                        PurchaseOrderRecievedList.Add(ObjPurReqList);
                    }
                    ObjPurReqData.XMLData = XmlConvertor.PurchaseOrderRecievedDataToXML(PurchaseOrderRecievedList).ToString();

                    int result = ObjPurReqBO.UpdatePurchaseOrderRecievedList(ObjPurReqData);
                    if (result== 1)
                    {                      
                        
                        btnTab2Save.Attributes["disabled"] = "disabled";
                        btnTab2Print.Attributes.Remove("disabled");

                        lblmessageTab2.Visible = true;
                        Messagealert_.ShowMessage(lblmessageTab2, "Save", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "Error", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                    }
                }

                catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    lblmessageTab2.Text = ExceptionMessage.GetMessage(ex);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                }          
            }
        }

        protected void btnTab2Reset_Click(object sender, EventArgs e)
        {
            Tab2Reset();
        }

        protected void Tab2Reset()
        {
            txtTab2_ReqNo.Text = "";
            txtTab2_ReqDate.Text = "";
            txtTab2_ApprovedBy.Text = "";
            txtTab2_ApprovedDate.Text = "";
            txtTab2_Supplier.Text = "";
            txtTab2_PONumber.Text = "";
            GvPurchaseOrderChecking.DataSource = null;
            GvPurchaseOrderChecking.DataBind();
            GvPurchaseOrderChecking.Visible = false;
            divmsg2.Visible = false;
            lblmessageTab2.Text = "";
            div2.Visible = false;
            lblResultTab2.Text = "";    
        }


        //------------------------------------- End Tab 2 ---------------------------------------
    }
}