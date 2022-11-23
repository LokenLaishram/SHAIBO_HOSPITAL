using Mediqura.BOL.CommonBO;
using Mediqura.BOL.UserBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.Admin
{
    public partial class ApplicationUtility : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindgrid(0);
            }
        }
        private void bindgrid(int p)
        {
            List<ApplicationData> obj = GetApplicationDetails(0);
         
            if (obj.Count > 0)
            {
                GvApplicationUtility.DataSource = obj;
                GvApplicationUtility.DataBind();
                GvApplicationUtility.Visible = true;
            }
            else
            {
                GvApplicationUtility.DataSource = null;
                GvApplicationUtility.DataBind();
                GvApplicationUtility.Visible = true;
            }
        }
        private List<ApplicationData> GetApplicationDetails(int p)
        {
            ApplicationData objpat = new ApplicationData();
            ApplicationBO objBO = new ApplicationBO();
            return objBO.GetApplicationDetails(objpat);
        }
        protected void GvApplicationUtility_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label status = (Label)e.Row.FindControl("lblIsActive");
                CheckBox chkbox = (CheckBox)e.Row.FindControl("chekboxselect");

                if (status.Text == "1")
                {
                    chkbox.Checked = true;
                }
                else
                {
                    chkbox.Checked = false;
                }
            }
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            GvApplicationUtility.DataSource = null;
            GvApplicationUtility.DataBind();
            GvApplicationUtility.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
          }
        protected void btnupdateClick(object sender, EventArgs e)
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
            List<ApplicationData> List = new List<ApplicationData>();
            ApplicationBO objBO = new ApplicationBO();
            ApplicationData objrec = new ApplicationData();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in GvApplicationUtility.Rows)
                {
                    Label AppID = (Label)GvApplicationUtility.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    CheckBox Chkbox = (CheckBox)GvApplicationUtility.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect");
                    ApplicationData obj = new ApplicationData();
                    obj.AppID = Convert.ToInt32(AppID.Text);

                    if (Chkbox.Checked == true)
                    {
                        obj.Status = 1;
                    }
                    else
                    {
                        obj.Status = 0;
                    }
                     List.Add(obj);
                }
                objrec.XMLData = XmlConvertor.AppUtilityRecordDatatoXML(List).ToString();
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;



                int result = objBO.UpdateApplicationUtility(objrec);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    bindgrid(0);
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
   
    }
}