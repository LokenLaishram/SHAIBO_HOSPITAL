using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.OTData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.OTBO;
using Mediqura.BOL.MedNurseBO;
using Mediqura.CommonData.MedNurseData;

namespace Mediqura.Web.MedNurse
{
    public partial class IntakeOutputChart : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                txtpatientNames.Text = "";
                
            }
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            IntakeOutputChartData objpatdata = new IntakeOutputChartData();
            IntakeOutputChartBO objpatBO = new IntakeOutputChartBO();

            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                string ID = source.Substring(source.LastIndexOf(':') + 1);
                objpatdata.IPNo = ID;
            }
            else
            {
                txtpatientNames.Text = "";
                return;
            }
           // objpatdata.date = DateTime.Now; 
            objpatdata.DateFrom = DateTime.Now;
            objpatdata.DateTo = DateTime.Now;

            objpatdata.searchby = 0;
            Session["IntakeOutputChartDataList"] = null;
            List<IntakeOutputChartData> objpat = objpatBO.GetIntakeOutputPatientDetail(objpatdata);
            if (objpat.Count > 0)
            {   
                txtage.Text = objpat[0].Age.ToString();
                txtsex.Text = objpat[0].Sex;
                txtipno.Text = objpat[0].IPNo;
                txtbedroom.Text = objpat[0].WardBedNo.ToString();
                txtdoa.Text = objpat[0].DOA.ToString();
                txtdocter.Text = objpat[0].Doctor;
               // txttoday.Text = DateTime.Now.ToString();
               // txtdatefrom.Text = DateTime.Now.ToString();
                txtdateto.Text = DateTime.Now.ToString();
                lblUHID.Text = objpat[0].UHID.ToString();
                GVIntakeOutput.DataSource = objpat;
                GVIntakeOutput.DataBind();
                GVIntakeOutput.Visible = true;
                lblmessage.Visible = false;
                txttotalintakechart.Text =objpat[0].totalintakechart.ToString();
                txttotaloutputchart.Text = objpat[0].totaloutputchart.ToString();
                txttotalbalancechart.Text = objpat[0].totalbalancechart.ToString();
                //txttotalfluids.Text = objpat[0].totalfluids.ToString();
                //txttotaloral.Text = objpat[0].totaloral.ToString();
                //txttotalurine.Text = objpat[0].totalurine.ToString();
                //txttotalothes.Text = objpat[0].totalothers.ToString();

                Session["IntakeOutputChartDataList"] = objpat;
                Messagealert_.ShowMessage(lblresult, "Total:" + objpat.Count + " Record(s) found.", 1);
           
            }
            else
            {
                txtage.Text = "";
                txtsex.Text = "";
                txtipno.Text = "";
                txtbedroom.Text = "";
                txtdoa.Text = "";
                //txttoday.Text = "";
                txtdatefrom.Text = "";
                txtdateto.Text = "";
                lblmessage.Visible = true;
                Session["IntakeOutputChartDataList"] = null;
                Messagealert_.ShowMessage(lblmessage, "No record found.", 0);
                return;
            }
           
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]

        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            IntakeOutputChartData Objpaic = new IntakeOutputChartData();
            IntakeOutputChartBO objInfoBO = new IntakeOutputChartBO();
            List<IntakeOutputChartData> getResult = new List<IntakeOutputChartData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            addrow();
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
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                             
                List<IntakeOutputChartData> objresults = GetIntakeOutputPatientDetail(0);
                if (objresults.Count > 0)
                {
                    Messagealert_.ShowMessage(lblresult, "Total:" + objresults.Count + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    GVIntakeOutput.DataSource = objresults;
                    GVIntakeOutput.DataBind();
                    GVIntakeOutput.Visible = true;                
                }
                else
                {
                    GVIntakeOutput.DataSource = null;                   
                    GVIntakeOutput.Visible = true;
                    
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<IntakeOutputChartData> GetIntakeOutputPatientDetail(int p)
        {
            IntakeOutputChartData objpat = new IntakeOutputChartData();
            IntakeOutputChartBO objBO = new IntakeOutputChartBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true); 
            DateTime DateFrom = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime DateTo = txtdateto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            
            string IPNo;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                IPNo = source.Substring(source.LastIndexOf(':') + 1);
                objpat.IPNo = IPNo;
               
            }
            else
            {
                objpat.IPNo = "";
                
            }
            objpat.DateFrom = DateFrom;
            objpat.DateTo = DateTo;
            objpat.searchby = 0;
            return objBO.GetIntakeOutputPatientDetail(objpat);
        }
       
        private void addrow()
        {
            List<IntakeOutputChartData> IntakeOutputChartDataList = Session["IntakeOutputChartDataList"] == null ? new List<IntakeOutputChartData>() : (List<IntakeOutputChartData>)Session["IntakeOutputChartDataList"];
            IntakeOutputChartData ObjService = new IntakeOutputChartData();
            ObjService.PatientName = txtpatientNames.Text.ToString();
            ObjService.ID = 0;
            ObjService.RowNo = ((GVIntakeOutput.Rows.Count) + 1);
            ObjService.IntakeOutputDate = DateTime.Now;
            ObjService.fluidsstart = DateTime.Now;
            ObjService.fluidsend = DateTime.Now;
            ObjService.fluids = 0;
            ObjService.oralstart = DateTime.Now;
            ObjService.oralend = DateTime.Now;
            ObjService.oral = 0;
            ObjService.urinestart = DateTime.Now;
            ObjService.urineend = DateTime.Now;
            ObjService.urine = 0;
            ObjService.others = 0;
            ObjService.remarks = "";
            ObjService.IPNo = txtipno.Text.Trim();            
            ObjService.UHID = Convert.ToInt64(lblUHID.Text);
            ObjService.PatientName = txtpatientNames.Text.ToString();
            IntakeOutputChartDataList.Add(ObjService);
            if (IntakeOutputChartDataList.Count > 0)
            {
                GVIntakeOutput.DataSource = IntakeOutputChartDataList;
                GVIntakeOutput.DataBind();
                GVIntakeOutput.Visible = true;
                Session["IntakeOutputChartDataList"] = IntakeOutputChartDataList;
                lblupmessage.Visible = false;
                Messagealert_.ShowMessage(lblresult, "Total:" + IntakeOutputChartDataList.Count + " Record(s) found.", 1);
            }
            else
            {
                GVIntakeOutput.DataSource = null;
                GVIntakeOutput.DataBind();
                GVIntakeOutput.Visible = true;
            }

        }
        
        protected void GVIntakeOutput_RowCommand(object sender, GridViewCommandEventArgs e)
        {           
                try
                {
                    if (e.CommandName == "Deletes")
                    {
                        if (LogData.DeleteEnable == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                            divmsg1.Visible = true;
                            lblupmessage.Visible = false;
                            divmsg1.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = GVIntakeOutput.Rows[i];

                        Label ID = (Label)gr.Cells[0].FindControl("lblID");
                        if (ID.Text == "0")
                        {

                            List<IntakeOutputChartData> GetIntakeOutputPatientDetail = Session["IntakeOutputChartDataList"] == null ? new List<IntakeOutputChartData>() : (List<IntakeOutputChartData>)Session["IntakeOutputChartDataList"];
                           
                            GetIntakeOutputPatientDetail.RemoveAt(i);
                            if (GetIntakeOutputPatientDetail.Count > 0)
                            {
                                Session["GetIntakeOutputPatientDetail"] = GetIntakeOutputPatientDetail;
                                GVIntakeOutput.DataSource = GetIntakeOutputPatientDetail;
                                GVIntakeOutput.DataBind();
                            }
                            else
                            {
                                Session["GetIntakeOutputPatientDetail"] = null;
                                GVIntakeOutput.DataSource = null;
                                GVIntakeOutput.DataBind();
                            }
                        }
                        else
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


                            IntakeOutputChartData obj = new IntakeOutputChartData();
                            IntakeOutputChartBO objBO = new IntakeOutputChartBO();

                            obj.ID = Convert.ToInt32(ID.Text);

                            obj.EmployeeID = LogData.EmployeeID;
                            int Result = objBO.CancelIntakeOutputChartDataLists(obj);
                            if (Result == 1)
                            {
                                bindgrid();
                                lblupmessage.Text = "";
                                Messagealert_.ShowMessage(lblmessage, "cancel", 1);
                                divmsg1.Attributes["class"] = "SucessAlert";
                                divmsg1.Visible = true;
                               
                            }
                            else
                            {
                                Messagealert_.ShowMessage(lblmessage, "system", 0);
                                divmsg1.Attributes["class"] = "FailAlert";
                                divmsg1.Visible = true;
                            }
                        }
                    }
                 }
                catch (Exception ex) 
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            
        }
        protected void GVIntakeOutput_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvUHID = (Label)e.Row.FindControl("lblgvUHID");
                Label lblgvName = (Label)e.Row.FindControl("lblgvName");
                TextBox txtdate = (TextBox)e.Row.FindControl("txtdate");
                TextBox txtfluidsstart = (TextBox)e.Row.FindControl("txtfluidsstart");
                TextBox txtfluidsend = (TextBox)e.Row.FindControl("txtfluidsend");
                TextBox txtFluids = (TextBox)e.Row.FindControl("txtFluids");
                TextBox txtoralstart = (TextBox)e.Row.FindControl("txtoralstart");
                TextBox txtoralend = (TextBox)e.Row.FindControl("txtoralend");
                TextBox txtoral = (TextBox)e.Row.FindControl("txtoral");
                TextBox txturinestart = (TextBox)e.Row.FindControl("txturinestart");
                TextBox txturineend = (TextBox)e.Row.FindControl("txturineend");
                TextBox txturine = (TextBox)e.Row.FindControl("txturine");
                TextBox txtothers = (TextBox)e.Row.FindControl("txtothers");
                TextBox txtremarks = (TextBox)e.Row.FindControl("txtremarks");
                TextBox txttotalintakechart = (TextBox)e.Row.FindControl("txttotalintakechart");
                TextBox txttotaloutputchart = (TextBox)e.Row.FindControl("txttotaloutputchart");
                TextBox txttotalbalancechart = (TextBox)e.Row.FindControl("txttotalbalancechart");
               
                if (txtdate.Text == "")
                {
                    txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                if (txtfluidsstart.Text == "")
                {
                    txtfluidsstart.Text = String.Format("{0:hh:mm:ss tt}", DateTime.Now);
                }
                if (txtfluidsend.Text == "")
                {
                    txtfluidsend.Text = String.Format("{0:hh:mm:ss tt}", DateTime.Now);
                }
                if (txtoralstart.Text == "")
                {
                    txtoralstart.Text = String.Format("{0:hh:mm:ss tt}", DateTime.Now);
                }
                if (txtoralend.Text == "")
                {
                    txtoralend.Text = String.Format("{0:hh:mm:ss tt}", DateTime.Now);
                }
                if (txturinestart.Text == "")
                {
                    txturinestart.Text = String.Format("{0:hh:mm:ss tt}", DateTime.Now);
                }
                if (txturineend.Text == "")
                {
                    txturineend.Text = String.Format("{0:hh:mm:ss tt}", DateTime.Now);
                }              
            }
        }
       
        protected void btnsearch_Click(object sender, System.EventArgs e)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            IntakeOutputChartData objpat = new IntakeOutputChartData();
            IntakeOutputChartBO objBO = new IntakeOutputChartBO();
            Session["IntakeOutputChartDataList"] = null;
            string IPNo;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                IPNo = source.Substring(source.LastIndexOf(':') + 1);
                objpat.IPNo = IPNo;
            }
            else
            {
                objpat.IPNo = "";                
            }
           // DateTime D = txttoday.Text.Trim() == "" ? DateTime.Now : DateTime.Parse(txttoday.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
           // objpat.date = date1;

            DateTime DateFrom = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime: DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = DateFrom;
            DateTime DateTo = txtdatefrom.Text.Trim() == "" ? DateTime.Now : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateTo = DateTo;


            objpat.searchby = 1;
            List<IntakeOutputChartData> lstpat = objBO.GetIntakeOutputPatientDetail(objpat);
            if (lstpat.Count > 0)
            {
                txttotalintakechart.Text = lstpat[0].totalintakechart.ToString();
                txttotaloutputchart.Text = lstpat[0].totaloutputchart.ToString();
                txttotalbalancechart.Text = lstpat[0].totalbalancechart.ToString();
                GVIntakeOutput.DataSource = lstpat;
                GVIntakeOutput.DataBind();
                GVIntakeOutput.Visible = true;
                lblmessage.Visible = false;
                lblupmessage.Visible = false;
                Session["IntakeOutputChartDataList"] = lstpat;
                Messagealert_.ShowMessage(lblresult, "Total:" + lstpat.Count + " Record(s) found.", 1);
            }
            else
            {
                GVIntakeOutput.DataSource = null;
                GVIntakeOutput.DataBind();
                GVIntakeOutput.Visible = false;
                Messagealert_.ShowMessage(lblresult, "No Record Found", 0);
            }

        }
        protected void txtothers_TextChanged(object sender, EventArgs e)
        {
            double fluids1 = 0;
            double oral1 = 0;
            double urine1 = 0;
            double others1 = 0;
            double totalintake = 0;
            double totaloutput = 0;
            double totalbalance = 0;
            foreach (GridViewRow gvr in GVIntakeOutput.Rows)
            {
                TextBox fluids = (TextBox)(gvr.FindControl("txtFluids"));
                TextBox oral = (TextBox)(gvr.FindControl("txtoral"));
                TextBox urine = (TextBox)(gvr.FindControl("txturine"));
                TextBox others = (TextBox)(gvr.FindControl("txtothers"));

                fluids1 = Math.Round(Convert.ToDouble(fluids.Text),2);
                oral1 = Math.Round(Convert.ToDouble(oral.Text),2);
                urine1 = Math.Round(Convert.ToDouble(urine.Text),2);
                others1 = Math.Round(Convert.ToDouble(others.Text),2);
                totalintake = Math.Round(Convert.ToDouble(fluids.Text), 2) + Math.Round(Convert.ToDouble(oral.Text),2);
                totaloutput = Math.Round(Convert.ToDouble(urine.Text), 2) + Math.Round(Convert.ToDouble(others.Text),2);
                totalbalance = Math.Round(totaloutput,2)-Math.Round(totalintake,2);

                txttotalintakechart.Text = Math.Round(totalintake,2).ToString();
                txttotaloutputchart.Text = Math.Round(totaloutput,2).ToString();
                txttotalbalancechart.Text = Math.Round(totalbalance,2).ToString();
               
            }
        }
        protected void btnsave_Click(object sender, System.EventArgs e)
        {
            int result = 0;
            List<IntakeOutputChartData> lststudentlist = new List<IntakeOutputChartData>();
            IntakeOutputChartData Objpaic = new IntakeOutputChartData();
            IntakeOutputChartBO objInfoBO = new IntakeOutputChartBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            foreach (GridViewRow row in GVIntakeOutput.Rows)
            {
                Label lblID = (Label)row.Cells[0].FindControl("lblID");
                Label lblgvUHID = (Label)row.Cells[0].FindControl("lblgvUHID");
                Label lblgvName = (Label)row.Cells[0].FindControl("lblgvName");
                Label lblIPNo = (Label)row.Cells[0].FindControl("lblIPNo");
                TextBox txtdate = (TextBox)row.Cells[0].FindControl("txtdate");
                TextBox txtfluidsstart = (TextBox)row.Cells[0].FindControl("txtfluidsstart");
                TextBox txtfluidsend = (TextBox)row.Cells[0].FindControl("txtfluidsend");
                TextBox txtFluids = (TextBox)row.Cells[0].FindControl("txtFluids");
                TextBox txtoralstart = (TextBox)row.Cells[0].FindControl("txtoralstart");
                TextBox txtoralend = (TextBox)row.Cells[0].FindControl("txtoralend");
                TextBox txtoral = (TextBox)row.Cells[0].FindControl("txtoral");
                TextBox txturinestart = (TextBox)row.Cells[0].FindControl("txturinestart");
                TextBox txturineend = (TextBox)row.Cells[0].FindControl("txturineend");
                TextBox txturine = (TextBox)row.Cells[0].FindControl("txturine");
                TextBox txtothers = (TextBox)row.Cells[0].FindControl("txtothers");
                TextBox txtremarks = (TextBox)row.Cells[0].FindControl("txtremarks");
                TextBox txtTotalIntake = (TextBox)row.Cells[0].FindControl("txtTotalIntake");
                TextBox txtTotalOutput = (TextBox)row.Cells[0].FindControl("txtTotalOutput");
                TextBox txttotalbalance = (TextBox)row.Cells[0].FindControl("txttotalbalance");

                DateTime IntakeOutputDate = txtdate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                Objpaic.IntakeOutputDate = IntakeOutputDate;

                //DateTime IntakeOutputDate = txtdate.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + txtdate.Text.Trim());
                //Objpaic.IntakeOutputDate = IntakeOutputDate;
                DateTime fluidsstart = txtfluidsstart.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + txtfluidsstart.Text.Trim());
                Objpaic.fluidsstart = fluidsstart;
                DateTime fluidsend = txtfluidsend.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + txtfluidsend.Text.Trim());
                Objpaic.fluidsend = fluidsend;
                Objpaic.fluids = Math.Round(Convert.ToDouble(txtFluids.Text == "" ? "0.0" : txtFluids.Text),2);
                DateTime oralstart = txtoralstart.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + txtoralstart.Text.Trim());
                Objpaic.oralstart = oralstart;
                DateTime oralend = txtoralend.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + txtoralend.Text.Trim());
                Objpaic.oralend = oralend;
                Objpaic.oral = Math.Round(Convert.ToDouble(txtoral.Text == "" ? "0.0" : txtoral.Text),2);
                DateTime urinestart = txturinestart.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + txturinestart.Text.Trim());
                Objpaic.urinestart = urinestart;
                DateTime urineend = txturineend.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + txturineend.Text.Trim());
                Objpaic.urineend = urineend;
                Objpaic.urine = Math.Round(Convert.ToDouble(txturine.Text == "" ? "0.0" : txturine.Text),2);
                Objpaic.others = Math.Round(Convert.ToDouble(txtothers.Text == "" ? "0.0" : txtothers.Text),2);
                Objpaic.remarks = txtremarks.Text.Trim();
                Objpaic.PatientName = lblgvName.Text.Trim();
                Objpaic.IPNo = lblIPNo.Text.Trim();
                Objpaic.HospitalID = LogData.HospitalID;
                Objpaic.EmployeeID = LogData.EmployeeID;
                Objpaic.IPaddress = LogData.IPaddress;
                Objpaic.UHID = Convert.ToInt64(lblgvUHID.Text.Trim());               
                Objpaic.ID = Convert.ToInt64(lblID.Text.Trim());                
                double AB = Math.Round(Convert.ToDouble(txtFluids.Text == "" ? "0.0" : txtFluids.Text), 2) + Math.Round(Convert.ToDouble(txtoral.Text == "" ? "0.0" : txtoral.Text), 2);
                double CD = Math.Round(Convert.ToDouble(txturine.Text == "" ? "0.0" : txturine.Text), 2) + Math.Round(Convert.ToDouble(txtothers.Text == "" ? "0.0" : txtothers.Text), 2);
                double balance = CD - AB;
                Objpaic.totalintakechart = AB;
                Objpaic.totaloutputchart = CD;
                Objpaic.totalbalancechart = balance;


                if (txtpatientNames.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                int results = objInfoBO.InsertIntakeOutputdetails(Objpaic);
                result = results + 1;
            }
            
            if (result > 0)
            {
                DateTime DateFrom = txtdatefrom.Text.Trim() == "" ? DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                Objpaic.DateFrom = DateFrom;
                DateTime DateTo = txtdateto.Text.Trim() == "" ? DateTime.Now : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                Objpaic.DateTo = DateTo;
                Objpaic.searchby = 0;
                List<IntakeOutputChartData> objpat = objInfoBO.GetIntakeOutputPatientDetail(Objpaic);
                if (objpat.Count > 0)
                {
                   
                    GVIntakeOutput.DataSource = objpat;
                    GVIntakeOutput.DataBind();
                    GVIntakeOutput.Visible = true;
                    lblmessage.Visible = false;
                    Session["IntakeOutputChartDataList"] = objpat;
                    Messagealert_.ShowMessage(lblupmessage, "Save Successfully", 1);   
                
                }
                else
                {
                    Messagealert_.ShowMessage(lblupmessage, "system", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                }
                
            }

        }
        protected void btnprint_Click(object sender, System.EventArgs e)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            IntakeOutputChartData objpat = new IntakeOutputChartData();
            IntakeOutputChartBO objBO = new IntakeOutputChartBO();
            string IPNo = txtipno.Text == "" ? "" : txtipno.Text.Trim();
            DateTime DateFrom = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = DateFrom;
            DateTime DateTo = txtdatefrom.Text.Trim() == "" ? DateTime.Now : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateTo = DateTo;
            string url = "../MedNurse/Reports/ReportViewer.aspx?option=IntakeOutputChart&Ipno=" + IPNo.ToString() + "&DateFrom=" + DateFrom.ToString() + "&DateTo=" + DateTo.ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }      
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            lblupmessage.Visible = false;
            txtpatientNames.Text = "";
            txtage.Text = "";
            txtsex.Text = "";
            txtipno.Text = "";
            txtbedroom.Text = "";
            txtdoa.Text = "";
            txtdocter.Text = "";
            txtdateto.Text = "";
            txtdatefrom.Text = "";
            txttotalintakechart.Text = "";
            txttotaloutputchart.Text = "";
            txttotalbalancechart.Text = "";
            GVIntakeOutput.Visible = false;
            txtdatefrom.Text = "";
            txtdateto.Text = "";

           
        }      
       
     }

 }

     