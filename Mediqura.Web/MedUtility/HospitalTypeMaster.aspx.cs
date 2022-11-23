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

namespace Mediqura.Web.MedUtility
{
    public partial class HospitalTypeMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                bindddl();
            }
        }
        //protected string getDigitalPath(string fileName)
        //{
        //    string path = "";
        //    try
        //    {
        //         if (Directory.Exists(Request.PhysicalApplicationPath + @"MedLogo/ ") == false)
        //            Directory.CreateDirectory(Request.PhysicalApplicationPath + @"MedLogo/ ");

        //        if (File.Exists(Request.PhysicalApplicationPath + @"MedLogo/ " + fileName))
        //        {
        //            File.Delete(Request.PhysicalApplicationPath + @"MedLogo/ " + fileName);
        //        }
        //         FileUploadLogo.SaveAs(Request.PhysicalApplicationPath + @"MedLogo/ " + fileName);
        //        path = @"MedLogo/" + fileName;
        //    }
        //    catch
        //    {
        //        return "fail";
        //    }
        //  return path;

        //}
        
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_country, mstlookup.GetLookupsList(LookupName.Country));
            Commonfunction.PopulateDdl(ddl_state, mstlookup.GetLookupsList(LookupName.State));
            Commonfunction.PopulateDdl(ddl_district, mstlookup.GetLookupsList(LookupName.District));
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
            //    string folderPath = Server.MapPath("~/MedLogo/");

            //    //Check whether Directory (Folder) exists.
            //    if (!Directory.Exists(folderPath))
            //    {
            //        //If Directory (Folder) does not exists. Create it.
            //        Directory.CreateDirectory(folderPath);
            //    }

            //    //Save the File to the Directory (Folder).
            //    FileUploadLogo.SaveAs(folderPath + Path.GetFileName(FileUploadLogo.FileName));

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

                if (txt_hospitalcode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_hospitalcode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_hospitalname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "HospitalName", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_hospitalname.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_hospitaladdress.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "HospitalAddress", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_hospitaladdress.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_country.SelectedValue == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Country", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_country.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_state.SelectedValue == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "State", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_state.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_pincode.Text == "")
                {
                    if (Commonfunction.Checkvalidpin(txt_pincode.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Pin", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

                        txt_pincode.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_website.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Website", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_website.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_emailid.Text == "")
                {
                    if (Commonfunction.Checkemail(txt_emailid.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Email", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

                        txt_emailid.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_recognitionNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "RecogNo", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_recognitionNo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_phoneno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "PhoneNo", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_phoneno.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_MobileNo.Text == "")
                {
                    if (Commonfunction.Checkvalidmobile(txt_MobileNo.Text) == false)
                    {
                      
                    Messagealert_.ShowMessage(lblmessage, "MobileNo",0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_MobileNo.Focus();
                    return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_faxNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "FaxNo", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_faxNo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
               
                HospitalTypeMasterData objHospitalTypeMasterData = new HospitalTypeMasterData();
                HospitalTypeMasterBO objHospitalTypeMasterBO = new HospitalTypeMasterBO();
                //IFormatProvider option = new System.Globalization.CultureInfo("en-GB",true);
                ////string fileName =FileUpload_Logo.FileName.ToString();                           
                //string fileName = Path.GetFileName(FileUploadLogo.FileName);
                //if (fileName =="")
                //{
                //    objHospitalTypeMasterData.LogoLocationimage = "../MedLogo/.png";
                //    objHospitalTypeMasterData.Logoimage = null;

                //}
                //else
                //{
                //    if (!FileUploadLogo.HasFile)
                //    {
                //        Messagealert_.ShowMessage(lblmessage, "system", 0);
                //        return;
                //    }
                //    else
                //    {
                //        string path = getDigitalPath(fileName);
                //        objHospitalTypeMasterData.LogoLocationimage = path;
                //        int length = FileUploadLogo.PostedFile.ContentLength;
                //        byte[] imgbyte = new byte[length];
                //        HttpPostedFile img = FileUploadLogo.PostedFile;
                //        img.InputStream.Read(imgbyte, 0, length);
                //        objHospitalTypeMasterData.Logoimage = imgbyte;
                //        if (path == "fail" || objHospitalTypeMasterData.LogoLocationimage=="")
                //        {
                //            Messagealert_.ShowMessage(lblmessage, "system", 0);
                //            return;
                //        }
                      
                //    }

                //}
                objHospitalTypeMasterData.HospitalTypeCode = txt_hospitalcode.Text == "" ? null : txt_hospitalcode.Text;
                objHospitalTypeMasterData.HospitalName = txt_hospitalname.Text == "" ? null : txt_hospitalname.Text;
                objHospitalTypeMasterData.HospitalAddress = txt_hospitaladdress.Text == "" ? null : txt_hospitaladdress.Text;
                objHospitalTypeMasterData.CountryID = Convert.ToInt32(ddl_country.SelectedValue == "0" ? null : ddl_country.SelectedValue);
                objHospitalTypeMasterData.StateID = Convert.ToInt32(ddl_state.SelectedValue == "0" ? null : ddl_state.SelectedValue);
                objHospitalTypeMasterData.DistrictID = Convert.ToInt32(ddl_district.SelectedValue == "0" ? null : ddl_district.SelectedValue);
                objHospitalTypeMasterData.PinNo = Convert.ToInt32(txt_pincode.Text == "" ? null : txt_pincode.Text);
                objHospitalTypeMasterData.Website = txt_website.Text == "" ? null : txt_website.Text;
                objHospitalTypeMasterData.EmailID = txt_emailid.Text == "" ? null : txt_emailid.Text;
                objHospitalTypeMasterData.RecognitionNo = txt_recognitionNo.Text == "" ? null : txt_recognitionNo.Text;
                objHospitalTypeMasterData.PhoneNo = txt_phoneno.Text == "" ? null : txt_phoneno.Text;
                objHospitalTypeMasterData.MobileNo = txt_MobileNo.Text == "" ? null : txt_MobileNo.Text;
                objHospitalTypeMasterData.FaxNo = txt_faxNo.Text == "" ? null : txt_faxNo.Text;
                objHospitalTypeMasterData.EmployeeID = LogData.EmployeeID;
                objHospitalTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objHospitalTypeMasterData.HospitalID = LogData.HospitalID;
                objHospitalTypeMasterData.FinancialYearID = LogData.FinancialYearID;
                objHospitalTypeMasterData.IPaddress = LogData.IPaddress;
          
                objHospitalTypeMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        objHospitalTypeMasterData.ActionType = Enumaction.Update;
                        objHospitalTypeMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objHospitalTypeMasterBO.UpdateHospitalTypeDetails(objHospitalTypeMasterData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";

                    ViewState["ID"] = null;
                    bindgrid();
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

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
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void GvHospitalType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    HospitalTypeMasterData objHospitalTypeMasterData = new HospitalTypeMasterData();
                    HospitalTypeMasterBO objHospitalTypeMasterBO = new HospitalTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvHospitalType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objHospitalTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objHospitalTypeMasterData.ActionType = Enumaction.Select;

                    List<HospitalTypeMasterData> GetResult = objHospitalTypeMasterBO.GetHospitalTypeDetailsByID(objHospitalTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_hospitalcode.Text = GetResult[0].HospitalTypeCode;
                        txt_hospitalname.Text = GetResult[0].HospitalName;
                        txt_hospitaladdress.Text = GetResult[0].HospitalAddress;
                        ddl_country.SelectedValue = GetResult[0].CountryID.ToString();
                        ddl_state.SelectedValue = GetResult[0].StateID.ToString();
                        ddl_district.SelectedValue = GetResult[0].DistrictID.ToString();
                        ddl_district.SelectedValue = GetResult[0].DistrictID.ToString();
                        txt_pincode.Text = GetResult[0].PinNo.ToString();
                        txt_website.Text = GetResult[0].Website;
                        txt_emailid.Text = GetResult[0].EmailID;
                        txt_recognitionNo.Text = GetResult[0].RecognitionNo;
                        txt_phoneno.Text = GetResult[0].PhoneNo;
                        txt_MobileNo.Text = GetResult[0].MobileNo;
                        txt_faxNo.Text = GetResult[0].FaxNo;
                        ViewState["ID"] = GetResult[0].ID;
                    }
                }
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
                    HospitalTypeMasterData objHospitalTypeMasterData = new HospitalTypeMasterData();
                    HospitalTypeMasterBO objHospitalTypeMasterBO = new HospitalTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvHospitalType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objHospitalTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objHospitalTypeMasterData.EmployeeID = LogData.EmployeeID;
                    objHospitalTypeMasterData.ActionType = Enumaction.Delete;
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
                        objHospitalTypeMasterData.Remarks = txtremarks.Text;
                    }

                    HospitalTypeMasterBO objHospitalTypeMasterBO1 = new HospitalTypeMasterBO();
                    int Result = objHospitalTypeMasterBO1.DeleteHospitalTypeDetailsByID(objHospitalTypeMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";

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
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private void bindgrid()
        {
            try
            {
                
                List<HospitalTypeMasterData> lstemp = GetHospitalType(0);

                if (lstemp.Count > 0)
                {
                    GvHospitalType.DataSource = lstemp;
                    GvHospitalType.DataBind();
                    GvHospitalType.Visible = true;

                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";

                 }
                else
                {
                    GvHospitalType.DataSource = null;
                    GvHospitalType.DataBind();
                    GvHospitalType.Visible = true;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<HospitalTypeMasterData> GetHospitalType(int p)
        {
            HospitalTypeMasterData objHospitalTypeMasterData = new HospitalTypeMasterData();
            HospitalTypeMasterBO objHospitalTypeMasterBO = new HospitalTypeMasterBO();
            objHospitalTypeMasterData.HospitalTypeCode = txt_hospitalcode.Text == "" ? null : txt_hospitalcode.Text.Trim();
            objHospitalTypeMasterData.HospitalName = txt_hospitalname.Text == "" ? null : txt_hospitalname.Text.Trim();
            objHospitalTypeMasterData.HospitalAddress = txt_hospitaladdress.Text == "" ? null : txt_hospitaladdress.Text.Trim();
            objHospitalTypeMasterData.CountryID = Convert.ToInt32(ddl_country.SelectedValue == "" ? null : ddl_country.SelectedValue);
            objHospitalTypeMasterData.StateID = Convert.ToInt32(ddl_state.SelectedValue == "" ? null : ddl_state.SelectedValue);
            objHospitalTypeMasterData.DistrictID = Convert.ToInt32(ddl_district.SelectedValue == "" ? null : ddl_district.SelectedValue);
            objHospitalTypeMasterData.PinNo = Convert.ToInt32(txt_pincode.Text == "" ? null : txt_pincode.Text);
            objHospitalTypeMasterData.Website = txt_website.Text == "" ? null : txt_website.Text.Trim();
            objHospitalTypeMasterData.EmailID = txt_emailid.Text == "" ? null : txt_emailid.Text.Trim();
            objHospitalTypeMasterData.RecognitionNo = txt_recognitionNo.Text == "" ? null : txt_recognitionNo.Text.Trim();
            objHospitalTypeMasterData.PhoneNo = txt_phoneno.Text == "" ? null : txt_phoneno.Text.Trim();
            objHospitalTypeMasterData.MobileNo = txt_MobileNo.Text == "" ? null : txt_MobileNo.Text.Trim();
            objHospitalTypeMasterData.FaxNo = txt_faxNo.Text == "" ? null : txt_faxNo.Text.Trim();
            objHospitalTypeMasterData.EmployeeID = LogData.EmployeeID;
            objHospitalTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objHospitalTypeMasterBO.SearchHospitalTypeDetails(objHospitalTypeMasterData);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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

            bindgrid();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txt_hospitalcode.Text = "";
            txt_hospitalname.Text = "";
            txt_hospitaladdress.Text = "";
            ddl_country.SelectedValue = "0";
            ddl_state.SelectedValue = "0";
            ddl_district.SelectedValue = "0";
            ddl_district.SelectedValue = "0";
            txt_pincode.Text = "";
            txt_website.Text = "";
            txt_emailid.Text = "";
            txt_recognitionNo.Text = "";
            txt_phoneno.Text = "";
            txt_MobileNo.Text = "";
            txt_faxNo.Text = "";
            GvHospitalType.DataSource = null;
            GvHospitalType.DataBind();
            GvHospitalType.Visible = false;
         }
    }
}