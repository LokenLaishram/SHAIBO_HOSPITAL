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
using System.Configuration;
using System.Data.SqlClient;
using Mediqura.Utility;

namespace Mediqura.Web.MedUtility
{
    public partial class LabSubTestMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                supplementoryvalues();

            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.LabGroups));
            ddl_labgroup.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue)));
            txt_parameter.Text = "";
            Commonfunction.PopulateDdl(ddl_machine, mstlookup.GetLookupsList(LookupName.MachineName));
            Commonfunction.PopulateDdl(ddl_template, mstlookup.GetLookupsList(LookupName.LabTemplate));
        }
        protected void supplementoryvalues()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Session["LabSubtestlist"] = null;
            Session["unitlist"] = null;
            Session["methodlist"] = null;
            Session["reagentlist"] = null;
            Session["samplelist"] = null;
            Session["rowtypelist"] = null;
            Session["containerlist"] = null;
            List<LookupItem> unitlist = Session["unitlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["unitlist"];
            Session["unitlist"] = mstlookup.GetLookupsList(LookupName.Unit);
            List<LookupItem> methodlist = Session["methodlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["methodlist"];
            Session["methodlist"] = mstlookup.GetLookupsList(LookupName.Method);
            List<LookupItem> reagentlist = Session["reagentlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["reagentlist"];
            Session["reagentlist"] = mstlookup.GetLookupsList(LookupName.Reagent);
            List<LookupItem> samplelist = Session["samplelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["samplelist"];
            Session["samplelist"] = mstlookup.GetLookupsList(LookupName.SmpleType);
            List<LookupItem> rowtypelist = Session["rowtypelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["rowtypelist"];
            Session["rowtypelist"] = mstlookup.GetLookupsList(LookupName.RowType);
            List<LookupItem> containerlist = Session["containerlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["containerlist"];
            Session["containerlist"] = mstlookup.GetLookupsList(LookupName.Container);

        }
        protected void ddl_labgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue)));
                txt_parameter.Text = "";
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
            }
        }
        protected void ddl_labsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            //Commonfunction.PopulateDdl(ddl_test, mstlookup.GetTestNameBySubGroupID(Convert.ToInt32(ddl_labsubgroup.SelectedValue)));
            AutoCompleteExtender2.ContextKey = ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            ServicesData Objpaic = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.ServiceTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetCenterwisetestservices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            Objpaic.LabSubGroupID = Convert.ToInt32(contextKey == "" ? "0" : contextKey);
            getResult = objInfoBO.GetLabServicesByserviceTypeID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }
        protected void txt_test_TextChanged(object sender, EventArgs e)
        {
            bindgrid();

        }

        protected void txt_testname_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                if (Commonfunction.SemicolonSeparation_String_32(txt_test.Text) == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "TestName", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_test.Text = "";
                    txt_test.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<LabServiceMasterData> objsubtestlist = GetLabSubtestlist(0, Commonfunction.SemicolonSeparation_String_32(txt_test.Text));
                if (objsubtestlist.Count > 0)
                {
                    GvLabSubTest.Visible = true;
                    ddl_machine.SelectedValue = objsubtestlist[0].MachineID.ToString();
                    ddl_template.SelectedValue = objsubtestlist[0].TemplateType.ToString();
                    txt_Reamrks.Text = objsubtestlist[0].Remarks.ToString();
                    List<LabServiceMasterData> LabServiceList = Session["LabSubtestlist"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["LabSubtestlist"];
                    Session["LabSubtestlist"] = null;
                    Session["LabSubtestlist"] = objsubtestlist;
                    GvLabSubTest.DataSource = Session["LabSubtestlist"];
                    GvLabSubTest.DataBind();
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvLabSubTest.Visible = true;
                    Session["LabSubtestlist"] = null;
                    GvLabSubTest.DataSource = Session["LabSubtestlist"];
                    GvLabSubTest.DataBind();
                    txt_Reamrks.Text = "";
                    ddl_machine.SelectedIndex = 0;
                    ddl_template.SelectedIndex = 0;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void GvLabSubTest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            List<LookupItem> unitlist = Session["unitlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["unitlist"];
            List<LookupItem> methodlist = Session["methodlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["methodlist"];
            List<LookupItem> reagentlist = Session["reagentlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["reagentlist"];
            List<LookupItem> samplelist = Session["samplelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["samplelist"];
            List<LookupItem> rowtypelist = Session["rowtypelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["rowtypelist"];
            List<LookupItem> containerlist = Session["containerlist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["containerlist"];

            foreach (GridViewRow row in GvLabSubTest.Rows)
            {
                try
                {
                    DropDownList ddl1 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[2].FindControl("ddl_unit");
                    DropDownList ddl2 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddl_sample");
                    DropDownList ddl3 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddlrowtype");
                    DropDownList ddl4 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddl_method");
                    DropDownList ddl5 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddl_reagent");
                    DropDownList ddl6 = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[3].FindControl("ddl_containerID");

                    Label UnitID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[2].FindControl("lblunitID");
                    Label SampleID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_sampletypeID");
                    Label RowTypeID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_rowtypeID");
                    Label MethodID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lblmethodID");
                    Label ReagentID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_reagenttypeID");
                    Label ContainerID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lblcontainerID");
                    Label Defaultchek = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_defaultID");

                    CheckBox chk = (CheckBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("chekbox");

                    if (Defaultchek.Text == "1")
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }

                    Commonfunction.PopulateDdl(ddl1, unitlist);
                    Commonfunction.PopulateDdl(ddl2, samplelist);
                    Commonfunction.PopulateDdl(ddl3, rowtypelist);
                    Commonfunction.PopulateDdl(ddl4, methodlist);
                    Commonfunction.PopulateDdl(ddl5, reagentlist);
                    Commonfunction.PopulateDdl(ddl6, containerlist);
                    if (UnitID.Text != "0")
                    {
                        ddl1.Items.FindByValue(UnitID.Text).Selected = true;
                    }
                    if (SampleID.Text != "0")
                    {
                        ddl2.Items.FindByValue(SampleID.Text).Selected = true;
                    }
                    if (RowTypeID.Text != "0")
                    {
                        ddl3.Items.FindByValue(RowTypeID.Text).Selected = true;
                    }
                    if (MethodID.Text != "0")
                    {
                        ddl4.Items.FindByValue(MethodID.Text).Selected = true;
                    }
                    if (ReagentID.Text != "0")
                    {
                        ddl5.Items.FindByValue(ReagentID.Text).Selected = true;
                    }
                    if (ContainerID.Text != "0")
                    {
                        ddl6.Items.FindByValue(ContainerID.Text).Selected = true;
                    }
                }
                catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            }

        }
        //protected void btnsave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (LogData.SaveEnable == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        if (ddl_labgroup.SelectedIndex == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "Group", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";

        //            ddl_labgroup.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        if (ddl_labsubgroup.SelectedIndex == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "Subgroup", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";

        //            ddl_labgroup.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        if (ddl_machine.SelectedIndex == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "MachineName", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";

        //            ddl_machine.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        List<LabServiceMasterData> List = new List<LabServiceMasterData>();
        //        LabServiceMasterData objlabserviceData = new LabServiceMasterData();
        //        LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
        //        foreach (GridViewRow row in GvLabSubTest.Rows)
        //        {
        //            TextBox order = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_order");
        //            Label ID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
        //            DropDownList unit = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_unit");
        //            DropDownList sample = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_sample");
        //            DropDownList reagent = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_reagent");
        //            DropDownList method = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_method");
        //            DropDownList ContainerID = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_containerID");

        //            TextBox Description = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_subtest");
        //            TextBox agefrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_age");
        //            TextBox ageto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_ageto");
        //            TextBox MNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemale");
        //            TextBox MNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemaleto");
        //            TextBox FNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemale");
        //            TextBox FNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemaleto");
        //            TextBox Tm_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemalefrom");
        //            TextBox Tm_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemaleto");
        //            TextBox Tf_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemalefrom");
        //            TextBox Tf_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemaleto");
        //            CheckBox chk = (CheckBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("chekbox");
        //            DropDownList rowtype = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddlrowtype");

        //            LabServiceMasterData obj1 = new LabServiceMasterData();

        //            obj1.UnitID = Convert.ToInt32(unit.SelectedValue == "" ? "0" : unit.SelectedValue);
        //            obj1.OrderNo = Convert.ToInt32(order.Text == "" ? "0" : order.Text);
        //            obj1.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
        //            obj1.SampleTypeID = Convert.ToInt32(sample.SelectedValue == "" ? "0" : sample.SelectedValue);
        //            obj1.ReagentTypeID = Convert.ToInt32(reagent.SelectedValue == "" ? "0" : reagent.SelectedValue);
        //            obj1.ContainerID = Convert.ToInt32(ContainerID.SelectedValue == "" ? "0" : ContainerID.SelectedValue);
        //            obj1.MethodID = Convert.ToInt32(method.SelectedValue == "" ? "0" : method.SelectedValue);
        //            obj1.defaultValue = chk.Checked ? 1 : 0;
        //            obj1.SubTestName = Description.Text.Trim();
        //            obj1.AgeRangeFrom = agefrom.Text.Trim();
        //            obj1.AgeRangeTo = ageto.Text.Trim();
        //            obj1.NormalRangeMaleFrom = MNrfrom.Text.Trim();
        //            obj1.NormalRangeMaleTo = MNrto.Text.Trim();
        //            obj1.NormalRangeFeMaleFrom = FNrfrom.Text.Trim();
        //            obj1.NormalRangeFeMaleTo = FNrto.Text.Trim();
        //            obj1.NormalRangeTransFeMaleFrom = Tf_Nrfrom.Text.Trim();
        //            obj1.NormalRangeTransFeMaleTo = Tf_Nrto.Text.Trim();
        //            obj1.NormalRangeTransMaleFrom = Tm_Nrfrom.Text.Trim();
        //            obj1.NormalRangeTransMaleTo = Tm_Nrto.Text.Trim();
        //            obj1.RowTypeID = Convert.ToInt32(rowtype.SelectedValue == "" ? "0" : rowtype.SelectedValue);
        //            List.Add(obj1);
        //        }
        //        objlabserviceData.XMLData = XmlConvertor.LabSubTestRecordDatatoXML(List).ToString();
        //        objlabserviceData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue);
        //        objlabserviceData.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue);
        //        objlabserviceData.TestID = Convert.ToInt32(ddl_test.SelectedValue == "" ? "0" : ddl_test.SelectedValue);
        //        objlabserviceData.MachineID = Convert.ToInt32(ddl_machine.SelectedValue == "" ? "0" : ddl_machine.SelectedValue);
        //        objlabserviceData.Remarks = txt_Reamrks.Text.Trim();
        //        objlabserviceData.EmployeeID = LogData.EmployeeID;
        //        objlabserviceData.FinancialYearID = LogData.FinancialYearID;
        //        objlabserviceData.IPaddress = LogData.IPaddress;
        //        objlabserviceData.HospitalID = LogData.HospitalID;
        //        objlabserviceData.ActionType = Enumaction.Insert;

        //        int result = objlabserviceBO.UpdateLabSubTest(objlabserviceData);
        //        if (result == 1)
        //        {
        //            bindgrid();
        //            Messagealert_.ShowMessage(lblmessage, "save", 1);
        //            supplementoryvalues();
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "SucessAlert";

        //        }
        //        else
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "system", 0);
        //            divmsg1.Visible = true;
        //            divmsg1.Attributes["class"] = "FailAlert";
        //        }

        //    }
        //    catch (Exception ex) //Exception in agent layer itself
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
        //        Messagealert_.ShowMessage(lblmessage, "system", 0);

        //    }
        //}
        protected void Bulk_Update(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[38] {
                 new DataColumn("PID", typeof(Int32)),
                 new DataColumn("OrderNo", typeof(Int32)),
                 new DataColumn("LabGroupID", typeof(Int32)),
                 new DataColumn("LabSubGroupID", typeof(Int32)),
                 new DataColumn("TestID", typeof(Int32)),
                 new DataColumn("SubTestCode", typeof(string)),
                 new DataColumn("SubTestName", typeof(string)),
                 new DataColumn("Unit", typeof(Int32)),
                 new DataColumn("Sample", typeof(Int32)),
                 new DataColumn("Machine", typeof(Int32)),
                 new DataColumn("Method", typeof(Int32)),
                 new DataColumn("Reagent", typeof(Int32)),
                 new DataColumn("ContainerID", typeof(Int32)),
                 new DataColumn("AgeFrom", typeof(string)),
                 new DataColumn("AgeTo", typeof(string)),
                 new DataColumn("AgeFromD", typeof(Int32)),
                 new DataColumn("AgeToD", typeof(Int32)),
                 new DataColumn("NormalRangeMaleFrom", typeof(string)),
                 new DataColumn("NormalRangeMaleTo", typeof(string)),
                 new DataColumn("NormalRangeFeMaleFrom", typeof(string)),
                 new DataColumn("NormalRangeFeMaleTo", typeof(string)),
                 new DataColumn("NormalRangeTransFeMaleFrom", typeof(string)),
                 new DataColumn("NormalRangeTransFeMaleTo", typeof(string)),
                 new DataColumn("NormalRangeTransMaleFrom", typeof(string)),
                 new DataColumn("NormalRangeTransMaleTo", typeof(string)),
                 new DataColumn("RowType", typeof(Int32)),
                 new DataColumn("TemplateType", typeof(Int32)),
                 new DataColumn("DefaultValue", typeof(Int32)),
                 new DataColumn("UserLoginID", typeof(Int32)),
                 new DataColumn("AddedBy", typeof(string)),
                 new DataColumn("AddedDate", typeof(DateTime)),
                 new DataColumn("ModifiedDate", typeof(DateTime)),
                 new DataColumn("ModifiedBy", typeof(string)),
                 new DataColumn("HospitalID", typeof(Int32)),
                 new DataColumn("FinancialYearID", typeof(Int32)),
                 new DataColumn("RangeWording", typeof(string)),
                 new DataColumn("Remark", typeof(string)),
                 new DataColumn("IsActive", typeof(string))
               });
            int parametercount = 0;
            foreach (GridViewRow row in GvLabSubTest.Rows)
            {
                TextBox order = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_order");
                Label IDs = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                DropDownList unit = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_unit");
                DropDownList samples = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_sample");
                DropDownList reagent = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_reagent");
                DropDownList method = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_method");
                DropDownList ContainerIDs = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddl_containerID");

                TextBox Description = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_subtest");
                TextBox agefrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_age");
                TextBox ageto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_ageto");
                TextBox MNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemale");
                TextBox MNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangemaleto");
                TextBox FNrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemale");
                TextBox FNrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangefemaleto");
                TextBox Tm_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemalefrom");
                TextBox Tm_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransemaleto");
                TextBox Tf_Nrfrom = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemalefrom");
                TextBox Tf_Nrto = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_normalrangetransefemaleto");
                Label GroupID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_groupdID");
                Label SubGroupID = (Label)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("lbl_subgroupID");
                CheckBox chk = (CheckBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("chekbox");
                DropDownList rowtype = (DropDownList)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("ddlrowtype");
                TextBox RangeWording = (TextBox)GvLabSubTest.Rows[row.RowIndex].Cells[0].FindControl("txt_Rangewording");

                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime pardate = System.DateTime.Now;
                parametercount = parametercount + 1;
                int PID = Convert.ToInt32(IDs.Text == "" ? "0" : IDs.Text);
                int OrderNo = Convert.ToInt32(order.Text == "" ? "0" : order.Text);
                int Unit = Convert.ToInt32(unit.SelectedValue == "" ? "0" : unit.SelectedValue);
                int Sample = Convert.ToInt32(samples.SelectedValue == "" ? "0" : samples.SelectedValue);
                int Reagent = Convert.ToInt32(reagent.SelectedValue == "" ? "0" : reagent.SelectedValue);
                int ContainerID = Convert.ToInt32(ContainerIDs.SelectedValue == "" ? "0" : ContainerIDs.SelectedValue);
                int Method = Convert.ToInt32(method.SelectedValue == "" ? "0" : method.SelectedValue);
                int DefaultValue = chk.Checked ? 1 : 0;
                string SubTestName = Description.Text;
                string AgeFrom = agefrom.Text.Trim();
                string AgeTo = ageto.Text.Trim();
                Int32 AgeFromD = 0;
                Int32 AgeToD = 0;
                string NormalRangeMaleFrom = MNrfrom.Text.Trim();
                string NormalRangeMaleTo = MNrto.Text.Trim();
                string NormalRangeFeMaleFrom = FNrfrom.Text.Trim();
                string NormalRangeFeMaleTo = FNrto.Text.Trim();
                string NormalRangeTransFeMaleFrom = Tf_Nrfrom.Text.Trim();
                string NormalRangeTransFeMaleTo = Tf_Nrto.Text.Trim();
                string NormalRangeTransMaleFrom = Tm_Nrfrom.Text.Trim();
                string NormalRangeTransMaleTo = Tm_Nrto.Text.Trim();
                int RowType = Convert.ToInt32(rowtype.SelectedValue == "" ? "0" : rowtype.SelectedValue);
                int TemplateType = Convert.ToInt32(ddl_template.SelectedValue == "" ? "0" : ddl_template.SelectedValue);
                int LabGroupID = Convert.ToInt32(GroupID.Text == "" ? "0" : GroupID.Text);
                int LabSubGroupID = Convert.ToInt32(SubGroupID.Text == "" ? "0" : SubGroupID.Text);
                int TestID = Commonfunction.SemicolonSeparation_String_32(txt_test.Text);
                int Machine = Convert.ToInt32(ddl_machine.SelectedValue == "" ? "0" : ddl_machine.SelectedValue);
                string Remark = txt_Reamrks.Text.Trim();
                string rangeremark =RangeWording.Text.Trim();
                Int64 UserLoginID = LogData.EmployeeID;
                int FinancialYearID = LogData.FinancialYearID;
                int HospitalID = LogData.HospitalID;
                string SubTestCode = "";
                string AddedBy = LogData.EmployeeID.ToString();
                DateTime AddedDate = pardate;
                DateTime ModifiedDate = pardate;
                string ModifiedBy = LogData.EmployeeID.ToString();
                int IsActive = 1;
                dt.Rows.Add(PID, OrderNo, LabGroupID, LabSubGroupID, TestID, SubTestCode, SubTestName,
                 Unit, Sample, Machine, Method, Reagent, ContainerID, AgeFrom, AgeTo, AgeFromD, AgeToD, NormalRangeMaleFrom,
                 NormalRangeMaleTo, NormalRangeFeMaleFrom, NormalRangeFeMaleTo, NormalRangeTransFeMaleFrom,
                 NormalRangeTransFeMaleTo, NormalRangeTransMaleFrom, NormalRangeTransMaleTo, RowType, TemplateType,
                 DefaultValue, UserLoginID, AddedBy, AddedDate, ModifiedDate, ModifiedBy, HospitalID, FinancialYearID, rangeremark, Remark, IsActive);
            }
            if (parametercount == 0)
            {
                txt_parameter.Focus();
                Messagealert_.ShowMessage(lblmessage, "Paracount", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_MDQ_Update_LabParameters"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tlabpara", dt);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        bindgrid();
                        Messagealert_.ShowMessage(lblmessage, "save", 1);
                        supplementoryvalues();
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                        LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                    }
                }
            }
        }
        private List<LabServiceMasterData> GetLabSubtestlist(int p, int testID)
        {
            LabServiceMasterData objlabserviceData = new LabServiceMasterData();
            LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
            objlabserviceData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "0" ? null : ddl_labgroup.SelectedValue);
            objlabserviceData.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "0" ? null : ddl_labsubgroup.SelectedValue);
            objlabserviceData.TestID = testID;
            objlabserviceData.SubTestName = txt_parameter.Text.Trim();
            return objlabserviceBO.SearchLabSubTestDetails(objlabserviceData);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            ddl_labgroup.SelectedIndex = 0;
            ddl_labsubgroup.SelectedIndex = 0;
            ddl_machine.SelectedIndex = 0;
            ddl_template.SelectedIndex = 0;
            Session["LabSubtestlist"] = null;
            GvLabSubTest.DataSource = Session["LabSubtestlist"];
            GvLabSubTest.DataBind();
            GvLabSubTest.Visible = false;
            txt_test.Text = "";
            txt_parameter.Text = "";
            txt_Reamrks.Text = "";
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.LabGroups));
            ddl_labgroup.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue)));
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvLabSubTest.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvLabSubTest.Columns[2].Visible = false;
                    GvLabSubTest.Columns[3].Visible = false;
                    GvLabSubTest.Columns[4].Visible = false;
                    GvLabSubTest.Columns[5].Visible = false;
                    GvLabSubTest.Columns[6].Visible = false;
                    GvLabSubTest.Columns[17].Visible = false;
                    GvLabSubTest.Columns[18].Visible = false;
                    GvLabSubTest.Columns[19].Visible = false;

                    GvLabSubTest.RenderControl(hw);
                    GvLabSubTest.HeaderRow.Style.Add("width", "15%");
                    GvLabSubTest.HeaderRow.Style.Add("font-size", "10px");
                    GvLabSubTest.Style.Add("text-decoration", "none");
                    GvLabSubTest.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabSubTest.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LabSubGroupDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";

                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Parameter Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LabParameterDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        protected DataTable GetDatafromDatabase()
        {
            List<LabServiceMasterData> LabServiceDetails = GetLabSubtestlist(0, Commonfunction.SemicolonSeparation_String_32(txt_test.Text));
            List<LabSubTestDatatoExcel> ListexcelData = new List<LabSubTestDatatoExcel>();
            int i = 0;
            foreach (LabServiceMasterData row in LabServiceDetails)
            {
                LabSubTestDatatoExcel ExcelSevice = new LabSubTestDatatoExcel();
                //ExcelSevice.SubTestCode = LabServiceDetails[i].Code;
                ExcelSevice.SeqNo = LabServiceDetails[i].SeqNo;
                ExcelSevice.SubTestName = LabServiceDetails[i].SubTestName;
                ExcelSevice.Unit = LabServiceDetails[i].Unit;
                ExcelSevice.SampleType = LabServiceDetails[i].SampleType;
                ExcelSevice.Reagent = LabServiceDetails[i].Reagent;
                ExcelSevice.Method = LabServiceDetails[i].Method;
                ExcelSevice.Container = LabServiceDetails[i].Container;
                ExcelSevice.AgeRangeFrom = LabServiceDetails[i].AgeRangeFrom;
                ExcelSevice.AgeRangeTo = LabServiceDetails[i].AgeRangeTo;
                ExcelSevice.NormalRangeMaleFrom = LabServiceDetails[i].NormalRangeMaleFrom;
                ExcelSevice.NormalRangeMaleTo = LabServiceDetails[i].NormalRangeMaleTo;
                ExcelSevice.NormalRangeFeMaleFrom = LabServiceDetails[i].NormalRangeFeMaleFrom;
                ExcelSevice.NormalRangeFemaleTo = LabServiceDetails[i].NormalRangeFeMaleTo;

                ListexcelData.Add(ExcelSevice);
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
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Commonfunction.SemicolonSeparation_String_32(txt_test.Text) == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "TestName", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_test.Text = "";
                txt_test.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            List<LabServiceMasterData> LabSubtestlist = Session["LabSubtestlist"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["LabSubtestlist"];
            LabServiceMasterData ObjService = new LabServiceMasterData();
            ObjService.SubTestName = txt_parameter.Text.ToString();
            ObjService.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue);
            ObjService.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue);
            ObjService.ID = 0;
            ObjService.OrderNo = ((GvLabSubTest.Rows.Count) + 1);
            LabSubtestlist.Add(ObjService);
            if (LabSubtestlist.Count > 0)
            {
                GvLabSubTest.DataSource = LabSubtestlist;
                GvLabSubTest.DataBind();
                GvLabSubTest.Visible = true;
                Session["LabSubtestlist"] = LabSubtestlist;
                txt_parameter.Text = "";
                //txt_parameter.Focus();
            }
            else
            {
                GvLabSubTest.DataSource = null;
                GvLabSubTest.DataBind();
                GvLabSubTest.Visible = true;
            }
        }
        protected void gviplabsubtestlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabSubTest.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    if (ID.Text == "0")
                    {
                        List<LabServiceMasterData> LabServiceList = Session["LabSubtestlist"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["LabSubtestlist"];
                        LabServiceList.RemoveAt(i);
                        if (LabServiceList.Count > 0)
                        {
                            Session["LabSubtestlist"] = LabServiceList;
                            GvLabSubTest.DataSource = LabServiceList;
                            GvLabSubTest.DataBind();
                        }
                        else
                        {
                            Session["LabSubtestlist"] = LabServiceList;
                            GvLabSubTest.DataSource = LabServiceList;
                            GvLabSubTest.DataBind();
                        }
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
                    }
                    else
                    {
                        LabServiceMasterData objlabserviceData = new LabServiceMasterData();
                        LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
                        objlabserviceData.ID = Convert.ToInt32(ID.Text);
                        objlabserviceData.EmployeeID = LogData.EmployeeID;
                        objlabserviceData.ActionType = Enumaction.Delete;
                        LabServiceMasterBO objlabserviceBO1 = new LabServiceMasterBO();
                        int Result = objlabserviceBO1.DeleteLabSubTestDetailsByID(objlabserviceData);
                        if (Result == 1)
                        {
                            bindgrid();
                            //List<LabServiceMasterData> LabServiceList = Session["LabSubtestlist"] == null ? new List<LabServiceMasterData>() : (List<LabServiceMasterData>)Session["LabSubtestlist"];
                            //LabServiceList.RemoveAt(i);
                            //if (LabServiceList.Count > 0)
                            //{
                            //    Session["LabSubtestlist"] = LabServiceList;
                            //    GvLabSubTest.DataSource = LabServiceList;
                            //    GvLabSubTest.DataBind();
                            //}
                            //else
                            //{
                            //    Session["LabSubtestlist"] = LabServiceList;
                            //    GvLabSubTest.DataSource = LabServiceList;
                            //    GvLabSubTest.DataBind();
                            //}
                        }
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
        protected void btn_search_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
    }
}