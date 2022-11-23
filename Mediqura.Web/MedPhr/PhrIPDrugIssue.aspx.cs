﻿using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedPharBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedPharData;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
    public partial class PhrIPDrugIssue : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AutoCompleteExtender2.ContextKey = LogData.MedSubStockID.ToString();
                BtnReset();
                bindddl();
                if (Session["IPSaleDeatail"] != null)
                {
                    txtpatientNames.Text = Session["IPSaleDeatail"].ToString();
                    getpatientdetails();
                    txtpatientNames.ReadOnly = true;
                    Session["IPSaleDeatail"] = null;
                }
				txtwardbedno.Attributes["disabled"] = "disabled";
				txtage.Attributes["disabled"] = "disabled";
				txtgender.Attributes["disabled"] = "disabled";
				txt_wardbedNo.Attributes["disabled"] = "disabled";
				txtaddress.Attributes["disabled"] = "disabled";
				txtrate.Attributes["disabled"] = "disabled";
				btnsave.Attributes["disabled"] = "disabled";
				btnprint.Attributes["disabled"] = "disabled";
				//----TAB2 ----//
				txt_wardbedNo.Attributes["disabled"] = "disabled";
				txt_Address.Attributes["disabled"] = "disabled";
				txt_sex.Attributes["disabled"] = "disabled";
				txt_Age.Attributes["disabled"] = "disabled";
            }
           

        }
        private void bindddl()
        {
            int CurMonth = Convert.ToInt32(DateTime.Now.Month);
            MasterLookupBO mstlookup = new MasterLookupBO();

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPPatientName(string prefixText, int count, string contextKey)
        {
            PharIPIssueData Objpaic = new PharIPIssueData();
            Phar_IPIssueBO objmedBO = new Phar_IPIssueBO();
            List<PharIPIssueData> getResult = new List<PharIPIssueData>();
            Objpaic.IPPatientName = prefixText;
            getResult = objmedBO.GetIPPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPPatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDrugName(string prefixText, int count, string contextKey)
        {
            PharIPIssueData Objdrg = new PharIPIssueData();
            Phar_IPIssueBO objmedBO = new Phar_IPIssueBO();
            List<PharIPIssueData> getResult = new List<PharIPIssueData>();
            Objdrg.MedSubStockID = Convert.ToInt32(contextKey);
            Objdrg.ItemName = prefixText;
            getResult = objmedBO.GetDrugName(Objdrg);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getsearchbycomposition(string prefixText, int count, string contextKey)
        {
            MedIndentData Objpaic = new MedIndentData();
            MedStoreIndentBO objInfoBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            Objpaic.ItemName = prefixText;
            Objpaic.MedSubStockID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.SearchDruglistByComposition(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDoctorName(string prefixText, int count, string contextKey)
        {
            PharIPIssueData Objdr = new PharIPIssueData();
            Phar_IPIssueBO objmedBO = new Phar_IPIssueBO();
            List<PharIPIssueData> getResult = new List<PharIPIssueData>();
            Objdr.DoctorName = prefixText;
            getResult = objmedBO.GetDoctorName(Objdr);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].DoctorName.ToString());
            }
            return list;
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            Session["IPDrugIssueList"] = null;
            gvDrugdetails.DataSource = null;
            gvDrugdetails.DataBind();
            gvDrugdetails.Visible = true;

            getpatientdetails();
        }
        private void getpatientdetails()
        {
            PharIPIssueData ObjMedi = new PharIPIssueData();
            Phar_IPIssueBO objmediBO = new Phar_IPIssueBO();
            List<PharIPIssueData> getResult = new List<PharIPIssueData>();
            string IPNO;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                IPNO = source.Substring(source.LastIndexOf(':') + 1);
                ObjMedi.IPNo = IPNO.Trim();
            }
            else
            {
                ObjMedi.IPNo = "";
                txtpatientNames.Text = "";
                txtpatientNames.Focus();
                return;
            }

            getResult = objmediBO.GetPatientDetailsByIPNO(ObjMedi);
            if (getResult.Count > 0)
            {
                hdnuhid.Value = getResult[0].UHID.ToString();
                hdnipnumber.Value = getResult[0].IPNo.ToString();
                txtwardbedno.Text = getResult[0].WardBedNo.ToString();
                txtage.Text = getResult[0].Age.ToString();
                txtgender.Text = getResult[0].GenderName.ToString();
                txtaddress.Text = getResult[0].Address.ToString();
                if (Convert.ToDecimal(getResult[0].Payable) > Convert.ToDecimal(getResult[0].PHRUpperLimit))
                {
                    if (Convert.ToInt32(getResult[0].PHRcreditAlowed) == 1)
                    {
                        txtdrugname.Attributes.Remove("disabled");
                        txtdrugname.Focus();
                    }
                    else
                    {
                        txtdrugname.Attributes["disabled"] = "disabled";
                        Messagealert_.ShowMessage(lblmessage, "PhrAccessLimit", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                    }
                }
                else
                {
                    txtdrugname.Attributes.Remove("disabled");
                    lblmessage.Text = "";
                    divmsg1.Visible = false;
                    txtdrugname.Focus();
                }
               
            }
            else
            {
                hdnuhid.Value = "";
                hdnipnumber.Value = "";
                txtwardbedno.Text = "";
                txtage.Text = "";
                txtgender.Text = "";

            }

        }
        protected void txt_drugsname_TextChanged(object sender, EventArgs e)
        {
            List<StockGRNData> ListStock = new List<StockGRNData>();
            StockGRNData objStock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            if (txtdrugname.Text.Contains(":"))
            {
                bool isIDnumeric = txtdrugname.Text.Substring(txtdrugname.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                if (isIDnumeric == true)
                {
                    objStock.ID = isIDnumeric ? Convert.ToInt64(txtdrugname.Text.Contains(":") ? txtdrugname.Text.Substring(txtdrugname.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    objStock.ID = 0;
                    txtdrugname.Text = "";
                    txtdrugname.Focus();
                    return;
                }
            }
            else
            {
                objStock.ID = 0;
                txtdrugname.Text = "";
                txtdrugname.Focus();
                return;
            }
            objStock.SubStockID = LogData.MedSubStockID;
            ListStock = objBO.GetStockItemDetailsBySubStockID(objStock);
            if (ListStock.Count > 0)
            {
                txtcomposition.Text = txtcomposition.Text;
                txtcomposition.Text = ListStock[0].Remarks.ToString();
                // txtNoUnit.Text = "1";
                txtequivalentqty.Text = ListStock[0].EquivalentQtyPerUnit.ToString();
                hdnequivalentqty.Value = ListStock[0].EquivalentQtyPerUnit.ToString();
                hdnmrpperqty.Value = ListStock[0].MRPPerQty.ToString();
                txt_totalavail.Text = ListStock[0].EquivalentQtyBalance.ToString();
                txtrate.Text = Commonfunction.Getrounding((Convert.ToDecimal(ListStock[0].MRPPerQty) * Convert.ToDecimal(ListStock[0].EquivalentQtyPerUnit)).ToString());
                txtNoUnit.Focus();
            }
        }
        protected void txt_searchcomposition_TextChanged(object sender, EventArgs e)
        {
            List<StockGRNData> ListStock = new List<StockGRNData>();
            StockGRNData objStock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            if (txtcomposition.Text.Contains(":"))
            {
                bool isIDnumeric = txtcomposition.Text.Substring(txtcomposition.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                if (isIDnumeric == true)
                {
                    objStock.ID = isIDnumeric ? Convert.ToInt64(txtcomposition.Text.Contains(":") ? txtcomposition.Text.Substring(txtcomposition.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    objStock.ID = 0;
                    txtcomposition.Text = "";
                }
            }
            else
            {
                objStock.ID = 0;
                txtcomposition.Text = "";
            }
            objStock.SubStockID = LogData.MedSubStockID;
            ListStock = objBO.GetStockItemDetailsBySubStockID(objStock);
            if (ListStock.Count > 0)
            {
                txtdrugname.Text = txtcomposition.Text;
                txtcomposition.Text = ListStock[0].Remarks.ToString();
                // txtNoUnit.Text = "1";
                txtequivalentqty.Text = ListStock[0].EquivalentQtyPerUnit.ToString();
                hdnequivalentqty.Value = ListStock[0].EquivalentQtyPerUnit.ToString();
                txt_totalavail.Text = ListStock[0].EquivalentQtyBalance.ToString();
                hdnmrpperqty.Value = ListStock[0].MRPPerQty.ToString();
                txtrate.Text = Commonfunction.Getrounding((Convert.ToDecimal(ListStock[0].MRPPerQty) * Convert.ToDecimal(ListStock[0].EquivalentQtyPerUnit)).ToString());
            }
            else
            {
                txtdrugname.Text = "";
                txtcomposition.Text = "";
                txt_totalavail.Text = "";
                txtNoUnit.Text = "";
                txtequivalentqty.Text = "";
                hdnequivalentqty.Value = "";
                hdnmrpperqty.Value = "";
                txtrate.Text = "";
            }
        }
        ///......................................................//
        protected void btnadd_Click(object sender, EventArgs e)
        {
            txtrate.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtequivalentqty.Text == "" ? "0" : txtequivalentqty.Text) * Convert.ToDecimal(hdnmrpperqty.Value == "" ? "0" : hdnmrpperqty.Value)).ToString());

            if (txtpatientNames.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Custommer", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtpatientNames.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txtdrugname.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtdrugname.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txtNoUnit.Text.Trim() == "" || txtNoUnit.Text.Trim() == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "Nounit", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtNoUnit.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txtequivalentqty.Text.Trim() == "" || txtequivalentqty.Text.Trim() == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "ReqdQty", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtequivalentqty.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            List<PharIPIssueData> ListStock = new List<PharIPIssueData>();
            List<PharIPIssueData> DrugList = Session["IPDrugIssueList"] == null ? new List<PharIPIssueData>() : (List<PharIPIssueData>)Session["IPDrugIssueList"];
            PharIPIssueData objStock = new PharIPIssueData();

            objStock.UHID = Convert.ToInt32(hdnuhid.Value == "" ? "0" : hdnuhid.Value);
            objStock.MedSubStockID = LogData.MedSubStockID;


            int x = (Convert.ToInt32(txtequivalentqty.Text == "" ? "0" : txtequivalentqty.Text) / Convert.ToInt32(hdnequivalentqty.Value == "" ? "0" : hdnequivalentqty.Value));
            int y = (Convert.ToInt32(txtequivalentqty.Text == "" ? "0" : txtequivalentqty.Text) % Convert.ToInt32(hdnequivalentqty.Value == "" ? "0" : hdnequivalentqty.Value));

            string Z = (x).ToString() + "." + (y).ToString();

            objStock.NoUnit = Convert.ToDecimal(Z);
            objStock.EquivalentQty = Convert.ToDecimal(txtequivalentqty.Text == "" ? "0" : txtequivalentqty.Text);
            objStock.NetCharge = Convert.ToDecimal(txtrate.Text == "" ? "0" : txtrate.Text);
            objStock.MRPperQty = Convert.ToDecimal(hdnmrpperqty.Value == "" ? "0" : hdnmrpperqty.Value);
            objStock.WardBedNo = txtwardbedno.Text == "" ? "" : txtwardbedno.Text;

            string SubStockID, DrgName;
            var source = txtdrugname.Text.ToString();
            if (source.Contains(":"))
            {
                SubStockID = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gvDrugdetails.Rows)
                {
                    Label SubStkID = (Label)gvDrugdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    if (SubStockID == SubStkID.Text)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtdrugname.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                objStock.SubStockID = Convert.ToInt32(SubStockID);
                int indexStop = source.LastIndexOf('|');
                DrgName = source.Substring(0, indexStop);
                objStock.DrugName = DrgName;
            }
            else
            {
                txtdrugname.Text = "";
                return;
            }

            DrugList.Add(objStock);

            if (DrugList.Count > 0)
            {
                gvDrugdetails.DataSource = DrugList;
                gvDrugdetails.DataBind();
                gvDrugdetails.Visible = true;
                Session["IPDrugIssueList"] = DrugList;
                btnsave.Attributes.Remove("disabled");
                Clear();
                txtdrugname.Focus();
                TotalSum();
            }
            else
            {
                gvDrugdetails.DataSource = null;
                gvDrugdetails.DataBind();
                gvDrugdetails.Visible = true;
            }

        }
        //Sum of the gridview
        protected void TotalSum()
        {
            decimal RatetTotal = 0;
            decimal NoUnittotal = 0;
            decimal QtyTotal = 0;
            decimal NetchargesTotal = 0;
            foreach (GridViewRow gvr in gvDrugdetails.Rows)
            {
                Label rate = (Label)gvr.Cells[0].FindControl("lbl_rate");
                Label Unit = (Label)gvr.Cells[0].FindControl("lbl_unit");
                Label qty = (Label)gvr.Cells[0].FindControl("lbl_qty");
                Label Netcharges = (Label)gvr.Cells[0].FindControl("lbl_netcharges");
                RatetTotal = RatetTotal + Convert.ToDecimal(rate.Text.Trim());
                NoUnittotal = NoUnittotal + Convert.ToDecimal(Unit.Text.Trim());
                QtyTotal = QtyTotal + Convert.ToDecimal(qty.Text.Trim());
                NetchargesTotal = NetchargesTotal + Convert.ToDecimal(Netcharges.Text.Trim());

            }
            txt_TotalRate.Text = RatetTotal.ToString();
            txt_TotalUnit.Text = NoUnittotal.ToString();
            txt_TotalQty.Text = QtyTotal.ToString();
            txt_TotalNetCharge.Text = Math.Round(NetchargesTotal).ToString();
        }
        protected void Clear()
        {
            txtdrugname.Text = "";
            txtcomposition.Text = "";
            txt_totalavail.Text = "";
            txtNoUnit.Text = "";
            txtequivalentqty.Text = "";
            txtrate.Text = "";
            txtdrugname.Focus();
        }
        protected void gvDrugdetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvDrugdetails.Rows[i];
                    List<PharIPIssueData> ItemList = Session["IPDrugIssueList"] == null ? new List<PharIPIssueData>() : (List<PharIPIssueData>)Session["IPDrugIssueList"];
                    ItemList.RemoveAt(i);
                    Label rate = (Label)gr.Cells[0].FindControl("lbl_rate");
                    Label Unit = (Label)gr.Cells[0].FindControl("lbl_unit");
                    Label qty = (Label)gr.Cells[0].FindControl("lbl_qty");
                    Label Netcharges = (Label)gr.Cells[0].FindControl("lbl_netcharges");

                    Session["IPDrugIssueList"] = ItemList;
                    gvDrugdetails.DataSource = ItemList;
                    gvDrugdetails.DataBind();
                    TotalSum();
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

        //-----------------------------------------------------//

		protected void btnyes_Click(object sender, EventArgs e)
		{
			mpconfirmation.Hide();

			try
			{
				List<PharIPIssueData> DrugList = new List<PharIPIssueData>();
				PharIPIssueData ObjDrug = new PharIPIssueData();
				Phar_IPIssueBO objOTBO = new Phar_IPIssueBO();
				IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

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
				if (txtpatientNames.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "patientname", 0);
					divmsg1.Attributes["class"] = "FailAlert";
					divmsg1.Visible = true;
					txtpatientNames.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}

				if (Convert.ToDecimal(txtDepositAmount.Text == "" ? "0" : txtDepositAmount.Text) > Convert.ToDecimal(txt_TotalNetCharge.Text == "" ? "0" : txt_TotalNetCharge.Text))
				{
					Messagealert_.ShowMessage(lblmessage, "ExceedAmount", 0);
					divmsg1.Attributes["class"] = "FailAlert";
					divmsg1.Visible = true;
					txtDepositAmount.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				// get all the record from the gridview
				int itemcount = 0;
				foreach (GridViewRow row in gvDrugdetails.Rows)
				{
					Label SubStockID = (Label)gvDrugdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
					Label ItemID = (Label)gvDrugdetails.Rows[row.RowIndex].Cells[0].FindControl("lblItemID");
					Label DrugName = (Label)gvDrugdetails.Rows[row.RowIndex].Cells[0].FindControl("lblitemname");
					Label MRPperQty = (Label)gvDrugdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_rate");
					Label Unit = (Label)gvDrugdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_unit");
					Label EqvQty = (Label)gvDrugdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_qty");
					Label Charge = (Label)gvDrugdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_netcharges");
					PharIPIssueData ObjDetails = new PharIPIssueData();
					ObjDetails.SubStockID = Convert.ToInt32(SubStockID.Text == "" ? "0" : SubStockID.Text);
					ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
					ObjDetails.DrugName = DrugName.Text == "" ? "" : DrugName.Text;
					ObjDetails.MRPperQty = Convert.ToDecimal(MRPperQty.Text == "" ? "0" : MRPperQty.Text);
					ObjDetails.NoUnit = Convert.ToDecimal(Unit.Text == "" ? "0" : Unit.Text);
					ObjDetails.EquivalentQty = Convert.ToDecimal(EqvQty.Text == "" ? "0" : EqvQty.Text);
					ObjDetails.NetCharge = Convert.ToDecimal(Charge.Text == "" ? "0" : Charge.Text);
					itemcount = itemcount + 1;
					DrugList.Add(ObjDetails);
				}
				ObjDrug.XMLData = XmlConvertor.DrugDetailsDatatoXML(DrugList).ToString();
				if (itemcount == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "ItemCount", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}

				string IPNO;
				// string PatName;
				var source = txtpatientNames.Text.ToString();
				if (source.Contains(":"))
				{
					IPNO = source.Substring(source.LastIndexOf(':') + 1);
					//int indexStop = source.LastIndexOf('/');
					//PatName = source.Substring(0, indexStop);
					ObjDrug.IPNo = IPNO.Trim();
					ObjDrug.IPPatientName = "";
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "patientname", 0);
					divmsg1.Attributes["class"] = "FailAlert";
					divmsg1.Visible = true;
					txtpatientNames.Focus();
					return;
				}


				ObjDrug.MedSubStockID = LogData.MedSubStockID;
				ObjDrug.UHID = Convert.ToInt32(hdnuhid.Value == "" ? "0" : hdnuhid.Value);
				ObjDrug.WardBedNo = txtwardbedno.Text == "" ? "" : txtwardbedno.Text;
				ObjDrug.TotalMRPperQty = Convert.ToDecimal(txt_TotalRate.Text == "" ? "0" : txt_TotalRate.Text);
				ObjDrug.TotalNoUnit = Convert.ToDecimal(txt_TotalUnit.Text == "" ? "0" : txt_TotalUnit.Text);
				ObjDrug.TotalEqvQty = Convert.ToDecimal(txt_TotalQty.Text == "" ? "0" : txt_TotalQty.Text);
				ObjDrug.TotalNetCharge = Convert.ToDecimal(txt_TotalNetCharge.Text == "" ? "0" : txt_TotalNetCharge.Text);
				ObjDrug.DepositAmount = Convert.ToDecimal(txtDepositAmount.Text == "" ? "0" : txtDepositAmount.Text);
				ObjDrug.EmployeeID = LogData.EmployeeID;
				ObjDrug.HospitalID = LogData.HospitalID;
				ObjDrug.FinancialYearID = LogData.FinancialYearID;
				ObjDrug.ActionType = Enumaction.Insert;
				List<PharIPIssueData> results = objOTBO.UpdateIPDrugIssueDetails(ObjDrug);
				if (results.Count > 0)
				{
					if (results[0].IPDrgIssueNo != "")
					{
						Messagealert_.ShowMessage(lblmessage, "save", 1);
						txtIssueNo.Text = results[0].IPDrgIssueNo.ToString();
						divmsg1.Attributes["class"] = "SucessAlert";
						btnsave.Attributes["disabled"] = "disabled";
						divmsg1.Visible = true;
						btnprint.Attributes.Remove("disabled");
						Clear();
					}

					if (results[0].Result == 4)
					{
						Messagealert_.ShowMessage(lblmessage, "Patient is not found in IP admission", 0);
						divmsg1.Attributes["class"] = "FailAlert";
						divmsg1.Visible = true;
					}
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "system", 0);
					divmsg1.Attributes["class"] = "FailAlert";
					divmsg1.Visible = true;
				}

			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				string msg = ex.ToString();
				Messagealert_.ShowMessage(lblmessage, msg, 0);
				divmsg1.Attributes["class"] = "FailAlert";
				divmsg1.Visible = true;
				bindgrid();
			}
		}
		protected void btnno_Click(object sender, EventArgs e)
		{
			mpconfirmation.Hide();
			btnsave.Attributes.Remove("disabled");
		}

		

        protected void btnsave_Click(object sender, EventArgs e)
        {
			mpconfirmation.Show();
			btnyes.Focus();
			lbl_totalquantity.Text = txt_TotalQty.Text.Trim() == "" ? "0" : txt_TotalQty.Text.Trim();
			lbl_totnetcharge.Text = txt_TotalNetCharge.Text.Trim() == "" ? "0" : txt_TotalNetCharge.Text.Trim();
			lbl_totdeposit.Text = txtDepositAmount.Text.Trim() == "" ? "0" : txtDepositAmount.Text.Trim();
        }

        protected void btnsearchs_Click(object sender, EventArgs e)
        {
            bindgrid();
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

                if (txtpatientNames.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtpatientNames.Focus();
                    return;
                }
                else
                {
                    var source1 = txtpatientNames.Text.ToString();
                    if (source1.Contains(":"))
                    {
                        lblmessage.Visible = false;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtpatientNames.Focus();
                        return;
                    }
                }
                List<PharIPIssueData> obj = GetIPDrugRecordList(0);
                if (obj.Count > 0)
                {
                    Messagealert_.ShowMessage(lblresult, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsgs3.Attributes["class"] = "SucessAlert";
                    gvDrugdetails.DataSource = obj;
                    gvDrugdetails.DataBind();
                    gvDrugdetails.Visible = true;
                    lblresult.Visible = true;
                    divmsgs3.Visible = true;
                }
                else
                {
                    lblresult.Visible = false;
                    gvDrugdetails.DataSource = null;
                    gvDrugdetails.DataBind();
                    gvDrugdetails.Visible = true;
                    lblresult.Visible = false;
                    divmsgs3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<PharIPIssueData> GetIPDrugRecordList(int p)
        {
            PharIPIssueData objmedi = new PharIPIssueData();
            Phar_IPIssueBO objBO = new Phar_IPIssueBO();
            string IPNO;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                IPNO = source.Substring(source.LastIndexOf(':') + 1);
                objmedi.IPNo = IPNO.Trim();
            }
            string SubStockID;
            var source1 = txtdrugname.Text.ToString();
            if (source1.Contains(":"))
            {
                SubStockID = source1.Substring(source1.LastIndexOf(':') + 1);
                objmedi.SubStockID = Convert.ToInt32(SubStockID);
            }
            else
            {
                objmedi.SubStockID = 0;

            }
            return objBO.GetIPDrugRecordList(objmedi);
        }

        protected void btnclear_Click(object sender, System.EventArgs e)
        {
            BtnReset();
			Response.Redirect("~/CurrentPatientlist.aspx", false);
        }
        protected void BtnReset()
        {
            gvDrugdetails.DataSource = null;
            gvDrugdetails.DataBind();
            gvDrugdetails.Visible = false;
            txtpatientNames.Text = "";
            txtaddress.Text = "";
            txtwardbedno.Text = "";
            txtage.Text = "";
            txtgender.Text = "";
            txt_totalavail.Text = "";
            txtdrugname.Text = "";
            txtcomposition.Text = "";
            txtNoUnit.Text = "";
            txtequivalentqty.Text = "";
            txtrate.Text = "";
            txt_TotalRate.Text = "";
            txt_TotalUnit.Text = "";
            txt_TotalQty.Text = "";
            txt_TotalNetCharge.Text = "";
            txtDepositAmount.Text = "";
            Session["IPDrugIssueList"] = null;
            divmsg1.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            ViewState["ID"] = null;
            txtIssueNo.Text = "";

            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            txtpatientNames.ReadOnly = false;
        }

        //------TAB2--------//

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Get_IPPatientName(string prefixText, int count, string contextKey)
        {
            PharIPIssueData Objpaic = new PharIPIssueData();
            Phar_IPIssueBO objmedBO = new Phar_IPIssueBO();
            List<PharIPIssueData> getResult = new List<PharIPIssueData>();
            Objpaic.IPPatientName = prefixText;
            getResult = objmedBO.GetAllIPPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPPatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Get_DrugName(string prefixText, int count, string contextKey)
        {
            PharIPIssueData Objdrg = new PharIPIssueData();
            Phar_IPIssueBO objmedBO = new Phar_IPIssueBO();
            List<PharIPIssueData> getResult = new List<PharIPIssueData>();
            Objdrg.MedSubStockID = Convert.ToInt32(contextKey);
            Objdrg.ItemName = prefixText;
            getResult = objmedBO.GetDrugName(Objdrg);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            MedIndentData Objpaic = new MedIndentData();
            MedStoreIndentBO objInfoBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNameListInStore(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);

            }
            return list;
        }
        protected void txt_IPpatients_TextChanged(object sender, EventArgs e)
        {
            PharIPIssueData ObjMedi = new PharIPIssueData();
            Phar_IPIssueBO objmediBO = new Phar_IPIssueBO();
            List<PharIPIssueData> getResult = new List<PharIPIssueData>();
            string IPNO;
            var source2 = txt_IPpatient.Text.ToString();
            if (source2.Contains(":"))
            {
                IPNO = source2.Substring(source2.LastIndexOf(':') + 1);
                ObjMedi.IPNo = IPNO.Trim();
            }
            else
            {
                ObjMedi.IPNo = "";
                txt_IPpatient.Text = "";
                txt_IPpatient.Focus();
                return;
            }

            getResult = objmediBO.GetPatientDetailsByIPNO(ObjMedi);

            if (getResult.Count > 0)
            {
                hdn_UHID.Value = getResult[0].UHID.ToString();
                hdn_IPNO.Value = getResult[0].IPNo.ToString();
                txt_wardbedNo.Text = getResult[0].WardBedNo.ToString();
                txt_Address.Text = getResult[0].Address.ToString();
                txt_sex.Text = getResult[0].GenderName.ToString();
                txt_Age.Text = getResult[0].Age.ToString();
                tab2_bindgrid();
            }
            else
            {
                hdn_UHID.Value = "";
                hdn_IPNO.Value = "";
                txt_wardbedNo.Text = "";
                txt_Address.Text = "";
                txt_Age.Text = "";
                txt_sex.Text = "";
            }
        }
        protected void btn_searchs_Click(object sender, EventArgs e)
        {
            tab2_bindgrid();
        }
        protected void tab2_bindgrid()
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

                if (txt_IPpatient.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "patientname", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtpatientNames.Focus();
                    return;
                }
                else
                {
                    var source1 = txt_IPpatient.Text.ToString();
                    if (source1.Contains(":"))
                    {
                        lblmessage2.Visible = false;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "patientname", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtpatientNames.Focus();
                        return;
                    }
                }

                List<PharIPIssueData> obj = Get_IPDrugRecordList(0);
                if (obj.Count > 0)
                {
                    Messagealert_.ShowMessage(lblresults, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg.Attributes["class"] = "SucessAlert";
                    GvIPDrugRecordlist.DataSource = obj;
                    GvIPDrugRecordlist.DataBind();
                    GvIPDrugRecordlist.Visible = true;
                    divmsg.Visible = true;
                    lblresults.Visible = true;
                }
                else
                {
                    lblresults.Visible = false;
                    GvIPDrugRecordlist.DataSource = null;
                    GvIPDrugRecordlist.DataBind();
                    gvDrugdetails.Visible = true;
                    divmsg.Visible = false;
                    lblresults.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Visible = true;
            }
        }

        private List<PharIPIssueData> Get_IPDrugRecordList(int p)
        {
            PharIPIssueData objmedi = new PharIPIssueData();
            Phar_IPIssueBO objBO = new Phar_IPIssueBO();
            string IPNO;
            var source3 = txt_IPpatient.Text.ToString();
            if (source3.Contains(":"))
            {
                IPNO = source3.Substring(source3.LastIndexOf(':') + 1);
                objmedi.IPNo = IPNO.Trim();
            }
            string ItemIDs;
            var source4 = txt_drug.Text.ToString();
            if (source4.Contains(":"))
            {
                ItemIDs = source4.Substring(source4.LastIndexOf(':') + 1);
                objmedi.ItemID = Convert.ToInt32(ItemIDs);
            }
            else
            {
                objmedi.ItemID = 0;
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objmedi.DateFrom = from;
            objmedi.DateTo = To;
            objmedi.Status = ddl_status.SelectedValue == "0" ? true : false;
            return objBO.Get_IPDrugRecordList(objmedi);

        }
        protected void GvIPDrugRecordlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.RoleID == 1 || LogData.RoleID == 40)
                    {
                        if (LogData.DeleteEnable == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else
                        {
                            lblmessage2.Visible = false;
                        }
                        PharIPIssueData objDrgData = new PharIPIssueData();
                        Phar_IPIssueBO objDrgBO = new Phar_IPIssueBO();
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = GvIPDrugRecordlist.Rows[i];
                        Label ID = (Label)gr.Cells[0].FindControl("lblID");
                        Label DrgRecNo = (Label)gr.Cells[0].FindControl("lblDrgRecNo");
                        Label DepositNo = (Label)gr.Cells[0].FindControl("lbl_DepositNos");
                        Label DepositAmt = (Label)gr.Cells[0].FindControl("lbl_DepositAmt");
                        TextBox Remarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                        if (Remarks.Text == "")
                        {
                            Messagealert_.ShowMessage(lblresults, "Remarks", 0);
                            divmsg.Attributes["class"] = "FailAlert";
                            Remarks.Focus();
                            return;
                        }
                        else
                        {
                            objDrgData.Remarks = Remarks.Text;
                        }
                        objDrgData.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                        objDrgData.IPDrgIssueNo = DrgRecNo.Text.Trim() == "" ? "0" : DrgRecNo.Text.Trim();
                        objDrgData.DepositNos = DepositNo.Text.Trim() == "" ? "0" : DepositNo.Text.Trim();
                        objDrgData.DepositAmount = Convert.ToDecimal(DepositAmt.Text.Trim() == "" ? "0" : DepositAmt.Text.Trim());
                        objDrgData.EmployeeID = LogData.EmployeeID;
                        objDrgData.ActionType = Enumaction.Delete;
                        int Result = objDrgBO.DeleteIPPatientDrugRecordByID(objDrgData);
                        if (Result == 1)
                        {
                            tab2_bindgrid();
                            Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "SucessAlert";

                        }

                        else
                        {
                            Messagealert_.ShowMessage(lblmessage2, "system", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";

                        }
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void Reset_OnClick(object sender, System.EventArgs e)
        {

            GvIPDrugRecordlist.DataSource = null;
            GvIPDrugRecordlist.DataBind();
            GvIPDrugRecordlist.Visible = false;
            txt_IPpatient.Text = "";
            txt_Address.Text = "";
            txt_wardbedNo.Text = "";
            txt_Age.Text = "";
            txt_sex.Text = "";
            txt_drug.Text = "";
            txtfrom.Text = "";
            txtto.Text = "";
            divmsg2.Visible = false;
            lblmessage2.Visible = false;
            lblresults.Visible = false;
            ViewState["ID"] = null;
        }

    }
}