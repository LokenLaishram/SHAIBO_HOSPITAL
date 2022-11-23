using Mediqura.BOL.AdmissionBO;
using Mediqura.CommonData.AdmissionData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.Display
{
    public partial class CurrentPatientList : System.Web.UI.Page
    {
        static int slno;
        static int activePatient;
        static int passivePatient;
        protected void Page_Load(object sender, EventArgs e)
        {
            slno = 0;
            List<CurrentAdmissionListData> result = GetPatientList(0);
            if (result.Count > 0) {
                gvadmissionlist.DataSource = result;
                gvadmissionlist.DataBind();
                gvadmissionlist.Visible = true;
            }
        }
        public List<CurrentAdmissionListData> GetPatientList(int curIndex)
        {
            CurrentAdmissionListData objData = new CurrentAdmissionListData();
            CurrentAdmissionListBO objBO = new CurrentAdmissionListBO();
            objData.wardId = 0;
            return objBO.GetAdmissionDetailList(objData);
        }

        protected void gvadmissionlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSlNo = (Label)e.Row.FindControl("lblSlNo");
                Label lblbed = (Label)e.Row.FindControl("lblbed");
                Label lblWard = (Label)e.Row.FindControl("lblWard");
                Label lblWardTotal = (Label)e.Row.FindControl("lblWardTotal");
                Label lblSubHeading = (Label)e.Row.FindControl("lblSubHeading");
                Label lblIsRelease = (Label)e.Row.FindControl("lblIsRelease");
                Label lblPatientActive = (Label)e.Row.FindControl("lblPatientActive");
                if (lblPatientActive.Text == "0")
                {

                    if (lblSubHeading.Text == "1")
                    {
                    }
                    else
                    {
                        e.Row.Cells[5].BackColor = Color.FromName("#F3F00F");
                    }
                    passivePatient = passivePatient + 1;
                    slno = slno + 1;
                    lblSlNo.Text = slno.ToString();
                }
                else
                {
                    activePatient = activePatient + 1;
                    slno = slno + 1;
                    lblSlNo.Text = slno.ToString();
                }
                if (lblSubHeading.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#33aa99");
                    lblWard.ForeColor = Color.FromName("#FFFFFF");
                    GridView editGrid = sender as GridView;
                    e.Row.Cells[1].ColumnSpan = 2;
                    e.Row.Cells[2].ColumnSpan = 2;
                    e.Row.Cells[3].Controls.Clear();
                    e.Row.Cells[5].Controls.Clear();
                    lblWard.Visible = true;
                    slno = slno - 1;
                    passivePatient = passivePatient - 1;
                    lblSlNo.Text = "";
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                }
                else
                {
                    lblWard.Visible = false;

                }

            }
        }
    }
}