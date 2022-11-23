<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="NurseDashboard.aspx.cs" Inherits="Mediqura.Web.MedDashboard.NurseDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <div class="col-sm-12">
        <asp:Literal ID="BedDataliterals" runat="server"></asp:Literal>
    </div>
    <script type="text/javascript">
        var IPNO = "";
        function loadIp(ip) {
            IPNO = ip;

        }
        $(function () {
            $.contextMenu({
                selector: '#right-click',
                callback: function (key, options) {

                    switch (key) {

                        case "1":
                            if (IPNO.charAt(0) == "i" || IPNO.charAt(0) == "I") {
                                window.open("../MedBills/ServiceList.aspx?IP=" + IPNO, "_self");
                            } else {
                                window.open("../MedEmergency/EmergencyServiceRecord.aspx?EM=" + IPNO, "_self");
                            }

                            break;

                        case "2":
                            if (IPNO.charAt(0) == "i" || IPNO.charAt(0) == "I") {
                                window.open("../MedBills/IPDrugRequest.aspx?IP=" + IPNO, "_self");
                            } else {
                                window.open("../MedEmergency/EmergencyDrugRecord.aspx?EM=" + IPNO, "_self");
                            }
                            break;
                        case "3":
                            if (IPNO.charAt(0) == "i" || IPNO.charAt(0) == "I") {
                                window.open("../MedBills/Reports/ReportViewer.aspx?option=InterimBill&IPNo=" + IPNO)
                                break;
                            }
                    }


                },
                items: {
                    "1": { name: "Service Record" },
                    "2": { name: "Drug Record" },
                    "3": { name: "Service List" }


                }
            });

        });
        $(function () {
            $.contextMenu({
                selector: '#ot-right-click',
                callback: function (key, options) {

                    switch (key) {

                        case "1":
                            if (IPNO.charAt(0) == "i" || IPNO.charAt(0) == "I") {
                                window.open("../MedOT/OTRegistration.aspx?IP=" + IPNO, "_self");
                            }

                            break;

                        case "2":
                            if (IPNO.charAt(0) == "i" || IPNO.charAt(0) == "I") {
                                window.open("../MedBills/OTservices.aspx?IP=" + IPNO, "_self");
                            }
                            break;
                        case "3":
                            if (IPNO.charAt(0) == "i" || IPNO.charAt(0) == "I") {
                                window.open("../MedBills/Reports/ReportViewer.aspx?option=InterimBill&IPNo=" + IPNO)
                                break;
                            }
                    }


                },
                items: {
                    "1": { name: "OT Registration" },
                    "2": { name: "OT Service Record" },
                    "3": { name: "Service List" }


                }
            });

        });
    </script>
</asp:Content>
