using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.PatientBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.AdmissionData;
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
using Mediqura.Utility;
namespace Mediqura.Web.MedUtility
{
    public partial class UploadDocument : BasePage
    {
        string pathToSave = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }

        protected void txt_IPNo_TextChanged(object sender, EventArgs e)
        {
            DischargeIntimationData Objpaic = new DischargeIntimationData();
            DischargeIntimationBO objInfoBO = new DischargeIntimationBO();
            List<DischargeIntimationData> getResult = new List<DischargeIntimationData>();
            Objpaic.IPNo = txt_IPNo.Text.Trim() == "" ? "0" : txt_IPNo.Text.Trim();
            getResult = objInfoBO.GetPatientAdmissionDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txt_name.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();

            }
            else
            {
                txt_name.Text = "";
                txt_address.Text = "";
                txt_IPNo.Text = "";

            }
        }

        protected void txt_name_TextChanged(object sender, EventArgs e)
        {
            UploadFileData Objpaic = new UploadFileData();
            UploadFileBO objInfoBO = new UploadFileBO();
            List<UploadFileData> getResult = new List<UploadFileData>();
            Objpaic.PatientName = txt_name.Text.Trim() == "" ? "0" : txt_name.Text.Trim();
            getResult = objInfoBO.GetPatientAdmissionDetailsByName(Objpaic);
            if (getResult.Count > 0)
            {
                txt_IPNo.Text = getResult[0].IPNo.ToString();
                txt_address.Text = getResult[0].Address.ToString();

            }
            else
            {
                txt_name.Text = "";
                txt_address.Text = "";
                txt_IPNo.Text = "";

            }
        }

        void SaveFile(HttpPostedFile httpFile)
        {
           
            pathToSave = Server.MapPath("~/Upload/");
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
            try
            {
                pathToSave = Server.MapPath("~/Upload/");
                string filename = FileUpload1.FileName;
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
                    List<UploadFileData> fileList = Session["fileList"] == null ? new List<UploadFileData>() : (List<UploadFileData>)Session["fileList"];
                    UploadFileData Objfile = new UploadFileData();
                    Objfile.Tittle = txt_tittle.Text.ToString() == "" ? "0" : txt_tittle.Text.ToString();
                    Objfile.IPNo = txt_IPNo.Text.Trim() == "" ? "0" : txt_IPNo.Text.Trim();
                    Objfile.FileName = filename;
                    Objfile.FilePath = pathToSave;
                    Objfile.ContentType = contentType;

                    fileList.Add(Objfile);
                    if (fileList.Count > 0)
                    {
                        gvFileUpload.DataSource = fileList;
                        gvFileUpload.DataBind();
                        gvFileUpload.Visible = true;
                        Session["fileList"] = fileList;
                        txt_tittle.Text = "";
                        filename = "";
                        pathToSave = "";
                        contentType = "";
                        document = null;
                        //FileUpload1.SaveAs(pathToSave);
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
            txt_IPNo.Text = "";
            txt_name.Text = "";
            txt_address.Text = "";
            txt_tittle.Text = "";
            div1.Visible = false;
            gvFileUpload.DataSource = null;
            gvFileUpload.Visible = false;
            Session["fileList"] = null;
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
            pathToSave = Server.MapPath("~/FileUpload/");
            string filename = FileUpload1.FileName;
            pathToSave += filename;
            filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string extension = Path.GetExtension(filename);
            string contentType = FileUpload1.PostedFile.ContentType;
            HttpPostedFile file = FileUpload1.PostedFile;
            byte[] document = new byte[file.ContentLength];
            file.InputStream.Read(document, 0, file.ContentLength);
            List<UploadFileData> Listfile = new List<UploadFileData>();
            UploadFileData objUploadData = new UploadFileData();
            UploadFileBO objUploadrBO = new UploadFileBO();
            try
            {
                foreach (GridViewRow row in gvFileUpload.Rows)
                {
                    Label title = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lbltitle");
                    Label path = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lblfilePath");
                    Label Fname = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lblFname");
                    Label content = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lblcontentType");
                    Label IPNo = (Label)gvFileUpload.Rows[row.RowIndex].Cells[0].FindControl("lblIPNo");
                    UploadFileData objUploadDetails = new UploadFileData();
                    objUploadDetails.Tittle = title.Text == "" ? null : title.Text;
                    objUploadDetails.FileName = Fname.Text == "" ? null : Fname.Text;
                    objUploadDetails.ContentType = content.Text == "" ? null : content.Text;
                    objUploadDetails.FilePath = path.Text == "" ? null : path.Text;
                    objUploadDetails.IPNo = IPNo.Text == "" ? null : IPNo.Text;


                    //objUploadDetails.PdfDocument = Convert.ToByte(document);
                    Listfile.Add(objUploadDetails);

                }
                objUploadData.XMLData = XmlConvertor.FileDatatoXML(Listfile).ToString();
                objUploadData.IPNo = txt_IPNo.Text == "" ? null : txt_IPNo.Text;
                objUploadData.PatientName = txt_name.Text == "" ? null : txt_name.Text;
                objUploadData.Address = txt_address.Text == "" ? null : txt_address.Text;
                objUploadData.ActionType = Enumaction.Insert;

                int result = objUploadrBO.UpdateOPDLabBill(objUploadData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    Session["fileList"] = null;
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    //ViewState["ID"] = null;
                    //bindgrid();
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
                    //FileInfo file = new FileInfo(path);
                    if ((System.IO.File.Exists(path.Text)))
                    {
                        System.IO.File.Delete(path.Text);
                    }
                    List<UploadFileData> fileList = Session["fileList"] == null ? new List<UploadFileData>() : (List<UploadFileData>)Session["fileList"];
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

        protected void txtIPNo_TextChanged(object sender, EventArgs e)
        {
            if (txtIPNo.Text != "")
            {
                bindgridList();
            }
        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                bindgridList();
            }

        }
        protected void bindgridList()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Please enter valid from date.", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg3.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Please enter valid to date.", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg3.Visible = false;
                }
                List<UploadFileData> objdeposit = GetUploadList(0);
                if (objdeposit.Count > 0)
                {
                    gvUploadList.DataSource = objdeposit;
                    gvUploadList.DataBind();
                    gvUploadList.Visible = true;

                    Messagealert_.ShowMessage(lblresultList, "Total: " + objdeposit[0].MaximumRows.ToString() + " Record found", 1);
                    div4.Attributes["class"] = "SucessAlert";
                    div4.Visible = true;
                    //if (txt_name.Text == "")
                    //{
                    //    txt_name.Text = objdeposit[0].PatientName.ToString();
                    //}

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
        public List<UploadFileData> GetUploadList(int curIndex)
        {
            UploadFileData objpat = new UploadFileData();
            UploadFileBO objbillingBO = new UploadFileBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.IPNo = txtIPNo.Text == "" ? null : txtIPNo.Text;
            objpat.PatientName = txtName.Text == "" ? null : txtName.Text.Trim();
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objbillingBO.GetUploadList(objpat);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgridList();
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtIPNo.Text = "";
            txtName.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            lblresultList.Text = "";
            lblresultList.Visible = false;
            div4.Visible = false;
            div3.Visible = false;
            gvUploadList.DataSource = null;
            gvUploadList.Visible = false;
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
                    UploadFileData objpat = new UploadFileData();
                    UploadFileBO objbillingBO = new UploadFileBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvUploadList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
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

                    UploadFileBO objOTRoleMasterBO1 = new UploadFileBO();
                    int Result = objOTRoleMasterBO1.DeleteFileByID(objpat);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        div3.Visible = true;
                        div3.Attributes["class"] = "SucessAlert";
                        bindgridList();

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";

                    }

                    Label path = (Label)gr.Cells[0].FindControl("lblfilePath");
                    if ((System.IO.File.Exists(path.Text)))
                    {
                        System.IO.File.Delete(path.Text);
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        div3.Visible = true;
                        div3.Attributes["class"] = "SucessAlert";
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

       
      
    }
}