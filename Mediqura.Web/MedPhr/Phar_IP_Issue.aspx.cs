using Mediqura.BOL.CommonBO;
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
    public partial class Phar_IP_Issue : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = LogData.MedSubStockID.ToString();
            if (!IsPostBack)
            {
                bindddl();
            }
            txtwardbedno.Attributes["disabled"] = "disabled";
            txtage.Attributes["disabled"] = "disabled";
            txtgender.Attributes["disabled"] = "disabled";
            txt_wardbedNo.Attributes["disabled"] = "disabled";
            txtaddress.Attributes["disabled"] = "disabled";

            txtrate.Attributes["disabled"] = "disabled";
            //----TAB2 ----//
            txt_wardbedNo.Attributes["disabled"] = "disabled";
            txt_Address.Attributes["disabled"] = "disabled";
            txt_sex.Attributes["disabled"] = "disabled";
            txt_Age.Attributes["disabled"] = "disabled";

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
                txtdrugname.Focus();
                bindgrid();
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
                }
            }
            else
            {
                objStock.ID = 0;
                txtdrugname.Text = "";
            }
            objStock.SubStockID = LogData.MedSubStockID;
            ListStock = objBO.GetStockItemDetailsBySubStockID(objStock);
            if (ListStock.Count > 0)
            {
                txtcomposition.Text = txtcomposition.Text;
                txtcomposition.Text = ListStock[0].Remarks.ToString();
                txtNoUnit.Text = "1";
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
                txtNoUnit.Text = "1";
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
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
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

                if (txtdrugname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Drug name cannot be blank!", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtdrugname.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtNoUnit.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Unit", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtNoUnit.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtequivalentqty.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter equivelent quantity.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtequivalentqty.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtrate.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Rate cannot be blank.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtrate.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                string IPNO;
                string PatName;
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    IPNO = source.Substring(source.LastIndexOf(':') + 1);
                    int indexStop = source.LastIndexOf('/');
                    PatName = source.Substring(0, indexStop);
                    ObjDrug.IPNo = IPNO.Trim();
                    ObjDrug.IPPatientName = PatName;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtpatientNames.Focus();
                    return;
                }

                string SubStockID;
                string DrgName;
                var source1 = txtdrugname.Text.ToString();
                if (source1.Contains(":"))
                {
                    SubStockID = source1.Substring(source1.LastIndexOf(':') + 1);
                    int indexStop = source1.LastIndexOf('|');
                    DrgName = source1.Substring(0, indexStop);

                    ObjDrug.SubStockID = Convert.ToInt32(SubStockID);
                    ObjDrug.DrugName = DrgName;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Drug name cannot be blank!", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtdrugname.Focus();
                    return;
                }
                ObjDrug.MedSubStockID = LogData.MedSubStockID;
                ObjDrug.MRPperQty = Convert.ToDecimal(hdnmrpperqty.Value == "" ? "0" : hdnmrpperqty.Value);

                int x = (Convert.ToInt32(txtequivalentqty.Text == "" ? "0" : txtequivalentqty.Text) / Convert.ToInt32(hdnequivalentqty.Value == "" ? "0" : hdnequivalentqty.Value));
                int y = (Convert.ToInt32(txtequivalentqty.Text == "" ? "0" : txtequivalentqty.Text) % Convert.ToInt32(hdnequivalentqty.Value == "" ? "0" : hdnequivalentqty.Value));

                string Z = (x).ToString() + "." + (y).ToString();

                ObjDrug.NoUnit = Convert.ToDecimal(Z);
                ObjDrug.EquivalentQty = Convert.ToDecimal(txtequivalentqty.Text == "" ? "0" : txtequivalentqty.Text);
                ObjDrug.NetCharge = Convert.ToDecimal(txtrate.Text == "" ? "0" : txtrate.Text);
                ObjDrug.UHID = Convert.ToInt32(hdnuhid.Value == "" ? "0" : hdnuhid.Value);
                ObjDrug.WardBedNo = txtwardbedno.Text == "" ? "" : txtwardbedno.Text;
                ObjDrug.EmployeeID = LogData.EmployeeID;
                ObjDrug.HospitalID = LogData.HospitalID;
                ObjDrug.FinancialYearID = LogData.FinancialYearID;
                ObjDrug.ActionType = Enumaction.Insert;
                List<PharIPIssueData> results = objOTBO.UpdateIPDrugIssueDetails(ObjDrug);
                if (results[0].IPDrgIssueNo != "")
                {
                    if (results[0].IPDrgIssueNo != "")
                    {
                        bindgrid();
                        Messagealert_.ShowMessage(lblmessage, "save", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        Clear();
                    }

                    if (results[0].IPDrgIssueNo == "4")
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
                }
                else
                {
                    lblresult.Visible = false;
                    gvDrugdetails.DataSource = null;
                    gvDrugdetails.DataBind();
                    gvDrugdetails.Visible = true;
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
            divmsg1.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            ViewState["ID"] = null;
            btnsave.Text = "Add";
            btnsave.Attributes.Remove("disabled");
        }


        //------TAB2--------//

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Get_IPPatientName(string prefixText, int count, string contextKey)
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
                    txt_IPpatient.Focus();
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
                }
                else
                {
                    lblresults.Visible = false;
                    GvIPDrugRecordlist.DataSource = null;
                    GvIPDrugRecordlist.DataBind();
                    gvDrugdetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Visible = true;
            }
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
                    Label SubStockID = (Label)gr.Cells[0].FindControl("lblSubstockID");
                    Label nounit = (Label)gr.Cells[0].FindControl("lbl_unit");
                    Label Qty = (Label)gr.Cells[0].FindControl("lbl_qty");
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
                    objDrgData.SubStockID = Convert.ToInt32(SubStockID.Text == "" ? "0" : SubStockID.Text);
                    objDrgData.NoUnit = Convert.ToDecimal(nounit.Text == "" ? "0" : nounit.Text);
                    objDrgData.EquivalentQty = Convert.ToInt32(Qty.Text == "" ? "0" : Qty.Text);
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
            divmsg2.Visible = false;
            ViewState["ID"] = null;
        }

    }
}