using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web
{
    public partial class dashboardManager : System.Web.UI.Page
    {
        static Int64 userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["type"] != null) {
                // userID =LogData.EmployeeID;
                userID = Convert.ToInt64(Request["type"]);
                searchBed();
            }
        }
        public void searchBed()
        {
            int vacant = 0;
            int occupied = 0;
            int disready = 0;
            int undermaintanence = 0;
            StringBuilder UIstring = new StringBuilder();
            List<BedDasboard> lstemp = GetBedType(0);
            for (int i = 0; i < lstemp.Count; i++)
            {

                if (lstemp[i].BedStatus == 1)
                {
                    string temp = "   <div class=\"col-sm-3\">" +
                                                       " <div class=\"info-box\">" +
                                                       "     <span class=\"info-box-icon bg-green\"><i class=\"fa fa-bed\"></i></span>" +
                                                           " <div class=\"info-box-content\">" +
                                                               " <span class=\"info-box-text text-center\"> BED " + lstemp[i].BedNo.ToString() + "</span>" +
                                                                "<span><i class=\"fa fa-institution\"></i>&nbsp;&nbsp;&nbsp;" + lstemp[i].Ward.ToString() + "<br />" +
                                                               " </span>" +
                                                               " <span><i class=\"fa fa-wheelchair\"></i>&nbsp;&nbsp;" + lstemp[i].IPNo.ToString() + "<br />" +
                                                              "  </span>" +
                                                              "  <span><i class=\"fa fa-male\"></i>&nbsp; " + lstemp[i].PatientName.ToString() + "<br />" +
                                                                "</span>" +
                                                          "  </div>" +

                                                       " </div>" +

                                                    "</div>";
                    vacant = vacant + 1;
                    UIstring.Append(temp);
                }
                if (lstemp[i].BedStatus == 2)
                {
                    string temp = "   <div id='right-click' class=\"col-sm-3\">" +
                                                       " <div class=\"info-box\">" +
                                                       "     <span class=\"info-box-icon bg-red\"><i class=\"fa fa-bed\"></i></span>" +
                                                           " <div class=\"info-box-content\">" +
                                                                   " <span class=\"info-box-text text-center\"> BED " + lstemp[i].BedNo.ToString() + "</span>" +
                                                                "<span><i class=\"fa fa-institution\"></i>&nbsp;&nbsp;&nbsp;" + lstemp[i].Ward.ToString() + "<br />" +
                                                               " </span>" +
                                                               " <span><i class=\"fa fa-wheelchair\"></i>&nbsp;&nbsp;" + lstemp[i].IPNo.ToString() + "<br />" +
                                                              "  </span>" +
                                                              "  <span><i class=\"fa fa-male\"></i>&nbsp; " + lstemp[i].PatientName.ToString() + "<br />" +
                                                                "</span>" +
                                                          "  </div>" +

                                                       " </div>" +

                                                    "</div>";
                    occupied = occupied + 1;
                    UIstring.Append(temp);
                }
                if (lstemp[i].BedStatus == 3)
                {
                    string temp = "   <div class=\"col-sm-3\">" +
                                                       " <div class=\"info-box\">" +
                                                       "     <span class=\"info-box-icon bg-yellow\"><i class=\"fa fa-bed\"></i></span>" +
                                                           " <div class=\"info-box-content\">" +
                                                                 " <span class=\"info-box-text text-center\"> BED " + lstemp[i].BedNo.ToString() + "</span>" +
                                                                "<span><i class=\"fa fa-institution\"></i>&nbsp;&nbsp;&nbsp;" + lstemp[i].Ward.ToString() + "<br />" +
                                                               " </span>" +
                                                               " <span><i class=\"fa fa-wheelchair\"></i>&nbsp;&nbsp;" + lstemp[i].IPNo.ToString() + "<br />" +
                                                              "  </span>" +
                                                              "  <span><i class=\"fa fa-male\"></i>&nbsp; " + lstemp[i].PatientName.ToString() + "<br />" +
                                                                "</span>" +
                                                          "  </div>" +

                                                       " </div>" +

                                                    "</div>";
                    disready = disready + 1;
                    UIstring.Append(temp);
                }
                if (lstemp[i].BedStatus == 4)
                {
                    string temp = "   <div class=\"col-sm-3\">" +
                                                       " <div class=\"info-box\">" +
                                                       "     <span class=\"info-box-icon bg-blue\"><i class=\"fa fa-bed\"></i></span>" +
                                                           " <div class=\"info-box-content\">" +
                                                                  " <span class=\"info-box-text text-center\"> BED " + lstemp[i].BedNo.ToString() + "</span>" +
                                                                "<span><i class=\"fa fa-institution\"></i>&nbsp;&nbsp;&nbsp;" + lstemp[i].Ward.ToString() + "<br />" +
                                                               " </span>" +
                                                               " <span><i class=\"fa fa-wheelchair\"></i>&nbsp;&nbsp;" + lstemp[i].IPNo.ToString() + "<br />" +
                                                              "  </span>" +
                                                              "  <span><i class=\"fa fa-male\"></i>&nbsp; " + lstemp[i].PatientName.ToString() + "<br />" +
                                                                "</span>" +
                                                          "  </div>" +

                                                       " </div>" +

                                                    "</div>";
                    undermaintanence = undermaintanence + 1;
                    UIstring.Append(temp);
                }


            }
            BedDataliterals.Text = "" + UIstring;
        }
        private List<BedDasboard> GetBedType(int p)
        {
            BedMasterData objFloorMasterData = new BedMasterData();
            BedMasterBO objBlockMasterBO = new BedMasterBO();
            return objBlockMasterBO.NurseBedDashBoard(userID);
        }
    }
}