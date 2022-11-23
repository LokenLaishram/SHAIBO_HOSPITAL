<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DischargeReportViewer.aspx.cs" Inherits="Mediqura.Web.MedIPD.DischargeReportViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <script src="../Scripts/jquery3.1.1.min.js"></script>
    <script src="../Scripts/jQuery.print.js"></script>
    <script>

        function print() {

            $.print("#report");
        }
    </script>
    <style>
        .shadow {
            background-color: #FFF;
            margin-left: 15%;
            margin-right: 15%;
            padding: 30px;
            -webkit-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            -moz-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            -ms-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            -o-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            -webkit-transition: all 0.25s ease-in-out;
            -moz-transition: all 0.25s ease-in-out;
            -ms-transition: all 0.25s ease-in-out;
            -o-transition: all 0.25s ease-in-out;
            transition: all 0.25s ease-in-out;
        }
        .print-btn {
        position: fixed;
    float: right;
    right: 33px;
    bottom: 12px;
    background: #a2e4a7;
    border-color: #5e735e;
    padding-left: 30px;
    padding-right: 30px;
    padding-top: 5px;
    padding-bottom: 5px;
    font-size: 15px;
    color: #344439;
    font-weight: 600;
    cursor:pointer;
        }
        body {
            background-color: #f7f7f7;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="shadow">
            <div id="report">
                <asp:Literal runat="server" ID="ltReport"></asp:Literal>
            </div>
        </div>

    </form>
    <button class="print-btn" onclick="print();">Print</button>
</body>
</html>