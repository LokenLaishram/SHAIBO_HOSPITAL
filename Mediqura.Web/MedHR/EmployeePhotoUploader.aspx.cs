using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedHrData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;



namespace Mediqura.Web.MedHR
{
    public partial class EmployeePhotoUploader : BasePage
    {
        string pathToSavePhoto = "";
        string pathToSaveSign = "";
        string ID1 = "";
        string folderPhotto = "";
        string folderSign = "";
        string filename = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                Commonfunction.Insertzeroitemindex(ddl_employee);
            }

        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_department, mstlookup.GetLookupsList(LookupName.Department));


        }

        protected void gvEmpList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //CheckBox cb1 = (CheckBox)e.Row.FindControl("chksource");
                    //CheckBox cb2 = (CheckBox)e.Row.FindControl("chkdestination");
                    Label IsSource = (Label)e.Row.FindControl("lbl_source");
                    FileUpload empphotouploader = (FileUpload)e.Row.FindControl("empphotouploader");
                    //Button btnUp = (Button)e.Row.FindControl("btnupdate");
                    CheckBox cb3 = (CheckBox)e.Row.FindControl("chksourceSign");
                    CheckBox cb4 = (CheckBox)e.Row.FindControl("chkDestnSign");
                    Label IsDestn = (Label)e.Row.FindControl("lbl_Signsource");
                    FileUpload emSignuploader = (FileUpload)e.Row.FindControl("empSignuploader");
                    //Button btnSign = (Button)e.Row.FindControl("btnDigiSgn");
                    if (IsSource.Text == "1")
                    {
                        //cb1.Checked = true;
                        //cb1.Enabled = false;
                        //cb2.Checked = true;
                        //cb2.Enabled = false;
                        //empphotouploader.Enabled = false;
                        //btnUp.Enabled = false;
                    }
                    else
                    {
                        //cb1.Checked = false;
                        //cb2.Checked = false;
                        //empphotouploader.Enabled = true;
                        //btnUp.Enabled = true;
                    }
                    if (IsDestn.Text == "1")
                    {
                        //cb3.Checked = true;
                        //cb3.Enabled = false;
                        //cb4.Checked = true;
                        //cb4.Enabled = false;
                        //emSignuploader.Enabled = false;
                        //btnSign.Enabled = false;
                    }
                    else
                    {
                        //cb3.Checked = false;
                        //cb4.Checked = false;
                        //emSignuploader.Enabled = true;
                        //btnSign.Enabled = true;
                    }
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
        }

        protected void ddl_department_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_employee, mstlookup.GetEmployeeByDep(Convert.ToInt32(ddl_department.SelectedValue))); ;
        }

        protected void btnsearch_Click(object sender, System.EventArgs e)
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
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_department.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_department.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                List<EmployeeData> objdeposit = GetPhotoList(0);
                if (objdeposit.Count > 0)
                {
                    btn_save.Visible = true;
                    gvEmpList.DataSource = objdeposit;
                    gvEmpList.DataBind();
                    gvEmpList.Visible = true;

                }
                else
                {
                    btn_save.Visible = false;
                    gvEmpList.DataSource = null;
                    gvEmpList.DataBind();
                    gvEmpList.Visible = true;
                    lblresult.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        public List<EmployeeData> GetPhotoList(int curIndex)
        {
            EmployeeData objpat = new EmployeeData();
            EmpFileBO objbillingBO = new EmpFileBO();
            objpat.DepartmentID = Convert.ToInt32(ddl_department.SelectedValue == "" ? null : ddl_department.SelectedValue);
            objpat.EmployeeID = Convert.ToInt64(ddl_employee.SelectedValue == "" ? null : ddl_employee.SelectedValue);
            return objbillingBO.GetPhotoList(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvEmpList.DataSource = null;
            gvEmpList.DataBind();
            gvEmpList.Visible = false;
            ddl_department.SelectedIndex = 0;
            ddl_employee.SelectedIndex = 0;
            lblresult.Visible = false;
            lblresult.Text = "";
            lblmessage.Visible = false;
            btn_save.Visible = false;
        }
        protected void gvEmpList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Upload")
                {
                    EmployeeData objempphoto = new EmployeeData();
                    EmpFileBO objempBO = new EmpFileBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvEmpList.Rows[i];
                    CheckBox cb = (CheckBox)gr.Cells[0].FindControl("chksource"); //find the C
                    CheckBox cb1 = (CheckBox)gr.Cells[0].FindControl("chkdestination"); //find the C
                    FileUpload Empphotouploader = (FileUpload)gr.Cells[0].FindControl("empphotouploader");
                    CheckBox cbSource = (CheckBox)gr.Cells[0].FindControl("chksource"); //find the C
                    CheckBox cbDestination = (CheckBox)gr.Cells[0].FindControl("chkdestination"); //find the C
                    Button btnUpdate = (Button)gr.Cells[0].FindControl("btnupdate"); //find the C
                    Label empID = (Label)gr.Cells[0].FindControl("code");
                    if (cb != null && cb1 != null)
                    {
                        if (cb.Checked && cb1.Checked)
                        {
                            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                            if (cbSource.Checked == true && cbDestination.Checked == true)
                            {
                                string fileName = Empphotouploader.FileName;
                                if (fileName == "")
                                {
                                    objempphoto.EmployeePhotoLocation = "../EduImages/EmpDummyPh.jpg";
                                    objempphoto.ImageFile = null;
                                }
                                else
                                {
                                    if (!Empphotouploader.HasFile)
                                    {
                                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                                        return;
                                    }
                                    else
                                    {
                                        //Photo Path
                                        if (Directory.Exists(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/") == false)
                                            Directory.CreateDirectory(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/");

                                        if (File.Exists(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/" + fileName))
                                        {
                                            File.Delete(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/" + fileName);
                                            // return "exist";
                                        }
                                        Empphotouploader.SaveAs(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/" + fileName);
                                        string path = @"~/MedHR/EmployeePhoto/" + fileName;


                                        objempphoto.EmployeePhotoLocation = path;
                                        //imageuploader as bit image
                                        int length = Empphotouploader.PostedFile.ContentLength;
                                        //create a byte array to store the binary image data
                                        byte[] imgbyte = new byte[length];
                                        //store the currently selected file in memeory
                                        HttpPostedFile img = Empphotouploader.PostedFile;
                                        //set the binary data
                                        img.InputStream.Read(imgbyte, 0, length);
                                        objempphoto.ImageFile = imgbyte;

                                        if (path == "fail" || objempphoto.EmployeePhotoLocation == "")
                                        {
                                            Messagealert_.ShowMessage(lblmessage, "system", 0);
                                            return;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    objempphoto.EmployeeID = Convert.ToInt64(empID.Text == "" ? null : empID.Text);
                    int results = objempBO.UpLoadPhotoEmp(objempphoto);
                    if (results == 1)
                    {
                        bindgrid();
                        btnUpdate.Attributes["disabled"] = "disabled";
                        lblmessage.Visible = true;
                        Messagealert_.ShowMessage(lblmessage, "update", 1);
                        div1.Attributes["class"] = "SucessAlert";
                        div1.Visible = true;
                        //btn_save.Attributes["disabled"] = "disabled";
                    }

                }
                if (e.CommandName == "Signature")
                {
                    EmployeeData objempphoto = new EmployeeData();
                    EmpFileBO objempBO = new EmpFileBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvEmpList.Rows[i];
                    CheckBox cb = (CheckBox)gr.Cells[0].FindControl("chksourceSign"); //find the C
                    CheckBox cb1 = (CheckBox)gr.Cells[0].FindControl("chkDestnSign"); //find the C
                    FileUpload EmpSignuploader = (FileUpload)gr.Cells[0].FindControl("empSignuploader");
                    CheckBox cbSource = (CheckBox)gr.Cells[0].FindControl("chksource"); //find the C
                    CheckBox cbDestination = (CheckBox)gr.Cells[0].FindControl("chkdestination"); //find the C
                    Button btnUpdate = (Button)gr.Cells[0].FindControl("btnupdate"); //find the C
                    Label empID = (Label)gr.Cells[0].FindControl("code");
                    if (cb != null && cb1 != null)
                    {
                        if (cb.Checked && cb1.Checked)
                        {
                            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                            if (cbSource.Checked == true && cbDestination.Checked == true)
                            {
                                string fileName = EmpSignuploader.FileName;
                                if (fileName == "")
                                {
                                    objempphoto.DigitalSignatureLocation = "../EduImages/EmpDummyPh.jpg";
                                    objempphoto.SignatureFile = null;
                                }
                                else
                                {
                                    if (!EmpSignuploader.HasFile)
                                    {
                                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                                        return;
                                    }
                                    else
                                    {
                                        //Photo Path
                                        if (Directory.Exists(Request.PhysicalApplicationPath + @"~/MedHR/EmployeeSign/") == false)
                                            Directory.CreateDirectory(Request.PhysicalApplicationPath + @"~/MedHR/EmployeeSign/");

                                        if (File.Exists(Request.PhysicalApplicationPath + @"~/MedHR/EmployeeSign/" + fileName))
                                        {
                                            File.Delete(Request.PhysicalApplicationPath + @"~/MedHR/EmployeeSign/" + fileName);
                                            // return "exist";
                                        }
                                        EmpSignuploader.SaveAs(Request.PhysicalApplicationPath + @"~/MedHR/EmployeeSign/" + fileName);
                                        string path = @"~/MedHR/EmployeeSign/" + fileName;


                                        objempphoto.DigitalSignatureLocation = path;
                                        //imageuploader as bit image
                                        int length = EmpSignuploader.PostedFile.ContentLength;
                                        //create a byte array to store the binary image data
                                        byte[] Signbyte = new byte[length];
                                        //store the currently selected file in memeory
                                        HttpPostedFile img = EmpSignuploader.PostedFile;
                                        //set the binary data
                                        img.InputStream.Read(Signbyte, 0, length);
                                        objempphoto.SignatureFile = Signbyte;

                                        if (path == "fail" || objempphoto.EmployeePhotoLocation == "")
                                        {
                                            Messagealert_.ShowMessage(lblmessage, "system", 0);
                                            return;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    objempphoto.EmployeeID = Convert.ToInt64(empID.Text == "" ? null : empID.Text);
                    int results = objempBO.UpLoadSignEmp(objempphoto);
                    if (results == 1)
                    {
                        bindgrid();
                        btnUpdate.Attributes["disabled"] = "disabled";
                        lblmessage.Visible = true;
                        Messagealert_.ShowMessage(lblmessage, "update", 1);
                        div1.Attributes["class"] = "SucessAlert";
                        div1.Visible = true;
                        //btn_save.Attributes["disabled"] = "disabled";
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

        protected void btn_update_Click(object sender, System.EventArgs e)
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

            List<EmployeeData> Listfile = new List<EmployeeData>();
            EmployeeData objempdata = new EmployeeData();
            EmpFileBO objBO = new EmpFileBO();
            try
            {
                // get all the record from the gridview
                int countbed = 0;
                foreach (GridViewRow row in gvEmpList.Rows)
                {
                    CheckBox cb = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chksource"); //find the C
                    CheckBox cb1 = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chkdestination"); //find the C
                    FileUpload Empphotouploader = (FileUpload)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("empphotouploader");
                    Label empID = (Label)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("code");
                    CheckBox cbSource = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chksource"); //find the C
                    CheckBox cbDestination = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chkdestination"); //find the C
                    EmployeeData ObjDetails = new EmployeeData();
                    if (cb != null && cb1 != null)
                    {
                        if (cb.Checked && cb1.Checked)
                        {
                            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                            if (cbSource.Checked == true && cbDestination.Checked == true)
                            {
                                string fileName = Empphotouploader.FileName;
                                if (fileName == "")
                                {
                                    objempdata.EmployeePhotoLocation = "../EduImages/EmpDummyPh.jpg";
                                    objempdata.ImageFile = null;
                                }
                                else
                                {
                                    if (!Empphotouploader.HasFile)
                                    {
                                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                                        return;
                                    }
                                    else
                                    {
                                        //Photo Path
                                        if (Directory.Exists(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/") == false)
                                            Directory.CreateDirectory(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/");

                                        if (File.Exists(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/" + fileName))
                                        {
                                            File.Delete(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/" + fileName);
                                            // return "exist";
                                        }
                                        Empphotouploader.SaveAs(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/" + fileName);
                                        string path = @"~/MedHR/EmployeePhoto/" + fileName;


                                        ObjDetails.EmployeePhotoLocation = path;
                                        //imageuploader as bit image
                                        int length = Empphotouploader.PostedFile.ContentLength;
                                        //create a byte array to store the binary image data
                                        byte[] imgbyte = new byte[length];
                                        //store the currently selected file in memeory
                                        HttpPostedFile img = Empphotouploader.PostedFile;
                                        //set the binary data
                                        img.InputStream.Read(imgbyte, 0, length);
                                        ObjDetails.ImageFile = imgbyte;

                                        if (path == "fail" || objempdata.EmployeePhotoLocation == "")
                                        {
                                            Messagealert_.ShowMessage(lblmessage, "system", 0);
                                            return;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    ObjDetails.EmployeeID = Convert.ToInt64(empID.Text == "" ? null : empID.Text);
                    Listfile.Add(ObjDetails);
                }
                objempdata.XMLData = XmlConvertor.EmpPhototoXML(Listfile).ToString();
                objempdata.ActionType = Enumaction.Insert;
                int result = objBO.UpdateEmpPhoto(objempdata);
                if (result == 1)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
                    //btn_save.Attributes["disabled"] = "disabled";
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

        protected void btn_reset_Click(object sender, System.EventArgs e)
        {
            Commonfunction.Insertzeroitemindex(ddl_employee);
            ddl_department.SelectedIndex = 0;
            gvEmpList.DataSource = null;
            gvEmpList.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
            divmsg1.Visible = false;

        }

        protected void btn_save_Click(object sender, System.EventArgs e)
        {
            Int64 empID = 0;
            Byte[] ImageFile = null;
            Byte[] SignatureFile = null;
            List<EmployeeData> Listfile = new List<EmployeeData>();
            EmployeeData objempdata = new EmployeeData();
            EmpFileBO objBO = new EmpFileBO();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[14]
            {
                
             new DataColumn("EmployeeID", typeof(Int64)),
	         new DataColumn("EmployeePhotoLocation", typeof(string)),
	         new DataColumn("ImageFile", typeof(System.Byte[])),
             new DataColumn("IsPhotoUploaded", typeof(Int32)),
             new DataColumn("DigitalSignatureLocation", typeof(string)),
             new DataColumn("DigitalSignature", typeof(System.Byte[])),
             new DataColumn("IsDigiSignUploaded", typeof(Int32)),
             new DataColumn("AddedBy", typeof(string)),
	         new DataColumn("AddedDate", typeof(DateTime)),
             new DataColumn("ModifiedDate", typeof(DateTime)),
	         new DataColumn("ModifiedBy", typeof(string)),
	         new DataColumn("HospitalID", typeof(Int32)),
	         new DataColumn("FinancialYearID", typeof(Int32)),
	         new DataColumn("IsActive", typeof(string))
	     
                     });

            // get all the record from the gridview
            int countbed = 0;
            foreach (GridViewRow row in gvEmpList.Rows)
            {
                CheckBox cb = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chksource"); //find the C
                CheckBox cb1 = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chkdestination"); //find the C
                FileUpload Empphotouploader = (FileUpload)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("empphotouploader");
                FileUpload EmpSignuploader = (FileUpload)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("empSignuploader");
                Label lblempID = (Label)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("code");
                CheckBox cbSource = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chksource"); //find the C
                CheckBox cbDestination = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chkdestination"); //find the C
                CheckBox cbDigiSrc = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chksourceSign"); //find the C
                CheckBox cbDigiDestn = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chkDestnSign"); //find the C
                CheckBox cbSourceSign = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chksourceSign"); //find the C
                CheckBox cbDestinationSign = (CheckBox)gvEmpList.Rows[row.RowIndex].Cells[0].FindControl("chksourceSign"); //find the C
                EmployeeData ObjDetails = new EmployeeData();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime pardate = System.DateTime.Now;

                IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);

                string fileName = Empphotouploader.FileName;
                string fileSign = EmpSignuploader.FileName;
                if (Empphotouploader.PostedFile.ContentLength > 204800)
                {
                    Messagealert_.ShowMessage(lblmessage, "Signature", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                //204800
                if (EmpSignuploader.PostedFile.ContentLength > 204800)
                {
                    Messagealert_.ShowMessage(lblmessage, "Photo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                if (fileName == "" && fileSign == "")
                {
                    objempdata.EmployeePhotoLocation = "../MedImages/DummyImage.png";
                    objempdata.ImageFile = null;
                    objempdata.DigitalSignatureLocation = "../MedImages/DummyImage.png";
                    objempdata.SignatureFile = null;
                }
                else
                {
                    Int64 EmpID = Convert.ToInt64(lblempID.Text == "" ? null : lblempID.Text);
                    if (fileName != "")
                    {
                        folderPhotto = Server.MapPath(@"~/MedHR/EmployeePhoto/" + EmpID + "/");
                        if (!Directory.Exists(folderPhotto))
                        {
                            Directory.CreateDirectory(folderPhotto);
                        }

                        if (File.Exists(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/" + EmpID + "/" + fileName))
                        {
                            File.Delete(folderPhotto);

                        }
                        //Empphotouploader.SaveAs(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/" + fileName);
                        Empphotouploader.SaveAs(Request.PhysicalApplicationPath + @"MedHR/EmployeePhoto/" + EmpID + "/" + fileName);
                    }
                    if (fileSign != "")
                    {
                        folderSign = Server.MapPath(@"~/MedHR/EmployeeSign/" + EmpID + "/");
                        if (!Directory.Exists(folderSign))
                        {
                            Directory.CreateDirectory(folderSign);
                        }
                        if (File.Exists(Request.PhysicalApplicationPath + @"~/MedHR/EmployeeSign/" + EmpID + "/" + fileName))
                        {
                            File.Delete(folderSign);

                        }
                        EmpSignuploader.SaveAs(Request.PhysicalApplicationPath + @"MedHR/EmployeeSign/" + EmpID + "/" + fileSign);
                    }

                    string path = @"~/MedHR/EmployeePhoto/" + EmpID + "/";
                    //Empphotouploader.SaveAs(Request.PhysicalApplicationPath + @"~/MedHR/EmployeePhoto/" + EmpID + "/" + fileName);
                    string pathPhoto = Server.MapPath(@"~/MedHR/EmployeePhoto/" + EmpID + "/" + filename);

                    path += fileName;


                    string SignPath = @"~/MedHR/EmployeeSign/" + EmpID + "/";
                    string pathSign = Server.MapPath(@"~/MedHR/EmployeeSign/" + EmpID + "/" + fileSign);
                    SignPath += fileSign;


                    ObjDetails.EmployeePhotoLocation = path;
                    string EmployeePhotoLocation = path;

                    ObjDetails.DigitalSignatureLocation = SignPath;
                    string DigitalSignatureLocation = SignPath;
                    //imageuploader as bit image
                    int length = Empphotouploader.PostedFile.ContentLength;

                    //create a byte array to store the binary image data
                    Byte[] imgbyte = new Byte[length];
                    //store the currently selected file in memeory
                    HttpPostedFile img = Empphotouploader.PostedFile;
                    //set the binary data
                    img.InputStream.Read(imgbyte, 0, length);
                    //ObjDetails.ImageFile = imgbyte;
                    if (fileName != "")
                    {
                        ImageFile = imgbyte;
                    }
                    else
                    {
                        ImageFile = null;
                    }
                    //Empphotouploader.SaveAs(pathPhoto);

                    //Signatureuploader as bit image
                    int lengthSign = EmpSignuploader.PostedFile.ContentLength;
                    //create a byte array to store the binary image data
                    Byte[] imgbyteSign = new Byte[lengthSign];
                    //store the currently selected file in memeory
                    HttpPostedFile sign = EmpSignuploader.PostedFile;
                    //set the binary data
                    sign.InputStream.Read(imgbyteSign, 0, lengthSign);
                    //ObjDetails.SignatureFile = imgbyteSign;
                    if (fileSign != "")
                    {
                        SignatureFile = imgbyteSign;
                    }
                    else
                    {
                        SignatureFile = null;
                    }
                    //EmpSignuploader.SaveAs(pathSign);
                    if (path == "fail" || objempdata.EmployeePhotoLocation == "" || SignPath == "fail" || objempdata.DigitalSignatureLocation == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        return;
                    }

                    Int64 EmployeeID = Convert.ToInt64(lblempID.Text == "" ? null : lblempID.Text);
                    empID = EmployeeID;
                    Int64 UserLoginID = LogData.EmployeeID;
                    int FinancialYearID = LogData.FinancialYearID;
                    int HospitalID = LogData.HospitalID;
                    string AddedBy = "";
                    DateTime AddedDate = pardate;
                    DateTime ModifiedDate = pardate;
                    string ModifiedBy = "";
                    int IsActive = 1;
                    int IsPhotoUploaded = 1;
                    int IsDigiSignUploaded = 1;
                    dt.Rows.Add(EmployeeID, EmployeePhotoLocation, ImageFile, IsPhotoUploaded, DigitalSignatureLocation, SignatureFile, IsDigiSignUploaded, AddedBy, AddedDate, ModifiedDate,
                    ModifiedBy, HospitalID, FinancialYearID, IsActive);

                }


            }

            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
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

                using (SqlCommand cmd = new SqlCommand("usp_MDQ_util_UpdateEmpPhoto"))
                //using (SqlCommand cmd = new SqlCommand("usp_MDQ_util_UpdateEmpPhotoNEW"))
                {

                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tempPhoto", dt);
                        cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = LogData.EmployeeID;
                        //cmd.Parameters.Add("@EmpID", SqlDbType.BigInt).Value = Convert.ToInt64(dt.Rows[0]["EmployeeID"].ToString());
                        cmd.Parameters.Add("@HospitalID", SqlDbType.Int).Value = LogData.HospitalID;
                        cmd.Parameters.Add("@FinancialYearID", SqlDbType.Int).Value = LogData.FinancialYearID;
                        cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        int result = Convert.ToInt32(cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction);
                        if (result == 1)
                        {
                            bindgrid();
                            Messagealert_.ShowMessage(lblmessage, "save", 1);
                            div1.Visible = true;
                            div1.Attributes["class"] = "SucessAlert";
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                        LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                    }
                }
            }

        }
    }
}