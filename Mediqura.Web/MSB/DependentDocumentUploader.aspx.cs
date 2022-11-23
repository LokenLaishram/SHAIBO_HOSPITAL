using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.BOL.MSBBO;
using Mediqura.BOL.PatientBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedHrData;
using Mediqura.CommonData.MSBData;
using Mediqura.CommonData.PatientData;
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
using System.IO;
using Mediqura.Utility;

namespace Mediqura.Web.MSB
{
    public partial class DependentDocumentUploader : BasePage
    {
        string pathToSave = "";
        string ID1 = "";
        string UHID="";
        string UHIDList = "";
        string folder = "";
        string filename = "";
        string pathToDB = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Commonfunction.Insertzeroitemindex(ddl_Dependent);
                Commonfunction.Insertzeroitemindex(ddl_dependentList);
            }
         
          
        }
        private void ddlbind()
        {
            int empID = Convert.ToInt32(txtEmpName.Text.Contains(":") ? txtEmpName.Text.Substring(txtEmpName.Text.LastIndexOf(':') + 1) : "0");
            //int empIDlist = Convert.ToInt32(txt_EmpNameList.Text.Contains(":") ? txt_EmpNameList.Text.Substring(txt_EmpNameList.Text.LastIndexOf(':') + 1) : "0");
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_Dependent, mstlookup.GetDependentByempID(empID));
          

        }
        private void ddlbindList()
        {
            int empIDlist = Convert.ToInt32(txt_EmpNameList.Text.Contains(":") ? txt_EmpNameList.Text.Substring(txt_EmpNameList.Text.LastIndexOf(':') + 1) : "0");
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_dependentList, mstlookup.GetDependentByempID(empIDlist));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmpdetails(string prefixText, int count, string contextKey)
        {

            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            Objpaic.EmployeeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetEmpdetails(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }

        void SaveFile(HttpPostedFile httpFile)
        {


            pathToSave = Server.MapPath("~/MSB/DependentDocument/");
            string filename = FileUpload1.FileName;
            string fileCheck = pathToSave + filename;
            string tempFileToCheck = "";
            if (System.IO.File.Exists(fileCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(fileCheck))
                {
                    tempFileToCheck = counter.ToString() + filename;
                    fileCheck = pathToSave + tempFileToCheck;
                    counter++;
                    filename = tempFileToCheck;
                    lblmessage.Text = "A file with the same name is already exist." + "<br/> your file was saved as" + filename;
                }
            }
            else
            {
                lblmessage.Visible = true;
                lblmessage.Text = "File uploaded successfully";
            }
            pathToSave += filename;
            FileUpload1.SaveAs(pathToSave);
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            var prefix = "";
            var DependantName = ddl_Dependent.SelectedItem.Text;
            string empName="";
            string depName = "";
            var source1 = txtEmpName.Text.ToString();
            if (source1.Contains(":"))
            {
                //ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                //int indexStart = source1.LastIndexOf('>');
                //int indexStop = source1.LastIndexOf(',');
                //int count = indexStop - (indexStart + 1);
                //empName = source1.Substring(indexStart + 1, count);
                ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                //int indexStart = source1.LastIndexOf('>');
                int indexStop = source1.LastIndexOf(',');
                //int count = indexStop - (indexStart + 1);
                empName = source1.Substring(0, indexStop);

            }
            if (DependantName.Contains(":"))
            {
                int indexStop = DependantName.LastIndexOf(',');
                depName = DependantName.Substring(0, indexStop);
                UHID = DependantName.Substring(DependantName.LastIndexOf(':') + 1);
                hdnDep.Value = depName;

            }

            try
            {

                if (txtEmpName.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "empname", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtEmpName.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (ddl_Dependent.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Dependent", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_Dependent.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txt_tittle.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Tittle", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_tittle.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (FileUpload1.FileName == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "DependentFile", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    FileUpload1.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                folder = Server.MapPath(@"~/MSB/DependentDocument/" + ID1 + "/"+UHID+"/");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    pathToSave = Server.MapPath(@"~/MSB/DependentDocument/" + ID1 + "/" + UHID + "/");
                    //prefix = "Cert_";
                    filename =FileUpload1.FileName;
                    pathToDB = ID1 + "/" + UHID + "/" + filename;
                    hdnfile.Value = filename;
               
                pathToSave += filename;
                if (!FileUpload1.HasFile & FileUpload1.PostedFile == null)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "select a file to upload";

                }
                else
                {
                    filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string extension = Path.GetExtension(filename);
                    string contentType = FileUpload1.PostedFile.ContentType;
                    HttpPostedFile file = FileUpload1.PostedFile;
                    byte[] document = new byte[file.ContentLength];
                    file.InputStream.Read(document, 0, file.ContentLength);
                    string title = txt_tittle.Text;
                    foreach (GridViewRow row in gvFileUpload.Rows)
                    {

                        Label gridTitle = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lblTitle");
                        if (title == gridTitle.ToString())
                        {
                            txt_tittle.Text = "";
                            filename = "";
                            pathToSave = "";
                            contentType = "";
                            document = null;
                            Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                            div1.Visible = true;
                            div1.Attributes["class"] = "FailAlert";
                            txt_tittle.Focus();
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }
                    }
                    FileUpload1.SaveAs(pathToSave);
                    List<DocumentUploadData> fileList = Session["fileList"] == null ? new List<DocumentUploadData>() : (List<DocumentUploadData>)Session["fileList"];
                    DocumentUploadData Objfile = new DocumentUploadData();
                    Objfile.Tittle = txt_tittle.Text.ToString() == "" ? "0" : txt_tittle.Text.ToString();
                    Objfile.EmployeeID = Convert.ToInt64(ID1 == "" ? "0" : ID1);
                    Objfile.EmpName = empName.ToString();
                    Objfile.DependentName = depName;
                    //Objfile.DependentName = hdnDep.ToString();
                    Objfile.UHID = Convert.ToInt64(UHID=="" ? "0":UHID);
                    Objfile.FileName = hdnfile.Value; ;
                    Objfile.FilePath = pathToDB;
                    Objfile.ContentType = contentType;

                    fileList.Add(Objfile);
                    if (fileList.Count > 0)
                    {
                        gvFileUpload.DataSource = fileList;
                        gvFileUpload.DataBind();
                        gvFileUpload.Visible = true;
                        Session["fileList"] = fileList;
                        filename = "";
                        pathToSave = "";
                        contentType = "";
                        document = null;
                        txt_tittle.Text = "";
                        ddl_Dependent.SelectedIndex = 0;
                   
                    }
                    else
                    {
                        gvFileUpload.DataSource = null;
                        gvFileUpload.DataBind();
                        gvFileUpload.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }

        }


        protected void btn_reset_Click(object sender, EventArgs e)
        {
            txtEmpName.Text = "";
            txt_tittle.Text = "";
            ddl_Dependent.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddl_Dependent);
            gvFileUpload.DataSource = null;
            gvFileUpload.Visible = false;
            Session["fileList"] = null;
            //divmsg3.Visible = false;
            //lblresult.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
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
            if (txtEmpName.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "empname", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtEmpName.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (gvFileUpload.Rows.Count == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "AddDependentDoc", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                //ddlDocType.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            pathToSave = Server.MapPath(@"~/" + folder + "/");
            string filename = hdnfile.Value;
            pathToSave += filename;
            filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string extension = Path.GetExtension(filename);
            string contentType = FileUpload1.PostedFile.ContentType;
            HttpPostedFile file = FileUpload1.PostedFile;
            byte[] document = new byte[file.ContentLength];
            file.InputStream.Read(document, 0, file.ContentLength);
            List<DocumentUploadData> Listfile = new List<DocumentUploadData>();
            DocumentUploadData objUploadData = new DocumentUploadData();
            DocumentUploadBO objUploadrBO = new DocumentUploadBO();
            try
            {
                foreach (GridViewRow row in gvFileUpload.Rows)
                {
                    Label tittle = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lbltitle");
                    Label path = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lblfilePath");
                    Label Fname = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lblFname");
                    Label content = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lblcontentType");
                    Label empNo = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lblempno");
                    Label UHID = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lbl_UHID");
                    DocumentUploadData objUploadDetails = new DocumentUploadData();
                    objUploadDetails.Tittle = tittle.Text == "" ? "" : tittle.Text;
                    objUploadDetails.FileName = Fname.Text == "" ? "" : Fname.Text;
                    objUploadDetails.ContentType = content.Text == "" ? "" : content.Text;
                    objUploadDetails.FilePath = path.Text == "" ? "" : path.Text;
                    objUploadDetails.EmployeeID = Convert.ToInt64(empNo.Text == "" ? "" : empNo.Text);
                    objUploadDetails.UHID = Convert.ToInt64(UHID.Text == "" ? "" : UHID.Text);
                    //objUploadDetails.PdfDocument = Convert.ToByte(document);
                    Listfile.Add(objUploadDetails);
                }
                objUploadData.XMLData = XmlConvertor.DependentDocumenttoXML(Listfile).ToString();
                //objUploadData.Tittle = txt_tittle.Text.Trim() == "" ? "" : txt_tittle.Text.Trim();
                objUploadData.DependentName = hdnDep.Value;
                objUploadData.EmployeeID = LogData.EmployeeID;
                //objUploadData.UHID = Convert.ToInt64(uhid == "" ? null : uhid);
                objUploadData.ActionType = Enumaction.Insert;
                int result = objUploadrBO.UpdateEmpFile(objUploadData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    Session["fileList"] = null;
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    
                }
                else if (result == 5)
                {
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }
                else
                    Messagealert_.ShowMessage(lblmessage, "system", 0);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }

        }

        protected void gvFileUpload_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
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
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvFileUpload.Rows[i];
                    Label path = (Label)gr.Cells[0].FindControl("lblfilePath");
                    //pathToSave = Server.MapPath(@"~/MSB/DependentDocument/" + path.Text.ToString());
                    string urlPath = Server.MapPath(@"~/MSB/DependentDocument/" + path.Text.ToString());
                    //string urlPath = "../MSB/DependentDocument/" + path.Text.ToString();
                    //FileInfo file = new FileInfo(path);
                    if ((System.IO.File.Exists(urlPath)))
                    {
                        System.IO.File.Delete(urlPath);
                    }
                    List<DocumentUploadData> fileList = Session["fileList"] == null ? new List<DocumentUploadData>() : (List<DocumentUploadData>)Session["fileList"];
                    fileList.RemoveAt(i);
                    Session["fileList"] = fileList;
                    gvFileUpload.DataSource = fileList;
                    gvFileUpload.DataBind();
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

        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {
            ddlbind();
        }
        protected void txt_EmpNameList_TextChanged(object sender, EventArgs e)
        {
            ddlbindList();
        }
        protected void bindgridList()
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
                if (txt_EmpNameList.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "empname", 0);
                    div3.Visible = true;
                    div3.Attributes["class"] = "FailAlert";
                    txt_EmpNameList.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                    div3.Visible = false;
                }

                List<DocumentUploadData> objdeposit = GetUploadList(0);
                if (objdeposit.Count > 0)
                {
                    gvUploadList.DataSource = objdeposit;
                    gvUploadList.DataBind();
                    gvUploadList.Visible = true;

                    Messagealert_.ShowMessage(lblresult, "Total: " + objdeposit[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    div3.Visible = true;
                    lblmessage2.Visible = true;

                }
                else
                {


                    gvUploadList.DataSource = null;
                    gvUploadList.DataBind();
                    gvUploadList.Visible = true;
                    gvUploadList.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }

        }

        public List<DocumentUploadData> GetUploadList(int curIndex)
        {
            string depNameList = "";
            var source1 = txt_EmpNameList.Text.ToString();
            if (source1.Contains(":"))
            {
                ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                //var empfolder = txtEmpName.Text;
            }
            var DependantName = ddl_dependentList.SelectedItem.Text;
            if (DependantName.Contains(":"))
            {
                int indexStop = DependantName.LastIndexOf(',');
                depNameList = DependantName.Substring(0, indexStop);
                UHIDList = DependantName.Substring(DependantName.LastIndexOf(':') + 1);
                //hdnDep.Value = depNameList;

            }

            DocumentUploadData objpat = new DocumentUploadData();
            DocumentUploadBO objbillingBO = new DocumentUploadBO();
            objpat.EmployeeID = Convert.ToInt64(ID1 == "" ? null : ID1);
            objpat.Tittle = txtTittleList.Text == "" ? null : txtTittleList.Text.Trim();
            objpat.UHID = Convert.ToInt64(UHIDList == "" ? null : UHIDList);
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            //DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //objpat.DateFrom = from;
            //objpat.DateTo = To;
            return objbillingBO.GetUploadList(objpat);
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_EmpNameList.Text = "";
            txtTittleList.Text = "";
            ddl_dependentList.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddl_dependentList);
            lblresult.Text = "";
            lblresult.Visible = false;
            divmsg3.Visible = false;
            div3.Visible = false;
            lblmessage2.Visible = false;
            gvUploadList.DataSource = null;
            gvUploadList.Visible = false;
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            bindgridList();
        }

        protected void gvUploadList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
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
                    DocumentUploadData objpat = new DocumentUploadData();
                    DocumentUploadBO objFileBO = new DocumentUploadBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvUploadList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("codeList");
                    objpat.fileID = Convert.ToInt64(ID.Text);
                    objpat.EmployeeID = LogData.EmployeeID;
                    objpat.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objpat.Remarks = txtremarks.Text;
                    }

                    DocumentUploadBO objOTRoleMasterBO1 = new DocumentUploadBO();
                    int Result = objOTRoleMasterBO1.DeleteDepFiledetailByID(objpat);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        lblmessage2.Visible = true;
                        div3.Visible = true;
                        div3.Visible = true;
                        div3.Attributes["class"] = "SucessAlert";
                        Label path = (Label)gr.Cells[0].FindControl("lblfilePath");
                        string urlPath = Server.MapPath(@"~/MSB/DependentDocument/" + path.Text.ToString());
                        if ((System.IO.File.Exists(urlPath)))
                        {
                            System.IO.File.Delete(urlPath);
                        }
                        bindgridList();

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";

                    }

                   

                }
               if (e.CommandName == "VIEW")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvUploadList.Rows[i];
                    Label path = (Label)gr.Cells[0].FindControl("lblfilePath");
                    string urlPath = "../MSB/DependentDocument/" + path.Text.ToString();
                    string fullURL = "window.open('" + urlPath + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }

        }

    }
}