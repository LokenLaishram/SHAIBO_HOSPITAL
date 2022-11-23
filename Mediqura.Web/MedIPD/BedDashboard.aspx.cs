using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedIPD
{
    public partial class BedDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                checkSelect();
                searchBed();
            }
        }
        public void checkSelect()
        {
            if (ddl_block.SelectedIndex == 0)
            {
                ddl_floor.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddl_floor.Attributes.Remove("disabled");
            }
            if (ddl_floor.SelectedIndex == 0)
            {
                ddl_ward.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddl_ward.Attributes.Remove("disabled");
            }
            if (ddl_ward.SelectedIndex == 0)
            {
                txt_room.ReadOnly = true;
            }
            else
            {
                txt_room.ReadOnly = false;
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
          
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetLookupsList(LookupName.FloorType));
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
           
        }
        protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            if (ddl_block.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetfloorByblockID(Convert.ToInt32(ddl_block.SelectedValue)));
            }

        }

        protected void ddl_floor_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            if (ddl_floor.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetWardByFloorID(Convert.ToInt32(ddl_floor.SelectedValue)));
            }
        }
        protected void ddl_ward_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            if (ddl_ward.SelectedIndex > 0)
            {
              
             
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getautoroomnos(string prefixText, int count, string contextKey)
        {
            BedMasterData Objpaic = new BedMasterData();
            BedMasterBO objInfoBO = new BedMasterBO();
            List<BedMasterData> getResult = new List<BedMasterData>();
            Objpaic.Room = prefixText;
            getResult = objInfoBO.Getautorooms(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Room.ToString());
            }
            return list;
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_bed_status.SelectedIndex = 0;
            ddl_block.SelectedIndex = 0;
            ddl_floor.SelectedIndex = 0;
            ddl_ward.SelectedIndex = 0;
            txt_room.Text = "";
            checkSelect();
            BedDataliterals.Text = "" ;
            litDischarge.Text = "0" ;
            litOccupied.Text = "0" ;
            litVacant.Text = "0";
            litMantenence.Text = "0";
        }
        protected void btnrearch_Click(object sender, EventArgs e)
        {
            searchBed();
        }
        public void searchBed() {
            int vacant = 0;
            int occupied = 0;
            int disready = 0;
            int undermaintanence = 0;
            StringBuilder UIstring = new StringBuilder();
            List<BedMasterData> lstemp = GetBedType(0);
            for (int i = 0; i < lstemp.Count; i++)
            {

                if (lstemp[i].BedStatus == 1)
                {
                    string temp = "   <div class=\"col-sm-3\">" +
                                                       " <div class=\"info-box\">" +
                                                       "     <span class=\"info-box-icon bg-green\"><i class=\"fa fa-bed\"></i></span>" +
                                                           " <div class=\"info-box-content\">" +
                                                               " <span class=\"info-box-text text-center\">" + lstemp[i].Room.ToString() + "</span>" +
                                                                "<span><i class=\"fa fa-male\"></i>&nbsp;&nbsp;&nbsp;" + lstemp[i].Ward.ToString() + "<br />" +
                                                               " </span>" +
                                                               " <span><i class=\"fa fa-wheelchair\"></i>&nbsp;&nbsp;" + lstemp[i].Block.ToString() + "<br />" +
                                                              "  </span>" +
                                                              "  <span><i class=\"fa fa-institution\"></i>&nbsp; " + lstemp[i].Floor1.ToString() + "<br />" +
                                                                "</span>" +
                                                          "  </div>" +

                                                       " </div>" +

                                                    "</div>";
                    vacant = vacant + 1;
                    UIstring.Append(temp);
                }
                if (lstemp[i].BedStatus == 2)
                {
                    string temp = "   <div class=\"col-sm-3\">" +
                                                       " <div class=\"info-box\">" +
                                                       "     <span class=\"info-box-icon bg-red\"><i class=\"fa fa-bed\"></i></span>" +
                                                           " <div class=\"info-box-content\">" +
                                                               " <span class=\"info-box-text text-center\">" + lstemp[i].Room.ToString() + "</span>" +
                                                                "<span><i class=\"fa fa-male\"></i>&nbsp;&nbsp;&nbsp;" + lstemp[i].Ward.ToString() + "<br />" +
                                                               " </span>" +
                                                               " <span><i class=\"fa fa-wheelchair\"></i>&nbsp;&nbsp;" + lstemp[i].Block.ToString() + "<br />" +
                                                              "  </span>" +
                                                              "  <span><i class=\"fa fa-institution\"></i>&nbsp; " + lstemp[i].Floor1.ToString() + "<br />" +
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
                                                               " <span class=\"info-box-text text-center\">" + lstemp[i].Room.ToString() + "</span>" +
                                                                "<span><i class=\"fa fa-male\"></i>&nbsp;&nbsp;&nbsp;" + lstemp[i].Ward.ToString() + "<br />" +
                                                               " </span>" +
                                                               " <span><i class=\"fa fa-wheelchair\"></i>&nbsp;&nbsp;" + lstemp[i].Block.ToString() + "<br />" +
                                                              "  </span>" +
                                                              "  <span><i class=\"fa fa-institution\"></i>&nbsp; " + lstemp[i].Floor1.ToString() + "<br />" +
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
                                                               " <span class=\"info-box-text text-center\">" + lstemp[i].Room.ToString() + "</span>" +
                                                                "<span><i class=\"fa fa-male\"></i>&nbsp;&nbsp;&nbsp;" + lstemp[i].Ward.ToString() + "<br />" +
                                                               " </span>" +
                                                               " <span><i class=\"fa fa-wheelchair\"></i>&nbsp;&nbsp;" + lstemp[i].Block.ToString() + "<br />" +
                                                              "  </span>" +
                                                              "  <span><i class=\"fa fa-institution\"></i>&nbsp; " + lstemp[i].Floor1.ToString() + "<br />" +
                                                                "</span>" +
                                                          "  </div>" +

                                                       " </div>" +

                                                    "</div>";
                    undermaintanence = undermaintanence + 1;
                    UIstring.Append(temp);
                }


            }
            BedDataliterals.Text = "" + UIstring;
            litDischarge.Text = "" + disready;
            litOccupied.Text = "" + occupied;
            litVacant.Text = "" + vacant;
            litMantenence.Text = "" + undermaintanence;
        }
        private List<BedMasterData> GetBedType(int p)
        {
            BedMasterData objFloorMasterData = new BedMasterData();
            BedMasterBO objBlockMasterBO = new BedMasterBO();
            objFloorMasterData.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objFloorMasterData.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objFloorMasterData.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            objFloorMasterData.Room = txt_room.Text == "" ? null : txt_room.Text;
            objFloorMasterData.BedStatus = Convert.ToInt16(ddl_bed_status.SelectedValue == "0" ? null : ddl_bed_status.SelectedValue);
            objFloorMasterData.IsActive = true;
            return objBlockMasterBO.SearchBedTypeDetails(objFloorMasterData);
        }
    }
}