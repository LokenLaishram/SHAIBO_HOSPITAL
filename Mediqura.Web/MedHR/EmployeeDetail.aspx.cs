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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedHR
{
    public partial class EmployeeDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                //initialize();
            }
        }
        private void initialize()
        {
            string IP = Commonfunction.GetClientIPAddress();
            string URL = "http://" + IP + ":8080";
            Boolean flag = Commonfunction.isValidURL(URL);
            if (flag)
            {
                btnFPScan.Visible = true;
            }
            else
            {
                btnFPScan.Visible = false;
            }
        }

        protected void ddlmarital_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlmarital.SelectedIndex == 1)
            {
                txt_spousename.ReadOnly = false;
            }
            else
            {
                txt_spousename.ReadOnly = true;
            }
        }
        protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_status.SelectedIndex == 1)
            {
                txtremarks.ReadOnly = false;
            }
            else
            {
                txtremarks.ReadOnly = true;
            }

        }

        protected void chekbox_CheckedChanged(object sender, EventArgs e)
        {

            if (chekbox.Checked)
            {
                txt_address1.ReadOnly = true;
                ddl_country1.Attributes["disabled"] = "disabled";
                ddl_state1.Attributes["disabled"] = "disabled";
                ddl_district1.Attributes["disabled"] = "disabled";
                txtpin1.ReadOnly = true;
                txt_address1.Text = txtaddress.Text;
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_district1, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddlstate.SelectedValue == "" ? "0" : ddlstate.SelectedValue)));
                ddl_country1.SelectedValue = ddlcountry.SelectedValue;
                ddl_state1.SelectedValue = ddlstate.SelectedValue;
                ddl_district1.SelectedValue = ddldistrict.SelectedValue;
                txtpin1.Text = txtpin.Text;
                txtemail.Focus();
            }
            else
            {
                txt_address1.ReadOnly = false;
                ddl_country1.Attributes.Remove("disabled");
                ddl_state1.Attributes.Remove("disabled");
                ddl_district1.Attributes.Remove("disabled");
                txtpin1.ReadOnly = false;
                txt_address1.Text = "";
                ddl_country1.SelectedIndex = 1;
                ddl_state1.SelectedValue = "22";
                ddl_district1.SelectedIndex = 0;
                txtpin1.Text = ""; ;


            }

        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlsalute, mstlookup.GetLookupsList(LookupName.Salutation));
            Commonfunction.PopulateDdl(ddlmarital, mstlookup.GetLookupsList(LookupName.Marital));
            Commonfunction.PopulateDdl(ddlcountry, mstlookup.GetLookupsList(LookupName.Country));
            ddlcountry.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddlstate, mstlookup.GetLookupsList(LookupName.State));
            ddlstate.SelectedValue = "22";
            Commonfunction.PopulateDdl(ddldistrict, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddlstate.SelectedValue)));
            Commonfunction.PopulateDdl(ddlnationality, mstlookup.GetLookupsList(LookupName.Nationality));
            ddlnationality.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddlreligion, mstlookup.GetLookupsList(LookupName.Religion));
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(ddldesignation, mstlookup.GetLookupsList(LookupName.Designation));
            Commonfunction.PopulateDdl(ddlemployeetype, mstlookup.GetLookupsList(LookupName.EmployeeType));
            Commonfunction.PopulateDdl(ddl_emp_grade, mstlookup.GetLookupsList(LookupName.EmpGrade));
            Commonfunction.PopulateDdl(ddl_staffcategory, mstlookup.GetLookupsList(LookupName.StaffCategory));
            Commonfunction.PopulateDdl(ddl_caste, mstlookup.GetLookupsList(LookupName.Caste));
            Commonfunction.PopulateDdl(ddl_bloodgrp, mstlookup.GetLookupsList(LookupName.BloodGroup));
            Commonfunction.PopulateDdl(ddl_country1, mstlookup.GetLookupsList(LookupName.Country));
            ddl_country1.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_state1, mstlookup.GetLookupsList(LookupName.State));
            ddl_state1.SelectedValue = "22";
            Commonfunction.PopulateDdl(ddl_district1, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddl_state1.SelectedValue)));
            btnsave.Attributes.Remove("disabled");
            Commonfunction.PopulateDdl(ddl_departments, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(ddl_employeetypes, mstlookup.GetLookupsList(LookupName.EmployeeType));
            Commonfunction.PopulateDdl(ddl_staffcategorys, mstlookup.GetLookupsList(LookupName.StaffCategory));
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddlsalute.SelectedIndex = 0;
            txtname.Text = "";
            txtdob.Text = "";
            txt_alias.Text = "";
            ViewState["ID"] = null;
            txt_aadhaarno.Text = "";
            ddldistrict.SelectedIndex = 0;
            txtaddress.Text = "";
            ddlmarital.SelectedIndex = 0;
            ddlreligion.SelectedIndex = 0;
            txtpin.Text = "";
            txtemail.Text = "";
            txtcontactno.Text = "";
            txtidmark.Text = "";
            lblmessage.Visible = false;
            txtempnos.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            txtqulaification.Text = "";
            ddldesignation.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
            ddlemployeetype.SelectedIndex = 0;
            txtpin.Text = "";
            txtcontactno.Text = "";
            txtemergencyno.Text = "";
            ddlreligion.SelectedIndex = 0;
            ddlnationality.SelectedIndex = 0;
            ddlmarital.SelectedIndex = 0;
            ddl_emp_grade.SelectedIndex = 0;
            txtaddress.Text = "";
            txtempno.Text = "";
            txtdoj.Text = "";
            divmsg1.Visible = false;
            txt_address1.Text = "";
            ddl_bloodgrp.SelectedIndex = 0;
            ddl_caste.SelectedIndex = 0;
            ddl_country1.SelectedIndex = 0;
            ddl_district1.SelectedIndex = 0;
            ddl_staffcategory.SelectedIndex = 0;
            ddl_state1.SelectedIndex = 0;
            txt_guardianname.Text = "";
            txt_spousename.Text = "";
            txt_wrk.Text = "";
            txtpin1.Text = "";
            ddl_status.SelectedIndex = 0;
            chekbox.Checked = false;
            MasterLookupBO mstlookup = new MasterLookupBO();
            ddlstate.SelectedValue = "22";
            Commonfunction.PopulateDdl(ddldistrict, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddlstate.SelectedValue)));
            ddl_state1.SelectedValue = "22";
            Commonfunction.PopulateDdl(ddl_district1, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddl_state1.SelectedValue)));
            btnsave.Attributes.Remove("disabled");
            ddl_status.SelectedIndex = 0;
            txtremarks.Text = "";
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
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
                EmployeeData objpat = new EmployeeData();
                EmployeeBO objpatBO = new EmployeeBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

                if (ddlsalute.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please Select Salutation.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlsalute.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Employee Name.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtname.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txt_guardianname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Care of.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_guardianname.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtempno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter employee no.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtempno.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddl_staffcategory.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Staff Category.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddl_staffcategory.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddl_bloodgrp.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Blood Group.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddl_bloodgrp.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txt_wrk.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter work experience.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_wrk.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtdob.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Date of Birth.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtdob.Focus();
                    return;
                }
                else
                {
                    if (Commonfunction.isValidDate(txtdob.Text) == false || Commonfunction.CheckOverDate(txtdob.Text) == true)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter valid date of birth.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdob.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                if (txtdoj.Text == "")
                {
                    if (Commonfunction.isValidDate(txtdoj.Text) == false || Commonfunction.CheckOverDate(txtdoj.Text) == true)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter valid date of joining.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdoj.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                if (ddldesignation.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Designation.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddldesignation.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }



                if (ddldepartment.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Department.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddldepartment.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }

                if (ddlemployeetype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Employee Type.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlemployeetype.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtqulaification.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Qualification.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtqulaification.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddlmarital.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Marital Status.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlmarital.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddlmarital.SelectedIndex == 1)
                {
                    if (txt_spousename.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter spouse name.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_spousename.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtaddress.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Address.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtaddress.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddlcountry.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Country.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlcountry.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddl_status.SelectedIndex == 0)
                {
                    txtremarks.ReadOnly = true;
                }
                else if (txtremarks.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter remarks.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtremarks.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddlstate.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select State.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlstate.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddldistrict.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select District.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    lblmessage.CssClass = "";
                    ddldistrict.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtpin.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Pin.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtpin.Focus();
                    return;
                }
                else
                {
                    if (txtpin.Text != "")
                    {
                        if (Commonfunction.Checkvalidpin(txtpin.Text) == false)
                        {
                            Messagealert_.ShowMessage(lblmessage, "Pin", 0);
                            divmsg1.Attributes["class"] = "FailAlert";
                            divmsg1.Visible = true;
                            txtpin.Focus();
                            return;
                        }
                    }
                    else
                    {
                        divmsg1.Visible = false;
                        lblmessage.Visible = false;
                    }
                }
                if (chekbox.Checked == false)
                {
                    if (txt_address1.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter permanent address.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_address1.Focus();
                        return;
                    }
                    else
                    {
                        divmsg1.Visible = false;
                        lblmessage.Visible = false;
                    }
                    if (ddl_country1.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please select permanent Country.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        ddl_country1.Focus();
                        return;
                    }
                    else
                    {
                        divmsg1.Visible = false;
                        lblmessage.Visible = false;
                    }
                    if (ddl_state1.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please select permanent State.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        ddl_state1.Focus();
                        return;
                    }
                    else
                    {
                        divmsg1.Visible = false;
                        lblmessage.Visible = false;
                    }
                    if (ddl_district1.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please select permanent District.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        lblmessage.CssClass = "";
                        ddl_district1.Focus();
                        return;
                    }
                    else
                    {
                        divmsg1.Visible = false;
                        lblmessage.Visible = false;
                    }
                    if (txtpin1.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter permanent Pin.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtpin1.Focus();
                        return;
                    }
                    else
                    {
                        if (txtpin.Text != "")
                        {
                            if (Commonfunction.Checkvalidpin(txtpin.Text) == false)
                            {
                                Messagealert_.ShowMessage(lblmessage, "Pin", 0);
                                divmsg1.Attributes["class"] = "FailAlert";
                                divmsg1.Visible = true;
                                txtpin.Focus();
                                return;
                            }
                        }
                        else
                        {
                            divmsg1.Visible = false;
                            lblmessage.Visible = false;
                        }
                    }
                }
                if (txtcontactno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Contact No.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtcontactno.Focus();
                    return;
                }
                else
                {
                    if (txtcontactno.Text != "")
                    {
                        if (Commonfunction.Checkvalidmobile(txtcontactno.Text) == false)
                        {
                            Messagealert_.ShowMessage(lblmessage, "mobile", 0);
                            divmsg1.Attributes["class"] = "FailAlert";
                            divmsg1.Visible = true;
                            txtcontactno.Focus();
                            return;
                        }
                    }
                    else
                    {
                        divmsg1.Visible = false;
                        lblmessage.Visible = false;
                    }
                }
                if (txtemergencyno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Emergency No.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtemergencyno.Focus();
                    return;
                }
                else
                {
                    if (Commonfunction.Checkvalidmobile(txtemergencyno.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "emergencymobile", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtcontactno.Focus();
                        return;

                    }
                    else
                    {
                        divmsg1.Visible = false;
                        lblmessage.Visible = false;
                    }
                }
                if (txtemail.Text != "")
                {
                    if (Commonfunction.Checkemail(txtemail.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Email", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtemail.Focus();
                        return;
                    }
                    else
                    {
                        divmsg1.Visible = false;
                        lblmessage.Visible = false;

                    }
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddl_caste.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Caste.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddldesignation.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (ddl_emp_grade.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "EmpGrade", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    lblmessage.CssClass = "";
                    ddl_emp_grade.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                string fileName = FileUploadImage.FileName.ToString();
                if (fileName != "")
                {
                    if (Directory.Exists(Request.PhysicalApplicationPath + @"EmployeePhoto/") == false)
                        Directory.CreateDirectory(Request.PhysicalApplicationPath + @"EmployeePhoto/");

                    if (File.Exists(Request.PhysicalApplicationPath + @"EmployeePhoto/" + fileName))
                    {
                        File.Delete(Request.PhysicalApplicationPath + @"EmployeePhoto/" + fileName);
                        // return "exist";
                    }
                    FileUploadImage.SaveAs(Request.PhysicalApplicationPath + @"EmployeePhoto/" + fileName);
                    string path = @"EmployeePhoto/" + fileName;

                    objpat.EmployeePhotoLocation = path;
                    //imageuploader as bit image
                    int length = FileUploadImage.PostedFile.ContentLength;
                    //create a byte array to store the binary image data
                    byte[] imgbyte = new byte[length];
                    //store the currently selected file in memeory
                    HttpPostedFile img = FileUploadImage.PostedFile;
                    //set the binary data
                    img.InputStream.Read(imgbyte, 0, length);
                    objpat.ImageFile = imgbyte;
                }
                else
                {
                    objpat.EmployeePhotoLocation = FileUploadImage.FileName.ToString();
                }
                string fileName1 = FileUpload1.FileName.ToString();
                if (fileName1 != "")
                {
                    if (Directory.Exists(Request.PhysicalApplicationPath + @"EmployeeDigitalSignature/") == false)
                        Directory.CreateDirectory(Request.PhysicalApplicationPath + @"EmployeeDigitalSignature/");

                    if (File.Exists(Request.PhysicalApplicationPath + @"EmployeeDigitalSignature/" + fileName1))
                    {
                        File.Delete(Request.PhysicalApplicationPath + @"EmployeeDigitalSignature/" + fileName1);
                        // return "exist";
                    }
                    FileUploadImage.SaveAs(Request.PhysicalApplicationPath + @"EmployeeDigitalSignature/" + fileName1);
                    string path1 = @"EmployeeDigitalSignature/" + fileName1;

                    objpat.DigitalSignatureLocation = path1;
                    //imageuploader as bit image
                    int length1 = FileUpload1.PostedFile.ContentLength;
                    //create a byte array to store the binary image data
                    byte[] imgbyte1 = new byte[length1];
                    //store the currently selected file in memeory
                    HttpPostedFile img1 = FileUpload1.PostedFile;
                    //set the binary data
                    img1.InputStream.Read(imgbyte1, 0, length1);
                    objpat.SignatureFile = imgbyte1;
                }
                else
                {
                    objpat.DigitalSignatureLocation = FileUpload1.FileName.ToString();
                }
                objpat.SalutationID = Convert.ToInt32(ddlsalute.SelectedValue == "" ? "0" : ddlsalute.SelectedValue);
                objpat.EmpName = txtname.Text.Trim() == "" ? "" : txtname.Text.Trim();
                objpat.AliasName = txt_alias.Text.Trim() == "" ? "" : txt_alias.Text.Trim();
                objpat.AadhaarNo = txt_aadhaarno.Text.Trim() == "" ? "" : txt_aadhaarno.Text.Trim();
                objpat.SpouseName = txt_spousename.Text.Trim() == "" ? "" : txt_spousename.Text.Trim();
                objpat.GuardianName = txt_guardianname.Text.Trim() == "" ? "" : txt_guardianname.Text.Trim();
                objpat.StaffCategoryID = Convert.ToInt32(ddl_staffcategory.SelectedValue == "" ? "0" : ddl_staffcategory.SelectedValue);
                objpat.BloodGroupID = Convert.ToInt32(ddl_bloodgrp.SelectedValue == "" ? "0" : ddl_bloodgrp.SelectedValue);
                objpat.WorkExp = txt_wrk.Text == "" ? "" : txt_wrk.Text.Trim();
                objpat.DesignationID = Convert.ToInt32(ddldesignation.SelectedValue == "" ? "0" : ddldesignation.SelectedValue);
                objpat.NationalityID = Convert.ToInt32(ddlnationality.SelectedValue == "" ? "0" : ddlnationality.SelectedValue);
                objpat.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                objpat.EmployeeTypeID = Convert.ToInt32(ddlemployeetype.SelectedValue == "" ? "0" : ddlemployeetype.SelectedValue);
                DateTime DateofBirth = txtdob.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdob.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objpat.DOB = DateofBirth;
                DateTime DateofJoining = txtdoj.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdoj.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objpat.DOJ = DateofJoining;
                objpat.MaritalStatusID = Convert.ToInt32(ddlmarital.SelectedValue == "" ? "0" : ddlmarital.SelectedValue);
                objpat.CurrentAddress = txtaddress.Text == "" ? "" : txtaddress.Text.Trim();
                objpat.CurrentStateID = Convert.ToInt32(ddlstate.SelectedValue == "" ? "0" : ddlstate.SelectedValue);
                objpat.CurrentDistrictID = Convert.ToInt32(ddldistrict.SelectedValue == "" ? "0" : ddldistrict.SelectedValue);
                objpat.CurrentCountryID = Convert.ToInt32(ddlcountry.SelectedValue == "" ? "0" : ddlcountry.SelectedValue);
                String text = txtpin.Text == "" ? "0" : txtpin.Text;
                objpat.PermAddress = txt_address1.Text.Trim() == "" ? "" : txt_address1.Text.Trim();
                objpat.PermStateID = Convert.ToInt32(ddl_state1.SelectedValue == "" ? "0" : ddl_state1.SelectedValue);
                objpat.PermDistrictID = Convert.ToInt32(ddldistrict.SelectedValue == "" ? "0" : ddldistrict.SelectedValue);
                objpat.PermCountryID = Convert.ToInt32(ddl_district1.SelectedValue == "" ? "0" : ddl_district1.SelectedValue);
                String text1 = txtpin1.Text == "" ? "0" : txtpin1.Text;
                objpat.IDmarks = txtidmark.Text.Trim() == "" ? "" : txtidmark.Text.Trim();
                objpat.ReligionID = Convert.ToInt32(ddlreligion.SelectedValue == "" ? "0" : ddlreligion.SelectedValue);
                objpat.EmailID = txtemail.Text.Trim() == "" ? "" : txtemail.Text.Trim();
                objpat.Remarks = txtremarks.Text.Trim() == "" ? "" : txtremarks.Text.Trim();
                objpat.IsActive = ddl_status.SelectedValue == "0" ? true : false;

                objpat.CastID = Convert.ToInt32(ddl_caste.SelectedValue == "" ? "0" : ddl_caste.SelectedValue);
                objpat.MobileNo = txtcontactno.Text == "" ? "" : txtcontactno.Text.Trim();
                objpat.CurrentPIN = Convert.ToInt32(txtpin.Text == "" ? "0" : txtpin.Text);
                objpat.EmployeeNo = txtempno.Text == "" ? "" : txtempno.Text.Trim();
                objpat.Qualification = txtqulaification.Text == "" ? "" : txtqulaification.Text.Trim();
                objpat.PhoneNo = txtemergencyno.Text == "" ? "" : txtemergencyno.Text.Trim();
                objpat.EmpGradeID = Convert.ToInt32(ddl_emp_grade.SelectedValue == "0" ? "0" : ddl_emp_grade.SelectedValue);
                objpat.FPData = FPtemplate.Value == "" ? "0" : FPtemplate.Value;
                objpat.EmployeeID = LogData.EmployeeID;
                objpat.HospitalID = LogData.HospitalID;
                objpat.FinancialYearID = LogData.FinancialYearID;
                objpat.ExcludeMsb = Convert.ToInt32(ddl_exclude_msb.SelectedValue == "0" ? "0" : ddl_exclude_msb.SelectedValue);
                objpat.ActionType = Enumaction.Insert;
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
                        objpat.ActionType = Enumaction.Update;
                        objpat.EmployeeID = Convert.ToInt32(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                    }
                }
                int results = objpatBO.UpdateEmployeeDetails(objpat);
                if (results == 1 || results == 2)
                {
                    ViewState["ID"] = null;
                    btnsave.Attributes["disabled"] = "disabled";
                    Messagealert_.ShowMessage(lblmessage, results == 1 ? "save" : "update", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                    return;
                }
                else if (results == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "Already Exist!", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmployeeNo = prefixText;
            getResult = objInfoBO.GetEmployeeNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmployeeNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetEmployeeName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetContactno(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.MobileNo = prefixText;
            getResult = objInfoBO.GetContactno(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].MobileNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmpNo(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmployeeNo = prefixText;
            getResult = objInfoBO.GetEmployeeNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmployeeNo.ToString());
            }
            return list;
        }

        protected void btnsearch_Click(object sender, EventArgs e)
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

            bindgrid(1);
        }
        protected void bindgrid(int page)
        {
            try
            {
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<EmployeeData> patientdetails = GetEmployeeData(page);
                if (patientdetails.Count > 0)
                {
                    GvemployeeList.VirtualItemCount = patientdetails[0].MaximumRows;//total item is required for custom paging
                    GvemployeeList.PageIndex = page - 1;

                    GvemployeeList.DataSource = patientdetails;
                    GvemployeeList.DataBind();
                    GvemployeeList.Visible = true;
                    divmsg3.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + patientdetails[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    divmsg2.Visible = false;
                }
                else
                {
                    GvemployeeList.DataSource = null;
                    GvemployeeList.DataBind();
                    GvemployeeList.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<EmployeeData> GetEmployeeData(int curIndex)
        {
            EmployeeData objpat = new EmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.EmployeeNo = txtempnos.Text == "" ? null : txtempnos.Text.Trim();
            //objpat.EmpName = txtemployeename.Text == "" ? null : txtemployeename.Text.Trim();
            objpat.EmployeeID = Convert.ToInt64(txtemployeename.Text.Substring(txtemployeename.Text.LastIndexOf(':') + 1) == "" ? "0" : txtemployeename.Text.Substring(txtemployeename.Text.LastIndexOf(':') + 1));
            objpat.MobileNo = txtcontactnos.Text == "" ? null : txtcontactnos.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.StaffCategoryID = Convert.ToInt32(ddl_staffcategorys.SelectedValue == "" ? "0" : ddl_staffcategorys.SelectedValue);
            objpat.EmployeeTypeID = Convert.ToInt32(ddl_employeetypes.SelectedValue == "" ? "0" : ddl_employeetypes.SelectedValue);
            objpat.DepartmentID = Convert.ToInt32(ddl_departments.SelectedValue == "" ? "0" : ddl_departments.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.CurrentIndex = curIndex;
            return objstdBO.SearchEmployeetDetails(objpat);
        }
        public List<EmployeeData> GetEmployeeDetails(int curIndex)
        {

            EmployeeData objpat = new EmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.EmployeeNo = txtempnos.Text == "" ? null : txtempnos.Text.Trim();
            //objpat.EmpName = txtemployeename.Text == "" ? null : txtemployeename.Text.Trim();
            objpat.EmployeeID = Convert.ToInt64(txtemployeename.Text.Substring(txtemployeename.Text.LastIndexOf(':') + 1) == "" ? "0" : txtemployeename.Text.Substring(txtemployeename.Text.LastIndexOf(':') + 1));
            objpat.MobileNo = txtcontactnos.Text == "" ? null : txtcontactnos.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.StaffCategoryID = Convert.ToInt32(ddl_staffcategorys.SelectedValue == "" ? "0" : ddl_staffcategorys.SelectedValue);
            objpat.EmployeeTypeID = Convert.ToInt32(ddl_employeetypes.SelectedValue == "" ? "0" : ddl_employeetypes.SelectedValue);
            objpat.DepartmentID = Convert.ToInt32(ddl_departments.SelectedValue == "" ? "0" : ddl_departments.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objstdBO.SearchEmployeeDetails(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtempnos.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            GvemployeeList.DataSource = null;
            GvemployeeList.DataBind();
            GvemployeeList.Visible = false;
            lblmessage.Visible = false;
            txtemployeename.Text = "";
            txtcontactnos.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            divmsg3.Visible = false;
            ViewState["ID"] = null;
            ddl_departments.SelectedIndex = 0;
            ddl_employeetypes.SelectedIndex = 0;
            ddl_staffcategorys.SelectedIndex = 0;
        }
        protected void txtautoUHID_TextChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void txtcontactnos_TextChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void GvemployeeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "EditEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvemployeeList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Int64 PatID = Convert.ToInt64(ID.Text);
                    EditPatient(PatID);
                    tabcontaemployee.ActiveTabIndex = 0;
                }
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
                        lblmessage.Visible = false;
                    }
                    EmployeeData objpatnt = new EmployeeData();
                    EmployeeBO objstdBO = new EmployeeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvemployeeList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Remarks", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        divmsg2.Visible = true;
                        return;
                    }
                    else
                    {
                        objpatnt.Remarks = txtremarks.Text;
                        divmsg2.Visible = false;
                    }
                    objpatnt.EmployeeID = Convert.ToInt64(ID.Text);
                    objpatnt.UserLoginId = LogData.EmployeeID;
                    int Result = objstdBO.DeleteEmployeeDetailsByID(objpatnt);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;

                        bindgrid(1);
                        return;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        return;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
                return;
            }
        }
        protected void EditPatient(Int64 patID)
        {
            try
            {
                List<EmployeeData> patientdetails = GetEditEmployeeDetails(patID);
                if (patientdetails.Count > 0)
                {
                    ddlsalute.SelectedValue = patientdetails[0].SalutationID.ToString();
                    txtname.Text = patientdetails[0].EmpName.ToString();
                    txt_alias.Text = patientdetails[0].AliasName.ToString();
                    txt_aadhaarno.Text = patientdetails[0].AadhaarNo.ToString();
                    txt_spousename.Text = patientdetails[0].SpouseName.ToString();
                    txt_guardianname.Text = patientdetails[0].GuardianName.ToString();
                    txtqulaification.Text = patientdetails[0].Qualification.ToString();
                    txtidmark.Text = patientdetails[0].IDmarks.ToString();
                    txtdob.Text = patientdetails[0].DOB.ToString("dd/MM/yyyy");
                    txtdoj.Text = patientdetails[0].DOJ.ToString("dd/MM/yyyy");
                    txtaddress.Text = patientdetails[0].CurrentAddress.ToString();
                    txt_wrk.Text = patientdetails[0].Experience.ToString();
                    txtcontactno.Text = patientdetails[0].MobileNo.ToString();
                    txtemergencyno.Text = patientdetails[0].PhoneNo.ToString();
                    txtemail.Text = patientdetails[0].EmailID.ToString();
                    txtpin.Text = patientdetails[0].CurrentPIN.ToString();
                    txtidmark.Text = patientdetails[0].IDmarks.ToString();
                    txtempno.Text = patientdetails[0].EmployeeNo.ToString();
                    ddlreligion.SelectedValue = patientdetails[0].ReligionID.ToString();
                    ddlmarital.SelectedValue = patientdetails[0].MaritalStatusID.ToString();
                    if (ddlmarital.SelectedIndex == 1)
                    {
                        txt_spousename.ReadOnly = false;
                    }
                    else
                    {
                        txt_spousename.ReadOnly = true;
                    }
                    ddldepartment.SelectedValue = patientdetails[0].DepartmentID.ToString();
                    ddldesignation.SelectedValue = patientdetails[0].DesignationID.ToString();
                    ddlemployeetype.SelectedValue = patientdetails[0].EmployeeTypeID.ToString();
                    ddlnationality.SelectedValue = patientdetails[0].NationalityID.ToString();
                    ddlcountry.SelectedValue = patientdetails[0].CurrentCountryID.ToString();
                    ddlstate.SelectedValue = patientdetails[0].CurrentStateID.ToString();
                    MasterLookupBO mstlookup = new MasterLookupBO();
                    Commonfunction.PopulateDdl(ddldistrict, mstlookup.GetDistrictByStateD(Convert.ToInt32(patientdetails[0].CurrentStateID)));
                    ddldistrict.SelectedValue = patientdetails[0].CurrentDistrictID.ToString();
                    txt_address1.Text = patientdetails[0].PermAddress.ToString();
                    ddl_country1.SelectedValue = patientdetails[0].PermCountryID.ToString();
                    ddl_state1.SelectedValue = patientdetails[0].PermStateID.ToString();
                    Commonfunction.PopulateDdl(ddl_district1, mstlookup.GetDistrictByStateD(Convert.ToInt32(patientdetails[0].PermStateID)));
                    ddl_district1.SelectedValue = patientdetails[0].PermDistrictID.ToString();
                    txtpin1.Text = patientdetails[0].PermPIN.ToString();
                    ddl_staffcategory.SelectedValue = patientdetails[0].StaffCategoryID.ToString();
                    ddl_bloodgrp.SelectedValue = patientdetails[0].BloodGroupID.ToString();
                    ddl_caste.SelectedValue = patientdetails[0].CastID.ToString();
                    Commonfunction.PopulateDdl(ddl_emp_grade, mstlookup.GetLookupsList(LookupName.EmpGrade));
                    ddl_emp_grade.SelectedValue = patientdetails[0].EmpGradeID.ToString();
                    ViewState["ID"] = patientdetails[0].EmployeeID.ToString();
                    lblmessage.Visible = false;
                    ddl_status.SelectedValue = (patientdetails[0].StatusID == 0 ? 1 : 0).ToString();
                    ddl_exclude_msb.SelectedValue = patientdetails[0].ExcludeMsb.ToString();
                    FPtemplate.Value = patientdetails[0].FPData.ToString();
                    if (ddlmarital.SelectedIndex == 1)
                    {
                        txtremarks.ReadOnly = false;
                    }
                    else
                    {
                        txtremarks.ReadOnly = true;
                    }
                    txtremarks.Text = patientdetails[0].Remarks.ToString();
                    btnsave.Attributes.Remove("disabled");

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
                return;
            }
        }
        public List<EmployeeData> GetEditEmployeeDetails(Int64 ID)
        {
            EmployeeData objpat = new EmployeeData();
            EmployeeBO objpatBO = new EmployeeBO();
            objpat.EmployeeID = ID;
            return objpatBO.GetEmployeeDetailbyID(objpat);
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Employee Details");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=EmployeeDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<EmployeeData> EmployeeDetails = GetEmployeeDetails(0);
            List<EmployeeDatatoExcel> ListexcelData = new List<EmployeeDatatoExcel>();
            int i = 0;
            foreach (EmployeeData row in EmployeeDetails)
            {
                EmployeeDatatoExcel Ecxeclemp = new EmployeeDatatoExcel();
                Ecxeclemp.EmployeeNo = EmployeeDetails[i].EmployeeNo;
                Ecxeclemp.EmpName = EmployeeDetails[i].EmpName;
                Ecxeclemp.AliasName = EmployeeDetails[i].AliasName;
                Ecxeclemp.AadhaarNo = EmployeeDetails[i].AadhaarNo;
                Ecxeclemp.SpouseName = EmployeeDetails[i].SpouseName;
                Ecxeclemp.GuardianName = EmployeeDetails[i].GuardianName;
                Ecxeclemp.Qualification = EmployeeDetails[i].Qualification;
                Ecxeclemp.StaffCategory = EmployeeDetails[i].StaffCategory;
                Ecxeclemp.Department = EmployeeDetails[i].Department.ToString();
                Ecxeclemp.Designation = EmployeeDetails[i].Designation.ToString();
                Ecxeclemp.WorkExp = EmployeeDetails[i].WorkExp;
                Ecxeclemp.Gender = EmployeeDetails[i].Gender;
                Ecxeclemp.DateofBirth = EmployeeDetails[i].DateofBirth.ToString();
                Ecxeclemp.DateOfJoining = EmployeeDetails[i].DateOfJoining.ToString();
                Ecxeclemp.CastName = EmployeeDetails[i].CastName;
                Ecxeclemp.CurrentAddress = EmployeeDetails[i].CurrentAddress.ToString();
                Ecxeclemp.CurrDistrict = EmployeeDetails[i].CurrDistrict.ToString();
                Ecxeclemp.CurrState = EmployeeDetails[i].CurrState.ToString();
                Ecxeclemp.CurrCountry = EmployeeDetails[i].CurrCountry.ToString();
                Ecxeclemp.CurrentPIN = EmployeeDetails[i].CurrentPIN;
                Ecxeclemp.PermAddress = EmployeeDetails[i].PermAddress.ToString();
                Ecxeclemp.PerDistrict = EmployeeDetails[i].PerDistrict.ToString();
                Ecxeclemp.PerState = EmployeeDetails[i].PerState.ToString();
                Ecxeclemp.PerCountry = EmployeeDetails[i].PerCountry.ToString();
                Ecxeclemp.PermPIN = EmployeeDetails[i].PermPIN;
                Ecxeclemp.EmailID = EmployeeDetails[i].EmailID;
                Ecxeclemp.EmployeeType = EmployeeDetails[i].EmployeeType;
                Ecxeclemp.MaritalStatus = EmployeeDetails[i].MaritalStatus.ToString();
                Ecxeclemp.MobileNo = EmployeeDetails[i].MobileNo.ToString();
                Ecxeclemp.PhoneNo = EmployeeDetails[i].PhoneNo.ToString();
                Ecxeclemp.EmpGrade = EmployeeDetails[i].EmpGrade.ToString();
                ListexcelData.Add(Ecxeclemp);
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
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
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
            else
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportType", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        //public void ExportToPdf()
        //{
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //        {

        //            GvemployeeList.BorderStyle = BorderStyle.None;
        //            //Hide the Column containing CheckBox
        //            GvemployeeList.Columns[9].Visible = false;
        //            GvemployeeList.RenderControl(hw);
        //            GvemployeeList.HeaderRow.Style.Add("width", "15%");
        //            GvemployeeList.HeaderRow.Style.Add("font-size", "10px");
        //            GvemployeeList.Style.Add("text-decoration", "none");
        //            GvemployeeList.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
        //            GvemployeeList.Style.Add("font-size", "8px");
        //            StringReader sr = new StringReader(sw.ToString());
        //            Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //            pdfDoc.Open();
        //            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //            pdfDoc.Close();
        //            Response.ContentType = "application/pdf";
        //            Response.AddHeader("content-disposition", "attachment;filename=EmployeeDetails.pdf");
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.Write(pdfDoc);
        //            Response.End();
        //        }
        //    }
        //}
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlstate.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldistrict, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddlstate.SelectedValue)));
            }
        }
        protected void GvemployeeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void ddl_district1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_state1.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_district1, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddl_state1.SelectedValue)));
            }
        }
        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
        protected void btnFPScan_Click(object sender, EventArgs e)
        {
            string IP = Commonfunction.GetClientIPAddress();
            string URL = "http://" + IP + ":8080/CallMorphoAPI";
            string response = GET(URL);
            fingerData data = JsonConvert.DeserializeObject<fingerData>(response);
            string fptemp = data.Base64ISOTemplate;
            Bitmap fbimage = Base64StringToBitmap(data.Base64BMPIMage);
            FPImage.Visible = true;
            FPImage.ImageUrl = "data:image/bmp;base64," + data.Base64BMPIMage;
            FPtemplate.Value = data.Base64ISOTemplate;
        }
        public Bitmap Base64StringToBitmap(string base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }
        public class fingerData
        {
            public string ReturnCode { get; set; }
            public string Base64ISOTemplate { get; set; }
            public string Base64RAWIMage { get; set; }
            public string Base64BMPIMage { get; set; }
            public string NFIQ { get; set; }

        }
    }
}